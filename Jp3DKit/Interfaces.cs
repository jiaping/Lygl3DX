using HelixToolkit.Wpf.SharpDX;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Jp3DKit
{
    public interface IMouseMoveHitable:IVisible,IInputElement
    {
        bool MouseMoveHitTest(Ray ray, ref List<HitTestResult> hits);

        bool IsMouseMoveHitTestVisible { get; set; }
        bool IsLighting { get; set; }
    }
}
