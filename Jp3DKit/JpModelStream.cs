using HelixToolkit.Wpf;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Jp3DKit
{
   

    /// <summary>
    /// Imports a model from a file cache in ModelVisua3D.
    /// </summary>
    public static class JpModelStream 
    {
        //
        // 摘要:
        //     获取或设置要呈现的 System.Windows.Media.Media3D.Model3D 对象。
        //
        // 返回结果:
        //     要呈现的 System.Windows.Media.Media3D.Model3D 对象。
        private static Model3D _visual3DModel;
        public static  Model3D Visual3DModel{
            get {
                if (_visual3DModel==null)
                {
                    Load();
                }
                return _visual3DModel;
            }
        }

            //        string path = AppDomain.CurrentDomain.BaseDirectory;
            //path += "mx1.3ds";

        public static void Load()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            path += "..\\..\\..\\3dModel\\mx31.obj";
            Load(path);
        }
        public static void Load(string path)
        {
            var importer = new ModelImporter();
            _visual3DModel = path != null ? importer.Load(path) : null;
        }

    }

}
