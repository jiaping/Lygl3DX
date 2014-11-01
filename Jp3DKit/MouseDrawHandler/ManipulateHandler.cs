using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Jp3DKit.MouseDrawHandler
{
    /// <summary>
    /// 鼠标操作：绘制多边形、修改多边形、创建墓穴的处理器
    /// 实现与主程序中事件解藕，将处理程序独立出来
    /// 处理结束后通过事件（ModifyCompleteEvent)引发主程序处理维护数据的保存、显示等操作
    /// </summary>
    public interface IManipulateHandler
    {
        string ManipulateName { get; set; }
      //   IManipulateHandler Handler { get;  }
         JPViewport3DX Viewport { get; }
        /// <summary>
        /// 方便子类更改光标
        /// </summary>
        /// <returns></returns>
        Cursor GetManipulateCursor();

         void Start();
         /// <summary>
         /// 正常完成后，触发ManipulateCompleteEvent，让主程序处理相关数据后释放handler
         /// </summary>
         void End();
        /// <summary>
        /// 按escape键后，取消保存的操作
        /// </summary>
        void Cancel();
        /// <summary>
        ///释放handler
        /// </summary>
          void Complete();

         void RaiseModifyCompleteEvent();
    }
    /// <summary>
    /// 鼠标操作：绘制多边形、修改多边形、创建墓穴的处理器
    /// 实现与主程序中事件解藕，将处理程序独立出来
    /// 处理结束后通过事件（ModifyCompleteEvent)引发主程序处理绘制显示
    /// </summary>
    public abstract class ManipulateHandler:IManipulateHandler
    {
       // public IManipulateHandler Handler { get;  set; }
        public JPViewport3DX Viewport { get; private set; }
        public string ManipulateName { get; set; }
        /// <summary>
        /// Gets or sets the old cursor.
        /// </summary>
        private  Cursor OldCursor { get; set; }
        public ManipulateHandler(JPViewport3DX viewport,string manipulateName)
        {
            this.Viewport = viewport;
            this.ManipulateName = manipulateName;
        }

        public virtual  void Start()
        {
            this.Viewport.ManipulateHandler = this;
            this.Viewport.Cursor = this.GetManipulateCursor();
        }
        public  void End()
        {
            RaiseModifyCompleteEvent();
        }
        public virtual void Cancel()
        {
            this.Complete();
        }
        public virtual void Complete()
        {
            this.Viewport.Cursor = this.OldCursor;
            this.Viewport.ManipulateHandler = null;
        }
        public  void RaiseModifyCompleteEvent()
        {
            RoutedEventArgs args = new RoutedEventArgs(JPViewport3DX.ManipulateCompleteEvent);
            this.Viewport.RaiseEvent(args);
        }

        public virtual Cursor GetManipulateCursor()
        {
            return System.Windows.Input.Cursors.Cross;
        }
    }
}
