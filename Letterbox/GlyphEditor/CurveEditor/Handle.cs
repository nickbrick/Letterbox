using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Input;

namespace Letterbox
{
    public class Handle : Button
    {
        public ControlPoint ControlPoint { get; set; }
        public SecondaryHandle ChildBefore { get; set; }
        public SecondaryHandle ChildAfter { get; set; }
        public Vector DifferenceBefore { get; set; }
        public Vector DifferenceAfter { get; set; }
        public double Size;
        public delegate void HandleEventHandler(object handle, HandleEventArgs e);
        public event HandleEventHandler PositionChanged;
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
            var handler = PositionChanged;
            if (handler != null)
            {
                PositionChanged(this, new HandleEventArgs(position));
            }
        }
        public void SetChildren(SecondaryHandle before, SecondaryHandle after)
        {
            ChildBefore = before;
            ChildBefore.SetParent(this);
            ChildAfter = after;
            ChildAfter.SetParent(this);
        }
        public Point GetPosition()
        {
            Point result = new Point();
            result.X = this.Margin.Left + this.Width / 2;
            result.Y = this.Margin.Top + this.Height / 2;
            return result;
        }
    }

    public class HandleEventArgs : EventArgs
    {
        public Point Position;
        public HandleEventArgs(Point position)
        {
            Position = position;
        }
    }

    public class SecondaryHandle : Handle
    {
        public new Handle Parent;
        public Line Arm;
        public Vector Difference;
        public SecondaryHandle(ControlPoint controlPoint) : base(controlPoint)
        {
            Size = 5;
            Width = Size;
            Height = Size;
        }

        public void SetParent(Handle parent)
        {
            Parent = parent;
            Arm = new Line
            {
                IsHitTestVisible = false,
                Stroke = System.Windows.Media.Brushes.Black,
                X1 = this.GetPosition().X,
                Y1 = this.GetPosition().Y,
                X2 = this.Parent.GetPosition().X,
                Y2 = this.Parent.GetPosition().Y
            };
        }

        public Vector GetDifference()
        {
            Difference = Vector.Subtract((Vector)(this.GetPosition()), (Vector)(this.Parent.GetPosition()));
            return Difference;
        }
    }
}
