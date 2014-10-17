using Jp3DKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using HelixToolkit.Wpf.SharpDX;

namespace Jp3DKit.MouseDrawHandler
{
    public class DrawPolygenHandler 
    {
        public static DrawPolygenHandler Handler = null;
        
        protected JPViewport3DX Viewport { get; private set; }
        private Jp3DKit.DrawShapeRecord _drawShapeRecord;
        public Jp3DKit.DrawShapeRecord DrawShapeRecord
        { get { return _drawShapeRecord; } }

        public DrawPolygenHandler(JPViewport3DX viewport)
        {
            this.Viewport = viewport;
        }

        /// <summary>
        /// Gets the camera.
        /// </summary>
        /// <value>The camera.</value>
        protected ProjectionCamera Camera
        {
            get
            {
                return this.Viewport.Camera as ProjectionCamera;
            }
        }

        /// <summary>
        /// Gets the camera mode.
        /// </summary>
        /// <value>The camera mode.</value>
        protected CameraMode CameraMode
        {
            get
            {
                return this.Viewport.CameraMode;
            }
        }
        /// <summary>
        /// Gets or sets the old cursor.
        /// </summary>
        private Cursor OldCursor { get; set; }

        protected  System.Windows.Input.Cursor GetCursor()
        {
            return System.Windows.Input.Cursors.Cross;
        }
        public static void Start(JPViewport3DX viewport)
        {
            Handler = new DrawPolygenHandler(viewport);
            Handler.Viewport.MouseMove += Handler.OnMouseMove;
            Handler.Viewport.MouseUp += Handler.OnMouseUp;
            //handler.Viewport.MouseDoubleClick += handler.OnMouseDoubleClick;
            Handler.Viewport.Focus();
            Handler.Viewport.CaptureMouse();
        }
        public  void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(Viewport);
            _drawShapeRecord.IsDraw = true;
            var vv = Viewport.FindHits(p);
            if (vv.Count > 0)
            {
                if (_drawShapeRecord.Model == null)
                {
                    _drawShapeRecord.ShapeType = "Polygon";
                    _drawShapeRecord.AddPoint(vv[0].PointHit);
                    this.Viewport.Items.Add(_drawShapeRecord.Model);
                    Viewport.Attach(_drawShapeRecord.Model);
                }
                else
                {
                    _drawShapeRecord.AddPoint(vv[0].PointHit);
                    Viewport.Attach(_drawShapeRecord.Model);
                }
            }
            // this.Viewport.CaptureMouse();
        }
        public  void OnMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Point p = e.GetPosition(Viewport);
            if (_drawShapeRecord.IsCancel)
            {
                this.Viewport.ReleaseMouseCapture();
                _drawShapeRecord.IsDraw = false;
                if (_drawShapeRecord.Model != null) this.Viewport.Items.Remove(_drawShapeRecord.Model);
                _drawShapeRecord.Clear();
                Viewport.Cursor = Cursors.Arrow;
                e.Handled = true;
                return;
            }
            if (_drawShapeRecord.IsDraw)
            {
                var vv = Viewport.FindHits(p);
                if (vv.Count > 0)
                {
                    _drawShapeRecord.ReplaceLastPoint(vv[0].PointHit);
                    Viewport.Attach(_drawShapeRecord.Model);
                }
            }
        }
       
        public static void Complete()
        {
            if (Handler == null) return;
            Handler.Viewport.MouseMove -= Handler.OnMouseMove;
            Handler.Viewport.MouseUp -= Handler.OnMouseUp;
            Handler.Viewport.ReleaseMouseCapture();
            Handler.Viewport.Cursor = Handler.OldCursor;
            Handler.Viewport.Items.Remove(Handler._drawShapeRecord.Model);
            Handler._drawShapeRecord.Clear();
        }
    }
}
