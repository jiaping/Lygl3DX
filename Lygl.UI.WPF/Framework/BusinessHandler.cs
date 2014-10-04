using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Lygl.UI.Edit.ViewModels;
using Lygl.DalLib.Business;
using Lygl.DalLib.Browse;
using Lygl.DalLib.UserManager;
using System.Windows;
using System.ComponentModel.Composition;
using Lygl.UI.CommandMessage;
using Lygl.UI.Statistic;
using Lygl.UI.UserManager;
using Lygl.UI.Seek;

namespace Lygl.UI.Framework
{
    public interface IBusinessHandler
    {
    }

    #region Display business Command message
    /// <summary>
    /// 显示预定窗口
    /// </summary>
    [Export(typeof(ICommandMessage))]
    public class DispBusinessYdMessage : MxBusinessCMBase
    {
        public DispBusinessYdMessage() : base() { Name = "DispBusinessYd"; Label = "预定"; ToolTip = "显示预定业务处理窗口"; Group = "DispBusiness"; Category = "业务"; IsMainMenuItem = true; }
    }
    /// <summary>
    /// 显示购买窗口
    /// </summary>
    [Export(typeof(ICommandMessage))]
    public class DispBusinessGmMessage : MxBusinessCMBase
    {
        public DispBusinessGmMessage() : base() { Name = "DispBusinessGm"; Label = "购买"; ToolTip = "显示购买业务处理窗口"; Group = "DispBusiness"; Category = "业务"; IsMainMenuItem = true; }

        //public override bool IsCanExecute()
        //{
        //    if (ROMx != null && ROMx.MxStatusID > 1) return false;
        //    return base.IsCanExecute();
        //}
    }

    /// <summary>
    /// 显示编辑逝者窗口
    /// </summary>
    [Export(typeof(ICommandMessage))]
    public class DispSzAzMessage : MxBusinessCMBase
    {
        public DispSzAzMessage() : base() { Name = "DispSzAz"; Label = "逝者安葬"; ToolTip = "显示逝者安葬窗口"; Group = "DispBusiness"; Category = "业务"; IsMainMenuItem = true; }
    }
    /// <summary>
    /// 显示编辑联系人窗口
    /// </summary>
    [Export(typeof(ICommandMessage))]
    public class DispContactGlMessage : MxBusinessCMBase
    {
        public DispContactGlMessage() : base() { Name = "DispContactGl"; Label = "联系人管理"; ToolTip = "显示联系人管理窗口"; Group = "DispBusiness"; Category = "业务"; IsMainMenuItem = true; }
    }

    /// <summary>
    /// 显示预定窗口
    /// </summary>
    [Export(typeof(ICommandMessage))]
    public class DispBusinessLbMessage : MxBusinessCMBase
    {
        public DispBusinessLbMessage() : base() { Name = "DispBusinessLb"; Label = "立碑"; ToolTip = "显示立碑业务处理窗口"; Group = "DispBusiness"; Category = "业务"; IsMainMenuItem = true; }

        //public override bool IsCanExecute()
        //{
        //    if (IoC.Get<IGlobalData>().CurrentMx != null && IoC.Get<IGlobalData>().CurrentMx.MxStatusID < 2) return false;
        //    return base.IsCanExecute();
        //}
    }
   
    [Export(typeof(ICommandMessage))]
    public class UserResetPwdMessage : CommandMessageBase
    {
        public UserResetPwdMessage() : base() { Name = "ResetCurrentUserPwd"; Label = "用户重设密码"; ToolTip = "用户重设密码"; ; Category = "系统"; IsMainMenuItem = true; }
    }
    #endregion
 
    #region statistic and seek command
    [Export(typeof(ICommandMessage))]
    public class StatisticLyMessage : CommandMessageBase  //陵园汇总信息
    {
        public StatisticLyMessage() : base() { Name = "AllStatistic"; Label = "陵园汇总信息"; ToolTip = "陵园汇总信息"; Group = "Statistic"; Category = "统计查询"; IsMainMenuItem = true; }
    }
    [Export(typeof(ICommandMessage))]
    public class StatisticBusinessMessage : CommandMessageBase  //业务统计
    {
        public StatisticBusinessMessage() : base() { Name = "StatisticBusiness"; Label = "业务统计"; ToolTip = "业务统计"; Group = "Statistic"; Category = "统计查询"; IsMainMenuItem = true; }
    }
    [Export(typeof(ICommandMessage))]
    public class StatisticSrMessage : CommandMessageBase  //收入统计 
    {
        public StatisticSrMessage() : base() { Name = "StatisticSrMessage"; Label = "收入统计"; ToolTip = "收入统计"; Group = "Statistic"; Category = "统计查询"; IsMainMenuItem = true; }
    }


    [Export(typeof(ICommandMessage))]
    public class SeekGlfMessage : CommandMessageBase  //管理费查询 
    {
        public SeekGlfMessage() : base() { Name = "SeekGlfMessage"; Label = "管理费查询"; ToolTip = "管理费查询"; Group = "Statistic"; Category = "统计查询"; IsMainMenuItem = true; }
    }

    [Export(typeof(ICommandMessage))]
    public class SeekMxMessage : CommandMessageBase  //查询墓穴
    {
        public SeekMxMessage() : base() { Name = "SeekMx"; Label = "查询墓穴"; ToolTip = "查询墓穴"; Group = "Statistic"; Category = "统计查询"; IsMainMenuItem = true; }
    }
    [Export(typeof(ICommandMessage))]
    public class DispLogMessage : CommandMessageBase  //查询日志
    {
        public DispLogMessage() : base() { Name = "DispLog"; Label = "查询日志"; ToolTip = "查询日志"; Group = "Statistic"; Category = "统计查询"; IsMainMenuItem = true; }
    }
    //[Export(typeof(ICommandMessage))]
    //public class QfStatisticMessage : CommonCommandMessageBase  //年入墓数
    //{
    //    public QfStatisticMessage() : base() { Name = "NrmsStatistic"; Label = "年入墓数"; ToolTip = "统计年入墓数"; Group = "Statistic"; Category = "统计查询"; }
    //}
    //[Export(typeof(ICommandMessage))]
    //public class QfStatisticMessage : CommonCommandMessageBase  //年入墓数
    //{
    //    public QfStatisticMessage() : base() { Name = "NrmsStatistic"; Label = "年入墓数"; ToolTip = "统计年入墓数"; Group = "Statistic"; Category = "统计查询"; }
    //}
    //[Export(typeof(ICommandMessage))]
    //public class QfStatisticMessage : CommonCommandMessageBase  //年入墓数
    //{
    //    public QfStatisticMessage() : base() { Name = "NrmsStatistic"; Label = "年入墓数"; ToolTip = "统计年入墓数"; Group = "Statistic"; Category = "统计查询"; }
    //}
    //[Export(typeof(ICommandMessage))]
    //public class QfStatisticMessage : CommonCommandMessageBase  //年入墓数
    //{
    //    public QfStatisticMessage() : base() { Name = "NrmsStatistic"; Label = "年入墓数"; ToolTip = "统计年入墓数"; Group = "Statistic"; Category = "统计查询"; }
    //}
    #endregion

    /// <summary>
    /// 全局的命令消息处理程序，定义命令，处理命令，在IShell中创建
    /// 这里处理的命令最好不与具体模块中的对象有联系
    /// </summary>
    [Export(typeof(IBusinessHandler))]
    public class BusinessHandler:DependencyObject, IBusinessHandler,
        IHandle<DispBusinessDdMessage>,
        IHandle<DispBusinessLbMessage>,
        IHandle<DispBusinessYdMessage>,
        IHandle<DispBusinessGmMessage>,
        IHandle<DispSzAzMessage>,
        IHandle<DispContactGlMessage>,
        IHandle<DispBusinessGlfMessage>,
        IHandle<StatisticLyMessage>,
        IHandle<StatisticBusinessMessage>,
        IHandle<StatisticSrMessage>,
        //IHandle<QfStatisticMessage>,
        IHandle<SeekMxMessage>,
        IHandle<UserResetPwdMessage>,
        IHandle<DispInvoiceMessage>,
        IHandle<DispLogMessage>,
        IHandle<DispBusinessQtsfMessage>,
        IHandle<SeekGlfMessage>
    {
        public BusinessHandler()
        {
            IoC.Get<IEventAggregator>().Subscribe(this);
        }
        #region disp business command
        public void Handle(DispBusinessYdMessage message)
        {
            bool canEnter = false;
            MxRO _currentMx = IoC.Get<IGlobalData>().CurrentMx;
            BusinessYd yd = BusinessYd.GetBusinessYdByMxID(_currentMx.MxID, _currentMx.MxID);
            //如果为空，则表示没有预定，如果进入则自动创建新的预定，需要权限
            if (yd.BusinessID == Guid.Empty)
            {
                if (_currentMx.MxStatusID > 1)
                {
                    MessageBox.Show("没有预定！", "提示");
                    return;
                }
                if ((Csla.ApplicationContext.User.Identity as CustomIdentity).HavePermission("AddBusinessYd"))
                {
                    canEnter = true;
                }
                else
                {
                    MessageBox.Show("没有添加预定的权限！\n请与管理员联系。", "提示");
                    return;
                }
            }
            else
            { canEnter = true; }
            if (canEnter)
            {
                IoC.Get<IGlobalData>().ShowDialog<BusinessYdViewModel>(new BusinessYdViewModel(IoC.Get<IGlobalData>().CurrentMx));
            }
        }
        public void Handle(DispBusinessGmMessage message)
        {
            IoC.Get<IGlobalData>().ShowDialog<BusinessGmViewModel>(new BusinessGmViewModel(IoC.Get<IGlobalData>().CurrentMx));
        }
        public void Handle(DispSzAzMessage message)
        {
            IoC.Get<IGlobalData>().ShowDialog<EditSzViewModel>(new EditSzViewModel(IoC.Get<IGlobalData>().CurrentMx));

        }
        public void Handle(DispContactGlMessage message)
        {
            IoC.Get<IGlobalData>().ShowDialog<EditContactViewModel>(new EditContactViewModel(IoC.Get<IGlobalData>().CurrentMx));

        }
        public void Handle(DispBusinessGlfMessage message)
        {
            IoC.Get<IGlobalData>().ShowDialog<BusinessGlfViewModel>(new BusinessGlfViewModel(IoC.Get<IGlobalData>().CurrentMx));
        }

        public void Handle(DispBusinessDdMessage message)
        {
            IoC.Get<IGlobalData>().ShowDialog<BusinessDdViewModel>(new BusinessDdViewModel(IoC.Get<IGlobalData>().CurrentMx));
        }
        public void Handle(DispBusinessLbMessage message)
        {
            IoC.Get<IGlobalData>().ShowDialog<BusinessLbViewModel>(new BusinessLbViewModel(IoC.Get<IGlobalData>().CurrentMx));
        }
        public void Handle(DispInvoiceMessage message)
        {
            IoC.Get<IGlobalData>().ShowDialog<BusinessInvoiceViewModel>(new BusinessInvoiceViewModel(IoC.Get<IGlobalData>().CurrentMx));
        }
        public void Handle(DispBusinessQtsfMessage message)
        {
            IoC.Get<IGlobalData>().ShowDialog<BusinessQtsfViewModel>(new BusinessQtsfViewModel(IoC.Get<IGlobalData>().CurrentMx));
        }
        #endregion

        public void Handle(UserResetPwdMessage message)
        {
            Guid useID=(Csla.ApplicationContext.User.Identity as CustomIdentity).User.UserID;
            IoC.Get<IGlobalData>().ShowDialog<ResetPwdViewModel>(new ResetPwdViewModel(useID, true),new string[]{"NoResize","Transparency"});
        }

        #region statistic 
        

        public void Handle(SeekMxMessage message)
        {
            IoC.Get<IGlobalData>().ShowDialog<SeekMxViewModel>(new SeekMxViewModel());
        }
        public void Handle(SeekGlfMessage message)
        {
            IoC.Get<IGlobalData>().ShowDialog<SeekGlfViewModel>(new SeekGlfViewModel());
        }
        public void Handle(DispLogMessage message)
        {
            IoC.Get<IGlobalData>().ShowDialog<LogViewModel>(new LogViewModel());
        }
        //统计类命令
        public void Handle(StatisticLyMessage message)
        {
            IoC.Get<IGlobalData>().ShowDialog<StatisticLyViewModel>(new StatisticLyViewModel());
        }
        public void Handle(StatisticBusinessMessage message)
        {
            IoC.Get<IGlobalData>().ShowDialog<StatisticBusinessViewModel>(new StatisticBusinessViewModel());
        }
        public void Handle(StatisticSrMessage message)
        {
            IoC.Get<IGlobalData>().ShowDialog<StatisticSrViewModel>(new StatisticSrViewModel());
        }
        #endregion 
    }
}
