using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letterbox
{
    public class Typeface
    {
        public SortedSet<Glyph> Glyphs;
        //public HashSet<Part> Parts;
        public HashSet<Parameter> Parameters;

        public Typeface()
        {
            Glyphs = new SortedSet<Glyph> { new Glyph("a") };
            Parameters = new HashSet<Parameter> { new Parameter() };
        }

        public Glyph FindGlyph(string character)
        {
            return Glyphs.Where(glyph => glyph.Character == character).FirstOrDefault();
        }
    }

}
