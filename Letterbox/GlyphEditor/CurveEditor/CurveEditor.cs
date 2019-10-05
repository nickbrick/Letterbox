using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Letterbox
{
    public class CurveEditor : Canvas
    {
        public Shape Shape { get; set; }
        public double Scale = 50;
        public Point Origin = new Point(200, 300);

        public CurveEditor() : base()
        {
            Shape = new Shape() { Parts = new List<Part> { new Part() } };

            foreach (Part part in Shape.Parts)
            {
                this.Children.Add(part.Path);
                foreach (ControlPoint controlPoint in part.ControlPoints)
                {
                    var handle = new Handle(controlPoint);
                    handle.SetPosition(ToPixel(controlPoint.Position));
                    this.Children.Add(handle);
                }
                DrawPart(part);
            }
        }

        public CurveEditor(Glyph glyph)
        {
            //Shape = glyph.Shape;
            Shape = new Shape() { Parts = new List<Part> { new Part() { ClassName = "test", Path = new Path() } } };
            Render();
        }

        public void DrawShape()
        {
            foreach (Part part in Shape.Parts)
            {
                DrawPart(part);
            }
        }

        public void DrawPart(Part part)
        {
            var controlPoints = part.ControlPoints;
            //for (int i = 0; i < controlPoints.Count - 1; i += 4)
            //{
            part.Path.Data = new PathGeometry(
                new PathFigureCollection(
                    new List<PathFigure> {
                        new PathFigure(
                            ToPixel(controlPoints.FirstOrDefault().Position),
                            new List<PolyBezierSegment> {
                                new PolyBezierSegment(controlPoints.Skip(1).Select(point => ToPixel(point.Position)), true)
                            },
                            false)
                    }
                )
            );
            //}
        }

        private Point ToPixel(Point model)
        {
            var screen = Point.Multiply(model, new System.Windows.Media.Matrix(Scale, 0, 0, -Scale, Origin.X, Origin.Y));
            return screen;
        }

        private void Render()
        {
            throw new NotImplementedException();
        }
    }
}
