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

        public Part()
        {
            ClassName = "Default";
            Path = new Path();
            Path.Stroke = System.Windows.Media.Brushes.Black;

            ControlPoints = new List<ControlPoint>();
            ControlPoints.Add(new ControlPoint(-1, 0));
            ControlPoints.Add(new ControlPoint(-1, 2));
            ControlPoints.Add(new ControlPoint(1, 2));
            ControlPoints.Add(new ControlPoint(1, 0));

        }

        
    }

}
