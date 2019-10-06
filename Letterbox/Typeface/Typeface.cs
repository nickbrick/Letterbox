using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letterbox
{
    public class Typeface
    {
        SortedSet<Glyph> Glyphs;
        //HashSet<Part> Parts;
        HashSet<Parameter> Parameters;

        public Typeface()
        {
            Glyphs = new SortedSet<Glyph> { new Glyph("a") };
            Parameters = new HashSet<Parameter> { new Parameter() };
        }
    }

}
