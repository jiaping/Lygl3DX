using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lygl.DalLib.Browse;
using System.ComponentModel.Composition;
using Lygl.UI.Framework;
using Lygl.DalLib.UserManager;
using Caliburn.Micro;
using Lygl.UI.ViewModels;
 
using Lygl.UI.Shell;
using Lygl.UI.CommandMessage;
using Lygl.DalLib.Edit;
using System.Windows;
using System.Windows.Media;
using Jp3DKit;

namespace Lygl.UI
{
    public interface IGlobalData
    {
        //全局墓区字典
        AreaROL Areas { get; }
      //  AreaRO CurrentMq { get; set; }
        MxRO CurrentMx { get; set; }
        JPViewport3DX ViewPort3DX { get; set; }

        //全局缓存中的只读墓区、墓穴对象，该缓存是在加载Grahpy视图时，同步的
        //如果对数据进行了修改，也要即时更新
        MxROL AreaMxsDictAdd(Guid areaID);
        //void AddAreaMxListDict(Guid areaID, MxROL mxs);
        MxROL GetMxROLByAreaID(Guid areaID);
         
        //获取全局缓存中的只读墓穴对象，该缓存是在加载Grahpy视图时，同步的
        MxRO GetMxRO(Guid areaID, Guid mxID);
        MxRO GetMxRO(Guid mxID);
        //string GetAreaid(string mxid);
        //void UpdateMxRO(Guid areaID, Guid mxID);   对单个的更新合并到整个区域的更新，因为区域是个整体

        #region 更新全局缓存,同时更新Graphy视图
        //保存Mx状态数据，同时更新Mx全局缓存,更新Graphy视图
        void UpdateMxStatus(MxRO mx);
        //更新Mx全局缓存,更新Graphy视图
         void UpdateMx(MxEdit mx);
         //更新Area全局缓存，同时更新Graphy视图
         //void UpdateArea(Guid areaID);
        #endregion 

         

          bool? ShowDialog<T>(T viewModel, string[] settingFlags = null);
        
    }

    [Export(typeof(IGlobalData))]
    public  class GlobalData:IGlobalData
    {
        /// <summary>
        /// 全局墓区字典
        /// </summary>

        public AreaROL Areas { get;private set; }
        ///全局缓存中的只读墓穴对象，以墓区为键分类
        ///每一墓区只有唯一列表，而ModelInstanceManage中AreaMxModelClassifyDict又将墓区分成几个不同的列表来显示不同类型墓穴
        private static Dictionary<Guid, MxROL> AreaMxROLDict;

       // public AreaRO CurrentMq { get; set; }
        public JPViewport3DX ViewPort3DX { get; set; }

        

        public  GlobalData()
        {
            Areas = AreaROL.GetAreaROL();
            AreaMxROLDict = new Dictionary<Guid, MxROL>();
        }




        #region CurrentMx
        private MxRO _currentMx;
        public MxRO CurrentMx
        {
            get
            {
                return _currentMx;
            }
            set
            {
                _currentMx = value;
               
                IoC.Get<IEventAggregator>().Publish(new CurrentMxChangedMessage(_currentMx),Execute.OnUIThread);
                if (_currentMx != null)
                {
                    var activeItem = (IoC.Get<IShell>() as IHaveActiveItem).ActiveItem;
                    //MyGraphViewModel vm = activeItem as MyGraphViewModel;
                    //if (vm != null)
                    //{
                    //    //MxVisualShape mx = vm.FindMxShape(_currentMx);
                    //    //mx.IsSelected = true;
                    //}
                }
            }
        }

        #endregion 
        #region 墓穴区域和墓穴的只读数据的全局缓存
        

        /// <summary>
        /// 加载墓区中墓穴数据到全局字典中,该数据与数据库中MxROL数据一致
        /// </summary>
        /// <param name="areaID">需加载的墓区ID</param>
        /// <returns>添加后返回添加列表的引用</returns>
        public  MxROL   AreaMxsDictAdd(Guid areaID)
        {
            MxROL mxs = MxROL.GetMxROLByAreaID(areaID);

            if (AreaMxROLDict.ContainsKey(areaID))
            {
                AreaMxROLDict.Remove(areaID);
            }
            AreaMxROLDict.Add(areaID, mxs);
            return mxs;
        }


        //public  void AddAreaMxListDict(Guid areaID, MxROL mxs)
        //{
        //    AreaMxROLDict.Add(areaID, mxs);
        //}
        public MxROL GetMxROLByAreaID(Guid areaID)
        {
            MxROL mxs;
            AreaMxROLDict.TryGetValue(areaID, out mxs);
            return mxs;

        }
        //获取全局缓存中的只读墓穴对象，该缓存是在加载Grahpy视图时，同步的
        public  MxRO GetMxRO(Guid areaID, Guid mxID)
        {
            MxROL mxList;
            AreaMxROLDict.TryGetValue(areaID,out mxList);

            foreach (var item in mxList)
            {
                if (item.MxID == mxID)
                {
                    return item;
                }
            }
            throw new Exception(string.Format("not find Mx={0} in GlobalData.AreaMxROLDict!",mxID));
        }

        //获取全局缓存中的只读墓穴对象，该缓存是在加载Grahpy视图时，同步的
        public MxRO GetMxRO( Guid mxID)
        {
            foreach (var mxList in AreaMxROLDict.Values)
            {
                MxROL mxs =mxList as MxROL;
                foreach (var item in mxs)
                {
                    if (item.MxID == mxID)
                    {
                        return item;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 根据墓穴ID查找所在墓区
        /// </summary>
        /// <param name="mxid">墓穴id　string</param>
        /// <returns>返回墓区ID字符串</returns>
        //public string GetAreaid(string mxid)
        //{
        //    foreach (var dictItem in AreaMxROLDict)
        //    {
        //        foreach (var mxItem in dictItem.Value)
        //        {
        //            if (mxItem.MxID.ToString() == mxid)
        //            {
        //                return dictItem.Key.ToString();
        //            }
        //        }
        //    }
        //    return null;
        //}

        #region 更新全局缓存,同时更新Graphy视图
        //保存Mx状态数据，同时更新Mx全局缓存,更新Graphy视图
        public void UpdateMxStatus(MxRO mx)
        {
            Lygl.DalLib.Util.CommonFactory.UpdateMxStatus(mx);
            //更新全局缓存，自动更新Graphy v
            IoC.Get<IGlobalData>().AreaMxsDictAdd(mx.AreaID);
            IoC.Get<IEventAggregator>().Publish(new CurrentMxChangedMessage(IoC.Get<IGlobalData>().GetMxRO(mx.MxID)), Execute.OnUIThread);
        }

        //更新Mx全局缓存,更新Graphy视图
        public void UpdateMx(MxEdit mx)
        {
            //更新全局缓存，自动更新Graphy
            IoC.Get<IGlobalData>().AreaMxsDictAdd(mx.AreaID);
            IoC.Get<IEventAggregator>().Publish(new CurrentMxChangedMessage(IoC.Get<IGlobalData>().GetMxRO(mx.MxID)), Execute.OnUIThread);
        }

        //更新Area全局缓存，同时更新Graphy视图
        //public void UpdateArea(Guid areaID)
        //{
        //    //更新全局缓存，自动更新Graphy
        //    IoC.Get<IGlobalData>().AreaMxsDictAdd(areaID);
        //}
        #endregion 
     
        

#endregion

        #region UI display utility

        public bool? ShowDialog<T>(T viewModel,string[] settingFlags)
        {
            Dictionary<string, object> settings = new Dictionary<string, object>() { { "ResizeMode", ResizeMode.NoResize } };
            if (settingFlags != null) 
            {
                if (settingFlags.Contains("Transparency"))
                {
                    settings.Add("Background", new SolidColorBrush(Colors.Transparent));
                    settings.Add("AllowsTransparency", true);
                    settings.Add("WindowStyle",WindowStyle.None);
                }
            }
            return IoC.Get<IWindowManager>().ShowDialog(viewModel, null, settings);;
        }
       
        #endregion 


    }
}
