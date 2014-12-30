using System.Linq;
using System.Windows;
using HelixToolkit.Wpf.SharpDX;



using MatrixTransform3D = System.Windows.Media.Media3D.MatrixTransform3D;
using Matrix = SharpDX.Matrix;

using System.Windows.Input;
using System.Collections.Generic;
using SharpDX;
using Ray = SharpDX.Ray;
using DraggableGeometryModel3D = Jp3DKit.MouseDrawHandler.DraggableGeometryModel3D;

namespace Jp3DKit
{
    public sealed class InteractionHandle3D : GroupModel3D, IHitable, ISelectable
    {
        private Vector3[] positions;

        public void AddControlsHandles(JpMqModel3D mq)
        {
            this.Owner = mq;
            Vector3[] tempPoints = new Vector3[mq.Positions.Count()];
            mq.Positions.CopyTo(tempPoints, 0);
            this.Children.Clear();

            this.positions = tempPoints;
            cornerHandles = new DraggableGeometryModel3D[this.positions.Length];
           
            for (int i = 0; i < this.positions.Length; i++)
            {
                CreateCornerHandle(i,this.positions[i]);       
            }
            CreateCenterHandle(this.positions);
        }

        private void CreateCornerHandle(int index,Vector3 vv )
        {
            var translate = Matrix3DExtensions.Translate3D(vv.ToVector3D());
            this.cornerHandles[index] = new DraggableGeometryModel3D()
            {
                DragZ = true,
                Visibility = Visibility.Visible,
                Material = this.Material,
                Geometry = NodeGeometry,
                Transform = new MatrixTransform3D(translate),
            };
            this.cornerHandles[index].MouseMove3D += OnNodeMouse3DMove;
            this.cornerHandles[index].MouseUp3D += OnNodeMouse3DUp;
            this.cornerHandles[index].MouseDown3D += OnNodeMouse3DDown;

            this.Children.Add(cornerHandles[index]);
        }

        private void CreateCenterHandle(Vector3[] points)
        {
            var bb = GetBoundsCenter(points);
            System.Console.WriteLine(bb);
            var b4 = new MeshBuilder();
            b4.AddBox(new Vector3(0, 0, 0), 0.175, 0.175, 0.175);
            var BoxGeometry = b4.ToMeshGeometry3D();
            centerHandle=new DraggableGeometryModel3D()
            {
                    //DragZ = false,
                    //DragX = (i % 2 == 1),
                    //DragY = (i % 2 == 0),
                    Material = this.Material,
                    Geometry = BoxGeometry,
                    Transform = new MatrixTransform3D( Matrix3DExtensions.Translate3D(bb)),
                };
            this.centerHandle.MouseMove3D += centerHandle_MouseMove3D;
            this.centerHandle.MouseUp3D += centerHandle_MouseUp3D;
            this.centerHandle.MouseDown3D += centerHandle_MouseDown3D;
            this.Children.Add(this.centerHandle);
         }

        void centerHandle_MouseDown3D(object sender, RoutedEventArgs e)
        {
            //var args = e as Mouse3DEventArgs;
            //if (args == null) return;
            //if (args.Viewport == null) return;

            //this.isCaptured = true;
            var args = e as Mouse3DEventArgs;
            if (args == null) return;
            if (args.Viewport == null) return;

            this.isCaptured = true;
            this.viewport = args.Viewport;
            this.camera = args.Viewport.Camera;
            this.lastHitPos = args.HitTestResult.PointHit;
        }

        void centerHandle_MouseMove3D(object sender, RoutedEventArgs e)
        {
            if (this.isCaptured)
            {
                Application.Current.MainWindow.Cursor = Cursors.SizeAll;
                var args = e as Mouse3DEventArgs;

                // move dragmodel                         
                var normal = this.camera.LookDirection;

                // hit position                        
                var newHit = this.viewport.UnProjectOnPlane(args.Position, lastHitPos, normal);
                if (newHit.HasValue)
                {
                    var offset = (newHit.Value - lastHitPos);
                    var trafo = this.Transform.Value;

                    if (this.DragX)
                        trafo.OffsetX += offset.X;

                    if (this.DragY)
                        trafo.OffsetY += offset.Y;

                    if (!this.DragZ)
                        trafo.OffsetZ += offset.Z;

                    //this.dragTransform.Matrix = trafo;
                    //this.centerHandle.SetTransformTranslateMatrix(trafo);
                    this.lastHitPos = newHit.Value;
                    UpdateAllTransforms(offset);
                }
                
            }
        }

        private void UpdateAllTransforms(System.Windows.Media.Media3D.Vector3D offset)
        {
            //foreach (var item in this.cornerHandles)
            //{
            //    var bb = item.Transform.Value;
            //    bb.OffsetX += offset.X;
            //     bb.OffsetY += offset.Y;
            //     bb.OffsetZ += offset.Z;
            //     item.SetTransformTranslateMatrix(bb);
            //}
            Vector3[] pp = new Vector3[this.cornerHandles.Length];
            for (int i = 0; i < this.cornerHandles.Length; i++)
            {
                DraggableGeometryModel3D corner = this.cornerHandles[i];
                var bb = corner.Transform.Value;
                bb.OffsetX += offset.X;
                bb.OffsetY += offset.Y;
                bb.OffsetZ += offset.Z;
                corner.SetTransformTranslateMatrix(bb );
                pp[i] += bb.ToMatrix().TranslationVector;
            }
            //var cornerTrafos = this.cornerHandles.Select(x => (x.Transform as MatrixTransform3D)).ToArray();
            //var cornerMatrix = cornerTrafos.Select(x => (x).Value).ToArray();
            //this.positions = cornerMatrix.Select(x => x.ToMatrix().TranslationVector).ToArray();

            //this.pp.CopyTo(this.Owner.Positions, 0);
            this.positions = pp;
            this.Owner.Positions = this.positions;
        }

        void centerHandle_MouseUp3D(object sender, RoutedEventArgs e)
        {
            if (this.isCaptured)
            {
                Application.Current.MainWindow.Cursor = Cursors.Arrow;
                this.Owner.Positions = this.positions;
                //UpdateTransforms(sender);
            }
            //if (this.isCaptured)
            //{
            //    Application.Current.MainWindow.Cursor = Cursors.Arrow;
            //    this.isCaptured = false;
            //    this.camera = null;
            //    this.viewport = null;
            //    this.Owner.Positions = this.positions;
            //}
        }

        private Vector3 GetBoundsCenter(Vector3[] points)
        {
            var bb = SharpDX.BoundingBox.FromPoints(points);
            Vector3 center = new Vector3();
            center.X = (bb.Minimum.X + bb.Maximum.X) / 2;
            center.Y =(bb.Minimum.Y + bb.Maximum.Y) / 2;
            center.Z = (bb.Minimum.Z + bb.Maximum.Z) / 2;
            return center;
        }

        private JpMqModel3D Owner;
        private DraggableGeometryModel3D[] cornerHandles ;
        private DraggableGeometryModel3D centerHandle;
        //private DraggableGeometryModel3D[] midpointHandles = new DraggableGeometryModel3D[4];
        //private MeshGeometryModel3D[] edgeHandles = new MeshGeometryModel3D[4];
        private bool isCaptured;
        private Viewport3DX viewport;
        private Camera camera;
        private System.Windows.Media.Media3D.Point3D lastHitPos;
        private MatrixTransform3D dragTransform;
        //private Material selectionMaterial;


        private static Geometry3D NodeGeometry;
        //private static Geometry3D EdgeHGeometry, EdgeVGeometry;
        //private static Geometry3D BoxGeometry;

        static InteractionHandle3D()
        {
            var b1 = new MeshBuilder();
            b1.AddSphere(new Vector3(0.0f, 0.0f, 0), 0.135);
            NodeGeometry = b1.ToMeshGeometry3D();

            //var b2 = new MeshBuilder();
            //b2.AddCylinder(new Vector3(0, 0, 0), new Vector3(1, 0, 0), 0.05, 32, true, true);
            //EdgeHGeometry = b2.ToMeshGeometry3D();

            //var b3 = new MeshBuilder();
            //b3.AddCylinder(new Vector3(0, 0, 0), new Vector3(0, 1, 0), 0.05, 32, true, true);
            //EdgeVGeometry = b3.ToMeshGeometry3D();

            //var b4 = new MeshBuilder();
            //b4.AddBox(new Vector3(0, 0, 0), 0.175, 0.175, 0.175);
            //BoxGeometry = b4.ToMeshGeometry3D();
        }

        /// <summary>
        /// 
        /// </summary>
        public InteractionHandle3D()
        {
            //this.Owner = mq;
            this.Material = PhongMaterials.Orange;
            this.dragTransform = new MatrixTransform3D();
        }

       

        private void OnNodeMouse3DDown(object sender, RoutedEventArgs e)
        {
            var args = e as Mouse3DEventArgs;
            if (args == null) return;
            if (args.Viewport == null) return;

            this.isCaptured = true;
        }

        private void OnNodeMouse3DUp(object sender, RoutedEventArgs e)
        {

            if (this.isCaptured)
            {
                Application.Current.MainWindow.Cursor = Cursors.Arrow;
                this.Owner.Positions = this.positions;
                //UpdateTransforms(sender);

            }
        }

        private void OnNodeMouse3DMove(object sender, RoutedEventArgs e)
        {
            if (this.isCaptured)
            {
                 UpdateTransforms(sender);
            }
        }

        private void UpdateTransforms(object sender)
        {
            var cornerTrafos = this.cornerHandles.Select(x => (x.Transform as MatrixTransform3D)).ToArray();
            var cornerMatrix = cornerTrafos.Select(x => (x).Value).ToArray();
            this.positions = cornerMatrix.Select(x => x.ToMatrix().TranslationVector).ToArray();
           

            //BoundingBox bb;
            //if (sender == cornerHandles[0] || sender == cornerHandles[2])
            //{
            //    Application.Current.MainWindow.Cursor = Cursors.SizeNESW;
            //    bb = BoundingBox.FromPoints(new[] { positions[0], positions[2] });
            //}
            //else// if (sender == cornerHandles[1] || sender == cornerHandles[3])
            //{
            //    Application.Current.MainWindow.Cursor = Cursors.SizeNWSE;
            //    bb = BoundingBox.FromPoints(new[] { positions[1], positions[3] });
            //}
            //else
            //{
            ////    if (sender == midpointHandles[0] || sender == midpointHandles[2])
            ////    {
            ////        Application.Current.MainWindow.Cursor = Cursors.SizeNS;
            ////    }
            ////    else
            ////    {
            ////        Application.Current.MainWindow.Cursor = Cursors.SizeWE;
            ////    }
            //    positions = this.midpointHandles.Select(x => x.Transform.Value.ToMatrix().TranslationVector).ToArray();
            //    bb = BoundingBox.FromPoints(positions);
            //}

            // 3 --- 2 
            // |     |
            // 0 --- 1
            //positions[0].X = bb.Minimum.X;
            //positions[1].X = bb.Maximum.X;
            //positions[2].X = bb.Maximum.X;
            //positions[3].X = bb.Minimum.X;

            //positions[0].Y = bb.Minimum.Y;
            //positions[1].Y = bb.Minimum.Y;
            //positions[2].Y = bb.Maximum.Y;
            //positions[3].Y = bb.Maximum.Y;

            //for (int i = 0; i < 4; i++)
            //{
            //    if (sender != cornerHandles[i])
            //    {
            //        cornerTrafos[i].Matrix = Matrix3DExtensions.Translate3D(positions[i].ToVector3D());
            //    }

            //    var m = Matrix3DExtensions.Translate3D(0.5 * (positions[i] + positions[(i + 1) % 4]).ToVector3D());
            //    ((MatrixTransform3D)this.midpointHandles[i].Transform).Matrix = m;
            //}

            // 3 --- 2 
            // |     |
            // 0 --- 1
            //var m0 = Matrix.Scaling(positions[1].X - positions[0].X, 1, 1) * Matrix.Translation(positions[0]);
            //((MatrixTransform3D)this.edgeHandles[0].Transform).Matrix = (m0.ToMatrix3D());
            //var m2 = Matrix.Scaling(positions[1].X - positions[0].X, 1, 1) * Matrix.Translation(positions[3]);
            //((MatrixTransform3D)this.edgeHandles[2].Transform).Matrix = (m2.ToMatrix3D());

            //var m1 = Matrix.Scaling(1, positions[2].Y - positions[1].Y, 1) * Matrix.Translation(positions[1]);
            //((MatrixTransform3D)this.edgeHandles[1].Transform).Matrix = (m1.ToMatrix3D());
            //var m3 = Matrix.Scaling(1, positions[2].Y - positions[1].Y, 1) * Matrix.Translation(positions[0]);
            //((MatrixTransform3D)this.edgeHandles[3].Transform).Matrix = (m3.ToMatrix3D());


        }

       
        public static readonly DependencyProperty DragXProperty =
            DependencyProperty.Register("DragX", typeof(bool), typeof(InteractionHandle3D), new UIPropertyMetadata(true));

        public static readonly DependencyProperty DragYProperty =
            DependencyProperty.Register("DragY", typeof(bool), typeof(InteractionHandle3D), new UIPropertyMetadata(true));

        public static readonly DependencyProperty DragZProperty =
            DependencyProperty.Register("DragZ", typeof(bool), typeof(InteractionHandle3D), new UIPropertyMetadata(true));

        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(InteractionHandle3D), new UIPropertyMetadata(false));


        public bool DragX
        {
            get { return (bool)this.GetValue(DragXProperty); }
            set { this.SetValue(DragXProperty, value); }
        }
        public bool DragY
        {
            get { return (bool)this.GetValue(DragYProperty); }
            set { this.SetValue(DragYProperty, value); }
        }
        public bool DragZ
        {
            get { return (bool)this.GetValue(DragZProperty); }
            set { this.SetValue(DragZProperty, value); }
        }

        public bool IsSelected
        {
            get { return (bool)this.GetValue(IsSelectedProperty); }
            set { this.SetValue(IsSelectedProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public Material Material
        {
            get { return (Material)this.GetValue(MaterialProperty); }
            set { this.SetValue(MaterialProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty MaterialProperty =
            DependencyProperty.Register("Material", typeof(Material), typeof(InteractionHandle3D), new UIPropertyMetadata(MaterialChanged));

        /// <summary>
        /// 
        /// </summary>
        private static void MaterialChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is PhongMaterial)
            {
                foreach (var item in ((GroupModel3D)d).Children)
                {
                    var model = item as MaterialGeometryModel3D;
                    if (model != null)
                    {
                        model.Material = e.NewValue as PhongMaterial;
                    }
                }
            }
        }
    }
    }

