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
            var bowl = new Part() { ClassName = "Bowl" };
            bowl.ControlPoints = new List<ControlPoint>()
            {
                new ControlPoint(new Point(2.64,1.8), bowl, type: ControlPointType.Secondary),
                new ControlPoint(new Point(1.32,1.38), bowl, type: ControlPointType.Primary),
                new ControlPoint(new Point(0.92,1.34), bowl, type: ControlPointType.Secondary),
                new ControlPoint(new Point(0.76,1.54), bowl, type: ControlPointType.Secondary),
                new ControlPoint(new Point(0.24,1.36), bowl, type: ControlPointType.Primary),
                new ControlPoint(new Point(-1.32,0.82), bowl, type: ControlPointType.Secondary),
                new ControlPoint(new Point(-1.62,-0.98), bowl, type: ControlPointType.Secondary),
                new ControlPoint(new Point(-0.16,-0.90), bowl, type: ControlPointType.Primary),
                new ControlPoint(new Point(0.56,-0.72), bowl, type: ControlPointType.Secondary),
                new ControlPoint(new Point(1.4,0.34), bowl, type: ControlPointType.Secondary),
                new ControlPoint(new Point(1.34,1.38), bowl, type: ControlPointType.Primary),
                new ControlPoint(new Point(1.64,1.68), bowl, type: ControlPointType.Secondary)
            };
            a.Shape.Parts.Add(bowl);
            var stem = new Part() { ClassName = "Stem" };
            stem.ControlPoints = new List<ControlPoint>()
            {
                new ControlPoint(new Point(3.6,1.9   ), stem, type: ControlPointType.Secondary),
                new ControlPoint(new Point(2.28,1.48 ), stem, type: ControlPointType.Primary),
                new ControlPoint(new Point(1.88,1.44 ), stem, type: ControlPointType.Secondary),
                new ControlPoint(new Point(1.02,0.12 ), stem, type: ControlPointType.Secondary),
                new ControlPoint(new Point(1.86,-0.74), stem, type: ControlPointType.Primary),
                new ControlPoint(new Point(2.18,-0.94), stem, type: ControlPointType.Secondary)
            };
            a.Shape.Parts.Add(stem);

            Glyphs.Add(a);

            var b = new Glyph("b") { Shape = new Shape() { Parts = new List<Part>() } };
            var bbowl = new Part() { ClassName = "Bowl" };
            bbowl.ControlPoints = new List<ControlPoint>()
            {
                new ControlPoint(new Point(2.64,1.8), bowl, type: ControlPointType.Secondary),
                new ControlPoint(new Point(1.32,1.38), bowl, type: ControlPointType.Primary),
                new ControlPoint(new Point(0.92,1.34), bowl, type: ControlPointType.Secondary),
                new ControlPoint(new Point(0.76,1.54), bowl, type: ControlPointType.Secondary),
                new ControlPoint(new Point(0.24,1.36), bowl, type: ControlPointType.Primary),
                new ControlPoint(new Point(-1.32,0.82), bowl, type: ControlPointType.Secondary),
                new ControlPoint(new Point(-1.62,-0.98), bowl, type: ControlPointType.Secondary),
                new ControlPoint(new Point(-0.16,-0.90), bowl, type: ControlPointType.Primary),
                new ControlPoint(new Point(0.56,-0.72), bowl, type: ControlPointType.Secondary),
                new ControlPoint(new Point(1.4,0.34), bowl, type: ControlPointType.Secondary),
                new ControlPoint(new Point(1.34,1.38), bowl, type: ControlPointType.Primary),
                new ControlPoint(new Point(1.64,1.68), bowl, type: ControlPointType.Secondary)
            };
            //b.Shape.Parts.Add(bbowl);
            var asc = new Part() { ClassName = "Ascender" };
            asc.ControlPoints = new List<ControlPoint>()
            {
                new ControlPoint(new Point(1.6,1.9   ), stem, type: ControlPointType.Secondary),
                new ControlPoint(new Point(0.28,1.48 ), stem, type: ControlPointType.Primary),
                new ControlPoint(new Point(-1.88,1.44 ), stem, type: ControlPointType.Secondary),
                new ControlPoint(new Point(-1.02,0.12 ), stem, type: ControlPointType.Secondary),
                new ControlPoint(new Point(-1.86,-0.74), stem, type: ControlPointType.Primary),
                new ControlPoint(new Point(0.18,-0.94), stem, type: ControlPointType.Secondary)
            };
            b.Shape.Parts.Add(asc);

            Glyphs.Add(b);
        }
    }
}
