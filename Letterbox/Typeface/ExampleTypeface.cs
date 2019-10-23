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
                new ControlPoint(new Point(2.44,0.4), bbowl, type: ControlPointType.Secondary),
                new ControlPoint(new Point(1.12,-0.02), bbowl, type: ControlPointType.Primary),
                new ControlPoint(new Point(1,-0.74), bbowl, type: ControlPointType.Secondary),
                new ControlPoint(new Point(-0.24,0.02), bbowl, type: ControlPointType.Secondary),
                new ControlPoint(new Point(-0.76,-0.16), bbowl, type: ControlPointType.Primary),
                new ControlPoint(new Point(-1.4,-0.16), bbowl, type: ControlPointType.Secondary),
                new ControlPoint(new Point(-1.98,-1.06), bbowl, type: ControlPointType.Secondary),
                new ControlPoint(new Point(-0.52,-0.98), bbowl, type: ControlPointType.Primary),
                new ControlPoint(new Point(0.32,-0.9), bbowl, type: ControlPointType.Secondary),
                new ControlPoint(new Point(0.16,-0.46), bbowl, type: ControlPointType.Secondary),
                new ControlPoint(new Point(-0.4,-0.12), bbowl, type: ControlPointType.Primary),
                new ControlPoint(new Point(-0.1,0.18), bbowl, type: ControlPointType.Secondary)
            };
            b.Shape.Parts.Add(bbowl);
            var asc = new Part() { ClassName = "Ascender" };
            asc.ControlPoints = new List<ControlPoint>()
            {
                new ControlPoint(new Point(-0.18,1.96), asc, type: ControlPointType.Secondary),
                new ControlPoint(new Point(-1.5,1.54), asc, type: ControlPointType.Primary),
                new ControlPoint(new Point(-0.96,1.14 ), asc, type: ControlPointType.Secondary),
                new ControlPoint(new Point(-1.06,-0.18 ), asc, type: ControlPointType.Secondary),
                new ControlPoint(new Point(-1.9,-1.04), asc, type: ControlPointType.Primary),
                new ControlPoint(new Point(0.14,-1.24), asc, type: ControlPointType.Secondary)

                
                
                
                
                
                
            };
            b.Shape.Parts.Add(asc);

            Glyphs.Add(b);
        }
    }
}
