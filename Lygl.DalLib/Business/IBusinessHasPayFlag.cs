using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lygl.DalLib.Business
{
    /// <summary>
    /// 用于命令消息类中，以便判断业务类是否已付费
    /// </summary>
    public interface IBusinessHasPayFlag
    {
        bool PayFlag { get; }
    }
}
