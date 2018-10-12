using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Kinect.Toolkit;

//using Microsoft.Kinect.Toolkit.Interaction;
//using Microsoft.Kinect.Toolkit.Controls;




//namespace KinectButton
namespace KinectHandTracking

{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Members
        bool captured = false;
        double x_shape, x_canvas, y_shape, y_canvas;
        UIElement source = null;

        KinectSensor _sensor;
        MultiSourceFrameReader _reader;
        IList<Body> _bodies;

        public Ellipse Ellipse { get; private set; }
        public Rectangle Rectangle { get; private set; }
        //kinectButton.Click += new RoutedEventHandler(kinectButton_Click);
        

        List<Button> buttons;
        static Button selected;
        public Button HoverButton;




        #endregion

        #region Constructor

        public MainWindow()
        {
            InitializeComponent();
        }
        private void InitializeButtons()
        {
            //buttons = new List<Button> { button};
        }

        #endregion

        private void DrawingRanadTeeth(Rectangle recName, double width, double height, double positionOnXAxis, double positionOnYAxis)
        {
            recName.StrokeThickness = 2;
            recName.Stroke = Brushes.GreenYellow;
            recName.Width = width;
            recName.Height = height;
            Canvas.SetLeft(recName, positionOnXAxis);
            Canvas.SetTop(recName, positionOnYAxis);
            

        }

        #region test func
        private void TestDrawingForFindPosition(Ellipse ellipseName,double width,double height,double positionOnXAxis,double positionOnYAxis)
        {
            ellipseName.StrokeThickness = 5;
            ellipseName.Stroke = Brushes.Green;
            ellipseName.Width = width;
            ellipseName.Height = height;
            Canvas.SetLeft(ellipseName, positionOnXAxis);
            Canvas.SetTop(ellipseName, positionOnYAxis);
        }

      

        private void shape_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            source = (UIElement)sender;
            Mouse.Capture(source);
            captured = true;
            x_shape = Canvas.GetLeft(source);
            x_canvas = e.GetPosition(canvas).X;
            y_shape = Canvas.GetTop(source);
            y_canvas = e.GetPosition(canvas).Y;

            //Console.WriteLine(x_shape + " " + y_shape);
            Console.WriteLine("x:"+Math.Round(x_canvas,2) + " " +"y:"+ Math.Round(y_canvas,2));
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Best-Dosl\Desktop\KinectHandTracking\mouse_coordinate.txt",true))
            {
                file.WriteLine("x:" + Math.Round(x_canvas, 2) + " " + "y:" + Math.Round(y_canvas, 2));
            }


        }

        private void shape_MouseMove(object sender, MouseEventArgs e)
        {
            if (captured)
            {
                double x = e.GetPosition(canvas).X;
                double y = e.GetPosition(canvas).Y;
                x_shape += x - x_canvas;
                Canvas.SetLeft(source, x_shape);
                x_canvas = x;
                y_shape += y - y_canvas;
                Canvas.SetTop(source, y_shape);
                y_canvas = y;
            }
        }

        private void shape_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Mouse.Capture(null);
            captured = false;
        }
        #endregion

        #region Event handlers

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _sensor = KinectSensor.GetDefault();

            if (_sensor != null)
            {
                _sensor.Open();

                _reader = _sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color | FrameSourceTypes.Depth | FrameSourceTypes.Infrared | FrameSourceTypes.Body);
                _reader.MultiSourceFrameArrived += Reader_MultiSourceFrameArrived;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (_reader != null)
            {
                _reader.Dispose();
            }

            if (_sensor != null)
            {
                _sensor.Close();
            }
        }
        //void image_MouseMove(object sender, MouseEventArgs e)
        //{
            //Point mouseLocation = e.GetPosition(sender as IInputElement);
        //}

        void Reader_MultiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {
            var reference = e.FrameReference.AcquireFrame();

            // Color
            //var pointColor = position.ToPoint(Visualization.Color);
            //var left = pointColor.X;
            //var top = pointColor.Y;
            using (var frame = reference.ColorFrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    camera.Source = frame.ToBitmap();
                }
            }

            // Body
            using (var frame = reference.BodyFrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    canvas.Children.Clear();

                    _bodies = new Body[frame.BodyFrameSource.BodyCount];

                    frame.GetAndRefreshBodyData(_bodies);


                    #region drawing_ranad


                 

                    Rectangle ranad_teeth1 = new Rectangle();
                    //y - 110
                    DrawingRanadTeeth(ranad_teeth1, 60, 150, 350, 495);
                    canvas.Children.Add(ranad_teeth1);                 
                    //2 y - 110
                    Rectangle ranad_teeth2 = new Rectangle();
                    DrawingRanadTeeth(ranad_teeth2, 60, 150, 410, 510);
                    canvas.Children.Add(ranad_teeth2);
                    //3
                    Rectangle ranad_teeth3 = new Rectangle();
                    DrawingRanadTeeth(ranad_teeth3, 60, 40, 470, 630);
                    canvas.Children.Add(ranad_teeth3);

                    Rectangle ranad_teeth4 = new Rectangle();
                    DrawingRanadTeeth(ranad_teeth4, 60, 40, 530, 640);
                    canvas.Children.Add(ranad_teeth4);

                    Rectangle ranad_teeth5 = new Rectangle();
                    DrawingRanadTeeth(ranad_teeth5, 60, 40, 590, 650);
                    canvas.Children.Add(ranad_teeth5);

                    Rectangle ranad_teeth6 = new Rectangle();
                    DrawingRanadTeeth(ranad_teeth6, 60, 40, 650, 660);
                    canvas.Children.Add(ranad_teeth6);

                    Rectangle ranad_teeth7 = new Rectangle();
                    DrawingRanadTeeth(ranad_teeth7, 60, 40, 710, 665);
                    canvas.Children.Add(ranad_teeth7);

                    Rectangle ranad_teeth8 = new Rectangle();
                    DrawingRanadTeeth(ranad_teeth8, 60, 40, 770, 670);
                    canvas.Children.Add(ranad_teeth8);

                    Rectangle ranad_teeth9 = new Rectangle();
                    DrawingRanadTeeth(ranad_teeth9, 60, 40, 830, 672.5);
                    canvas.Children.Add(ranad_teeth9);

                    Rectangle ranad_teeth10 = new Rectangle();
                    DrawingRanadTeeth(ranad_teeth10, 60, 40, 890, 675);
                    canvas.Children.Add(ranad_teeth10);

                    Rectangle ranad_teeth11 = new Rectangle();
                    DrawingRanadTeeth(ranad_teeth11, 60, 40, 950, 676);
                    canvas.Children.Add(ranad_teeth11);

                    Rectangle ranad_teeth12 = new Rectangle();
                    DrawingRanadTeeth(ranad_teeth12, 60, 40, 1010, 676);
                    canvas.Children.Add(ranad_teeth12);

                    Rectangle ranad_teeth13 = new Rectangle();
                    DrawingRanadTeeth(ranad_teeth13, 60, 40, 1070, 675);
                    canvas.Children.Add(ranad_teeth13);

                    Rectangle ranad_teeth14 = new Rectangle();
                    DrawingRanadTeeth(ranad_teeth14, 60, 40, 1130, 672.5);
                    canvas.Children.Add(ranad_teeth14);

                    Rectangle ranad_teeth15 = new Rectangle();
                    DrawingRanadTeeth(ranad_teeth15, 60, 40, 1190, 670);
                    canvas.Children.Add(ranad_teeth15);

                    Rectangle ranad_teeth16 = new Rectangle();
                    DrawingRanadTeeth(ranad_teeth16, 60, 40, 1250, 665);
                    canvas.Children.Add(ranad_teeth16);
                    //17
                    Rectangle ranad_teeth17 = new Rectangle();
                    DrawingRanadTeeth(ranad_teeth17, 60, 40, 1310, 660);
                    canvas.Children.Add(ranad_teeth17);

                    Rectangle ranad_teeth18 = new Rectangle();
                    DrawingRanadTeeth(ranad_teeth18, 60, 40, 1370, 650);
                    canvas.Children.Add(ranad_teeth18);

                    Rectangle ranad_teeth19 = new Rectangle();
                    DrawingRanadTeeth(ranad_teeth19, 60, 40, 1430, 640);
                    canvas.Children.Add(ranad_teeth19);

                    Rectangle ranad_teeth20 = new Rectangle();
                    DrawingRanadTeeth(ranad_teeth20, 60, 40, 1490, 630);
                    canvas.Children.Add(ranad_teeth20);

                    Rectangle ranad_teeth21 = new Rectangle();
                    DrawingRanadTeeth(ranad_teeth21, 60, 40, 1550, 620);
                    canvas.Children.Add(ranad_teeth21);

                    Rectangle ranad_teeth22 = new Rectangle();
                    DrawingRanadTeeth(ranad_teeth22, 60, 40, 1610, 605);
                    canvas.Children.Add(ranad_teeth22);

                    #endregion

                    #region drawing_circle_example

                    //Draw the drum
                    //Ellipse circle1 = new Ellipse();
                    //circle1.StrokeThickness = 5;
                    //circle1.Stroke = Brushes.Black;

                    //circle1.Width = 140;
                    //circle1.Height = 55;
                    //Canvas.SetLeft(circle1, 120);
                    //Canvas.SetTop(circle1, 600);
                    //canvas.Children.Add(circle1);

                    //Ellipse circle2 = new Ellipse();
                    //circle2.StrokeThickness = 5;
                    //circle2.Stroke = Brushes.Black;
                    //circle2.Width = 180;
                    //circle2.Height = 70;
                    //Canvas.SetLeft(circle2, 270);
                    //Canvas.SetTop(circle2, 605);
                    //canvas.Children.Add(circle2);

                    //Ellipse circle3 = new Ellipse();
                    //circle3.StrokeThickness = 5;
                    //circle3.Stroke = Brushes.Black;
                    //circle3.Width = 180;
                    //circle3.Height = 70;
                    //Canvas.SetLeft(circle3, 410);
                    //Canvas.SetTop(circle3, 620);
                    //canvas.Children.Add(circle3);
                    
                    #endregion

                    foreach (var body in _bodies)
                    {
                        if (body != null)

                        {
                            if (body.IsTracked)
                                
                            {

                                // Find the joints
                               
                                var HandRight = body.Joints[JointType.HandRight];//new code
                                //Joint handTipRight = body.Joints[JointType.HandTipRight];
                                //Joint thumbRight = body.Joints[JointType.ThumbRight];
                                double rightX = Math.Round(HandRight.Position.X * 100, 2);
                                double rightY = Math.Round(HandRight.Position.Y * -100, 2);
                                double rightZ = Math.Round(HandRight.Position.Z ,2);

                                var HandLeft = body.Joints[JointType.HandLeft];//new code
                                //Joint handTipLeft = body.Joints[JointType.HandTipLeft];
                                //Joint thumbLeft = body.Joints[JointType.ThumbLeft];
                                double leftX = Math.Round(HandLeft.Position.X * -100, 2);
                                double leftY = Math.Round(HandLeft.Position.Y * -100, 2);
                                double leftZ = Math.Round(HandLeft.Position.Z  , 2);

                                // Draw hands and thumbs

                                //canvas.DrawSkeleton(body,_sensor.CoordinateMapper);
                                canvas.DrawPoint(HandRight, _sensor.CoordinateMapper);
                                canvas.DrawPoint(HandLeft, _sensor.CoordinateMapper);
                                //canvas.DrawSkeleton(body, _sensor.CoordinateMapper);
                                //canvas.DrawThumb(thumbRight, _sensor.CoordinateMapper);
                                //canvas.DrawThumb(thumbLeft, _sensor.CoordinateMapper);

                                //double hitFirstMax = 70;
                                //double hitFirstMin = 60;

                                //set direction
                                //double direction = 1;

                                //if (leftY > 5)
                                //{
                                //    direction = 1;
                                //}
                                //else
                                //{
                                 //   direction = 0;
                                //}


                                // Find the hand stys
                                string rightHandState = "-";
                                string leftHandState = "-";


                                rightHandState = System.Convert.ToString(rightX + " , " + rightY+"\n "+ rightZ);
                                leftHandState = System.Convert.ToString(leftX + " , " + leftY + "\n " + leftZ);





                                tblRightHandState.Text = rightHandState;
                                tblLeftHandState.Text = leftHandState;

                                #region testFindPositionForEachTeeth



                                #endregion
                                
                                #region notDoneSoundRecordSection

                                if ((leftX <= 78) && (leftX >= 74) && ((leftY >= -10) &&(leftY<=8)))
                                {
                                    System.Media.SoundPlayer teeth1_Sound = new System.Media.SoundPlayer(@"E:\soundForKinect\1.wav");
                                    teeth1_Sound.Play();
                                }
                                else if ((leftX <= 55) && (leftX >= 51) && ((leftY >= 8) && (leftY <= 11)))
                                {
                                    System.Media.SoundPlayer teeth2_Sound = new System.Media.SoundPlayer(@"E:\soundForKinect\2.wav");
                                    teeth2_Sound.Play();
                                }
                                else if ((leftX <= 30) && (leftX >= 20) && ((leftY >= 7.9) && (leftY <= 10.1)))
                                {
                                    System.Media.SoundPlayer teeth3_Sound = new System.Media.SoundPlayer(@"E:\soundForKinect\3.wav");
                                    teeth3_Sound.Play();
                                }
                                else if ((leftX <= 30) && (leftX >= 20) && ((leftY >= 7.9) && (leftY <= 10.1)))
                                {
                                    System.Media.SoundPlayer teeth4_Sound = new System.Media.SoundPlayer(@"E:\soundForKinect\4.wav");
                                    teeth4_Sound.Play();
                                }
                                else if ((leftX <= 30) && (leftX >= 20) && ((leftY >= 7.9) && (leftY <= 10.1)))
                                {
                                    System.Media.SoundPlayer teeth5_Sound = new System.Media.SoundPlayer(@"E:\soundForKinect\5.wav");
                                    teeth5_Sound.Play();
                                }
                                else if ((leftX <= 30) && (leftX >= 20) && ((leftY >= 7.9) && (leftY <= 10.1)))
                                {
                                    System.Media.SoundPlayer teeth6_Sound = new System.Media.SoundPlayer(@"E:\soundForKinect\6.wav");
                                    teeth6_Sound.Play();
                                }
                                else if ((leftX <= 30) && (leftX >= 20) && ((leftY >= 7.9) && (leftY <= 10.1)))
                                {
                                    System.Media.SoundPlayer teeth7_Sound = new System.Media.SoundPlayer(@"E:\soundForKinect\7.wav");
                                    teeth7_Sound.Play();
                                }
                                else if ((leftX <= 30) && (leftX >= 20) && ((leftY >= 7.9) && (leftY <= 10.1)))
                                {
                                    System.Media.SoundPlayer teeth8_Sound = new System.Media.SoundPlayer(@"E:\soundForKinect\8.wav");
                                    teeth8_Sound.Play();
                                }
                                else if ((leftX <= 30) && (leftX >= 20) && ((leftY >= 7.9) && (leftY <= 10.1)))
                                {
                                    System.Media.SoundPlayer teeth9_Sound = new System.Media.SoundPlayer(@"E:\soundForKinect\9.wav");
                                    teeth9_Sound.Play();
                                }
                                else if ((leftX <= 30) && (leftX >= 20) && ((leftY >= 7.9) && (leftY <= 10.1)))
                                {
                                    System.Media.SoundPlayer teeth10_Sound = new System.Media.SoundPlayer(@"E:\soundForKinect\10.wav");
                                    teeth10_Sound.Play();
                                }
                                else if ((leftX <= 30) && (leftX >= 20) && ((leftY >= 7.9) && (leftY <= 10.1)))
                                {
                                    System.Media.SoundPlayer teeth11_Sound = new System.Media.SoundPlayer(@"E:\soundForKinect\11.wav");
                                    teeth11_Sound.Play();
                                }
                                else if ((leftX <= 30) && (leftX >= 20) && ((leftY >= 7.9) && (leftY <= 10.1)))
                                {
                                    System.Media.SoundPlayer teeth12_Sound = new System.Media.SoundPlayer(@"E:\soundForKinect\12.wav");
                                    teeth12_Sound.Play();
                                }
                                else if ((leftX <= 30) && (leftX >= 20) && ((leftY >= 7.9) && (leftY <= 10.1)))
                                {
                                    System.Media.SoundPlayer teeth13_Sound = new System.Media.SoundPlayer(@"E:\soundForKinect\13.wav");
                                    teeth13_Sound.Play();
                                }
                                else if ((leftX <= 30) && (leftX >= 20) && ((leftY >= 7.9) && (leftY <= 10.1)))
                                {
                                    System.Media.SoundPlayer teeth14_Sound = new System.Media.SoundPlayer(@"E:\soundForKinect\14.wav");
                                    teeth14_Sound.Play();
                                }
                                else if ((leftX <= 30) && (leftX >= 20) && ((leftY >= 7.9) && (leftY <= 10.1)))
                                {
                                    System.Media.SoundPlayer teeth15_Sound = new System.Media.SoundPlayer(@"E:\soundForKinect\15.wav");
                                    teeth15_Sound.Play();
                                }
                                else if ((leftX <= 30) && (leftX >= 20) && ((leftY >= 7.9) && (leftY <= 10.1)))
                                {
                                    System.Media.SoundPlayer teeth16_Sound = new System.Media.SoundPlayer(@"E:\soundForKinect\16.wav");
                                    teeth16_Sound.Play();
                                }
                                else if ((leftX <= 30) && (leftX >= 20) && ((leftY >= 7.9) && (leftY <= 10.1)))
                                {
                                    System.Media.SoundPlayer teeth17_Sound = new System.Media.SoundPlayer(@"E:\soundForKinect\17.wav");
                                    teeth17_Sound.Play();
                                }
                                else if ((leftX <= 30) && (leftX >= 20) && ((leftY >= 7.9) && (leftY <= 10.1)))
                                {
                                    System.Media.SoundPlayer teeth18_Sound = new System.Media.SoundPlayer(@"E:\soundForKinect\18.wav");
                                    teeth18_Sound.Play();
                                }
                                else if ((leftX <= 30) && (leftX >= 20) && ((leftY >= 7.9) && (leftY <= 10.1)))
                                {
                                    System.Media.SoundPlayer teeth19_Sound = new System.Media.SoundPlayer(@"E:\soundForKinect\19.wav");
                                    teeth19_Sound.Play();
                                }
                                else if ((leftX <= 30) && (leftX >= 20) && ((leftY >= 7.9) && (leftY <= 10.1)))
                                {
                                    System.Media.SoundPlayer teeth20_Sound = new System.Media.SoundPlayer(@"E:\soundForKinect\20.wav");
                                    teeth20_Sound.Play();
                                }
                                else if ((leftX <= 30) && (leftX >= 20) && ((leftY >= 7.9) && (leftY <= 10.1)))
                                {
                                    System.Media.SoundPlayer teeth21_Sound = new System.Media.SoundPlayer(@"E:\soundForKinect\21.wav");
                                    teeth21_Sound.Play();
                                }
                                else if ((leftX <= 30) && (leftX >= 20) && ((leftY >= 7.9) && (leftY <= 10.1)))
                                {
                                    System.Media.SoundPlayer teeth22_Sound = new System.Media.SoundPlayer(@"E:\soundForKinect\22.wav");
                                    teeth22_Sound.Play();
                                }

                                #endregion

                            }
                        }
                    }
                }
            }
        }

        #endregion

        void kinectButton_Click(object sender, RoutedEventArgs e)
        {
            selected.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, selected));
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
           // message.Content = "Button 1 clicked!";
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            //message.Content = "Button 2 clicked!";
        }

        private void quitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            selected.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, selected));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Well done!");
        }
    }
}
