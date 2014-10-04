using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Lygl.UI.CommandMessage;
using Caliburn.Micro;
using Lygl.DalLib.UserManager;
using System.Windows;

namespace Lygl.UI.Shell
{
    public class MenuCategoryItem
    {
        public MenuCategoryItem()
        {
            MenuSubItems = new ObservableCollection<MenuSubItem>();
        }
        public MenuCategoryItem(string categoryName)
        {
            CategoryName = categoryName;
            MenuSubItems = new ObservableCollection<MenuSubItem>();
        }

        public string CategoryName { get; set; }
        public ObservableCollection<MenuSubItem> MenuSubItems { get; set; }
    }

    public delegate void delegateExecute();
    public class MenuSubItem:DependencyObject  //:INotifyPropertyChangedEx
    {

        //public INotifyPropertyChanged Model
        //{
        //    get
        //    {
        //        return _model;
        //    }
        //    set
        //    {
        //        if (_model != value)
        //        {
        //            if (_model != null) _model.PropertyChanged -= Model_PropertyChanged;
        //            _model = value;
        //            if (_model != null) _model.PropertyChanged += Model_PropertyChanged;
        //        }
        //    }
        //}
        public ICommandMessage _cmItem;
        public ICommandMessage CMItem 
        {
            get { return _cmItem;}
            set {
                if (_cmItem!=value)
                {
                    if (_cmItem != null) _cmItem.PropertyChanged -= _cmItem_PropertyChanged;
                    _cmItem = value;
                    if (_cmItem != null) _cmItem.PropertyChanged += _cmItem_PropertyChanged;
                }
            }
        }

        void _cmItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            CanExecute = CMItem.CanExecute;
        }

        //delegateExecute Execute;
        public void Execute()
        {
            CMItem.Publish();//.Execute();
        }
        public string Label
        {
            get
            {
                return CMItem.Label;
            }
        }
        public static readonly DependencyProperty CanExecuteProperty =
           DependencyProperty.Register("CanExecute", typeof(bool),
           typeof(MenuSubItem), new PropertyMetadata(true));

        public bool CanExecute
        {
            get
            {
                return (bool) GetValue(CanExecuteProperty);
            }
            set
            {
                SetValue(CanExecuteProperty,value);
            }

        }


        public MenuSubItem(ICommandMessage cmi)
        {
            CMItem = cmi;
            CanExecute = CMItem.CanExecute;
            //Execute = cmi.Execute;
        }
    }
    public class MainMenuHelper
    {
        public MainMenuHelper()
        {
            ICommandMessageAggregator agg = IoC.Get<ICommandMessageAggregator>();
            _menuCategoryItems = new ObservableCollection<MenuCategoryItem>();
            IEnumerable<ICommandMessage> cmi = agg.CommandMessages.Where(cm => cm.IsMainMenuItem == true);
            foreach (var item in cmi)
            {
                AddCommandMessageItem(item);
            }
        }


        private void AddCommandMessageItem(ICommandMessage cmi)
        {
            MenuCategoryItem currentMenuCategoryItem = null;
            foreach (var item in _menuCategoryItems)
            {
                if (item.CategoryName == cmi.Category)
                {
                    currentMenuCategoryItem = item;
                    break;
                }
            }
            if (currentMenuCategoryItem == null)
            {
                currentMenuCategoryItem = new MenuCategoryItem(cmi.Category);
                _menuCategoryItems.Add(currentMenuCategoryItem);
            }
            currentMenuCategoryItem.MenuSubItems.Add(new MenuSubItem(cmi));
        }

        public ObservableCollection<MenuCategoryItem> _menuCategoryItems;
        public ObservableCollection<MenuCategoryItem> MenuCategoryItems
        {
            get
            {
                return _menuCategoryItems;
            }
        }

    }
    
}
