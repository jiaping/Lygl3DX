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
    public class JpSceneModel3D : MeshGeometryModel3D
    {
        #region property
        public string ModelFileName
        {
            get { return (string)GetValue(ModelFileNameProperty); }
            set { SetValue(ModelFileNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ModelFileName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModelFileNameProperty =
            DependencyProperty.Register("ModelFileName", typeof(string), typeof(JpSceneModel3D), new PropertyMetadata(null));

        public string ModelPath
        {
            get { return (string)GetValue(ModelPathProperty); }
            set { SetValue(ModelPathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ModelPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModelPathProperty =
            DependencyProperty.Register("ModelPath", typeof(string), typeof(JpSceneModel3D), new PropertyMetadata(null));

        public IEnumerable<MxModelInfo> Instances
        {
            get { return (IEnumerable<MxModelInfo>)this.GetValue(InstancesProperty); }
            set { this.SetValue(InstancesProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty InstancesProperty =
            DependencyProperty.Register("Instances", typeof(IEnumerable<MxModelInfo>), typeof(JpSceneModel3D), new UIPropertyMetadata(null, InstancesChanged));

      



        public float Alpha
        {
            get { return (float)GetValue(AlphaProperty); }
            set { SetValue(AlphaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Alpha.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AlphaProperty =
            DependencyProperty.Register("Alpha", typeof(float), typeof(JpSceneModel3D), new PropertyMetadata(1f));


        #endregion
        #region Structures
        //[StructLayout(LayoutKind.Sequential)]
        //public struct VertexPositionNormalTexture
        //{
        //    public Vector3 Position;
        //    public Vector2 TexCoord;
        //    public Vector3 Normal;
        //}
        public class Material
        {
            public string Name;

            public Vector3 vAmbient;
            public Vector3 vDiffuse;
            public Vector3 vSpecular;

            public int nShininess;
            public float fAlpha;

            public bool bSpecular;

            public string TextureFileName;
            //public ShaderResourceView TextureRV11;
            public SharpDX.Toolkit.Graphics.Texture2D TextureRV11;
            public ShaderResourceView TextrueMap;
            //public SharpDX.Direct3D11.EffectTechnique Technique;

        }
        internal struct AttributeRage
        {

            public int AttribId;

            public int FaceStart;

            public int FaceCount;

            public int VertexStart;

            public int VertexCount;

        }
        #endregion

        #region Variables / Properties
        //Buffer VertexBuffer { get; set; }
        //Buffer IndexBuffer { get; set; }
        //int VertexCount { get; set; }
        //public int IndexCount { get; private set; }
        //public Texture Texture { get; private set; }
        //public TextureManager TextureManager { get; private set; }
        private InputLayout vertexLayout;
        private Buffer vertexBuffer;
        private Buffer indexBuffer;
        private Buffer instanceBuffer;
        private SharpDX.Direct3D11.EffectTechnique effectTechnique;
        protected SharpDX.Direct3D11.EffectPass effectPass;
        private EffectTransformVariables effectTransforms;
        protected HelixToolkit.Wpf.SharpDX.MaterialGeometryModel3D.EffectMaterialVariables effectMaterial;
        
        private EffectVectorVariable vFrustum, vViewport, vLineParams;
        private SharpDX.Direct3D11.RasterizerState rasterState;
        private SharpDX.Direct3D11.DepthStencilState depthStencilState;

        protected MeshGeometry3D geometry;
        protected MxModelInfo[] instanceArray;


        protected PhongMaterial phongMaterial;
        protected EffectScalarVariable bHasInstances;

        protected bool isChanged = true;
        protected bool hasInstances = false;
        protected bool hasShadowMap = false;

        
        public List<SharpDX.Toolkit.Graphics.VertexPositionNormalTexture> ModelVertices { get; private set; }
        public List<int> ModelIndices { get; private set; }
        public List<int> ModelAttribute { get; private set; }
        public List<Material> ModelMaterials { get; private set; }
        private List<AttributeRage> AttributeTable;

        //顶点缓存
        private VertexBufferBinding[] vertexBufferBindings = null;
        private VertexBufferBinding vertexBufferBinding;

        private int CurSubSet;
        #endregion

        #region Constructors
        public JpSceneModel3D()
        {
            CurSubSet = 0;
            ModelVertices = new List<SharpDX.Toolkit.Graphics.VertexPositionNormalTexture>();
            ModelIndices = new List<int>();
            ModelAttribute = new List<int>();
            ModelMaterials = new List<Material>();
            ModelPath = AppDomain.CurrentDomain.BaseDirectory + @"3DModel\";
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="depthBias"></param>
        protected override void OnRasterStateChanged(int depthBias)
        {
            if (this.IsAttached)
            {
                Disposer.RemoveAndDispose(ref this.rasterState);
                /// --- set up rasterizer states
                var rasterStateDesc = new RasterizerStateDescription()
                {
                    FillMode = FillMode.Wireframe,
                    CullMode = CullMode.Back,
                    DepthBias = depthBias,
                    DepthBiasClamp = -500,
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
                if (ModelFileName == null) throw new ArgumentNullException("ModelFileName", "模型文件名不能为空");
                if (!LoadModel()) return;
                this.Geometry = ConvertModelToMeshGeometry3D();
                base.Attach(host);
                LoadMaterialsTexture();

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
                this.vertexBuffer = Device.CreateBuffer(BindFlags.VertexBuffer, DefaultVertex.SizeInBytes, this.ModelVertices.Select((x, ii) => new DefaultVertex()
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

                this.indexBuffer = Device.CreateBuffer(BindFlags.IndexBuffer, sizeof(int), this.ModelIndices.ToArray());

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
                this.hasInstances = this.Instances != null;
                this.bHasInstances = this.effect.GetVariableByName("bHasInstances").AsScalar();
                if (this.hasInstances)
                {
                    this.instanceBuffer = Buffer.Create(this.Device, this.instanceArray.Select(x => x.ModelPos).ToArray(), new BufferDescription(Matrix.SizeInBytes * this.instanceArray.Length, ResourceUsage.Dynamic, BindFlags.VertexBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0));
                }

                //this.texDiffuseMapVariable = effect.GetVariableByName("texDiffuseMap").AsShaderResource();
                //this.bHasDiffuseMapVariable = effect.GetVariableByName("bHasDiffuseMap").AsScalar();
                
                
                this.effectMaterial = new HelixToolkit.Wpf.SharpDX.MaterialGeometryModel3D.EffectMaterialVariables(this.effect);
                this.effectPass = this.effectTechnique.GetPassByIndex(0);
                //this.effectPass = this.technique.GetPassByIndex(0);
                if (!hasInstances)
                    vertexBufferBinding = new VertexBufferBinding(this.vertexBuffer, DefaultVertex.SizeInBytes, 0);
                this.OnRasterStateChanged(this.DepthBias);
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
                
                g.Positions =new HelixToolkit.Wpf.SharpDX.Core.Vector3Collection( this.ModelVertices.Select(x => x.Position));
                g.Normals =new HelixToolkit.Wpf.SharpDX.Core.Vector3Collection( this.ModelVertices.Select(x => x.Normal));
                g.TextureCoordinates =new HelixToolkit.Wpf.SharpDX.Core.Vector2Collection( this.ModelVertices.Select(x => x.TextureCoordinate));
                g.Indices =new HelixToolkit.Wpf.SharpDX.Core.IntCollection( this.ModelIndices);
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
            Disposer.RemoveAndDispose(ref this.depthStencilState);

            //this.effectName = null;
            this.phongMaterial = null;
            this.geometry = null;
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

            if (this.Geometry == null)
                return;

            if (this.Visibility != System.Windows.Visibility.Visible)
                return;

            if (renderContext.IsShadowPass)
                if (!this.IsThrowingShadow)
                    return;
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
                this.hasInstances = this.Instances != null;
                this.bHasInstances.Set(this.hasInstances);

                /// --- set context
                this.Device.ImmediateContext.InputAssembler.InputLayout = this.vertexLayout;
                this.Device.ImmediateContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
                this.Device.ImmediateContext.InputAssembler.SetIndexBuffer(this.indexBuffer, Format.R32_UInt, 0);
                //this.device.ImmediateContext.Rasterizer.State = rasterState;
                //this.device.ImmediateContext.OutputMerger.DepthStencilState = depthStencilState;

                //this.effectMaterial = new HelixToolkit.SharpDX.Wpf.MaterialGeometryModel3D.EffectMaterialVariables(this.effect);

                //var effectPass = this.technique.GetPassByIndex(0);
                if (this.hasInstances)
                {
                    /// --- update instance buffer
                    
                    if (this.isChanged)
                    {
                        DataStream stream;
                        Device.ImmediateContext.MapSubresource(this.instanceBuffer, MapMode.WriteDiscard, global::SharpDX.Direct3D11.MapFlags.None, out stream);
                        stream.Position = 0;
                        stream.WriteRange(this.instanceArray.Select(x => x.ModelPos).ToArray(), 0, this.instanceArray.Length);
                        Device.ImmediateContext.UnmapSubresource(this.instanceBuffer, 0);
                        stream.Dispose();
                        vertexBufferBindings = new[] {
                                                                                            new VertexBufferBinding(this.vertexBuffer, DefaultVertex.SizeInBytes, 0),
                                                                                            new VertexBufferBinding(this.instanceBuffer, Matrix.SizeInBytes, 0),
                                                                                        };
                        this.isChanged = false;
                    }
                    /// --- INSTANCING: need to set 2 buffers            
                    this.Device.ImmediateContext.InputAssembler.SetVertexBuffers(0, vertexBufferBindings);
                    foreach (var item in AttributeTable)
                    {
                        SetTechniqueAndMaterial(item);
                        this.effectPass.Apply(Device.ImmediateContext);
                        //this.OnRasterStateChanged(this.DepthBias);
                        this.Device.ImmediateContext.DrawIndexedInstanced(item.FaceCount * 3, this.instanceArray.Length, item.FaceStart * 3, 0, 0);
                    }
                }
                else
                {
                    /// --- bind buffer                
                    this.Device.ImmediateContext.InputAssembler.SetVertexBuffers(0, vertexBufferBinding);
                    
                    foreach (var item in AttributeTable)
                    {
                        SetTechniqueAndMaterial(item);
                        //this.OnRasterStateChanged(this.DepthBias);
                        this.effectPass.Apply(Device.ImmediateContext);
                        
                        this.Device.ImmediateContext.DrawIndexed(item.FaceCount * 3, item.FaceStart * 3, 0);
                    }

                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Attach error :" + e.Message);
            }
        }

        private void SetTechniqueAndMaterial(AttributeRage item)
        {
            var material = ModelMaterials.AsEnumerable<Material>().ElementAt(item.AttribId);
            SetEffectMterialVariable(material);
            //    /// --- render the geometry
            //for (int i = 0; i < this.technique.Description.PassCount; i++)
            //{

            //this.technique.GetPassByIndex(0).Apply(device.ImmediateContext);
        }

        private void SetEffectMterialVariable(Material material)
        {
            //this.effectMaterial = new HelixToolkit.SharpDX.Wpf.MaterialGeometryModel3D.EffectMaterialVariables(this.effect);
            var vd = new Vector4(material.vDiffuse, 1);
            this.effectMaterial.vMaterialAmbientVariable.Set(material.vAmbient);
            this.effectMaterial.vMaterialDiffuseVariable.Set(vd);
            this.effectMaterial.vMaterialSpecularVariable.Set(material.vSpecular); 

            if (material.TextrueMap == null)
            {
                this.effectMaterial.texDiffuseMapVariable.SetResource(null);
                this.effectMaterial.bHasDiffuseMapVariable.Set(false);
            }
            else
            {
                this.effectMaterial.texDiffuseMapVariable.SetResource(material.TextrueMap);
                this.effectMaterial.bHasDiffuseMapVariable.Set(true);
            }

        }

        protected void AttachMaterial()
        {
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


        public bool LoadModel()
        {
            bool IsLeftHandCoord = false;
            List<Vector3> Positions = new List<Vector3>();
            List<Vector2> TexCoords = new List<Vector2>();
            List<Vector3> Normals = new List<Vector3>();

            string MaterialFileName = string.Empty;
            List<string> lines = null;

            try
            {
                lines = File.ReadLines(ModelPath + ModelFileName).ToList();

                #region parse file
                foreach (var line in lines)// (var i = 4; i < lines.Count && i < 4 + VertexCount; i++)
                {
                    if (line.Trim().StartsWith("#"))
                    {
                        // Comment
                    }
                    else if (line.Trim().StartsWith("vt"))
                    {
                        // Vertex TexCoord                    
                        var value = line.Trim().Substring(2).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                        TexCoords.Add(new Vector2() { X = float.Parse(value[0]), Y = float.Parse(value[1]) });
                    }
                    else if (line.Trim().StartsWith("vn"))
                    {
                        // Vertex Normal
                        var value = line.Trim().Substring(2).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                        Normals.Add(new Vector3() { X = float.Parse(value[0]), Y = float.Parse(value[1]), Z = float.Parse(value[2]) });
                    }
                    else if (line.Trim().StartsWith("v"))
                    {
                        // Vertex Position
                        var value = line.Trim().Substring(1).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                        Positions.Add(new Vector3() { X = float.Parse(value[0]), Y = float.Parse(value[1]), Z = float.Parse(value[2]) });
                    }
                    else if (line.Trim().StartsWith("f"))
                    {
                        #region
                        /**
                        // Face
                    VertexPositionNormalTexture vertex;

                                            // OBJ format uses 1-based arrays
                        var value = line.Trim().Substring(1).Split(new char[] { ' ','/' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                    for (var iFace=0 ;iFace<3;iFace++)
                    {
                        vertex.Position =Positions.AsEnumerable<Vector3>().ElementAt(int.Parse( value[iFace*3+0])-1);
                        vertex.TextureCoordinate = TexCoords.AsEnumerable<Vector2>().ElementAt(int.Parse(value[iFace*3+1])-1);
                        vertex.Normal =Positions.AsEnumerable<Vector3>().ElementAt(int.Parse( value[iFace*3+2])-1);

                        // If a duplicate vertex doesn't exist, add this vertex to the Vertices
                        // list. Store the index in the Indices array. The Vertices and Indices
                        // lists will eventually become the Vertex Buffer and Index Buffer for
                        // the mesh.
                    //todo check duplicate
                        ModelVertices.Add(vertex);
                        ModelIndices.Add( ModelVertices.Count - 1 );
                    }
                                      
                    ModelAttribute.Add( CurSubSet );
                    **/
                    #endregion
                        SharpDX.Toolkit.Graphics.VertexPositionNormalTexture vertex;

                        // OBJ format uses 1-based arrays
                        var value = line.Trim().Substring(1).Split(new char[] { ' ', '/' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                        if (IsLeftHandCoord) //如果目标为左手座标系统，则需要转换
                        {
                            for (var iFace = 2; iFace >= 0; --iFace)
                            {
                                Vector3 v = Positions.AsEnumerable<Vector3>().ElementAt(int.Parse(value[iFace * 3 + 0]) - 1);
                                vertex.Position = new Vector3(v.X, v.Y, v.Z * -1.0f);
                                Vector2 t = TexCoords.AsEnumerable<Vector2>().ElementAt(int.Parse(value[iFace * 3 + 1]) - 1);
                                vertex.TextureCoordinate = new Vector2(t.X, 1.0f - t.Y);
                                Vector3 n = Normals.AsEnumerable<Vector3>().ElementAt(int.Parse(value[iFace * 3 + 2]) - 1);
                                vertex.Normal = new Vector3(n.X, n.Y, n.Z * -1.0f);

                                // If a duplicate vertex doesn't exist, add this vertex to the Vertices
                                // list. Store the index in the Indices array. The Vertices and Indices
                                // lists will eventually become the Vertex Buffer and Index Buffer for
                                // the mesh.
                                //todo check duplicate
                                ModelVertices.Add(vertex);
                                ModelIndices.Add(ModelVertices.Count - 1);
                            }
                        }
                        else
                        {
                            for (var iFace = 0; iFace < 3; iFace++)
                            {
                                Vector3 v = Positions.AsEnumerable<Vector3>().ElementAt(int.Parse(value[iFace * 3 + 0]) - 1);
                                vertex.Position = new Vector3(v.X, v.Y, v.Z);
                                Vector2 t = TexCoords.AsEnumerable<Vector2>().ElementAt(int.Parse(value[iFace * 3 + 1]) - 1);
                                vertex.TextureCoordinate = new Vector2(t.X, t.Y);
                                Vector3 n = Normals.AsEnumerable<Vector3>().ElementAt(int.Parse(value[iFace * 3 + 2]) - 1);
                                vertex.Normal = new Vector3(n.X, n.Y, n.Z);

                                // If a duplicate vertex doesn't exist, add this vertex to the Vertices
                                // list. Store the index in the Indices array. The Vertices and Indices
                                // lists will eventually become the Vertex Buffer and Index Buffer for
                                // the mesh.
                                //todo check duplicate
                                ModelVertices.Add(vertex);
                                ModelIndices.Add(ModelVertices.Count - 1);
                            }
                        }

                        ModelAttribute.Add(CurSubSet);
                    }
                    else if (line.Trim().StartsWith("mtllib"))
                    {
                        // Material library
                        MaterialFileName = line.Trim().Substring(6).Trim();
                    }
                    else if (line.Trim().StartsWith("usemtl"))
                    {
                        // Material
                        var strName = line.Trim().Substring(6).Trim();
                        bool bFound = false;
                        foreach (var item in ModelMaterials)
                        {
                            if (item.Name == strName)
                            {
                                bFound = true;

                                CurSubSet = ModelMaterials.IndexOf(item);
                                break;
                            }
                        }

                        if (!bFound)
                        {
                            var pMaterial = new Material();

                            CurSubSet = ModelMaterials.Count();

                            InitMaterial(ref pMaterial);
                            pMaterial.Name = strName;

                            ModelMaterials.Add(pMaterial);
                        }
                    }
                    else
                    {
                        // Unimplemented or unrecognized command
                    }


                #endregion
                }
                if (MaterialFileName != string.Empty)
                {
                    LoadMaterialsFromMTL(ModelPath + MaterialFileName);

                }
                SetupAttributeTable();
                return true;
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("加载3D模型文件出错！请检查文件是否存在！\n文件名：" + ModelPath + ModelFileName, "错误：");
                return false;
            }
        }

        private void SetupAttributeTable()
        {
            AttributeTable = new List<AttributeRage>();

            int attrId = 0;
            int start = 0;
            int count = 0;
            int i = 0;
            foreach (var item in ModelAttribute)
            {
                if (item != attrId)
                {
                    AttributeTable.Add(new AttributeRage() { AttribId = attrId, FaceCount = count, FaceStart = start });
                    attrId = item;
                    start = i;
                    count = 1;
                }
                else count++;
                ++i;
            }
            AttributeTable.Add(new AttributeRage() { AttribId = attrId, FaceCount = count, FaceStart = start });
            #region 合并相同资源
            List<AttributeRage> newAttributeTabl = new List<AttributeRage>();
            List<int> newIndices = new List<int>();
            for (int j = 0; j < ModelMaterials.Count; j++)
            {
                //int index= GetAttributeTableItemIndex(newAttributeTabl,AttributeTable[j].AttribId);
                AttributeRage rage = new AttributeRage();
                rage.AttribId = j;
                rage.FaceStart = newIndices.Count / 3;
                rage.FaceCount = 0;
                foreach (var item in AttributeTable)
                {
                    if (item.AttribId == j)
                    {
                        rage.FaceCount += item.FaceCount;
                        newIndices.AddRange(this.ModelIndices.GetRange(item.FaceStart * 3, item.FaceCount * 3));
                    }
                }
                newAttributeTabl.Add(rage);
            }
            this.ModelIndices = newIndices;
            AttributeTable = newAttributeTabl;
            #endregion
        }

        public override bool HitTest(Ray rayWS, ref List<HitTestResult> hits)
        {
            if (this.Instances != null)
            {
                bool hit = false;
                foreach (var modeinfo in Instances)
                {
                    var b = this.Bounds;
                    this.PushMatrix(modeinfo.ModelPos); 
                  //  this.Bounds = BoundingBox.FromPoints(this.Geometry.Positions.Select(x => Vector3.TransformCoordinate(x, this.modelMatrix)).ToArray());
                    if (base.HitTest(rayWS, ref hits))
                    {
                        hit = true;
                        var lastHit = hits[hits.Count - 1];
                        lastHit.Tag =this.Tag.ToString()+":"+ modeinfo.MxID;  //返回实体对象的ID
                        //#if DEBUG
                        //                        System.Diagnostics.Debug.WriteLine("hitTest ModelID:" + modeinfo.ModelID);
                        //#endif
                        hits[hits.Count - 1] = lastHit;
                    }
                    this.PopMatrix();
                    this.Bounds = b;
                }
                return hit;
            }
            else
            {
                return base.HitTest(rayWS, ref hits);
            }
        }

        private void InitMaterial(ref Material pMaterial)
        {
            pMaterial.vAmbient = new Vector3(0.2f, 0.2f, 0.2f);
            pMaterial.vDiffuse = new Vector3(0.8f, 0.8f, 0.8f);
            pMaterial.vSpecular = new Vector3(1.0f, 1.0f, 1.0f);
            pMaterial.nShininess = 0;
            pMaterial.fAlpha = 1.0f;
            pMaterial.bSpecular = false;
            pMaterial.TextureRV11 = null;
            pMaterial.TextrueMap = null;
        }

        private bool LoadMaterialsFromMTL(string strFileName)
        {

            try
            {
                var lines = File.ReadLines(strFileName).ToList();

                Material pMaterial = null;


                foreach (var line in lines)// (var i = 4; i < lines.Count && i < 4 + VertexCount; i++)
                {
                    if (line.Trim().StartsWith("newmtl"))
                    {
                        for (int i = 0; i < ModelMaterials.Count(); i++)
                        {

                            Material pCurMaterial = ModelMaterials.AsEnumerable<Material>().ElementAt(i);
                            string name = line.Trim().Substring(6).Trim();
                            if (pCurMaterial.Name == name)
                            {
                                pMaterial = pCurMaterial;
                                break;
                            }
                        }
                    }
                    // The rest of the commands rely on an active material
                    if (pMaterial == null) continue;

                    if (line.Trim().StartsWith("#"))
                    {
                        // Comment
                    }
                    else if (line.Trim().StartsWith("Ka"))
                    {
                        // Ambient color
                        var value = line.Trim().Substring(2).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                        pMaterial.vAmbient = new Vector3() { X = float.Parse(value[0]), Y = float.Parse(value[1]), Z = float.Parse(value[2]) };
                    }
                    else if (line.Trim().StartsWith("Kd"))
                    {
                        // Diffuse color
                        var value = line.Trim().Substring(2).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                        pMaterial.vDiffuse = new Vector3() { X = float.Parse(value[0]), Y = float.Parse(value[1]), Z = float.Parse(value[2]) };
                    }
                    else if (line.Trim().StartsWith("Ks"))
                    {
                        // Specular color
                        var value = line.Trim().Substring(2).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                        pMaterial.vSpecular = new Vector3() { X = float.Parse(value[0]), Y = float.Parse(value[1]), Z = float.Parse(value[2]) };
                    }
                    else if (line.Trim().StartsWith("d") || line.Trim().StartsWith("Tr"))
                    {
                        // Alpha
                        pMaterial.fAlpha = float.Parse(line.Trim().Substring(2).Trim());
                    }
                    else if (line.Trim().StartsWith("Ns"))
                    {
                        // Shininess
                        pMaterial.nShininess = int.Parse(line.Trim().Substring(2).Trim());
                    }
                    else if (line.Trim().StartsWith("illum"))
                    {
                        // Specular on/off
                        pMaterial.bSpecular = (int.Parse(line.Trim().Substring(5).Trim()) == 2);
                    }
                    else if (line.Trim().StartsWith("map_Kd"))
                    {
                        // Texture
                        pMaterial.TextureFileName = line.Trim().Substring(6).Trim();
                    }

                    else
                    {
                        // Unimplemented or unrecognized command
                    }
                }

            }
            catch (Exception e)
            {
                throw e;
            }

            return true;
        }

        public void LoadMaterialsTexture()
        {
            // Load material textures
            try
            {

                for (int iMaterial = 0; iMaterial < ModelMaterials.Count(); ++iMaterial)
                {
                    Material pMaterial = ModelMaterials.AsEnumerable<Material>().ElementAt(iMaterial);
                    if (pMaterial.TextureFileName != null)
                    {
                        //pMaterial.TextureRV11 = cm.Load<SharpDX.Toolkit.Graphics.Texture2D>(pMaterial.TextureFileName.Substring(4));
                        //pMaterial.TextureRV11 = SharpDX.Toolkit.Graphics.Texture.Load(gd, SystemConfiguration.DataFilePath + pMaterial.TextureFileName.Replace("/", "\\"));
                        //  var filepath = SystemConfiguration.FullDataFilePath + pMaterial.TextureFileName.Replace("/","\\");
                        // pMaterial.TextureRV11 = ShaderResourceView.FromFile( SystemConfiguration.DataFilePath + pMaterial.TextureFileName.Replace("/", "\\"));
                        using (var texture = SharpDX.Direct3D11.Texture2D.FromFile<SharpDX.Direct3D11.Texture2D>(this.Device, ModelPath + pMaterial.TextureFileName))
                        {
                            pMaterial.TextrueMap = new ShaderResourceView(this.Device, texture);
                        }
                        //pMaterial.TextrueMap = ShaderResourceView.FromFile(device, ModelPath + pMaterial.TextureFileName);

                        //BitmapImage bi = new BitmapImage();
                        //// BitmapImage.UriSource must be in a BeginInit/EndInit block.
                        //bi.BeginInit();
                        //bi.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + @"model\mx\Stone_Marble.jpg", UriKind.RelativeOrAbsolute);
                        //bi.EndInit();
                        //pMaterial.TextrueMap = bi;

                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        internal int AddVertex(SharpDX.Toolkit.Graphics.VertexPositionNormalTexture pVertex)
        {

            ModelVertices.Add(pVertex);
            return ModelVertices.Count - 1;
        }


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
