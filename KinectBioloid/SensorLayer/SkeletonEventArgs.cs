using System;
using Microsoft.Kinect;

namespace KinectBioloid.SensorLayer
{
    public class SkeletonEventArgs:EventArgs
    {
        
        public ColorImagePoint RightHandPosition { get; set; }
        public ColorImagePoint LeftHandPosition { get; set; }
    }
}