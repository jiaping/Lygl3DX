using SharpDX;
using SharpDX.Direct3D11;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;


using HelixToolkit.Wpf.SharpDX;
using HelixToolkit.Wpf;
using System;
using Buffer = SharpDX.Direct3D11.Buffer;
using SharpDX.DXGI;
using SharpDX.Direct3D;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf.SharpDX.Core;
using System.Globalization;
using HelixToolkit.Wpf.SharpDX.Utilities;


namespace Jp3DKit
{

    /// <summary>
    /// loader model data from .obj
    /// </summary>
    public class JpMxModel3D : JpCompositeModel3D
    {
        Int64 lastTime;
        Int64 degree;
        private MeshGeometryModel3D selectCurcle;
        public MxModel3D mxModel;
        private Point3D selectPoint;
        string selectGeometryStr = "0.207055,0,0.772741,-9.79717E-17,0,0.8,-1.22465E-16,0,1,0.258819,0,0.965926,0.5,0,0.866025,0.707107,0,0.707107,0.866025,0,0.5,0.965926,0,0.258819,0.772741,0,0.207055,0.69282,0,0.4,0.565685,0,0.565685,0.4,0,0.69282,1,0,1.83697E-16,0.965926,0,-0.258819,0.866025,0,-0.5,0.707107,0,-0.707107,0.5,0,-0.866025,0.258819,0,-0.965926,0.207055,0,-0.772741,0.4,0,-0.69282,0.565685,0,-0.565685,0.69282,0,-0.4,0.772741,0,-0.207055,0.8,0,1.46958E-16,-0.772741,0,0.207055,-0.8,0,-4.89859E-17,-1,0,-6.12323E-17,-0.965926,0,0.258819,-0.866025,0,0.5,-0.707107,0,0.707107,-0.5,0,0.866025,-0.258819,0,0.965926,-0.207055,0,0.772741,-0.4,0,0.69282,-0.565685,0,0.565685,-0.69282,0,0.4,-0.866025,0,-0.5,-0.965926,0,-0.258819,-0.772741,0,-0.207055,-0.69282,0,-0.4,-0.565685,0,-0.565685,-0.4,0,-0.69282,-0.207055,0,-0.772741,0,0,-0.8,0,0,-1,-0.258819,0,-0.965926,-0.5,0,-0.866025,-0.707107,0,-0.707107";
        string selectGeometryIndiceStr = "0,1,2,2,3,4,4,5,6,6,7,8,11,0,2,2,4,6,6,8,9,10,11,2,6,9,10,10,2,6,23,12,13,13,14,15,15,16,17,17,18,19,22,23,13,13,15,17,17,19,20,21,22,13,13,17,20,20,21,13,24,25,26,26,27,28,28,29,30,30,31,32,35,24,26,26,28,30,30,32,33,34,35,26,30,33,34,34,26,30,47,36,37,37,38,39,42,43,44,44,45,46,46,47,37,37,39,40,41,42,44,44,46,37,37,40,41,41,44,37";

        public override bool MouseMoveHitTest(Ray rayWS, ref List<HitTestResult> hits)
        {
            if (this.Visibility == Visibility.Collapsed)
            {
                return false;
            }
            if (this.IsMouseMoveHitTestVisible == false)
            {
                return false;
            }
            var mxsBounds = this.mxModel.Bounds;
            if (rayWS.Intersects(ref mxsBounds))  //判断是否与该组墓穴合成框相交
            {
                if (this.mxModel.Instances != null)//逐个与墓穴实例对象比较相交
                {
                    bool hit = false;
                    var result = new HitTestResult();
                    result.Distance = double.MaxValue;
                    foreach (var modeinfo in this.mxModel.Instances)
                    {
                        var b = this.Bounds;
                        float d;
                        var bounds = BoundingBox.FromPoints(this.mxModel.Geometry.Positions.Select(x => Vector3.TransformCoordinate(x, modeinfo.ModelPos)).ToArray());
                        if (rayWS.Intersects(ref bounds))
                        {
                            if (Collision.RayIntersectsBox(ref rayWS, ref bounds, out d))
                            {
                                if (d < result.Distance)
                                {
                                    result.IsValid = true;
                                    result.ModelHit = this;
                                    // transform hit-info to world space now:
                                    result.PointHit = (rayWS.Position + (rayWS.Direction * d)).ToPoint3D();
                                    result.Distance = d;
                                    //var n = Vector3.Cross(p1 - p0, p2 - p0);
                                    //n.Normalize();
                                    //// transform hit-info to world space now:
                                    //result.NormalAtHit = n.ToVector3D();// Vector3.TransformNormal(n, m).ToVector3D();
                                    result.Tag = this.mxModel.Tag.ToString() + ":" + modeinfo.ModelID + ":Matrix" + modeinfo.ModelPos.ToMatrix3D().ToString();
                                }
                                hit = true;
                            }
                        }
                    }
                    if (hit)
                    {
                        hits.Add(result);
                    }
                    return hit;
                }
                else
                {
                    return base.HitTest(rayWS, ref hits);
                }
            }
            return false;
        }

        public JpMxModel3D(JPViewport3DX vp, string modelFileName, List<Entity2ModelInfo> instances, string tag)
        {
            //todo:: IntCollection.Parse();需要研究它的原理

            selectCurcle = new MeshGeometryModel3D();
            HelixToolkit.Wpf.SharpDX.MeshGeometry3D mg = new HelixToolkit.Wpf.SharpDX.MeshGeometry3D();
            mg.Indices = IntCollection.Parse(selectGeometryIndiceStr); // selectGeometryIndiceStr.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(i => int.Parse(i));
            mg.Positions = Vector3Collection.Parse(selectGeometryStr);// Vector3ArrayConverter.FromString(selectGeometryStr);

            mg.Normals = mg.Positions;// mg.Positions.Select(o => Vector3.UnitY).ToArray();

            this.selectCurcle.Material = HelixToolkit.Wpf.SharpDX.PhongMaterials.Blue;
            selectCurcle.Geometry = mg;
            this.selectCurcle.Material = HelixToolkit.Wpf.SharpDX.PhongMaterials.Blue;
            this.Children.Add(selectCurcle);
            selectCurcle.Visibility = System.Windows.Visibility.Hidden;

            mxModel = new MxModel3D(modelFileName);
            //mxModel.ModelFileName = modelFileName;
            mxModel.Instances = instances;
            mxModel.Tag = tag;
            this.Tag = tag;
            if (mxModel.Instances.Count() > 0)
                this.Children.Add(mxModel);
            //DrawBoundBox();
        }



        private void DrawBoundBox()
        {
            LineGeometryModel3D LineBoundBox = new LineGeometryModel3D();

            LineBoundBox.Geometry = LineBuilder.GenerateBoundingBox(this.mxModel.Bounds);
            LineBoundBox.Color = new Color(255, 0, 0, 1);
            this.Children.Add(LineBoundBox);
        }


        public override void OnMouseMoveOver3D(object sender, RoutedEventArgs e)
        {
            MouseMoveOver3DEventArgs ee = (MouseMoveOver3DEventArgs)e;
            if (ee.InOut)
            {
                string mxtag = ee.Hit.Tag.ToString();
                var bb = mxtag.Substring(mxtag.IndexOf("Matrix") + 6).Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(x => float.Parse(x)).ToArray();
                Matrix matrix = new Matrix(bb);
                Point3D newSelectPoint = matrix.TranslationVector.ToPoint3D();
                if (this.selectCurcle.Visibility == System.Windows.Visibility.Visible && this.selectPoint == newSelectPoint)
                    return;
                this.selectPoint = newSelectPoint;

                //HelixToolkit.Wpf.SharpDX.MeshGeometry3D mg = new HelixToolkit.Wpf.SharpDX.MeshGeometry3D();
                //mg.Indices = IntCollection.Parse(selectGeometryIndiceStr); //  selectGeometryIndiceStr.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(i => int.Parse(i)).ToArray();
                //mg.Positions =new  Vector3Collection( Vector3Collection.Parse(selectGeometryStr).Select(o => Vector3.TransformCoordinate(o, matrix)));
                //mg.Normals = new Vector3Collection(mg.Positions.Select(o => Vector3.UnitY));

                //selectCurcle.Visibility = System.Windows.Visibility.Hidden;
                //this.Children.Remove(selectCurcle);
                //selectCurcle.Geometry = mg;                

                //this.selectCurcle.Material =
                //    new PhongMaterial
                //        {
                //            Name = "Red",
                //            AmbientColor = PhongMaterials.ToColor(0, 0, 0, 1.0),
                //            DiffuseColor = PhongMaterials.ToColor(0.1313f, 0.7764, 0.1343f, 1f),
                //            SpecularColor = PhongMaterials.ToColor(0, 0, 0, 1.0),
                //            EmissiveColor = PhongMaterials.ToColor(0.0, 0.0, 0.0, 1.0),
                //            SpecularShininess = 12.8f,
                //        }.Clone();

                selectCurcle.Visibility = System.Windows.Visibility.Visible;
                selectCurcle.Transform = CreateAnimatedTransform1(this.selectPoint.ToVector3D(), new Vector3D(0, 1, 0), 3);
                //this.Children.Add(selectCurcle);
            }
            else
            {
                selectCurcle.Visibility = System.Windows.Visibility.Hidden;
                // selectCurcle.IsVisible = false;
            }
        }
        ///利用d3d方式实现动画
        //public override void Update(TimeSpan timeSpan)
        //{
        //    ///如果光圈不可见，则不需要更新
        //    if (this.selectCurcle.Visibility == System.Windows.Visibility.Hidden) return;
        //    base.Update(timeSpan);
        //    Int64 time = (Int64)(timeSpan.TotalMilliseconds - lastTime);
        //    if (time / 50 > 1)
        //    {
        //        lastTime = (Int64)timeSpan.TotalMilliseconds;

        //        degree += (Int64)(time / 50) * 10;
        //        this.selectCurcle.Transform = (new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), degree), this.selectPoint));
        //    }
        //}

        /// <summary>
        /// 利用wpf方式实现动画
        /// </summary>
        /// <param name="translate"></param>
        /// <param name="axis"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        private System.Windows.Media.Media3D.Transform3D CreateAnimatedTransform1(Vector3D translate, Vector3D axis, double speed = 4)
        {
            var lightTrafo = new System.Windows.Media.Media3D.Transform3DGroup();
            lightTrafo.Children.Add(new System.Windows.Media.Media3D.TranslateTransform3D(translate));

            var rotateAnimation = new System.Windows.Media.Animation.Rotation3DAnimation
            {
                RepeatBehavior = System.Windows.Media.Animation.RepeatBehavior.Forever,

                By = new System.Windows.Media.Media3D.AxisAngleRotation3D(axis, 90),
                Duration = TimeSpan.FromSeconds(speed / 4),
                IsCumulative = true,
            };

            var rotateTransform = new System.Windows.Media.Media3D.RotateTransform3D();
            rotateTransform.CenterX = translate.X; rotateTransform.CenterY = translate.Y; rotateTransform.CenterZ = translate.Z;
            rotateTransform.BeginAnimation(System.Windows.Media.Media3D.RotateTransform3D.RotationProperty, rotateAnimation);
            lightTrafo.Children.Add(rotateTransform);

            return lightTrafo;
        }
        /// <summary>
        /// The on children changed.
        /// </summary>
        protected virtual void OnChildrenChanged()
        {
            this.Children.Clear();
            //this.Children.Add(selectCurcle);
            if (mxModel.Instances.Count() > 0)
                this.Children.Add(mxModel);

        }


        public override bool HitTest(Ray ray, ref List<HitTestResult> hits)
        {
            bool hit = false;
            foreach (var c in this.Children)
            {
                var hc = c as IHitable;
                if (hc != null)
                {
                    if (hc.HitTest(ray, ref hits))
                    {
                        hit = true;
                    }
                }
            }
            return hit;
        }

    }
}
