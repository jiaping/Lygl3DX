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
using Lygl.DalLib.Business;
using System.Windows;
using Csla;
using System.ComponentModel;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using Lygl.UI.Framework.ViewModelBase;
using Csla.Core;

namespace Lygl.UI.Framework
{
    
    //[Export(typeof(BusinessListViewModel))]
    /// <summary>
    /// 实现通用列表VM
    /// 实现节点的选择、更改的通知事件
    /// 实现列表的添加、删除，保存，撤消
    /// </summary>
    /// <typeparam name="T">列表的类</typeparam>
    /// <typeparam name="C">列表项类</typeparam>
    public class CommonListViewModel<T, C> : ScreenWithModelBase<T>
    {
        public CommonListViewModel()
        {
            Model = DataPortal.Fetch<T>();
        }
        public CommonListViewModel(Guid mxID)
        {
            Model = DataPortal.Fetch<T>(mxID);
        }
        public CommonListViewModel(T model)
        {
            Model = model;
        }
        

        #region 节点选择属性和事件
        /// <summary>
        /// 有二种方法可让ListView控件与选择项同步
        /// 1、在视图中使用SelectedItem="{Binding CurrnetNode}"，使选择项与CurrentNode同步
        /// 2、在视图中使用cal:Message.Attach="[SelectionChanged]=[SelectionChanged($Source,$EventArgs)]"，当选择项改变时，触发事件SelectionChanged
        /// 第一种方法，可通过创建时设置CurrentNode=* 来设置初始选择项
        /// 通过RaisePropertyChangedEventImmediately("CurrentNode");来通知UI更改选择项，以便事件订阅者获得通知
        /// </summary>
        public C _currentNode;
        public C CurrentNode
        {
            get
            {
                return _currentNode;
            }
            set
            {
                _currentNode = value;
                RaisePropertyChangedEventImmediately("CurrentNode");
            }
        }
        /// <summary>
        /// 保存后，Model对象被更新后的对象替换，所以重新定位到更新的子对象上
        /// </summary>
        public void SaveList()
        {
            int currentIndex = (Model as IList<C>).IndexOf(_currentNode);
            DoSave();
            CurrentNode = (Model as IList<C>)[currentIndex];
        }
        public void CancelList()
        {
            DoCancel();
            //解决没记录时，如果添加，立即取消时，新建对象未被删除
            if ((Model as ICollection<C>).Count == 0) { CurrentNode = default(C); }
        }
        /// <summary>
        /// 提供添加新列表项后，初始化新项的订阅事件
        /// </summary>
        public event EventHandler<AddedNewEventArgs<C>> AddedNewItem;
        public void  ListAddNew()
        {
            C cc=(C)DoAddNew();
            OnAddedNewItem(cc); 
            CurrentNode = cc;
        }
        public virtual void OnAddedNewItem(C newModel)
        {
            if (AddedNewItem != null)
                AddedNewItem(this, new AddedNewEventArgs<C>(newModel));
        }
        public void ListDeleteItem(C item)
        {
            int currentIndex = (Model as IList<C>).IndexOf(item);
            DoRemove(item);
            DoSave();
            if ((Model as IList<C>).Count>0)
            CurrentNode = (Model as IList<C>)[currentIndex-1];
        }
        
        //public void SelectionChanged(ListView sender, SelectionChangedEventArgs e)
        //{
        //    CurrentNode =(C) sender.SelectedItem;                  
        //}
        #endregion
        //protected override void OnDeactivate(bool close)
        //{
        //    if (close) IoC.Get<IEventAggregator>().Unsubscribe(this);
        //    base.OnDeactivate(close);
        //}
    }
}
