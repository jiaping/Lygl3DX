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
    /// <summary>
    /// 鼠标操作绘制多边形
    /// </summary>
    public class DrawPolygenHandler : ManipulateHandler
    {
        
        private Jp3DKit.DrawShapeRecord _drawShapeRecord;
        public Jp3DKit.DrawShapeRecord DrawShapeRecord
        { get { return _drawShapeRecord; } }

        public DrawPolygenHandler(JPViewport3DX viewport,string manipulateName)
            : base(viewport, manipulateName)
        {
            //this.Viewport = viewport;
        }

        /// <summary>
        /// 开始操作
        /// </summary>
        /// <param name="viewport"></param>
        public override void Start()
        {
            base.Start();

            this.Viewport.MouseMove += this.OnMouseMove;
            this.Viewport.MouseUp += this.OnMouseUp;
            //handler.Viewport.MouseDoubleClick += handler.OnMouseDoubleClick;
            this.Viewport.Focus();
            this.Viewport.CaptureMouse();
        }

        public override void Complete()
        {
            this.Viewport.MouseMove -= this.OnMouseMove;
            this.Viewport.MouseUp -= this.OnMouseUp;
            this.Viewport.ReleaseMouseCapture();
           
            this.Viewport.Items.Remove(this._drawShapeRecord.Model);
            this._drawShapeRecord.Clear();

            base.Complete();
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
    }
}
