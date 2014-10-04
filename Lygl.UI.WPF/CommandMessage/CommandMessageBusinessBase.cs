using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using Lygl.DalLib.Core;
using System.ComponentModel;
using Lygl.DalLib.Browse;
using Lygl.UI.Framework.ViewModelBase;

namespace Lygl.UI.CommandMessage
{
    /// <summary>
    /// 这是供CommandMessage使用的Message,以便当环境（当前选择墓穴改变时）绑定的命令消息得到通知
    /// 同时传递，墓穴的相关信息，使绑定的命令消息能改变状态（如，enabled,unenable),执行命令时可将，墓穴信息作为上下文
    /// 这个方法与CommandMessageAggregator中集中通知比起来，显得更灵活，
    /// （但不知同步问题如何，如墓穴还未来得及改变，但执行命令就要执行了，这会出现执行到错误墓穴上）
    /// 这个消息，纯粹使用了Caliburn中的事件聚合消息，因为发布和处理都是直接使用，而不象ICommandMessage的命令消息中使用绑定，自动执行
    /// </summary>
    public class CurrentMxChangedMessage
    {
        public MxRO Mx;
        public CurrentMxChangedMessage(MxRO mx)
        {
            Mx = mx;
        }
    }
    [Export(typeof(ICommandMessage))]
    public class RefreshViewportMessage : CommandMessageBase
    {
        public RefreshViewportMessage()
            : base()
        {
            Name = "RefreshViewport"; Label = "刷新视图"; ToolTip = "刷新视图"; Group = "";
            IsMainMenuItem = false; Category = "陵园视图";
        }
    }
    /// <summary>
    /// 实现与当前墓穴状态相关的命令消息
    /// 获得当前墓穴更改的通知
    /// </summary>
    //[Export(typeof(ICommandMessage))]
    public abstract class CommandMessageBusinessBase : CommandMessageBase
        //, IHandle<CurrentMxChangedMessage>
    {
        #region abslote
        //发布通知命令，当前墓穴更改
        //如果不使用这种方法，也可将ROMx放到全局缓存中，
        //但仍然需要接收必更改的通知，来触发更改命令控件的状态
        //在更改当前墓穴时，使用下面的语句来发布更改消息
        //IoC.Get<IEventAggregator>().Publish(new CurrentMxChangedMessage(IoC.Get<IGlobalData>().GetMxRO(new Guid(_shapeData.ID))));
        //private MxRO _roMx;
        //public MxRO ROMx
        //{
        //    get
        //    {
        //        return _roMx;
        //    }
        //    set
        //    {
        //        //if (_roMx == null)   //这里不要判断是否相等，因为当前编辑的MX保存时，标识没变，但数据发生变化，也要获得通知 || ( _roMx.MxID != value.MxID)
        //        _roMx = value;
        //        CanExecute = IsCanExecute();
        //    }
        //}
        //public void Handle(CurrentMxChangedMessage message)
        //{
        //    _isBusy = true;
        //    try
        //    {
        //        if (_roMx != null)
        //        {
        //            if (message.Mx!=null)
        //            {
        //                ROMx = message.Mx;
        //            }
        //            else
        //            {
        //                ROMx= null;
        //            }
        //        } else 
        //        {
        //            if (message.Mx != null)
        //            {
        //                ROMx= message.Mx;
        //            }
        //        }
        //    }
        //    finally
        //    {
        //        _isBusy = false;
        //    }
        //}
        #endregion 
        #region
        //public MxRO ROMx
        //{
        //    get
        //    {
        //        return IoC.Get<IGlobalData>().CurrentMx;
        //    }
        //}
        //public void Handle(CurrentMxChangedMessage message)
        //{
        //    CanExecute = IsCanExecute();
        //}
        #endregion

        public CommandMessageBusinessBase()
            : base()
        {
            //IoC.Get<IEventAggregator>().Subscribe(this);
            //CanExecute = IsCanExecute();
        }

    }
    /// <summary>
    /// 与墓穴相关的业务命令基类
    /// 需要授权和权限设置
    /// 是否可执行，要考虑当前墓穴的状态
    /// </summary>
    public abstract class NeedMxCommandMessageBase : CommandMessageBase
    {
        public override bool IsCanExecute()
        {
            //if (ROMx == null) return false; 在继承类中，具体判断
            if (ViewModel == null) return false;
            if ((ViewModel as IViewModelIsEdit).IsEdit) return false;
            if ((ViewModel as IHaveModel).Model == null) return false;
            return base.IsCanExecute();
        } 
    }

    /// <summary>
    /// 与墓穴相关的业务命令基类
    /// 需要授权和权限设置
    /// 是否可执行，要考虑当前墓穴的状态
    /// </summary>
    public abstract class CommonCommandMessageBase : CommandMessageBase
    {
        public override bool IsCanExecute()
        {
            return base.IsCanExecute();
        }


    }
}
