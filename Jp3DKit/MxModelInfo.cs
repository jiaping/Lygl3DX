using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jp3DKit
{
    /// <summary>
    /// 实体对象与模型对象的对应关系信息
    /// 用来表示可视模型对应逻辑对象的相关数据，如模型代表对象ID，Name等
    /// 用于显示数据用，也用来将图形数据传递到外部
    /// </summary>
    public class MxModelInfo:IEquatable<MxModelInfo>
    {
       
        /// <summary>
        /// 逻辑对象的ID,唯一标示对象
        /// 为了优化速度，简化操作，字符串由二部分组成，{int}:{guid},整数表示实体对象类型，guid表示对象的ID
        /// </summary>
        public  string MxID { get; set; }

        
        /// <summary>
        /// 表示模型从空间原点移动到实际位置的变换矩阵
        /// 位置也就是从模型局部坐标到世界坐标的变换矩阵，
        /// </summary>
        public Matrix ModelPos { get; set; }

        /// <summary>
        /// 3D模型文件名， 根据墓穴类型、状态确定的显示模型文件名
        /// </summary>
        public string ModelFileName { get; set; }

        public MxModelInfo(string mxID)
        {
            MxID = mxID;
            ModelPos = Matrix.Identity;
            this.ModelFileName = "Mx.obj";
        }

        public MxModelInfo(string mxID,string modelFileName)
        {
            this.MxID = mxID; 
            this.ModelPos = Matrix.Identity;
            this.ModelFileName = modelFileName;
        }
        public MxModelInfo(string mxID,Matrix pos,string modelFileName)
        {
            this.MxID = mxID;
            this.ModelPos = pos;
            this.ModelFileName = modelFileName;
        }

        public bool Equals(MxModelInfo other)
        {
            if (this.MxID == other.MxID) return true; else return false;
        }

        /// <summary>
        ///   获取墓区中墓穴显示分类名
        /// 格式为：areaid(墓区ID)+":"+墓穴所显示3D模型名,作为分类名
        /// </summary>
        /// <param name="areaID"></param>
        /// <param name="mmi">MxModelINfo</param>
        /// <returns></returns>
        public static string GetAreaMxsClassifyName(string areaID, MxModelInfo mmi)
        {
            string classifyName = string.Format("{0}:{1}", areaID, mmi.ModelFileName);
            return classifyName;
        }
    }
}
