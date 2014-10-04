using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Globalization;
using System.Windows;

namespace Lygl.UI.Framework.ValidateRules
{
    /// <summary>
    /// 位置输入的检验规则
    /// </summary>
    public class PosValidateRule:ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Point pos;
            try
            {
                pos = Point.Parse(value.ToString());
            }
            catch (FormatException)
            {
                return new ValidationResult(false, "不是有效的座标格式");
            }
            catch (InvalidOperationException)
            {
                return new ValidationResult(false, "不是有效的座标位置");
            }
                return ValidationResult.ValidResult;
        }
    }

}
