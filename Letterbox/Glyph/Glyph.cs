using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letterbox
{
    public class Glyph : IComparable
    {
        public string Character { get; set; }
        public Shape Shape { get; set; }
        public Glyph(string character)
        {
            Character = character;
        }
        public HashSet<Parameter> GetParameters()
        {
            var parameters = new HashSet<Parameter>();
            foreach (Part part in this.Shape.Parts)
            {
                foreach (ControlPoint controlPoint in part.ControlPoints)
                {
                    foreach (Influence influence in controlPoint.Influences)
                    {
                        parameters.Add(influence.Source);
                    }
                }
            }
            return parameters;
        }

        public int CompareTo(object obj)
        {
            return Character.CompareTo(((Glyph)obj).Character);
        }
    }
}
