using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Jp3DKit
{
    public static class Vector3ArrayConverter
    {
        public static string ToString(Vector3[] vectors)
        {
            string result="";
            foreach (var item in vectors)
            {
                result += item.X.ToString()+","+item.Y.ToString()+","+item.Z.ToString()+",";
            }
            result.Substring(0, result.Length - 1);
            return result;
        }

        public static Vector3[] FromString(string str)
        {
            var arr = str.Split(new char[] { ',' },StringSplitOptions.RemoveEmptyEntries).ToArray();
            if (arr.Length / 3 != (int)arr.Length / 3) throw new Exception("Vector3ArrayConverter from string error:string format error");
            Vector3[] result = new Vector3[arr.Length / 3];
            for (int i = 0; i < arr.Length; i+=3)
            {
                result[i / 3].X = float.Parse( arr[i]);
                result[i / 3].Y = float.Parse(arr[i+1]);
                result[i / 3].Z = float.Parse(arr[i+2]);
            }
            return result;
        }
    }
}
