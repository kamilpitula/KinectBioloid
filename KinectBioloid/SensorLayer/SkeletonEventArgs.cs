using System;
using Microsoft.Kinect;

namespace KinectBioloid.SensorLayer
{
    public class SkeletonEventArgs:EventArgs
    {
        
        public DepthImagePoint RightHandPosition { get; set; }
        public DepthImagePoint LeftHandPosition { get; set; }
    }
}