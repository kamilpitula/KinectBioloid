using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Microsoft.Kinect;

namespace KinectBioloid.SensorLayer
{
    public class SkeletonDrawer : ISkeletonDrawer
    {
        private readonly Brush _rightHandBrush;
        private readonly Brush _leftHandBrush;
        private readonly Brush _actionPointBrush;
        private const double HandCenterThickness = 5;

        public SkeletonDrawer()
        {
            _rightHandBrush = Brushes.Aqua;
            _leftHandBrush = Brushes.Chartreuse;
            _actionPointBrush = Brushes.Crimson;
        }

        public DrawingImage DrawSkeleton(SkeletonEventArgs jointPositions)
        {
            DrawingGroup drawingGroup = new DrawingGroup();
            DrawingImage image = new DrawingImage(drawingGroup);

            var leftHand = jointPositions.LeftHandPosition;
            var rightHand = jointPositions.RightHandPosition;

            using (DrawingContext dc = drawingGroup.Open())
            {
                dc.DrawRectangle(Brushes.Transparent, null, new Rect(0.0, 0.0, 640, 480));
                var actionPointsToDraw = ActionPoints.GetActionPoints();

                foreach (var actionPoint in actionPointsToDraw)
                {
                    dc.DrawEllipse(_actionPointBrush, null, new Point(actionPoint.X, actionPoint.Y), actionPoint.Radius,
                        actionPoint.Radius);
                }

                if (leftHand != null && rightHand != null)
                {
                    dc.DrawEllipse(_rightHandBrush, null, PointToScreen(rightHand), HandCenterThickness, HandCenterThickness);
                    dc.DrawEllipse(_leftHandBrush, null, PointToScreen(leftHand), HandCenterThickness, HandCenterThickness);
                }
            }

            return image;
        }

        private Point PointToScreen(ColorImagePoint skeletonPoint)
        {

            return new Point(skeletonPoint.X, skeletonPoint.Y);
        }
    }
}
