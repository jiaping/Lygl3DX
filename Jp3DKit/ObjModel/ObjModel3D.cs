using HelixToolkit.Wpf.SharpDX;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Buffer = SharpDX.Direct3D11.Buffer;

namespace Jp3DKit.ObjModel
{
    /// <summary>
    /// 从obj文件中获得的地形文件
    /// </summary>
    public class ObjModel3D : Model3D
    {
        public ModelData Model;
        private Dictionary<string, ShaderResourceView> diffusemapDict;
        #region Variables / Properties
        //public Texture Texture { get; private set; }
        //public TextureManager TextureManager { get; private set; }
        private InputLayout vertexLayout;
        private Buffer vertexBuffer;
        private Buffer indexBuffer;
        private Buffer instanceBuffer;
        private SharpDX.Direct3D11.EffectTechnique effectTechnique;

        private EffectTransformVariables effectTransforms;
        protected HelixToolkit.Wpf.SharpDX.MaterialGeometryModel3D.EffectMaterialVariables effectMaterial;

        protected ShaderResourceView texNormalMapView;

        private EffectVectorVariable vFrustum, vViewport, vLineParams;
        private SharpDX.Direct3D11.RasterizerState rasterState;


        //protected Geometry3D geometry;
        protected MxModelInfo[] instanceArray;


        protected PhongMaterial phongMaterial;
        protected EffectScalarVariable bHasInstances;

        protected bool isChanged = true;
        //protected bool hasInstances = false;
        protected bool hasShadowMap = false;

        ////顶点缓存
        //private VertexBufferBinding[] vertexBufferBindings = null;
        private VertexBufferBinding vertexBufferBinding;
        //protected SharpDX.Direct3D11.EffectPass effectPass;
       // private SharpDX.Direct3D11.DepthStencilState depthStencilState;
        #endregion

        #region Constructors
        public ObjModel3D(ModelData model)
        {
            this.Model = model;
            //ModelPath = AppDomain.CurrentDomain.BaseDirectory + @"3DModel\";
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="depthBias"></param>
        protected  void OnRasterStateChanged(int depthBias)
        {
            if (this.IsAttached)
            {
                Disposer.RemoveAndDispose(ref this.rasterState);
                /// --- set up rasterizer states
                var rasterStateDesc = new RasterizerStateDescription()
                {
                    FillMode = FillMode.Wireframe,
                    CullMode = CullMode.Back,
                    DepthBias =0,// depthBias,
                    DepthBiasClamp = -1000,
                    SlopeScaledDepthBias = +0,
                    IsDepthClipEnabled = true,
                    IsFrontCounterClockwise = true,

                    //IsMultisampleEnabled = true,
                    //IsAntialiasedLineEnabled = true,                    
                    //IsScissorEnabled = true,
                };
                this.rasterState = new SharpDX.Direct3D11.RasterizerState(this.Device, rasterStateDesc);
            }
        }

        public override void Attach(IRenderHost host)
        {
            try
            {
                /// --- attach
                //this.effectName = Techniques.RenderJpSimple;//host.RenderTechnique.ToString();
                //if (ModelFileName == null) throw new ArgumentNullException("ModelFileName", "模型文件名不能为空");
                //if (!LoadModel()) return;
                //this.Geometry = ConvertModelToMeshGeometry3D();
                base.Attach(host);
                AttachMaterial();
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
                this.vertexBuffer = Device.CreateBuffer(BindFlags.VertexBuffer, DefaultVertex.SizeInBytes, this.Model.ModelVertices.Select((x, ii) => new DefaultVertex()
                {
                    Position = new Vector4(x.Position, 1f),
                    Color = new Color4(0f, 0f, 1f, 1f),
                    //Color       = this.geometry.Colors != null ? this.geometry.Colors[ii] : new Color4(1f, 1f, 1f, 1f),
                    TexCoord = x.TextureCoordinate,
                    Normal = x.Normal,
                    Tangent = new Vector3(),
                    BiTangent = new Vector3(),
                }).ToArray());

                /// --- init index buffer

                this.indexBuffer = Device.CreateBuffer(BindFlags.IndexBuffer, sizeof(int), this.Model.ModelIndices.ToArray());

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

                /// --- init instances buffer            
                //this.hasInstances = this.Instances != null;
                this.bHasInstances = this.effect.GetVariableByName("bHasInstances").AsScalar();
                //if (this.hasInstances)
                //{
                //    this.instanceBuffer = Buffer.Create(this.Device, this.instanceArray.Select(x => x.ModelPos).ToArray(), new BufferDescription(Matrix.SizeInBytes * this.instanceArray.Length, ResourceUsage.Dynamic, BindFlags.VertexBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0));
                //}

                //this.texDiffuseMapVariable = effect.GetVariableByName("texDiffuseMap").AsShaderResource();
                //this.bHasDiffuseMapVariable = effect.GetVariableByName("bHasDiffuseMap").AsScalar();


                this.effectMaterial = new HelixToolkit.Wpf.SharpDX.MaterialGeometryModel3D.EffectMaterialVariables(this.effect);
                //this.effectPass = this.effectTechnique.GetPassByIndex(0);
                //if (!hasInstances)
                    vertexBufferBinding = new VertexBufferBinding(this.vertexBuffer, DefaultVertex.SizeInBytes, 0);
                //this.OnRasterStateChanged(this.DepthBias);
                    this.OnRasterStateChanged(0);
                /// --- flush
                this.Device.ImmediateContext.Flush();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Attach error :" + e.Message);
            }
        }

        private Geometry3D ConvertModelToMeshGeometry3D()
        {
            var g = new MeshGeometry3D();
            try
            {
                g.Positions = new HelixToolkit.Wpf.SharpDX.Core.Vector3Collection(this.Model.ModelVertices.Select(x => x.Position));
                g.Normals = new HelixToolkit.Wpf.SharpDX.Core.Vector3Collection(this.Model.ModelVertices.Select(x => x.Normal));
                g.TextureCoordinates = new HelixToolkit.Wpf.SharpDX.Core.Vector2Collection(this.Model.ModelVertices.Select(x => x.TextureCoordinate));
                g.Indices = new HelixToolkit.Wpf.SharpDX.Core.IntCollection(this.Model.ModelIndices);
            }
            catch (Exception e)
            {
                throw e;
            }
            return g;
        }

        public override void Detach()
        {
            Disposer.RemoveAndDispose(ref this.vertexBuffer);
            Disposer.RemoveAndDispose(ref this.indexBuffer);
            Disposer.RemoveAndDispose(ref this.instanceBuffer);
            Disposer.RemoveAndDispose(ref this.effectTransforms);
            Disposer.RemoveAndDispose(ref this.bHasInstances);
            Disposer.RemoveAndDispose(ref this.rasterState);
            //Disposer.RemoveAndDispose(ref this.depthStencilState);

            //this.effectName = null;
            this.phongMaterial = null;
            //this.Geometry = null;
            this.effectTechnique = null;
            this.vertexLayout = null;

            base.Detach();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Render(RenderContext renderContext)
        {
            /// --- check to render the model
            if (!this.IsRendering)
                return;

            //if (this.Geometry == null)
            //    return;

            if (this.Visibility != System.Windows.Visibility.Visible)
                return;

            //if (renderContext.IsShadowPass)
            //    if (!this.IsThrowingShadow)
            //        return;
            try
            {
                /// --- set constant paramerers             
                var worldMatrix = this.modelMatrix * Matrix.Identity;
                this.effectTransforms.mWorld.SetMatrix(ref worldMatrix);
                var viewMatrix = renderContext.ViewMatrix;
                var projectionMatrix = renderContext.ProjectrionMatrix;
                //this.effectTransforms.mView.SetMatrix(ref viewMatrix);
                //this.effectTransforms.mProjection.SetMatrix(ref projectionMatrix);
                //this.effectTransforms.vEyePos.Set(renderContext.Camera.Position.ToVector3());



                /// --- check shadowmaps
                this.hasShadowMap = this.renderHost.IsShadowMapEnabled;


                /// --- check instancing
                //this.hasInstances = this.Instances != null;
                //this.bHasInstances.Set(this.hasInstances);

                /// --- set context
                this.Device.ImmediateContext.InputAssembler.InputLayout = this.vertexLayout;
                this.Device.ImmediateContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
                this.Device.ImmediateContext.InputAssembler.SetIndexBuffer(this.indexBuffer, Format.R32_UInt, 0);
                //this.device.ImmediateContext.Rasterizer.State = rasterState;
                //this.device.ImmediateContext.OutputMerger.DepthStencilState = depthStencilState;

                //this.effectMaterial = new HelixToolkit.SharpDX.Wpf.MaterialGeometryModel3D.EffectMaterialVariables(this.effect);

                //var effectPass = this.technique.GetPassByIndex(0);
                //if (this.hasInstances)
                //{
                //    /// --- update instance buffer

                //    if (this.isChanged)
                //    {
                //        DataStream stream;
                //        Device.ImmediateContext.MapSubresource(this.instanceBuffer, MapMode.WriteDiscard, global::SharpDX.Direct3D11.MapFlags.None, out stream);
                //        stream.Position = 0;
                //        stream.WriteRange(this.instanceArray.Select(x => x.ModelPos).ToArray(), 0, this.instanceArray.Length);
                //        Device.ImmediateContext.UnmapSubresource(this.instanceBuffer, 0);
                //        stream.Dispose();
                //        vertexBufferBindings = new[] {
                //                                                                            new VertexBufferBinding(this.vertexBuffer, DefaultVertex.SizeInBytes, 0),
                //                                                                            new VertexBufferBinding(this.instanceBuffer, Matrix.SizeInBytes, 0),
                //                                                                        };
                //        this.isChanged = false;
                //    }
                //    /// --- INSTANCING: need to set 2 buffers            
                //    this.Device.ImmediateContext.InputAssembler.SetVertexBuffers(0, vertexBufferBindings);
                //    foreach (var item in this.Model.AttributeTable)
                //    {
                //        SetTechniqueAndMaterial(item);
                //        this.effectPass.Apply(Device.ImmediateContext);
                //        //this.OnRasterStateChanged(this.DepthBias);
                //        this.Device.ImmediateContext.DrawIndexedInstanced(item.FaceCount * 3, this.instanceArray.Length, item.FaceStart * 3, 0, 0);
                //    }
                //}
                //else
                {
                    /// --- bind buffer                
                    this.Device.ImmediateContext.InputAssembler.SetVertexBuffers(0, vertexBufferBinding);

                    foreach (var item in this.Model.AttributeTable)
                    {
                        SetTechniqueAndMaterial(item);
                        //this.OnRasterStateChanged(this.DepthBias);
                        this.effectTechnique.GetPassByIndex(0).Apply(Device.ImmediateContext);

                        this.Device.ImmediateContext.DrawIndexed(item.FaceCount * 3, item.FaceStart * 3, 0);
                    }

                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Attach error :" + e.Message);
            }
        }

        private void SetTechniqueAndMaterial(MaterialAttributeRange item)
        {
            MaterialDefinition material ;
            Model.ModelMaterials.TryGetValue(item.MaterialName,out material);
           
            this.effectMaterial.vMaterialAmbientVariable.Set(material.Ambient);
            this.effectMaterial.vMaterialDiffuseVariable.Set(material.Diffuse);
            this.effectMaterial.vMaterialSpecularVariable.Set(material.Specular);

            if (material.DiffuseMap== null)
            {
                this.effectMaterial.texDiffuseMapVariable.SetResource(null);
                this.effectMaterial.bHasDiffuseMapVariable.Set(false);
            }
            else
            {
                ShaderResourceView rv;
                diffusemapDict.TryGetValue(item.MaterialName,out rv);
                this.effectMaterial.texDiffuseMapVariable.SetResource(rv);
                this.effectMaterial.bHasDiffuseMapVariable.Set(true);
            }
            //SetEffectMterialVariable(material);
            //    /// --- render the geometry
            //for (int i = 0; i < this.technique.Description.PassCount; i++)
            //{

            //this.technique.GetPassByIndex(0).Apply(device.ImmediateContext);
        }

        private void SetEffectMterialVariable(MaterialDefinition material)
        {
            this.effectMaterial.vMaterialAmbientVariable.Set(material.Ambient);
            this.effectMaterial.vMaterialDiffuseVariable.Set(material.Diffuse);
            this.effectMaterial.vMaterialSpecularVariable.Set(material.Specular);

            if (material.DiffuseMap== null)
            {
                this.effectMaterial.texDiffuseMapVariable.SetResource(null);
                this.effectMaterial.bHasDiffuseMapVariable.Set(false);
            }
            else
            {
                //ShaderResourceView rv=diffusemapDict.TryGetValue()
                this.effectMaterial.texDiffuseMapVariable.SetResource(this.texNormalMapView);
                this.effectMaterial.bHasDiffuseMapVariable.Set(true);
            }

        }

        protected void AttachMaterial()
        {
            diffusemapDict = new Dictionary<string, ShaderResourceView>(this.Model.ModelMaterials.Count);
            foreach (var item in this.Model.ModelMaterials)
            {
                try
                {
                    if(item.Value.DiffuseMap!=null)
                    {
                        var aa=(item.Value.Material as PhongMaterial).DiffuseMap;
                       var cc=aa.ToByteArray();
                        diffusemapDict.Add(item.Key, ShaderResourceView.FromMemory(Device, aa.ToByteArray()));
                        //var dd=ShaderResourceView.FromFile(Device, aa.ToString().Substring(8));
                       // diffusemapDict.Add(item.Key, dd);
                    }
                }
                catch (Exception e)
                {
                    
                    throw;
                }
                
            }
            //this.phongMaterial = Material as PhongMaterial;
            //if (phongMaterial != null)
            //{
            //    this.effectMaterial = new EffectMaterialVariables(this.effect);

            //    /// --- has texture
            //    if (phongMaterial.DiffuseMap != null)
            //    {
            //        this.texDiffuseMapView = ShaderResourceView.FromMemory(device, phongMaterial.DiffuseMap.ToByteArray());
            //        this.effectMaterial.texDiffuseMapVariable.SetResource(this.texDiffuseMapView);
            //        this.effectMaterial.bHasDiffuseMapVariable.Set(true);
            //    }
            //    else
            //    {
            //        this.effectMaterial.bHasDiffuseMapVariable.Set(false);
            //    }

            //    // --- has bumpmap
            //    if (phongMaterial.NormalMap != null)
            //    {
            //        if (this.geometry.Tangents == null)
            //        {
            //            System.Windows.MessageBox.Show(string.Format("No Tangent-Space found. NormalMap will be omitted."), "Warning");
            //            phongMaterial.NormalMap = null;
            //        }
            //        else
            //        {
            //            this.texNormalMapView = ShaderResourceView.FromMemory(device, phongMaterial.NormalMap.ToByteArray());
            //            this.effectMaterial.texNormalMapVariable.SetResource(this.texNormalMapView);
            //            this.effectMaterial.bHasNormalMapVariable.Set(true);
            //        }
            //    }
            //    else
            //    {
            //        this.effectMaterial.bHasNormalMapVariable.Set(false);
            //    }

            //    // --- has displacement map
            //    if (phongMaterial.DisplacementMap != null)
            //    {
            //        this.texDisplacementMapView = ShaderResourceView.FromMemory(device, phongMaterial.DisplacementMap.ToByteArray());
            //        this.effectMaterial.texDisplacementMapVariable.SetResource(this.texDisplacementMapView);
            //        this.effectMaterial.bHasDisplacementMapVariable.Set(true);
            //    }
            //    else
            //    {
            //        this.effectMaterial.bHasDisplacementMapVariable.Set(false);
            //    }
            //}
        }
        /// <summary>
        /// 
        /// </summary>
        public override void Dispose()
        {
            this.Detach();
        }


        //public override bool HitTest(Ray rayWS, ref List<HitTestResult> hits)
        //{
        //    //if (this.Instances != null)
        //    //{
        //    //    bool hit = false;
        //    //    foreach (var modeinfo in Instances)
        //    //    {
        //    //        var b = this.Bounds;
        //    //        this.PushMatrix(modeinfo.ModelPos);
        //    //        //  this.Bounds = BoundingBox.FromPoints(this.Geometry.Positions.Select(x => Vector3.TransformCoordinate(x, this.modelMatrix)).ToArray());
        //    //        if (base.HitTest(rayWS, ref hits))
        //    //        {
        //    //            hit = true;
        //    //            var lastHit = hits[hits.Count - 1];
        //    //            lastHit.Tag = this.Tag.ToString() + ":" + modeinfo.ModelID;  //返回实体对象的ID
        //    //            //#if DEBUG
        //    //            //                        System.Diagnostics.Debug.WriteLine("hitTest ModelID:" + modeinfo.ModelID);
        //    //            //#endif
        //    //            hits[hits.Count - 1] = lastHit;
        //    //        }
        //    //        this.PopMatrix();
        //    //        this.Bounds = b;
        //    //    }
        //    //    return hit;
        //    //}
        //    //else
        //    {
        //        //return HitTestGetTriangle(rayWS, ref hits);
        //        return base.HitTest(rayWS, ref hits);
        //    }
        //}
        /// <summary>
        /// 用于测试显示命中三角形的法线
        /// </summary>
        /// <param name="rayWS"></param>
        /// <param name="hits"></param>
        /// <returns></returns>
        //public bool HitTestGetTriangle(Ray rayWS, ref List<HitTestResult> hits)
        //{
        //    var g = this.Geometry as MeshGeometry3D;
        //    var h = false;
        //    var result = new HitTestResult();
        //    result.Distance = double.MaxValue;

        //    if (g != null)
        //    {
        //        var m = this.modelMatrix;
        //        // put bounds to world space
        //        var b = BoundingBox.FromPoints(this.Geometry.Positions.Select(x => Vector3.TransformCoordinate(x, m)).ToArray());
        //        //var b = this.Bounds;

        //        // this all happens now in world space now:
        //        if (rayWS.Intersects(ref b))
        //        {
        //            foreach (var t in g.Triangles)
        //            {
        //                float d;
        //                var p0 = Vector3.TransformCoordinate(t.P0, m);
        //                var p1 = Vector3.TransformCoordinate(t.P1, m);
        //                var p2 = Vector3.TransformCoordinate(t.P2, m);
        //                if (Collision.RayIntersectsTriangle(ref rayWS, ref p0, ref p1, ref p2, out d))
        //                {
        //                    if (d < result.Distance) // If d is NaN, the condition is false.
        //                    {
        //                        result.IsValid = true;

        //                        MeshGeometryModel3D bb = new MeshGeometryModel3D();
        //                        MeshGeometry3D cc = new MeshGeometry3D();
        //                        cc.Positions = new HelixToolkit.Wpf.SharpDX.Core.Vector3Collection(new Vector3[3] { p0, p1, p2 });
        //                        cc.Indices = new HelixToolkit.Wpf.SharpDX.Core.IntCollection(new int[3] { 0, 1, 2 });
        //                        cc.Normals = new HelixToolkit.Wpf.SharpDX.Core.Vector3Collection(new Vector3[3] { result.NormalAtHit.ToVector3(), result.NormalAtHit.ToVector3(), result.NormalAtHit.ToVector3() });
        //                        // transform hit-info to world space now:
        //                        result.PointHit = (rayWS.Position + (rayWS.Direction * d)).ToPoint3D();
        //                        result.Distance = d;

        //                        var n = Vector3.Cross(p1 - p0, p2 - p0);
        //                        n.Normalize();

        //                        bb.Geometry = cc;
        //                        bb.Material = PhongMaterials.Red;
        //                        //    new PhongMaterial
        //                        //{
        //                        //    Name = "Red",
        //                        //    AmbientColor = PhongMaterials.ToColor(1, 0.1, 0.1, 1.0),
        //                        //    DiffuseColor = PhongMaterials.ToColor(1, 0, 0, 0.1f),
        //                        //    SpecularColor = PhongMaterials.ToColor(1, 0.0225, 0.0225, 1.0),
        //                        //    EmissiveColor = PhongMaterials.ToColor(0.0, 0.0, 0.0, 1.0),
        //                        //    SpecularShininess = 12.8f,
        //                        //}.Clone();
        //                        result.ModelHit = bb;

        //                        // transform hit-info to world space now:
        //                        result.NormalAtHit = n.ToVector3D();// Vector3.TransformNormal(n, m).ToVector3D();
        //                        h = true;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    if (h)
        //    {
        //        hits.Add(result);
        //    }
        //    return h;
        //}

        /* *             
            // If this vertex doesn't already exist in the Vertices list, create a new entry.
            // Add the index of the vertex to the Indices list.
            bool bFoundInList = false;
            uint index = 0;

            // Since it's very slow to check every element in the vertex list, a hashtable stores
            // vertex indices according to the vertex position's index as reported by the OBJ file
            if((uint)m_VertexCache.GetSize() > hash)
            {
                CacheEntry pEntry = m_VertexCache.GetAt(hash);
                while(pEntry != null)
                {
                    VERTEX pCacheVertex = m_Vertices.GetData() + pEntry.index;

                    // If this vertex is identical to the vertex already in the list, simply
                    // point the index buffer to the existing vertex
        //C++ TO C# CONVERTER TODO TASK: The memory management function 'memcmp' has no equivalent in C#:
        //C++ TO C# CONVERTER TODO TASK: The memory management function 'memcmp' has no equivalent in C#:
                    if(0 == memcmp(pVertex, pCacheVertex, sizeof(VERTEX)))
                    {
                        bFoundInList = true;
                        index = pEntry.index;
                        break;
                    }

                    pEntry = pEntry.pNext;
                }
            }

            // Vertex was not found in the list. Create a new entry, both within the Vertices list
            // and also within the hashtable cache
            if(!bFoundInList)
            {
                // Add to the Vertices list
                index = m_Vertices.GetSize();
                m_Vertices.Add(*pVertex);

                // Add this to the hashtable
                CacheEntry pNewEntry = new CacheEntry;
                if(pNewEntry == null)
                    return (uint)-1;

                pNewEntry.index = index;
                pNewEntry.pNext = null;

                // Grow the cache if needed
                while((uint)m_VertexCache.GetSize() <= hash)
                {
                    m_VertexCache.Add(null);
                }

                // Add to the end of the linked list
                CacheEntry pCurEntry = m_VertexCache.GetAt(hash);
                if(pCurEntry == null)
                {
                    // This is the head element
                    m_VertexCache.SetAt(hash, pNewEntry);
                }
                else
                {
                    // Find the tail
                    while(pCurEntry.pNext != null)
                    {
                        pCurEntry = pCurEntry.pNext;
                    }

                    pCurEntry.pNext = pNewEntry;
                }
            }

            return index;
         **/

        /** 
                private bool LoadTexture(Device device, string textureFileName)
                {
                    textureFileName = SystemConfiguration.DataFilePath + textureFileName;

                    // Create the texture object.
                    Texture = new Texture();

                    // Initialize the texture object.
                    Texture.Initialize(device, textureFileName);


                    return true;
                }
                public void Shutdown()
                {
                    // Release the model texture.
                    ReleaseTexture();

                    // Release the vertex and index buffers.
                    ShutdownBuffers();

                    // Release the model data.
                    ReleaseModel();
                }

                private void ReleaseModel()
                {
                    ModelObject = null;
                }

                private void ReleaseTexture()
                {
                    // Release the texture object.
                    if (Texture != null)
                    {
                        Texture.Shutdown();
                        Texture = null;
                    }
                }
                public void Render(DeviceContext deviceContext)
                {
                    // Put the vertex and index buffers on the graphics pipeline to prepare for drawings.
                    RenderBuffers(deviceContext);
                }

                private bool InitializeBuffers(Device device)
                {
                    try
                    {
                        // Create the vertex array.
                        var vertices = new ModelShader.Vertex[VertexCount];
                        // Create the index array.
                        var indices = new int[IndexCount];

                        for (var i = 0; i < VertexCount; i++)
                        {
                            vertices[i] = new ModelShader.Vertex()
                            {
                                position = new Vector3(ModelObject[i].x, ModelObject[i].y, ModelObject[i].z),
                                texture = new Vector2(ModelObject[i].tu, ModelObject[i].tv),
                                normal = new Vector3(ModelObject[i].nx, ModelObject[i].ny, ModelObject[i].nz)
                            };

                            indices[i] = i;
                        }

                        // Create the vertex buffer.
                        VertexBuffer = Buffer.Create(device, BindFlags.VertexBuffer, vertices);

                        // Create the index buffer.
                        IndexBuffer = Buffer.Create(device, BindFlags.IndexBuffer, indices);

                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }

                private void ShutdownBuffers()
                {
                    // Return the index buffer.
                    if (IndexBuffer != null)
                    {
                        IndexBuffer.Dispose();
                        IndexBuffer = null;
                    }

                    // Release the vertex buffer.
                    if (VertexBuffer != null)
                    {
                        VertexBuffer.Dispose();
                        VertexBuffer = null;
                    }
                }

                private void RenderBuffers(DeviceContext deviceContext)
                {
                    // Set the vertex buffer to active in the input assembler so it can be rendered.
                    deviceContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(VertexBuffer, Utilities.SizeOf<ModelShader.Vertex>(), 0));
                    // Set the index buffer to active in the input assembler so it can be rendered.
                    deviceContext.InputAssembler.SetIndexBuffer(IndexBuffer, Format.R32_UInt, 0);
                    // Set the type of the primitive that should be rendered from this vertex buffer, in this case triangles.
                    deviceContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
                }
         * **/
        #endregion

        #region Override Methods
        public object Clone()
        {
            return MemberwiseClone();
        }
        #endregion


    }
}
