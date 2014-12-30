using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jp3DKit
{
    public enum OperateMode
    {
        None = 0,
        /// <summary>
        /// 画墓区
        /// </summary>
        DrawMq = 1,//画多边形
        /// <summary>
        /// 点击创建墓穴
        /// </summary>
        DrawMx = 2,    
        DrawPath = 3,
        Delete = 4,
        MoveShape = 5,
        /// <summary>
        /// 修改墓穴位置
        /// </summary>
        ModifyMxPos=6, 
        /// <summary>
        /// 修改墓区位置和顶点
        /// </summary>
        ModifyMq=7 ,  //
        DrawPolygonOld = 8//画多边形
    }

    public enum MouseMode
    {
        None,
        DraggingRectangles,
        Panning,
        Zooming,
        DragZooming,

        Add,
        Move,
        Delete
    }
}
