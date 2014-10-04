using HelixToolkit.Wpf.SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Jp3DKit
{
    public class JpCompositeModel3D : CompositeModel3D, IMouseMoveHitable
    {
        public bool IsMouseMoveHitTestVisible
        {
            get { return (bool)GetValue(IsMouseMoveHitTestVisibleProperty); }
            set { SetValue(IsMouseMoveHitTestVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsMouseMoveHitTestVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsMouseMoveHitTestVisibleProperty =
            DependencyProperty.Register("IsMouseMoveHitTestVisible", typeof(bool), typeof(JpCompositeModel3D), new PropertyMetadata(true));

        public bool IsLighting
        {
            get { return (bool)GetValue(IsLightingProperty); }
            set { SetValue(IsLightingProperty, value); }
        }

        public static readonly DependencyProperty IsLightingProperty =
            DependencyProperty.Register("IsLighting", typeof(bool), typeof(JpCompositeModel3D), new PropertyMetadata(false));

        public event RoutedEventHandler MouseMoveOver3D
        {
            add { AddHandler(MouseMoveOver3DEvent, value); }
            remove { RemoveHandler(MouseMoveOver3DEvent, value); }
        }
        
        public static readonly RoutedEvent MouseMoveOver3DEvent =
            EventManager.RegisterRoutedEvent("MouseMoveOver3D", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(JpCompositeModel3D));

        public JpCompositeModel3D()
        {
            this.MouseMoveOver3D += OnMouseMoveOver3D;
            }

        ~JpCompositeModel3D()
        {
            this.MouseMoveOver3D -= OnMouseMoveOver3D;
        }
        public virtual void OnMouseMoveOver3D(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public virtual bool MouseMoveHitTest(SharpDX.Ray ray, ref List<HitTestResult> hits)
        { return false; }
    }

    public class MouseMoveOver3DEventArgs : RoutedEventArgs
    {
        public JPViewport3DX Viewport { get; private set; }
        public bool InOut { get; private set; }
        public HitTestResult Hit {get;private set;}

        public MouseMoveOver3DEventArgs(object source, bool inOut,HitTestResult hit, JPViewport3DX viewport = null)
            : base(JpCompositeModel3D.MouseMoveOver3DEvent, source)
        {
            this.Viewport = viewport;
            this.InOut = inOut;
            this.Hit = hit;
        }
    }
}
