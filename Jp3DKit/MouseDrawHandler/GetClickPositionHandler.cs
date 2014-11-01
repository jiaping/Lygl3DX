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
    public class GetClickPositionHandler: ManipulateHandler
    {
            public Point Position;

            public GetClickPositionHandler(JPViewport3DX viewport, string manipulateName)
                : base(viewport, manipulateName)
            {
            }

            /// <summary>
            /// 开始操作
            /// </summary>
            /// <param name="viewport"></param>
            public override void Start()
            {
                base.Start();

                this.Viewport.MouseUp += this.OnMouseUp;
                //handler.Viewport.MouseDoubleClick += handler.OnMouseDoubleClick;
                this.Viewport.Focus();
                this.Viewport.CaptureMouse();
            }

            public override void Complete()
            {
                this.Viewport.MouseUp -= this.OnMouseUp;
                this.Viewport.ReleaseMouseCapture();

                base.Complete();
            }

            public void OnMouseUp(object sender, MouseButtonEventArgs e)
            {
                Position= e.GetPosition(Viewport);
                //var vv = Viewport.FindHits(p);
                //if (vv.Count > 0)
                //{
                //    Position=vv[0].PointHit;
                    this.End();
                //}
            }
        
    }
}
