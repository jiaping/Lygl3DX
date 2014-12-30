        using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using HelixToolkit.Wpf.SharpDX;
using SharpDX;


namespace Jp3DKit.MouseDrawHandler
{
    public class ModifyMqHandler : ManipulateHandler
    {
        //public Point Position;

        private InteractionHandle3D Modifier;
        public JpMqModel3D Mq { get; set; }

        private Vector3[] oldPositions;

        public ModifyMqHandler(JPViewport3DX viewport, string manipulateName, ManipulateCompleteHandleFun handleFun)
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
            this.Viewport.OperateMode = OperateMode.ModifyMq;
            this.Viewport.MouseUp += this.OnMouseUp;
            //handler.Viewport.MouseDoubleClick += handler.OnMouseDoubleClick;
            this.Viewport.Focus();
            this.Viewport.CaptureMouse();
            Modifier = new InteractionHandle3D();
        }

        public override void Complete()
        {
            this.Viewport.MouseUp -= this.OnMouseUp;
            this.Viewport.ReleaseMouseCapture();

            base.Complete();
            this.Viewport.OperateMode = OperateMode.None;
            this.Viewport.Items.Remove(this.Modifier);
            this.Modifier.Detach();
            this.Modifier.Children.Clear();
        }

        public void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            var Position = e.GetPosition(Viewport);
              Mq=  Viewport.MqSceneModel.LocateMq(Viewport, Position);
              if (Mq == null) return;
              oldPositions = new Vector3[Mq.Positions.Length];
              Mq.Positions.CopyTo(oldPositions, 0);
            this.Modifier.AddControlsHandles(Mq);
            this.Viewport.Items.Add(this.Modifier);
            this.Viewport.Attach(this.Modifier);

            #region AddBoundingBox
            //var dd = SharpDX.BoundingBox.FromPoints(Mq.Face.Geometry.Positions.ToArray());
            //LineGeometryModel3D lineModel = new LineGeometryModel3D();
            //lineModel.Geometry = LineBuilder.GenerateBoundingBox(dd);

            //this.Viewport.Items.Add(lineModel);
            //this.Viewport.Attach(lineModel); 
            #endregion

            this.Viewport.MouseUp -= this.OnMouseUp;
            this.Viewport.ReleaseMouseCapture();
        }

        public override void Cancel()
        {
            if (Mq == null || oldPositions == null) return;
            Mq.Positions = oldPositions;
            base.Cancel();
        }
        
    }

}

