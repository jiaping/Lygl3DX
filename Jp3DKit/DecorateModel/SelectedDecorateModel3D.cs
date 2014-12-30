using HelixToolkit.Wpf.SharpDX;
using HelixToolkit.Wpf.SharpDX.Core;
using SharpDX;
using System;
using System.Linq;
using System.Windows;
using Vector3D = System.Windows.Media.Media3D.Vector3D;
using Point3D=System.Windows.Media.Media3D.Point3D;


namespace Jp3DKit.DecorateModel
{
    public class SelectedDecorateModel3D:DecorateModel3D
    {
        private string selectGeometryStr = "0.207055,0,0.772741,-9.79717E-17,0,0.8,-1.22465E-16,0,1,0.258819,0,0.965926,0.5,0,0.866025,0.707107,0,0.707107,0.866025,0,0.5,0.965926,0,0.258819,0.772741,0,0.207055,0.69282,0,0.4,0.565685,0,0.565685,0.4,0,0.69282,1,0,1.83697E-16,0.965926,0,-0.258819,0.866025,0,-0.5,0.707107,0,-0.707107,0.5,0,-0.866025,0.258819,0,-0.965926,0.207055,0,-0.772741,0.4,0,-0.69282,0.565685,0,-0.565685,0.69282,0,-0.4,0.772741,0,-0.207055,0.8,0,1.46958E-16,-0.772741,0,0.207055,-0.8,0,-4.89859E-17,-1,0,-6.12323E-17,-0.965926,0,0.258819,-0.866025,0,0.5,-0.707107,0,0.707107,-0.5,0,0.866025,-0.258819,0,0.965926,-0.207055,0,0.772741,-0.4,0,0.69282,-0.565685,0,0.565685,-0.69282,0,0.4,-0.866025,0,-0.5,-0.965926,0,-0.258819,-0.772741,0,-0.207055,-0.69282,0,-0.4,-0.565685,0,-0.565685,-0.4,0,-0.69282,-0.207055,0,-0.772741,0,0,-0.8,0,0,-1,-0.258819,0,-0.965926,-0.5,0,-0.866025,-0.707107,0,-0.707107";
        private  string selectGeometryIndiceStr = "0,1,2,2,3,4,4,5,6,6,7,8,11,0,2,2,4,6,6,8,9,10,11,2,6,9,10,10,2,6,23,12,13,13,14,15,15,16,17,17,18,19,22,23,13,13,15,17,17,19,20,21,22,13,13,17,20,20,21,13,24,25,26,26,27,28,28,29,30,30,31,32,35,24,26,26,28,30,30,32,33,34,35,26,30,33,34,34,26,30,47,36,37,37,38,39,42,43,44,44,45,46,46,47,37,37,39,40,41,42,44,44,46,37,37,40,41,41,44,37";
        /// <summary>
        /// 光标显示的位置
        /// </summary>
        public Vector3D PositionPoint
        {
            get { return (Vector3D)GetValue(PositionPointProperty); }
            set { SetValue(PositionPointProperty, value); }
        }

        /// <summary>
        /// 光标显示的位置
        /// </summary>
        public static readonly DependencyProperty PositionPointProperty =
            DependencyProperty.Register("PositionPoint", typeof(Vector3D), typeof(SelectedDecorateModel3D), new UIPropertyMetadata(new Vector3D(), PositionPointChanged));
        
        private static void PositionPointChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = ((SelectedDecorateModel3D)d);
            obj.Transform = CreateAnimatedTransform1((Vector3D)e.NewValue,obj.IsAnimation, new Vector3D(0, 1, 0), 3);
            //var obj = ((DecorateModel3D)d);
            //if (obj.IsAttached)
            //    obj.bHasCubeMap.Set((bool)e.NewValue);
        }

        public bool IsAnimation { get; set; }

        public SelectedDecorateModel3D()
        {
            IsAnimation = false;
            MeshGeometry3D mg = new HelixToolkit.Wpf.SharpDX.MeshGeometry3D();
            mg.Indices = IntCollection.Parse(selectGeometryIndiceStr); // selectGeometryIndiceStr.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(i => int.Parse(i));
            mg.Positions = Vector3Collection.Parse(selectGeometryStr);// Vector3ArrayConverter.FromString(selectGeometryStr);
            mg.Normals = Vector3Collection.Parse(Vector3ArrayConverter.ConvertToString(mg.Positions.Select(o => Vector3.UnitY).ToArray())); //mg.Positions;

            this.Geometry = mg;
            this.Material = HelixToolkit.Wpf.SharpDX.PhongMaterials.Blue;
            this.Transform = CreateAnimatedTransform1(PositionPoint,IsAnimation, new Vector3D(0, 1, 0), 3);

            //MeshGeometryModel3D meshModel = new MeshGeometryModel3D();
            //HelixToolkit.Wpf.SharpDX.MeshGeometry3D geometry = new HelixToolkit.Wpf.SharpDX.MeshGeometry3D();
            //geometry.Positions = new HelixToolkit.Wpf.SharpDX.Core.Vector3Collection();
            //geometry.TextureCoordinates = new HelixToolkit.Wpf.SharpDX.Core.Vector2Collection();
            //geometry.Normals = new HelixToolkit.Wpf.SharpDX.Core.Vector3Collection();
            //geometry.Indices = new HelixToolkit.Wpf.SharpDX.Core.IntCollection();
            //geometry.Positions.Add(new Vector3(0.65f, 0f, -0f));
            //geometry.Positions.Add(new Vector3(-0.00228214f, 0f, -2.46236f));
            //geometry.Positions.Add(new Vector3(0f, 0f, -0f));
            //geometry.TextureCoordinates.Add(new Vector2(1.06627f, 0f));
            //geometry.TextureCoordinates.Add(new Vector2(-0.00374367f, 4.03931f));
            //geometry.TextureCoordinates.Add(new Vector2(0f, 0f));
            //geometry.Normals.Add(new Vector3(0f, 1f, 0f));
            //geometry.Normals.Add(new Vector3(0f, 1f, 0f));
            //geometry.Normals.Add(new Vector3(0f, 1f, 0f));
            //geometry.Indices.Add(0);
            //geometry.Indices.Add(1);
            //geometry.Indices.Add(2);
            //this.Geometry = geometry;
            //this.Material = PhongMaterials.Red;
           // this.Transform = new System.Windows.Media.Media3D.TranslateTransform3D(-3, 0, 0);
        }

          /// <summary>
        /// 利用wpf方式实现动画
        /// </summary>
        /// <param name="translate"></param>
        /// <param name="axis"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        private static  System.Windows.Media.Media3D.Transform3D CreateAnimatedTransform1(Vector3D translate, bool isAnimation, Vector3D axis, double speed = 4)
        {
            var lightTrafo = new System.Windows.Media.Media3D.Transform3DGroup();
            lightTrafo.Children.Add(new System.Windows.Media.Media3D.TranslateTransform3D(translate));
            if (isAnimation)
            {
                var rotateAnimation = new System.Windows.Media.Animation.Rotation3DAnimation
                {
                    RepeatBehavior = System.Windows.Media.Animation.RepeatBehavior.Forever,

                    By = new System.Windows.Media.Media3D.AxisAngleRotation3D(axis, 90),
                    Duration = TimeSpan.FromSeconds(speed / 4),
                    IsCumulative = true,
                };
                var rotateTransform = new System.Windows.Media.Media3D.RotateTransform3D();
                rotateTransform.CenterX = translate.X; rotateTransform.CenterY = translate.Y; rotateTransform.CenterZ = translate.Z;
                rotateTransform.BeginAnimation(System.Windows.Media.Media3D.RotateTransform3D.RotationProperty, rotateAnimation);
                lightTrafo.Children.Add(rotateTransform);
            }
            return lightTrafo;
        }
    }
}
