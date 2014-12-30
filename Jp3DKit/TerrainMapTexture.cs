using HelixToolkit.Wpf.SharpDX;
using HelixToolkit.Wpf.SharpDX.Core;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jp3DKit
{
    /// <summary>
    /// Terrain texture using a bitmap. Set the Left,Right,Bottom and Top coordinates to get the right alignment.
    /// modify from helixtoolkit.wpf.MapTexture
    /// </summary>
    public class MapTexture : TerrainTexture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapTexture"/> class.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        public MapTexture(string source)
        {
            this.Material = PhongMaterials.Green;//.MaterialHelper.CreateImageMaterial(source, 1);
        }

        /// <summary>
        /// Gets or sets the bottom.
        /// </summary>
        /// <value>The bottom.</value>
        public float Bottom { get; set; }

        /// <summary>
        /// Gets or sets the left.
        /// </summary>
        /// <value>The left.</value>
        public float Left { get; set; }

        /// <summary>
        /// Gets or sets the right.
        /// </summary>
        /// <value>The right.</value>
        public float Right { get; set; }

        /// <summary>
        /// Gets or sets the top.
        /// </summary>
        /// <value>The top.</value>
        public float Top { get; set; }

        /// <summary>
        /// Calculates the texture of the specified model.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <param name="mesh">
        /// The mesh.
        /// </param>
        public override void Calculate(TerrainModel1 model, MeshGeometry3D mesh)
        {
            var texcoords = new Vector2Collection();
            foreach (var p in mesh.Positions)
            {
                float x = p.X + model.Offset.X;
                float y = p.Y + model.Offset.Y;
                float u = (x - this.Left) / (this.Right - this.Left);
                float v = (y - this.Top) / (this.Bottom - this.Top);
                texcoords.Add(new Vector2(u, v));
            }

            this.TextureCoordinates = texcoords;
        }

    }
}
