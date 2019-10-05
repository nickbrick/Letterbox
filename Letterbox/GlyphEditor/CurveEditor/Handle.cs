using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Letterbox
{
    public class Handle : Button
    {
        public ControlPoint ControlPoint { get; set; }
        public double Size;
        public Handle(ControlPoint controlPoint)
        {
            ControlPoint = controlPoint;
            Size = 11;
            Width = Size;
            Height = Size;
        }

        public void SetPosition(Point position)
        {
            var x = position.X;
            var y = position.Y;
            Margin = new Thickness(x - Size / 2, y - Size / 2, 0, 0);
        }
    }
}
