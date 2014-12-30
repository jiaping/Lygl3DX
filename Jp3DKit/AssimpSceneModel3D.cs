#if ASSIMPLIB
using AssimpTest.AssimpWrappers;
using HelixToolkit.Wpf.SharpDX;
using HelixToolkit.Wpf.SharpDX.Core;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Jp3DKit
{
    public class AssimpSceneModel3D : MeshGeometryModel3D
    {
        /// <summary>
        /// The material count for unnamed material sorting.
        /// </summary>
        public static int iNextMaterial = 1;

        #region Public property
        public string ModelFilePath;

        /// <summary>
        /// The radius of this mesh.
        /// </summary>
        public float Radius { get; private set; }
        #endregion

        #region Private property
        private Scene scene;
        /// <summary>
        /// 顶点缓存
        /// </summary>
        private VertexBufferBinding vertexBufferBinding;
        
        private SharpDX.Direct3D11.EffectPass effectPass;
        ///// <summary>
        ///// The vertex buffer for this mesh.  You probably want an GPU memory one.
        ///// </summary>
        //private PositionTextured[] tVertex;

        ///// <summary>
        ///// The index buffer for this mesh.
        ///// </summary>
        //private UInt32[] tIndex;

        /// <summary>
        /// The 'attribute buffer' which says where the materials point to in the mesh.  Note that this is *not* the same.
        /// </summary>
        private Attribute[] tAttibutes;

        /// <summary>
        /// The material buffer which stores the material names - as provided by assimp.
        /// </summary>
        private String[] tMaterials;

        /// <summary>
        /// The texture array.
        /// </summary>

        //private Texture[] tTextures;
        ShaderResourceView[] TexturesView;
        #endregion

        public AssimpSceneModel3D(string modelFileName)
        {
            this.ModelFilePath = AppDomain.CurrentDomain.BaseDirectory + @"3DModel\" + modelFileName;
        }

        #region 渲染相关
        public override void Attach(IRenderHost host)
        {
            try
            {
                
                /// --- attach
                //this.effectName = Techniques.RenderJpSimple;//host.RenderTechnique.ToString();
                MeshGeometry3D geometry = new MeshGeometry3D();
                LoadModel(ref geometry);
                this.Geometry = geometry;       // ConvertModelToMeshGeometry3D();
                base.Attach(host);
                //this.renderTechnique = Techniques.RenderWires;
                reloadTextures(this.Device);
            //    LoadMaterialsTexture();
                this.vertexLayout = EffectsManager.Instance.GetLayout(this.renderTechnique);
                this.effectTechnique = effect.GetTechniqueByName(this.renderTechnique.Name);
                // --- get variables
                //this.vertexLayout = EffectsManager.Instance.GetLayout(host.RenderTechnique);
                //this.effectTechnique = effect.GetTechniqueByName(host.RenderTechnique.Name);
                //this.technique = effect.GetTechniqueByName(this.renderTechnique.Name);
                //this.vertexLayout = host.Effects.GetLayout(this.effectName);
                //this.technique = effect.GetTechniqueByName(this.effectName);
                //this.geometry = this.Geometry as MeshGeometry3D;

                /// --- constant buffer for transformation
                this.effectTransforms = new EffectTransformVariables(this.effect);

                /// --- material 
                //this.AttachMaterial();

                /// --- init vertex buffer
                //this.vertexBuffer = Device.CreateBuffer(BindFlags.VertexBuffer, DefaultVertex.SizeInBytes, this.ModelVertices.Select((x, ii) => new DefaultVertex()
                //{
                //    Position = new Vector4(x.Position, 1f),
                //    Color = new Color4(0f, 0f, 1f, 1f),
                //    //Color       = this.geometry.Colors != null ? this.geometry.Colors[ii] : new Color4(1f, 1f, 1f, 1f),
                //    TexCoord = x.TextureCoordinate,
                //    Normal = x.Normal,
                //    Tangent = new Vector3(),
                //    BiTangent = new Vector3(),
                //}).ToArray());

                /// --- init index buffer

                //this.indexBuffer = Device.CreateBuffer(BindFlags.IndexBuffer, sizeof(int), this.ModelIndices.ToArray());

                var rasterStateDesc = new RasterizerStateDescription()
                {
                    FillMode = FillMode.Wireframe,
                    CullMode = CullMode.Back,
                    IsMultisampleEnabled = true,
                    IsAntialiasedLineEnabled = true,
                    IsFrontCounterClockwise = true,
                };
                this.rasterState = new SharpDX.Direct3D11.RasterizerState(this.Device, rasterStateDesc);
                //var depthStencilDesc = new DepthStencilStateDescription()
                //{
                //    DepthComparison = Comparison.LessEqual,
                //    DepthWriteMask = global::SharpDX.Direct3D11.DepthWriteMask.All,
                //    IsDepthEnabled = true,
                //};
                //this.depthStencilState = new SharpDX.Direct3D11.DepthStencilState(this.device, depthStencilDesc);


                this.effectMaterial = new HelixToolkit.Wpf.SharpDX.MaterialGeometryModel3D.EffectMaterialVariables(this.effect);
                this.effectPass = this.effectTechnique.GetPassByIndex(0);
                //this.effectPass = this.technique.GetPassByIndex(0);
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

        public override void Render(RenderContext renderContext)
        {
            if (!this.IsRendering) return;
            if (this.Geometry == null) return;
            if (this.Visibility != System.Windows.Visibility.Visible) return;

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
                

                /// --- set context
                this.Device.ImmediateContext.InputAssembler.InputLayout = this.vertexLayout;
                this.Device.ImmediateContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
                /// --- bind buffer                
                this.Device.ImmediateContext.InputAssembler.SetVertexBuffers(0, vertexBufferBinding);
                this.Device.ImmediateContext.InputAssembler.SetIndexBuffer(this.indexBuffer, Format.R32_UInt, 0);

                this.Device.ImmediateContext.Rasterizer.State = rasterState;
                //this.device.ImmediateContext.OutputMerger.DepthStencilState = depthStencilState;
                //this.effectMaterial = new HelixToolkit.SharpDX.Wpf.MaterialGeometryModel3D.EffectMaterialVariables(this.effect);
                //var effectPass = this.technique.GetPassByIndex(0);

                foreach (var item in this.tAttibutes)
                {
                    SetTechniqueAndMaterial(item.iAttributeID);
                    //this.OnRasterStateChanged(this.DepthBias);
                    this.effectPass.Apply(Device.ImmediateContext);
                    
                    this.Device.ImmediateContext.DrawIndexed(item.iFaceCount*3, item.iIndexStart, item.iVertexStart);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Attach error :" + e.Message);
            }
        }

        #endregion

        #region assimp.scene to geometry 相关处理函数
        public void LoadModel(ref MeshGeometry3D geometry)
        {
            #region Assimp Import Flags
            /// <summary>
            /// Target post-processing steps for the standard flat tangent mesh.
            /// </summary>
            const aiPostProcessSteps DefaultFlags = aiPostProcessSteps.CalcTangentSpace // calculate tangents and bitangents if possible
               | aiPostProcessSteps.JoinIdenticalVertices // join identical vertices/ optimize indexing
               | aiPostProcessSteps.ValidateDataStructure // perform a full validation of the loader's output
               | aiPostProcessSteps.ImproveCacheLocality // improve the cache locality of the output vertices
               | aiPostProcessSteps.RemoveRedundantMaterials // remove redundant materials
               | aiPostProcessSteps.FindDegenerates // remove degenerated polygons from the import
               | aiPostProcessSteps.FindInvalidData // detect invalid model data, such as invalid normal vectors
               | aiPostProcessSteps.GenUVCoords // convert spherical, cylindrical, box and planar mapping to proper UVs
               | aiPostProcessSteps.TransformUVCoords // preprocess UV transformations (scaling, translation ...)
               | aiPostProcessSteps.FindInstances // search for instanced meshes and remove them by references to one master
               | aiPostProcessSteps.LimitBoneWeights // limit bone weights to 4 per vertex
               //| aiPostProcessSteps.OptimizeMeshes // join small meshes, if possible;
                // | aiPostProcessSteps.FixInfacingNormals
               | aiPostProcessSteps.GenSmoothNormals // generate smooth normal vectors if not existing
               | aiPostProcessSteps.SplitLargeMeshes // split large, unrenderable meshes into submeshes
               | aiPostProcessSteps.Triangulate // triangulate polygons with more than 3 edges
               | aiPostProcessSteps.SortByPType // make 'clean' meshes which consist of a single typ of primitives
               | aiPostProcessSteps.PreTransformVertices // bake transforms, fixes most errors for Xna
               //| aiPostProcessSteps.FlipUVs  // common DirectX issue (Xna also)                                                                           //反转纹理座票
              //| aiPostProcessSteps.MakeLeftHanded  // Makes the model import the right way round (not flipped left to right).     //转换为左手
               //| aiPostProcessSteps.FlipWindingOrder                                                                                                                  //反转点顺序
               ;
            #endregion
            this.scene = AssimpImporter.readFile(this.ModelFilePath, DefaultFlags);
            // If we had an error then throw an exception.
            if (this.scene == null)
                throw new Exception(AssimpImporter.getErrorString());

            // Order the meshes into a material-mesh array table.
            Dictionary<String, List<Mesh>> dTable = new Dictionary<String, List<Mesh>>();

            // Sort the meshes into material (i.e. attribute order).
            int iVBufferSize = 0;
            int iIBufferSize = 0;
            foreach (Mesh pMesh in this.scene.getMeshes())
            {
                // Check we have the appropriate data.
                //if (!(pMesh.hasPositions() & pMesh.hasNormals() & pMesh.hasTangentsAndBitangents() & pMesh.hasTextureCoords(0) & pMesh.hasFaces()))
                //    throw new Exception("Cannot import mesh. Required all, found: Positions=" + pMesh.hasPositions() + ", Normals=" + pMesh.hasNormals() + ", TangentsAndBitangents=" + pMesh.hasTangentsAndBitangents() + ", TextureCoords=" + pMesh.hasTextureCoords(0) + ", Faces=" + pMesh.hasFaces() + ".");

                // Convert the Assimp material to an Evolved material.
                AssimpTest.AssimpWrappers.Material pMat = this.scene.getMaterial((int)pMesh.getMaterialIndex());
                String sMaterial = pMat.getStringProperty("$tex.file");
                if (sMaterial == null)
                    sMaterial += "UnboundMaterial_" + (iNextMaterial++);
                else
                    sMaterial = AppDomain.CurrentDomain.BaseDirectory + @"3DModel\" + sMaterial;

                // If this is the first entry - add to the list.
                List<Mesh> lMeshes = null;
                if (!dTable.TryGetValue(sMaterial, out lMeshes))
                {
                    lMeshes = new List<Mesh>();
                    dTable[sMaterial] = lMeshes;
                }

                // Work out the max vertex and face count.
                iVBufferSize += (int)pMesh.getNumVertices();
                iIBufferSize += (int)pMesh.getNumFaces() * 3;

                // Insert the mesh under the appropriate material.
                lMeshes.Add(pMesh);
            }

            // Create the buffers and reset the stats.
            geometry.Positions = new Vector3Collection(iVBufferSize); // tVertex = new PositionTextured[iVBufferSize];
            geometry.TextureCoordinates = new Vector2Collection(iVBufferSize);
            geometry.Normals = new Vector3Collection(iVBufferSize);
            geometry.Indices = new IntCollection(iIBufferSize);             // tIndex = new UInt32[iIBufferSize];
            tAttibutes = new Attribute[dTable.Count];
            tMaterials = new String[dTable.Count];
            TexturesView= new ShaderResourceView[dTable.Count];
            dTable.Keys.CopyTo(tMaterials, 0);

            Radius = 0f;

            // Monitor global poisition in the vertex, index and attribute buffers.
            int iAttribute = 0;
            int iBVertex = 0;
            int iBIndex = 0;

            // For each material.
            foreach (String sMaterial in dTable.Keys)
            {
                // Create a new attribute.
                Attribute pAttrib = new Attribute();
                tAttibutes[iAttribute] = pAttrib;

                // Store the buffer offsets into the attribute.
                pAttrib.iAttributeID = iAttribute;
                pAttrib.iVertexStart = iBVertex;
                pAttrib.iIndexStart = iBIndex;

                int iAVertex = 0;
                int iAFace = 0;

                // Copy the vertices and indicies of each mesh (updating the buffer counts).
                foreach (Mesh pMesh in dTable[sMaterial])
                {
                    // Copy verticies.
                    int iMeshVerts = (int)pMesh.getNumVertices();
                    for (int iVertex = 0; iVertex < iMeshVerts; ++iVertex)
                    {
                        // Create the vertex.
                        Vector3 pVertex;//PositionTextured pVertex = new PositionTextured();
                        Vector2 pUVCoord;
                        Vector3 pNormal;
                        // Copy data.
                        pVertex = toDX9(pMesh.getPosition(iVertex));    //pVertex.Position = toDX9(pMesh.getPosition(iVertex));
                        pUVCoord = toDX9UV(pMesh.getTextureCoordinate(0, iVertex));//pVertex.UV = toDX9UV(pMesh.getTextureCoordinate(0, iVertex));
                        pNormal = toDX9(pMesh.getNormal(iVertex));
                        // Update the radius.
                        Radius = Math.Max(pVertex.Length(), Radius);
                   
                        // Store.
                        geometry.Positions.Add(pVertex);   //tVertex[iVertex + iBVertex] = pVertex;
                        geometry.TextureCoordinates.Add(pUVCoord);
                        geometry.Normals.Add(pNormal);
                    }

                    // Increment the vertex count by the number of verticies we just looped over.
                    iAVertex += iMeshVerts;
                    iBVertex += iMeshVerts;

                    // Copy indicies.
                    int iMeshFaces = (int)pMesh.getNumFaces();
                    for (int iFace = 0; iFace < iMeshFaces; ++iFace)
                    {
                        uint[] tIndices = pMesh.getFace(iFace).getIndices();
                        if (tIndices.Length != 3)
                            throw new Exception("Cannot load a mesh which does not have only triangluar faces.");

                        geometry.Indices.Add((int)tIndices[0]);      //tIndex[iBIndex++] = tIndices[0];
                        geometry.Indices.Add( (int)tIndices[1]);     //tIndex[iBIndex++] = tIndices[1];
                        geometry.Indices.Add( (int)tIndices[2]);     //tIndex[iBIndex++] = tIndices[2];
                        iBIndex += 3;
                    }

                    // Increment the face count by the number of faces we just looped over.
                    iAFace += iMeshFaces;
                }

                // Save the sizes.
                pAttrib.iFaceCount = iAFace;
                pAttrib.iVertexCount = iAVertex;

                // Increment the attribute counter.
                ++iAttribute;
            }
        }
        private void SetTechniqueAndMaterial(int materialIndex)
        {
            AssimpTest.AssimpWrappers.Material pMaterial= this.scene.getMaterial(materialIndex);
            aiColor4D a,d,s,e;
            string diffuseTextureName;
            //注意a向量为0表示透明，1表示不透明
            if (pMaterial.getProperty<aiColor4D>("$clr.ambient", out a))
                this.effectMaterial.vMaterialAmbientVariable.Set(new Color4(a.r, a.g, a.b, 1));
            if( pMaterial.getProperty<aiColor4D>("$clr.diffuse", out d))
                this.effectMaterial.vMaterialDiffuseVariable.Set(new Color4(d.r, d.g, d.b, 1));
            if (pMaterial.getProperty<aiColor4D>("$clr.specular", out s))
                this.effectMaterial.vMaterialSpecularVariable.Set(new Color4(s.r, s.g, s.b, 1));
            if (pMaterial.getProperty<aiColor4D>("$clr.emissive", out e))
                this.effectMaterial.vMaterialEmissiveVariable.Set(new Color4(e.r, e.g, e.b, 1));
            diffuseTextureName = pMaterial.getStringProperty("$tex.file");
            if (diffuseTextureName == null)
            {
                this.effectMaterial.texDiffuseMapVariable.SetResource(null);
                this.effectMaterial.bHasDiffuseMapVariable.Set(false);
            }
            else
            {
                ShaderResourceView rv = TexturesView[materialIndex];
                this.effectMaterial.texDiffuseMapVariable.SetResource(rv);
                this.effectMaterial.bHasDiffuseMapVariable.Set(true);
            }


            //SetEffectMterialVariable(material);
            //    /// --- render the geometry
            //for (int i = 0; i < this.technique.Description.PassCount; i++)
            //{

            //this.technique.GetPassByIndex(0).Apply(device.ImmediateContext);
        }
      
        public void reloadTextures(SharpDX.Direct3D11.Device device)
        {
            //string texturepath = AppDomain.CurrentDomain.BaseDirectory + @"3dmodel/" ;
            // Try to load the textures from the instructions in the mesh file.
            for (int iAttr = 0; iAttr < tMaterials.Length; ++iAttr)
            {
                try
                {
                    var bb=new BitmapImage(new Uri( tMaterials[iAttr], UriKind.RelativeOrAbsolute));
                    TexturesView[iAttr] = ShaderResourceView.FromMemory(device, bb.ToByteArray());
                }
                catch (Exception pError)
                {
                    Console.WriteLine("Could not load texture for '" + tMaterials[iAttr] + "'.  Error = " + pError.Message);
                    TexturesView[iAttr] = null;
                }
            }
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Convert an Assimp aiColour4D into an integer.
        /// </summary>
        /// <param name="vValue">The assimp value.</param>
        /// <returns>The equivilant DX value.</returns>
        public static int toDX9(aiColor4D vValue)
        {
            Color4 pColour = new Color4(vValue.a, vValue.r, vValue.g, vValue.b);
            return pColour.ToRgba();   ///???
        }

        /// <summary>
        /// Convert an Assimp aiVector3D into a DirectX Vector3D.
        /// </summary>
        /// <param name="vValue">The assimp value.</param>
        /// <param name="vOut">The DirectX value.</param>
        public static Vector3 toDX9(aiVector3D vValue)
        {
            return new Vector3(vValue.x, vValue.y, vValue.z);
        }

        /// <summary>
        /// Convert an Assimp aiVector3D into a DirectX Vector3D.
        /// </summary>
        /// <param name="vValue">The assimp value.</param>
        /// <param name="vOut">The DirectX value.</param>
        public static void toDX9(aiVector3D vValue, out Vector3 vOut)
        {
            vOut.X = vValue.x;
            vOut.Y = vValue.y;
            vOut.Z = vValue.z;
        }

        /// <summary>
        /// Convert an Assimp aiVector3D into a DirectX Vector2.
        /// </summary>
        /// <param name="vValue">The assimp value.</param>
        /// <returns>The equivilant DirectX value.</returns>
        public static Vector2 toDX9UV(aiVector3D vValue)
        {
            return new Vector2(vValue.x, vValue.y);
        }
        #endregion

        /// <summary>
        /// The vertex format for rendering this mesh.
        /// </summary>
        public struct PositionTextured
        {
            /// <summary>
            /// Vertex point.
            /// </summary>
            public Vector3 Position;

            /// <summary>
            /// Vertex texture coordinates.
            /// </summary>
            public Vector2 UV;
        }

        #region Attribute Flag
        /// <summary>
        /// An attribute entry corresponds to a subset of the mesh and specifies the block of memory in the vertex/index buffers where the geometry for the subset resides.
        /// </summary>
        public class Attribute
        {
            /// <summary>
            /// The subset ID.
            /// </summary>
            public int iAttributeID;

            /// <summary>
            /// An offset into the index buffer (FaceStart * 3) that identifies the start of the triangles that are ascociated with this subset.
            /// </summary>
            public int iIndexStart;

            /// <summary>
            /// The number of faces (triangles) in the subset.
            /// </summary>
            public int iFaceCount;

            /// <summary>
            /// An offset into the vertex buffer that identifies the start of the verticies that are associated with this subset.
            /// </summary>
            public int iVertexStart;

            /// <summary>
            /// The number of verticies in the subset.
            /// </summary>
            public int iVertexCount;
        }
        #endregion
    }
}
#endif