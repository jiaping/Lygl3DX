using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla.Core;
using Csla.Rules;

namespace Lygl.DalLib.CustomRule
{
    /// <summary>
    /// usage: 
    /// BusinessRules.AddRule(new CalcTotal(SumTotalProperty, QtyProperty, PriceEaProperty));
    /// </summary>
    public class CalcTotal : Csla.Rules.CommonRules.CommonBusinessRule
    {
        private readonly IPropertyInfo _qtyProperty;
        private readonly IPropertyInfo _priceProperty;

        public CalcTotal(IPropertyInfo primaryProperty, IPropertyInfo qtyProperty, IPropertyInfo priceProperty)
            : base(primaryProperty)
        {
            _qtyProperty = qtyProperty;
            _priceProperty = priceProperty;
            InputProperties = new MobileList<IPropertyInfo>() { qtyProperty, priceProperty };
        }

        protected override void Execute(RuleContext context)
        {
            dynamic qty = context.InputPropertyValues[_qtyProperty];
            dynamic price = context.InputPropertyValues[_priceProperty];

            context.AddOutValue(qty * price);
        }
    }
}
