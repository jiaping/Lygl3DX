using Jp3DKit;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Media3D;


    /// <summary>
    /// A visual element that shows a model loaded from a file.
    /// </summary>
    /// <remarks>
    /// Supported file formats are: .3ds .obj .lwo .stl .off
    /// </remarks>
    
[Obsolete("请使用基于sharpdx的类",true)]
    public class JpModelVisual3D : UIElement3D
    {
       /// <summary>
       /// 设置content model 的源
       /// </summary>
        public Stream ModelSource
        {
            get { return (Stream)GetValue(ModelSourceProperty); }
            set { SetValue(ModelSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ModelSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModelSourceProperty =
            DependencyProperty.Register("ModelSource", typeof(Stream), typeof(JpModelVisual3D), new UIPropertyMetadata(null, ModelSourceChanged));

        private static void ModelSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((JpModelVisual3D)d).OnModelSourceChanged();
        }

        public virtual void OnModelSourceChanged()
        {
            this.Visual3DModel = JpModelStream.Visual3DModel;
            this.OnModelLoaded();
        }

        
        public Model3D Content
        {
            get { return this.Visual3DModel; }
            set { this.Visual3DModel = JpModelStream.Visual3DModel; }
        }

        /// <summary>
        /// The model loaded event.
        /// </summary>
        private static readonly RoutedEvent ModelLoadedEvent = EventManager.RegisterRoutedEvent(
            "ModelLoaded", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(JpModelVisual3D));

        /// <summary>
        /// Occurs when the model has been loaded.
        /// </summary>
        public event RoutedEventHandler ModelLoaded
        {
            add
            {
                this.AddHandler(ModelLoadedEvent, value);
            }

            remove
            {
                this.RemoveHandler(ModelLoadedEvent, value);
            }
        }

        /// <summary>
        /// Called when the model has been loaded.
        /// </summary>
        protected virtual void OnModelLoaded()
        {
            var args = new RoutedEventArgs { RoutedEvent = ModelLoadedEvent };
            this.RaiseEvent(args);
        }
    }
