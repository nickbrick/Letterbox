using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Letterbox
{
    public class Navigation : DependencyObject
    {
        public Point Origin { get; set; }
        public Point InitialOrigin { get; set; }
        public Point PanStart { get; set; } 

        public Point CurrentMouseModel {
            get { return (Point)GetValue(CurrentMouseModelProperty); }
            set { SetValue(CurrentMouseModelProperty, value); }
        }
        public static readonly DependencyProperty CurrentMouseModelProperty =
            DependencyProperty.Register("CurrentMouseModel", typeof(Point), typeof(Navigation), new PropertyMetadata());

        public Point CurrentMousePixel {
            get { return (Point)GetValue(CurrentMousePixelProperty); }
            set { SetValue(CurrentMousePixelProperty, value); }
        }
        public static readonly DependencyProperty CurrentMousePixelProperty =
            DependencyProperty.Register("CurrentMousePixel", typeof(Point), typeof(Navigation), new PropertyMetadata());
    }
}
