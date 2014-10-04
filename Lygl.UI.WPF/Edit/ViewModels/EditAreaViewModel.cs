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
using Lygl.UI.Shell;

namespace Lygl.UI.Edit.ViewModels
{
 #region Message
    [Export(typeof(ICommandMessage))]
    public class ModifyAreaMessage : ModifyCommandMessageBase
    {
        /// <summary>
        /// 暂不在菜单中，因为没有当前选择墓区
        /// </summary>
        public ModifyAreaMessage() : base() { Name = "EditArea"; Label = "编辑墓区"; ToolTip = "编辑墓区"; Group = "EditArea";  Category = "墓穴管理"; }
    }

    [Export(typeof(ICommandMessage))]
    public class SaveAreaDataMessage : SaveCommandMessageBase
    {

        public SaveAreaDataMessage() : base() { Name = "SaveAreaData"; Label = "保存"; ToolTip = "保存修改的墓区数据"; Group = "EditArea"; }
    }
    [Export(typeof(ICommandMessage))]
    public class CancelAreaDataMessage : CancelCommandMessageBase
    {

        public CancelAreaDataMessage() : base() { Name = "CancelAreaData"; Label = "取消"; ToolTip = "不保存修改的墓区数据"; Group = "EditArea"; }
    }
    [Export(typeof(ICommandMessage))]
    public class DeleteAreaCommandMessage : DeleteCommandMessageBase
    {
        public DeleteAreaCommandMessage() : base() { Name = "DeleteAreaData"; Label = "删除"; ToolTip = "删除当前墓区"; Group = "EditArea"; Category = "墓穴管理"; }
    }
    
    #endregion 

    [Export(typeof(EditAreaViewModel))]
    public class EditAreaViewModel : BusinessSimpleViewModel<AreaEdit>,
        IHandle<ModifyAreaMessage>,
        IHandle<SaveAreaDataMessage>,
        IHandle<CancelAreaDataMessage>,
        IHandle<DeleteAreaCommandMessage>
    {
        //private IShapeBaseData _shapeData;  //显示的图形数据，数据修改后，通过它的DataChanged方法更新数据

        //public EditAreaViewModel(IShapeBaseData areaData,bool isEdit=false)
        //    :base("墓区信息",new string[] {"EditArea"})
        //{
        //    _shapeData = areaData;
        //    Model = AreaEdit.GetAreaEdit(new Guid(areaData.ID));
        //    IsEdit = isEdit;            
        //}

        public EditAreaViewModel(Guid areaID, bool isEdit = false)
            : base("墓区信息", new string[] { "EditArea" })
        {
            // _shapeData = area;
            Model = AreaEdit.GetAreaEdit(areaID);
            IsEdit = isEdit;
        }

     

        #region 命令处理
       
        public void Handle(ModifyAreaMessage message)
        {
            IsEdit = true;
            NotifyOfPropertyChange("IsEdit");
        }
        public void Handle(SaveAreaDataMessage message)
        {
            base.CMSave();
                //_shapeData.Name = Model.Name;
                //_shapeData.DataChanged();
                this.TryClose(true);
        }
        public void Handle(CancelAreaDataMessage message)
        {
            base.CMCancel();
            this.TryClose(false);

        }
        public void Handle(DeleteAreaCommandMessage message)
        {
            MxROL mxs = IoC.Get<IGlobalData>().GetMxROLByAreaID(Model.AreaID);
            if (mxs.Count > 0)
            {
                MessageBox.Show(string.Format("墓区中含有{0}个墓穴，不能删除", mxs.Count), "提示！", MessageBoxButton.OK);
            }
            else
            {
                Guid areaid = Model.AreaID;
                base.CMDelete();
                //更新全局墓区缓存，从视图中删除显示
                ModelInstancesManager.UnDispAreaModel(IoC.Get<IGlobalData>().ViewPort3DX, IoC.Get<IGlobalData>().Areas.FindAreaROByAreaID(areaid));         
                IoC.Get<IGlobalData>().Areas.Remove(areaid);
                //IoC.Get<IEventAggregator>().Publish(new RefreshViewportMessage());
            }
        }

        #endregion 

    }
}
