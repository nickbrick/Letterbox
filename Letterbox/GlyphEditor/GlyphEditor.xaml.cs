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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Letterbox
{
    /// <summary>
    /// Interaction logic for GlyphEditor.xaml
    /// </summary>
    public partial class GlyphEditor : Page
    {
        public Glyph Workpiece { get; set; }
        public Glyph Left;
        public Glyph Right;
        public HashSet<Guideline> Guidelines;
        public GlyphEditor()
        {
            InitializeComponent();
            Loaded += GlyphEditor_Loaded;
            //MouseMove += GlyphEditor_MouseMove;
            Open(new Glyph("a"));
            Render();
        }

        private void GlyphEditor_MouseMove(object sender, MouseEventArgs e)
        {
            curveEditor.CurveEditor_MouseMove(sender, e);
        }

        private void GlyphEditor_Loaded(object sender, RoutedEventArgs e)
        {
            curveEditor.Navigation.Origin = new Point(curveEditor.ActualWidth / 2, curveEditor.ActualHeight / 2);
            curveEditor.DrawShape();

        }

        public void Open(Glyph glyph)
        {
            //Workpiece = glyph;
            Workpiece = glyph;
            //Guidelines = glyph.GetParameters().Select(param => new Guideline(param)).ToHashSet();
        }
        public void Render()
        {
            var lbl = new Label();
            lbl.Content = Workpiece.Character;
            
        }
    }
}
