using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KinectBioloid.RobotLayer;
using KinectBioloid.SensorLayer;
using KinectBioloid.ViewModels;

namespace KinectBioloid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            IKinectService kinectService = new KinectService();
            IPositionChecker positionChecker = new PositionChecker(kinectService);
            IRobot robot = new Robot(positionChecker);
            ISkeletonDrawer skeletonDrawer = new SkeletonDrawer();
            this.DataContext = new MainWindowViewModel(kinectService, skeletonDrawer);
        }
    }
}
