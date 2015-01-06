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
                                        result.Tag = modeinfo.MxID + ":Matrix:" + modeinfo.ModelPos.ToMatrix3D().ToString();
                                       // result.Tag = item.Value.Tag.ToString() + ":" + modeinfo.MxID + ":Matrix:" + modeinfo.ModelPos.ToMatrix3D().ToString();
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

        public MxSceneModel(JPViewport3DX vp, Dictionary<string, List<MxModelInfo>> dict)
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
        private void DispMxModelInstanceDictItem(KeyValuePair<string, List<MxModelInfo>> item)
        {
            string modelfilename = item.Key.Split(new char[] { ':' })[1];  //{[f5ddad78-8185-4ecd-8a79-117aea643796:mx.obj

            #region 墓穴对象使用实例模式
            var mxModel = new MxModel3D(modelfilename);
            //mxModel.ModelFileName = modelFileName;
            mxModel.Instances = item.Value;
            mxModel.Tag = item.Key;
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
        public void ResetMxModelInstances(string mxAreaClassifyTag, List<MxModelInfo> list)
        {            
            MxModel3D mxModel;
            mxModelsDict.TryGetValue(mxAreaClassifyTag, out mxModel);
            if (mxModel == null)
            {
                mxModel = new MxModel3D(mxAreaClassifyTag.Split(new char[] { ':' })[1]);
                mxModelsDict.Add(mxAreaClassifyTag, mxModel);
                mxModel.Instances = list;
                this.Children.Add(mxModel);
            }
            else
            {
                mxModel.Instances = null;
                mxModel.Instances = list;
            }
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
            var bb = cc[4].Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(x => float.Parse(x)).ToArray();
            Matrix matrix = new Matrix(bb);
            //MouseUp3DEventArgs ee = (MouseUp3DEventArgs)e;
            //    string mxtag = ee.HitTestResult.Tag.ToString();
            //    Matrix matrix = Matrix.Translation(ee.HitTestResult.PointHit.ToVector3());
                Point3D newSelectPoint = matrix.TranslationVector.ToPoint3D();

                this.selectedCurcle.PositionPoint = newSelectPoint.ToVector3D();
                //this.hoveredCurcle.Transform = CreateAnimatedTransform1(this.selectPoint.ToVector3D(), new Vector3D(0, 1, 0), 3);
                this.selectedCurcle.IsActive = true;
                this.CurrentMxID = cc[2];
                this.CurrentMqID = cc[0];
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
                                        result.Tag = item.Value.Tag.ToString() + ":" + modeinfo.MxID + ":Matrix:" + modeinfo.ModelPos.ToMatrix3D().ToString();
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
        /// <summary>
        /// 更新mx的显示模型
        /// 只有当显示模型发生改变时，也即显示的分类发生改变时才有必要调用
        /// </summary>
        /// <param name="areaID"></param>
        /// <param name="oldMMI"></param>
        /// <param name="currentMMI"></param>
        public void UpdateMxModel(string areaID,MxModelInfo oldMMI,MxModelInfo currentMMI)
        {
            MxModel3D mxModel;
            //先移除原来的
            string classifyName=MxModelInfo.GetAreaMxsClassifyName(areaID,oldMMI);
            mxModelsDict.TryGetValue(classifyName, out mxModel);
            if (mxModel == null) throw new Exception("没找到分类名为："+classifyName+"的墓穴");
                List<MxModelInfo> list = (List<MxModelInfo>)mxModel.Instances;
                list.Remove(list.Find(o => o.MxID == oldMMI.MxID));
                mxModel.Instances = null;
                mxModel.Instances = list;
            //将新的添加到相应的显示模块中           
                string newClassifyName = MxModelInfo.GetAreaMxsClassifyName(areaID, currentMMI);
                mxModelsDict.TryGetValue(newClassifyName, out mxModel);
                if (mxModel == null)
                {
                    mxModel = new MxModel3D(currentMMI.ModelFileName);
                    mxModel.Tag = newClassifyName;
                    mxModelsDict.Add(newClassifyName, mxModel);
                    mxModel.Instances = new List<MxModelInfo>(new [] {currentMMI});
                    this.Children.Add(mxModel);
                }
                else
                {
                    List<MxModelInfo> list1 = (List<MxModelInfo>)mxModel.Instances;
                    list1.Add(currentMMI);
                    mxModel.Instances = null;
                    mxModel.Instances = list1;
                }
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
