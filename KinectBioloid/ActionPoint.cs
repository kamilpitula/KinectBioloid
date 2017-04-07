using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectBioloid
{
     public class ActionPoint
    {
        public double Radius { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public ActionPointsEnum PositionName { get; set; }
    }
}
