using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jp3DKit
{
    public enum OperateMode
    {
        None = 0,
        DrawPolygon = 1,//画多边形
        DrawMx = 2,     //画墓穴
        DrawPath = 3,
        Delete = 4,
        MoveShape = 5,
        Select = 6,
        ModifyPolygon=7   //编辑多边形
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
