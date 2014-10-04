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
using Lygl.UI.Framework;
using Lygl.DalLib.Browse;
using System.Windows;

namespace Lygl.UI.Edit.ViewModels
{
 #region Message
    [Export(typeof(ICommandMessage))]
    public class ModifyPathMessage : ModifyCommandMessageBase
    {
        /// <summary>
        /// 暂不在菜单中，因为没有当前选择墓区
        /// </summary>
        public ModifyPathMessage() : base() { Name = "EditPath"; Label = "编辑路径"; ToolTip = "编辑路径"; Group = "EditPath"; Category = "墓穴管理"; }
    }

    [Export(typeof(ICommandMessage))]
    public class SavePathDataMessage : SaveCommandMessageBase
    {

        public SavePathDataMessage() : base() { Name = "SavePathData"; Label = "保存"; ToolTip = "保存修改的路径数据"; Group = "EditPath"; }
    }
    [Export(typeof(ICommandMessage))]
    public class CancelPathDataMessage : CancelCommandMessageBase
    {

        public CancelPathDataMessage() : base() { Name = "CancelPathData"; Label = "取消"; ToolTip = "不保存修改的路径数据"; Group = "EditPath"; }
    }
    [Export(typeof(ICommandMessage))]
    public class DeletePathCommandMessage : DeleteCommandMessageBase
    {
        public DeletePathCommandMessage() : base() { Name = "DeletePathData"; Label = "删除"; ToolTip = "删除当前路径"; Group = "EditPath"; Category = "墓穴管理"; }
    }
    
    #endregion 

    [Export(typeof(EditPathViewModel))]
    public class EditPathViewModel : BusinessSimpleViewModel<PathEdit>,
        IHandle<ModifyPathMessage>,
        IHandle<SavePathDataMessage>,
        IHandle<CancelPathDataMessage>,
        IHandle<DeletePathCommandMessage>
    {
        //private IShapeBaseData _shapeData;  //显示的图形数据，数据修改后，通过它的DataChanged方法更新数据

        //public EditPathViewModel(IShapeBaseData pathData, bool isEdit = false)
        //    : base("路径信息", new string[] { "EditPath" })
        //{
        //    _shapeData = pathData;
        //    Model = PathEdit.GetPathEdit(new Guid(pathData.ID));
        //    IsEdit = isEdit;            
        //}
        public EditPathViewModel()
            : base("路径信息", new string[] { "EditPath" })
        { }
     

        #region 命令处理

        public void Handle(ModifyPathMessage message)
        {
            IsEdit = true;
            NotifyOfPropertyChange("IsEdit");
        }
        public void Handle(SavePathDataMessage message)
        {
            base.CMSave();
                //_shapeData.Name = Model.Name;
                //_shapeData.DataChanged();
                this.TryClose(true);
        }
        public void Handle(CancelPathDataMessage message)
        {
            base.CMCancel();
            this.TryClose(false);

        }
        public void Handle(DeletePathCommandMessage message)
        {
            //MxROL mxs = IoC.Get<IGlobalData>().GetMxROLByPathID(Model.PathID);
            //if (mxs.Count > 0)
            //{
            //    MessageBox.Show(string.Format("墓区中含有{0}个墓穴，不能删除", mxs.Count), "提示！", MessageBoxButton.OK);
            //}
            //else
            //{
            //    base.CMDelete();
            //}
        }

        #endregion 

    }
}
