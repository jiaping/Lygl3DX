using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lygl.DalLib;
using System.Windows;
using System.Windows.Media.Imaging;
using System.ComponentModel;
using System.Windows.Media;
using Csla;
using Lygl.DalLib.Core;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using Lygl.UI.CommandMessage;
using Lygl.UI.Shell;
using Lygl.DalLib.NVL;
using Lygl.DalLib.Browse;
using Lygl.DalLib.Edit;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

using Jp3DKit;
using HelixToolkit.Wpf.SharpDX;
using MeshBuilder = HelixToolkit.Wpf.MeshBuilder;
using GeometryModel3D = System.Windows.Media.Media3D.GeometryModel3D;
using Camera = HelixToolkit.Wpf.SharpDX.Camera;
using PerspectiveCamera = HelixToolkit.Wpf.SharpDX.PerspectiveCamera;
using MeshGeometry3D = HelixToolkit.Wpf.SharpDX.MeshGeometry3D;
using Matrix = SharpDX.Matrix;
using SharpDX;
using System.Windows.Input;
using SharpDX.D3DCompiler;
using System.Windows.Documents;
using Point = System.Windows.Point;
using Csla.Core;
using Jp3DKit.MouseDrawHandler;

namespace Lygl.UI.ViewModels
{
    #region Message
    [Export(typeof(ICommandMessage))]
    public class DrawMqMessage : CommandMessageBase
    {
        public DrawMqMessage()
            : base()
        {
            Name = "DrawMq"; Label = "添加墓区"; ToolTip = "添加墓区"; Group = "LyView";
            ToolTip = "添加墓区"; IsMainMenuItem = true; Category = "陵园视图";
        }
    }

    [Export(typeof(ICommandMessage))]
    public class DrawMxMessage : CommandMessageBase
    {
        public DrawMxMessage()
            : base()
        {
            Name = "DrawMx"; Label = "添加墓穴"; ToolTip = "添加墓穴"; Group = "LyView";
            ToolTip = "添加墓区"; IsMainMenuItem = true; Category = "陵园视图";
        }
    }
    [Export(typeof(ICommandMessage))]
    public class ModifyPosMessage : CommandMessageBase
    {
        public ModifyPosMessage()
            : base()
        {
            Name = "ModifyPos"; Label = "修改位置和角度"; ToolTip = "修改形状的位置和角度"; Group = "LyView";
            IsMainMenuItem = true; Category = "陵园视图";
        }
    }
    [Export(typeof(ICommandMessage))]
    public class DrawPathMessage : CommandMessageBase
    {
        public DrawPathMessage()
            : base()
        {
            Name = "DrawPath"; Label = "添加道路"; ToolTip = "添加道路"; Group = "LyView";
            IsMainMenuItem = true; Category = "陵园视图";
        }
    }
    [Export(typeof(ICommandMessage))]
    public class MoveShapeMessage : CommandMessageBase
    {
        public MoveShapeMessage()
            : base()
        {
            Name = "MoveShape"; Label = "移动形状"; ToolTip = "移动形状"; Group = "LyView";
            IsMainMenuItem = true; Category = "陵园视图";
        }
    }
    [Export(typeof(ICommandMessage))]
    public class ModifyMqMessage : CommandMessageBase
    {
        public ModifyMqMessage()
            : base()
        {
            Name = "ModifyMq"; Label = "修改墓区形状"; ToolTip = "修改墓区形状"; Group = "LyView";
            IsMainMenuItem = true; Category = "陵园视图";
        }
    }
    [Export(typeof(ICommandMessage))]
    public class ZoomOutMessage : CommandMessageBase
    {
        public ZoomOutMessage()
            : base()
        {
            Name = "ZoomOut"; Label = "缩放"; ToolTip = "缩放"; Group = "LyView";
            IsMainMenuItem = true; Category = "陵园视图";
        }
    }

   
    #endregion

    /// <summary>
    /// 实现主3D视图模块
    /// </summary>
    [Export(typeof(MainViewModel))]
    class MainViewModel : Lygl.UI.Framework.ViewModelBase.ScreenWithModel<AreaROL>,
        IHandle<DrawMqMessage>,
        IHandle<DrawMxMessage>,
        IHandle<ModifyPosMessage>,
        IHandle<DrawPathMessage>,
        IHandle<MoveShapeMessage>,
        IHandle<ZoomOutMessage>,
        IHandle<ModifyMqMessage>,
        IHandle<RefreshViewportMessage>
    {
        private JPViewport3DX _viewport;
        private Jp3DKit.DrawShapeRecord _drawShapeRecord;

        public Camera Camera {get;private set;}
        public Vector3 DirectionalLightDirection { get; private set; }
        public Transform3D DirectionalLightTransform { get; private set; }
        public Color4 DirectionalLightColor { get; private set; }
        public Color4 AmbientLightColor { get; private set; }
        
        public PhongMaterial ModelMaterial { get; private set; }
        public System.Windows.Media.Media3D.Transform3D ModelTransform { get; private set; }

        private OperateMode operateMode{get;set;}

        private JpMqModel3D currentMq;
        public MainViewModel()
        {
            IoC.Get<IEventAggregator>().Subscribe(this);
            //Model = AreaList.GetAreaList();
            //MxROL.UpdateAllMxStatus();
            DataPortal.Execute(new Lygl.DalLib.DBCommand.UpdateAllMxStatusCommand());
            Model = IoC.Get<IGlobalData>().Areas; // AreaROL.GetAreaROL();
            Camera = new PerspectiveCamera { Position = new Point3D(53, 23, 5), LookDirection = new Vector3D(-3, -3, -5), UpDirection = new Vector3D(1, 0, 0) };
            this.DirectionalLightColor = (Color4)SharpDX.Color.White;
            this.AmbientLightColor = (Color4)SharpDX.Color.LightGray;
            this.DirectionalLightDirection = new Vector3(0, -1, -1);
            //this.DirectionalLightTransform = new TranslateTransform3D(0.0, 200.0, 0.0);
            

            ModelMaterial = PhongMaterials.Glass;
            ModelTransform = System.Windows.Media.Media3D.Transform3D.Identity;

            operateMode = OperateMode.None;

        }

        protected override void OnDeactivate(bool close)
        {
            if (close) IoC.Get<IEventAggregator>().Unsubscribe(this);
            base.OnDeactivate(close);
        }
        protected override void OnActivate()
        {
            base.OnActivate();
        }

        /// <summary>
        /// 窗口加载过程中，完成初始化工作，业务处理的入口
        /// </summary>
        /// <param name="view"></param>
        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            //添加手势命令-Escape,用于取消操作
            (view as UIElement).CommandBindings.Add(new CommandBinding(JpViewport3DXCommands.Escape, this.EscapeHandler));
            (view as UIElement).InputBindings.Add(new KeyBinding(JpViewport3DXCommands.Escape, Key.Escape, ModifierKeys.None));
            //添加手势命令-Ctrl+S,用于保存操作
            (view as UIElement).CommandBindings.Add(new CommandBinding(JpViewport3DXCommands.ControlSave, this.ControlSaveHandler));
            (view as UIElement).InputBindings.Add(new KeyBinding(JpViewport3DXCommands.ControlSave, Key.S, ModifierKeys.Control));
            
            _viewport = (view as FrameworkElement).FindName("MainViewport") as JPViewport3DX;
            IoC.Get<IGlobalData>().ViewPort3DX = _viewport;
            _viewport.MouseDoubleClick += _viewport_MouseDoubleClick;
            _viewport.MouseLeftButtonUp += _viewport_MouseLeftButtonUp;
            _viewport.MouseMove += _viewport_MouseMove;
            

            #region 添加点光源
            //PointLight3D pointLight = new PointLight3D();
            //pointLight.Color = (Color4)SharpDX.Color.White;
            //pointLight.Range = 10;
            //Transform3D pointLightTransfo = CreateAnimatedTransform1(new Vector3D(-4, 10, 0), new Vector3D(0, 1, 0), 3);// new TranslateTransform3D(20.0, 5.0, 0.0);
            //pointLight.Transform = pointLightTransfo;
            //MeshGeometryModel3D pointLightSphere = new MeshGeometryModel3D();
            //HelixToolkit.Wpf.SharpDX.MeshBuilder mb = new HelixToolkit.Wpf.SharpDX.MeshBuilder();
            //mb.AddSphere(new Vector3(0.0f, 5.0f, 0.0f), 0.2);
            //pointLightSphere.Geometry = mb.ToMeshGeometry3D();
            //pointLightSphere.Material = PhongMaterials.Red;
            //pointLightSphere.Transform = pointLightTransfo;
            //this._viewport.Items.Add(pointLight);
            //this._viewport.Items.Add(pointLightSphere);
            #endregion

            ModelInstancesManager.LoadEntityModelInfo(IoC.Get<IGlobalData>().Areas);
            ModelInstancesManager.LoadModels(_viewport);
        }

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
            rotateTransform.BeginAnimation(System.Windows.Media.Media3D.RotateTransform3D.RotationProperty, rotateAnimation);
            lightTrafo.Children.Add(rotateTransform);

            return lightTrafo;
        }

        

        #region _viewport相关鼠标事件及相关业务逻辑处理
        /// <summary>
        /// 响应键盘事件，Ctrl+S
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlSaveHandler(object sender, ExecutedRoutedEventArgs e)
        {
            if (this.currentMq != null && this.currentMq.IsModify)
            {
                string mqID = this.currentMq.Tag.ToString();
                var ss = mqID.Split(new char[] { ':' });
                AreaEdit ae = AreaEdit.GetAreaEdit(Guid.Parse(ss[1]));
                ae.GeometryText = Vector3ArrayConverter.ConvertToString(this.currentMq.Positions);  //pg.ToString(new Lygl.UI.Framework.FormatProvider.GeometryIntFormatProvider());
                try
                {
                    var savable = ae as ISavable;
                   savable.Save();
                   this.currentMq.IsModify = false;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 响应键盘事件，Escape
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EscapeHandler(object sender, ExecutedRoutedEventArgs e)
        {
            if (operateMode == OperateMode.DrawMx)
            {
                this._viewport.Cursor = Cursors.Arrow;
                operateMode = OperateMode.None;
            }
            if (operateMode == OperateMode.DrawPolygon)
            {
                this._viewport.Cursor = Cursors.Arrow;
                if (_drawShapeRecord.IsDraw) _drawShapeRecord.IsCancel = true;
                if(_drawShapeRecord.Model!=null) this._viewport.Items.Remove(_drawShapeRecord.Model);
                _drawShapeRecord.Clear();
                operateMode = OperateMode.None;
                this._viewport.ReleaseMouseCapture();
            }
            if (this.currentMq != null && this.currentMq.IsModify)
            {
                this.currentMq.RevertPoints();
                this.currentMq.IsModify = false;
            }
        }

        void _viewport_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(_viewport);
            switch (operateMode)
            {
                case OperateMode.None:
                    break;
                case OperateMode.DrawPolygon:
                    break;
                case OperateMode.DrawPolygonOld:
                    #region addPolygon
                    _drawShapeRecord.IsDraw = true;
                    var vv = _viewport.FindHits(p);
                    if (vv.Count > 0)
                    { 
                        if (_drawShapeRecord.Model == null)
                        {
                            _drawShapeRecord.ShapeType = "Polygon";
                            _drawShapeRecord.AddPoint(vv[0].PointHit);
                            this._viewport.Items.Add(_drawShapeRecord.Model);
                            _drawShapeRecord.Model.Attach(this._viewport.RenderHost);
                        }
                        else
                        {
                            _drawShapeRecord.AddPoint(vv[0].PointHit);
                            _viewport.Attach(_drawShapeRecord.Model);
                        }
                    }
                    this._viewport.CaptureMouse();
                    e.Handled = true;
                    #endregion
                    break;
                case OperateMode.DrawMx:
                    #region 
                    //查找Mx所在的区域
                    var hits=_viewport.FindHits(p);
                    AreaRO currentArea = null;
                    string areaID = "";
                    Vector3 hitGroundPoint=Vector3.Zero ;
                    foreach (var item in hits)
                    {
                            if (item.IsValid)
                            {
                                if (item.Tag != null)  //墓穴对象返回的tag
                                {
                                        return;
                                }
                                else
                                {
                                    if (item.ModelHit.Tag != null) //墓区对象返回的tag
                                    {
                                        var modelID = (string)item.ModelHit.Tag;
                                        var ss = modelID.Split(new char[] { ':' });
                                        //if (ss[0] == "AllMq")
                                        //{
                                            
                                        //    continue;
                                        //}
                                        if (ss[0] == "MQ")
                                        {
                                            areaID = ss[1];
                                            hitGroundPoint = new Vector3((float)item.PointHit.X, (float)item.PointHit.Y, (float)item.PointHit.Z);
                                            continue;
                                        }
                                    }
                                }
                            }
                            if (areaID != string.Empty && hitGroundPoint != Vector3.Zero) break;
                    }
                    if (areaID == string.Empty)
                    {
                        System.Windows.MessageBox.Show("请在墓区范围内空白处点击，确定新建墓穴！");
                        break;
                    }
                    currentArea = AreaROL.GetAreaROLByID(new Guid(areaID)).DefaultIfEmpty().First();
                    if (currentArea == null ) throw new Exception("不能在空区域添加墓穴");
                    
                    MxEdit newMx = MxEdit.NewMxEdit();
                    newMx.MxID = Guid.NewGuid();
                    newMx.MxName =currentArea.Name +"新建墓穴"+newMx.MxID.ToString();// currentArea.Name + "新建墓穴";
                    var pos=Matrix3D.Identity;
                    pos.OffsetX=hitGroundPoint.X;pos.OffsetY=hitGroundPoint.Y;pos.OffsetZ=hitGroundPoint.Z;pos.M44=1;
                    newMx.Pos =pos.ToString();
                    newMx.MxStatus = MxStatusNVL.GetMxStatusNVL().Value(0);      //表示初始状态 Convert.ToInt32(MxStatus.DaiShou);
                    newMx.MxType = MxTypeNVL.GetMxTypeNVL().Value(0); // Convert.ToInt32(MxType.DanRen);
                    newMx.MxStyle = MxStyleNVL.GetMxStyleNVL().Value(1);
                    newMx.AreaID = currentArea.AreaID;
                    newMx.Angle = currentArea.Angle;
                    operateMode = OperateMode.None;
                    this._viewport.Cursor = Cursors.Arrow;
                    //var bb = !EditMx(newMx);
                    MxEdit cloneMx = newMx.Clone();
                    newMx = cloneMx.Save();
                    IoC.Get<IGlobalData>().AreaMxsDictAdd(currentArea.AreaID);   //更新全局列表缓存
                    IoC.Get<IGlobalData>().CurrentMx = IoC.Get<IGlobalData>().GetMxRO(newMx.MxID);
                    //编辑形状数据
                    if (!EditMx(IoC.Get<IGlobalData>().CurrentMx, true) ?? false)
                    {
                        //撤消建立
                        newMx.Delete();
                        cloneMx = newMx.Clone();
                        newMx = cloneMx.Save();
                        IoC.Get<IGlobalData>().AreaMxsDictAdd(currentArea.AreaID);   //更新全局列表缓存
                        return;
                    }
                    Entity2ModelInfo emi = new Entity2ModelInfo(EntityType.MX, newMx.MxID);
                    emi.ModelPos = new Matrix(newMx.Pos.Split(new char[] { ',' }).Select(x => float.Parse(x)).ToArray());
                    ModelInstancesManager.AddMxToMxModel(emi, _viewport);    
#endregion
                    break;
                case OperateMode.DrawPath:
                    break;
                case OperateMode.Delete:
                    break;
                case OperateMode.MoveShape:
                    var hits1 = _viewport.FindHits(p);
                    foreach (var item in hits1)
                    {
                        if (item.IsValid && item.Tag != null)
                        {
                            if (ModelInstancesManager.SelectedModel.Visibility == Visibility.Visible) return;
                            StartModifyModel(item.Tag.ToString());
                            break;
                        }
                    }
                    e.Handled = true;
                    break;
                case OperateMode.Select:
                    break;
                case OperateMode.ModifyPolygon:
                    var obj = _viewport.FindHits(p);
                if (obj.Count > 0)
                {
                    foreach (var item in obj)
                    {
                        if (item.ModelHit is JpMqModel3D)
                        {
                            this.currentMq = item.ModelHit as JpMqModel3D;
                            ((JpMqModel3D)item.ModelHit).IsModify = true;
                            this.operateMode = OperateMode.None;
                        }
                    }
                }
                e.Handled = true;
                    break;
                default:
                    break;
            }
        }

        void _viewport_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = e.GetPosition(_viewport);
            switch (operateMode)
            {
                case OperateMode.None:
                    break;
                case OperateMode.DrawPolygon:
                    break;
                case OperateMode.DrawPolygonOld:
                    #region AddPolygon
                    if (_drawShapeRecord.IsCancel)
                    {
                        this._viewport.ReleaseMouseCapture();
                        operateMode = OperateMode.None;
                        _drawShapeRecord.IsDraw = false;
                        if (_drawShapeRecord.Model != null) this._viewport.Items.Remove(_drawShapeRecord.Model);
                        _drawShapeRecord.Clear();
                        _viewport.Cursor = Cursors.Arrow;
                        e.Handled = true;
                        return;
                    }
                    if (_drawShapeRecord.IsDraw)
                    {
                        var vv = _viewport.FindHits(p);
                        if (vv.Count > 0)
                        {
                            _drawShapeRecord.ReplaceLastPoint(vv[0].PointHit);
                            _viewport.Attach(_drawShapeRecord.Model);
                        }
                    }
                   
                    #endregion
                    break;
                case OperateMode.DrawMx:
                    break;
                case OperateMode.DrawPath:
                    break;
                case OperateMode.Delete:
                    break;
                case OperateMode.MoveShape:
                    break;
                case OperateMode.Select:
                    break;
                default:
                    break;
            }
        }

        private void StartModifyModel(string modelTag)
        {
            var ss = modelTag.Split(new char[] { ':' });
            if (ss[3] == "MX")
            {
                MxRO mx = IoC.Get<IGlobalData>().GetMxRO(new Guid(ss[1]), new Guid(ss[4]));
                IoC.Get<IGlobalData>().CurrentMx = mx;
                ModelInstancesManager.StartModifyModel(modelTag, _viewport);
            }
        }

        /// <summary>
        /// 将些事件由JPViewport3DX取出来，更好地与业务逻辑交互
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _viewport_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            switch (operateMode)
            {
                case OperateMode.None:
                    #region
                    if (e.ChangedButton == MouseButton.Left)
                    {
                        var hits = _viewport.FindHits(e.GetPosition(_viewport));
                        string modelID = null;
                        foreach (var item in hits)
                        {
                            if (item.IsValid)
                            {
                                if (item.Tag != null)  //墓穴对象返回的tag
                                {
                                    modelID = (string)item.Tag;
                                    var ss = modelID.Split(new char[] { ':' });
                                    if (ss[3] == "MX")    //TODO: if not =="Mx" 
                                    {
                                        MxRO mx = IoC.Get<IGlobalData>().GetMxRO(new Guid(ss[1]), new Guid(ss[4]));
                                        IoC.Get<IGlobalData>().CurrentMx = mx;
                                        //if (mx.MxStatusID==0)
                                            //IoC.Get<IEventAggregator>().Publish(new Lygl.UI.Framework.DispBusinessYdMessage());
                                        EditMx(mx);
                                        return;
                                    }
                                }
                                else
                                {
                                    if (item.ModelHit.Tag != null) //墓区对象返回的tag
                                    {
                                        modelID = (string)item.ModelHit.Tag;
                                        var ss = modelID.Split(new char[] { ':' });
                                        if (ss[0] == "MQ")    
                                        {
                                            EditArea(new Guid(ss[1]));
                                            return;
                                        }
                                    }
                                }
                                //if (ModelSelected != null)
                                //    ModelSelected(this, new ModelSelectedEventArg(modelID));
                                //break;
                            }
                        }
                    }
                    #endregion
                    break;
                case OperateMode.DrawPolygon:
                    if (DrawPolygenHandler.Handler != null && DrawPolygenHandler.Handler.DrawShapeRecord.IsDraw)
                    {
                        CreateNewMq(   Vector3ArrayConverter.ConvertToString(DrawPolygenHandler.Handler.DrawShapeRecord.Model.Geometry.Positions.ToArray()));
                        DrawPolygenHandler.Complete();
                    }
                    break;
                case OperateMode.DrawPolygonOld:
                    #region AddPolygon
                    
                    if (_drawShapeRecord.IsDraw)
                    {
                        _drawShapeRecord.IsDraw = false;
                        _viewport.Cursor = Cursors.Arrow;
                        _drawShapeRecord.RemoveLastPoint();
                        //StreamGeometry streamGeometry = new StreamGeometry();
                        //double maxX, minX;
                        //double maxY, minY;
                        //using (StreamGeometryContext ctx = streamGeometry.Open())
                        //{
                        //    PointCollection points = ((Polygon)_drawShapeRecord.Shape).Points;
                        //    ctx.BeginFigure(points[0], true /* is filled */, true /* is closed */);
                        //    maxX = minX = points[0].X;
                        //    maxY = minY = points[0].Y;
                        //    for (int i = 1; i < points.Count; i++)
                        //    {
                        //        if (points[i].X > maxX) maxX = points[i].X;
                        //        if (points[i].X < minX) minX = points[i].X;
                        //        if (points[i].Y > maxY) maxY = points[i].Y;
                        //        if (points[i].Y < minY) minY = points[i].Y;
                        //        ctx.LineTo(points[i], true, true);
                        //    }
                        //}
                        //streamGeometry.Freeze();

                        //if (maxX - minX <= 100 || maxY - minY <= 100)
                        //{
                        //    MessageBox.Show("当前区域太小，请重新创建", "错误");
                        //}
                        //else
                        {
                            CreateNewMq(_drawShapeRecord.Model.Geometry.Positions.ToString());

                        }
                        this._viewport.Items.Remove(_drawShapeRecord.Model);
                        _drawShapeRecord.Clear();
                        operateMode= OperateMode.None;
                        this._viewport.ReleaseMouseCapture();
                        e.Handled = true;
                    }
                    else
                    {
                        this._viewport.ReleaseMouseCapture();
                        operateMode = OperateMode.None;
                    }
                    #endregion
                    break;
                case OperateMode.DrawMx:
                    break;
                case OperateMode.DrawPath:
                    break;
                case OperateMode.Delete:
                    break;
                case OperateMode.MoveShape:
                    EndModifyModel();
                    operateMode = OperateMode.None;
                    break;
                case OperateMode.Select:
                    break;
                default:
                    break;
            }
        }

        private void CreateNewMq(string mqGeometryPositions)
        {
            AreaEdit newArea = AreaEdit.NewAreaEdit();
            newArea.AreaID = Guid.NewGuid();
            newArea.Name = "新建区域" + newArea.AreaID.ToString();
            newArea.GeometryText = mqGeometryPositions;// _drawShapeRecord.Model.Geometry.Positions.ToString();// Vector3ArrayConverter.ToString(_drawShapeRecord.Model.Geometry.Positions);  //pg.ToString(new Lygl.UI.Framework.FormatProvider.GeometryIntFormatProvider());
            newArea.ApplyEdit();
            AreaEdit tempArea = newArea.Clone();
            newArea = tempArea.Save();
            if (!EditArea(newArea.AreaID, true) ?? false)
            {
                //撤消建立
                newArea.Delete();
                tempArea = newArea.Clone();
                newArea = tempArea.Save();
            }
            else
            {
                //AreaROL.GetAreaROL().First();
                //Model.Add(newArea.AreaID);
                IoC.Get<IGlobalData>().AreaMxsDictAdd(newArea.AreaID);   //更新全局列表缓存
                IoC.Get<IGlobalData>().Areas.Add(newArea.AreaID);
                ModelInstancesManager.DispAreaModel(_viewport, IoC.Get<IGlobalData>().Areas.FindAreaROByAreaID(newArea.AreaID), true);
                ModelInstancesManager.AddAreaItems2MxModelInstancesDict(newArea.AreaID);  //添加墓穴模型实例字典中墓区的分类列表                                
            }
        }
        /// <summary>
        /// 处理模型位置修改结束动作
        /// </summary>
        private void EndModifyModel()
        {
            
            ModelInstancesManager.CompleteModelPosModify(_viewport);
        }
        #endregion

        #region 命令按钮相关
        public void Handle(DrawMqMessage message)
        {
            if (operateMode == OperateMode.None)
            {
                operateMode = OperateMode.DrawPolygon;
                //this._viewport.Cursor = Cursors.Cross;
                //this._viewport.Focus();
                DrawPolygenHandler.Start(this._viewport);
            }
        }
        public void Handle(DrawPathMessage message)
        {
            //if (_graphyControl == null) throw new ArgumentNullException("JpGraphyControl is null");
            //(_graphyControl as JpGraphyControl).Status = OperateStatus.DrawPath;
            ModelInstancesManager.ToggleModifierDisp(_viewport);
        }
        public void Handle(ModifyPosMessage message)
        {
            //if (_graphyControl == null) throw new ArgumentNullException("JpGraphyControl is null");
            ////(_graphyControl as JpGraphyControl).Status = OperateStatus.DrawPolygon;
            this.operateMode = OperateMode.MoveShape;// _viewport.OperateMode = OperateMode.MoveShape;
        }
        public void Handle(MoveShapeMessage message)
        {
            //if (_graphyControl == null) throw new ArgumentNullException("JpGraphyControl is null");
            //(_graphyControl as JpGraphyControl).Status = OperateStatus.MoveShape;
            this.operateMode = OperateMode.MoveShape;
            //_viewport.OperateMode = this.operateMode;
            //_viewport.selectHandler.Execute();
        }
        public void Handle(ModifyMqMessage message)
        {
            //if (_graphyControl == null) throw new ArgumentNullException("JpGraphyControl is null");
            //(_graphyControl as JpGraphyControl).Status = OperateStatus.MoveShape;
            this.operateMode = OperateMode.ModifyPolygon;
            //_viewport.OperateMode = this.operateMode;
            //_viewport.selectHandler.Execute();
        }
        public void Handle(ZoomOutMessage message)
        {
            _viewport.ZoomExtents(500);
        }

        public void Handle(RefreshViewportMessage message)
        {
            ModelInstancesManager.ReLoadModels(_viewport);
        }
        public void Handle(DrawMxMessage message)
        {
            if (operateMode == OperateMode.None)
            {
                operateMode = OperateMode.DrawMx;
                this._viewport.Cursor = Cursors.Cross;
                this._viewport.Focus();
            }
        }
        #endregion


        #region jpGraphyControl relation
        private bool? EditMx(MxRO mx, bool isEdit = false)
        {
            Lygl.UI.Edit.ViewModels.EditMxViewModel um = new Lygl.UI.Edit.ViewModels.EditMxViewModel(mx, isEdit);
            Dictionary<string, object> settings = new Dictionary<string, object> { { "ResizeMode", ResizeMode.NoResize } };
            return IoC.Get<IWindowManager>().ShowDialog(um, null, settings);
            //return IoC.Get<IWindowManager>().ShowDialog(um, null, settings);

        }
        private bool? EditMx(MxEdit mx)
        {
            Lygl.UI.Edit.ViewModels.EditMxViewModel um = new Lygl.UI.Edit.ViewModels.EditMxViewModel(mx);
            Dictionary<string, object> settings = new Dictionary<string, object> { { "ResizeMode", ResizeMode.NoResize } };
            return IoC.Get<IWindowManager>().ShowDialog(um, null, settings);
            //return IoC.Get<IWindowManager>().ShowDialog(um, null, settings);

        }
        private bool? EditArea(Guid areaID, bool isEdit = false)
        {
            Lygl.UI.Edit.ViewModels.EditAreaViewModel um = new Lygl.UI.Edit.ViewModels.EditAreaViewModel(areaID, isEdit);
            Dictionary<string, object> settings = new Dictionary<string, object> { { "ResizeMode", ResizeMode.NoResize } };
            return IoC.Get<IWindowManager>().ShowDialog(um, null, settings);
        }

        //private bool? EditArea(IShapeBaseData areaData, bool isEdit = false)
        //{
        //    Lygl.UI.Edit.ViewModels.EditAreaViewModel um = new Lygl.UI.Edit.ViewModels.EditAreaViewModel(areaData, isEdit);
        //    Dictionary<string, object> settings = new Dictionary<string, object> { { "ResizeMode", ResizeMode.NoResize } };
        //    return IoC.Get<IWindowManager>().ShowDialog(um, null, settings);
        //}

        //private bool? EditPath(IShapeBaseData pathData, bool isEdit = false)
        //{
        //    Lygl.UI.Edit.ViewModels.EditPathViewModel um = new Lygl.UI.Edit.ViewModels.EditPathViewModel(pathData, isEdit);
        //    Dictionary<string, object> settings = new Dictionary<string, object> { { "ResizeMode", ResizeMode.NoResize } };
        //    return IoC.Get<IWindowManager>().ShowDialog(um, null, settings);
        //}

       

        //private void PopupVisualShapeContextMenu(VisualShape visualShape)
        //{
        //    if (visualShape is MxVisualShape)
        //    {
        //        IoC.Get<IGlobalData>().CurrentMx = IoC.Get<IGlobalData>().GetMxRO(new Guid(visualShape.Data.ID));
        //        ContextMenu textmenu = new ContextMenu();
        //        IList<ICommandMessage> cmList = IoC.Get<ICommandMessageAggregator>().GetGroup(new string[] { "DispBusiness", "InvoiceShare" });
        //        foreach (var item in cmList)
        //        {
        //            JpMenuItem mi = new JpMenuItem(item);

        //            textmenu.Items.Add(mi);
        //        }

        //        //ClearText.Click += new RoutedEventHandler(btnOpenFile_Click);
        //        //MenuItem SaveText = new MenuItem();
        //        //SaveText.Tag = "SaveOCR";
        //        this._graphyControl.ContextMenu = textmenu;
        //        this._graphyControl.ContextMenu.IsOpen = true;
        //    }
        //}
        /*
        //装载区域和墓穴数据

       
        */
      
        
        #endregion
    }
}
