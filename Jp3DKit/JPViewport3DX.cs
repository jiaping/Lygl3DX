using HelixToolkit.Wpf.SharpDX;
using Jp3DKit.MouseDrawHandler;
using Jp3DKit.TerrainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using HitTestResult = HelixToolkit.Wpf.SharpDX.HitTestResult;

namespace Jp3DKit
{
    public class JPViewport3DX : Viewport3DX
    {
        //private List<HitTestResult> mouseMoveHitModels = new List<HitTestResult>();
        private GeometryModel3D mouseMoveHitModel ;

        public OperateMode OperateMode { get; set; }
        /// <summary>
        /// 标识场景中地形地面场景对象
        /// 因为要经常用到获取地面点，同时地形地面场景对象较多，程序正常运行时，用到鼠标hit不多，因此设计为不可hit
        /// 设计一个新的GetTerrainPoint来获取hit
        /// </summary>
        public TerrainSceneModel TerrainSceneModel
        {
            get { return (TerrainSceneModel)GetValue(TerrainSceneModelProperty); }
            set { SetValue(TerrainSceneModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TerrainSceneModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TerrainSceneModelProperty =
            DependencyProperty.Register("TerrainSceneModel", typeof(TerrainSceneModel), typeof(JPViewport3DX), new UIPropertyMetadata(null, TerrainSceneModelChanged));

        private static void TerrainSceneModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                var vp = (JPViewport3DX)d;
                vp.Items.Add(e.NewValue);
            }
        }

        public MqSceneModel MqSceneModel
        {
            get { return (MqSceneModel)GetValue(MqSceneModelProperty); }
            set { SetValue(MqSceneModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MqSceneModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MqSceneModelProperty =
            DependencyProperty.Register("MqSceneModel", typeof(MqSceneModel), typeof(JPViewport3DX), new UIPropertyMetadata(null,MqSceneModelChanged));

        private static void MqSceneModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                var vp = (JPViewport3DX)d;
                vp.Items.Add(e.NewValue);
            }
        }



        public MxSceneModel MxSceneModel
        {
            get { return (MxSceneModel)GetValue(MxSceneModelProperty); }
            set { SetValue(MxSceneModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MxSceneModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MxSceneModelProperty =
            DependencyProperty.Register("MxSceneModel", typeof(MxSceneModel), typeof(JPViewport3DX), new UIPropertyMetadata(null,MxSceneModelChanged) );
         private static void MxSceneModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                var vp = (JPViewport3DX)d;
                vp.Items.Add(e.NewValue);
            }
        }
        

        public JPViewport3DX()
            : base()
        {
            //this.CommandBindings.RemoveAt(1);
            //this.CommandBindings.Add(new CommandBinding(ViewportCommands.SetTarget, this.SetTargetHandler));
            SetGestures();
            this.OperateMode = OperateMode.None;
        }

        private void SetTargetHandler(object sender, ExecutedRoutedEventArgs e)
        {
            System.Windows.Media.Media3D.Vector3D normal;
            System.Windows.Media.Media3D.Point3D? nearestPoint =this.FindNearestPoint(Mouse.GetPosition(this));
                this.Camera.Position = nearestPoint ?? this.Camera.Position;
        }

        public  IManipulateHandler ManipulateHandler;
        #region 已废弃，将事件触发改为委托实现
        ///// <summary>
        ///// 鼠标操作：绘制多边形、修改多边形、创建墓穴的处理器
        ///// 实现与主程序中事件解藕，将处理程序独立出来
        ///// 处理结束后通过事件（ModifyCompleteEvent)引发主程序处理维护数据的保存、显示等操作
        ///// </summary>
        //public event RoutedEventHandler ManipulateComplete
        //{
        //    add { AddHandler(ManipulateCompleteEvent, value); }
        //    remove { RemoveHandler(ManipulateCompleteEvent, value); }
        //}
        //public static readonly RoutedEvent ManipulateCompleteEvent =
        //     EventManager.RegisterRoutedEvent("ManipulateComplete", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(JPViewport3DX)); 
        #endregion
        /// <summary>
        /// 声明当前墓穴改变处理器
        /// </summary>
        public event RoutedEventHandler CurrentMxChanged
        {
            add { AddHandler(CurrentMxChangedEvent, value); }
            remove { RemoveHandler(CurrentMxChangedEvent, value); }
        }
        public static readonly RoutedEvent CurrentMxChangedEvent =
             EventManager.RegisterRoutedEvent("CurrentMxChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(JPViewport3DX));
        /// <summary>
        /// 添加模型对象，同时附加到renderhost;
        /// 用于动态增加模型
        /// </summary>
        /// <param name="gm"></param>
        public void AddModel3D(Element3D element)
        {
            this.Items.Add(element);
            element.Attach(this.RenderHost);
            this.CameraRotationMode = CameraRotationMode.Turntable;
        }

        public void Attach(Element3D element)
        {
            element.Attach(this.RenderHost);
        }
        /// <summary>
        /// 设置Viewport3DX的手势
        /// </summary>
        /// 
        private void SetGestures()
        {
            this.InputBindings.Clear();
            // TODO:
            // Runtime error: 'None+U' key and modifier combination is not supported for KeyGesture.
            // But vp works when defining in xaml...
            //this.InputBindings.Add(new KeyBinding(ViewportCommands.TopView, Key.U, ModifierKeys.None));
            //this.InputBindings.Add(new KeyBinding(ViewportCommands.BottomView, Key.D, ModifierKeys.None));
            //this.InputBindings.Add(new KeyBinding(ViewportCommands.FrontView, Key.F, ModifierKeys.None));
            //this.InputBindings.Add(new KeyBinding(ViewportCommands.BackView, Key.B, ModifierKeys.None));
            //this.InputBindings.Add(new KeyBinding(ViewportCommands.LeftView, Key.L, ModifierKeys.None));
            //this.InputBindings.Add(new KeyBinding(ViewportCommands.RightView, Key.R, ModifierKeys.None));
            
            this.InputBindings.Add(new KeyBinding(ViewportCommands.ZoomExtents, Key.E, ModifierKeys.Control));
            this.InputBindings.Add(
                new MouseBinding(
                    ViewportCommands.ZoomExtents, new MouseGesture(MouseAction.LeftDoubleClick, ModifierKeys.Control)));
            this.InputBindings.Add(
                new MouseBinding(ViewportCommands.Rotate, new MouseGesture(MouseAction.MiddleClick, ModifierKeys.None)));
            this.InputBindings.Add(
                new MouseBinding(ViewportCommands.Zoom, new MouseGesture(MouseAction.RightClick, ModifierKeys.Control)));
            this.InputBindings.Add(
                new MouseBinding(ViewportCommands.Pan, new MouseGesture(MouseAction.RightClick, ModifierKeys.None)));
            //this.InputBindings.Add(
            //   new MouseBinding(ViewportCommands.Pan, new MouseGesture(MouseAction.RightClick, ModifierKeys.Shift)));
            this.InputBindings.Add(
                new MouseBinding(
                    ViewportCommands.ChangeFieldOfView, new MouseGesture(MouseAction.RightClick, ModifierKeys.Alt)));
            this.InputBindings.Add(
                new MouseBinding(
                    ViewportCommands.ZoomRectangle,
                    new MouseGesture(MouseAction.RightClick, ModifierKeys.Control | ModifierKeys.Shift)));
            //this.InputBindings.Add(
            //    new MouseBinding(
            //        ViewportCommands.SetTarget, new MouseGesture(MouseAction.LeftClick, ModifierKeys.None)));
            this.InputBindings.Add(
                new MouseBinding(
                    ViewportCommands.Reset, new MouseGesture(MouseAction.MiddleDoubleClick, ModifierKeys.Control)));

            //添加手势命令-Escape,用于取消操作
            this.CommandBindings.Add(new CommandBinding(JpViewport3DXCommands.Escape, this.EscapeHandler));
            this.InputBindings.Add(new KeyBinding(JpViewport3DXCommands.Escape, Key.Escape, ModifierKeys.None));
            //添加手势命令-Ctrl+S,用于保存操作
            this.CommandBindings.Add(new CommandBinding(JpViewport3DXCommands.ControlSave, this.ControlSaveHandler));
            this.InputBindings.Add(new KeyBinding(JpViewport3DXCommands.ControlSave, Key.S, ModifierKeys.Control));

            //也可在xaml文件中绑定
            //<!--<Wpf:Viewport3DX.InputBindings>   UseDefaultGestures="False"
            //    <KeyBinding Key="B" Command="Wpf:ViewportCommands.BackView"/>
            //    <KeyBinding Key="F" Command="Wpf:ViewportCommands.FrontView"/>
            //    <KeyBinding Key="U" Command="Wpf:ViewportCommands.TopView"/>
            //    <KeyBinding Key="D" Command="Wpf:ViewportCommands.BottomView"/>
            //    <KeyBinding Key="L" Command="Wpf:ViewportCommands.LeftView"/>
            //    <KeyBinding Key="R" Command="Wpf:ViewportCommands.RightView"/>
            //    <KeyBinding Gesture="Control+E" Command="Wpf:ViewportCommands.ZoomExtents"/>
            //    <MouseBinding Gesture="RightClick" Command="Wpf:ViewportCommands.Rotate"/>
            //    <MouseBinding Gesture="MiddleClick" Command="Wpf:ViewportCommands.Zoom"/>
            //    <MouseBinding Gesture="Shift+RightClick" Command="Wpf:ViewportCommands.Pan"/>
            //    <MouseBinding Gesture="control+RightClick" Command="Wpf:ViewportCommands.ZoomRectangle" />
            //</Wpf:Viewport3DX.InputBindings>-->
        }

        private LineAdorner lineAdorner;
        /// <summary>
        /// Shows the DrawLine.
        /// </summary>
        /// <param name="rect">The zoom rectangle.</param>
        public void ShowLineAdorner(Point startPoint,Point endPoint1)
        {
            var myAdornerLayer = AdornerLayer.GetAdornerLayer(this.RenderHost);
            if (this.lineAdorner != null)
                myAdornerLayer.Remove(this.lineAdorner);
            this.lineAdorner = new LineAdorner(
                this.RenderHost, startPoint, endPoint1, Colors.LightGray, Colors.Black, 3, 1, 10, DashStyles.Solid);
            myAdornerLayer.Add(this.lineAdorner);
        }
        /// <summary>
        /// Hides the zoom rectangle.
        /// </summary>
        public void HideLineAdorner()
        {
            AdornerLayer myAdornerLayer = AdornerLayer.GetAdornerLayer(this.RenderHost);
            if (this.lineAdorner != null)
            {
                myAdornerLayer.Remove(this.lineAdorner);
            }

            this.lineAdorner = null;

            //this.RefreshViewport();
        }
     
        /// <summary>
        /// Invoked when an unhandled MouseMove attached event reaches an element in its route that is derived from this class.
        /// </summary>
        /// <param name="e">
        /// The <see cref="T:System.Windows.Input.MouseEventArgs"/> that contains the event data.
        /// </param>
        protected override void OnMouseMove(MouseEventArgs e)
        {

            if (e.MiddleButton == MouseButtonState.Pressed)
                return;
            if (e.LeftButton == MouseButtonState.Pressed || e.RightButton == MouseButtonState.Pressed)//||e.MiddleButton==MouseButtonState.Pressed
            {
                base.OnMouseMove(e);
              //  System.Diagnostics.Debug.WriteLine("mouse press move!");
            }
            else
                this.MouseMoveHitTest(e);  //调用鼠标移动时对象的捕捉
        }

        //protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        //{
        //    base.OnMouseLeftButtonUp(e);
        //    var hits = this.MouseMoveFindHits(e.GetPosition(this));
        //}
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void MouseMoveHitTest(MouseEventArgs e)
        {
            var hits = this.MouseMoveFindHits(e.GetPosition(this));
            if (hits.Count()==0 &&mouseMoveHitModel != null)
            {
                //Mouse.Capture(null, CaptureMode.SubTree);
                mouseMoveHitModel.RaiseEvent(new MouseMoveOver3DEventArgs(mouseMoveHitModel, false, new HitTestResult()));
                mouseMoveHitModel = null;
            }
            if (hits.Count > 0)
            {
                var firsthit = hits.First();
                if (mouseMoveHitModel==null)
                {
                    mouseMoveHitModel = firsthit.ModelHit;
                    //Mouse.Capture(mouseMoveHitModel, CaptureMode.SubTree);
                    mouseMoveHitModel.RaiseEvent(new MouseMoveOver3DEventArgs(firsthit.ModelHit, true, firsthit));
                } else
                {
                    if (!firsthit.ModelHit.Equals(mouseMoveHitModel))
                    {
                        //Mouse.Capture(null, CaptureMode.SubTree);
                        mouseMoveHitModel.RaiseEvent(new MouseMoveOver3DEventArgs(mouseMoveHitModel, false, new HitTestResult()));

                        mouseMoveHitModel = firsthit.ModelHit;
                        //Mouse.Capture(mouseMoveHitModel, CaptureMode.SubTree);
                        mouseMoveHitModel.RaiseEvent(new MouseMoveOver3DEventArgs(firsthit.ModelHit, true, firsthit));
                    }
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
            if (this.ManipulateHandler == null) return;
            this.ManipulateHandler.Cancel();
            //if (operateMode == OperateMode.DrawMx)
            //{
            //    this._viewport.Cursor = Cursors.Arrow;
            //    operateMode = OperateMode.None;
            //}
            //if (operateMode == OperateMode.DrawPolygon)
            //{
            //    this._viewport.Cursor = Cursors.Arrow;
            //    if (_drawShapeRecord.IsDraw) _drawShapeRecord.IsCancel = true;
            //    if (_drawShapeRecord.Model != null) this._viewport.Items.Remove(_drawShapeRecord.Model);
            //    _drawShapeRecord.Clear();
            //    operateMode = OperateMode.None;
            //    this._viewport.ReleaseMouseCapture();
            //}
            //if (this.currentMq != null && this.currentMq.IsModify)
            //{
            //    this.currentMq.RevertPoints();
            //    this.currentMq.IsModify = false;
            //}
        }
        /// <summary>
        /// 响应键盘事件，Ctrl+S
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ControlSaveHandler(object sender, ExecutedRoutedEventArgs e)
        {
            if (this.ManipulateHandler == null) return;
            this.ManipulateHandler.End();
            //if (this.currentMq != null && this.currentMq.IsModify)
            //{
            //    string mqID = this.currentMq.Tag.ToString();
            //    var ss = mqID.Split(new char[] { ':' });
            //    AreaEdit ae = AreaEdit.GetAreaEdit(Guid.Parse(ss[1]));
            //    ae.GeometryText = Vector3ArrayConverter.ConvertToString(this.currentMq.Positions);  //pg.ToString(new Lygl.UI.Framework.FormatProvider.GeometryIntFormatProvider());
            //    try
            //    {
            //        var savable = ae as ISavable;
            //        savable.Save();
            //        this.currentMq.IsModify = false;
            //    }
            //    catch (Exception ex)
            //    {
            //        throw ex;
            //    }
            //}
        }

    }
    public class CurrentMxChangedEventArgs : RoutedEventArgs
    {
        public string AreaID { get; set; }
        public string MxID { get; set; }
        public CurrentMxChangedEventArgs(RoutedEvent routedEvent, string areaID,string mxID)
            : base(routedEvent)
        {
            this.AreaID = areaID;
            this.MxID=mxID;
        }
    }
    public class ModifyCompleteEventArgs : RoutedEventArgs
    {
        public IManipulateHandler Handler;
        public ModifyCompleteEventArgs(RoutedEvent routedEvent, object source )
            : base(routedEvent, source)
        {
            Handler = source as IManipulateHandler;
        }
    }
}
