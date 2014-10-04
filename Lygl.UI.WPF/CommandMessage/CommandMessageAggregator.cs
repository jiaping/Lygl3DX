using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Windows;
using Lygl.UI.Framework.ViewModelBase;
using System.ComponentModel;

namespace Lygl.UI.CommandMessage
{

    /// <summary>
    /// 在CommandMessageAggregator中实现集中管理命令消息
    /// 通过Mef来实现消息的聚合，降低模块的耦合
    /// 同时实现根据消息名来获取消息
    /// </summary>
    public interface ICommandMessageAggregator
    {
        IEnumerable<ICommandMessage> CommandMessages {get;}
        ICommandMessage Get(string name);
        IList<ICommandMessage> GetGroup(string[] groupsName);

           //设置命令对象的VM
        void SetViewModel(string cmName, DependencyObject vm);
        void SetGroupViewModel(string cmGroupName, DependencyObject vm);
    }
    [Export(typeof(ICommandMessageAggregator))]
    public  class CommandMessageAggregator : ICommandMessageAggregator
    {
        [ImportMany]
        public IEnumerable<ICommandMessage> CommandMessages { get; set; }

        public ICommandMessage Get(string name)
        {
            ICommandMessage result = null;
            foreach (var item in CommandMessages)
            {
                if (item.Name == name)
                {
                    result = item;
                    break;
                }
            }
            return result;
        }
        public IList<ICommandMessage> GetGroup(string[] groupsName)
        {
            List<ICommandMessage> result = new List<ICommandMessage>();
            foreach (var item in CommandMessages)
            {
                //if (item.Group == groupName)
                if (groupsName.Contains(item.Group))
                {
                    result.Add(item);
                }
            }
            return result;
        }
        //设置命令对象的VM
        public void SetViewModel(string cmName,DependencyObject vm)
        {
            ICommandMessage cm= Get(cmName);
            if (cm!=null){
                cm.ViewModel = vm;
                IHaveModel model= cm.ViewModel as IHaveModel;
                if (model != null) cm.Model = model as INotifyPropertyChanged ;
            }
        }
        //设置命令组对象的VM
        public void SetGroupViewModel(string cmGroupName, DependencyObject vm)
        {
            IEnumerable<ICommandMessage> cmGroup = GetGroup(new string[] {cmGroupName});
            foreach (var item in cmGroup)
            {
                item.ViewModel = vm;
                IHaveModel model= item.ViewModel as IHaveModel;
                if (model != null) item.Model = model as INotifyPropertyChanged ;
            }
        }
    }
}
