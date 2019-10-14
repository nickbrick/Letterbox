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
        private List<Handle> SelectedHandles = new List<Handle>();
        private Point lastClickPosition = new Point();

        public CurveEditor() : base()
        {
            Shape = new Shape() { Parts = new List<Part> { new Part() } };
            ActivePart = Shape.Parts.FirstOrDefault();
            base.MouseLeftButtonDown += AddBezierControlPoint;
            base.PreviewMouseLeftButtonDown += CurveEditor_PreviewMouseLeftButtonDown;
            base.PreviewMouseMove += CurveEditor_MouseMove;
            ActivePart.ControlPointInserted += ActivePart_ControlPointInserted;
            ActivePart.BezierControlPointInserted += ActivePart_BezierControlPointInserted;

            foreach (Part part in Shape.Parts)
            {
                this.Children.Add(part.Path);
                foreach (ControlPoint controlPoint in part.ControlPoints)
                {
                    AddHandle(controlPoint);
                }
                DrawPart(part);
            }
            foreach (var handle in Children.OfType<Handle>())
            {
                RegisterHandleEvents(handle);
            }
        }

        private void CurveEditor_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            lastClickPosition = e.GetPosition(this);
        }

        private void CurveEditor_MouseMove(object sender, MouseEventArgs e)
        {
            //foreach (var handle in SelectedHandles)
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                foreach (var handle in SelectedHandles)
                {
                    if (handle != null)
                    {
                        handle.SetPosition(e.GetPosition(this));
                        if (!(handle is SecondaryHandle))
                        {
                            var child = handle.ChildBefore;
                            var dif = child.Difference;
                            handle.ChildBefore.SetPosition(Point.Add(handle.GetPosition(), handle.ChildBefore.Difference));
                            handle.ChildAfter.SetPosition(Point.Add(handle.GetPosition(), handle.ChildAfter.Difference));
                        }
                    }
                }
                Console.WriteLine("move");
            }
        }

        private void RegisterHandleEvents(Handle handle)
        {
            handle.PreviewMouseLeftButtonDown += Handle_MouseLeftButtonDown;
            handle.PreviewMouseLeftButtonUp += Handle_MouseLeftButtonUp;
            handle.PositionChanged += Handle_PositionChanged;
        }

        private void Handle_PositionChanged(object sender, HandleEventArgs e)
        {
            if (sender is SecondaryHandle)
            {
                var handle = (SecondaryHandle)sender;
                var parent = handle.Parent;
                handle.Arm.X1 = e.Position.X;
                handle.Arm.Y1 = e.Position.Y;
                handle.Arm.X2 = parent.GetPosition().X;
                handle.Arm.Y2 = parent.GetPosition().Y;
            }
            var handle = (Handle)sender;
            handle.ControlPoint.set
            DrawPart(ActivePart);
        }

        private void Handle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Handle handle = (Handle)sender;
            SelectedHandles.Clear();
            Console.WriteLine(SelectedHandles.Count);

        }

        private void Handle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Handle handle = (Handle)sender;
            SelectedHandles.Add(handle);
            if (!(handle is SecondaryHandle))
            {
                handle.ChildBefore.GetDifference();
                handle.ChildAfter.GetDifference();
            }
            Console.WriteLine(SelectedHandles.Count);
        }

        public CurveEditor(Glyph glyph)
        {
            //Shape = glyph.Shape;
            Shape = new Shape() { Parts = new List<Part> { new Part() { ClassName = "test", Path = new Path() } } };
        }
        private void ActivePart_ControlPointInserted(object part, PartEventArgs e)
        {
            AddHandle(e.ControlPoints.First());

            DrawPart(ActivePart);
        }
        private void ActivePart_BezierControlPointInserted(object part, PartEventArgs e)
        {
            AddHandle(e.ControlPoints);
            DrawPart(ActivePart);
        }
        private void AddHandle(ControlPoint controlPoint)
        {
            Handle handle;
            switch (controlPoint.Type)
            {
                case ControlPointType.Primary:
                    {
                        handle = new Handle(controlPoint);
                        break;
                    }
                case ControlPointType.Secondary:
                    {
                        handle = new SecondaryHandle(controlPoint);
                        break;
                    }
                default:
                    {
                        handle = new Handle(controlPoint);
                        break;
                    }
            }
            handle.SetPosition(ToPixel(controlPoint.Position));
            this.Children.Add(handle);
            RegisterHandleEvents(handle);
        }
        private void AddHandle(List<ControlPoint> controlPoints)
        {
            var beforeHandle = new SecondaryHandle(controlPoints.First());
            var mainHandle = new Handle(controlPoints.ElementAt(1));
            var afterHandle = new SecondaryHandle(controlPoints.Last());

            mainHandle.SetChildren(beforeHandle, afterHandle);

            beforeHandle.SetPosition(ToPixel(controlPoints.First().Position));
            mainHandle.SetPosition(ToPixel(controlPoints.ElementAt(1).Position));
            afterHandle.SetPosition(ToPixel(controlPoints.Last().Position));

            this.Children.Add(beforeHandle);
            this.Children.Add(mainHandle);
            this.Children.Add(afterHandle);

            beforeHandle.SetParent(mainHandle);
            afterHandle.SetParent(mainHandle);

            this.Children.Add(beforeHandle.Arm);
            this.Children.Add(afterHandle.Arm);

            RegisterHandleEvents(beforeHandle);
            RegisterHandleEvents(mainHandle);
            RegisterHandleEvents(afterHandle);

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
            if (inverse)
            {
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
            var model = Point.Multiply(pixel, GetTransformationToPixelMatrix(inverse: true));
            return model;
        }
        private void AddBezierControlPoint(object sender, MouseButtonEventArgs e)
        {
            var mousePixel = e.GetPosition(this);
            var mouseModel = ToModel(mousePixel);

            ActivePart.AddBezierControlPoint(mouseModel);

            DrawPart(ActivePart);
        }
    }
}
