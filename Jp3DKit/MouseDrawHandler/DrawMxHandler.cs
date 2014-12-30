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
    public class DrawMxHandler: ManipulateHandler
    {
            public Point Position;

            public DrawMxHandler(JPViewport3DX viewport, string manipulateName, ManipulateCompleteHandleFun handleFun)
                : base(viewport, manipulateName, handleFun)
            {
            }

            /// <summary>
            /// 开始操作
            /// </summary>
            /// <param name="viewport"></param>
            public override void Start()
            {
                base.Start();
                this.Viewport.OperateMode = OperateMode.DrawMx;
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
                this.Viewport.OperateMode = OperateMode.None;
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
