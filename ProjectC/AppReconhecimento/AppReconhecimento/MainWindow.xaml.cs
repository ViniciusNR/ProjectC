using System.Windows;
using Microsoft.Kinect;
using AuxiliarKinect.FuncoesBasicas;
using System.Linq;
using System.Windows.Media;

namespace AppRecognition
{
    public partial class MainWindow : Window
    {
        bool rightHandAboveHead;
        bool leftHandAboveHead;

        public MainWindow()
        {
            InitializeComponent();
            InicializarSensor();
        }
        private void InicializarSensor()
        {
            KinectSensor kinect = InicializadorKinect.InicializarPrimeiroSensor(10);
            kinect.SkeletonStream.Enable();

            kinect.SkeletonFrameReady += KinectEvent;
        }

        private void KinectEvent(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (SkeletonFrame frame = e.OpenSkeletonFrame())
            {
                if (frame != null)
                {
                    aboveHead(frame);
                }
            }
        }

        private void aboveHead(SkeletonFrame frame) {
            Skeleton[] skeleton = new Skeleton[6];
            frame.CopySkeletonDataTo(skeleton);
            Skeleton user = skeleton.FirstOrDefault(sklt => sklt.TrackingState == SkeletonTrackingState.Tracked);
            if (user != null){
                Joint rightHand = user.Joints[JointType.HandRight];
                Joint leftHand = user.Joints[JointType.HandLeft];
                Joint head = user.Joints[JointType.Head];
                bool testRightHandAboveHead = rightHand.Position.Y > head.Position.Y;
                bool testLeftHandAboveHead = leftHand.Position.Y > head.Position.Y;

                if (rightHandAboveHead != testRightHandAboveHead){
                    rightHandAboveHead = testRightHandAboveHead;
                    if (rightHandAboveHead){
                        MessageBox.Show("Right hand is above your head!");
                        square2.Fill = new SolidColorBrush(System.Windows.Media.Colors.AliceBlue);
                    }
                    else {
                        square2.Fill = new SolidColorBrush(System.Windows.Media.Colors.Gray);
                    }
                }

                if (leftHandAboveHead != testLeftHandAboveHead)
                {
                    leftHandAboveHead = testLeftHandAboveHead;
                    if (leftHandAboveHead)
                    {
                        MessageBox.Show("Left hand is above your head!");
                        square1.Fill = new SolidColorBrush(System.Windows.Media.Colors.AliceBlue);
                    }
                    else
                    {
                        square1.Fill = new SolidColorBrush(System.Windows.Media.Colors.Gray);
                    }
                }
            }
        }
    }
}
