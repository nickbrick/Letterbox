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
        public ControlPointType Type { get; set; }

        public ControlPoint(Point position, ControlPointType type=ControlPointType.Primary)
        {
            Position = position;
            Type = type;
        }
    }

    public enum ControlPointType
    {
        Primary,
        Secondary
    }
}
