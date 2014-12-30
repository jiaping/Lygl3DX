using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

using global::SharpDX;
using Color = global::SharpDX.Color4;
using Int32Collection = System.Collections.Generic.List<int>;
using Object3DGroup = System.Collections.Generic.List<HelixToolkit.Wpf.SharpDX.Object3D>;
using Plane3D = global::SharpDX.Plane;
using Point = global::SharpDX.Vector2;
using Point3D = global::SharpDX.Vector3;
using Point3DCollection = System.Collections.Generic.List<global::SharpDX.Vector3>;
using PointCollection = System.Collections.Generic.List<global::SharpDX.Vector2>;
using Ray3D = global::SharpDX.Ray;
using Vector3D = global::SharpDX.Vector3;
using HelixToolkit.Wpf.SharpDX;
using SharpDX.Direct3D11;

namespace Jp3DKit.ObjModel
{
    // --------------------------------------------------------------------------------------------------------------------
    // <copyright file="ObjReader.cs" company="Helix Toolkit">
    //   Copyright (c) 2014 Helix Toolkit contributors
    // </copyright>
    // <summary>
    //   A Wavefront .obj file reader.
    // </summary>
    // --------------------------------------------------------------------------------------------------------------------

        //public class Object3D
        //{
        //    public Geometry3D Geometry { get; set; }
        //    public Material Material { get; set; }
        //    public Matrix Transform { get; set; }
        //    public string Name { get; set; }
        //}
        ///本文件修改自helixtool.wps.objreader
        /// <summary>
        /// A Wavefront .obj file reader.
        /// </summary>
        /// <remarks>
        /// See the file format specifications at
        /// http://en.wikipedia.org/wiki/Obj
        /// http://en.wikipedia.org/wiki/Material_Template_Library
        /// http://www.martinreddy.net/gfx/3d/OBJ.spec
        /// http://www.eg-models.de/formats/Format_Obj.html
        /// </remarks>
        
    public struct MaterialAttributeRange
    {

        public string MaterialName;

        public int FaceStart;

        public int FaceCount;

        public int VertexStart;

        public int VertexCount;

    }
    public class MaterialDefinition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialDefinition"/> class.
        /// </summary>
        public MaterialDefinition()
        {
            this.Dissolved = 1.0;
        }

        /// <summary>
        /// Gets or sets the alpha map.
        /// </summary>
        /// <value>The alpha map.</value>
        public string AlphaMap { get; set; }

        /// <summary>
        /// Gets or sets the ambient color.
        /// </summary>
        /// <value>The ambient.</value>
        public Color Ambient { get; set; }

        /// <summary>
        /// Gets or sets the ambient map.
        /// </summary>
        /// <value>The ambient map.</value>
        public string AmbientMap { get; set; }

        /// <summary>
        /// Gets or sets the bump map.
        /// </summary>
        /// <value>The bump map.</value>
        public string BumpMap { get; set; }

        /// <summary>
        /// Gets or sets the diffuse color.
        /// </summary>
        /// <value>The diffuse.</value>
        public Color Diffuse { get; set; }

        /// <summary>
        /// Gets or sets the diffuse map.
        /// </summary>
        /// <value>The diffuse map.</value>
        public string DiffuseMap { get; set; }

        /// <summary>
        /// Gets or sets the opacity value.
        /// </summary>
        /// <value>The opacity.</value>
        /// <remarks>
        /// 0.0 is transparent, 1.0 is opaque.
        /// </remarks>
        public double Dissolved { get; set; }

        /// <summary>
        /// Gets or sets the illumination.
        /// </summary>
        /// <value>The illumination.</value>
        public int Illumination { get; set; }

        /// <summary>
        /// Gets or sets the specular color.
        /// </summary>
        /// <value>The specular color.</value>
        public Color Specular { get; set; }

        /// <summary>
        /// Gets or sets the specular coefficient.
        /// </summary>
        /// <value>The specular coefficient.</value>
        public double SpecularCoefficient { get; set; }

        /// <summary>
        /// Gets or sets the specular map.
        /// </summary>
        /// <value>The specular map.</value>
        public string SpecularMap { get; set; }

        /// <summary>
        /// Gets or sets the material.
        /// </summary>
        /// <value>The material.</value>
        public Material Material { get; set; }

        /// <summary>
        /// Gets the material from the specified path.
        /// </summary>
        /// <param name="texturePath">
        /// The texture path.
        /// </param>
        /// <returns>
        /// The material.
        /// </returns>
        public Material GetMaterial(string texturePath)
        {
            if (this.Material == null)
            {
                this.Material = this.CreateMaterial(texturePath);
                //this.Material.Freeze();
            }

            return this.Material;
        }

        /// <summary>
        /// Creates the material.
        /// </summary>
        /// <param name="texturePath">The texture path.</param>
        /// <returns>A WPF material.</returns>
        private Material CreateMaterial(string texturePath)
        {
            var mat = new PhongMaterial()
            {
                AmbientColor = this.Ambient,
                //AmbientMap = this.AmbientMap,

                DiffuseColor = this.Diffuse,
                DiffuseMap = (this.DiffuseMap == null) ? null : LoadImage(this.DiffuseMap),

                SpecularColor = this.Specular,
                SpecularShininess = (float)this.SpecularCoefficient,
                //SpecularMap = this.SpecularMap,

                NormalMap = (this.BumpMap == null) ? null : LoadImage(this.BumpMap),
                //Dissolved = this.Dissolved,
                //Illumination = this.Illumination,

            };

            //return mg.Children.Count != 1 ? mg : mg.Children[0];
            return mat;
        }


        private static BitmapImage LoadImage(string path)
        {
            string texturepath = AppDomain.CurrentDomain.BaseDirectory + @"3dmodel/" + path;

            var bmp = new BitmapImage(new Uri(texturepath, UriKind.RelativeOrAbsolute));
            return bmp;
        }

    }
   public struct ModelData
   {
       public List<SharpDX.Toolkit.Graphics.VertexPositionNormalTexture> ModelVertices;
       public List<int> ModelIndices;
       public Dictionary<string, MaterialDefinition> ModelMaterials;
       public List<MaterialAttributeRange> AttributeTable;
   }
    public class JpObjReader 
        {
            /// <summary>
            /// Initializes a new instance of the <see cref = "ObjReader" /> class.
            /// </summary>
            public JpObjReader()
            {
                this.SkipTransparencyValues = true;

                this.DefaultColor = global::SharpDX.Color.Gold;

                this.Points = new List<Point3D>();
                this.TextureCoordinates = new List<Point>();
                this.Normals = new List<Vector3D>();
                this.ModelMaterials = new Dictionary<string, MaterialDefinition>();
                this.CurMaterialName = string.Empty;
                
                ModelVertices = new List<SharpDX.Toolkit.Graphics.VertexPositionNormalTexture>();
                ModelIndices = new List<int>();
                ModelAttribute = new List<string>();
                //ModelPath = AppDomain.CurrentDomain.BaseDirectory + @"3DModel\";
            }


            /// <summary>
            /// Gets or sets the default color.
            /// </summary>
            /// <value>The default color.</value>
            /// <remarks>
            /// The default value is Colors.Gold.
            /// </remarks>
            public Color DefaultColor { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether to ignore errors.
            /// </summary>
            /// <value><c>true</c> if errors should be ignored; <c>false</c> if errors should throw an exception.</value>
            /// <remarks>
            /// The default value is on (true).
            /// </remarks>
            public bool IgnoreErrors { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether to skip transparency values ("Tr") in the material files.
            /// </summary>
            /// <value>
            /// <c>true</c> if transparency values should be skipped; otherwise, <c>false</c>.
            /// </value>
            /// <remarks>
            /// This option is added to allow disabling the "Tr" values in files where it has been defined incorrectly.
            /// The transparency values ("Tr") are interpreted as 0 = transparent, 1 = opaque.
            /// The dissolve values ("d") are interpreted as 0 = transparent, 1=opaque.
            /// </remarks>
            public bool SkipTransparencyValues { get; set; }

            public List<SharpDX.Toolkit.Graphics.VertexPositionNormalTexture> ModelVertices { get; private set; }
            public List<int> ModelIndices { get; private set; }
            public List<string> ModelAttribute { get; private set; }
            /// <summary>
            /// Gets the materials in the imported material files.
            /// </summary>
            /// <value>The materials.</value>
            public Dictionary<string, MaterialDefinition> ModelMaterials { get; private set; }
            private List<MaterialAttributeRange> AttributeTable;

            private string CurMaterialName;


            

            /// <summary>
            /// Gets or sets the path to the textures.
            /// </summary>
            /// <value>The texture path.</value>
            public string TexturePath { get; set; }

            /// <summary>
            /// Additional info how to treat the model
            /// </summary>
            public ModelInfo ModelInfo { get; private set; }

            /// <summary>
            /// Reads the model from the specified path.
            /// </summary>
            /// <param name="path">
            /// The path.
            /// </param>
            /// <returns>
            /// The model.
            /// </returns>
            public ModelData Read(string path, ModelInfo info = default(ModelInfo))
            {
                this.TexturePath = Path.GetDirectoryName(path);
                this.ModelInfo = info;

                using (var s = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    return this.Read(s);
                }
            }

            /// <summary>
            /// Reads the model from the specified stream.
            /// </summary>
            /// <param name="s">
            /// The stream.
            /// </param>
            /// <returns>
            /// The model.
            /// </returns>
            public ModelData Read(Stream s, ModelInfo info = default(ModelInfo))
            {
                using (this.Reader = new StreamReader(s))
                {
                    this.currentLineNo = 0;
                    while (!this.Reader.EndOfStream)
                    {
                        this.currentLineNo++;
                        var line = this.Reader.ReadLine();
                        if (line == null)
                        {
                            break;
                        }

                        line = line.Trim();
                        if (line.StartsWith("#") || line.Length == 0)
                        {
                            continue;
                        }

                        string keyword, values;
                        SplitLine(line, out keyword, out values);

                        try
                        {

                            switch (keyword.ToLower())
                            {
                                // Vertex data
                                case "v": // geometric vertices
                                    this.AddVertex(values);
                                    break;
                                case "vt": // texture vertices
                                    this.AddTexCoord(values);
                                    break;
                                case "vn": // vertex normals
                                    this.AddNormal(values);
                                    break;
                                case "vp": // parameter space vertices
                                case "cstype": // rational or non-rational forms of curve or surface type: basis matrix, Bezier, B-spline, Cardinal, Taylor
                                case "degree": // degree
                                case "bmat": // basis matrix
                                case "step": // step size
                                    // not supported
                                    break;

                                // Elements
                                case "f": // face
                                    this.AddFace(values);
                                    break;
                                case "p": // point
                                case "l": // line
                                case "curv": // curve
                                case "curv2": // 2D curve
                                case "surf": // surface
                                    // not supported
                                    break;

                                // Free-form curve/surface body statements
                                case "parm": // parameter name
                                case "trim": // outer trimming loop (trim)
                                case "hole": // inner trimming loop (hole)
                                case "scrv": // special curve (scrv)
                                case "sp":  // special point (sp)
                                case "end": // end statement (end)
                                    // not supported
                                    break;

                                // Connectivity between free-form surfaces
                                case "con": // connect
                                    // not supported
                                    break;

                                // Grouping
                                case "g": // group name
                                    break;
                                case "s": // smoothing group
                                    break;
                                case "mg": // merging group
                                    break;
                                case "o": // object name
                                    // not supported
                                    break;

                                // Display/render attributes
                                case "mtllib": // material library
                                    this.LoadMaterialLib(values);
                                    break;
                                case "usemtl": // material name
                                    this.SetMaterial(values);
                                    break;
                                case "usemap": // texture map name

                                    break;
                                case "bevel": // bevel interpolation
                                case "c_interp": // color interpolation
                                case "d_interp": // dissolve interpolation
                                case "lod": // level of detail
                                case "shadow_obj": // shadow casting
                                case "trace_obj": // ray tracing
                                case "ctech": // curve approximation technique
                                case "stech": // surface approximation technique
                                    // not supported
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Windows.MessageBox.Show(string.Format("Error loading object: {0}", ex.Message), "Error", MessageBoxButton.OKCancel);
                        }
                    }
                }
                SetupAttributeTable();

                return new ModelData() { ModelVertices = this.ModelVertices, ModelIndices = this.ModelIndices, ModelMaterials = this.ModelMaterials, AttributeTable = this.AttributeTable };

            }

            /// <summary>
            /// Reads a GZipStream compressed OBJ file.
            /// </summary>
            /// <param name="path">
            /// The path.
            /// </param>
            /// <returns>
            /// A Model3D object containing the model.
            /// </returns>
            /// <remarks>
            /// This is a file format used by Helix Toolkit only.
            /// Use the GZipHelper class to compress an .obj file.
            /// </remarks>
            public Object3DGroup ReadZ(string path)
            {
                this.TexturePath = Path.GetDirectoryName(path);
                using (var s = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var deflateStream = new GZipStream(s, CompressionMode.Decompress, true);
                    return null;// this.Read(deflateStream);
                }
            }

            /// <summary>
            /// The line number of the line being parsed.
            /// </summary>
            private int currentLineNo;

         

            /// <summary>
            /// Gets or sets the normals.
            /// </summary>
            private IList<Vector3D> Normals { get; set; }

            /// <summary>
            /// Gets or sets the points.
            /// </summary>
            private IList<Point3D> Points { get; set; }

            /// <summary>
            /// Gets or sets the stream reader.
            /// </summary>
            private StreamReader Reader { get; set; }

            /// <summary>
            /// Gets or sets the texture coordinates.
            /// </summary>
            private IList<Point> TextureCoordinates { get; set; }

            /// <summary>
            /// Parses a color string.
            /// </summary>
            /// <param name="values">
            /// The input.
            /// </param>
            /// <returns>
            /// The parsed color.
            /// </returns>
            private static Color ColorParse(string values)
            {
                var fields = Split(values);
                return System.Windows.Media.Color.FromRgb((byte)(fields[0] * 255), (byte)(fields[1] * 255), (byte)(fields[2] * 255)).ToColor4();
            }

            /// <summary>
            /// Parse a string containing a double value.
            /// </summary>
            /// <param name="input">
            /// The input string.
            /// </param>
            /// <returns>
            /// The value.
            /// </returns>
            private static double DoubleParse(string input)
            {
                return double.Parse(input, CultureInfo.InvariantCulture);
            }

            /// <summary>
            /// Splits the specified string using whitespace(input) as separators.
            /// </summary>
            /// <param name="input">
            /// The input string.
            /// </param>
            /// <returns>
            /// List of input.
            /// </returns>
            private static IList<double> Split(string input)
            {
                input = input.Trim();
                var fields = input.SplitOnWhitespace();
                var result = new double[fields.Length];
                for (int i = 0; i < fields.Length; i++)
                {
                    result[i] = DoubleParse(fields[i]);
                }

                return result;
            }

            /// <summary>
            /// Splits a line in keyword and arguments.
            /// </summary>
            /// <param name="line">
            /// The line.
            /// </param>
            /// <param name="keyword">
            /// The keyword.
            /// </param>
            /// <param name="arguments">
            /// The arguments.
            /// </param>
            private static void SplitLine(string line, out string keyword, out string arguments)
            {
                int idx = line.IndexOf(' ');
                if (idx < 0)
                {
                    keyword = line;
                    arguments = null;
                    return;
                }

                keyword = line.Substring(0, idx);
                arguments = line.Substring(idx + 1);
            }

            /// <summary>
            /// Adds a face.
            /// </summary>
            /// <param name="values">
            /// The input values.
            /// </param>
            /// <remarks>
            /// Adds a polygonal face. The numbers are indexes into the arrays of vertex positions,
            /// texture coordinates, and normals respectively. A number may be omitted if,
            /// for example, texture coordinates are not being defined in the model.
            /// There is no maximum number of vertices that a single polygon may contain.
            /// The .obj file specification says that each face must be flat and convex.
            /// </remarks>
            private void AddFace(string values)
            {

                SharpDX.Toolkit.Graphics.VertexPositionNormalTexture vertex;

                var fields = values.SplitOnWhitespace();
                var faceIndices = new List<int>();
                foreach (var field in fields)
                {
                    if (string.IsNullOrEmpty(field))
                    {
                        continue;
                    }

                    var ff = field.Split('/');
                    int vi = int.Parse(ff[0]);
                    int vti = ff.Length > 1 && ff[1].Length > 0 ? int.Parse(ff[1]) : -1;
                    int vni = ff.Length > 2 && ff[2].Length > 0 ? int.Parse(ff[2]) : -1;

                    // Handle relative indices (negative numbers)
                    if (vi < 0)
                    {
                        vi = this.Points.Count + vi;
                    }

                    if (vti < 0)
                    {
                        vti = this.TextureCoordinates.Count + vti;
                    }

                    if (vni < 0)
                    {
                        vni = this.Normals.Count + vni;
                    }

                    // Check if the indices are valid
                    if (vi - 1 >= this.Points.Count)
                    {
                        if (this.IgnoreErrors)
                        {
                            return;
                        }

                        throw new FileFormatException(string.Format("Invalid vertex index ({0}) on line {1}.", vi, this.currentLineNo));
                    }
                    
                    vertex.Position = this.Points[vi - 1];
                    vertex.TextureCoordinate = this.TextureCoordinates[vti - 1];
                    vertex.Normal = this.Normals[vni - 1];

                    ModelIndices.Add(ModelVertices.Count);
                    ModelVertices.Add(vertex);
                }
                ModelAttribute.Add(CurMaterialName);
            }

            /// <summary>
            /// Adds a normal.
            /// </summary>
            /// <param name="values">
            /// The input values.
            /// </param>
            private void AddNormal(string values)
            {
                var fields = Split(values);
                this.Normals.Add(new Vector3D((float)fields[0], (float)fields[1], (float)fields[2]));
            }

            /// <summary>
            /// Adds a texture coordinate.
            /// </summary>
            /// <param name="values">
            /// The input values.
            /// </param>
            private void AddTexCoord(string values)
            {
                var fields = Split(values);
                this.TextureCoordinates.Add(new Point((float)fields[0],  (float)fields[1]));
            }

            /// <summary>
            /// Adds a vertex.
            /// </summary>
            /// <param name="values">
            /// The input values.
            /// </param>
            private void AddVertex(string values)
            {
                var fields = Split(values);
                this.Points.Add(new Point3D((float)fields[0], (float)fields[1], (float)fields[2]));
            }

            /// <summary>
            /// Builds the model.
            /// </summary>
            /// <returns>
            /// A Model3D object.
            /// </returns>
            private Object3DGroup BuildModel()
            {
                var modelGroup = new Object3DGroup();
                //foreach (var g in this.Groups)
                //{
                //    foreach (var gm in g.CreateModels(this.ModelInfo))
                //    {
                //        modelGroup.Add(gm);
                //    }
                //}

                return modelGroup;
            }

            /// <summary>
            /// Gets the material with the specified name.
            /// </summary>
            /// <param name="materialName">
            /// The material name.
            /// </param>
            /// <returns>
            /// The material.
            /// </returns>
            private Material GetMaterial(string materialName)
            {
                MaterialDefinition mat;
                if (!string.IsNullOrEmpty(materialName) && this.ModelMaterials.TryGetValue(materialName, out mat))
                {
                    return mat.GetMaterial(this.TexturePath);
                }

                return PhongMaterials.DefaultVRML;// MaterialHelper.CreateMaterial(new SolidColorBrush(this.DefaultColor));
            }

            /// <summary>
            /// Loads a material library.
            /// </summary>
            /// <param name="mtlFile">
            /// The mtl file.
            /// </param>
            private void LoadMaterialLib(string mtlFile)
            {
                var path = Path.Combine(this.TexturePath, mtlFile);
                if (!File.Exists(path))
                {
                    return;
                }

                using (var mreader = new StreamReader(path))
                {
                    MaterialDefinition currentMaterial = null;

                    while (!mreader.EndOfStream)
                    {
                        var line = mreader.ReadLine();
                        if (line == null)
                        {
                            break;
                        }

                        line = line.Trim();

                        if (line.StartsWith("#") || line.Length == 0)
                        {
                            continue;
                        }

                        string keyword, value;
                        SplitLine(line, out keyword, out value);

                        switch (keyword.ToLower())
                        {
                            case "newmtl":
                                if (value != null)
                                {
                                    currentMaterial = new MaterialDefinition();
                                    this.ModelMaterials.Add(value, currentMaterial);
                                }

                                break;
                            case "ka":
                                if (currentMaterial != null && value != null)
                                {
                                    currentMaterial.Ambient = ColorParse(value);
                                }

                                break;
                            case "kd":
                                if (currentMaterial != null && value != null)
                                {
                                    currentMaterial.Diffuse = ColorParse(value);
                                }

                                break;
                            case "ks":
                                if (currentMaterial != null && value != null)
                                {
                                    currentMaterial.Specular = ColorParse(value);
                                }

                                break;
                            case "ns":
                                if (currentMaterial != null && value != null)
                                {
                                    currentMaterial.SpecularCoefficient = DoubleParse(value);
                                }

                                break;
                            case "d":
                                if (currentMaterial != null && value != null)
                                {
                                    currentMaterial.Dissolved = DoubleParse(value);
                                }

                                break;
                            case "tr":
                                if (!this.SkipTransparencyValues && currentMaterial != null && value != null)
                                {
                                    currentMaterial.Dissolved = DoubleParse(value);
                                }

                                break;
                            case "illum":
                                if (currentMaterial != null && value != null)
                                {
                                    currentMaterial.Illumination = int.Parse(value);
                                }

                                break;
                            case "map_ka":
                                if (currentMaterial != null)
                                {
                                    currentMaterial.AmbientMap = value;
                                }

                                break;
                            case "map_kd":
                                if (currentMaterial != null)
                                {
                                    currentMaterial.DiffuseMap = value;
                                }

                                break;
                            case "map_ks":
                                if (currentMaterial != null)
                                {
                                    currentMaterial.SpecularMap = value;
                                }

                                break;
                            case "map_d":
                                if (currentMaterial != null)
                                {
                                    currentMaterial.AlphaMap = value;
                                }

                                break;
                            case "map_bump":
                            case "bump":
                                if (currentMaterial != null)
                                {
                                    currentMaterial.BumpMap = value;
                                }

                                break;
                        }
                    }
                }
                foreach (var item in ModelMaterials)
                {
                    item.Value.Material = item.Value.GetMaterial(this.TexturePath);
                }
            }

            /// <summary>
            /// Sets the material for the current group.
            /// </summary>
            /// <param name="materialName">
            /// The material name.
            /// </param>
            private void SetMaterial(string materialName)
            {

                //if (!ModelMaterials.ContainsKey(materialName))
                //{
                //    MaterialDefinition md = new MaterialDefinition();
                //    md.Material = md.GetMaterial(materialName);
                //    ModelMaterials.Add(materialName, new MaterialDefinition());
                //}
                CurMaterialName = materialName;
            }


            private void SetupAttributeTable()
            {
                AttributeTable = new List<MaterialAttributeRange>();
                string materailName = ModelAttribute[0];
                int start = 0;
                int count = 0;
                int i = 0;
                foreach (var item in ModelAttribute)
                {
                    if (item != materailName)
                    {
                        AttributeTable.Add(new MaterialAttributeRange() { MaterialName = materailName, FaceCount = count, FaceStart = start });
                        materailName = item;
                        start = i;
                        count = 1;
                    }
                    else count++;
                    ++i;
                }
                AttributeTable.Add(new MaterialAttributeRange() { MaterialName = materailName, FaceCount = count, FaceStart = start });
                #region 合并相同资源
                List<MaterialAttributeRange> newAttributeTabl = new List<MaterialAttributeRange>();
                List<int> newIndices = new List<int>();
                    
                foreach (var item in ModelMaterials)
                {
                    //int index= GetAttributeTableItemIndex(newAttributeTabl,AttributeTable[j].AttribId);
                    MaterialAttributeRange rage = new MaterialAttributeRange();
                    rage.MaterialName = item.Key;
                    rage.FaceStart = newIndices.Count / 3;
                    rage.FaceCount = 0;
                    foreach (var item1 in AttributeTable)
                    {
                        if (item1.MaterialName == item.Key)
                        {
                            rage.FaceCount += item1.FaceCount;
                            newIndices.AddRange(this.ModelIndices.GetRange(item1.FaceStart * 3, item1.FaceCount * 3));
                        }
                    }
                    newAttributeTabl.Add(rage);
                }
                this.ModelIndices = newIndices;
                AttributeTable = newAttributeTabl;
                #endregion
            }

        }
}
