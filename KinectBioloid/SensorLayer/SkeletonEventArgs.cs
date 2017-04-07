using System;
using Microsoft.Kinect;

namespace KinectBioloid.SensorLayer
{
    public class SkeletonEventArgs:EventArgs
    {
        
        public SkeletonPoint RightHandPosition { get; set; }
        public SkeletonPoint LeftHandPosition { get; set; }
    }
}