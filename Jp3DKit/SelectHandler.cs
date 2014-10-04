using HelixToolkit.SharpDX.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace Jp3DKit
{
    internal class SelectHandler : OperateHandler
    {
        public SelectHandler(JPViewport3DX view)
            : base(view)
        {
        }

        public override void Completed(ManipulationEventArgs e)
        {
            base.Completed(e);
            var hits =this.Viewport.FindHits(e.CurrentPosition);
            //if (hits.Count > 0)
            //{
            //    Mouse.Capture(this, CaptureMode.SubTree);
            //    foreach (var hit in hits.Where(x => x.IsValid))
            //    {
            //        hit.ModelHit.RaiseEvent(new MouseDown3DEventArgs(hit.ModelHit, hit, e.GetPosition(this), this));
            //        this.mouseHitModels.Add(hit);

            //        // the winner takes it all: only the nearest hit is taken!
            //        break;
            //    }
            //}        
        }
    
    }
}
