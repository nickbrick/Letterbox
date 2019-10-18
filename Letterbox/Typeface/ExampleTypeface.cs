using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Letterbox
{
    public class ExampleTypeface : Typeface
    {
        public ExampleTypeface()
        {
            Glyphs = new SortedSet<Glyph>();
            var a = new Glyph("a") { Shape = new Shape() { Parts = new List<Part>() } } ;
            a.Shape.Parts.Add(new Part()
            {
                ClassName = "Bowl",
                ControlPoints = new List<ControlPoint>()
            {
                new ControlPoint(new Point(2.64,1.8),ControlPointType.Secondary),
                new ControlPoint(new Point(1.32,1.38),ControlPointType.Primary),
                new ControlPoint(new Point(0.919999999999997,1.34),ControlPointType.Secondary),
                new ControlPoint(new Point(0.76,1.54),ControlPointType.Secondary),
                new ControlPoint(new Point(0.24,1.36),ControlPointType.Primary),
                new ControlPoint(new Point(-1.32000000000001,0.819999999999997),ControlPointType.Secondary),
                new ControlPoint(new Point(-1.62000000000001,-0.979999999999997),ControlPointType.Secondary),
                new ControlPoint(new Point(-0.16,-0.900000000000002),ControlPointType.Primary),
                new ControlPoint(new Point(0.56,-0.720000000000001),ControlPointType.Secondary),
                new ControlPoint(new Point(1.4,0.339999999999999),ControlPointType.Secondary),
                new ControlPoint(new Point(1.34,1.38),ControlPointType.Primary),
                new ControlPoint(new Point(1.64,1.68),ControlPointType.Secondary)
            }
            });
            Glyphs.Add(a);
        }
    }
}
