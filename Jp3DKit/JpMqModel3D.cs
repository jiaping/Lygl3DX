using SharpDX;
using SharpDX.Direct3D11;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;


using HelixToolkit.Wpf.SharpDX;
using HelixToolkit.Wpf;
using System;
using Buffer = SharpDX.Direct3D11.Buffer;
using SharpDX.DXGI;
using SharpDX.Direct3D;
using System.Windows.Media.Imaging;
using System.Windows.Input;


namespace Jp3DKit
{

    /// <summary>
    /// loader model data from .obj
    /// </summary>
    public class JpMqModel3D : JpCompositeModel3D
    {
        private LineGeometryModel3D Line;
        private MeshGeometryModel3D Face;
        private InteractionHandle3D Modifier;
        
        private Color4 diffuseColor;


        //表示墓区是否处于修改状态
        public bool IsModify
        {
            get { return (bool)GetValue(IsModifyProperty); }
            set { SetValue(IsModifyProperty, value); }
        }

        
        public static readonly DependencyProperty IsModifyProperty =
            DependencyProperty.Register("IsModify", typeof(bool), typeof(JpMqModel3D), new PropertyMetadata(false,ModifyChanged));

        private static void ModifyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((JpMqModel3D)d).OnModifyChanged();
        }

        private  void OnModifyChanged()
        {
            if (IsModify)
            {
                
                this.Modifier.AddCornerHandles(this.Positions);
                this.Modifier.Attach(this.renderHost);
            }
            else
            {
                this.Modifier.Detach();
            }
        }
       

        //墓区的实际点集
        public Vector3[] Positions
        {
            get { return (Vector3[])GetValue(PositionsProperty); }
            set { SetValue(PositionsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Positions.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PositionsProperty =
            DependencyProperty.Register("Positions", typeof(Vector3[]), typeof(JpMqModel3D), new PropertyMetadata(null,PositionsChanged));

        private static void PositionsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((JpMqModel3D)d).OnPositionsChanged();

        }


        public void OnPositionsChanged()
        {
            //修改线框
            LineBuilder lb = new LineBuilder();
            lb.Add(true, this.Positions);
            if (this.Line.IsAttached)
            {
                this.Line.Detach();
            }
            this.Line.Geometry = lb.ToLineGeometry3D();
            if (this.IsAttached)
                this.Line.Attach(this.renderHost);
            //this.Line.Attach(this.renderHost);
            //修改多边形面显示
            HelixToolkit.Wpf.SharpDX.MeshBuilder mb = new HelixToolkit.Wpf.SharpDX.MeshBuilder(false, false);
            mb.AddPolygon(this.Positions);
            //List<Vector3> listV = new List<Vector3>();
            //List<Vector2> listc = new List<Vector2>();
            //var points = this.Positions;
            //var count = points.Count();
            //for (int i = 0; i < points.Count(); i++)
            //{
            //    if (i < count / 2)
            //    {
            //        var point = points[count - i - 1];
            //        points[count - i - 1] = points[i];
            //        points[i] = point;
            //    }
            //    // points[i].Y = points[i].Y + 2f;
            //    listV.Add(new Vector3(0, 1, 0));
            //    listc.Add(new Vector2());
            //}
            //HelixToolkit.Wpf.SharpDX.MeshBuilder mb = new HelixToolkit.Wpf.SharpDX.MeshBuilder(true, true);
            //mb.AddTriangleFan(points, listV, listc);
            if (this.IsAttached)
                this.Face.Detach();
            this.Face.Geometry = mb.ToMeshGeometry3D();
            if (this.IsAttached)
                this.Face.Attach(this.renderHost);

            // ((JpMqModel3D)d).Modifier.Positions = ((JpMqModel3D)d).Positions;
        }

        public override bool MouseMoveHitTest(Ray rayWS, ref List<HitTestResult> hits)
        {
            
            if (this.Visibility == Visibility.Collapsed)
            {
                return false;
            }
            if (this.IsMouseMoveHitTestVisible == false)
            {
                return false;
            }
            var h = false;
            FaceHitTest(ref rayWS, hits, ref h);
            return h;
        }
        //测试鼠标是否位于面上
        private void FaceHitTest(ref Ray rayWS, List<HitTestResult> hits, ref bool h)
        {
            var mqBounds = this.Face.Bounds;
            if (rayWS.Intersects(ref mqBounds))  //判断是否与该组面框相交
            {
                var g = this.Face.Geometry as MeshGeometry3D;

                var result = new HitTestResult();
                result.Distance = double.MaxValue;

                if (g != null)
                {
                    var m = this.modelMatrix;
                    // put bounds to world space
                    var b = BoundingBox.FromPoints(this.Face.Geometry.Positions.Select(x => Vector3.TransformCoordinate(x, m)).ToArray());
                    //var b = this.Bounds;

                    // this all happens now in world space now:
                    if (rayWS.Intersects(ref b))
                    {
                        foreach (var t in g.Triangles)
                        {
                            float d;
                            var p0 = Vector3.TransformCoordinate(t.P0, m);
                            var p1 = Vector3.TransformCoordinate(t.P1, m);
                            var p2 = Vector3.TransformCoordinate(t.P2, m);
                            if (Collision.RayIntersectsTriangle(ref rayWS, ref p0, ref p1, ref p2, out d))
                            {
                                if (d < result.Distance)
                                {
                                    result.IsValid = true;
                                    result.ModelHit = this;
                                    // transform hit-info to world space now:
                                    result.PointHit = (rayWS.Position + (rayWS.Direction * d)).ToPoint3D();
                                    result.Distance = d;

                                    var n = Vector3.Cross(p1 - p0, p2 - p0);
                                    n.Normalize();
                                    // transform hit-info to world space now:
                                    result.NormalAtHit = n.ToVector3D();// Vector3.TransformNormal(n, m).ToVector3D();
                                }
                                h = true;
                            }
                        }
                    }
                }
                if (h)
                {
                    result.IsValid = h;
                    hits.Add(result);
                }
            }
        }

        static JpMqModel3D()
        {
            FocusableProperty.OverrideMetadata(typeof(JpMqModel3D), new UIPropertyMetadata(true));
        }
        public JpMqModel3D(JPViewport3DX vp,string geometryText,string mqTag)
            :base()
        {
            //this.CommandBindings.Add(new CommandBinding(JpViewport3DXCommands.ControlEnterkeypress, this.ControlEnterKeyPressHandler));
            //this.InputBindings.Add(
            //    new KeyBinding(JpViewport3DXCommands.ControlEnterkeypress, Key.Enter, ModifierKeys.None)
            //    );
            this.Line = new LineGeometryModel3D {  };
            this.Face = new MeshGeometryModel3D { };
            diffuseColor = PhongMaterials.ToColor(1, 0, 0, 0.1f);

            Line = new LineGeometryModel3D() { Thickness = 0.5, Color = new SharpDX.Color(0f, 0f, 1f, 0.05f), Opacity = 0.05f };

            Face = new MeshGeometryModel3D();
            
            Face.Tag = mqTag;// GetAreaModelTag(item); //areaModelItem.Key;
            Face.Material = new PhongMaterial
            {
                Name = "Red",
                AmbientColor = PhongMaterials.ToColor(1, 0.1, 0.1, 1.0),
                DiffuseColor = diffuseColor,//PhongMaterials.ToColor(1, 0, 0, 0.1f),
                SpecularColor = PhongMaterials.ToColor(0.0225, 0.0225, 0.0225, 1.0),
                EmissiveColor = PhongMaterials.ToColor(0.0, 0.0, 0.0, 1.0),
                SpecularShininess = 12.8f,
            }.Clone();
            
            Modifier = new InteractionHandle3D(this);

            //设置图形点集，触发line,face的geometry的变动

            this.Children.Add(Face);
            this.Tag = mqTag;
            Positions = Vector3ArrayConverter.FromString(geometryText);
            //显示边界框
            //DrawBoundBox();
        }

        private void ControlEnterKeyPressHandler(object sender, ExecutedRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        //private void ControlEnterKeyPressHandler(object sender, ExecutedRoutedEventArgs e)
        //{
        //    var cc = false;
        //}

        private void DrawBoundBox()
        {
            LineGeometryModel3D LineBoundBox = new LineGeometryModel3D();
            LineBoundBox.Geometry = LineBuilder.GenerateBoundingBox(this.Face.Bounds);
            LineBoundBox.Color = new Color(255, 0, 0, 1);
            this.Children.Add(LineBoundBox);
        }
        public override void OnMouseMoveOver3D(object sender, RoutedEventArgs e)
        {
            MouseMoveOver3DEventArgs ee= (MouseMoveOver3DEventArgs)e;
           
            if (ee.InOut)
            {
                this.Children.Add(Line);
            }
            else
            {
                this.Children.Remove(Line);
            }
        }

        public override void Attach(IRenderHost host)
        {
            base.Attach(host);
            this.renderHost = host;
        }

        public override void Render(RenderContext context)
        {
           
            foreach (var c in this.Children)
            {
                var model = c as ITransformable;
                if (model != null)
                {
                    // apply transform
                    model.Transform = this.Transform;
                    //model.PushMatrix(this.modelMatrix);
                    // render model
                    c.Render(context);
                    //model.PopMatrix();
                }
                else
                {
                    c.Render(context);
                }
            }
            if (IsModify)
                Modifier.Render(context);
        }
        /// <summary>
        /// The on children changed.
        /// </summary>
        protected virtual void OnChildrenChanged()
        {
            this.Children.Clear();
            this.Children.Add(Line);
            this.Children.Add(Face);
        }
    

        public override bool HitTest(Ray ray, ref List<HitTestResult> hits)
        {
            bool hit = false;
            FaceHitTest(ref ray, hits, ref hit);
            //foreach (var c in this.Children)
            //{
            //    var hc = c as IHitable;
            //    if (hc != null)
            //    {
            //        if (hc.HitTest(ray, ref hits))
            //        {
            //            hit = true;
            //        }
            //    }
            //}
            //使修改器能被击中
            if (IsModify)
            {
                foreach (var item in Modifier.Children)
                {
                    var hc = item as IHitable;
                    if (hc != null)
                    {
                        if (hc.HitTest(ray, ref hits))
                        {
                            hit = true;
                        }
                    }
                }
            }
            return hit;
        }
    }

}
