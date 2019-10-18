using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Letterbox
{
    public class ControlPoint
    {
        public Point Position { get; set; }
        public HashSet<Influence> Influences;
        public Part Part { get; }
        public ControlPointType Type { get; set; }

        public ControlPoint(Point position, Part part, ControlPointType type = ControlPointType.Primary)
        {
            Position = position;
            Type = type;
            Part = part;
        }
        public void SetPostition(Point position)
        {
            Position = position;
        }
    }

    public enum ControlPointType
    {
        Primary,
        Secondary
    }
}
