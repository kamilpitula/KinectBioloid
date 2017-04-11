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
        private IKinectService kinectService;
        private IPositionChecker positionChecker;
        private IRobot robot;
        private ISkeletonDrawer skeletonDrawer;

        public MainWindow()
        {
            InitializeComponent();
            kinectService = new KinectService();
            positionChecker = new PositionChecker(kinectService);
            robot = new Robot(positionChecker);
            skeletonDrawer = new SkeletonDrawer();

            this.Closing += MainWindow_Closing;


            this.DataContext = new MainWindowViewModel(kinectService, skeletonDrawer);

        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (kinectService != null)
            {
                kinectService.Cleanup();
            }
        }
    }
}
