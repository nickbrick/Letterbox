using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Letterbox
{
    public class Part
    {
        public string ClassName;
        public Path Path;
        public List<ControlPoint> ControlPoints;
        public GeometryCollection geometries = new GeometryCollection();

        public delegate void PartEventHandler(object part, PartEventArgs e);
        public event PartEventHandler ControlPointInserted;

        public Part()
        {
            ClassName = "Default";
            Path = new Path();
            Path.Stroke = System.Windows.Media.Brushes.Black;

            ControlPoints = new List<ControlPoint>();
            ControlPoints.Add(new ControlPoint(new Point(-1, 0)));
            ControlPoints.Add(new ControlPoint(new Point(-1, 2)));
            ControlPoints.Add(new ControlPoint(new Point(1, 2)));
            ControlPoints.Add(new ControlPoint(new Point(1, 0)));
            ControlPoints.Add(new ControlPoint(new Point(1.1, 0.1)));
        }

        //public void AddBezierControlPoint(Point position)
        //{
        //    ControlPoints.Add(new ControlPoint(Point.Add(position, new Vector(-0.1, 0))));
        //    ControlPoints.Add(new ControlPoint(position));
        //    ControlPoints.Add(new ControlPoint(Point.Add(position, new Vector(0.1, 0))));
        //}


        public void AddControlPoint(Point position, ControlPointType type)
        {
            var newControlPoint = new ControlPoint(position, type);
            ControlPoints.Add(newControlPoint);
            ControlPointInserted(this, new PartEventArgs(this, newControlPoint, ControlPoints.Count));
        }
    }

    public class PartEventArgs : EventArgs
    {
        public Part Part { get; private set; }
        public ControlPoint ControlPoint { get; private set; }
        public int Position { get; private set; }
        public PartEventArgs(Part part, ControlPoint controlPoint, int position)
        {
            Part = part;
            ControlPoint = controlPoint;
            Position = position;
        }
    }
}
