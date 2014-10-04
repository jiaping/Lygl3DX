using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lygl.UI.Message
{

    /// <summary>
    /// 命令类型
    /// </summary>
    public enum MessageType
    {
        Simple,          //默认为Simple，由一个按钮触发或者Group的MenuItem
        Menu,
        //PopupWindow,     //弹出二级窗口 
        //SingleChoice,    //单选
    }

    /// <summary>
    /// 命令的分类
    /// </summary>
    public enum MessageCategory
    {
        Unspecified,
        //Filter,
        RecordEdit,
        View,
        Print,
        RecordsNavigation,
        Search,
        ViewsHistoryNavigation,
        ViewsNavigation,
        Export,
        Options,
        Tools,
        About
    }

}
