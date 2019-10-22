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
        }

        private void GlyphEditor_Loaded(object sender, RoutedEventArgs e)
        {
            Workpiece = ((MainWindow)System.Windows.Application.Current.MainWindow).ExampleTypeface.Glyphs.Where(glyph => glyph.Character == "b").FirstOrDefault();
            curveEditor.LoadShape(Workpiece.Shape);
            

        }

        public void Open(Glyph glyph)
        {
            //Workpiece = glyph;
            Workpiece = glyph;
            //Guidelines = glyph.GetParameters().Select(param => new Guideline(param)).ToHashSet();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            curveEditor.Shape.Parts.ForEach(part => part.ControlPoints.ForEach(point => Console.WriteLine(point.Position)));
        }
    }
}
