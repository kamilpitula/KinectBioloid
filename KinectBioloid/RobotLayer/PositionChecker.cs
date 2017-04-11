using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using KinectBioloid.SensorLayer;

namespace KinectBioloid.RobotLayer
{
    public class PositionChecker : IPositionChecker
    {
        private IKinectService _kinectService;
        private readonly List<ActionPoint> _actionPointsToCheck;

        public PositionChecker(IKinectService kinectService)
        {
            _kinectService = kinectService;
            _kinectService.SkeletonUpdated += _kinectService_SkeletonUpdated;
            _actionPointsToCheck = ActionPoints.GetActionPoints();
        }

        private void _kinectService_SkeletonUpdated(object sender, SkeletonEventArgs e)
        {
            
            var leftHand = new Point(e.LeftHandPosition.X ,e.LeftHandPosition.Y );
            var rightHand = new Point(e.RightHandPosition.X ,e.RightHandPosition.Y );

            ActionPointsEnum leftHandPosition = ActionPointsEnum.NotMatch;
            ActionPointsEnum rightHandPosition = ActionPointsEnum.NotMatch;

            foreach (var actionPoint in _actionPointsToCheck)
            {
                if (IsInsideCircle(actionPoint, leftHand))
                {
                    leftHandPosition = actionPoint.PositionName;
                }
                if (IsInsideCircle(actionPoint, rightHand))
                {
                    rightHandPosition = actionPoint.PositionName;
                }
            }

            if (leftHandPosition != ActionPointsEnum.NotMatch && rightHandPosition != ActionPointsEnum.NotMatch)
            {
                if (MatchFound != null)
                {
                    MatchFound(this,
                    new MatchFoundEventArgs()
                    {
                        LeftHandPosition = leftHandPosition,
                        RightHandPosition = rightHandPosition
                    });
                }
                
            }
            
        }

        private bool IsInsideCircle(ActionPoint actionPoint, Point point)
        {
            return (Math.Pow((point.X - actionPoint.X), 2.0) + Math.Pow((point.Y - actionPoint.Y), 2.0)) <
                   Math.Pow(actionPoint.Radius, 2.0);
        }

        public event EventHandler<MatchFoundEventArgs> MatchFound;
    }
}
