using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Lygl.UI.Framework.FormatProvider
{
    public class GeometryIntFormatProvider : IFormatProvider, ICustomFormatter
    {
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
                return this;
            else
                return null;
        }

        public string Format(string fmt, object arg, IFormatProvider formatProvider)
        {
            // Provide default formatting if arg is not an Int64.
            Type dd = arg.GetType();
            if (arg.GetType() !=typeof(System.Windows.Media.PathFigure) )//typeof(Int64 )
                try
                {
                    return HandleOtherFormats(fmt, arg);
                }
                catch (FormatException e)
                {
                    throw new FormatException(String.Format("The format of '{0}' is invalid.", fmt), e);
                }
            string ss = arg.ToString();
            CharEnumerator ii = ss.GetEnumerator();
            bool hasDot=false;
            StringBuilder sb=new StringBuilder();
            while (ii.MoveNext())
            {
                if (ii.Current == '.')
                {
                    hasDot = true;
                    continue;
                }
                else
                {
                    if (!hasDot) { sb.Append(ii.Current); }
                    else
                    {

                        if (!((ii.Current.CompareTo('0') >= 0) && (ii.Current.CompareTo('9') <= 0)))
                        {
                            hasDot = false;
                            sb.Append(ii.Current);
                        }
                    };
                }
            }
            sb.Append("Z");

            return sb.ToString();

        }

        private string HandleOtherFormats(string format, object arg)
        {
           
                return arg.ToString();
            
        }
    }
  

}
