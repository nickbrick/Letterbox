using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letterbox
{
    public class LetterStripEventArgs : EventArgs
    {
        public string Character { get; private set; }
    }
}
