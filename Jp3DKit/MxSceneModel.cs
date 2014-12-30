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
using Jp3DKit.DecorateModel;
using Jp3DKit.ObjModel;

namespace Jp3DKit
{

    /// <summary>
    /// loader model data from .obj
    /// </summary>
    public class MxSceneModel : JpCompositeModel3D
    {
        private JPViewport3DX vp;
        private SelectedDecorateModel3D hoveredCurcle;
        private SelectedDecorateModel3D selectedCurcle;
        private Dictionary<string,MxModel3D> mxModelsDict;
        private Point3D selectPoint;

        /// <summary>
        /// 存储当前墓穴ID
        /// </summary>
        public string CurrentMxID { get; set; }
        public string CurrentMqID { get; set; }

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
            bool hit = false;
            var result = new HitTestResult();
            result.Distance = double.MaxValue;
            foreach (var item in mxModelsDict)
            {
                var mxsBounds = item.Value.Bounds;
                if (rayWS.Intersects(ref mxsBounds))  //判断是否与该组墓穴合成框相交
                {
                    if (item.Value.Instances != null)//逐个与墓穴实例对象比较相交
                    {
                        foreach (var modeinfo in item.Value.Instances)
                        {
                            var b = this.Bounds;
                            float d;
                            var bounds = BoundingBox.FromPoints(item.Value.ModelVertices.Select(x => Vector3.TransformCoordinate(x.Position, modeinfo.ModelPos)).ToArray());
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
                                        result.Tag = item.Value.Tag.ToString() + ":" + modeinfo.ModelID + ":Matrix:" + modeinfo.ModelPos.ToMatrix3D().ToString();
                                    }
                                    hit = true;
                                }
                            }
                        }
                        if (hit)
                        {
                            hits.Add(result);
                        }
                    }
                    else
                    {
                        return base.HitTest(rayWS, ref hits);
                    }
                }
            }
            return false;
        }
       // [Obsolete]
        //public JpMxModel3D(JPViewport3DX vp, string modelFileName, List<Entity2ModelInfo> instances, string tag)
        //{
        //    //todo:: IntCollection.Parse();需要研究它的原理

        //    selectCurcle = new MeshGeometryModel3D();
        //    HelixToolkit.Wpf.SharpDX.MeshGeometry3D mg = new HelixToolkit.Wpf.SharpDX.MeshGeometry3D();
        //    mg.Indices = IntCollection.Parse(selectGeometryIndiceStr); // selectGeometryIndiceStr.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(i => int.Parse(i));
        //    mg.Positions = Vector3Collection.Parse(selectGeometryStr);// Vector3ArrayConverter.FromString(selectGeometryStr);

        //    mg.Normals = mg.Positions;// mg.Positions.Select(o => Vector3.UnitY).ToArray();

        //    this.selectCurcle.Material = HelixToolkit.Wpf.SharpDX.PhongMaterials.Blue;
        //    selectCurcle.Geometry = mg;
        //    this.selectCurcle.Material = HelixToolkit.Wpf.SharpDX.PhongMaterials.Blue;
        //    this.Children.Add(selectCurcle);
        //    selectCurcle.Visibility = System.Windows.Visibility.Hidden;

        //    //mxModel = new MxModel3D(modelFileName);
        //    ////mxModel.ModelFileName = modelFileName;
        //    //mxModel.Instances = instances;
        //    //mxModel.Tag = tag;
        //    //this.Tag = tag;
        //    //if (mxModel.Instances.Count() > 0)
        //    //    this.Children.Add(mxModel);
        //    //DrawBoundBox();
        //}

        public MxSceneModel(JPViewport3DX vp, Dictionary<string, List<Entity2ModelInfo>> dict)
        {
            mxModelsDict = new Dictionary<string,MxModel3D>();
            //todo:: IntCollection.Parse();需要研究它的原理
            this.vp = vp;
            //create  SelectedDecorateModel3D
            this.selectedCurcle = new SelectedDecorateModel3D() { IsAnimation = true };
            this.hoveredCurcle = new SelectedDecorateModel3D() { IsAnimation = false };
            this.Children.Add(hoveredCurcle);
            this.Children.Add(selectedCurcle);

            //create mxModels
           foreach (var item in dict)
            //var item = dict.Last();
            //var item = dict.Single(o => o.Key == "MQ:6eb58029-254f-4952-9d0a-c7acd1eddeef:DS");
            //var item = dict.Single(o => o.Key == "MQ:f5ddad78-8185-4ecd-8a79-117aea643796:DS");
            {
                DispMxModelInstanceDictItem( item);
            }   
        }
        private  void DispMxModelInstanceDictItem( KeyValuePair<string, List<Entity2ModelInfo>> item, bool forceAttach = false)
        {
            string modelfilename;
            switch (item.Key.Substring(item.Key.Length - 2, 2))
            {
                case "DS": modelfilename = "mx0.obj"; break;
                case "YS": modelfilename = "mx1.obj"; break;
                default:
                    modelfilename = "mx.obj";
                    break;
            }           
                #region 墓穴对象使用实例模式
                var mxModel = new MxModel3D(modelfilename);
                //mxModel.ModelFileName = modelFileName;
                mxModel.Instances = item.Value;
                mxModel.Tag = item.Key;
                this.Tag = "JpMxModel3D";
                this.mxModelsDict.Add(item.Key, mxModel);
                if (mxModel.Instances.Count() > 0)
                    this.Children.Add(mxModel);
                #endregion
                #region 墓穴对象不使用实例模式
                //JpObjReader reader = new JpObjReader();
                //var bb = reader.Read(AppDomain.CurrentDomain.BaseDirectory + @"3DModel\mx.obj");
                //foreach (var mxmatrix in item.Value)
                //{
                //    var models = new ObjModel3D(bb);
                //    //var models = new MxModel3D(modelfilename);
                //    models.PushMatrix(mxmatrix.ModelPos);
                //    models.Tag = item.Key;
                //    this.Children.Add(models);
                //}
                #endregion           
        }
        /// <summary>
        /// 重设MxModel3D的墓穴实例
        /// 用于添加、删除或修改墓穴时，重设墓穴的显示；
        /// </summary>
        /// <param name="areaTag"></param>
        /// <param name="list"></param>
        public void ResetMxModelInstances(string mxAreaClassifyTag, List<Entity2ModelInfo> list)
        {            
            MxModel3D mxModel;
            mxModelsDict.TryGetValue(mxAreaClassifyTag, out mxModel);
            mxModel.Instances=null;
            mxModel.Instances = list;            
        }

       
        /// <summary>
        /// 处理鼠标点击，改变当前墓穴，显示当前墓穴图标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void OnMouse3DUp(object sender, RoutedEventArgs e)
        {
            base.OnMouse3DUp(sender, e);

            //System.Diagnostics.Debug.WriteLine("OnMouse3DUp");
            MouseUp3DEventArgs ee = (MouseUp3DEventArgs)e;
            string mxtag = ee.HitTestResult.Tag.ToString();
            //var bb = mxtag.Substring(mxtag.IndexOf("Matrix") + 6).Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(x => float.Parse(x)).ToArray();
            var cc = mxtag.Split(new char[] { ':' });
            var bb = cc[6].Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(x => float.Parse(x)).ToArray();
            Matrix matrix = new Matrix(bb);
            //MouseUp3DEventArgs ee = (MouseUp3DEventArgs)e;
            //    string mxtag = ee.HitTestResult.Tag.ToString();
            //    Matrix matrix = Matrix.Translation(ee.HitTestResult.PointHit.ToVector3());
                Point3D newSelectPoint = matrix.TranslationVector.ToPoint3D();

                this.selectedCurcle.PositionPoint = newSelectPoint.ToVector3D();
                //this.hoveredCurcle.Transform = CreateAnimatedTransform1(this.selectPoint.ToVector3D(), new Vector3D(0, 1, 0), 3);
                this.selectedCurcle.IsActive = true;
                this.CurrentMxID = cc[4];
                this.CurrentMqID = cc[1];
                this.RaiseEvent(new CurrentMxChangedEventArgs(JPViewport3DX.CurrentMxChangedEvent, this.CurrentMqID, this.CurrentMxID));
        }
     /// <summary>
     /// 处理鼠标over时，显示当前over图标
     /// </summary>
     /// <param name="sender"></param>
     /// <param name="e"></param>
        public override void OnMouseMoveOver3D(object sender, RoutedEventArgs e)
        {
            MouseMoveOver3DEventArgs ee = (MouseMoveOver3DEventArgs)e;
            if (ee.InOut)
            {
                string mxtag = ee.Hit.Tag.ToString();
                var bb = mxtag.Substring(mxtag.IndexOf("Matrix:") + 7 ).Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(x => float.Parse(x)).ToArray();
                Matrix matrix = new Matrix(bb);
                Point3D newSelectPoint = matrix.TranslationVector.ToPoint3D();
                if (this.hoveredCurcle.IsActive && this.selectPoint == newSelectPoint)
                    return;
                this.selectPoint = newSelectPoint;
                this.hoveredCurcle.PositionPoint = this.selectPoint.ToVector3D();
                //this.hoveredCurcle.Transform = CreateAnimatedTransform1(this.selectPoint.ToVector3D(), new Vector3D(0, 1, 0), 3);
                this.hoveredCurcle.IsActive = true;
                //this.Children.Add(selectCurcle);
            }
            else
            {
                this.hoveredCurcle.IsActive = false;
                // selectCurcle.IsVisible = false;
            }
        }

        /// <summary>
        /// The on children changed.
        /// </summary>
        protected virtual void OnChildrenChanged()
        {
            //this.Children.Clear();
            ////this.Children.Add(selectCurcle);
            //if (mxModel.Instances.Count() > 0)
            //    this.Children.Add(mxModel);

        }

        public override bool HitTest(Ray rayWS, ref List<HitTestResult> hits)
        {
            //bool hit = false;
            //foreach (var c in this.Children)
            //{
            //    var hc = c as IHitable;
            //    if (hc != null)
            //    {
            //        if (hc.HitTest(ray, ref hits))
            //        {
            //            hit = true;
            //        }
            //    }
            //}
            //这里处理鼠标命中mxModel时将hits对象列表中最后一个替换为当前对象也即JpMxModel3D
           //这样当前对象即可处理onMouse3dDown这样的事件
            //if (hit)
            //{
            //    var lastHit = hits[hits.Count - 1];
            //    lastHit.ModelHit = this;
            //    hits[hits.Count - 1] = lastHit;
            //}
            //return hit;


            if (this.Visibility == Visibility.Collapsed)
            {
                return false;
            }
            if (this.IsMouseMoveHitTestVisible == false)
            {
                return false;
            }
            bool hit = false;
            var result = new HitTestResult();
            result.Distance = double.MaxValue;
            int i = 0;
            foreach (var item in mxModelsDict)
            {
                var mxsBounds = item.Value.Bounds;                
                if (rayWS.Intersects(ref mxsBounds))  //判断是否与该组墓穴合成框相交
                {
                    if (item.Value.Instances != null)//逐个与墓穴实例对象比较相交
                    {
                        foreach (var modeinfo in item.Value.Instances)
                        {
                            var b = this.Bounds;
                            float d;
                            var bounds = BoundingBox.FromPoints(item.Value.ModelVertices.Select(x => Vector3.TransformCoordinate(x.Position, modeinfo.ModelPos)).ToArray());
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
                                        result.Tag = item.Value.Tag.ToString() + ":" + modeinfo.ModelID + ":Matrix:" + modeinfo.ModelPos.ToMatrix3D().ToString();
                                    }
                                    hit = true;
                                }
                            }
                        }
                        if (hit)
                        {
                            hits.Add(result);
                        }
                    }
                    else
                    {
                        return base.HitTest(rayWS, ref hits);
                    }
                }
            }
            return false;
        }

        
        ///利用d3d方式实现动画 obsolute
        //Int64 lastTime;
        //Int64 degree;
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
    }
}
