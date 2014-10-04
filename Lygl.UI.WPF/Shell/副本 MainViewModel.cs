using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lygl.Lib;
using System.Windows;
using System.Windows.Media.Imaging;
using JpGraphy.GraphyControl;
using JpGraphy.GIS;
using JpGraphy.EntityShape;
using System.ComponentModel;
using System.Windows.Media;
using JpGraphy.ShapeData;
using Csla;
using Lygl.Lib.Core;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using Lygl.UI.CommandMessage;
using Lygl.UI.Shell;
using Lygl.Lib.NVL;
using Lygl.Lib.Browse;
using Lygl.Lib.Edit;
using System.Windows.Controls;
using JpGraphy.GraphyCanvas;
using JpGraphy.GraphyVisualSharp;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using Jp3DKit;
using HelixToolkit.SharpDX.Wpf;

namespace Lygl.UI.ViewModels
{
    #region Message
    [Export(typeof(ICommandMessage))]
    public class DrawMqMessage : CommandMessageBase
    {
        public DrawMqMessage()
            : base()
        {
            Name = "DrawMq"; Label = "添加墓区"; ToolTip = "添加墓区"; Group = "LyView";
            ToolTip = "添加墓区"; IsMainMenuItem = true; Category = "陵园视图";
        }
    }

    [Export(typeof(ICommandMessage))]
    public class DrawMxMessage : CommandMessageBase
    {
        public DrawMxMessage()
            : base()
        {
            Name = "DrawMx"; Label = "添加墓穴"; ToolTip = "添加墓穴"; Group = "LyView";
            ToolTip = "添加墓区"; IsMainMenuItem = true; Category = "陵园视图";
        }
    }
    [Export(typeof(ICommandMessage))]
    public class ModifyPosMessage : CommandMessageBase
    {
        public ModifyPosMessage()
            : base()
        {
            Name = "ModifyPos"; Label = "修改位置和角度"; ToolTip = "修改形状的位置和角度"; Group = "LyView";
            IsMainMenuItem = true; Category = "陵园视图";
        }
    }
    [Export(typeof(ICommandMessage))]
    public class DrawPathMessage : CommandMessageBase
    {
        public DrawPathMessage()
            : base()
        {
            Name = "DrawPath"; Label = "添加道路"; ToolTip = "添加道路"; Group = "LyView";
            IsMainMenuItem = true; Category = "陵园视图";
        }
    }
    [Export(typeof(ICommandMessage))]
    public class MoveShapeMessage : CommandMessageBase
    {
        public MoveShapeMessage()
            : base()
        {
            Name = "MoveShape"; Label = "移动形状"; ToolTip = "移动形状"; Group = "LyView";
            IsMainMenuItem = true; Category = "陵园视图";
        }
    }

    [Export(typeof(ICommandMessage))]
    public class ZoomOutMessage : CommandMessageBase
    {
        public ZoomOutMessage()
            : base()
        {
            Name = "ZoomOut"; Label = "缩放"; ToolTip = "缩放"; Group = "LyView";
            IsMainMenuItem = true; Category = "陵园视图";
        }
    }
    #endregion



    /// <summary>
    /// 实现主3D视图模块
    /// </summary>
    [Export(typeof(MainViewModel))]
    class MainViewModel : Lygl.UI.Framework.ViewModelBase.ScreenWithModel<AreaROL>,
        IHandle<DrawMqMessage>,
        IHandle<DrawMxMessage>,
        IHandle<ModifyPosMessage>,
        IHandle<DrawPathMessage>,
        IHandle<MoveShapeMessage>,
        IHandle<ZoomOutMessage>
    {
        private JpGraphyControl _graphyControl;
        private Viewport3DX _viewport;
        //private HelixToolkit.Wpf.HelixViewport3D _naviViewport;
        //Dictionary<Guid, MxROL> _mxROLDict;

        public MainViewModel()
        {
            IoC.Get<IEventAggregator>().Subscribe(this);
            //Model = AreaList.GetAreaList();
            //MxROL.UpdateAllMxStatus();
            DataPortal.Execute(new Lygl.Lib.DBCommand.UpdateAllMxStatusCommand());
            Model = AreaROL.GetAreaROL();
          
        }

        protected override void OnDeactivate(bool close)
        {
            if (close) IoC.Get<IEventAggregator>().Unsubscribe(this);
            base.OnDeactivate(close);
        }

        //public GraphyWapperViewModel()
        //{
        //    Model = AreaList.GetAreaList();
        //}
        //public JpGraphyControl GraphyControl
        //{
        //    get
        //    {
        //        if (_graphyControl==null) _graphyControl= new JpGraphyControl();
        //        return _graphyControl;
        //    }
        //}
        //



        #region 命令按钮相关
        //public IList<ICommandMessage> toolBar
        //{
        //    get
        //    {
        //        return IoC.Get<ICommandMessageAggregator>().GetGroup(new string[] { "LyView","EditArea","EditMx" });
        //    }
        //}
        #region 添加墓区
        //public ICommandMessage DrawMq
        //{
        //    get
        //    {
        //        return IoC.Get<ICommandMessageAggregator>().Get("DrawMq");
        //    }
        //}

        public void Handle(DrawMqMessage message)
        {
            if (_graphyControl == null) throw new ArgumentNullException("JpGraphyControl is null");
            (_graphyControl as JpGraphyControl).Status = OperateStatus.DrawPolygon;
        }
        public void Handle(DrawPathMessage message)
        {
            if (_graphyControl == null) throw new ArgumentNullException("JpGraphyControl is null");
            (_graphyControl as JpGraphyControl).Status = OperateStatus.DrawPath;
        }
        public void Handle(ModifyPosMessage message)
        {
            if (_graphyControl == null) throw new ArgumentNullException("JpGraphyControl is null");
            //(_graphyControl as JpGraphyControl).Status = OperateStatus.DrawPolygon;
        }
        public void Handle(MoveShapeMessage message)
        {
            if (_graphyControl == null) throw new ArgumentNullException("JpGraphyControl is null");
            (_graphyControl as JpGraphyControl).Status = OperateStatus.MoveShape;
        }
        public void Handle(ZoomOutMessage message)
        {
            _viewport.ZoomExtents(500);
        }
        #endregion
        #region 添加墓穴
        //public ICommandMessage DrawMx
        //{
        //    get
        //    {
        //        return IoC.Get<ICommandMessageAggregator>().Get("DrawMx");
        //    }
        //}
        public void Handle(DrawMxMessage message)
        {
            if (_graphyControl == null) throw new ArgumentNullException("JpGraphyControl is null");
            (_graphyControl as JpGraphyControl).Status = OperateStatus.DrawMx;
        }
        #endregion
        #endregion


        #region jpGraphyControl relation
        //private void jpGraphyControlLoaded(object sender, RoutedEventArgs e)
        public void GraphyControlLoaded()
        {
            JpGraphyControl.SetContentSize(14000, 14000);//*(96/72)

            MapVisualLayer mapVisualLayer = new MapVisualLayer("VisualImageSharpThumb"); //bottom thumb map
            MapVisualLayer mapVisualLayerPart = new MapVisualLayer("VisualImageSharp");
            LoadMapThumb1400Layer(mapVisualLayer);





            _graphyControl.AddVisualLayers(mapVisualLayer);
            _graphyControl.AddVisualLayers(mapVisualLayerPart);
            _graphyControl.AddVisualLayers(new VisualLayer("AreaLayer"));
            _graphyControl.AddVisualLayers(new VisualLayer("PathLayer"));

            LoadPathVisualShape();
            //由于是异步获取数据，此时数据未加载完成
            LoadAreaVisualShape();//现在可以
        }

        private void GraphyControl_MouseLeftButtonDownGetVisual(object sender, EventArgs e)
        {
            VisualShape visual = sender as VisualShape;
            //if (visual is AreaVisualShape)
            //{
            //    ZoomContent zc = this._graphyControl.GetZoomContent();
            //    zc.RenderTransformOrigin = new Point(0.5, 0.5);  //0.5,0.5表示中心点
            //    zc.RenderTransform = new RotateTransform(-visual.Angle);
            //    EditArea(visual.Data);
            //    return;
            //}
            if (visual is MxVisualShape)
            {
                IoC.Get<IGlobalData>().CurrentMx = IoC.Get<IGlobalData>().GetMxRO(new Guid(visual.Data.ID));
                // EditMx(visual.Data);
                return;
            }
            IoC.Get<IGlobalData>().CurrentMx = null;
        }


        private bool? EditMx(IShapeBaseData mxData, bool isEdit = false)
        {
            Lygl.UI.Edit.ViewModels.EditMxViewModel um = new Lygl.UI.Edit.ViewModels.EditMxViewModel(mxData, isEdit);
            Dictionary<string, object> settings = new Dictionary<string, object> { { "ResizeMode", ResizeMode.NoResize } };
            return IoC.Get<IWindowManager>().ShowDialog(um, null, settings);
            //return IoC.Get<IWindowManager>().ShowDialog(um, null, settings);

        }
        private bool? EditArea(IShapeBaseData areaData, bool isEdit = false)
        {
            Lygl.UI.Edit.ViewModels.EditAreaViewModel um = new Lygl.UI.Edit.ViewModels.EditAreaViewModel(areaData, isEdit);
            Dictionary<string, object> settings = new Dictionary<string, object> { { "ResizeMode", ResizeMode.NoResize } };
            return IoC.Get<IWindowManager>().ShowDialog(um, null, settings);
        }

        private bool? EditPath(IShapeBaseData pathData, bool isEdit = false)
        {
            Lygl.UI.Edit.ViewModels.EditPathViewModel um = new Lygl.UI.Edit.ViewModels.EditPathViewModel(pathData, isEdit);
            Dictionary<string, object> settings = new Dictionary<string, object> { { "ResizeMode", ResizeMode.NoResize } };
            return IoC.Get<IWindowManager>().ShowDialog(um, null, settings);
        }

        /// <summary>
        /// 订阅视图的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GraphyControl_ObjectOperate(object sender, ObjectOperateEventArgs e)
        {
            switch (e.Operation)
            {
                case ObjectOperation.AddArea:
                    AreaEdit newArea = AreaEdit.NewAreaEdit();
                    newArea.AreaID = new Guid(e.VisualShape.Data.ID);
                    newArea.Name = e.VisualShape.Data.Name;
                    PathGeometry pg = new PathGeometry();
                    pg.AddGeometry((e.VisualShape as AreaVisualShape).Geometry);
                    newArea.GeometryText = pg.ToString(new Lygl.UI.Framework.FormatProvider.GeometryIntFormatProvider());
                    newArea.Angle = 0;
                    newArea.ApplyEdit();
                    AreaEdit tempArea = newArea.Clone();
                    newArea = tempArea.Save();
                    if (!EditArea(e.VisualShape.Data, true) ?? false)
                    {
                        //撤消建立
                        e.IsCancel = true;
                        newArea.Delete();
                        tempArea = newArea.Clone();
                        newArea = tempArea.Save();
                        return;
                    }
                    AreaROL.GetAreaROL().First();
                    Model.Add(newArea.AreaID);
                    IoC.Get<IGlobalData>().AddAreaMxListDict(newArea.AreaID);   //更新全局列表缓存
                    //todo need update GlobalData.AreaMxROLDict
                    break;
                case ObjectOperation.AddPath:
                    PathEdit newPath = PathEdit.NewPathEdit();
                    newPath.PathID = new Guid(e.VisualShape.Data.ID);
                    newPath.Name = e.VisualShape.Data.Name;
                    PathGeometry pgPath = new PathGeometry();
                    pgPath.AddGeometry((e.VisualShape as PathVisualShape).Geometry);
                    newPath.GeometryText = pgPath.ToString(new Lygl.UI.Framework.FormatProvider.GeometryIntFormatProvider());
                    newPath.ApplyEdit();
                    PathEdit tempEdit = newPath.Clone();
                    newPath = tempEdit.Save();
                    if (!EditPath(e.VisualShape.Data, true) ?? false)
                    {
                        //撤消建立
                        e.IsCancel = true;
                        newPath.Delete();
                        tempEdit = newPath.Clone();
                        newPath = tempEdit.Save();
                        return;
                    }
                    //to do refresh
                    //PathROL.GetPathROL().First();
                    //Model.Add(newPath.PathID);
                    //IoC.Get<IGlobalData>().AddAreaMxListDict(newArea.AreaID);   //更新全局列表缓存
                    break;
                case ObjectOperation.AddMx:
                    //查找Mx所在的区域
                    AreaRO currentArea = null;
                    foreach (AreaRO item in Model)
                    {
                        Guid areaGuid = new Guid((e.VisualShape.Parent as AreaVisualShape).UniqueId);
                        object itemGuid = item.AreaID;
                        if (itemGuid.Equals(areaGuid))
                        {
                            currentArea = item;
                            break;
                        }
                    }
                    if (currentArea == null) throw new Exception("不能在空区域添加墓穴");
                    MxEdit newMx = MxEdit.NewMxEdit();
                    newMx.MxID = new Guid((e.VisualShape as MxVisualShape).UniqueId);
                    newMx.MxName = currentArea.Name + e.VisualShape.Data.Name;
                    newMx.Pos = string.Format("{0:F0},{1:F0}", (e.VisualShape as MxVisualShape).OrgPos.X, (e.VisualShape as MxVisualShape).OrgPos.Y);
                    newMx.MxStatus = MxStatusNVL.GetMxStatusNVL().Value(0);      //表示初始状态 Convert.ToInt32(MxStatus.DaiShou);
                    newMx.MxType = MxTypeNVL.GetMxTypeNVL().Value(0); // Convert.ToInt32(MxType.DanRen);
                    newMx.MxStyle = MxStyleNVL.GetMxStyleNVL().Value(1);
                    newMx.AreaID = currentArea.AreaID;
                    newMx.Angle = currentArea.Angle;
                    MxEdit cloneMx = newMx.Clone();
                    newMx = cloneMx.Save();
                    IoC.Get<IGlobalData>().AddAreaMxListDict(currentArea.AreaID);   //更新全局列表缓存
                    IoC.Get<IGlobalData>().CurrentMx = IoC.Get<IGlobalData>().GetMxRO(newMx.MxID);
                    //编辑形状数据
                    if (!EditMx(e.VisualShape.Data, true) ?? false)
                    {
                        //撤消建立
                        e.IsCancel = true;
                        newMx.Delete();
                        cloneMx = newMx.Clone();
                        newMx = cloneMx.Save();
                        IoC.Get<IGlobalData>().AddAreaMxListDict(currentArea.AreaID);   //更新全局列表缓存
                        return;
                    }
                    break;
                case ObjectOperation.Delete:
                    break;
                case ObjectOperation.Update:
                    break;
                case ObjectOperation.ContextMenuAdjust:

                    break;
                case ObjectOperation.PopupContextMenu:
                    PopupVisualShapeContextMenu(e.VisualShape);
                    break;
                default:
                    break;
            }
        }

        private void PopupVisualShapeContextMenu(VisualShape visualShape)
        {
            if (visualShape is MxVisualShape)
            {
                IoC.Get<IGlobalData>().CurrentMx = IoC.Get<IGlobalData>().GetMxRO(new Guid(visualShape.Data.ID));
                ContextMenu textmenu = new ContextMenu();
                IList<ICommandMessage> cmList = IoC.Get<ICommandMessageAggregator>().GetGroup(new string[] { "DispBusiness", "InvoiceShare" });
                foreach (var item in cmList)
                {
                    JpMenuItem mi = new JpMenuItem(item);

                    textmenu.Items.Add(mi);
                }

                //ClearText.Click += new RoutedEventHandler(btnOpenFile_Click);
                //MenuItem SaveText = new MenuItem();
                //SaveText.Tag = "SaveOCR";
                this._graphyControl.ContextMenu = textmenu;
                this._graphyControl.ContextMenu.IsOpen = true;
            }
        }
        //装载区域和墓穴数据
        private void LoadAreaVisualShape()
        {

            foreach (var area in this.Model)
            {

                Geometry pg = Geometry.Parse(area.GeometryText);
                AreaData ad = new AreaData() { ID = area.AreaID.ToString(), Name = area.Name, Angle = area.Angle };
                // Geometry rectGeometry = new RectangleGeometry(new Rect(_drawShapeRecord.StartPoint.X, _drawShapeRecord.StartPoint.Y, _drawShapeRecord.Shape.Width, _drawShapeRecord.Shape.Height));
                AreaVisualShape avs = new AreaVisualShape(ad, pg);
                _graphyControl.GetVisualLayers().ToList().Find(c => c.Name == "AreaLayer").AddObject(avs);
                //this.content.GetVisualLayer("AreaLayer").AddObject(avs);


                //if (area.Mxs.Count > 0)
                //MxROL mxs = MxROL.GetMxROLByAreaID(area.AreaID);
                MxROL mxs = IoC.Get<IGlobalData>().AddAreaMxListDict(area.AreaID);
                // GlobalData.AreaMxROLDict.Add(area.AreaID, mxs);          //_mxROLDict.Add(area.AreaID, mxs);
                if (mxs.Count > 0)
                {
                    //foreach (Mx mx in area.Mxs)
                    foreach (MxRO mx in mxs)
                    {
                        MxVisualShape mxShape = new MxVisualShape(mx as IMx);
                        if (mx.MxStatusID == 3 || mx.MxStatusID == 48 || mx.MxStatusID == 4 || mx.MxStatusID == 5 || mx.MxStatusID == 39)
                            mxShape.IsFlash = true;
                        avs.AddObject(mxShape);
                    }
                }
            }
        }

        //装载路径
        private void LoadPathVisualShape()
        {
            PathROL pathlist = PathROL.GetPathROL();
            foreach (var path in pathlist)
            {

                Geometry pg = Geometry.Parse(path.GeometryText);
                PathData ad = new PathData() { ID = path.PathID.ToString(), Name = path.Name };
                PathVisualShape avs = new PathVisualShape(ad, pg);
                _graphyControl.GetVisualLayers().ToList().Find(c => c.Name == "PathLayer").AddObject(avs);

            }
        }

        //重画整个区域
        public void DrawArea(string areaID, MxROL mxs)
        {
            try
            {
                VisualLayer areaLayer = _graphyControl.GetVisualLayers().ToList().Find(c => c.Name == "AreaLayer");
                VisualShape areaShape = areaLayer.GetObject(areaID);
                areaShape.Clear();

                if (mxs.Count > 0)
                {
                    //foreach (Mx mx in area.Mxs)
                    foreach (MxRO mx in mxs)
                    {
                        MxVisualShape mxShape = new MxVisualShape(mx as IMx);
                        if (mx.MxStatusID == 48) mxShape.IsFlash = true;
                        areaShape.AddObject(mxShape);
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("没能找到区域对应的形状");
            }
        }
        /// <summary>
        /// 在Grahp视图中查找墓穴对应的形状数据
        /// </summary>
        /// <param name="mx">要查找的墓穴</param>
        /// <returns>墓穴形状</returns>
        //public AreaVisualShape FindAreaShape(string areaID)
        //{
        //    try
        //    {
        //        VisualLayer areaLayer = _graphyControl.GetVisualLayers().ToList().Find(c => c.Name == "AreaLayer");
        //        VisualShape areaShape = areaLayer.GetObject(areaID);
        //        return areaShape as AreaVisualShape;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //        throw new Exception("没能找到区域对应的形状");
        //    }

        //}
        /// <summary>
        /// 在Grahp视图中查找墓穴对应的形状数据
        /// </summary>
        /// <param name="mx">要查找的墓穴</param>
        /// <returns>墓穴形状</returns>
        public MxVisualShape FindMxShape(MxRO mx)
        {
            VisualLayer areaLayer = _graphyControl.GetVisualLayers().ToList().Find(c => c.Name == "AreaLayer");
            VisualShape areaShape = areaLayer.GetObject(mx.AreaID.ToString());
            VisualShape mxShape = areaShape.GetObject(mx.MxID.ToString());
            return mxShape as MxVisualShape;
        }

        //视图加载时获取jpGraphyControl,保存到变量中
        //protected override void OnViewLoaded(object view)
        //{
        //    base.OnViewLoaded(view);
        //    _graphyControl = (JpGraphyControl)(view as FrameworkElement).FindName("GraphyControl");
        //    _graphyControl.ObjectOperate += new ObjectOperateEventHandler(GraphyControl_ObjectOperate);
        //    _graphyControl.MouseLeftButtonDownGetVisual += new EventHandler(GraphyControl_MouseLeftButtonDownGetVisual);
        //    _graphyControl.MouseLeftDoubleClickVisual += new EventHandler(_graphyControl_MouseLeftDoubleClickVisual);
        //    _graphyControl.UpdateMap += new UpdateMapEventHandler(_graphyControl_UpdateMap);
        //    //更新shape的位置时，提供写入数据到库中
        //    _graphyControl.UpdateShapePos +=new UpdateShapePosEventHandler(_graphyControl_UpdateShapePos); 
        //    GraphyControlLoaded();
        //}
        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            //_graphyControl = (JpGraphyControl)(view as FrameworkElement).FindName("GraphyControl");
            //_graphyControl.ObjectOperate += new ObjectOperateEventHandler(GraphyControl_ObjectOperate);
            //_graphyControl.MouseLeftButtonDownGetVisual += new EventHandler(GraphyControl_MouseLeftButtonDownGetVisual);
            //_graphyControl.MouseLeftDoubleClickVisual += new EventHandler(_graphyControl_MouseLeftDoubleClickVisual);
            //_graphyControl.UpdateMap += new UpdateMapEventHandler(_graphyControl_UpdateMap);
            ////更新shape的位置时，提供写入数据到库中
            //_graphyControl.UpdateShapePos += new UpdateShapePosEventHandler(_graphyControl_UpdateShapePos);
            //GraphyControlLoaded();
            _viewport = (view as FrameworkElement).FindName("MainViewport") as Viewport3DX;
            //_naviViewport = (view as FrameworkElement).FindName("NaviViewport") as HelixToolkit.Wpf.HelixViewport3D;
            LoadArea();
            LoadArea2MainViewport(this.Model.First().AreaID);
        }
        //装载区域和墓穴数据 到 导航视图
        private void LoadArea()
        {
            int i=0;

           #region 装载墓区模型
		  string path = AppDomain.CurrentDomain.BaseDirectory;
          path += "..\\..\\..\\3dModel\\41.3ds";
          HelixToolkit.Wpf.FileModelVisual3D visualMq = new HelixToolkit.Wpf.FileModelVisual3D() { Source = path };
          //_viewport.Children.Add(visualMq);
          _naviViewport.Children.Add(visualMq);
	#endregion

            foreach (var area in this.Model)
            //var area = this.Model.First();
            {
                
                AreaData ad = new AreaData() { ID = area.AreaID.ToString(), Name = area.Name, Angle = area.Angle };
                // Geometry rectGeometry = new RectangleGeometry(new Rect(_drawShapeRecord.StartPoint.X, _drawShapeRecord.StartPoint.Y, _drawShapeRecord.Shape.Width, _drawShapeRecord.Shape.Height));
                //--AreaVisualShape avs = new AreaVisualShape(ad, pg);
                //--_graphyControl.GetVisualLayers().ToList().Find(c => c.Name == "AreaLayer").AddObject(avs);
                //this.content.GetVisualLayer("AreaLayer").AddObject(avs);

                //_viewport.Children.Add( CreateAreaVisual3D(area));
                _naviViewport.Children.Add(CreateAreaVisual3D(area));
                #region load mx in each area
                /*
                //if (area.Mxs.Count > 0)
                //MxROL mxs = MxROL.GetMxROLByAreaID(area.AreaID);
                MxROL mxs = IoC.Get<IGlobalData>().AddAreaMxListDict(area.AreaID);
                // GlobalData.AreaMxROLDict.Add(area.AreaID, mxs);          //_mxROLDict.Add(area.AreaID, mxs);
                if (mxs.Count > 0)
                {
                    ModelVisual3D visual1 = new ModelVisual3D();
                    Model3DGroup mg = new Model3DGroup();
                    foreach (MxRO mx in mxs)
                    {
                        //{1,0,0,0,0,1,0,0,0,0,1,0,3.51446283830748,0,0,1}
                        //JpModelVisual3D mxModel = new JpModelVisual3D() { Content = Jp3DKit.JpModelStream.Visual3DModel };
                       
                        //var matrix = new MatrixTransform3D();
                        //var matrix3D= new Matrix3D(){ M11=1,M12=0,M13=0,M14=0,M21=0,M22=1,M23=0,M24=0,M31=0,M32=0,M33=1,M34=0,OffsetX=3.51446283830748,OffsetY=0,OffsetZ=0,M44=1};
                        //matrix.Matrix= new Matrix3D();
                        Point p = Point.Parse(mx.Pos);
                        p.X = p.X / 100;
                        p.Y = p.Y / 100;
                        Matrix3D matrix3D = new Matrix3D() { M11 = 1, M12 = 0, M13 = 0, M14 = 0, M21 = 0, M22 = 1, M23 = 0, M24 = 0, M31 = 0, M32 = 0, M33 = 1, M34 = 0, OffsetX = p.X, OffsetY = p.Y, OffsetZ = 30, M44 = 1 };
                        Model3DGroup mxmg = new Model3DGroup();
                        //mxmg.Children.Add(Jp3DKit.JpModelStream.Visual3DModel as Model3DGroup);
                       
                        
                        mxmg.Children.Add(Jp3DKit.JpModelStream.Visual3DModel as Model3DGroup);
                        mxmg.Transform = new MatrixTransform3D(matrix3D);
                        mg.Children.Add(mxmg);
                        

                        //mxModel.Transform = new MatrixTransform3D(matrix3D);
                        //viewport.Children.Add(mxModel);
                        i++;
                        //if (i == 10)
                        //    break;
                        MxVisualShape mxShape = new MxVisualShape(mx as IMx);
                        //if (mx.MxStatusID == 3 || mx.MxStatusID == 48 || mx.MxStatusID == 4 || mx.MxStatusID == 5 || mx.MxStatusID == 39) 
                        //    mxShape.IsFlash = true;
                        //avs.AddObject(mxShape);
                    }
                    visual1.Content = mg;
                    //_viewport.Children.Add(visual1);
                    _naviViewport.Children.Add(visual1);
                }
                //_naviViewport.ZoomExtents();
                */
                  #endregion
            }
        }
        
        
        /// <summary>
        /// 装载选定区域到主视图中
        /// </summary>
        /// <param name="areaID"></param>
        private void LoadArea2MainViewport(Guid areaID)
        {
            int i = 0;
            ProjectionCamera camera= _viewport.Camera;
            Model3DGroup lights= _viewport.Lights;
            _viewport.Children.Clear();
            _viewport.Camera = camera;
            _viewport.Children.Add(new DefaultLights());
           

            AreaRO area = this.Model.First(item => item.AreaID == areaID);

            _viewport.Children.Add( CreateAreaTubeVisual3D(area));

                AreaData ad = new AreaData() { ID = area.AreaID.ToString(), Name = area.Name, Angle = area.Angle };

                //_viewport.Children.Add(CreateAreaVisual3D(area));
                MxROL mxs = IoC.Get<IGlobalData>().AddAreaMxListDict(area.AreaID);
                if (mxs.Count > 0)
                {
                    ModelVisual3D visual1 = new ModelVisual3D();
                    Model3DGroup mg = new Model3DGroup();
                    foreach (MxRO mx in mxs)
                    {
                        Point p = Point.Parse(mx.Pos);
                        p.X = p.X / 100;
                        p.Y = p.Y / 100;
                        Matrix3D matrix3D = new Matrix3D() { M11 = 1, M12 = 0, M13 = 0, M14 = 0, M21 = 0, M22 = 1, M23 = 0, M24 = 0, M31 = 0, M32 = 0, M33 = 1, M34 = 0, OffsetX = p.X, OffsetY = p.Y, OffsetZ = 30, M44 = 1 };
                        Model3DGroup mxmg = new Model3DGroup();
                        mxmg.Children.Add(Jp3DKit.JpModelStream.Visual3DModel as Model3DGroup);
                        mxmg.Transform = new MatrixTransform3D(matrix3D);
                        mg.Children.Add(mxmg);
                        i++;
                        MxVisualShape mxShape = new MxVisualShape(mx as IMx);
                    }
                    visual1.Content = mg;
                    _viewport.Children.Add(visual1);
                }
                Geometry boundGeometry = _viewport.Viewport.Clip;
                // 装载墓区模型
                string path = AppDomain.CurrentDomain.BaseDirectory;
                path += "..\\..\\..\\3dModel\\41.3ds";
                HelixToolkit.Wpf.FileModelVisual3D visualMq = new HelixToolkit.Wpf.FileModelVisual3D() { Source = path };


                _viewport.Children.Add(visualMq);
                _viewport.Viewport.Clip = boundGeometry;
                _viewport.Viewport.ClipToBounds = true;
        }

        /// <summary>
        /// Create a visual 3d  
        /// </summary>
        /// <param name="pg">
        /// pg is the ploygon geometry
        /// </param>
        /// <returns></returns>
        private AreaVisual3D CreateAreaVisual3D(AreaRO area)
        {
            Geometry pg = Geometry.Parse(area.GeometryText);
            MeshBuilder mb = new MeshBuilder(false,false);
            List<PathFigure> pgf = pg.GetFlattenedPathGeometry().Figures.ToList();
            //if (pgf.Count > 1)
            //{
            //    throw new Exception("area point too many");
            //}
            List<Point> list = null;
            //foreach (var item in pgf)
            {
                var item = pgf[0];
                list = GetPathFigurePoints(item);
            }
            
            mb.AddExtrudedGeometry(list, new Vector3D(1, 0, 0), new Point3D(0, 0, 41), new Point3D(0, 0, 40));

            List<Point3D> list1 = new List<Point3D>();
            List<Vector3D> listV = new List<Vector3D>();
            foreach (var item in list)
            {
                list1.Add(new Point3D(item.X, item.Y, 41));
                listV.Add(new Vector3D(0,0,1));
            }
            mb.AddTriangleFan(list1);
            
            AreaVisual3D av = new AreaVisual3D();
            GeometryModel3D gm = new GeometryModel3D();
            gm.Geometry = mb.ToMesh();
            gm.Material = MaterialHelper.CreateMaterial(new SolidColorBrush(Colors.Red));
            gm.BackMaterial = MaterialHelper.CreateMaterial(new SolidColorBrush(Colors.Red));
            av.Visual3DModel = gm;
           
            av.MouseLeftButtonDown += this.AreaMouseLeftButtonDown;
            //av.Name = area.Name;
            av.ID = area.AreaID;
            return av;
        }

        /// <summary>
        /// 根据二维geometry创建一个管装框
        /// </summary>   
        /// <param name="area"></param>
        /// <returns></returns>
        private TubeVisual3D CreateAreaTubeVisual3D(AreaRO area)
        {
            Geometry pg = Geometry.Parse(area.GeometryText);
            List<PathFigure> pgf = pg.GetFlattenedPathGeometry().Figures.ToList();
            
            List<Point> list = GetPathFigurePoints(pgf[0]);
            list.RemoveAt(list.Count-1);
            TubeVisual3D ev = new TubeVisual3D();
            ev.Diameter = 1;
            ev.IsPathClosed = true;
            Point3DCollection pc = new Point3DCollection();
            foreach (var item in list)
            {
                pc.Add(new Point3D(item.X, item.Y, 41));
            }
            
            ev.Material = MaterialHelper.CreateMaterial(new SolidColorBrush(Colors.Red));
            ev.Path = pc;
            
            return ev;
        }

        private void AreaMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            AreaVisual3D av = sender as AreaVisual3D;
            LoadArea2MainViewport(av.ID);
        }

        private List<Point> GetPathFigurePoints(PathFigure item)
        {
            List<Point> points = new List<Point>();
            string s = item.ToString();
            string[] ss = s.Split(new char[] { 'M', 'L', ' ','z' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < ss.Length; i++)
            {
                Point p = Point.Parse(ss[i]);
                p.X = p.X / 100; p.Y = p.Y / 100;
                points.Add(p);
            }
            return points;
        }

        //更新shape的位置时，提供写入数据到库中
        void _graphyControl_UpdateShapePos(object sender, UpdateShapePosEventArgs e)
        {
            switch (e.ShapeType)
            {
                case 0: //area
                    AreaVisualShape avs = sender as AreaVisualShape;
                    AreaEdit ae = AreaEdit.GetAreaEdit(new Guid(avs.UniqueId));
                    ae.GeometryText = e.NewPosValue;
                    ae.Save();
                    break;
                case 1: //mx
                    MxVisualShape mvs = sender as MxVisualShape;
                    MxEdit me = MxEdit.GetMxEdit(new Guid(mvs.UniqueId));
                    me.Pos = e.NewPosValue;
                    me.Save();
                    break;
                case 2: //path
                    PathVisualShape pvs = sender as PathVisualShape;
                    PathEdit pe = PathEdit.GetPathEdit(new Guid(pvs.UniqueId));
                    pe.GeometryText = e.NewPosValue;
                    pe.Save();
                    break;
                default:
                    break;
            }

        }

        void _graphyControl_UpdateMap(object sender, UpdateMapEventArgs e)
        {
            MapVisualLayer mapVisualLayer = (MapVisualLayer)_graphyControl.GetGraphyElement().GetVisualLayer("VisualImageSharp");
            //VisualShape[] arrayShape = new JpGraphy.GraphyVisualSharp.VisualImageSharp[mapVisualLayer.SharpList.Count];
            //mapVisualLayer.SharpList.CopyTo(arrayShape);
            //mapVisualLayer.Clear();
            if (e.ContentScale > 0.35)
            {
                int x = (int)(e.MousePos.X / 350) - 1;
                int y = (int)(e.MousePos.Y / 350) - 1;
                LoadPartMapLayer(x, y, e.ContentScale, mapVisualLayer);
            }
            //else
            //{
            //    LoadMapThumb1400Layer(mapVisualLayer);
            //}
        }
        private void LoadPartMapLayer(int x, int y, double scale, MapVisualLayer mapVisualLayer)
        {
            int num = (int)(1400 / (350 * scale) / 2);
            int minx = (x - num) > 0 ? x - num : 0;
            int miny = (y - num) > 0 ? y - num : 0;

            try
            {
                for (int col = minx; col < minx + 2 * num + 1; col++)
                {
                    for (int row = miny; row < miny + 2 * num + 1; row++)
                    {
                        Uri filename = new System.Uri(string.Format(@"BitmapImage\part\{0}_{1}.jpg", col, row), System.UriKind.Relative);
                        BitmapImage biMap = new System.Windows.Media.Imaging.BitmapImage(filename);
                        JpGraphy.GraphyVisualSharp.VisualImageSharp VisualImageSharp = new JpGraphy.GraphyVisualSharp.VisualImageSharp(string.Format("Map{0}{1}", col, row), new Point(col * 350, row * 350), biMap.Width, biMap.Height);
                        VisualImageSharp.BitmapImage = biMap;
                        mapVisualLayer.AddObject(VisualImageSharp);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void LoadMapThumb1400Layer(MapVisualLayer mapVisualLayer)
        {
            Uri filename = new System.Uri(@"BitmapImage\all1400.png", System.UriKind.Relative);
            BitmapImage biMap = new System.Windows.Media.Imaging.BitmapImage(filename);
            JpGraphy.GraphyVisualSharp.VisualImageSharp VisualImageSharp = new JpGraphy.GraphyVisualSharp.VisualImageSharp("Mapthumb1400", new Point(0, 0), 14000, 14000);
            VisualImageSharp.BitmapImage = biMap;
            mapVisualLayer.AddObject(VisualImageSharp);
        }

        /// <summary>
        /// 响应双击事件
        /// 弹出编辑窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _graphyControl_MouseLeftDoubleClickVisual(object sender, EventArgs e)
        {
            VisualShape visual = sender as VisualShape;
            if (visual is AreaVisualShape)
            {
                ZoomContent zc = this._graphyControl.GetZoomContent();
                zc.RenderTransformOrigin = new Point(0.5, 0.5);  //0.5,0.5表示中心点
                zc.RenderTransform = new RotateTransform(-visual.Angle);
                IoC.Get<IGlobalData>().CurrentMx = null;
                EditArea(visual.Data);

                return;
            }
            if (visual is MxVisualShape)
            {
                IoC.Get<IGlobalData>().CurrentMx = IoC.Get<IGlobalData>().GetMxRO(new Guid(visual.Data.ID));
                EditMx(visual.Data);
                return;
            }
            if (visual is PathVisualShape)
            {
                ZoomContent zc = this._graphyControl.GetZoomContent();
                zc.RenderTransformOrigin = new Point(0.5, 0.5);  //0.5,0.5表示中心点
                zc.RenderTransform = new RotateTransform(-visual.Angle);
                IoC.Get<IGlobalData>().CurrentMx = null;
                EditPath(visual.Data);

                return;
            }
            IoC.Get<IGlobalData>().CurrentMx = null;
        }
        #endregion

       public  void RLClick()
        {
            this._viewport.CameraController.AddRotateForce(20, 20);
            //this._viewport.CameraController.ZoomExtents(0);
            //this._viewport.CameraController.rotatec            
        }
    }
}
