//#define AREACLASSIFY
using Caliburn.Micro;
using HelixToolkit.Wpf.SharpDX;
using Jp3DKit;
 
using Lygl.DalLib.Browse;
using Lygl.DalLib.Edit;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Matrix = SharpDX.Matrix;
using GeometryModel3D = HelixToolkit.Wpf.SharpDX.GeometryModel3D;


namespace Lygl.UI.Shell
{
    /// <summary>
    /// 将mx根据状态分成组，显示不同图标，更直观，便于维护
    /// </summary>
    public static class ModelInstancesManager
    {
        /// <summary>
        /// 存储实体模型的字典库
        /// 所有操作都是围绕它展开的/n
        /// 组名-组中模型对象列表
        /// 当前的分组情况为：每一墓区根据墓穴显示模型的不同又分成几个小组
        /// 额外增加了一个　选择区，以显示被选中的对象，以及修改操作对象，增加了额外的操作功能
        /// </summary>
        public static Dictionary<string, List<Entity2ModelInfo>> AreaMxModelClassifyDict;
        /// <summary>
        /// 存储墓区模型数据，主要是geometry
        /// </summary>
        //public static Dictionary<string, string> AreaModelDict;

        /// <summary>
        /// 下面几个对象用于显示显示及修改位置　时使用
        /// </summary>
        public static JpObjModel3D SelectedModel = new JpObjModel3D() { ModelFileName = "mx.obj", Visibility = Visibility.Hidden };
        private static UICompositeManipulator3D ModelModifier = new UICompositeManipulator3D() { Visibility = Visibility.Visible };

        /// <summary>
        /// 装载实体数据到字典中
        /// 字典中每一条代表墓区中所有同一显示模型的墓穴模型信息列表
        /// </summary>
        /// <param name="areas">墓区信息</param>
        public static void LoadEntityModelInfo(AreaROL areas)
        {
            if (AreaMxModelClassifyDict == null)
                AreaMxModelClassifyDict = new Dictionary<string, List<Entity2ModelInfo>>();
            AreaMxModelClassifyDict.Clear();

            foreach (var area in areas)
            {
                AddAreaItems2MxModelInstancesDict(area.AreaID);

                #region load mx in each area
                MxROL mxs = IoC.Get<IGlobalData>().AreaMxsDictAdd(area.AreaID);
                if (mxs.Count > 0)
                {
                    foreach (MxRO mx in mxs)
                    {
                        List<Entity2ModelInfo> list = GetAreaMxModelClassifyDictItem(mx);
                        Entity2ModelInfo emi = GetEntityModelInfo(mx);
                        //var dd = emi.ModelPos.ToMatrix3D().ToString();
                        list.Add(emi);
                    }                    
                }
                #endregion
            }
        }

        /// <summary>
        /// 将墓区的分类列表添加到字典中
        /// </summary>
        /// <param name="area"></param>
        public static void AddAreaItems2MxModelInstancesDict(Guid areaID)
        {
#if AREACLASSIFY
            Entity2ModelInfo areaInfo = new Entity2ModelInfo(EntityType.MQ, areaID);
            //根据模型的不同geometry分类显示
            AreaMxModelClassifyDict.Add("DS", new List<Entity2ModelInfo>());   //
            AreaMxModelClassifyDict.Add("YS", new List<Entity2ModelInfo>());   //
            AreaMxModelClassifyDict.Add("LB", new List<Entity2ModelInfo>());   //
#else
            Entity2ModelInfo areaInfo = new Entity2ModelInfo(EntityType.MQ, areaID);
            //根据模型的不同geometry分类显示
            AreaMxModelClassifyDict.Add(areaInfo.ModelID + ":DS", new List<Entity2ModelInfo>());   //
            AreaMxModelClassifyDict.Add(areaInfo.ModelID + ":YS", new List<Entity2ModelInfo>());   //
            AreaMxModelClassifyDict.Add(areaInfo.ModelID + ":LB", new List<Entity2ModelInfo>());   //
#endif

        }

        /// <summary>
        /// 将实体信息加载到视图窗口中
        /// </summary>
        /// <param name="vp"></param>
        public static void LoadModels(JPViewport3DX vp)
        {
            JpSceneModel3D mq = new JpSceneModel3D() { ModelFileName = "all主墓区2.obj", Tag = "ALLMq" };
            vp.Items.Add(mq);
            vp.Items.Add(ModelModifier);
            vp.Items.Remove(ModelModifier);
            vp.Items.Add(SelectedModel);

            DispAreaModels(vp, IoC.Get<IGlobalData>().Areas);  //显示墓区模型

            foreach (var item in AreaMxModelClassifyDict)
            //var item = AreaMxModelClassifyDict.Single(o=>o.Key=="MQ:d9630cd3-b451-4522-87d1-89ba8a8967d9:DS" );
            {
                DispMxModelInstanceDictItem(vp, item);
            }            

            //JpObjModel3D obj = new JpObjModel3D();
            //obj.ModelFileName = "mx.obj";
            //vp.Items.Add(obj);
            //MeshBuilder mb = new MeshBuilder(true, true);
            //mb.AddTriangle(new Vector3(3, 0, -1), new Vector3(2, 0, -1), new Vector3(2, 0, 0));
            //MeshGeometryModel3D model = new MeshGeometryModel3D();
            //model.Geometry = mb.ToMeshGeometry3D();
            //model.Material = PhongMaterials.Blue;
            //vp.Items.Add(model);

            //JpTerrainModel tm = new JpTerrainModel() { HeightMapFileName = "height.bmp", TextureFileName = "Tile_Travertine_2inch.jpg" };
            //tm.LoadModel();
            //vp.Items.Add(tm);
            ////tm.Transform = new TranslateTransform3D(-50, -20, -150);
            //QuadTreeModel qt = new QuadTreeModel() { terrainModel = tm };
            //vp.Items.Add(qt);
            //vp.Items.Add(tm);
            //qt.Transform = new TranslateTransform3D(-50, -20, -150);
        }

        public static void ReLoadModels(JPViewport3DX vp)
        {
            ClearViewportAreaMxItems(vp);
            ModelInstancesManager.LoadEntityModelInfo(IoC.Get<IGlobalData>().Areas);
            DispAreaModels(vp, IoC.Get<IGlobalData>().Areas,true);  //显示墓区模型

            foreach (var item in AreaMxModelClassifyDict)
           // var item = AreaMxModelClassifyDict.Single(o=>o.Key=="MQ:d9630cd3-b451-4522-87d1-89ba8a8967d9:DS" );
            {
                DispMxModelInstanceDictItem(vp, item,true);
            }
        }
        private static void ClearViewportAreaMxItems(JPViewport3DX vp)
        {
            while (vp.Items.Count > 4)
                vp.Items.RemoveAt(vp.Items.Count-1);
        }
        /// <summary>
        /// 显示分类的墓穴组
        /// </summary>
        /// <param name="vp"></param>
        /// <param name="item"></param>
        /// <param name="forceAttach">是否需要附加模型，用于加载完成后新增模型</param>
        private static void DispMxModelInstanceDictItem(JPViewport3DX vp, KeyValuePair<string, List<Entity2ModelInfo>> item, bool forceAttach = false)
        {
            

            string modelfilename;
            switch (item.Key.Substring(item.Key.Length - 2, 2))
            {
                case "DS": modelfilename = "mx0.obj"; break;
                case "YS": modelfilename = "mx1.obj"; break;
                default:
                    modelfilename = "mx.obj";
                    break;
            }
            try
            {
                #region 墓穴对象使用实例模式
                var models = new JpMxModel3D(vp,modelfilename,item.Value,item.Key);
                    vp.Items.Add(models);
                if (forceAttach) vp.Attach(models);
                #endregion
                #region 墓穴对象不使用实例模式
                //foreach (var mxmatrix in item.Value)
                //{
                //    var models = new JpMxModel3D();
                //    models.ModelFileName = modelfilename;
                //    models.PushMatrix(mxmatrix.ModelPos);
                //    models.Tag = item.Key;
                //    vp.Items.Add(models);
                //    if (forceAttach) vp.Attach(models);
                //}
                #endregion

            }
            catch (Exception e)
            {

                throw e;
            }
        }


        /// <summary>
        /// 显示墓区图形
        /// </summary>
        /// <param name="vp"></param>
        /// <param name="areas"></param>
        private static void DispAreaModels(JPViewport3DX vp, AreaROL areas, bool forceAttach = false)
        {
            foreach (var item in areas)
            {
                if (item.GeometryText.StartsWith("M"))
                {
                    #region old area data
                    //DispAreaModelOld(vp, item);
                    #endregion
                }
                else
                {
                    DispAreaModel(vp, item,forceAttach);

                }

            }

        }
        /// <summary>
        /// 画出墓区模型，并添加到视图中
        /// </summary>
        /// <param name="vp"></param>
        /// <param name="item"></param>
        public static void DispAreaModel(JPViewport3DX vp, AreaRO item, bool forceAttach = false)
        {
            var models = new JpMqModel3D(vp, item.GeometryText, GetAreaModelTag(item));
            vp.Items.Add(models);
           if (forceAttach) vp.Attach(models);
        }
        private static void DispAreaModelOld(JPViewport3DX vp, AreaRO area)
        {
            Geometry pg = Geometry.Parse(area.GeometryText);
            MeshBuilder mb = new MeshBuilder(true, true);
            List<PathFigure> pgf = pg.GetFlattenedPathGeometry().Figures.ToList();
            //if (pgf.Count > 1)
            //{
            //    throw new Exception("area point too many");
            //}
            List<Vector2> list = null;
            //foreach (var item in pgf)
            {
                var item = pgf[0];
                list = GetPathFigurePoints(item);
            }
            //mb.AddExtrudedGeometry(list, new Vector3(1, 0, 0), new Vector3(0, 0, 41), new Vector3(0, 0, 40));

            List<Vector3> list1 = new List<Vector3>();
            List<Vector3> listV = new List<Vector3>();
            List<Vector2> listc = new List<Vector2>();
            foreach (var item in list)
            {
                list1.Add(new Vector3(item.X, 18, item.Y));
                listV.Add(new Vector3(0, 0, 1));
                listc.Add(new Vector2());
            }
            mb.AddTriangleFan(list1, listV, listc);

            MeshGeometryModel3D gm = new MeshGeometryModel3D();
            gm.Tag = GetAreaModelTag(area);
            gm.Geometry = mb.ToMeshGeometry3D();

            gm.Material = new PhongMaterial
            {
                Name = "Red",
                AmbientColor = PhongMaterials.ToColor(0.1, 0.1, 0.1, 1.0),
                DiffuseColor = PhongMaterials.ToColor(1, 0, 0, 0.2),
                SpecularColor = PhongMaterials.ToColor(0.0225, 0.0225, 0.0225, 1.0),
                EmissiveColor = PhongMaterials.ToColor(0.0, 0.0, 0.0, 1.0),
                SpecularShininess = 12.8f,
            }.Clone();
            vp.Items.Add(gm);
        }
        /// <summary>
        /// 不显示某墓区
        /// </summary>
        /// <param name="vp"></param>
        /// <param name="area"></param>
        public static void UnDispAreaModel(JPViewport3DX vp, AreaRO area)
        {
            string modelID = GetAreaModelTag(area);
            List<GeometryModel3D> findAreas = new List<GeometryModel3D>();
            foreach (var item in vp.Items)
            {
                GeometryModel3D model = item as GeometryModel3D;
                if (model == null) continue;
                if (model.Tag != null && model.Tag.ToString() == modelID)
                {
                    findAreas.Add(model);
                }
            }
            foreach (var item in findAreas)
            {
                vp.Items.Remove(item);
            }
        }

        public static void UpdateAreaMxModel(JPViewport3DX vp,string mxAreaTag)
        {

        }
        private static List<Vector2> GetPathFigurePoints(PathFigure item)
        {
            List<Vector2> points = new List<Vector2>();
            string s = item.ToString();
            string[] ss = s.Split(new char[] { 'M', 'L', ' ', 'z' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < ss.Length; i++)
            {
                System.Windows.Point pp = System.Windows.Point.Parse(ss[i]);

                Vector2 p = new Vector2((float)pp.X, (float)pp.Y);
                p.X = p.X / 100; p.Y = p.Y / 100;
                points.Add(p);
            }
            return points;
        }

        /// <summary>
        /// 根据mx信息确定显示区域字典项
        /// </summary>
        /// <param name="mx"></param>
        /// <returns></returns>
        private static List<Entity2ModelInfo> GetAreaMxModelClassifyDictItem(MxRO mx)
        {
#if AREACLASSIFY
            List<Entity2ModelInfo> list;
            switch (mx.MxStatusID)
            {
                case 0:
                    AreaMxModelClassifyDict.TryGetValue( "DS", out list);

                    break;
                case 1: AreaMxModelClassifyDict.TryGetValue( "YS", out list);
                    break;
                default:
                    AreaMxModelClassifyDict.TryGetValue( "LB", out list);
                    break;
            }
            return list;
#else
            string areaModelID = "MQ:" + mx.AreaID.ToString();
            List<Entity2ModelInfo> list;
            switch (mx.MxStatusID)
            {
                case 0:
                    AreaMxModelClassifyDict.TryGetValue(areaModelID + ":DS", out list);

                    break;
                case 1: AreaMxModelClassifyDict.TryGetValue(areaModelID + ":YS", out list);
                    break;
                default:
                    AreaMxModelClassifyDict.TryGetValue(areaModelID + ":LB", out list);
                    break;
            }
            return list;
#endif
        }

        /// <summary>
        /// 根据ModelTag信息确定显示区域字典项
        /// </summary>
        /// <param name="modelTag">"MQ:GUID:Status:MX:Guid</param>
        /// <returns></returns>
        private static List<Entity2ModelInfo> GetDictItem(string modelTag)
        {
            var ss = modelTag.Split(new char[] { ':' });
            List<Entity2ModelInfo> list;
            AreaMxModelClassifyDict.TryGetValue(ss[0] + ":" + ss[1] + ":" + ss[2], out list);
            return list;
        }
        ///// <summary>
        ///// 获取选择区中实体对象列表
        ///// </summary>
        ///// <returns></returns>
        //private static List<Entity2ModelInfo> GetSelectDictItem()
        //{
        //    List<Entity2ModelInfo> list;
        //    ModelInstancesDict.TryGetValue("SelectArea", out list);
        //    return list;
        //}
        /// <summary>
        /// 将选择的墓穴从区域列表中移动到选择区域中
        /// </summary>
        /// <param name="mx"></param>
        /// <param name="vp"></param>
        public static void StartModifyModel(string modelTag, JPViewport3DX vp)
        {
           var mxitem = GetEntityModelInfo(modelTag);
           var list = GetDictItem(modelTag);
           //var selectList = GetSelectDictItem();
           //selectList.Add(mxitem);
           //JpObjModel3D selectAreaModels = null;
           foreach (var item in vp.Items)
           {
               JpMxModel3D model = item as JpMxModel3D;
               if (model == null) continue;
               if (model.mxModel.Tag.ToString() == modelTag.Substring(0, 42))
               {
                   model.mxModel.Instances = null;
                   if (!list.Remove(mxitem)) throw new Exception("移除对象失败");
                   model.mxModel.Instances = list;
                   SelectedModel.ModelFileName = "mx.obj";
                   SelectedModel.Tag = modelTag;
                   var bb = new System.Windows.Media.Media3D.TranslateTransform3D(mxitem.ModelPos.M41, mxitem.ModelPos.M42, mxitem.ModelPos.M43);
                   SelectedModel.Transform = bb;
                   SelectedModel.Visibility = Visibility.Visible;
                   vp.Attach(ModelModifier);
                   vp.Items.Add(ModelModifier);

                   ModelModifier.Bind(SelectedModel);
                   //ModelModifier.Visibility = Visibility.Visible;

                   break;
               }
           }

         }
        /// <summary>
        /// 将选择区中模型移出，同时添加到正常的区域中
        /// </summary>
        /// <param name="vp"></param>
        public static void CompleteModelPosModify(JPViewport3DX vp)
        {
            vp.Items.Remove(ModelModifier);
            ModelModifier.Visibility = Visibility.Hidden;
            MxEdit mxEdit = MxEdit.GetMxEdit(new Guid(SelectedModel.Tag.ToString().Substring(46, 36)));
            mxEdit.Pos = SelectedModel.Transform.Value.ToString();
            mxEdit.Save(true);
            Entity2ModelInfo emi = GetEntityModelInfo(SelectedModel.Tag.ToString());
            emi.ModelPos = new Matrix(mxEdit.Pos.Split(new char[] { ',' }).Select(x => float.Parse(x)).ToArray());
            SelectedModel.Visibility = Visibility.Hidden;
            AddMxToMxModel(emi, vp);
        }

        public static void CompletePolygonModify(JPViewport3DX vp)
        {
            vp.Items.Remove(ModelModifier);
            ModelModifier.Visibility = Visibility.Hidden;
            MxEdit mxEdit = MxEdit.GetMxEdit(new Guid(SelectedModel.Tag.ToString().Substring(46, 36)));
            mxEdit.Pos = SelectedModel.Transform.Value.ToString();
            mxEdit.Save(true);
            Entity2ModelInfo emi = GetEntityModelInfo(SelectedModel.Tag.ToString());
            emi.ModelPos = new Matrix(mxEdit.Pos.Split(new char[] { ',' }).Select(x => float.Parse(x)).ToArray());
            SelectedModel.Visibility = Visibility.Hidden;
            AddMxToMxModel(emi, vp);
        }
        /// <summary>
        /// 添加模型到3D视图中
        /// </summary>
        /// <param name="item"></param>
        public static void AddMxToMxModel(Entity2ModelInfo emi, JPViewport3DX vp)
        {
            var mx = IoC.Get<IGlobalData>().GetMxRO(emi.EntityID);
            var list = GetAreaMxModelClassifyDictItem(mx);
            var mxitem = GetEntityModelInfo(mx);
            string mqModelTag = GetMxAreaModelTag(mx);
            if (list.Count == 0)  //空组，需要添加新模型到视图中
            {
                list.Add(emi);
                DispMxModelInstanceDictItem(IoC.Get<IGlobalData>().ViewPort3DX, new KeyValuePair<string, List<Entity2ModelInfo>>(mqModelTag, list), true);
            }
            else
            {
                foreach (var item in vp.Items)
                {
                    JpMxModel3D model = item as JpMxModel3D;
                    if (model == null || model.Visibility == Visibility.Hidden) continue;
                    if (model.mxModel.Tag.ToString() == mqModelTag)
                    {
                        
                        model.mxModel.Instances = null;
                        list.Add(emi);
                        model.mxModel.Instances = list;
                        vp.Attach(model);
                    }
                }
            }
        }
        /// <summary>
        /// 在3D视图中,从墓穴模型对象中删除墓穴
        /// 当墓穴模型中，墓穴被删除时，同时更新显示
        /// </summary>
        /// <param name="item"></param>
        public static void RemoveMxFormMxModel(MxRO mx, JPViewport3DX vp)
        {
            //var mx = IoC.Get<IGlobalData>().GetMxRO(emi.EntityID);
            var list = GetAreaMxModelClassifyDictItem(mx);
            var mxitem = GetEntityModelInfo(mx);
            string mqModelTag = GetMxAreaModelTag(mx);
                foreach (var item in vp.Items)
                {
                    JpMxModel3D model = item as JpMxModel3D;
                    if (model == null || model.Visibility == Visibility.Hidden) continue;
                    if (model.mxModel.Tag.ToString() == mqModelTag)
                    {

                        model.mxModel.Instances = null;
                       var mxEmi= list.Find(o => (o.EntityID == mx.MxID));
                       list.Remove(mxEmi);
                        model.mxModel.Instances = list;
                        vp.Attach(model);
                    }
                }
        }
        /// <summary>
        /// 根据墓穴信息生成相应的显示模型对象的标记属性字符串
        /// 该字符串用来标记对象
        /// </summary>
        /// <param name="mx"></param>
        /// <returns></returns>
        private static string GetMxAreaModelTag(MxRO mx)
        {
            string areaModelID = "MQ:" + mx.AreaID.ToString();
            switch (mx.MxStatusID)
            {
                case 0:
                    areaModelID += ":DS";

                    break;
                case 1: areaModelID += ":YS";
                    break;
                default:
                    areaModelID += ":LB";
                    break;
            }
            return areaModelID;
        }
       
        /// <summary>
        /// 由墓穴信息生成实体模型信息
        /// 主要是将位置POS转换成矩阵
        /// </summary>
        /// <param name="mx"></param>
        /// <returns></returns>
        private static Entity2ModelInfo GetEntityModelInfo(MxRO mx)
        {
            Matrix m;
            //这里区分位置信息是，老式的２维坐标还是，新版的变换矩阵
            m = UpdateMxPos2Dto3D(mx);
            Entity2ModelInfo emi = new Entity2ModelInfo(EntityType.MX, mx.MxID, m);
            return emi;
        }
        /// <summary>
        /// 更新墓穴位置，从2维到3维
        /// 只用于升级用
        /// </summary>
        /// <param name="mx"></param>
        /// <returns></returns>
        private static Matrix UpdateMxPos2Dto3D(MxRO mx)
        {
            Matrix m;
            if (mx.Pos.Length < 32)
            {

                System.Windows.Point p = System.Windows.Point.Parse(mx.Pos);
                p.X = p.X / 80;
                p.Y = p.Y / 60;
                m = new Matrix() { M11 = 1, M12 = 0, M13 = 0, M14 = 0, M21 = 0, M22 = 1, M23 = 0, M24 = 0, M31 = 0, M32 = 0, M33 = 1, M34 = 0, M41 = (float)p.X - 60, M42 = 18f, M43 = (float)p.Y - 140, M44 = 1 };

                if (mx.MxName.Substring(0, 3) == "孝心园")
                {
                    MxEdit mxEdit = MxEdit.GetMxEdit(mx.MxID);
                    mxEdit.Pos = string.Format("1,0,0,0,0,1,0,0,0,0,1,0,{0},{1},{2},1", p.X - 60, -3.416, p.Y - 140);
                    mxEdit.Save(true);
                    float[] cc = mxEdit.Pos.Split(new char[] { ',' }).Select(x => float.Parse(x)).ToArray();
                    m = new Matrix(cc);
                }
            }
            else
            {
                float[] cc = mx.Pos.Split(new char[] { ',' }).Select(x => float.Parse(x)).ToArray();
                m = new Matrix(cc);
            }
            return m;
        }



        /// <summary>
        /// 获取实体模型信息
        /// </summary>
        /// <param name="modelID">模型中ＩＤ</param>
        /// <returns></returns>
        private static Entity2ModelInfo GetEntityModelInfo(string modelID)
        {
            Entity2ModelInfo emi = null;
            var ss = modelID.Split(new char[] { ':' });
            if (ss[3] == "MX")    //TODO: if not =="Mx" 
            {
                MxRO mx = IoC.Get<IGlobalData>().GetMxRO(new Guid(ss[1]), new Guid(ss[4]));
                emi = GetEntityModelInfo(mx);
            }
            return emi;
        }

        private static string GetAreaModelTag(AreaRO area)
        {
            return "MQ:" + area.AreaID.ToString();
        }

        public static void ToggleModifierDisp(JPViewport3DX vp)
        {

            if (vp.Items.Contains(ModelModifier))
                vp.Items.Remove(ModelModifier);
            else
                vp.Items.Add(ModelModifier);
            //switch (ModelModifier.Visibility)
            //{
            //    case Visibility.Collapsed:
            //        break;
            //    case Visibility.Hidden:
            //        ModelModifier.Visibility = Visibility.Visible;
                    
                    
            //        break;
            //    case Visibility.Visible:
            //        //ModelModifier.Visibility = Visibility.Hidden;
            //        vp.Items.Remove(ModelModifier);
            //        break;
            //    default:
            //        break;
            //}

        }
    }
}
