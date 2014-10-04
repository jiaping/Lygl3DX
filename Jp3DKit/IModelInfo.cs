using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jp3DKit
{
    /// <summary>
    /// 用来表示可视模型对应逻辑对象的相关数据，如模型代表对象ID，Name等
    /// 用于显示数据用，也用来将图形数据传递到外部
    /// </summary>
    public interface IEntity2ModelInfo
    {

        /// <summary>
        /// 逻辑对象的ID,唯一标示对象
        /// </summary>
        string EntityID { get; set; }
        /// <summary>
        /// 表示模型从空间原点移动到实际位置的变换矩阵
        /// 位置也就是从模型局部坐标到世界坐标的变换矩阵，
        /// </summary>
        Matrix ModelPos { get; set; }
    }
    /// <summary>
    /// 实体对象类型
    /// </summary>
    public enum EntityType
    {
        MQ = 0,
        MX = 1
    }
    /// <summary>
    /// 实体对象与模型对象的对应关系信息
    /// 用来表示可视模型对应逻辑对象的相关数据，如模型代表对象ID，Name等
    /// 用于显示数据用，也用来将图形数据传递到外部
    /// </summary>
    public class Entity2ModelInfo:IEquatable<Entity2ModelInfo>
    {
        /// <summary>
        /// 用一个整数来表示实体的类型
        /// 1:表示墓区
        /// 2：表示墓穴
        /// </summary>
        public EntityType EntityType { get; set; }
        /// <summary>
        /// 逻辑对象的ID,唯一标示对象
        /// 为了优化速度，简化操作，字符串由二部分组成，{int}:{guid},整数表示实体对象类型，guid表示对象的ID
        /// </summary>
        public  Guid EntityID { get; set; }

        /// <summary>
        /// 在显示界面中标识模型，它与实体对象的标识对应 entityType:enityID
        /// </summary>
        public string ModelID
        {
            get
            {
                return EntityType.ToString() + ":" + EntityID.ToString();
            }
        }
        /// <summary>
        /// 表示模型从空间原点移动到实际位置的变换矩阵
        /// 位置也就是从模型局部坐标到世界坐标的变换矩阵，
        /// </summary>
        public Matrix ModelPos { get; set; }

        //public Entity2ModelInfo(EntityTypeEnum type, Guid id)
        //{
        //    EntityType = type;
        //    EntityID = id;
        //    ModelPos = Matrix.Identity;
        //}

        public Entity2ModelInfo(EntityType type, Guid id)
        {
            this.EntityType = type;
            this.EntityID = id; 
            this.ModelPos = Matrix.Identity;
        }
        public Entity2ModelInfo(EntityType type, Guid id,Matrix pos)
        {
            this.EntityType = type;
            this.EntityID = id;
            this.ModelPos = pos;
        }




        public bool Equals(Entity2ModelInfo other)
        {
            if (this.ModelID == other.ModelID) return true; else return false;
        }
    }
}
