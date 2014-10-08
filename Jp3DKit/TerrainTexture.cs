using HelixToolkit.Wpf.SharpDX;
using HelixToolkit.Wpf.SharpDX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jp3DKit
{
    /// <summary>
    /// A terrain texture base class.
    /// Modify form helixtoolkit.wpf
    /// </summary>
    public abstract class TerrainTexture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref = "TerrainTexture" /> class.
        /// </summary>
        public TerrainTexture()
        {
            this.Material = PhongMaterials.Green; // Materials.Green;
        }

        /// <summary>
        /// Gets or sets the material.
        /// </summary>
        /// <value>The material.</value>
        public Material Material { get; set; }

        /// <summary>
        /// Gets or sets the texture coordinates.
        /// </summary>
        /// <value>The texture coordinates.</value>
        public Vector2Collection TextureCoordinates { get; set; }

        /// <summary>
        /// Calculates the texture of the specified model.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <param name="mesh">
        /// The mesh.
        /// </param>
        public virtual void Calculate(TerrainModel model, MeshGeometry3D mesh)
        {
        }

    }
}
