using HelixToolkit.Wpf.SharpDX;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace Jp3DKit
{
    public class SkyBoxModel : GeometryModel3D
    {
        private InputLayout vertexLayout;
        private Buffer vertexBuffer;
        private Buffer indexBuffer;
        private EffectTechnique effectTechnique;
        private EffectTransformVariables effectTransforms;

        public override void Attach(IRenderHost host)
        {
            try
            {
                
                MeshBuilder mb = new MeshBuilder(true, true, false);
                mb.AddSphere(new Vector3(),100);
                this.Geometry = mb.ToMeshGeometry3D();
                base.Attach(host);
                /// --- attach
                //this.effectName = Techniques.RenderJpSimple;//host.RenderTechnique.ToString();
                //if (ModelFileName == null) throw new ArgumentNullException("ModelFileName", "模型文件名不能为空");
                //if (!LoadModel()) return;
                //this.Geometry = ConvertModelToMeshGeometry3D();
                //base.Attach(host);
                //LoadMaterialsTexture();

                // --- get variables
                this.vertexLayout = EffectsManager.Instance.GetLayout(host.RenderTechnique);
                this.effectTechnique = effect.GetTechniqueByName(host.RenderTechnique.Name);
                //this.technique = effect.GetTechniqueByName(this.renderTechnique.Name);
                //this.vertexLayout = host.Effects.GetLayout(this.effectName);
                //this.technique = effect.GetTechniqueByName(this.effectName);
                //this.geometry = this.Geometry as MeshGeometry3D;

                /// --- constant buffer for transformation
                this.effectTransforms = new EffectTransformVariables(this.effect);

                /// --- material 
                //this.AttachMaterial();

                /// --- init vertex buffer
                MeshGeometry3D mg=(MeshGeometry3D)this.Geometry;
                this.vertexBuffer = Device.CreateBuffer(BindFlags.VertexBuffer, DefaultVertex.SizeInBytes, mg.Positions.Select((x, ii) => new DefaultVertex()
                {
                    Position = new Vector4(x, 1f),
                    Color = new Color4(0f, 1f, 0f, 1f),
                    //Color       = this.geometry.Colors != null ? this.geometry.Colors[ii] : new Color4(1f, 1f, 1f, 1f),
                    TexCoord = mg.TextureCoordinates[ii],
                    Normal = mg.Normals[ii],
                    Tangent = new Vector3(),
                    BiTangent = new Vector3(),
                }).ToArray());

                /// --- init index buffer

                this.indexBuffer = Device.CreateBuffer(BindFlags.IndexBuffer, sizeof(int), mg.Indices.ToArray());

                //var rasterStateDesc = new RasterizerStateDescription()
                //{
                //    FillMode = FillMode.Solid,
                //    CullMode = CullMode.Back,
                //    IsMultisampleEnabled = true,
                //    IsAntialiasedLineEnabled = true,
                //    IsFrontCounterClockwise = false,
                //};
                //this.rasterState = new SharpDX.Direct3D11.RasterizerState(this.device, rasterStateDesc);
                //var depthStencilDesc = new DepthStencilStateDescription()
                //{
                //    DepthComparison = Comparison.LessEqual,
                //    DepthWriteMask = global::SharpDX.Direct3D11.DepthWriteMask.All,
                //    IsDepthEnabled = true,
                //};
                //this.depthStencilState = new SharpDX.Direct3D11.DepthStencilState(this.device, depthStencilDesc);

               

                //this.texDiffuseMapVariable = effect.GetVariableByName("texDiffuseMap").AsShaderResource();
                //this.bHasDiffuseMapVariable = effect.GetVariableByName("bHasDiffuseMap").AsScalar();


                //this.effectMaterial = new HelixToolkit.Wpf.SharpDX.MaterialGeometryModel3D.EffectMaterialVariables(this.effect);
                //this.effectPass = this.effectTechnique.GetPassByIndex(0);
                //this.effectPass = this.technique.GetPassByIndex(0);
                    //vertexBufferBinding = new VertexBufferBinding(this.vertexBuffer, DefaultVertex.SizeInBytes, 0);
                this.OnRasterStateChanged(this.DepthBias);
                /// --- flush
                this.Device.ImmediateContext.Flush();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Attach error :" + e.Message);
            }
        }

        public override void Render(RenderContext renderContext)
        {
            /// --- do not render, if not enabled
            if (!this.IsRendering)
                return;

            if (this.Geometry == null)
                return;

            if (this.Visibility != System.Windows.Visibility.Visible)
                return;

            if (renderHost.RenderTechnique == Techniques.RenderDeferred || renderHost.RenderTechnique == Techniques.RenderGBuffer)
                return;

            if (renderContext.IsShadowPass)
                if (!this.IsThrowingShadow)
                    return;

            /// --- since these values are changed only per window resize, we set them only once here
            //if (this.isResized || renderContext.Camera != this.lastCamera)
            {
                //this.isResized = false;
                //this.lastCamera = renderContext.Camera;

                //if (renderContext.Camera is ProjectionCamera)
                //{
                //    var c = renderContext.Camera as ProjectionCamera;
                //    // viewport: W,H,0,0   
                //    var viewport = new Vector4((float)renderContext.Canvas.ActualWidth, (float)renderContext.Canvas.ActualHeight, 0, 0);
                //    var ar = viewport.X / viewport.Y;
                //    var fov = 100.0; // this is a fake value, since the line shader does not use it!
                //    var zn = c.NearPlaneDistance > 0 ? c.NearPlaneDistance : 0.1;
                //    var zf = c.FarPlaneDistance + 0.0;
                //    // frustum: FOV,AR,N,F
                //    var frustum = new Vector4((float)fov, (float)ar, (float)zn, (float)zf);
                //    this.vViewport.Set(ref viewport);
                //    this.vFrustum.Set(ref frustum);
                //}
            }
            /// --- set transform paramerers             
            var worldMatrix = this.modelMatrix * Matrix.Identity;
            this.effectTransforms.mWorld.SetMatrix(ref worldMatrix);
            var viewMatrix = renderContext.ViewMatrix;
            var projectionMatrix = renderContext.ProjectrionMatrix;

            /// --- set context
            this.Device.ImmediateContext.InputAssembler.InputLayout = this.vertexLayout;
            this.Device.ImmediateContext.InputAssembler.SetIndexBuffer(this.indexBuffer, Format.R32_UInt, 0);
            this.Device.ImmediateContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;


            /// --- set rasterstate            
            this.Device.ImmediateContext.Rasterizer.State = this.rasterState;

                /// --- bind buffer                
            this.Device.ImmediateContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(this.vertexBuffer, DefaultVertex.SizeInBytes, 0));

                /// --- render the geometry
                this.effectTechnique.GetPassByIndex(0).Apply(this.Device.ImmediateContext);
                this.Device.ImmediateContext.DrawIndexed(this.Geometry.Indices.Count, 0, 0);
        }
    }
}
