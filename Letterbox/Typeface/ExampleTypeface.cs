﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letterbox
{
    class ExampleTypeface : Typeface
    {
        public ExampleTypeface()
        {
            Glyphs = new SortedSet<Glyph>();
        }
    }
}