using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Jp3DKit
{
    public class SelectRectangleHandler:MouseGestureHandler
    {

         /// <summary>
        /// The zoom rectangle.
        /// </summary>
        private Rect zoomRectangle;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZoomRectangleHandler"/> class.
        /// </summary>
        /// <param name="controller">
        /// The controller.
        /// </param>
        public SelectRectangleHandler(CameraController controller)
            : base(controller)
        {
        }

        /// <summary>
        /// Occurs when the manipulation is completed.
        /// </summary>
        /// <param name="e">The <see cref="ManipulationEventArgs"/> instance containing the event data.</param>
        public override void Completed(ManipulationEventArgs e)
        {
            base.Completed(e);
            this.Controller.HideRectangle();
            //this.ZoomRectangle(this.zoomRectangle);
            //获取选择矩形rectangl
            //IList<Viewport3DHelper.HitResult> listResult= FindHits(this.Viewport, this.zoomRectangle);
            IList<JpModelVisual3D> SelectedList = FindHitsRect(this.Viewport, this.zoomRectangle);
            RectangleVisual3D rect3d = new RectangleVisual3D();
            double rwidth = this.zoomRectangle.Right-this.zoomRectangle.Left;
            double rlength= this.zoomRectangle.Bottom-this.zoomRectangle.Top;
            Point origin =new Point( this.zoomRectangle.Right+rwidth/2,this.zoomRectangle.Bottom+rlength/2);
            Point3D pointNeare, pointFar;
            Viewport3DHelper.Point2DtoPoint3D(this.Viewport, origin,out  pointNeare,out pointFar);
            Transform t = VisualTreeHelper.GetTransform(this.Viewport);
            var tt = rect3d.TransformToAncestor(this.Viewport as Viewport3D);

            rect3d.Origin = new Point3D(0,0,0);
            rect3d.Width = rwidth;
            rect3d.Length = rlength;
            rect3d.Normal = this.CameraLookDirection;
            

          //  rect3d.Transform = t as Transform3D;// 
            this.Viewport.Children.Add(rect3d);
            
        }

        private IList<JpModelVisual3D> FindHitsRect(Viewport3D viewport3D, Rect rect)
        {
            List<JpModelVisual3D> resultList = new List<JpModelVisual3D>();
            foreach (var item in viewport3D.Children)
            {
                if (item.GetType() == typeof(JpModelVisual3D))
                {
                    var visual = item as JpModelVisual3D;
                    
                }
                    resultList.Add(item as JpModelVisual3D);
            }
            return resultList;
        }

        /// <summary>
        /// Occurs when the position is changed during a manipulation.
        /// </summary>
        /// <param name="e">The <see cref="ManipulationEventArgs"/> instance containing the event data.</param>
        public override void Delta(ManipulationEventArgs e)
        {
            base.Delta(e);

            double ar = this.Controller.ActualHeight / this.Controller.ActualWidth;
            var delta = this.MouseDownPoint - e.CurrentPosition;

            if (Math.Abs(delta.Y / delta.X) < ar)
            {
                delta.Y = Math.Sign(delta.Y) * Math.Abs(delta.X * ar);
            }
            else
            {
                delta.X = Math.Sign(delta.X) * Math.Abs(delta.Y / ar);
            }

            //this.zoomRectangle = new Rect(this.MouseDownPoint, this.MouseDownPoint - delta);
            this.zoomRectangle = new Rect(this.MouseDownPoint, e.CurrentPosition);

            this.Controller.UpdateRectangle(this.zoomRectangle);
        }

        /// <summary>
        /// Occurs when the manipulation is started.
        /// </summary>
        /// <param name="e">The <see cref="ManipulationEventArgs"/> instance containing the event data.</param>
        public override void Started(ManipulationEventArgs e)
        {
            base.Started(e);
            this.zoomRectangle = new Rect(this.MouseDownPoint, this.MouseDownPoint);
            this.Controller.ShowRectangle(this.zoomRectangle, Colors.LightGray, Colors.Black);
        }

        /// <summary>
        /// Zooms to the specified rectangle.
        /// </summary>
        /// <param name="rectangle">
        /// The zoom rectangle.
        /// </param>
        public void ZoomRectangle(Rect rectangle)
        {
            if (!this.Controller.IsZoomEnabled)
            {
                return;
            }

            if (rectangle.Width < 10 || rectangle.Height < 10)
            {
                return;
            }

            CameraHelper.ZoomToRectangle(this.Camera, this.Viewport, rectangle);
            //this.Controller.OnZoomedByRectangle();
        }

        /// <summary>
        /// Occurs when the command associated with this handler initiates a check to determine whether the command can be executed on the command target.
        /// </summary>
        /// <returns>True if the execution can continue.</returns>
        protected override bool CanExecute()
        {
            return this.Controller.IsZoomEnabled;
        }

        /// <summary>
        /// Gets the cursor for the gesture.
        /// </summary>
        /// <returns>A cursor.</returns>
        protected override Cursor GetCursor()
        {
            return this.Controller.ZoomRectangleCursor;
        }

        /// <summary>
        /// Finds the hits for a given 2D viewport position.
        /// </summary>
        /// <param name="viewport">
        /// The viewport.
        /// </param>
        /// <param name="position">
        /// The position.
        /// </param>
        /// <returns>
        /// List of hits, sorted with the nearest hit first.
        /// </returns>
        public static IList<Viewport3DHelper.HitResult> FindHits(Viewport3D viewport, Rect rect)
        {
            var result = new List<Viewport3DHelper.HitResult>();
            HitTestResultCallback callback = hit =>
            {
                var rayHit = hit as RayMeshGeometry3DHitTestResult;
                if (rayHit != null)
                {
                    if (rayHit.MeshHit != null)
                    {
                        //var p = GetGlobalHitPosition(rayHit, viewport);
                        //var nn = GetNormalHit(rayHit);
                        //var n = nn.HasValue ? nn.Value : new Vector3D(0, 0, 1);
                        

                        result.Add(
                            new HelixToolkit.Wpf.Viewport3DHelper.HitResult
                            {
                               // Distance = (camera.Position - p).Length,
                                RayHit = rayHit
                                //,
                                //Normal = n,
                                //Position = p
                            });
                    }
                }

                return HitTestResultBehavior.Continue;
            };
            HitTestFilterCallback filterCallBack = o =>
                {
                    if (o.GetType() == typeof(MeshGeometry3D))
                    {
                        return HitTestFilterBehavior.Stop;
                    }
                    else
                    {
                        return HitTestFilterBehavior.Continue;
                    }
                };

            Point position = new Point(rect.X, rect.Y);
            var hitParams = new PointHitTestParameters(position);
            //var hitParams = new GeometryHitTestParameters(new RectangleGeometry(rect));
            VisualTreeHelper.HitTest(viewport, filterCallBack, callback, hitParams);

             return result.OrderBy(k => k.Distance).ToList();
        }
        /// <summary>
        /// Gets the hit position transformed to global (viewport) coordinates.
        /// </summary>
        /// <param name="rayHit">
        /// The hit structure.
        /// </param>
        /// <param name="viewport">
        /// The viewport.
        /// </param>
        /// <returns>
        /// The 3D position of the hit.
        /// </returns>
        //private static Point3D GetGlobalHitPosition(RayMeshGeometry3DHitTestResult rayHit, Viewport3D viewport)
        //{
        //    var p = rayHit.PointHit;

        //    // first transform the Model3D hierarchy
        //    var t2 = GetTransform(rayHit.VisualHit, rayHit.ModelHit);
        //    if (t2 != null)
        //    {
        //        p = t2.Transform(p);
        //    }

        //    // then transform the Visual3D hierarchy up to the Viewport3D ancestor
        //    var t = GetTransform(viewport, rayHit.VisualHit);
        //    if (t != null)
        //    {
        //        p = t.Transform(p);
        //    }

        //    return p;
        //}

        /// <summary>
        /// Gets the normal for a hit test result.
        /// </summary>
        /// <param name="rayHit">
        /// The ray hit.
        /// </param>
        /// <returns>
        /// The normal.
        /// </returns>
        private static Vector3D? GetNormalHit(RayMeshGeometry3DHitTestResult rayHit)
        {
            if ((rayHit.MeshHit.Normals == null) || (rayHit.MeshHit.Normals.Count < 1))
            {
                return null;
            }

            return rayHit.MeshHit.Normals[rayHit.VertexIndex1] * rayHit.VertexWeight1
                   + rayHit.MeshHit.Normals[rayHit.VertexIndex2] * rayHit.VertexWeight2
                   + rayHit.MeshHit.Normals[rayHit.VertexIndex3] * rayHit.VertexWeight3;
        }
    }
}
