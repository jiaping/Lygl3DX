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

namespace Lygl.UI.Edit.ViewModels
{

    //[Export(typeof(BusinessListViewModel))]
    class BusinessListViewModel<T,C> : Lygl.UI.Framework.ViewModelBase.ScreenWithModel<T>
    {
        public BusinessListViewModel( Guid mxID)
        {
            Model = DataPortal.Fetch<T>(mxID);
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
