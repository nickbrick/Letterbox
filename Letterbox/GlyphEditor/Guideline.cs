using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Letterbox
{
    public class Guideline
    {
        //public string ClassName;
        public Line Line;
        public Parameter Parameter;

        public Guideline(Parameter parameter)
        {
            Parameter = parameter;
            if (Parameter.Orientation == Orientation.Horizontal)
            {

            }
            else if (Parameter.Orientation == Orientation.Vertical)
            {

            }
            else
            {

            }
        }
    }
}
