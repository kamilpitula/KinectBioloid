using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using KinectBioloid.Annotations;
using KinectBioloid.SensorLayer;

namespace KinectBioloid.ViewModels
{
    class MainWindowViewModel:INotifyPropertyChanged
    {
        #region ctor
        private IKinectService _kinectService;
        private ISkeletonDrawer _skeletonDrawer;

        public MainWindowViewModel(IKinectService kinectService, ISkeletonDrawer skeletonDrawer)
        {
            _kinectService = kinectService;
            _skeletonDrawer = skeletonDrawer;
            _kinectService.SkeletonUpdated += _kinectService_SkeletonUpdated;
            _kinectService.ColorUpdated += _kinectService_ColorUpdated;
            _kinectService.Initialize();
        }
        #endregion

        #region properties

        private WriteableBitmap _imageSource;

        public WriteableBitmap ImageSource
        {
            get
            {
                return _imageSource;
            }
            set
            {
                _imageSource = value;
                OnPropertyChanged("ImageSource");
            }
        }

        private DrawingImage _skeletonSource;

        public DrawingImage SkeletonSource
        {
            get
            {
                return _skeletonSource;
            }
            set
            {
                _skeletonSource = value;
                OnPropertyChanged("SkeletonSource");
            }
        }

        #endregion

        #region events
        private void _kinectService_ColorUpdated(object sender, ColorEventArgs e)
        {
            ImageSource = e.Bitmap;
        }

        private void _kinectService_SkeletonUpdated(object sender, SkeletonEventArgs e)
        {
            SkeletonSource = _skeletonDrawer.DrawSkeleton(e);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
