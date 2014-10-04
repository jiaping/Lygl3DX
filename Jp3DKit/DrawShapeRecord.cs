using HelixToolkit.Wpf.SharpDX;
using HelixToolkit.Wpf.SharpDX.Core;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using Point = System.Windows.Point;

namespace Jp3DKit
{
    /// <summary>
    /// 存放鼠标绘制形状的信息
    /// </summary>
    public struct DrawShapeRecord
    {
        public bool IsDraw;
        public string ShapeType;
        public Point StartPoint;
        public Point EndPoint;
        public bool IsCancel;
        //public bool IsModify;

        public LineGeometryModel3D Model;
        private int PointCount ;

        private const float uplength = 0.01f;


        public void AddPoint(Point3D p)
        {

            if (Model == null)
            {
                PointCount = 2;
                var lg = new LineGeometry3D();
                lg.Positions =new Vector3Collection( new Vector3[] { new Vector3((float)p.X, (float)p.Y + uplength, (float)p.Z), new Vector3((float)p.X, (float)p.Y + uplength, (float)p.Z) });
                lg.Indices = IntCollection.Parse("0,1"); // new int[] { 0, 1 };
                Model = new LineGeometryModel3D() { Geometry = lg,Thickness=3,Color=SharpDX.Color.LightGray };
            }
            else
            {
                PointCount++;
                var array = new Vector3[PointCount];
                
                Model.Geometry.Positions.CopyTo(array, 0);
                array[PointCount - 1] = new Vector3((float)p.X, (float)p.Y + uplength, (float)p.Z);
                Model.Geometry.Positions=new Vector3Collection( array);
                int cc = PointCount * 2;
                var indices = new int[cc ];
                Model.Geometry.Indices.CopyTo(indices, 0);
                indices[cc - 4] = PointCount - 2;
                indices[cc - 3] = PointCount - 1;
                indices[cc - 2] = PointCount - 1;
                indices[cc - 1] = 0;
                Model.Geometry.Indices =new IntCollection( indices);
            }
        }

        public void ReplaceLastPoint(Point3D p)
        {
            Vector3 lastP=Model.Geometry.Positions.LastOrDefault();
            lastP.X = (float)p.X;
            lastP.Y = (float)p.Y + uplength;
            lastP.Z = (float)p.Z;
            //var array = new Vector3[Model.Geometry.Positions.Length];
            //Model.Geometry.Positions.CopyTo(array, 0);
            //array[Model.Geometry.Positions.Length - 1] = new Vector3((float)p.X, (float)p.Y + uplength, (float)p.Z);
            //Model.Geometry.Positions = array;     
        }
        public void RemoveLastPoint()
        {
            Model.Geometry.Positions.RemoveAt(Model.Geometry.Positions.Count - 1);
            //var array = new Vector3[Model.Geometry.Positions.Length-1];
            //Array.Copy(Model.Geometry.Positions, array, array.Length);
            //Model.Geometry.Positions =array;
        }
        public void Clear()
        {
            IsDraw = false;
            IsCancel = false;
            ShapeType = string.Empty;
            StartPoint.X = 0;
            StartPoint.Y = 0;
            PointCount = 0;
            EndPoint.X=0;
            EndPoint.Y = 0;
            if (Model != null)   // && (Model.Parent != null))
            {
                Model.Dispose();    // ((Canvas)Model.Parent).Children.Remove(Model);
                Model = null;
            }
        }

    }
}
