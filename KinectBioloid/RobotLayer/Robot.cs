using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectBioloid.RobotLayer
{
    public class Robot : IRobot
    {
        private IPositionChecker _positionChecker;

        public Robot(IPositionChecker positionChecker)
        {
            _positionChecker = positionChecker;
            _positionChecker.MatchFound += _positionChecker_MatchFound;
        }

        private void _positionChecker_MatchFound(object sender, MatchFoundEventArgs e)
        {
            var leftHandPosition = e.LeftHandPosition;
            var rightHandPosition = e.RightHandPosition;

            if (leftHandPosition == ActionPointsEnum.LeftUp && rightHandPosition == ActionPointsEnum.RightUp)
            {
                Console.WriteLine("GoraGora");
                //Tutaj kod wydajacy polecenia robotowi
            }
            if (leftHandPosition == ActionPointsEnum.LeftMid && rightHandPosition == ActionPointsEnum.RightMid)
            {
                Console.WriteLine("SrodekSrodek");
                //Tutaj kod wydajacy polecenia robotowi
            }
            if (leftHandPosition == ActionPointsEnum.LeftDown && rightHandPosition == ActionPointsEnum.RightDown)
            {
                Console.WriteLine("DolDol");
                //Tutaj kod wydajacy polecenia robotowi
            }
        }
    }
}
