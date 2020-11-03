using System;
using System.Collections.Generic;
using System.Text;

namespace BuyMeCoffee.OrchardCore.ViewModels
{
   public class BuyMeCoffeeViewModel
    {
        public string DataId { get; set; }
        public string Description { get; set; }
        public string Message { get; set; }
        public string WidgetColor { get; set; }
        public string WidgetPosition { get; set; }
        public double XMargin { get; set; } = 18; 
        public double YMargin { get; set; } = 18;
        public string WidgetVersion { get; set; } = "1.0.0";

    }
}
