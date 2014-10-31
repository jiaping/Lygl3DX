using HelixToolkit.Wpf.SharpDX;
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
        private List<HitTestResult> mouseMoveHitModels = new List<HitTestResult>();
        private GeometryModel3D mouseMoveHitModel ;


        public JPViewport3DX()
            : base()
        {
            //this.CommandBindings.RemoveAt(1);
            //this.CommandBindings.Add(new CommandBinding(ViewportCommands.SetTarget, this.SetTargetHandler));
            SetGestures();
        }

        private void SetTargetHandler(object sender, ExecutedRoutedEventArgs e)
        {
            System.Windows.Media.Media3D.Vector3D normal;
            System.Windows.Media.Media3D.Point3D? nearestPoint =this.FindNearestPoint(Mouse.GetPosition(this));
                this.Camera.Position = nearestPoint ?? this.Camera.Position;
        }

       

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
            if (e.LeftButton==MouseButtonState.Pressed || e.RightButton==MouseButtonState.Pressed )//||e.MiddleButton==MouseButtonState.Pressed
               base.OnMouseMove(e);
            else
                this.MouseMoveHitTest(e);  //调用鼠标移动时对象的捕捉
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        private void MouseMoveHitTest(MouseEventArgs e)
        {
            var hits = this.MouseMoveFindHits(e.GetPosition(this));
            if (mouseMoveHitModel != null)
            {
                mouseMoveHitModel.RaiseEvent(new MouseMoveOver3DEventArgs(mouseMoveHitModel, false, new HitTestResult()));
            }
            if (hits.Count > 0)
            {
                // Mouse.Capture(this, CaptureMode.SubTree);
                var firsthit=hits.First();
                //if (!firsthit.Equals(mouseMoveHitModel))
                {
                    mouseMoveHitModel = firsthit.ModelHit;
                    mouseMoveHitModel.RaiseEvent(new MouseMoveOver3DEventArgs(firsthit.ModelHit, true,firsthit));
                }
                //foreach (var hit in hits.Where(x => x.IsValid))
                //{
                //    hit.ModelHit.RaiseEvent(new MouseMove3DEventArgs(hit.ModelHit, hit, e.GetPosition(this), this));

                //    tempMouseMoveHitModels.Add(hit);

                //    // the winner takes it all: only the nearest hit is taken!
                //    break;
                //}
            }
           
        }
    }
}
