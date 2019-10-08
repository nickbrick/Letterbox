using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Shapes;

namespace Letterbox
{
    public class Handle : Button
    {
        public ControlPoint ControlPoint { get; set; }
        public SecondaryHandle ChildBefore { get; set; }
        public SecondaryHandle ChildAfter { get; set; }
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
        public void SetChildren(SecondaryHandle before, SecondaryHandle after)
        {
            ChildBefore = before;
            ChildBefore.AttachArm(this);
            ChildAfter = after;
            ChildAfter.AttachArm(this);
        }
    }

    public class SecondaryHandle : Handle
    {
        public new Handle Parent;
        public Line Arm;
        public SecondaryHandle(ControlPoint controlPoint) : base(controlPoint)
        {
            Size = 5;
            Width = Size;
            Height = Size;
        }

        public void AttachArm(Handle parent)
        {
            Parent = parent;
            Arm = new Line
            {
                Stroke = System.Windows.Media.Brushes.Black,
                X1 = this.Margin.Left,
                Y1 = this.Margin.Top,
                X2 = this.Parent.Margin.Left,
                Y2 = this.Parent.Margin.Top
            };
        }
    }
}
