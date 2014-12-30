using HelixToolkit.Wpf.SharpDX;
using Jp3DKit.ObjModel;
using SharpDX;

namespace Jp3DKit.TerrainModels
{
    /// <summary>
    /// 所有地形场景对象管理器
    /// </summary>
    public class TerrainSceneModel : GeometryModel3D, ITerrain
    {
        public ObjTerrainModel3D Terrain { get; set; }

        public TerrainSceneModel(JPViewport3DX vp)
        {
            JpObjReader reader = new JpObjReader();
            var dm = reader.Read(System.AppDomain.CurrentDomain.BaseDirectory + @"3DModel\allmq2.obj");
            Terrain = new ObjTerrainModel3D(vp, dm);
        }
        public override void Attach(IRenderHost host)
        {
            base.Attach(host);
            Terrain.Attach(host);
        }
        public override void Render(RenderContext context)
        {
            base.Render(context);
            // push matrix                    
            Terrain.PushMatrix(this.modelMatrix);
            // render model
            Terrain.Render(context);
            // pop matrix                   
            Terrain.PopMatrix();
        }

        #region ITerrain 成员

        public bool GetTerrainPoint(Vector2 point,out System.Windows.Media.Media3D.Point3D point3D)
        {
            return Terrain.GetTerrainPoint(point,out point3D);
        }
        #endregion
    }
}
