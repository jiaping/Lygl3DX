using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Jp3DKit
{
    /// <summary>
    /// 表示区域对象的可视化UI－UIElement3D<-Visual3D
    /// </summary>
    public class AreaVisual3D : UIElement3D
    {
        //public string Name {get;set;}
        public Guid ID { get; set; }

        new public Model3D Visual3DModel
        {
            get { return base.Visual3DModel; }
            set { base.Visual3DModel = value; }
        }
    }
}
