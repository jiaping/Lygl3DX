using HelixToolkit.Wpf.SharpDX;
using SharpDX;
using SharpDX.Direct3D11;
using System.Linq;
using System.Windows;
using HelixToolkit.Wpf.SharpDX.Extensions;
using SharpDX.Direct3D;
using SharpDX.DXGI;

namespace Jp3DKit.DecorateModel
{
    public  class DecorateModel3D:Model3D
    {
        // members to dispose          
        private Buffer vertexBuffer;
        private Buffer indexBuffer;
        private InputLayout vertexLayout;
        private EffectTechnique effectTechnique;

        private RasterizerState rasterState;
        private DepthStencilState depthStencilState;

        public EffectTransformVariables effectTransforms;
        public HelixToolkit.Wpf.SharpDX.MaterialGeometryModel3D.EffectMaterialVariables effectMatrials;

        public PhongMaterial Material;

        public MeshGeometry3D Geometry
        {
            get { return (MeshGeometry3D)GetValue(GeometryProperty); }
            set { SetValue(GeometryProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Geometry.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GeometryProperty =
            DependencyProperty.Register("Geometry", typeof(MeshGeometry3D), typeof(DecorateModel3D), new UIPropertyMetadata(null, GeometryChanged));

        private static void GeometryChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = ((DecorateModel3D)d);
            //if (obj.IsAttached)
            //    obj.bHasCubeMap.Set((bool)e.NewValue);
        }
        /// <summary>
        /// 标识对象是否为active，如果为否，模型将不被渲染
        /// 默认为true;
        /// </summary>
        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register("IsActive", typeof(bool), typeof(DecorateModel3D), new UIPropertyMetadata(true, IsActiveChanged));

        /// <summary>
        /// 标识对象是否为active，如果为否，模型将不被渲染
        /// 默认为true;
        /// </summary>
        public bool IsActive
        {
            get { return (bool)this.GetValue(IsActiveProperty); }
            set { this.SetValue(IsActiveProperty, value); }
        }

        private static void IsActiveChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //var obj = ((DecorateModel3D)d);
            //if (obj.IsAttached)
            //    obj.bHasCubeMap.Set((bool)e.NewValue);
        }

        public override void Attach(IRenderHost host)
        {
            /// --- attach
            this.renderTechnique = host.RenderTechnique;
            base.Attach(host);

            /// --- get variables               
            this.vertexLayout = EffectsManager.Instance.GetLayout(this.renderTechnique);
            this.effectTechnique = effect.GetTechniqueByName(this.renderTechnique.Name);
            this.effectTransforms = new EffectTransformVariables(this.effect);
            this.effectMatrials = new MaterialGeometryModel3D.EffectMaterialVariables(this.effect);

            // --- scale texcoords
            // var texScale = TextureCoodScale;

            // -- set geometry if given
            if (this.Geometry!= null)
            {
                //throw new HelixToolkitException("Geometry not found!");                
            
                    /// --- init vertex buffer
                    this.vertexBuffer = Device.CreateBuffer(BindFlags.VertexBuffer, DefaultVertex.SizeInBytes, this.CreateDefaultVertexArray());

                    /// --- init index buffer
                    this.indexBuffer = Device.CreateBuffer(BindFlags.IndexBuffer, sizeof(int), this.Geometry.Indices.GetInternalArray());
            }
            else
            {
                throw new System.Exception("Geometry must not be null");
            }
                /// --- set up rasterizer states
                var rasterStateDesc = new RasterizerStateDescription()
                {
                    FillMode = FillMode.Solid,
                    CullMode = CullMode.Back,
                   // DepthBias = depthBias,
                    DepthBiasClamp = -1000,
                    SlopeScaledDepthBias = +0,
                    IsDepthClipEnabled = true,
                    IsFrontCounterClockwise = true,

                    //IsMultisampleEnabled = true,
                    //IsAntialiasedLineEnabled = true,                    
                    //IsScissorEnabled = true,
                };
                this.rasterState = new RasterizerState(this.Device, rasterStateDesc);

                /// --- set up depth stencil state
                //var depthStencilDesc = new DepthStencilStateDescription()
                //{
                //    DepthComparison = Comparison.LessEqual,
                //    DepthWriteMask = global::SharpDX.Direct3D11.DepthWriteMask.All,
                //    IsDepthEnabled = true,
                //};
                //this.depthStencilState = new DepthStencilState(this.Device, depthStencilDesc);

            /// --setup materials
               
            /// --- flush
            this.Device.ImmediateContext.Flush();
        }

        /// <summary>
        /// Creates a <see cref="T:DefaultVertex[]"/>.
        /// </summary>
        private DefaultVertex[] CreateDefaultVertexArray()
        {
            var geometry = (MeshGeometry3D)this.Geometry;
            var colors = geometry.Colors != null ? geometry.Colors.GetInternalArray() : null;
            var textureCoordinates = geometry.TextureCoordinates != null ? geometry.TextureCoordinates.GetInternalArray() : null;
            var texScale = 1;// this.TextureCoodScale;
            var normals = geometry.Normals != null ? geometry.Normals.GetInternalArray() : null;
            var tangents = geometry.Tangents != null ? geometry.Tangents.GetInternalArray() : null;
            var bitangents = geometry.BiTangents != null ? geometry.BiTangents.GetInternalArray() : null;
            var positions = geometry.Positions.GetInternalArray();
            var vertexCount = geometry.Positions.Count;
            var result = new DefaultVertex[vertexCount];

            for (var i = 0; i < vertexCount; i++)
            {
                result[i] = new DefaultVertex
                {
                    Position = new Vector4(positions[i], 1f),
                    Color = colors != null ? colors[i] : Color4.White,
                    TexCoord = textureCoordinates != null ? texScale * textureCoordinates[i] : Vector2.Zero,
                    Normal = normals != null ? normals[i] : Vector3.Zero,
                    Tangent = tangents != null ? tangents[i] : Vector3.Zero,
                    BiTangent = bitangents != null ? bitangents[i] : Vector3.Zero,
                };
            }

            return result;
        }
        public override void Detach()
        {
            if (!this.IsAttached)
                return;

            this.effectTechnique = null;
            this.effectTechnique = null;
            this.vertexLayout = null;
            this.Geometry = null;

            Disposer.RemoveAndDispose(ref this.vertexBuffer);
            Disposer.RemoveAndDispose(ref this.indexBuffer);
            Disposer.RemoveAndDispose(ref this.rasterState);
            //Disposer.RemoveAndDispose(ref this.depthStencilState);
            Disposer.RemoveAndDispose(ref this.effectTransforms);

            base.Detach();
        }

        public override void Render(RenderContext renderContext)
        {
            if (!this.IsActive) return;
            if (!this.IsRendering) return;
            if (this.Geometry == null) return;
            /// --- set context
            this.Device.ImmediateContext.InputAssembler.InputLayout = this.vertexLayout;
            this.Device.ImmediateContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
            this.Device.ImmediateContext.InputAssembler.SetIndexBuffer(this.indexBuffer, Format.R32_UInt, 0);
            this.Device.ImmediateContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(this.vertexBuffer, DefaultVertex.SizeInBytes, 0));

            this.Device.ImmediateContext.Rasterizer.State = rasterState;
            //this.Device.ImmediateContext.OutputMerger.DepthStencilState = depthStencilState;
            if (Material != null)
            {
                this.effectMatrials.vMaterialAmbientVariable.Set(Material.AmbientColor);
                this.effectMatrials.vMaterialDiffuseVariable.Set(Material.DiffuseColor);
                this.effectMatrials.vMaterialSpecularVariable.Set(Material.SpecularColor);
                this.effectMatrials.bHasDiffuseMapVariable.Set(false);
                this.effectMatrials.bHasDisplacementMapVariable.Set(false);
                this.effectMatrials.bHasNormalMapVariable.Set(false);
                this.effectMatrials.bHasShadowMapVariable.Set(false);
            }
            /// --- set constant paramerers             
            var worldMatrix = this.modelMatrix * Matrix.Identity;
            this.effectTransforms.mWorld.SetMatrix(ref worldMatrix);

            /// --- render the geometry
            this.effectTechnique.GetPassByIndex(0).Apply(Device.ImmediateContext);
            this.Device.ImmediateContext.DrawIndexed(this.Geometry.Indices.Count, 0, 0);
        }
    }
}
