
using Jp3DKit.ObjModel;
using SharpDX;
using HelixToolkit.Wpf.SharpDX;
using System.Collections.Generic;
using System.Linq;
namespace Jp3DKit.TerrainModels
{
    public class ObjTerrainModel3D:ObjModel3D,ITerrain
    {
        private JPViewport3DX viewport;
        public ObjTerrainModel3D(JPViewport3DX vp, ModelData model)
            :base(model)
        {
            viewport = vp;
        }

        public bool GetTerrainPoint(Vector2 point,out System.Windows.Media.Media3D.Point3D point3D)
        {
            point3D = new System.Windows.Media.Media3D.Point3D();
            var ray = ViewportExtensions.UnProject(this.viewport, point);
            var hits = new List<HitTestResult>();
            TerrainPointTest(ray, ref hits);
            if (hits.Count > 0)
            {
                var bb = hits.OrderByDescending(k => k.Distance).ToList();
                point3D= bb.First().PointHit;
                return true;
            }
            return false;
        }
        public IEnumerable<Geometry3D.Triangle> Triangles
        {
            get
            {
                for (int i = 0; i < this.Model.ModelIndices.Count; i += 3)
                {
                    yield return new HelixToolkit.Wpf.SharpDX.Geometry3D.Triangle() { P0 = Model.ModelVertices[Model.ModelIndices[i]].Position, P1 = Model.ModelVertices[Model.ModelIndices[i + 1]].Position, P2 = Model.ModelVertices[Model.ModelIndices[i+2]].Position, };
                }
            }
        }

        public  bool TerrainPointTest(Ray rayWS, ref List<HitTestResult> hits)
        {
            //var g = this.Geometry as MeshGeometry3D;
            var g = this.Triangles;
           var h = false;
           var result = new HitTestResult();
           result.Distance = double.MaxValue;

           if (g != null)
           {
               var m = this.modelMatrix;
               // put bounds to world space
               var b = BoundingBox.FromPoints(this.Model.ModelVertices.Select(x => Vector3.TransformCoordinate(x.Position, m)).ToArray());
               //var b = this.Bounds;

               // this all happens now in world space now:
               if (rayWS.Intersects(ref b))
               {
                  // int ii = 0;
                   foreach (var t in g)
                   {
                       //System.Console.WriteLine(++ii);
                       float d;
                       var p0 = Vector3.TransformCoordinate(t.P0, m);
                       var p1 = Vector3.TransformCoordinate(t.P1, m);
                       var p2 = Vector3.TransformCoordinate(t.P2, m);
                       if (Collision.RayIntersectsTriangle(ref rayWS, ref p0, ref p1, ref p2, out d))
                       {
                           if (d < result.Distance) // If d is NaN, the condition is false.
                           {
                               result.IsValid = true;
                               result.ModelHit = null; //
                               // transform hit-info to world space now:
                               result.PointHit = (rayWS.Position + (rayWS.Direction * d)).ToPoint3D();
                               result.Distance = d;

                               var n = Vector3.Cross(p1 - p0, p2 - p0);
                               n.Normalize();
                               // transform hit-info to world space now:
                               result.NormalAtHit = n.ToVector3D();// Vector3.TransformNormal(n, m).ToVector3D();
                               h = true;
                           }
                       }
                   }
               }
           }
           if (h)
           {
               hits.Add(result);
           }
            return h;
        }
    }

    public interface ITerrain
    {
        bool GetTerrainPoint( Vector2 point ,out System.Windows.Media.Media3D.Point3D point3D);
    }
}
