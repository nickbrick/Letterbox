using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Letterbox
{
    class LetterStrip : ScrollViewer
    {
        public Orientation Orientation {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Orientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.RegisterAttached("Orientation", 
                typeof(Orientation), 
                typeof(LetterStrip), 
                new PropertyMetadata(Orientation.Vertical, Orientation_Changed));

        private static void Orientation_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var letterStrip = (LetterStrip)d;
            var newOrientation = (Orientation)(e.NewValue);
            letterStrip.stackPanel.Orientation = newOrientation;
            letterStrip.Width = Double.NaN;
            letterStrip.Height = Double.NaN;
            if (newOrientation == Orientation.Horizontal)
            {
                letterStrip.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        private bool fireButtonClickedEvent = true;
        private Point oldMousePosition;
        private StackPanel stackPanel;
        

        public event EventHandler<LetterStripEventArgs> SelectionChanged;

        public LetterStrip()
        {
            stackPanel = new StackPanel();
            stackPanel.Orientation = this.Orientation;
            HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            foreach (char c in "abcdefghijklmABCDEFGHIJKLM")
            {
                var button = new LetterButton(c.ToString());
                button.Click += LetterStrip_ButtonClicked;
                stackPanel.Children.Add(button);
            }
            stackPanel.PreviewMouseMove += StackPanel_PreviewMouseMove;
            stackPanel.PreviewMouseDown += StackPanel_PreviewMouseDown;
            this.AddChild(stackPanel);
        }

        private void LetterStrip_ButtonClicked(object sender, RoutedEventArgs e)
        {
            if (fireButtonClickedEvent)
            {
                Console.WriteLine(((LetterButton)sender).Content);
                //MessageBox.Show(((LetterButton)sender).Content.ToString());
                SelectionChanged(this, new LetterStripEventArgs(((LetterButton)sender).Content.ToString()));
            }
        }

        private void StackPanel_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            Point newMousePosition = Mouse.GetPosition((StackPanel)sender);

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (newMousePosition != oldMousePosition)
                    fireButtonClickedEvent = false;
                if (newMousePosition.Y < oldMousePosition.Y)
                    this.ScrollToVerticalOffset(this.VerticalOffset + 1);
                if (newMousePosition.Y > oldMousePosition.Y)
                    this.ScrollToVerticalOffset(this.VerticalOffset - 1);

                if (newMousePosition.X < oldMousePosition.X)
                    this.ScrollToHorizontalOffset(this.HorizontalOffset + 1);
                if (newMousePosition.X > oldMousePosition.X)
                    this.ScrollToHorizontalOffset(this.HorizontalOffset - 1);
            }
            else
            {
                oldMousePosition = newMousePosition;
            }
        }
        private void StackPanel_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            fireButtonClickedEvent = true;
            //Console.WriteLine(fireButtonClickedEvent);
        }


    }
}

