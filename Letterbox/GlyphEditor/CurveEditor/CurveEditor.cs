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
        public Part ActivePart1 { get; set; }
        public double Scale = 50;
        private List<Handle> SelectedHandles = new List<Handle>();
        public Guideline Baseline = new Guideline(null) { Line = new Line() };

        #region Dependency properties
        public Navigation Navigation {
            get { return (Navigation)GetValue(NavigationProperty); }
            set { SetValue(NavigationProperty, value); }
        }
        public static readonly DependencyProperty NavigationProperty =
            DependencyProperty.Register("Navigation", typeof(Navigation), typeof(CurveEditor), new PropertyMetadata());
        public Part ActivePart {
            get { return (Part)GetValue(ActivePartProperty); }
            set { SetValue(ActivePartProperty, value); }
        }
        public static readonly DependencyProperty ActivePartProperty =
            DependencyProperty.Register("ActivePart", typeof(Part), typeof(CurveEditor), new PropertyMetadata()); 
        #endregion

        public CurveEditor() : base()
        {
            Navigation = new Navigation();
        }

        public void LoadShape(Shape shape)
        {
            ClearContent();
            Shape = shape;
            ActivePart = Shape.Parts.FirstOrDefault();
            RegisterEvents();
            InitializeContent();
            DrawShape();
        }

        private void ClearContent()
        {
            Children.OfType<Path>().ToList().ForEach(path => Children.Remove(path));
            Children.OfType<Handle>().ToList().ForEach(handle =>
            {
                Children.Remove(handle);
                if (handle is SecondaryHandle)
                {
                    Children.Remove(((SecondaryHandle)handle).Arm);
                }
            });
        }

        public void Initialize()
        {
            Navigation.Origin = new Point(ActualWidth / 2, ActualHeight * 2 / 3);
            InitializeBaseline();
        }
        private void InitializeBaseline()
        {
            Baseline.Line.X1 = 0;
            Baseline.Line.Y1 = Navigation.Origin.Y;
            Baseline.Line.X2 = ActualWidth;
            Baseline.Line.Y2 = Navigation.Origin.Y;
            Baseline.Line.Stroke = System.Windows.Media.Brushes.Gray;
            Children.Add(Baseline.Line);
        }
        private void InitializeContent()
        {
            foreach (Part part in Shape.Parts)
            {
                this.Children.Add(part.Path);
                foreach (ControlPoint controlPoint in part.ControlPoints.Where(point => point.Type == ControlPointType.Primary))
                {
                    var index = part.ControlPoints.IndexOf(controlPoint);
                    var triplet = new List<ControlPoint> { part.ControlPoints.ElementAt(index - 1), controlPoint, part.ControlPoints.ElementAt(index + 1) };
                    AddHandles(triplet);
                }
                DrawPart(part);
            }
            foreach (var handle in Children.OfType<Handle>())
            {
                RegisterHandleEvents(handle);
            }
        }
        private void RegisterEvents()
        {
            base.MouseRightButtonDown += CurveEditor_MouseRightButtonDown;
            base.MouseDown += CurveEditor_MouseDown;
            base.MouseUp += CurveEditor_MouseUp;
            base.PreviewMouseMove += CurveEditor_MouseMove;
            base.SizeChanged += CurveEditor_SizeChanged;
            ActivePart.ControlPointInserted += ActivePart_ControlPointInserted;
            ActivePart.BezierControlPointInserted += ActivePart_BezierControlPointInserted;
        }

        #region CurveEditor events
        private void CurveEditor_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Navigation.Origin = new Point(ActualWidth / 2, ActualHeight * 2 / 3);
            DrawGuidelines();
            DrawShape();
        }
        private void CurveEditor_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ReleaseMouseCapture();
        }
        private void CurveEditor_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CaptureMouse();
            if (e.MiddleButton == MouseButtonState.Pressed)
            {
                Navigation.PanStart = e.GetPosition(this);
                Navigation.InitialOrigin = Navigation.Origin;
            }
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                ActivePart = null;
                SetSecondaryHandlesVisibility(false);
            }
        }
        private void CurveEditor_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var mousePixel = e.GetPosition(this);
            var mouseModel = ToModel(mousePixel);
            if (ActivePart != null)
            {
                ActivePart.AddBezierControlPoint(mouseModel);
                DrawPart(ActivePart);
            }
        }
        private void CurveEditor_MouseMove(object sender, MouseEventArgs e)
        {
            Navigation.CurrentMouseModel = ToModel(e.GetPosition(this));
            Navigation.CurrentMousePixel = e.GetPosition(this);

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragHandles(e);
            }
            if (e.MiddleButton == MouseButtonState.Pressed)
            {
                Pan(e);
            }
        } 
        #endregion

        private void Pan(MouseEventArgs e)
        {
            Navigation.Origin = Point.Add(Navigation.InitialOrigin, (Vector)(Point.Subtract(e.GetPosition(this), (Vector)Navigation.PanStart)));
            DrawGuidelines();
            DrawShape();
        }


        private void DragHandles(MouseEventArgs e)
        {
            foreach (var handle in SelectedHandles)
            {
                if (handle != null)
                {
                    handle.SetPosition(Point.Add(e.GetPosition(this), handle.Difference));
                    if (!(handle is SecondaryHandle))
                    {
                        var child = handle.ChildBefore;
                        var dif = child.Difference;
                        handle.ChildBefore.SetPosition(Point.Add(handle.GetPosition(), handle.ChildBefore.Difference));
                        handle.ChildAfter.SetPosition(Point.Add(handle.GetPosition(), handle.ChildAfter.Difference));
                    }
                }
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
            var handle = (Handle)sender;
            handle.ControlPoint.SetPostition(ToModel(e.Position));

            if (sender is SecondaryHandle)
            {
                var secondaryHandle = (SecondaryHandle)sender;
                var parent = secondaryHandle.Parent;
                secondaryHandle.Arm.X1 = e.Position.X;
                secondaryHandle.Arm.Y1 = e.Position.Y;
                secondaryHandle.Arm.X2 = parent.GetPosition().X;
                secondaryHandle.Arm.Y2 = parent.GetPosition().Y;
            }
            DrawPart(handle.ControlPoint.Part);
        }
        private void Handle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Handle handle = (Handle)sender;
            SelectedHandles.Clear();
        }
        private void Handle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Handle handle = (Handle)sender;
            ActivePart = handle.ControlPoint.Part;
            Console.WriteLine(ActivePart.ClassName);
            SelectedHandles.Add(handle);
            handle.GetDifference(e.GetPosition(this));
            if (!(handle is SecondaryHandle))
            {
                handle.ChildBefore.GetDifference();
                handle.ChildAfter.GetDifference();
                SetSecondaryHandlesVisibility(true, ActivePart);

            }
            e.Handled = true;
        }

        private void SetSecondaryHandlesVisibility(bool visible, Part part = null)
        {
            var visibility = visible ? Visibility.Visible : Visibility.Hidden;
            var handles = Children.OfType<SecondaryHandle>().ToList();
            if (part != null)
            {
                handles = handles.Where(handle => handle.ControlPoint.Part == part).ToList();
            }

            handles.ForEach(handle => { handle.Visibility = visibility; handle.Arm.Visibility = visibility; });
        }

        private void ActivePart_ControlPointInserted(object part, PartEventArgs e)
        {
            AddHandle(e.ControlPoints.First());

            DrawPart(ActivePart);
        }
        private void ActivePart_BezierControlPointInserted(object part, PartEventArgs e)
        {
            AddHandles(e.ControlPoints);
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
        private void AddHandles(List<ControlPoint> controlPoints)
        {
            var beforeHandle = new SecondaryHandle(controlPoints.First());
            var mainHandle = new Handle(controlPoints.ElementAt(1));
            var afterHandle = new SecondaryHandle(controlPoints.Last());

            mainHandle.SetChildren(beforeHandle, afterHandle);

            beforeHandle.SetPosition(ToPixel(controlPoints.First().Position));
            mainHandle.SetPosition(ToPixel(controlPoints.ElementAt(1).Position));
            afterHandle.SetPosition(ToPixel(controlPoints.Last().Position));

            this.Children.Add(mainHandle);
            this.Children.Add(beforeHandle);
            this.Children.Add(afterHandle);

            beforeHandle.SetParent(mainHandle);
            afterHandle.SetParent(mainHandle);

            this.Children.Add(beforeHandle.Arm);
            this.Children.Add(afterHandle.Arm);

            RegisterHandleEvents(beforeHandle);
            RegisterHandleEvents(mainHandle);
            RegisterHandleEvents(afterHandle);

        }

        #region Draws
        private void DrawShape()
        {
            foreach (var handle in Children.OfType<Handle>())
            {
                handle.SetPosition(ToPixel(handle.ControlPoint.Position));
            }
            foreach (Part part in Shape.Parts)
            {
                DrawPart(part);
            }
        }
        private void DrawPart(Part part)
        {
            var controlPoints = part.ControlPoints;
            part.Path.Data = new PathGeometry(
                new PathFigureCollection(
                    new List<PathFigure> {
                        new PathFigure(
                            ToPixel(controlPoints.ElementAt(1).Position),
                            new List<PolyBezierSegment> {
                                new PolyBezierSegment(controlPoints.Skip(2).Select(point => ToPixel(point.Position)), true)
                            },
                            false)
                    }
                )
            );
        }
        private void DrawGuidelines()
        {
            Baseline.Line.X2 = ActualWidth;
            Baseline.Line.Y1 = Navigation.Origin.Y;
            Baseline.Line.Y2 = Navigation.Origin.Y;
        } 
        #endregion

        #region Transformations
        private Matrix GetTransformationToPixelMatrix(bool inverse = false)
        {
            var matrix = new Matrix(Scale, 0, 0, -Scale, Navigation.Origin.X, Navigation.Origin.Y);
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
        private Point ToPixel(double model_)
        {
            var model = new Point(model_, model_);
            var pixel = Point.Multiply(model, GetTransformationToPixelMatrix());
            return pixel;
        }
        private Point ToModel(Point pixel)
        {
            var model = Point.Multiply(pixel, GetTransformationToPixelMatrix(inverse: true));
            return model;
        }
        private Point ToModel(double pixel_)
        {
            var pixel = new Point(pixel_, pixel_);
            var model = Point.Multiply(pixel, GetTransformationToPixelMatrix(inverse: true));
            return model;
        }
        #endregion
    }
}
