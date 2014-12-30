using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using System.Windows;
using System.ComponentModel;
using Lygl.UI.Framework.ViewModelBase;
using Lygl.UI.Framework;
using Lygl.DalLib.UserManager;
using Csla.Security;
using Csla.Core;
using Lygl.DalLib.Browse;
using Lygl.DalLib.Business;

namespace Lygl.UI.CommandMessage
{
    /// <summary>
    /// 通过Cliburn.Micro的EventAggregator实现命令消息的发布，执行
    /// 在View中，执行命令的控件项绑定 ：DataContext到指定命令消息，控件项 显示名到 消息的Label 执行事件到命令消息的Execute ;IsEnable=CanExecute
    /// 如： <Button x:Name="DrawMq" DataContext="{Binding Path=DrawMq}" Content="{Binding Path=Label}" cal:Message.Attach="[Click]=[Execute]" IsEnabled="{Binding Path=CanExecute}" HorizontalAlignment="Left" Margin="12,0,0,0" />
   ///     <Button x:Name="DrawMx" DataContext="{Binding Path=DrawMx}" Content="{Binding Path=Label}" cal:Message.Attach="[Click]=[Execute]" IsEnabled="{Binding Path=CanExecute}" HorizontalAlignment="Right" Margin="0,0,175,0"></Button>
   /// 主要是dataContext的绑定对应到相应的命令消息，命令消息是通过viewModel中的属性来获取，
   /// 当取得DataContext后，相关绑定即可完成
   /// 在CommandMessageAggregator中实现集中管理命令消息
   /// 在viewModel中，只要书写控件对应的属性来返回对应的命令
   /// 添加命令消息的处理程序即可，如：
   ///       #region 添加墓区
        //public ICommandMessage DrawMq
        //{
        //    get
        //    {
        //        return IoC.Get<ICommandMessageAggregator>().GetByName("DrawMq");
        //    }
        //}
   
        //public void Handle(DrawMqMessage message)
        //{
        //    if (_graphyControl == null) throw new ArgumentNullException("JpGraphyControl is null");
        //    (_graphyControl as JpGraphyControl).Status = OperateStatus.DrawPolygon;
        //}
        //#endregion
        //#region 添加墓穴
        //public ICommandMessage DrawMx
        //{
        //    get
        //    {
        //        return IoC.Get<ICommandMessageAggregator>().GetByName("DrawMx");
        //    }
        //}
        //public void Handle(DrawMxMessage message)
        //{
        //    if (_graphyControl == null) throw new ArgumentNullException("JpGraphyControl is null");
        //    (_graphyControl as JpGraphyControl).Status = OperateStatus.DrawMx;
        //}
    /// </summary>
    public interface ICommandMessage:INotifyPropertyChanged
    {
        /// <summary>
        /// 命令名
        /// </summary>
         string Name { get; set; }
        /// <summary>
        /// 按钮、菜单、权限显示文本
        /// </summary>
         string Label { get; set; }
        /// <summary>
        /// 提示、说明
        /// </summary>
         string ToolTip { get; set; }
         string ImageName { get; set; }
        /// <summary>
        /// 是否显示于主菜单中
        /// </summary>
         bool IsMainMenuItem { get; set; }
        /// <summary>
        /// 是否需要授权，是否显示在权限设置中
        /// </summary>
         bool NeedAuth { get; set; }
         int DispOrder { get; set; }
        /// <summary>
        /// 显示于菜单、工具栏、权限中的分组名
        /// </summary>
         string Category { get; set; }
         /// <summary>
         /// 命令在同一个ToolBar上的分组名
         /// 也用来根据分组设置命令中相关数据对象
         /// </summary>
         string Group { get; set; }
         //当前 VM
         DependencyObject ViewModel { get; set; }
         INotifyPropertyChanged Model { get; set; }

         void Publish();
        void Execute();
        bool CanExecute { get; set; }

    }

    public abstract class CommandMessageBase:PropertyChangedBase, ICommandMessage, IHandle<CurrentMxChangedMessage>
    {
        public CommandMessageBase()
        {
            IoC.Get<IEventAggregator>().Subscribe(this);
            NeedAuth = true;
        }
        #region command property

        public string Name { get; set; }
        public string Label { get; set; }
        public string ToolTip { get; set; }
        public string ImageName { get; set; }
        /// <summary>
        /// 命令是否显示在主菜单中
        /// </summary>
        public bool IsMainMenuItem { get; set; }
        public int DispOrder { get; set; }
        /// <summary>
        /// 是否需要授权，用于角色权限窗口，识别是否显示于权限窗口中
        /// </summary>
        public bool NeedAuth { get; set; }
        ///<summary>
        /// 命令的类别,用于在角色权限窗口中，分类名，主菜单中，显示为主菜单项
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 命令在同一个ToolBar上的分组名
        /// </summary>
        public string Group { get; set; }

        #endregion

        public DependencyObject ViewModel {get;set;}

        
        private INotifyPropertyChanged _model;
        public INotifyPropertyChanged Model
        {
            get
            {
                return _model;
            }
            set
            {
                if (_model != value)
                {  if (_model!=null) _model.PropertyChanged -= Model_PropertyChanged;
                    _model = value;
                    if (_model != null) _model.PropertyChanged += Model_PropertyChanged;
                }
            }
        }
        void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanExecute = IsCanExecute();
        }

        //public MxRO ROMx
        //{
        //    get
        //    {
        //        return IoC.Get<IGlobalData>().CurrentMx;
        //    }
        //}
        public void Handle(CurrentMxChangedMessage message)
        {
            CanExecute = IsCanExecute();
        }

        /// <summary>
        /// 用于触发事件的发布
        /// 可用于按钮绑定，点击时执行命令的发布
        /// </summary>
        public void Publish()
        {
            IoC.Get<IEventAggregator>().Publish(this,Caliburn.Micro.Execute.OnUIThread);
        }
        /// <summary>
        /// 事件处理程序中调用，执行命令的相关处理
        /// 执行事件处理，主要用来处理共性的数据，与业务密切的可放到视图模块的响应事件中处理
        /// </summary>
        public virtual void Execute()
        {
            //IoC.Get<IEventAggregator>().Publish(this);
            IoC.Get<Logger>().Log.InfoFormat("命令 {0}({1}) 被用户 {2} 执行。", this.Label, this.ToolTip, "aaa");
        }

        /// <summary>
        /// 仅检查是否有操作权限，如果没有权限，返回Flase,命令不能被执行
        /// </summary>
        /// <returns></returns>
        public virtual bool IsCanExecute()
        {
            if (Csla.ApplicationContext.User is UnauthenticatedPrincipal) return false;
            return (Csla.ApplicationContext.User.Identity as CustomIdentity).HavePermission(this.Name);
        }

       
        private bool _canExecute = true;
        public bool  CanExecute
        {
            get
            {
                return IsCanExecute();
                //return _canExecute;
            }
            set
            {
            	_canExecute = value;
                NotifyOfPropertyChange("CanExecute");
            }
        }
    }

    #region Add,Save,Delete,Modify,Cancel命令的基类
    /// <summary>
    /// “添加”命令的基类
    /// 需要授权，权限设置
    /// ViewMode为空时，不可执行“添加”命令，
    /// 只要ViewModel为编辑时（也就是IsEdit为True时），不可执行。
    /// </summary>
    public class AddCommandMessageBase : CommandMessageBase
    {

        public AddCommandMessageBase()
            : base()
        {
            NeedAuth = true;
        }
        public override bool IsCanExecute()
        {
            if (ViewModel == null) return false;
            if ((ViewModel as IViewModelIsEdit).IsEdit) return false;
            return base.IsCanExecute();
        }
    }
    /// <summary>
    /// “save”命令的基类
    /// 不需要授权和权限设置
    /// ViewMode为空时，不可执行“save”命令，
    /// 只要ViewModel为CanSave时，可执行。
    /// </summary>
    public class SaveCommandMessageBase : CommandMessageBase
    {

        public SaveCommandMessageBase() : base() {
            NeedAuth = false;
        }
        public override bool IsCanExecute()
        {
            if (ViewModel == null) return false;
            return (ViewModel as IViewModelCan).CanSave;
        }
    }
    /// <summary>
    /// 取消命令的基类
    /// 不需要权限设置
    /// ViewMode,Model为空时，不可执行“取消”命令，
    /// 只要ViewModel为编辑时（也就是IsEdit为True时），即可执行，而不是model.IsDirty时，这样一进入编辑状态，即可取消。
    /// </summary>
    public class CancelCommandMessageBase : CommandMessageBase
    {

        public CancelCommandMessageBase()
            : base()
        {
            NeedAuth = false;
        }
        public override bool IsCanExecute()
        {
            if (ViewModel == null) return false;
            if ((ViewModel as IHaveModel).Model == null) return false;
            return (ViewModel as IViewModelIsEdit).IsEdit;
        }
    }
    /// <summary>
    /// “Delete”命令的基类
    /// 需要授权，权限设置
    /// ViewMode为空时，不可执行“Delete”命令，
    /// ViewModel不为编辑时（也就是IsEdit为True时），可执行。
    /// </summary>
    public class DeleteCommandMessageBase : CommandMessageBase
    {

        public DeleteCommandMessageBase()
            : base()
        {
            NeedAuth = true;
        }
        public override bool IsCanExecute()
        {
            if (ViewModel == null) return false;
            if ((ViewModel as IViewModelIsEdit).IsEdit) return false;
            if ((ViewModel as IHaveModel).Model == null) return false;

            return base.IsCanExecute();
        }
    }
    /// <summary>
    /// “Modify”命令的基类
    /// 需要授权，权限设置
    /// ViewMode为空，Model为空时，不可执行“Modify”命令，
    /// ViewModel为编辑时（也就是IsEdit为True时），也不可执行。
    /// </summary>
    public class ModifyCommandMessageBase : CommandMessageBase
    {

        public ModifyCommandMessageBase()
            : base()
        {
            NeedAuth = true;
        }
        public override bool IsCanExecute()
        {
            //has modify authorization?
            if (ViewModel == null) return false;
            if ((ViewModel as IViewModelIsEdit).IsEdit) return false;
            if ((ViewModel as IHaveModel).Model == null) return false;
            if (((ViewModel as IHaveModel).Model as ITrackStatus).IsDirty) return false;
            return base.IsCanExecute();
        }
    }
    /// <summary>
    /// 所有业务的修改（Modify）命令基类
    /// 需要授权，权限设置
    /// ViewMode为空，Model为空时，不可执行“Modify”命令，
    /// ViewModel为编辑时（也就是IsEdit为True时），也不可执行。
    /// 如果已付款，则不能修改
    /// </summary>
    public class BusinessModifyCommandMessageBase : CommandMessageBase
    {

        public BusinessModifyCommandMessageBase()
            : base()
        {
            NeedAuth = true;
        }
        public override bool IsCanExecute()
        {
            //has modify authorization?
            if (ViewModel == null) return false;
            if ((ViewModel as IViewModelIsEdit).IsEdit) return false;
            if ((ViewModel as IHaveModel).Model == null) return false;
            if (((ViewModel as IHaveModel).Model as ITrackStatus).IsDirty) return false;
            if (((ViewModel as IHaveModel).Model as IBusinessHasPayFlag).PayFlag) return false;
            return base.IsCanExecute();
        }
    }
    /// <summary>
    /// 显示收费打印发票窗口的命令消息
    /// </summary>
    [Export(typeof(ICommandMessage))]
    public class DispInvoiceMessage : MxBusinessCMBase
    {
        public DispInvoiceMessage() : base() { Name = "DispInvoice"; Label = "收费"; ToolTip = "显示收费打印发票窗口"; Group = "InvoiceShare"; Category = "业务"; IsMainMenuItem = true; }
        public override bool IsCanExecute()
        {
            if (IoC.Get<IGlobalData>().CurrentMx == null ) return false;
            return base.IsCanExecute();
        }
    }
    
#endregion 

    /// <summary>
    /// 显示于主菜单中，或视图右键菜单中的业务命令对象，
    /// 需要（先选择）有当前MX的相关命令的基类
    /// </summary>
    public abstract class MxBusinessCMBase : CommandMessageBase
    {
        public override bool IsCanExecute()
        {
            if (IoC.Get<IGlobalData>().CurrentMx == null) return false;
            //因为这些命令一种是通过右键菜单执行，或者是主菜单执行，所以不需要viewmodel
            //if (ViewModel == null) return false;
            //if ((ViewModel != null)&&(ViewModel as IViewModelIsEdit).IsEdit) return false;
            bool result=base.IsCanExecute();
            //System.Diagnostics.Debug.WriteLine( string.Format("命令{0}-value:{1}", this.Name,result));
            return result;
        }
    }

    public static class CommandMessageNames
    {
        public  const string DrawMq = "DrawMq";
        public  const string DrawMx = "DrawMx";

        public const string ModifyMq = "ModifyMq";
        public const string ModifyMxPos = "ModifyMxPos";
    }


}
