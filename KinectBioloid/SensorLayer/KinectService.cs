using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Kinect;
using ROBOTIS;

namespace KinectBioloid.SensorLayer
{
    public class KinectService:IKinectService
    {
        private KinectSensor _sensor;
        private byte[] _colorPixels;
        private WriteableBitmap _colorBitmap;

        public void Initialize()
        {

            foreach (var potentialSensor in KinectSensor.KinectSensors)
            {
                if (potentialSensor.Status == KinectStatus.Connected)
                {
                    _sensor = potentialSensor;
                    break;
                }
            }
            if (_sensor != null)
            {
                _sensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
                _sensor.SkeletonStream.Enable();
                _sensor.DepthStream.Enable();
                _colorPixels = new byte[_sensor.ColorStream.FramePixelDataLength];
                _colorBitmap = new WriteableBitmap(_sensor.ColorStream.FrameWidth, _sensor.ColorStream.FrameHeight, 96.0,
                    96.0, PixelFormats.Bgr32, null);
               
                _sensor.AllFramesReady += _sensor_AllFramesReady;
                try
                {
                    _sensor.Start();
                }
                catch (IOException)
                {
                    _sensor = null;
                }

            }
           
        }

        private void _sensor_AllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            using (ColorImageFrame colorFrame = e.OpenColorImageFrame())
            {
                if (colorFrame != null)
                {
                    colorFrame.CopyPixelDataTo(_colorPixels);
                    _colorBitmap.WritePixels(new Int32Rect(0, 0, _colorBitmap.PixelWidth, _colorBitmap.PixelHeight),
                        _colorPixels, _colorBitmap.PixelWidth * sizeof(int), 0);
                    if (_colorBitmap != null)
                    {
                        if (ColorUpdated != null)
                        {
                            ColorUpdated(this, new ColorEventArgs() { Bitmap = _colorBitmap });
                        }
                    }

                }
            }

            var skeletons = new Skeleton[0];

            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame != null)
                {
                    skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
                    skeletonFrame.CopySkeletonDataTo(skeletons);
                }
            }

            if (skeletons.Length != 0)
            {
                foreach (Skeleton skeleton in skeletons)
                {
                    if (skeleton.TrackingState == SkeletonTrackingState.Tracked)
                    {
                        Joint rightHandJoint = skeleton.Joints[JointType.HandRight];
                        Joint leftHandJoint = skeleton.Joints[JointType.HandLeft];

                        if (rightHandJoint.TrackingState != JointTrackingState.NotTracked &&
                            leftHandJoint.TrackingState != JointTrackingState.NotTracked)
                        {
                            CoordinateMapper cm = new CoordinateMapper(_sensor);
                            var leftHandPosition = cm.MapSkeletonPointToColorPoint(leftHandJoint.Position, ColorImageFormat.RgbResolution640x480Fps30);
                            var rightHandPosition = cm.MapSkeletonPointToColorPoint(rightHandJoint.Position, ColorImageFormat.RgbResolution640x480Fps30);
                            if (this.SkeletonUpdated != null)
                            {
                                this.SkeletonUpdated(this,
                                    new SkeletonEventArgs()
                                    {
                                        RightHandPosition = rightHandPosition,
                                        LeftHandPosition = leftHandPosition
                                    });
                            }
                        }
                        if (rightHandJoint.TrackingState == JointTrackingState.NotTracked ||
                            leftHandJoint.TrackingState == JointTrackingState.NotTracked)
                        {
                            return;
                        }
                        if (rightHandJoint.TrackingState == JointTrackingState.Inferred &&
                            leftHandJoint.TrackingState == JointTrackingState.Inferred)
                        {
                            return;
                        }

                    }
                }
            }


        }

        

        public void Cleanup()
        {
            if (_sensor != null)
            {
                _sensor.Stop();
                _sensor.Dispose();
            }
        }

        public event EventHandler<SkeletonEventArgs> SkeletonUpdated;
        public event EventHandler<ColorEventArgs> ColorUpdated;
    }
}
