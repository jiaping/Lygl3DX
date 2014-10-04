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

namespace Lygl.UI.Framework
{
    public abstract class BusinessWithMainListViewModel<T, C> : ScreenWithModelBase<C>, IViewModelIsEdit
    {
        //保存传入的参数
        protected MxRO _currentMx;   
        //保存传入的参数,表示当前工具条的命令组名，在toolbar中使用
        private string[] _commandGroupNames;  
        public BusinessWithMainListViewModel(C role, bool isEdit = false)
        {
            Model = role;
            //IoC.Get<IEventAggregator>().Subscribe(this);
            IsEdit = isEdit;

            //在包含子VM的VM中，如果被包含的VM中为Parent对象，此时要关闭当前VM的中对象管理，否则，由于editlevel的不一致，对象不能保存
            ManageObjectLifetime = false;
        }

        public BusinessWithMainListViewModel(string commandGroupName)
        {
            //在包含子VM的VM中，如果被包含的VM中为Parent对象，此时要关闭当前VM的中对象管理，否则，由于editlevel的不一致，对象不能保存
            ManageObjectLifetime = false;
            IoC.Get<IEventAggregator>().Subscribe(this);
            _commandGroupNames = new string[] { commandGroupName };
        }
        public BusinessWithMainListViewModel(MxRO mx,string commandGroupName)
        {
            //在包含子VM的VM中，如果被包含的VM中为Parent对象，此时要关闭当前VM的中对象管理，否则，由于editlevel的不一致，对象不能保存
            ManageObjectLifetime = false;
            IoC.Get<IEventAggregator>().Subscribe(this);
            _currentMx = mx;
            _commandGroupNames = new string[] { commandGroupName };
        }
        public BusinessWithMainListViewModel(MxRO mx, string dispName, string commandGroupName)
        {
            //在包含子VM的VM中，如果被包含的VM中为Parent对象，此时要关闭当前VM的中对象管理，否则，由于editlevel的不一致，对象不能保存
            ManageObjectLifetime = false;
            IoC.Get<IEventAggregator>().Subscribe(this);
            _currentMx = mx;
            DisplayName = dispName;
            _commandGroupNames = new string[] { commandGroupName };
        }
        public BusinessWithMainListViewModel(MxRO mx, string displayName, string[] commandGroupNames)
        {
            //在包含子VM的VM中，如果被包含的VM中为Parent对象，此时要关闭当前VM的中对象管理，否则，由于editlevel的不一致，对象不能保存
            ManageObjectLifetime = false;
            IoC.Get<IEventAggregator>().Subscribe(this);
            _currentMx = mx;
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
                    RaisePropertyChangedEventImmediately("IsEdit");
                    RaisePropertyChangedEventImmediately("IsListEnable");
                }
            }
        }
        public bool IsListEnable
        {
            get
            {
                return !_isEdit;
            }
        }
        /// <summary>
        /// 把MainListVM的创建从businessList中移出来，是为了，在类初始化时，就可使用MainListVM
        /// 这在InvoiceView中得到应用，
        /// 因为在invoiceView类初始化时可能要创建新项
        /// </summary>
        private CommonBusinessListViewModel<T, C> _mainListVM;
        protected CommonBusinessListViewModel<T, C> MainListVM
        {
            get
            {
                if (_mainListVM == null)
                {
                    _mainListVM = new CommonBusinessListViewModel<T, C>(_currentMx.MxID);
                    //订阅属性更改通知
                    _mainListVM.PropertyChanged += new PropertyChangedEventHandler(blvm_PropertyChanged);
                    //订阅 供添加新列表项后，初始化新项的事件
                    _mainListVM.AddedNewItem += new EventHandler<AddedNewEventArgs<C>>(MainListVM_AddedNewItem);
                    //订阅 供保存当前model后，保存其它相关数据
                    _mainListVM.CurrentItemSaved += new EventHandler<EventArgs>(_mainListVM_CurrentItemSaved);

                    _mainListVM.CurrentNode = (_mainListVM.Model as ObservableCollection<C>).FirstOrDefault();

                }
                return _mainListVM;
            }
        }

        void _mainListVM_CurrentItemSaved(object sender, EventArgs e)
        {
            CurrentModelItemSaved();
        }
        /// <summary>
        /// 提供添加新列表项后，初始化新项的功能
        /// 需要初始化新项的可override 该函数
        /// </summary>
        /// <param name="newModel"></param>
        public virtual void CurrentModelItemSaved()
        { }
        public string MxName
        {
            get { return _currentMx.MxName; }
        }

        public CommonBusinessListView BusinessList
        {
            get
            {
                //if ((MainListVM.Model as ICollection<C>).Count() > 0)
                //{
                    return (CommonBusinessListView)Utils.GetBindingView(MainListVM);
                //}
                //return null;
            }
        }
        /// <summary>
        /// 提供添加新列表项后，初始化新项的功能
        /// 需要初始化新项的可override 该函数
        /// </summary>
        /// <param name="newModel"></param>
        public virtual void InitNewItem(C newModel)
        { }
        void MainListVM_AddedNewItem(object sender, AddedNewEventArgs<C> e)
        {
            InitNewItem(e.NewObject);
        }
        //protected virtual T GetMainList()
        //{
        //    return default(T);
        //}

        /// <summary>
        /// 通过订阅业务列表视图属性改变事件，获得更改当前业务对象的通知
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void blvm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentNode")
            {
                Model = (sender as CommonBusinessListViewModel<T, C>).CurrentNode;
            }
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
        public void CMSaveList()
        {
                MainListVM.SaveList();
                IsEdit = false;
        }
        /// <summary>
        /// 使用事务来保存数据
        /// Save command message handle base 
        /// using transnation
        /// </summary>
        public void CMSaveListWithTrans()
        {
            MainListVM.SaveListWithTrans();
            IsEdit = false;
        }
        /// <summary>
        /// Cancel command message handle base
        /// </summary>
        public void CMCancelList()
        {
            MainListVM.CancelList();
            IsEdit = false;
        }
        /// <summary>
        /// Add new list item command message handle base 
        /// </summary>
        public void CMListAddNew()
        {
            MainListVM.ListAddNew();
            IsEdit = true;
        }
        /// <summary>
        /// Delete item command message handle base 
        /// </summary>
        /// <param name="model">need delete item</param>
        public void CMListDeleteItem(C model)
        {
            MainListVM.ListDeleteItem(model);
        }

        /// <summary>
        /// Modify command message handle base 
        /// </summary>
        public void CMModifyItem()
        {
            IsEdit = true;
        }


        #endregion 

    }
}
