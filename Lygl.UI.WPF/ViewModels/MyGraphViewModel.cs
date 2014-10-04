using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lygl.DalLib;
using System.Windows;
using System.Windows.Media.Imaging;
 
using System.ComponentModel;
using System.Windows.Media;
 
using Csla;
using Lygl.DalLib.Core;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using Lygl.UI.CommandMessage;
using Lygl.UI.Shell;
using Lygl.DalLib.NVL;
using Lygl.DalLib.Browse;
using Lygl.DalLib.Edit;
using System.Windows.Controls;

namespace Lygl.UI.ViewModels
{
    //#region Message
    //[Export(typeof(ICommandMessage))]
    //public class DrawMqMessage : CommandMessageBase
    //{
    //    public DrawMqMessage()
    //        : base()
    //    {
    //        Name = "DrawMq"; Label = "添加墓区"; ToolTip = "添加墓区"; Group = "LyView";
    //        ToolTip = "添加墓区";  IsMainMenuItem = true; Category = "陵园视图";
    //    }
    //}

    //[Export(typeof(ICommandMessage))]
    //public class DrawMxMessage : CommandMessageBase
    //{
    //    public DrawMxMessage()
    //        : base()
    //    {
    //        Name = "DrawMx"; Label = "添加墓穴"; ToolTip = "添加墓穴"; Group = "LyView";
    //        ToolTip = "添加墓区"; IsMainMenuItem = true; Category = "陵园视图";
    //    }
    //}
    //[Export(typeof(ICommandMessage))]
    //public class ModifyPosMessage : CommandMessageBase
    //{
    //    public ModifyPosMessage()
    //        : base()
    //    {
    //        Name = "ModifyPos"; Label = "修改位置和角度"; ToolTip = "修改形状的位置和角度"; Group = "LyView";
    //        IsMainMenuItem = true; Category = "陵园视图";
    //    }
    //}
    //[Export(typeof(ICommandMessage))]
    //public class DrawPathMessage : CommandMessageBase
    //{
    //    public DrawPathMessage()
    //        : base()
    //    {
    //        Name = "DrawPath"; Label = "添加道路"; ToolTip = "添加道路"; Group = "LyView";
    //        IsMainMenuItem = true; Category = "陵园视图";
    //    }
    //}
    //[Export(typeof(ICommandMessage))]
    //public class MoveShapeMessage : CommandMessageBase
    //{
    //    public MoveShapeMessage()
    //        : base()
    //    {
    //        Name = "MoveShape"; Label = "移动形状"; ToolTip = "移动形状"; Group = "LyView";
    //        IsMainMenuItem = true; Category = "陵园视图";
    //    }
    //}
    //#endregion



    /// <summary>
    /// 实现对jpGrahpyControl的包装
    /// </summary>
    [Export(typeof(MyGraphViewModel))]
    class MyGraphViewModel : Lygl.UI.Framework.ViewModelBase.ScreenWithModel<AreaROL>, 
        IHandle<DrawMqMessage>, 
        IHandle<DrawMxMessage>,
        IHandle<ModifyPosMessage>,
        IHandle<DrawPathMessage>,
        IHandle<MoveShapeMessage>
    {
        //Dictionary<Guid, MxROL> _mxROLDict;

        public MyGraphViewModel()
        {
            IoC.Get<IEventAggregator>().Subscribe(this);
            //Model = AreaList.GetAreaList();
            //MxROL.UpdateAllMxStatus();
            DataPortal.Execute(new Lygl.DalLib.DBCommand.UpdateAllMxStatusCommand());
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
            //if (_graphyControl == null) throw new ArgumentNullException("JpGraphyControl is null");
            //(_graphyControl as JpGraphyControl).Status = OperateStatus.DrawPolygon;
        }
        public void Handle(DrawPathMessage message)
        {
            //if (_graphyControl == null) throw new ArgumentNullException("JpGraphyControl is null");
            //(_graphyControl as JpGraphyControl).Status = OperateStatus.DrawPath;
        }
        public void Handle(ModifyPosMessage message)
        {
            //if (_graphyControl == null) throw new ArgumentNullException("JpGraphyControl is null");
            //(_graphyControl as JpGraphyControl).Status = OperateStatus.DrawPolygon;
        }
        public void Handle(MoveShapeMessage message)
        {
            //if (_graphyControl == null) throw new ArgumentNullException("JpGraphyControl is null");
            //(_graphyControl as JpGraphyControl).Status = OperateStatus.MoveShape;
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
            //if (_graphyControl == null) throw new ArgumentNullException("JpGraphyControl is null");
            //(_graphyControl as JpGraphyControl).Status = OperateStatus.DrawMx;
        }
        #endregion
        #endregion


        #region jpGraphyControl relation
        /*
        //private void jpGraphyControlLoaded(object sender, RoutedEventArgs e)
        public void GraphyControlLoaded()
        {
            ////BitmapImage biMap = new System.Windows.Media.Imaging.BitmapImage((new System.Uri(@"BitmapImage\background.jpg", System.UriKind.Relative))); 
            ////BitmapImage biMap = new System.Windows.Media.Imaging.BitmapImage((new System.Uri(@"BitmapImage\孝心园.png", System.UriKind.Relative)));

            //BitmapImage biMap = new System.Windows.Media.Imaging.BitmapImage();
            //biMap.BeginInit();
            //biMap.UriSource = new System.Uri(@"BitmapImage\孝心园.png", System.UriKind.Relative);
            //    biMap.CacheOption = BitmapCacheOption.None;
            //    //biMap.DecodePixelHeight = 800;
            //    //biMap.DecodePixelWidth=1024;
            //    biMap.EndInit();
            //    BitmapImage biMapThumb = new System.Windows.Media.Imaging.BitmapImage();
            //    biMapThumb.BeginInit();
            //    biMapThumb.UriSource = new System.Uri(@"BitmapImage\孝心园thumb.png", System.UriKind.Relative);
            //    biMapThumb.CacheOption = BitmapCacheOption.None;
            //    //biMap.DecodePixelHeight = 800;
            //    //biMap.DecodePixelWidth=1024;
            //    biMapThumb.EndInit();
            ////BitmapImage biMap = new System.Windows.Media.Imaging.BitmapImage((new System.Uri(@"BitmapImage\孝心园.png", System.UriKind.Relative)),); 
            //JpGraphy.GraphyVisualSharp.VisualImageSharp VisualImageSharp = new JpGraphy.GraphyVisualSharp.VisualImageSharp("Map", new Point(0, 0), biMap.Width, biMap.Height);
            ////WPFGraphicsControl.WPFGraphicsUserControl.VisualFrameworkElement.Instance.ContentHeight
            ////VisualImageSharp.BitmapImage = new System.Windows.Media.Imaging.BitmapImage((new System.Uri(@"BitmapImage\虎门码头平面图.jpg", System.UriKind.Relative)));

            //VisualImageSharp.BitmapImage = biMapThumb;

            //JpGraphyControl.SetContentSize(biMap.Width, biMap.Height);
            JpGraphyControl.SetContentSize(14000, 14000);//*(96/72)
            
             MapVisualLayer mapVisualLayer = new MapVisualLayer("VisualImageSharpThumb"); //bottom thumb map
             MapVisualLayer mapVisualLayerPart = new MapVisualLayer("VisualImageSharp");
             LoadMapThumb1400Layer(mapVisualLayer);
             

            //设置图形的大小
            
           
            //VisualLayer visualImage = new VisualLayer("VisualImageSharp");
            //visualImage.AddObject(VisualImageSharp);

            //_graphyControl.AddVisualLayers(visualImage);

           
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
                    newPath.PathID= new Guid(e.VisualShape.Data.ID);
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
                AreaData ad = new AreaData() { ID = area.AreaID.ToString(), Name = area.Name,Angle=area.Angle };
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
        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            _graphyControl = (JpGraphyControl)(view as FrameworkElement).FindName("GraphyControl");
            _graphyControl.ObjectOperate += new ObjectOperateEventHandler(GraphyControl_ObjectOperate);
            _graphyControl.MouseLeftButtonDownGetVisual += new EventHandler(GraphyControl_MouseLeftButtonDownGetVisual);
            _graphyControl.MouseLeftDoubleClickVisual += new EventHandler(_graphyControl_MouseLeftDoubleClickVisual);
            _graphyControl.UpdateMap += new UpdateMapEventHandler(_graphyControl_UpdateMap);
            //更新shape的位置时，提供写入数据到库中
            _graphyControl.UpdateShapePos +=new UpdateShapePosEventHandler(_graphyControl_UpdateShapePos); 
            GraphyControlLoaded();
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
                LoadPartMapLayer(x, y, e.ContentScale,mapVisualLayer);
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
        }*/
        #endregion
    }
}
