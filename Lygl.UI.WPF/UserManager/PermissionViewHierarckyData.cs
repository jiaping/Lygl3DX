using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Lygl.UI.CommandMessage;
using Caliburn.Micro;
using Lygl.DalLib.UserManager;

namespace Lygl.UI.UserManager
{
    public class CategoryItem
    {
        public CategoryItem()
        {
            PermissionItems = new ObservableCollection<PermissionItem>();
        }
        public CategoryItem(string categoryName)
        {
            CategoryName = categoryName;
            PermissionItems = new ObservableCollection<PermissionItem>();
        }

        public string CategoryName { get; set; }
        public ObservableCollection<PermissionItem> PermissionItems { get; set; }
    }

    public delegate void delegateExecute();
    public class PermissionItem
    {
        public ICommandMessage cmi { get; set; }
        //delegateExecute Execute;

        public string Label
        {
            get
            {
                return cmi.Label;
            }
        }
        public string Description
        {
            get
            {
                return cmi.ToolTip;
            }
        }

        public bool IsChecked { get; set; }


        public PermissionItem(ICommandMessage item)
        {
            cmi = item;
        }
    }
    public class PermissionViewHierarckyDataHelper
    {
        public PermissionViewHierarckyDataHelper()
        {
            ICommandMessageAggregator agg = IoC.Get<ICommandMessageAggregator>();
            _CategoryItems = new ObservableCollection<CategoryItem>();
            IEnumerable<ICommandMessage> cmi = agg.CommandMessages.Where(cm => cm.NeedAuth == true);
            foreach (var item in cmi)
            {
                AddCommandMessageItem(item);
            }
        }


        private void AddCommandMessageItem(ICommandMessage cmi)
        {
            CategoryItem currentCategoryItem = null;
            foreach (var item in _CategoryItems)
            {
                if (item.CategoryName == cmi.Category)
                {
                    currentCategoryItem = item;
                    break;
                }
            }
            if (currentCategoryItem == null)
            {
                currentCategoryItem = new CategoryItem(cmi.Category);
                _CategoryItems.Add(currentCategoryItem);
            }
            currentCategoryItem.PermissionItems.Add(new PermissionItem(cmi));
        }

        public ObservableCollection<CategoryItem> _CategoryItems;
        public ObservableCollection<CategoryItem> CategoryItems
        {
            get
            {
                return _CategoryItems;
            }
        }

    }
}
