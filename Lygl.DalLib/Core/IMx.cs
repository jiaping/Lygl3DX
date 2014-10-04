using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lygl.DalLib.Core
{
    public interface IMx : IVisualLableEntity
    {
        string MxID { get; }
        string Pos { get; }
        string MxName { get; }
        String MxStatus { get; }
        string MxType { get; }
        int MxStyleID { get; }
        int Angle { get; }

        string GetImageName();
    }

    /// <summary>
    /// 从对象的数据实体（实现可视标签的）中获取标签文本
    /// 如MxRO对象
    /// </summary>
    public interface IVisualLableEntity
    {
        string GetLableText();
    }
}
