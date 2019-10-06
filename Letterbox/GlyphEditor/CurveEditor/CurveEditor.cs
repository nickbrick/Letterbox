using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Letterbox
{
    public class CurveEditor : Canvas
    {
        public Shape Shape { get; set; }
        public Part ActivePart { get; set; }
        public double Scale = 50;
        public Point Origin = new Point(200, 300);
        

        public CurveEditor() : base()
        {
            Shape = new Shape() { Parts = new List<Part> { new Part() } };
            ActivePart = Shape.Parts.FirstOrDefault();
            base.MouseDown += AddBezierControlPointAndHandle;

            foreach (Part part in Shape.Parts)
            {
                this.Children.Add(part.Path);
                foreach (ControlPoint controlPoint in part.ControlPoints)
                {
                    AddHandle(controlPoint);
                }
                DrawPart(part);
            }
        }




        public CurveEditor(Glyph glyph)
        {
            //Shape = glyph.Shape;
            Shape = new Shape() { Parts = new List<Part> { new Part() { ClassName = "test", Path = new Path() } } };
        }

        private void AddHandle(ControlPoint controlPoint, bool secondary=false)
        {
            Handle handle;
            if (secondary)
            {
                handle = new SecondaryHandle(controlPoint);
            }
            else
            {
                handle = new Handle(controlPoint);
            }
            handle.SetPosition(ToPixel(controlPoint.Position));
            this.Children.Add(handle);
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

        private Matrix GetTransformationToPixelMatrix(bool inverse = false)
        {
            var matrix = new Matrix(Scale, 0, 0, -Scale, Origin.X, Origin.Y);
            if (inverse) {
                matrix.Invert();
            }
            return matrix;
        }

        private Point ToPixel(Point model)
        {
            var pixel = Point.Multiply(model, GetTransformationToPixelMatrix());
            return pixel;
        }

        private Point ToModel(Point pixel)
        {
            var model = Point.Multiply(pixel, GetTransformationToPixelMatrix(true));
            return model;
        }

        private void AddBezierControlPointAndHandle(object sender, MouseButtonEventArgs e)
        {
            var mousePixel = e.GetPosition(this);
            var mouseModel = ToModel(mousePixel);
            ControlPoint newControlPoint;

            for (double offset = -0.3; offset <= 0.3; offset += 0.3)
            {
                var model = Point.Add(mouseModel, new Vector(offset, offset));
                newControlPoint = new ControlPoint(model);
                ActivePart.ControlPoints.Add(newControlPoint);
                var isSecondary = (offset != 0);
                AddHandle(newControlPoint, isSecondary);
            }
            DrawPart(ActivePart);
        }

        private void AddControlPointAndHandle(object sender, MouseButtonEventArgs e)
        {
            var mousePixel = e.GetPosition(this);
            var mouseModel = ToModel(mousePixel);
            var newControlPoint = new ControlPoint(mouseModel);
            ActivePart.ControlPoints.Add(newControlPoint);
            AddHandle(newControlPoint, ((ActivePart.ControlPoints.Count - 1) % 3 > 0));
            DrawPart(ActivePart);
            Console.WriteLine(mouseModel.ToString());
        }
    }
}
