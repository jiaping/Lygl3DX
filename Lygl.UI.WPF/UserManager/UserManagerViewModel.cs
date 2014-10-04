using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lygl.UI.Framework;
using Caliburn.Micro;
using Lygl.DalLib.UserManager;
using System.Windows;
using Csla;
using System.ComponentModel;
using Csla.Core;
using System.ComponentModel.Composition;
using Lygl.UI.CommandMessage;
using Lygl.DalLib.Util;
using System.Security.Cryptography;


namespace Lygl.UI.UserManager
{

    #region Message
    /// <summary>
    /// 显示角色管理窗口
    /// 由主窗口处理
    /// </summary>
    [Export(typeof(ICommandMessage))]
    public class ShowUserManageMessage : CommandMessageBase
    {
        public ShowUserManageMessage() : base() { Name = "ShowUserManager"; Label = "用户管理"; ToolTip = "显示用户管理窗口"; Group = "Main"; IsMainMenuItem = true; Category = "系统";  }
    }

    [Export(typeof(ICommandMessage))]
    public class SaveUserMessage : SaveCommandMessageBase
    {

        public SaveUserMessage() : base() { Name = "SaveUser"; Label = "保存"; ToolTip = "保存修改的用户信息"; Group = "UserEdit"; Category = "用户管理"; }
    }
    [Export(typeof(ICommandMessage))]
    public class CancelUserMessage : CancelCommandMessageBase
    {

        public CancelUserMessage() : base() { Name = "CancelUser"; Label = "取消"; ToolTip = "不保存修改的用户信息"; Group = "UserEdit"; }
    }
    [Export(typeof(ICommandMessage))]
    public class AddUserMessage : AddCommandMessageBase
    {
        public AddUserMessage() : base() { Name = "AddUser"; Label = "添加"; ToolTip = "添加用户"; Group = "UserEdit"; Category = "用户管理";  }
    }
    [Export(typeof(ICommandMessage))]
    public class DeleteRoleMessage : DeleteCommandMessageBase
    {
        public DeleteRoleMessage() : base() { Name = "DeleteUser"; Label = "删除"; ToolTip = "删除用户"; Group = "UserEdit"; Category = "用户管理";  }
    }
    [Export(typeof(ICommandMessage))]
    public class ModifyUserMessage : ModifyCommandMessageBase
    {
        public ModifyUserMessage() : base() { Name = "ModifyUser"; Label = "编辑"; ToolTip = "编辑用户信息"; Group = "UserEdit"; Category = "用户管理"; }
    }
    [Export(typeof(ICommandMessage))]
    public class ResetPwdMessage : ModifyCommandMessageBase
    {
        public ResetPwdMessage() : base() { Name = "ResetPwd"; Label = "重设密码"; ToolTip = "重设用户密码"; Group = "UserEdit"; Category = "用户管理"; }
    }
    #endregion 

    class UserManagerViewModel :CommonViewModelWithMainList<UserList,User>,
        IHandle<ModifyUserMessage>,
        IHandle<SaveUserMessage>,
        IHandle<CancelUserMessage>,
        IHandle<AddUserMessage>,
        IHandle<DeleteRoleMessage>,
        IHandle<ResetPwdMessage>
    {
        public UserManagerViewModel()
            :base("UserEdit","用户管理")
        {
        }


        #region OrgUserTree 

        //private OrgUserTreeViewModel _orgTree;
        //private IOrgUserTreeNodeInfo _currentSelectNode;
        //public IOrgUserTreeNodeInfo CurrentSelectedNode
        //{
        //    get
        //    {
        //        return _currentSelectNode;
        //    }


        //}

        //public OrgUserTreeViewModel TV
        //{
        //    get
        //    {
        //          //设置树ViewModel
        //        if (_orgTree == null)
        //        {
        //            OrgROL rootList = OrgROL.GetOrgROL(Guid.Empty);
        //            _orgTree = new OrgUserTreeViewModel(rootList);
        //            _orgTree.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(_orgTree_PropertyChanged);

        //            // Let the UI bind to the view-model.

        //        }
        //        return _orgTree;
        //    }
        //}
        //public OrgUserTreeViewModel OrgUserTree
        //{
        //    get
        //    {
        //        if (_orgTree == null)
        //        {
        //            OrgROL rootList = OrgROL.GetOrgROL(Guid.Empty);
        //            _orgTree = new OrgUserTreeViewModel(rootList);
        //            _orgTree.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(_orgTree_PropertyChanged);

        //            // Let the UI bind to the view-model.

        //        }
        //        return _orgTree;
        //    }
        //}

        //void _orgTree_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName == "CurrentNode")
        //    {
             
        //        _currentSelectNode = _orgTree.CurrentNode;
        //        if (_currentSelectNode.NodeType == 2)
        //        {
        //            CurrentUser = UserList.GetUserList(_currentSelectNode.NodeID).FirstOrDefault();
        //            Model = CurrentUser;
        //            RaisePropertyChangedEventImmediately("CurrentUserUserList");
        //        } 
        //    }
        //}
        #endregion 
    
      

        private string EnCryptPwd(string pwd)
        {
            return pwd+"1";
        }
        private string DeCryptPwd(string pwd)
        {
            return pwd+"2";
        }

        #region binding object
        private RoleList _allRoleList;
        private RoleList _canAddedRoleList;
        public RoleList CanAddedRoleList
        {
            get
            {
                if (_allRoleList == null)
                {
                    _allRoleList = RoleList.GetRoleList();
                }
                _canAddedRoleList = _allRoleList.Clone();
               
                foreach (var item in _allRoleList)
                {
                    if (CurrentUserRoleList.Exists(r=>r.RoleID==item.RoleID))
                        _canAddedRoleList.Remove(item.RoleID);
                }
                return _canAddedRoleList;
            }
        }

        private Role _canAddedRoleListSelectedItem;
        public void CanAddedRoleListSelectionChanged(System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 1)
            {
                _canAddedRoleListSelectedItem = e.AddedItems[0] as Role;
            }
        }

        private Role _currentRoleListSelectedItem;
        public void CurrentRoleListSelectionChanged(System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 1)
            {
                _currentRoleListSelectedItem = e.AddedItems[0] as Role;
            }
        }

        public void AddSelectedRole()
        {
            if (Model != null && _canAddedRoleListSelectedItem != null)
            {
                if (_currentUserRoleList.Exists(r=>r.RoleID==_canAddedRoleListSelectedItem.RoleID))
                {
                    MessageBox.Show(string.Format("用户({0})已属于角色({1})，不能重复添加！",Model.Name,_canAddedRoleListSelectedItem.Name), "提示", MessageBoxButton.OK);
                    return;
                }
                UserRole ur = Model.Roles.AddNew();
                ur.RoleID = _canAddedRoleListSelectedItem.RoleID;
                _currentUserRoleList.Clear();
                Reset_currentUserRoleList();
                RaisePropertyChangedEventImmediately("CurrentUserRoleList");
                RaisePropertyChangedEventImmediately("CanAddedRoleList");
            }
        }

        public void RemoveSelectedRole()
        {
            if (Model != null && _currentRoleListSelectedItem != null)
            {
                if (MessageBox.Show(string.Format("你确定要删除用户({0})所属的角色({1})吗？",Model.Name,_currentRoleListSelectedItem.Name), "确认删除用户所属角色！", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Model.Roles.Remove(_currentRoleListSelectedItem.RoleID);
                }
                RaisePropertyChangedEventImmediately("CurrentUserRoleList");
            }
        }
        public List<Role> _currentUserRoleList;
        public List<Role> CurrentUserRoleList
        {
            get
            {
                if (_currentUserRoleList == null)
                {
                    Reset_currentUserRoleList();
                }
                return _currentUserRoleList;
            }
        }
        private void Reset_currentUserRoleList()
        {
              _currentUserRoleList = new List<Role>();
                    if (Model != null)
                    {
                        foreach (var item in Model.Roles)
                        {
                            _currentUserRoleList.AddRange(RoleList.GetRoleListByRoleID(item.RoleID));
                        }
                    }
        }
        protected override void OnModelChanged(User oldValue, User newValue)
        {
            base.OnModelChanged(oldValue, newValue);
            _currentUserRoleList = null;
            RaisePropertyChangedEventImmediately("CurrentUserRoleList");
            RaisePropertyChangedEventImmediately("CanAddedRoleList");
        }
        #endregion


        #region 命令处理
        public void Handle(SaveUserMessage message)
        {
           base.CMSaveList();
        }
        public void Handle(CancelUserMessage message)
        {
            base.CMCancelList();
        }
        public void Handle(AddUserMessage message)
        {
            base.CMListAddNew();
        }
        public void Handle(DeleteRoleMessage message)
        {
            base.CMListDeleteItem(Model);
        }
        public void Handle(ModifyUserMessage message)
        {
            base.CMModifyItem();
        }
        public void Handle(ResetPwdMessage message)
        {
            IoC.Get<IGlobalData>().ShowDialog<ResetPwdViewModel>(new ResetPwdViewModel(this.Model.UserID,false));
        }



        #endregion 
    }
}
