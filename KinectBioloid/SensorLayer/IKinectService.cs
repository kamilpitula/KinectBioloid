using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KinectBioloid.Annotations;

namespace KinectBioloid.SensorLayer
{
    public interface IKinectService
    {
        void Initialize();
        void Cleanup();
        event EventHandler<SkeletonEventArgs> SkeletonUpdated;
        event EventHandler<ColorEventArgs> ColorUpdated;
    }
}
