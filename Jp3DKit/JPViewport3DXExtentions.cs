using HelixToolkit.Wpf.SharpDX;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Jp3DKit
{
    public static class JPViewport3DXExtentions
    {
        /// <summary>
        /// 用于鼠标移动时查询对象
        /// 利用ILightable来提高效率
        /// </summary>
        /// <param name="viewport"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static IList<HitTestResult> MouseMoveFindHits(this JPViewport3DX viewport, System.Windows.Point position)
        {
            var camera = viewport.Camera as ProjectionCamera;
            if (camera == null)
            {
                return null;
            }

            var ray =ViewportExtensions.UnProject(viewport, new Vector2((float)position.X, (float)position.Y));
            var hits = new List<HitTestResult>();

            foreach (var element in viewport.Items)
            {
                var model = element as IMouseMoveHitable;
                if (model != null)
                {
                    model.MouseMoveHitTest(ray, ref hits);
                }
            }

            return hits.OrderBy(k => k.Distance).ToList();
        }

       
    }
}
