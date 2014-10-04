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


namespace Jp3DKit
{

    /// <summary>
    /// loader model data from .obj
    /// </summary>
    public class JpMqModel3D : JpCompositeModel3D
    {
        private LineGeometryModel3D Line;
        private MeshGeometryModel3D Face;

        private Color4 diffuseColor;

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
            return h;
        }



        
        public JpMqModel3D(JPViewport3DX vp,string geometryText,string mqTag)
        {
            this.Line = new LineGeometryModel3D {  };
            this.Face = new MeshGeometryModel3D { };
            diffuseColor = PhongMaterials.ToColor(1, 0, 0, 0.1f);

            #region 画线框型
            LineGeometry3D lg = new LineGeometry3D();
            lg.Positions = Vector3ArrayConverter.FromString(geometryText);
            int[] indices = new int[lg.Positions.Length * 2];
            for (int i = 0; i < lg.Positions.Length * 2; i += 2)
            {
                indices[i] = i / 2;
                indices[i + 1] = i / 2 + 1;
            }
            indices[indices.Length - 1] = 0;
            lg.Indices = indices;
            Line = new LineGeometryModel3D() { Thickness = 0.5, Color = new SharpDX.Color(0f, 0f, 1f, 0.05f), Opacity = 0.05f };
            Line.Geometry = lg;
            Line.Tag = mqTag;// GetAreaModelTag(item);
            //vp.Items.Add(Line);
            //if (forceAttach) vp.Attach(Line);
            #endregion
            #region 画面型
            List<Vector3> listV = new List<Vector3>();
            List<Vector2> listc = new List<Vector2>();
            var points = Vector3ArrayConverter.FromString(geometryText);
            var count = points.Count();
            for (int i = 0; i < points.Count(); i++)
            {
                if (i < count / 2)
                {
                    var point = points[count - i - 1];
                    points[count - i - 1] = points[i];
                    points[i] = point;
                }
                // points[i].Y = points[i].Y + 2f;
                listV.Add(new Vector3(0, 1, 0));
                listc.Add(new Vector2());
            }
            HelixToolkit.Wpf.SharpDX.MeshBuilder mb = new HelixToolkit.Wpf.SharpDX.MeshBuilder(true, true);
            mb.AddTriangleFan(points, listV, listc);
            // MeshGeometryModel3D model = new MeshGeometryModel3D();
             Face = new MeshGeometryModel3D();

             Face.Geometry = mb.ToMeshGeometry3D();
             Face.Tag = mqTag;// GetAreaModelTag(item); //areaModelItem.Key;
             Face.Material = new PhongMaterial
            {
                Name = "Red",
                AmbientColor = PhongMaterials.ToColor(0.1, 0.1, 0.1, 1.0),
                DiffuseColor = diffuseColor,//PhongMaterials.ToColor(1, 0, 0, 0.1f),
                SpecularColor = PhongMaterials.ToColor(0.0225, 0.0225, 0.0225, 1.0),
                EmissiveColor = PhongMaterials.ToColor(0.0, 0.0, 0.0, 1.0),
                SpecularShininess = 12.8f,
            }.Clone();
            // vp.Items.Add(Face);
            //if (forceAttach) vp.Attach(model);
            #endregion

             //this.Children.Add(Line);
             this.Children.Add(Face);
             this.Tag = mqTag;
             DrawBoundBox();

        }
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
            foreach (var c in this.Children)
            {
                var hc = c as IHitable;
                if (hc != null)
                {
                    if (hc.HitTest(ray, ref hits))
                    {
                        hit = true;
                    }
                }
            }
            return hit;
        }
    }

}
