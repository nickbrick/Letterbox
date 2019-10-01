using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Letterbox
{
    class LetterButton : Button
    {
        public LetterButton(string character)
        {
            this.Content = character;
            this.Width = 30;
            this.Height = 30;
            this.BorderThickness = new Thickness(0);
            //this.Margin = new Thickness(0, -5, 0, 0);
        }
    }
}
