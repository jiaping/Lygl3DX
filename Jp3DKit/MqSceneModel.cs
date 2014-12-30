using HelixToolkit.Wpf.SharpDX;
using Jp3DKit.ObjModel;
using SharpDX;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Markup;
using System.Windows;
using System.Collections.ObjectModel;
using System.Linq;

namespace Jp3DKit.TerrainModels
{
    /// <summary>
    /// 所有墓区场景对象管理器
    /// </summary>
    public class MqSceneModel : Element3D,IHitable,IMouseMoveHitable
    {
        private readonly Collection<JpMqModel3D> children;
        private readonly Dictionary<string, JpMqModel3D> mqs;

        private LineGeometryModel3D seletedMqLine;

        public MqSceneModel()
        {
            this.children = new Collection<JpMqModel3D>();
            this.mqs = new Dictionary<string, JpMqModel3D>();
            this.seletedMqLine= new LineGeometryModel3D() { Thickness = 0.5, Color = new SharpDX.Color(0f, 0f, 1f, 0.05f), Opacity = 0.05f };

            //this.children.CollectionChanged += this.ChildrenChanged;
            this.IsMouseMoveHitTestVisible = true;
        }

   

        /// <summary>
        ///     Gets the children.
        /// </summary>
        /// <value>
        ///     The children.
        /// </value>
        public Collection<JpMqModel3D> Children { get { return this.children; } }
        /// <summary>
        /// 添加墓区
        /// </summary>
        /// <param name="mqTag"></param>
        /// <param name="model"></param>
        public void AddChild(string mqTag,JpMqModel3D model)
        {
            this.mqs.Add(mqTag, model);
            this.children.Add(model);
        }

        /// <summary>
        /// 修改墓区
        /// </summary>
        /// <param name="mqTag"></param>
        //public void ModifyMq(JPViewport3DX vp, System.Windows.Point pos)
        //{
        //    Ray ray = ViewportExtensions.UnProject(vp, pos.ToVector2());
        //    var hits = new List<HitTestResult>();
        //    foreach (var c in children)
        //    {
        //        c.HitTest(ray, ref hits);
        //    }
        //    if (hits.Count > 0)
        //    {
        //        (hits[0].ModelHit as JpMqModel3D).IsModify = true;
        //    }
        //}

        public JpMqModel3D LocateMq(JPViewport3DX vp, System.Windows.Point pos)
        {
            Ray ray = ViewportExtensions.UnProject(vp, pos.ToVector2());
            var hits = new List<HitTestResult>();
            foreach (var c in children)
            {
                c.HitTest(ray, ref hits);
            }
            if (hits.Count > 0)
            {
                //(hits[0].ModelHit as JpMqModel3D).IsModify = true;
                JpMqModel3D mq = (hits[0].ModelHit as JpMqModel3D);
               //Vector3[] tempPoints= new Vector3[mq.Positions.Count()];
               // mq.Positions.CopyTo(tempPoints, 0);
               // return tempPoints;
                return mq;
            }
            return null;
        }
        /// <summary>
        /// Handles changes in the Children collection.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.
        /// </param>
        //private void ChildrenChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    switch (e.Action)
        //    {
        //        case NotifyCollectionChangedAction.Remove:
        //        case NotifyCollectionChangedAction.Replace:
        //            foreach (Model3D item in e.OldItems)
        //            {
        //                // todo: detach?
        //                // yes, always
        //                item.Detach();
        //                if (item.Parent == this)
        //                {
        //                    this.RemoveLogicalChild(item);
        //                }
        //            }

        //            break;
        //    }

        //    switch (e.Action)
        //    {
        //        case NotifyCollectionChangedAction.Add:
        //        case NotifyCollectionChangedAction.Replace:
        //            foreach (Model3D item in e.NewItems)
        //            {
        //                if (this.IsAttached)
        //                {
        //                    // todo: attach?
        //                    // yes, always  
        //                    // where to get a refrence to renderHost?
        //                    // store it as private memeber of the class?
        //                    if (item.Parent == null)
        //                    {
        //                        this.AddLogicalChild(item);
        //                    }

        //                    item.Attach(this.renderHost);
        //                }
        //            }

        //            break;
        //    }

        //}
        /// <summary>
        /// Attaches the specified host.
        /// </summary>
        /// <param name="host">
        /// The host.
        /// </param>
        public override void Attach(IRenderHost host)
        {
            base.Attach(host);
            foreach (var model in this.Children)
            {
                if (model.Parent == null)
                {
                    this.AddLogicalChild(model);
                }

                model.Attach(host);
            }
        }

        /// <summary>
        ///     Detaches this instance.
        /// </summary>
        public override void Detach()
        {
            foreach (var model in this.Children)
            {
                model.Detach();
                if (model.Parent == this)
                {
                    this.RemoveLogicalChild(model);
                }
            }
            base.Detach();
        }
        public override void Dispose()
        {
            this.Detach();
        }
        /// <summary>
        /// Renders the specified context.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public override void Render(RenderContext context)
        {
            base.Render(context);

            // you mean like this?
            foreach (var c in this.Children)
            {
                var model = c as ITransformable;
                if (model != null)
                {
                    // push matrix                    
                   // model.PushMatrix(this.modelMatrix);
                    // render model
                    c.Render(context);
                    // pop matrix                   
                   // model.PopMatrix();
                }
                else
                {
                    c.Render(context);
                }
            }
        }

        #region IHitable 成员
        /// <summary>
        /// Compute hit-testing for all children
        /// </summary>
        public bool HitTest(Ray ray, ref List<HitTestResult> hits)
        {
            //bool hit = base.HitTest(ray, ref hits);
            bool hit = false;
            foreach (var c in this.Children)
            {
                var hc = c as IHitable;
                if (hc != null)
                {
                    var tc = c as ITransformable;
                    if (tc != null)
                    {
                        //tc.PushMatrix(this.modelMatrix);
                        if (hc.HitTest(ray, ref hits))
                        {
                            hit = true;
                        }
                       // tc.PopMatrix();
                    }
                    else
                    {
                        if (hc.HitTest(ray, ref hits))
                        {
                            hit = true;
                        }
                    }
                }
            }
            return hit;
        }    

        #endregion

        #region IMouseMoveHitable 成员

        public bool MouseMoveHitTest(Ray ray, ref List<HitTestResult> hits)
        {
            if (this.Visibility == Visibility.Collapsed)
            {
                return false;
            }
            if (this.IsMouseMoveHitTestVisible == false)
            {
                return false;
            }
            var h = false;
            foreach (var c in children)
            {
                    var hc = c as IMouseMoveHitable;
                    if (hc != null)
                    {
                            if (hc.MouseMoveHitTest(ray, ref hits))
                            {
                                h = true;
                            }
                    }
            }
            return h;
        }

        public bool IsMouseMoveHitTestVisible
        {
            get;
            set;
        }

        public bool IsLighting
        {
            get;
            set;
        }

        #endregion
    }
}
