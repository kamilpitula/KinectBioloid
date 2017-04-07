using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectBioloid.RobotLayer
{
    public interface IPositionChecker
    {
        event EventHandler<MatchFoundEventArgs> MatchFound;
    }
}
