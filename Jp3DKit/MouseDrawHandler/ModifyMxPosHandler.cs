        using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using HelixToolkit.Wpf.SharpDX;
using SharpDX;
using Jp3DKit.ObjModel;


namespace Jp3DKit.MouseDrawHandler
{
    public class ModifyMxPosHandler : ManipulateHandler
    {
        //public Point Position;

        private UICompositeManipulator3D modifier;
        private JpObjModel3D mxModel;
        private MxModelInfo mxMMI;
        public MxModelInfo MxEMI
        {
            get
            {
                if (IsCanceled)
                    return mxMMI;
                return new MxModelInfo( this.mxMMI.MxID, mxModel.Transform.ToMatrix(),this.mxMMI.ModelFileName);
            }
        }
       // public JpMqModel3D Mx { get; set; }

        

        public ModifyMxPosHandler(JPViewport3DX viewport, string manipulateName,MxModelInfo mxEMI,ManipulateCompleteHandleFun handleFun)
            : base(viewport, manipulateName, handleFun)
        {
            this.mxMMI = mxEMI;
        }

        /// <summary>
        /// 开始操作
        /// </summary>
        /// <param name="viewport"></param>
        public override void Start()
        {
            base.Start();
            this.Viewport.OperateMode = OperateMode.ModifyMxPos;
            //this.Viewport.MouseUp += this.OnMouseUp;
            //handler.Viewport.MouseDoubleClick += handler.OnMouseDoubleClick;
            this.Viewport.Focus();

            //JpObjReader reader = new JpObjReader();
            //var bb = reader.Read(AppDomain.CurrentDomain.BaseDirectory + @"3DModel\mx.obj");
            //this.mxModel = new ObjModel3D(bb); 
            this.mxModel = new JpObjModel3D() { ModelFileName = "mx.obj" };
            this.mxModel.Transform = new System.Windows.Media.Media3D.MatrixTransform3D(this.mxMMI.ModelPos.ToMatrix3D());
            this.Viewport.Items.Add(this.mxModel);
            this.Viewport.Attach(this.mxModel);
            modifier = new UICompositeManipulator3D();
            this.Viewport.Items.Add(modifier);
            this.Viewport.Attach(this.modifier);
            this.modifier.Bind(this.mxModel);
        }
        

        public override void Complete()
        {
            //this.Viewport.MouseUp -= this.OnMouseUp;
            this.Viewport.ReleaseMouseCapture();

            base.Complete();
            this.Viewport.OperateMode = OperateMode.None;
            this.Viewport.Items.Remove(this.modifier);
            this.modifier.Detach();
            this.modifier.Children.Clear();
            this.modifier = null;
            this.Viewport.Items.Remove(this.mxModel);
            this.mxModel.Detach();
            this.mxModel = null;
        }

       

        public override void Cancel()
        {
            //if (Mq == null || oldPositions == null) return;
            //Mq.Positions = oldPositions;

            //base.Cancel();
            this.IsCanceled = true;

            this.End(); //需要主程序处理相关数据
            //this.Complete();
        }
        
    }

}

