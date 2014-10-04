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

namespace Lygl.UI.Edit.ViewModels
{
    #region Message
    /// <summary>
    /// 显示角色管理窗口
    /// 由主窗口处理
    /// </summary>
    [Export(typeof(ICommandMessage))]
    public class ShowRoleManageMessage : CommandMessageBase
    {
        public ShowRoleManageMessage() : base() { Name = "ShowRoleManager"; Label = "角色管理"; ToolTip = "显示角色管理窗口"; Group = "Main"; IsMainMenuItem = true; Category = "系统"; }
    }

    [Export(typeof(ICommandMessage))]
    public class SaveRoleMessage : SaveCommandMessageBase
    {

        public SaveRoleMessage() : base() { Name = "SaveRole"; Label = "保存"; ToolTip = "保存修改的角色信息"; Group = "RoleEdit"; Category = "角色管理"; }
        public override bool IsCanExecute()
        {
            if (ViewModel == null) return false;
            return (ViewModel as RoleEditViewModel).CanSave;
        }
    }
    [Export(typeof(ICommandMessage))]
    public class CancelRoleMessage : CancelCommandMessageBase
    {
        public CancelRoleMessage() : base() { Name = "CancelRole"; Label = "取消"; ToolTip = "不保存修改的角色信息"; Group = "RoleEdit"; }
    }
    [Export(typeof(ICommandMessage))]
    public class AddRoleMessage : AddCommandMessageBase
    {
        public AddRoleMessage() : base() { Name = "AddRole"; Label = "添加"; ToolTip = "添加角色"; Group = "RoleEdit"; Category = "角色管理";  }
    }
    [Export(typeof(ICommandMessage))]
    public class DeleteRoleMessage :DeleteCommandMessageBase
    {
        public DeleteRoleMessage() : base() { Name = "DeleteRole"; Label = "删除"; ToolTip = "删除角色"; Group = "RoleEdit"; Category = "角色管理"; }
    }
    [Export(typeof(ICommandMessage))]
    public class ModifyRoleMessage : ModifyCommandMessageBase
    {
        public ModifyRoleMessage() : base() { Name = "ModifyRole"; Label = "编辑"; ToolTip = "编辑角色信息"; Group = "RoleEdit"; Category = "角色管理"; }
    }
    #endregion 

  
    [Export(typeof(RoleEditViewModel))]
    class RoleEditViewModel : CommonViewModelWithMainList<RoleList,Role>,
        IHandle<ModifyRoleMessage>,
        IHandle<SaveRoleMessage>,
        IHandle<CancelRoleMessage>,
        IHandle<AddRoleMessage>,
        IHandle<DeleteRoleMessage>
    {
        //public RoleEditViewModel(Role role, bool isEdit = false)
        //    : base("RoleEdit", "角色管理")
        //{
        //    Model = role;
        //    //IoC.Get<IEventAggregator>().Subscribe(this);
        //    IsEdit = isEdit;
        //}

        public RoleEditViewModel()
            : base("RoleEdit", "角色管理")
        {

        }

        protected override void OnModelChanged(Role oldValue, Role newValue)
        {
            base.OnModelChanged(oldValue, newValue);
            RaisePropertyChangedEventImmediately("TVPermissions");
        }
        /// <summary>
        /// 设置权限树所有权限项为未选择状态
        /// </summary>
        private void SetTVPermissionsToFlase()
        {
            foreach (var citem in _tvPermissions)
            {
                foreach (var pitem in citem.PermissionItems)
                {
                    pitem.IsChecked = false;
                }
            }
        }
        /// <summary>
        /// 设置权限树当前角色拥有的权限为选择状态
        /// </summary>
        private void SetTVPermissionChecket()
        {
            SetTVPermissionsToFlase();
            if (Model.Permissions != null)
            {
                foreach (var item in Model.Permissions)
                {
                    PermissionItem pi = FindItem(item);
                    if (pi != null)
                    {
                        pi.IsChecked = true;
                    }
                }
            }
            //NotifyOfPropertyChange("TVPermissions");
        }


        private PermissionItem FindItem(RolePermission rp)
        {
            foreach (var citem in _tvPermissions)
            {
                foreach (var pitem in citem.PermissionItems)
                {
                    if (rp.Name == pitem.cmi.Name) return pitem;
                }
            }
            return null;
        }
        

        private ObservableCollection<CategoryItem> _tvPermissions;
        public ObservableCollection<CategoryItem> TVPermissions 
        {
            get
            {
                //if (_tvPermissions == null)
                {
                    PermissionViewHierarckyDataHelper permissionViewHierarckyDataHelper = new PermissionViewHierarckyDataHelper();
                    _tvPermissions=permissionViewHierarckyDataHelper.CategoryItems;
                }
                
                if (Model != null) SetTVPermissionChecket();
                return _tvPermissions;
            }
        }
        #region NameValueList

        #endregion

        #region 命令处理
      
        public void Handle(SaveRoleMessage message)
        {
            if (Model.IsSavable)
            {
                Model.Permissions.Clear(); //删除所有
                foreach (var citem in _tvPermissions)
                {
                    foreach (var pitem in citem.PermissionItems)
                    {
                        if (pitem.IsChecked)
                        {
                            RolePermission rp = Model.Permissions.AddNew();
                            rp.PermissionID = Guid.NewGuid();
                            rp.Name = pitem.cmi.Name;
                            rp.Description = pitem.cmi.ToolTip;
                        }
                    }
                }

                base.CMSaveList();
#if DEBUG
                (Csla.ApplicationContext.User.Identity as CustomIdentity).ReCompositePermissionList();
#endif 
            }
        }
        public void Handle(CancelRoleMessage message)
        {
            base.CMCancelList();
        }
        public void Handle(AddRoleMessage message)
        {
            base.CMListAddNew();
        }
        public void Handle(DeleteRoleMessage message)
        {
            base.CMListDeleteItem(Model);
        }
        public void Handle(ModifyRoleMessage message)
        {
            base.CMModifyItem();
            if (Model.Permissions.Count > 0)
            {
                Model.Permissions.Clear();
            }
            else
            {
                Model.Permissions.AddNew();
            }
        }


        #endregion 

    }
}
