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
using System.Windows.Controls;
using Lygl.UI.Edit.Views;
using System.Windows;
using Lygl.DalLib.Business;
using Lygl.DalLib.Browse;
using Lygl.UI.Framework;
using Lygl.DalLib.UserManager;
using Lygl.UI.Shell;

namespace Lygl.UI.Edit.ViewModels
{
 #region Message
    [Export(typeof(ICommandMessage))]
    public class ModifyMxMessage : ModifyCommandMessageBase  
    {
        public ModifyMxMessage() : base() { Name = "EditMx"; Label = "编辑墓穴"; ToolTip = "编辑墓穴"; Group = "EditMx"; Category = "墓穴管理"; }

        //public override bool IsCanExecute()
        //{
        //    //if (ROMx == null) return false;
        //    //if (ViewModel == null) return false;
        //    //if ((ViewModel as  EditMxViewModel ).IsEdit) return false;
        //    if ((IoC.Get<IGlobalData>().CurrentMx!=null) && (IoC.Get<IGlobalData>().CurrentMx.MxStatusID > 1)) return false;
        //    return base.IsCanExecute();
        //}
    }

    [Export(typeof(ICommandMessage))]
    public class SaveMxDataMessage : SaveCommandMessageBase
    {
        public SaveMxDataMessage() : base() { Name = "SaveMxData"; Label = "保存"; ToolTip = "保存修改的墓穴数据"; Group = "EditMx";}
      
    }
    [Export(typeof(ICommandMessage))]
    public class CancelMxDataMessage : CancelCommandMessageBase
    {
        public CancelMxDataMessage() : base() { Name = "CancelMxData"; Label = "取消"; ToolTip = "不保存修改的墓穴数据"; Group = "EditMx"; }
    }
    [Export(typeof(ICommandMessage))]
    public class DeleteMxCommandMessage : DeleteCommandMessageBase
    {
        public DeleteMxCommandMessage() : base() { Name = "DeleteMx"; Label = "删除"; ToolTip = "删除当前墓穴"; Group = "EditMx"; Category = "墓穴管理"; }
    }
    
 #endregion
    [Export(typeof(EditMxViewModel))]
    class EditMxViewModel :BusinessSimpleViewModel<MxEdit>,
        IHandle<ModifyMxMessage>,
        IHandle<SaveMxDataMessage>,
        IHandle<CancelMxDataMessage>,
        IHandle<DeleteMxCommandMessage>,
        IHandle<CurrentMxChangedMessage>
        //IHandle<CurrentMxChangedMessage>
    {

        //private IShapeBaseData _shapeData;  //Graphy中显示的图形数据，数据修改后，通过它的DataChanged方法更新数据
        //表示是否为新建，如果是直接进入编辑，现时，保存、取消时自动关闭窗口
        private bool _isNew = false;  
        

        /// <summary>
        /// 通过Graphy视图的鼠标事件，获得Shape数据
        /// 通过shape数据获得Mx数据，进行数据修改
        /// </summary>
        /// <param name="mxData">从Grahpy中获取的当前Shape数据</param>
        /// <param name="isNew">是否为新建墓穴，如果是立即进入编辑状态，同时保存时，立即退出</param>
        public EditMxViewModel(MxRO mxData, bool isNew = false)
            : base("墓穴信息", new string[] { "EditMx" })
        {
            //_shapeData = mxData;
            _isNew = isNew;
            IsEdit = isNew;
            IoC.Get<IEventAggregator>().Subscribe(this);
            Model = MxEdit.GetMxEdit(mxData.MxID);
        }

        public EditMxViewModel(MxEdit mx)
            : base("墓穴信息", new string[] { "EditMx" })
        {
            //_shapeData = mxData;
            _isNew = true;
            IsEdit = true;
            IoC.Get<IEventAggregator>().Subscribe(this);
            Model =mx;
        }

        public EditMxViewModel(Guid mxID)
            : base("查看墓穴信息", new string[] { "EditMx" })
        {
            IoC.Get<IEventAggregator>().Subscribe(this);
            Model = MxEdit.GetMxEdit(mxID);
        }


        #region 命令处理

        /// <summary>
        /// 处理当前Mx状态更新的通知，
        /// </summary>
        /// <param name="message"></param>
        public void Handle(CurrentMxChangedMessage  message)
        {
            Model = MxEdit.GetMxEdit(message.Mx.MxID);
        }
        public void Handle(ModifyMxMessage message)
        {
            IsEdit = true;
            NotifyOfPropertyChange("IsEdit");
        }
        public void Handle(SaveMxDataMessage message)
        {
            if (base.CMSave())
            {
                //更新全局缓存,自动更新Graphy
                IoC.Get<IGlobalData>().UpdateMx(Model);
                if (_isNew) this.TryClose(true);
            } 
        }
        public void Handle(CancelMxDataMessage message)
        {
            base.CMCancel();
            if (_isNew)
               this.TryClose(false);

        }
        public void Handle(DeleteMxCommandMessage message)
        {
            if (Model.MxStatusID>1)
            {
                MessageBox.Show("当前墓穴已销售，不能删除", "提示！", MessageBoxButton.OK);
            }
            else
            {
                MxRO mx= IoC.Get<IGlobalData>().GetMxRO(Model.MxID);
                Guid areaID = Model.AreaID;
                base.CMDelete();
                //更新全局缓存,自动更新Graphy
                //IoC.Get<IGlobalData>().UpdateArea(areaID);
                ModelInstancesManager.RemoveMxFormMxModel(mx, IoC.Get<IGlobalData>().ViewPort3DX);
                IoC.Get<IGlobalData>().AreaMxsDictAdd(areaID);
                IoC.Get<IGlobalData>().CurrentMx = default(MxRO);
                //IoC.Get<IEventAggregator>().Publish(new RefreshViewportMessage());
            }
        }

        

        //public void Handle(CurrentMxChangedMessage message)
        //{
        //    Model = MxEdit.GetMxEdit(message.Mx.MxID);
        //}
        #endregion 

        #region NameValueList
        public MxTypeNVL MxTypeList
        {
            get
            {
                return MxTypeNVL.GetMxTypeNVL();
            }
        }

        public MxStatusNVL MxStatusList
        {
            get
            {
                return MxStatusNVL.GetMxStatusNVL();
            }
        }
        public MxStyleNVL MxStyleList
        {
            get
            {
                return MxStyleNVL.GetMxStyleNVL();
            }
        }
        public MxXsNVL MxXsList
        {
            get
            {
                return MxXsNVL.GetMxXsNVL();
            }
        }
        #endregion

        #region Sz Contact
        public string SzGroupBoxHeader
        {
            get;
            set;
        }
        public List<SzListItemView> SzItems
        {
            get
            {
                if (Model == null) return null;
                List<SzListItemView> list = new List<SzListItemView>();
                foreach (var item in MxSzEditList.GetMxSzEditListByMxID(Model.MxID))
                {
                    SzListItemView vm = (SzListItemView)Utils.GetBindingView(new SzListItemViewModel(item));
                    list.Add(vm);
                }
                if (list.Count > 0) { SzGroupBoxHeader = "逝者信息"; } else { SzGroupBoxHeader = ""; }
                return list;
            }
        }

        public FlowDiagramView FlowDiagram
        {
            get
            {
                if (Model == null) return null;
                MxRO mx = IoC.Get<IGlobalData>().GetMxRO(Model.AreaID,Model.MxID);
                FlowDiagramView vm = (FlowDiagramView)Utils.GetBindingView(new FlowDiagramViewModel(mx));
                return vm;
            }
        }
        public List<ContactListItemView> ContactItems
        {
            get
            {
                if (Model == null) return null;
                List<ContactListItemView> list = new List<ContactListItemView>();
                foreach (var item in ContactEditList.GetContactEditListByMxID(Model.MxID))
                {
                    ContactListItemView vm = (ContactListItemView)Utils.GetBindingView(new ContactListItemViewModel(item));
                    list.Add(vm);
                }
                return list;
            }
        }

        public void Pause()
        {
            object view=  this.GetView(null);
        }
        #endregion
    }
}
