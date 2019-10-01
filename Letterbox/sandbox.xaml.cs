using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Letterbox
{
    /// <summary>
    /// Interaction logic for sandbox.xaml
    /// </summary>
    public partial class sandbox : Window
    {
        public bool canClick = true;
        Point oldMousePosition;
        private void sp_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            Point newMousePosition = Mouse.GetPosition((StackPanel)sender);
            tb.Text = newMousePosition.X + " | " + newMousePosition.Y;

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (newMousePosition != oldMousePosition) { canClick = false; }
                if (newMousePosition.Y < oldMousePosition.Y)
                    sv.ScrollToVerticalOffset(sv.VerticalOffset + 1);
                if (newMousePosition.Y > oldMousePosition.Y)
                    sv.ScrollToVerticalOffset(sv.VerticalOffset - 1);

                if (newMousePosition.X < oldMousePosition.X)
                    sv.ScrollToHorizontalOffset(sv.HorizontalOffset + 1);
                if (newMousePosition.X > oldMousePosition.X)
                    sv.ScrollToHorizontalOffset(sv.HorizontalOffset - 1);
            }
            else
            {
                oldMousePosition = newMousePosition;
            }
            Console.WriteLine(canClick);
        }
   
    public sandbox()
    {
        InitializeComponent();
    }
    private void Sp_PreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        canClick = true;
        Console.WriteLine(canClick);
    }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (canClick)
            {
                MessageBox.Show("Button_Click_1");
            }
        }
    }
}
