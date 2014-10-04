using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lygl.DalLib.Edit;
 
using System.ComponentModel.Composition;
using Lygl.UI.CommandMessage;
using Caliburn.Micro;
using Lygl.DalLib.Core;
using Lygl.DalLib.NVL;
using Lygl.DalLib.Browse;
using System.ComponentModel;
using Lygl.UI.Edit.Views;
using System.Windows;
using Csla.Core;
using Lygl.DalLib.UserManager;
using Lygl.UI.Framework;
using Csla;
using Lygl.DalLib.Util;
using Lygl.UI.UserManager;
using System.Collections.ObjectModel;
using Lygl.UI.Framework.ViewModelBase;
using Lygl.Shell;

namespace Lygl.UI.Framework
{
    /// <summary>
    /// 仅有Model的视图窗口，Model一般为根对象
    /// 无根对象列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BusinessSimpleViewModel<T> : ScreenWithModelBase<T>, IViewModelIsEdit
    {
        //保存传入的参数
        protected  MxRO _currentMx;   
        //保存传入的参数,表示当前工具条的命令组名，在toolbar中使用
        private string[] _commandGroupNames;  
        public BusinessSimpleViewModel(T model, bool isEdit = false)
        {
            Model = model;
            //IoC.Get<IEventAggregator>().Subscribe(this);
            IsEdit = isEdit;
        }

        //public BusinessSimpleViewModel(string commandGroupName)
        //{
        //    //在包含子VM的VM中，如果被包含的VM中为Parent对象，此时要关闭当前VM的中对象管理，否则，由于editlevel的不一致，对象不能保存
        //    ManageObjectLifetime = false;
        //    IoC.Get<IEventAggregator>().Subscribe(this);
        //    _commandGroupNames = commandGroupName;
        //}
        public BusinessSimpleViewModel(MxRO mx, string displayName, string commandGroupName)
        {
            IoC.Get<IEventAggregator>().Subscribe(this);
            _currentMx = mx;
            DisplayName = displayName;
            _commandGroupNames = new string[] { commandGroupName };
        }
        public BusinessSimpleViewModel(MxRO mx,string displayName, string[] commandGroupNames)
        {
            IoC.Get<IEventAggregator>().Subscribe(this);
            _currentMx = mx;
            DisplayName = displayName;
            _commandGroupNames = commandGroupNames;
        }
        public BusinessSimpleViewModel(string displayName, string commandGroupName)
        {
            IoC.Get<IEventAggregator>().Subscribe(this);
            _currentMx = IoC.Get<IGlobalData>().CurrentMx;
            DisplayName = displayName;
            _commandGroupNames = new string[] {commandGroupName};
        }
        public BusinessSimpleViewModel(string displayName, string[] commandGroupNames)
        {
            IoC.Get<IEventAggregator>().Subscribe(this);
            _currentMx = IoC.Get<IGlobalData>().CurrentMx;
            DisplayName = displayName;
            _commandGroupNames = commandGroupNames;
        }

        #region Window Event handle

        protected override void OnDeactivate(bool close)
        {
            if (close) IoC.Get<IEventAggregator>().Unsubscribe(this);
            base.OnDeactivate(close);
        }
        public override void CanClose(Action<bool> callback)
        {
            if (Model != null && (Model as BusinessBase).IsDirty)
            {
                System.Windows.MessageBox.Show("正在修改数据，请先保存或取消修改", "提示！", MessageBoxButton.OK);
                callback(false);
            }
            else callback(true);
        }

        #endregion

        #region binding 

        //表示是否为编辑状态，来控制控件的编辑状态
        private bool _isEdit = false;
        public bool IsEdit
        {
            get
            {
                return _isEdit;
            }
            set
            {
                if (!_isEdit.Equals(value))
                {
                    _isEdit = value;
                    NotifyOfPropertyChange("IsEdit");
                    //RaisePropertyChangedEventImmediately("IsEdit");
                }
            }
        }

        public string MxName
        {
            get { return _currentMx.MxName; }
        }

        #endregion 
        /// <summary>
        /// 提供添加新列表项后，初始化新项的功能
        /// 需要初始化新项的可override 该函数
        /// </summary>
        /// <param name="newModel"></param>
        public virtual void InitNewItem(T newModel)
        { }
        void MainListVM_AddedNewItem(object sender, AddedNewEventArgs<T> e)
        {
            InitNewItem(e.NewObject);
        }
        protected virtual T GetMainList()
        {
            return default(T);
        }



        #region 命令处理
        public IList<ICommandMessage> toolBar
        {
            get
            {
                foreach (var item in _commandGroupNames)
                {
                    IoC.Get<ICommandMessageAggregator>().SetGroupViewModel(item, this);
                }
                
                return IoC.Get<ICommandMessageAggregator>().GetGroup(_commandGroupNames);
            }
        }

        /// <summary>
        /// Save command message handle base 
        /// </summary>
        public bool CMSave()
        {
            
            base.DoSave();
            if (Error == null)
            {
                IsEdit = false;
                
            }
            return !IsEdit;
        }
        /// <summary>
        /// Cancel command message handle base
        /// </summary>
        public  void CMCancel()
        {
            base.DoCancel();
            #region 主要是解决 没有记录时，添加一条，又立即取消，当前新添加记录仍存在的情况
            ITrackStatus ts = Model as ITrackStatus;
            if (ts!=null & ts.IsNew )
            {
                (Model as IEditableBusinessObject).Delete();
                Model = default(T);
            }
            #endregion 
            IsEdit = false;
        }
        ///// <summary>
        ///// Add new list item command message handle base 
        ///// </summary>
        //public void CMListAddNew()
        //{
        //    MainListVM.CMListAddNew();
        //    IsEdit = true;
        //}
        /// <summary>
        /// Delete item command message handle base 
        /// 删除当前对象，并保存，退出窗口
        /// </summary>
        /// <param name="model">need delete model</param>
        public void CMDelete()
        {
            base.DoDelete();
            base.DoSave();
            Model = default(T);
            this.TryClose(false);
        }

        /// <summary>
        /// Modify command message handle base 
        /// </summary>
        public void CMModify()
        {
            IsEdit = true;
        }
        protected override void OnError(Exception error)
        {
            base.OnError(error);
            IoC.Get<IGlobalData>().ShowDialog<ErrorViewModel>(new ErrorViewModel(error));
        }

        #endregion 

    }
}
