using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Input;
using System.Windows.Media;

namespace Letterbox
{
    public class Handle : Border
    {
        public ControlPoint ControlPoint { get; set; }
        public SecondaryHandle ChildBefore { get; set; }
        public SecondaryHandle ChildAfter { get; set; }
        public Vector Difference;
        public double Size;
        public double spread = 0.5;
        public delegate void HandleEventHandler(object handle, HandleEventArgs e);
        public event HandleEventHandler PositionChanged;

        public Handle(ControlPoint controlPoint)
        {
            ControlPoint = controlPoint;
            Size = 15;

            Width = Size;
            Height = Size;
            Background = new RadialGradientBrush(new GradientStopCollection()
                { 
                    new GradientStop(Colors.Black, spread),
                    new GradientStop(Colors.Transparent, spread) 
                }
            );
            CornerRadius = new CornerRadius((Size - 1) / 2);
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
        public Vector GetDifference(Point mousePosition)
        {
            Difference = Vector.Subtract((Vector)(this.GetPosition()), (Vector)(mousePosition));
            return Difference;
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
        public SecondaryHandle(ControlPoint controlPoint) : base(controlPoint)
        {
            Size = 13;
            Width = Size;
            Height = Size;
            spread = 0.3;
            BorderBrush = null;
            Background = new RadialGradientBrush(new GradientStopCollection()
                {
                    new GradientStop(Colors.Gray, spread),
                    new GradientStop(Colors.Transparent, spread)
                }
            );
        }

        public void SetParent(Handle parent)
        {
            Parent = parent;
            Arm = new Line
            {
                IsHitTestVisible = false,
                Stroke = System.Windows.Media.Brushes.Gray,
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
