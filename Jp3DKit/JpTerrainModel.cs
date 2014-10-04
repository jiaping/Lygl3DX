using HelixToolkit.Wpf;
using HelixToolkit.Wpf.SharpDX;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;

namespace Jp3DKit
{
    public class JpTerrainModel : GeometryModel3D
    {
        #region Structures
        public struct HeightMapType
        {
            public float x, y, z;
            public float nx, ny, nz;
            public float tu, tv;
            public static implicit operator DefaultVertex(HeightMapType hmapVector)
            {
                return new DefaultVertex()
                    {
                        Normal = new Vector3(hmapVector.nx, hmapVector.ny, hmapVector.nz),
                        Position = new Vector4(hmapVector.x, hmapVector.y, hmapVector.z,1.0f),
                        TexCoord = new Vector2(hmapVector.tu, hmapVector.tv),
                        Color = new Color4(0f, 0f, 1f, 1f),
                        Tangent = new Vector3(),
                        BiTangent = new Vector3()
                    };
            }
        }
        #endregion
        #region Structures
       

        [StructLayout(LayoutKind.Sequential)]
        internal struct MatrixBuffer
        {
            public Matrix world;
            public Matrix view;
            public Matrix projection;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct LightBuffer
        {
            public Vector4 ambientColor;
            public Vector4 diffuseColor;
            public Vector3 lightDirection;
            public float padding;
        }
        #endregion

        public string HeightMapFileName
        {
            get { return (string)GetValue(HeightMapFileNameProperty); }
            set { SetValue(HeightMapFileNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeightMapFileName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeightMapFileNameProperty =
            DependencyProperty.Register("HeightMapFileName", typeof(string), typeof(JpTerrainModel), new PropertyMetadata(null));



        public string TextureFileName
        {
            get { return (string)GetValue(TextureFileNameProperty); }
            set { SetValue(TextureFileNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextureFileName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextureFileNameProperty =
            DependencyProperty.Register("TextureFileName", typeof(string), typeof(JpTerrainModel), new PropertyMetadata(null));

        public string ModelPath
        {
            get { return (string)GetValue(ModelPathProperty); }
            set { SetValue(ModelPathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ModelPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModelPathProperty =
            DependencyProperty.Register("ModelPath", typeof(string), typeof(JpTerrainModel), new PropertyMetadata(null));

        #region Variables / Properties
        protected SharpDX.Direct3D11.Buffer VertexBuffer { get; set; }
        protected SharpDX.Direct3D11.Buffer IndexBuffer { get; set; }
        public int VertexCount { get; protected set; }
        public int IndexCount { get; protected set; }

        protected int TerrainWidth { get; set; }
        protected int TerrainHeight { get; set; }
        protected int NumberOfVerticesPerQuad { get; set; }

        List<HeightMapType> HeightMap { get; set; }

        ShaderResourceView TextureResource;

        //public Texture Texture { get; private set; }

        private int TextureRepeat { get; set; }

        private DefaultVertex[] Vertices { get; set; }
        private int[] Indices { get; set; }
        private EffectTechnique technique;
        protected EffectPass effectPass;
        private EffectTransformVariables effectTransforms;
        protected MaterialGeometryModel3D.EffectMaterialVariables effectMaterial;
        //顶点缓存
        private VertexBufferBinding[] vertexBufferBindings = null;
        private VertexBufferBinding vertexBufferBinding;

        public InputLayout vertexLayout { get; set; }

        public SharpDX.Direct3D11.Buffer vertexBuffer { get; set; }

        public SharpDX.Direct3D11.Buffer indexBuffer { get; set; }
        #endregion


        public JpTerrainModel()
        {
            ModelPath = AppDomain.CurrentDomain.BaseDirectory + @"3DModel\";
        }

        public override void Attach(IRenderHost host)
        {
            try
            {
               
                
                /// --- attach
                //this.effectName = Techniques.RenderJpSimple;//host.RenderTechnique.ToString();
                base.Attach(host);

                if (HeightMapFileName == null) throw new ArgumentNullException("ModelFileName", "地形文件名不能为空");
                if (!LoadModel()) return;
                // Load the texture for this model.
                if (TextureFileName != null)
                {
                    TextureResource = ShaderResourceView.FromFile(Device, ModelPath + TextureFileName);
                }
                //this.Geometry = ConvertModelToMeshGeometry3D();
                
                
                
                // --- get variables
                this.vertexLayout = EffectsManager.Instance.GetLayout(host.RenderTechnique);
                this.technique = effect.GetTechniqueByName(this.renderTechnique.Name);
                //this.vertexLayout = host.Effects.GetLayout(this.effectName);
                //this.technique = effect.GetTechniqueByName(this.effectName);
                //this.geometry = this.Geometry as MeshGeometry3D;

                /// --- constant buffer for transformation
                this.effectTransforms = new EffectTransformVariables(this.effect);

                /// --- material 
                //this.AttachMaterial();

                /// --- init vertex buffer
                this.vertexBuffer = Device.CreateBuffer(BindFlags.VertexBuffer, DefaultVertex.SizeInBytes, this.Vertices.Select((x, ii) => new DefaultVertex()
                {
                    Position = x.Position,
                    Color = new Color4(1f, 0f, 0f, 1f),
                    //Color       = this.geometry.Colors != null ? this.geometry.Colors[ii] : new Color4(1f, 1f, 1f, 1f),
                    TexCoord = x.TexCoord,
                    Normal = x.Normal,
                    Tangent = new Vector3(),
                    BiTangent = new Vector3(),
                }).ToArray());

                /// --- init index buffer

                this.indexBuffer = Device.CreateBuffer(BindFlags.IndexBuffer, sizeof(int), this.Indices.ToArray());

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


                this.effectMaterial = new HelixToolkit.Wpf.SharpDX.MaterialGeometryModel3D.EffectMaterialVariables(this.effect);
                this.effectPass = this.technique.GetPassByIndex(0);
                    vertexBufferBinding = new VertexBufferBinding(this.vertexBuffer, DefaultVertex.SizeInBytes, 0);
                this.OnRasterStateChanged(this.DepthBias);
                /// --- flush


                this.effectMaterial.vMaterialAmbientVariable.Set(PhongMaterials.ToColor(0.1, 0.1, 0.1, 1.0));
                this.effectMaterial.vMaterialDiffuseVariable.Set(PhongMaterials.ToColor(1, 1, 1, 1f));
                this.effectMaterial.vMaterialSpecularVariable.Set(PhongMaterials.ToColor(0.0225, 0.0225, 0.0225, 1.0)); 

                //this.effectMaterial.vMaterialAmbientVariable.Set(new Vector4(1f, 0.15f, 0.15f, 1.0f));
                //this.effectMaterial.vMaterialDiffuseVariable.Set(new Vector4(0, 1, 0, 1f));
                //this.effectMaterial.vMaterialSpecularVariable.Set(new Vector4(0, 1, 1, 1));

                this.effectMaterial.texDiffuseMapVariable.SetResource(TextureResource);
                this.effectMaterial.bHasDiffuseMapVariable.Set(true);
                //this.effectPass.Apply(Device.ImmediateContext);

                //Light.SetDirection(1, 0, 1);
                //Light.SetSpecularPower(32);
                
                this.Device.ImmediateContext.Flush();
                
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Attach error :" + e.Message);
            }
        }

        public bool LoadModel()
        {
            // Set the number of vertices per quad (2 triangles)
            NumberOfVerticesPerQuad = 6;

            // Set the value of the topology
            //CurrentTopology = PrimitiveTopology.TriangleList;

            // How many times the terrain texture will be repeated both over the width and length of the terrain.
            TextureRepeat = 1;

            // Load the height map for the terrain
            if (!LoadHeightMapFilename(HeightMapFileName))
                return false;

            // Normalize the height of the height map
            NormalizeHeightMap();

            // Calculate the normals for the terrain data.
            if (!CalculateNormals())
                return false;

            // Calculate the texture coordinates.
            CalculateTextureCoordinates();

            

            // Initialize the vertex and index buffer.
            // Calculate the number of the vertices in the terrain mesh.
            VertexCount = (TerrainWidth - 1) * (TerrainHeight - 1) * NumberOfVerticesPerQuad;
            // Set the index count to the same as the vertex count.
            IndexCount = VertexCount;

            FillArrays();

            return true;
        }

        private bool LoadHeightMapFilename(string heightMapFileName)
        {
            System.Drawing.Bitmap bitmap;
            try
            {
                // Open heightmap file
                bitmap = new System.Drawing.Bitmap(ModelPath + heightMapFileName);
            }
            catch (Exception)
            {
                return false;
            }

            // Save the dimensions of the terrain
            TerrainHeight = bitmap.Height;
            TerrainWidth = bitmap.Width;

            // Create the structure to hold the height map data
            HeightMap = new List<HeightMapType>(TerrainWidth * TerrainHeight);

            // Read the image data into the height map
            for (var j = 0; j < TerrainHeight; j++)
                for (var i = 0; i < TerrainWidth; i++)
                    HeightMap.Add(new HeightMapType()
                    {
                        x = i,
                        y = bitmap.GetPixel(i, j).R,
                        z = j
                    });

            return true;
        }

        private void NormalizeHeightMap()
        {
            for (var i = 0; i < HeightMap.Count; i++)
            {
                var temp = HeightMap[i];
                temp.y /= 15;
                HeightMap[i] = temp;
            }
        }
        private bool CalculateNormals()
        {
            // Create a temporary array to hold the un-normalized normal verctors.
            var normals = new Vector3[(TerrainHeight - 1) * (TerrainWidth - 1)];

            // Go through all the faces in the mesh and calculate their normals.
            for (var j = 0; j < (TerrainHeight - 1); j++)
            {
                for (var i = 0; i < (TerrainWidth - 1); i++)
                {
                    var index1 = (j * TerrainWidth) + i;
                    var index2 = (j * TerrainWidth) + (i + 1);
                    var index3 = ((j + 1) * TerrainWidth) + i;

                    // Get three vertices from the face.
                    var vertex1 = new[] { HeightMap[index1].x, HeightMap[index1].y, HeightMap[index1].z };
                    var vertex2 = new[] { HeightMap[index2].x, HeightMap[index2].y, HeightMap[index2].z };
                    var vertex3 = new[] { HeightMap[index3].x, HeightMap[index3].y, HeightMap[index3].z };

                    // Calculate the two vectors for this face.
                    var vector1 = new[] 
						{ 
							vertex1[0] - vertex3[0], 
							vertex1[1] - vertex3[1],
							vertex1[2] - vertex3[2]
						};
                    var vector2 = new[] 
						{ 
							vertex3[0] - vertex2[0], 
							vertex3[1] - vertex2[1],
							vertex3[2] - vertex2[2]
						};

                    var index = (j * (TerrainWidth - 1)) + i;

                    // Calculate the cross product of those two vectors to get the un-normalized value for this face normal.
                    normals[index] = new Vector3()
                    {
                        X = (vector1[1] * vector2[2]) - (vector1[2] * vector2[1]),
                        Y = (vector1[2] * vector2[0]) - (vector1[0] * vector2[2]),
                        Z = (vector1[0] * vector2[1]) - (vector1[1] * vector2[0])
                    };
                }
            }

            // Now go through all the vertices and take an average of each face normal
            // that the vertex touches to get the averaged normal fot that vertex.
            for (var j = 0; j < (TerrainHeight - 1); j++)
            {
                for (var i = 0; i < (TerrainWidth - 1); i++)
                {
                    int index;
                    // Initialize the sum
                    var sum = new[] { 0f, 0f, 0f };

                    // Initialize the count
                    var count = 0;

                    // Bottom left face
                    if (((i - 1) >= 0) && ((j - 1) >= 0))
                    {
                        index = ((j - 1) * (TerrainWidth - 1)) + (i - 1);
                        sum[0] += normals[index].X;
                        sum[1] += normals[index].Y;
                        sum[2] += normals[index].Z;
                        count++;
                    }

                    // Bottom right face
                    if ((i < (TerrainWidth - 1)) && ((j - 1) >= 0))
                    {
                        index = ((j - 1) * (TerrainWidth - 1)) + i;
                        sum[0] += normals[index].X;
                        sum[1] += normals[index].Y;
                        sum[2] += normals[index].Z;
                        count++;
                    }

                    // Upper left face
                    if (((i - 1) >= 0) && (j < (TerrainHeight - 1)))
                    {
                        index = (j * (TerrainWidth - 1)) + i;
                        sum[0] += normals[index].X;
                        sum[1] += normals[index].Y;
                        sum[2] += normals[index].Z;
                        count++;
                    }

                    // Upper right face
                    if ((i < (TerrainWidth - 1)) && (j < (TerrainHeight - 1)))
                    {
                        index = (j * (TerrainWidth - 1)) + i;
                        sum[0] += normals[index].X;
                        sum[1] += normals[index].Y;
                        sum[2] += normals[index].Z;
                        count++;
                    }

                    // Take the average of the faces touching this vertex.
                    sum[0] /= count;
                    sum[1] /= count;
                    sum[2] /= count;

                    // Calculate the length of this normal.
                    var length = (float)Math.Sqrt(sum[0] * sum[0] + sum[1] * sum[1] + sum[2] * sum[2]);

                    // Get the index to the vertex location in the height map array.
                    index = (j * TerrainWidth) + i;

                    // Normalize the final share normal fot this vertex and store it in the height map array.
                    var heightMap = HeightMap[index];
                    heightMap.nx = sum[0] / length;
                    heightMap.ny = sum[1] / length;
                    heightMap.nz = sum[2] / length;
                    HeightMap[index] = heightMap;
                }
            }

            return true;
        }

        private void CalculateTextureCoordinates()
        {
            // Calculate how much to increment the texture coordinates by.
            var incrementValue = (float)TextureRepeat / TerrainWidth;

            // Calculate how many times to repeat the texture.
            var incrementCount = TerrainWidth / TextureRepeat;

            // Initialize the tu and tv coordinate values.
            var tuCoordinate = 0f;
            var tvCoordinate = 1f;

            // Initialize the tu and tv coordinate indexes.
            var tuCount = 0;
            var tvCount = 0;

            // Loop through the entire height map and calculate the tu and tv coordinates for each vertex.
            for (var j = 0; j < TerrainHeight; j++)
            {
                for (var i = 0; i < TerrainWidth; i++)
                {
                    // Store the texture coordinate in the height map.
                    var heightMap = HeightMap[(TerrainWidth * j) + i];
                    heightMap.tu = tuCoordinate;
                    heightMap.tv = tvCoordinate;
                    HeightMap[(TerrainWidth * j) + i] = heightMap;

                    // Increment the tu texture coordinate by the increment value and increment the index by one.
                    tuCoordinate += incrementValue;
                    tuCount++;

                    // Check if at the far right end of the texture and if so then start at the beginning again.
                    if (tuCount == incrementCount)
                    {
                        tuCoordinate = 0.0f;
                        tuCount = 0;
                    }
                }

                // Increment the tv texture coordinate by the increment value and increment the index by one.
                tvCoordinate -= incrementValue;
                tvCount++;

                // Check if at the top of the texture and if so then start at the bottom again.
                if (tvCount == incrementCount)
                {
                    tvCoordinate = 1.0f;
                    tvCount = 0;
                }
            }
        }

        protected void FillArrays()
        {
            // Create the vertex array.
            Vertices = new DefaultVertex[VertexCount];
            // Create the index array.
            Indices = new int[IndexCount];
            var index = 0;

            for (var j = 0; j < TerrainHeight - 1; j++)
            {
                for (var i = 0; i < TerrainWidth - 1; i++)
                {
                    var indexBottomLeft = TerrainWidth * j + i;
                    var indexBottomRight = TerrainWidth * j + (i + 1);
                    var indexUpperLeft = TerrainWidth * (j + 1) + i;
                    var indexUpperRight = TerrainWidth * (j + 1) + (i + 1);

                    #region First Triangle
                    // Upper left
                    Vertices[index] = HeightMap[indexUpperLeft];
                    // Modify the TexCoord coordinates to cover the top edge.
                    if (Vertices[index].TexCoord.Y == 1.0f) Vertices[index].TexCoord.Y = 0.0f;
                    Indices[index] = index++;

                    // Upper right
                    Vertices[index] = HeightMap[indexUpperRight];
                    // Modify the TexCoord coordinates to cover the top and right edges.
                    if (Vertices[index].TexCoord.X == 0.0f) Vertices[index].TexCoord.X = 1.0f;
                    if (Vertices[index].TexCoord.Y == 1.0f) Vertices[index].TexCoord.Y = 0.0f;
                    Indices[index] = index++;

                    // Bottom Left
                    Vertices[index] = HeightMap[indexBottomLeft];
                    Indices[index] = index++;
                    #endregion

                    #region Second Triangle
                    // Bottom Left
                    Vertices[index] = HeightMap[indexBottomLeft];
                    Indices[index] = index++;

                    // Upper right
                    Vertices[index] = HeightMap[indexUpperRight];
                    // Modify the TexCoord coordinates to cover the top and right edges.
                    if (Vertices[index].TexCoord.X == 0.0f) Vertices[index].TexCoord.X = 1.0f;
                    if (Vertices[index].TexCoord.Y == 1.0f) Vertices[index].TexCoord.Y = 0.0f;
                    Indices[index] = index++;

                    // Bottom right
                    Vertices[index] = HeightMap[indexBottomRight];
                    // Modify the TexCoord coordinates to cover the right edge.
                    if (Vertices[index].TexCoord.X == 0.0f) Vertices[index].TexCoord.X = 1.0f;
                    Indices[index] = index++;
                    #endregion
                }
            }
            //// Create the vertex array.
            //Vertices = new JpSimpleVertex[VertexCount];
            //// Create the index array.
            //Indices = new int[IndexCount];
            //var index = 0;

            //for (var j = 0; j < TerrainHeight - 1; j++)
            //{
            //    for (var i = 0; i < TerrainWidth - 1; i++)
            //    {
            //        var indexBottomLeft = TerrainHeight * j + i;
            //        var indexBottomRight = TerrainHeight * j + (i + 1);
            //        var indexUpperLeft = TerrainHeight * (j + 1) + i;
            //        var indexUpperRight = TerrainHeight * (j + 1) + (i + 1);

            //        #region First Triangle

            //        // Bottom Left
            //        Vertices[index] = HeightMap[indexBottomLeft];
            //        Indices[index] = index++;

            //        // Upper right
            //        Vertices[index] = HeightMap[indexUpperRight];
            //        // Modify the texture coordinates to cover the top and right edges.
            //        if (Vertices[index].TexCoord.X == 0.0f) Vertices[index].TexCoord.X = 1.0f;
            //        if (Vertices[index].TexCoord.Y == 1.0f) Vertices[index].TexCoord.Y = 0.0f;
            //        Indices[index] = index++;

            //        // Upper left
            //        Vertices[index] = HeightMap[indexUpperLeft];
            //        // Modify the texture coordinates to cover the top edge.
            //        if (Vertices[index].TexCoord.Y == 1.0f) Vertices[index].TexCoord.Y = 0.0f;
            //        Indices[index] = index++;
            //        #endregion

            //        #region Second Triangle
            //        // Bottom right
            //        Vertices[index] = HeightMap[indexBottomRight];
            //        // Modify the texture coordinates to cover the right edge.
            //        if (Vertices[index].TexCoord.X == 0.0f) Vertices[index].TexCoord.X = 1.0f;
            //        Indices[index] = index++;

            //        // Upper right
            //        Vertices[index] = HeightMap[indexUpperRight];
            //        // Modify the texture coordinates to cover the top and right edges.
            //        if (Vertices[index].TexCoord.X == 0.0f) Vertices[index].TexCoord.X = 1.0f;
            //        if (Vertices[index].TexCoord.Y == 1.0f) Vertices[index].TexCoord.Y = 0.0f;
            //        Indices[index] = index++;


            //        // Bottom Left
            //        Vertices[index] = HeightMap[indexBottomLeft];
            //        Indices[index] = index++;
            //        #endregion
            //    }
            //}
        }

        public override void Render(RenderContext renderContext)
        {
            /// --- check to render the model
            if (!this.IsRendering)
                return;

            //if (this.Geometry == null)
            //    return;

            if (this.Visibility != System.Windows.Visibility.Visible)
                return;

            if (renderContext.IsShadowPass)
                if (!this.IsThrowingShadow)
                    return;
            try
            {
                
                /// --- set constant paramerers             
                //var worldMatrix = this.modelMatrix * Matrix.Identity;
                //this.effectTransforms.mWorld.SetMatrix(ref worldMatrix);
                this.effectTransforms.mWorld.SetMatrix(this.modelMatrix);
                //var viewMatrix = renderContext.ViewMatrix;
                //var projectionMatrix = renderContext.ProjectrionMatrix;

                //this.effectTransforms.mView.SetMatrix(ref viewMatrix);
                //this.effectTransforms.mProjection.SetMatrix(ref projectionMatrix);
                //this.effectTransforms.vEyePos.Set(renderContext.Camera.Position.ToVector3());

                /// --- set context
                this.Device.ImmediateContext.InputAssembler.InputLayout = this.vertexLayout;
                this.Device.ImmediateContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
                this.Device.ImmediateContext.InputAssembler.SetIndexBuffer(this.indexBuffer, Format.R32_UInt, 0);
                //this.device.ImmediateContext.Rasterizer.State = rasterState;
                //this.device.ImmediateContext.OutputMerger.DepthStencilState = depthStencilState;

                //this.effectMaterial = new HelixToolkit.SharpDX.Wpf.MaterialGeometryModel3D.EffectMaterialVariables(this.effect);

                //var effectPass = this.technique.GetPassByIndex(0);
                    /// --- bind buffer                
                    this.Device.ImmediateContext.InputAssembler.SetVertexBuffers(0, vertexBufferBinding);
                        //this.OnRasterStateChanged(this.DepthBias);

                    //this.effectMaterial.texDiffuseMapVariable.SetResource(TextureResource);
                    //this.effectMaterial.bHasDiffuseMapVariable.Set(true);
                    this.effectPass.Apply(Device.ImmediateContext);
                        this.Device.ImmediateContext.DrawIndexed(IndexCount,0, 0);
                    

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Attach error :" + e.Message);
            }
        }

        internal void CopyVertexArray(List<DefaultVertex> VertexList)
        {
            VertexList.AddRange(Vertices);
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Dispose()
        {
            this.Detach();
        }

        
    }
}
