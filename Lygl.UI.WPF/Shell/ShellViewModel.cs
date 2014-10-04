using System.Runtime.InteropServices;
namespace Lygl.UI.Shell
{
    using System.ComponentModel.Composition;
    using Caliburn.Micro;
    using Lygl.UI.Framework;
    using Lygl.UI.UserManager;
    using Lygl.UI.ViewModels;
    using Lygl.UI.CommandMessage;
    using System.Collections.Generic;
    using System.Windows.Controls;
    using System.Linq;
    using System.Collections.ObjectModel;
    using Lygl.UI.Edit.ViewModels;
    using System.Windows;
    using Lygl.Shell;

    #region Message
    //[Export(typeof(ICommandMessage))]
    //public class ShowMqMessage : CommandMessageBase
    //{
    //    public ShowMqMessage() : base() { Name = "ShowMq"; Label = "墓区图"; ToolTip = "显示墓区图"; Group = "Main"; IsMainMenuItem = true; Category = "系统"; }
    //}
        
   
    #endregion 

    [Export(typeof(IShell))]
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive, IShell,
        //IHandle<ShowMqMessage>,
        IHandle<ShowUserManageMessage>,
        IHandle<ShowRoleManageMessage>,
        IHandle<ShowAboutMessage>
    {
        //[ImportMany(RequiredCreationPolicy = CreationPolicy.Shared)]
        //public IEnumerable<ICommandMessage> CommandMessages { get; set; }

        //[Import]
        //private CommandMessageAggregator ma { get; set; }
        
        //这里存储全局业务处理程序，响应右键菜单命令消息
        private BusinessHandler businessHandler;

        [ImportingConstructor]
        public ShellViewModel(IEventAggregator _event)
        {
            //IoC.Get<IEventAggregator>().Subscribe(this);
            _event.Subscribe(this);
          //  MyGraphViewModel um = new MyGraphViewModel();
            MainViewModel um = new MainViewModel();
            this.ActivateItem(um);
            this.DisplayName = "陵园管理系统";
            businessHandler=new BusinessHandler();
        }
        #region 命令按钮相关

        
        //public void Handle(ShowMqMessage message)
        //{ 
        //    MyGraphViewModel um = new MyGraphViewModel();
        //    this.ActivateItem(um);

        //}
   
        public void Handle(ShowUserManageMessage message)
        {
            //UserManagerViewModel um = new UserManagerViewModel();
            //this.ActivateItem(um);
            UserManagerViewModel um = new UserManagerViewModel();
            Dictionary<string, object> settings = new Dictionary<string, object> { { "ResizeMode", ResizeMode.NoResize } };
            IoC.Get<IWindowManager>().ShowDialog(um, null, settings);
        }
        public void Handle(ShowRoleManageMessage message)
        {

            RoleEditViewModel um = new RoleEditViewModel();
            Dictionary<string, object> settings = new Dictionary<string, object> { { "ResizeMode", ResizeMode.NoResize } };
            IoC.Get<IWindowManager>().ShowDialog(um, null, settings);
        }
        public void Handle(ShowAboutMessage message)
        {
            AboutViewModel um = new AboutViewModel();
            Dictionary<string, object> settings = new Dictionary<string, object> { { "ResizeMode", ResizeMode.NoResize } };
            IoC.Get<IWindowManager>().ShowDialog(um, null, settings);
        }
        #endregion 
      

        private ObservableCollection<MenuCategoryItem> _mainMenu;
        public ObservableCollection<MenuCategoryItem> MainMenu
        {
            get
            {
                if (_mainMenu == null)
                {
                    MainMenuHelper newMainMenuHelper = new MainMenuHelper();
                    _mainMenu = newMainMenuHelper.MenuCategoryItems;
                }
                return _mainMenu;
            }
        }
        private IEnumerable<ICommandMessage> _naviList;
        public IEnumerable<ICommandMessage> NaviList
        {
            get
            {
                if (_naviList == null)
                {
                    ICommandMessageAggregator agg = IoC.Get<ICommandMessageAggregator>();
                    _naviList = agg.CommandMessages;
                }
                return _naviList;
            }
        }

        //public void OrgManage()
        //{
        //    OrgUserManagerTreeViewModel um = new OrgUserManagerTreeViewModel();
        //    this.ActivateItem(um);
        //}

        private Expander _mainNavi;
        public Expander MainNavi
        {
            get
            {
                if (_mainNavi==null)
                    CompositeNavi(ref _mainNavi);
                return _mainNavi;
            }
        }
        private void CompositeNavi(ref Expander navi)
       {
        //    if (navi != null) return;
        //    navi = new Expander() { IsExpanded = true, Header = "功能" };
            
        }
    }
}
