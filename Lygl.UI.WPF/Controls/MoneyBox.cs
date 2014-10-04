using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lygl.UI.Controls
{
    public class MoneyBox : NumericBoxBase<decimal>
    {

        private const string DEFAULT_FORMATSTRING = "C";

        public MoneyBox()
        {
            this.FormatString = DEFAULT_FORMATSTRING;
        }

        protected override bool CanParse(string text)
        {
            decimal buf;
            bool result = decimal.TryParse(
                this.Text,
                NumberStyles.Currency,
                CultureInfo.CurrentCulture,
                out buf);
           // Debug.WriteLineIf(!result, this.GetType().Name + ": Cannot parse: " + text);
            return result;
        }

        protected override decimal? DoParse(string text)
        {
            decimal buf;
            if (decimal.TryParse(
                this.Text,
                NumberStyles.Any,
                CultureInfo.CurrentCulture,
                out buf))
            {
                return buf;
            }
            return null;
        }

        protected override string ToText(decimal value)
        {
            return value.ToString(this.FormatString, CultureInfo.CurrentCulture);
        }

        [DefaultValue(DEFAULT_FORMATSTRING)]
        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string FormatString
        {
            get
            {
                return base.FormatString;
            }
            set
            {
                base.FormatString = value;
            }
        }


    }
}
