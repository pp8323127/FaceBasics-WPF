//------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Microsoft.Samples.Kinect.FaceBasics
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Media.Media3D;
    using Microsoft.Kinect;
    using Microsoft.Kinect.Face;

    using Microsoft.ProjectOxford.Face;
    using Microsoft.ProjectOxford.Face.Contract;
    using System.Linq;

    using System.Net;
    using System.Text.RegularExpressions;

    using System.Threading.Tasks;

    using System.Windows.Media.Animation;
    using System.Windows.Controls;

    using Microsoft.Kinect.Wpf.Controls;
    using Microsoft.Samples.Kinect.ControlsBasics.DataModel;

    /// <summary>
    /// Interaction logic for MainWindow
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        /// <summary>
        /// Thickness of face bounding box and face points
        /// </summary>
        private const double DrawFaceShapeThickness = 8;

        /// <summary>
        /// Font size of face property text 
        /// </summary>
        /// 30
        private const double DrawTextFontSize = 40;

        /// <summary>
        /// Radius of face point circle
        /// </summary>
        private const double FacePointRadius = 1.0;

        /// <summary>
        /// Text layout offset in X axis
        /// </summary>
        /// -0.1f
        private const float TextLayoutOffsetX = -0.08f;

        /// <summary>
        /// Text layout offset in Y axis
        /// </summary>
        /// -0.15f
        private const float TextLayoutOffsetY = 0.16f;

        /// <summary>
        /// Face rotation display angle increment in degrees
        /// </summary>
        private const double FaceRotationIncrementInDegrees = 5.0;

        /// <summary>
        /// Formatted text to indicate that there are no bodies/faces tracked in the FOV
        /// </summary>
        /// No bodies or faces are tracked ...
        private FormattedText textFaceNotTracked = new FormattedText(
                        "No Faces...",
                        CultureInfo.GetCultureInfo("en-us"),
                        FlowDirection.LeftToRight,
                        new Typeface("Microsoft JhengHei"),
                        DrawTextFontSize,
                        Brushes.White);

        /// <summary>
        /// Text layout for the no face tracked message
        /// </summary>
        private Point textLayoutFaceNotTracked = new Point(10.0, 10.0);

        /// <summary>
        /// Drawing group for body rendering output
        /// </summary>
        private DrawingGroup drawingGroup;

        /// <summary>
        /// Drawing image that we will display
        /// </summary>
        private DrawingImage imageSource;

        /// <summary>
        /// Active Kinect sensor
        /// </summary>
        private KinectSensor kinectSensor = null;

        /// <summary>
        /// Coordinate mapper to map one type of point to another
        /// </summary>
        private CoordinateMapper coordinateMapper = null;

        /// <summary>
        /// Reader for body frames
        /// </summary>
        private BodyFrameReader bodyFrameReader = null;

        /// <summary>
        /// Array to store bodies
        /// </summary>
        private Body[] bodies = null;

        /// <summary>
        /// Number of bodies tracked
        /// </summary>
        private int bodyCount;

        /// <summary>
        /// Face frame sources
        /// </summary>
        private FaceFrameSource[] faceFrameSources = null;

        /// <summary>
        /// Face frame readers
        /// </summary>
        private FaceFrameReader[] faceFrameReaders = null;

        /// <summary>
        /// Storage for face frame results
        /// </summary>
        private FaceFrameResult[] faceFrameResults = null;

        /// <summary>
        /// Width of display (color space)
        /// </summary>
        private int displayWidth;

        /// <summary>
        /// Height of display (color space)
        /// </summary>
        private int displayHeight;

        /// <summary>
        /// Display rectangle
        /// </summary>
        private Rect displayRect;

        /// <summary>
        /// List of brushes for each face tracked
        /// </summary>
        private List<Brush> faceBrush;

        /// <summary>
        /// Current status text to display
        /// </summary>
        private string statusText = null;




        //---------ColorFrame----------//

        /// <summary>
        /// Reader for color frames
        /// </summary>
        private ColorFrameReader colorFrameReader = null;

        /// <summary>
        /// Bitmap to display
        /// </summary>
        private WriteableBitmap colorBitmap = null;





        //---------SkeletonFrame----------//

        /// <summary>
        /// Radius of drawn hand circles 手部大小
        /// </summary> 30
        private const double HandSize = 100;

        /// <summary>
        /// Thickness of drawn joint lines 關節點大小
        /// </summary> 3
        private const double JointThickness = 15;

        /// <summary>
        /// Thickness of clip edge rectangles 邊界
        /// </summary> 10
        private const double ClipBoundsThickness = 20;

        /// <summary>
        /// Constant for clamping Z values of camera space points from being negative
        /// </summary>
        private const float InferredZPositionClamp = 0.1f;

        /// <summary>
        /// Brush used for drawing hands that are currently tracked as closed 手打開的顏色
        /// </summary>
        private readonly Brush handClosedBrush = new SolidColorBrush(Color.FromArgb(128, 255, 0, 0));

        /// <summary>
        /// Brush used for drawing hands that are currently tracked as opened 手握拳的顏色
        /// </summary>
        private readonly Brush handOpenBrush = new SolidColorBrush(Color.FromArgb(128, 0, 255, 0));

        /// <summary>
        /// Brush used for drawing hands that are currently tracked as in lasso (pointer) position 手部點不完全的顏色
        /// </summary>
        private readonly Brush handLassoBrush = new SolidColorBrush(Color.FromArgb(128, 0, 0, 255));

        /// <summary>
        /// Brush used for drawing joints that are currently tracked
        /// </summary>
        private readonly Brush trackedJointBrush = new SolidColorBrush(Color.FromArgb(255, 68, 192, 68));

        /// <summary>
        /// Brush used for drawing joints that are currently inferred 估計的關節點顏色
        /// </summary>        
        private readonly Brush inferredJointBrush = Brushes.Yellow;

        /// <summary>
        /// Pen used for drawing bones that are currently inferred 估計的骨架連線顏色
        /// </summary>        
        private readonly Pen inferredBonePen = new Pen(Brushes.Gray, 1);


        /// <summary>
        /// definition of bones
        /// </summary>
        private List<Tuple<JointType, JointType>> bones;

        /// <summary>
        /// Width of display (depth space)
        /// </summary>
        private int DepthdisplayWidth;

        /// <summary>
        /// Height of display (depth space)
        /// </summary>
        private int DepthdisplayHeight;

        /// <summary>
        /// List of colors for each body tracked
        /// </summary>
        private List<Pen> bodyColors;






        //---------CoordinateMappingBasics-WPF----------//

        /// <summary>
        /// Size of the RGB pixel in the bitmap
        /// </summary>
        private readonly int bytesPerPixel = (PixelFormats.Bgr32.BitsPerPixel + 7) / 8;

        /// <summary>
        /// Reader for depth/color/body index frames
        /// </summary>
        private MultiSourceFrameReader multiFrameSourceReader = null;

        /// <summary>
        /// The size in bytes of the bitmap back buffer
        /// </summary>
        private uint bitmapBackBufferSize = 0;

        /// <summary>
        /// Intermediate storage for the color to depth mapping
        /// </summary>
        private DepthSpacePoint[] colorMappedToDepthPoints = null;

        /// <summary>
        /// Bitmap to display
        /// </summary>
        private WriteableBitmap bitmap = null;









        //使用 subscription key，Microsoft Face API Key
        private readonly IFaceServiceClient faceServiceClent =
            new FaceServiceClient("4b84a43021ee4799bb07ef07a1fe91f5");


        // custome
        private int numFace = 0;
        private int nowBody = 0;
        private ulong[] saveTrackingID = null;
        private string[] DetectAgeGenderResult;
        private string[] DetectAgeResult;
        private string[] DetectGenderResult;

        private bool doClothes = false;
        private bool trackID = false;
        int nowTrackIndex = 0;
        ulong? nowTrackID = null;
        private string clothes_keyword_result = null;
        private string clothes_fileName = null;
        bool doDetect = true;


        Body[] Prebody = new Body[6];
        List<Body[]> preBody = new List<Body[]>();
        List<CameraSpacePoint>[] HandLeftMotion = new List<CameraSpacePoint>[6];
        List<CameraSpacePoint>[] HandRightMotion = new List<CameraSpacePoint>[6];
        private double distance(CameraSpacePoint pre, CameraSpacePoint aft)
        {
            double X = pre.X - aft.X;
            double Y = pre.Y - aft.Y;
            double Z = pre.Z - aft.Z;
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }




        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            // one sensor is currently supported
            this.kinectSensor = KinectSensor.GetDefault();

            // get the coordinate mapper
            this.coordinateMapper = this.kinectSensor.CoordinateMapper;

            // get the color frame details
            FrameDescription frameDescription = this.kinectSensor.ColorFrameSource.FrameDescription;

            // set the display specifics
            this.displayWidth = frameDescription.Width;
            this.displayHeight = frameDescription.Height;
            this.displayRect = new Rect(0.0, 0.0, this.displayWidth, this.displayHeight);

            // open the reader for the body frames
            this.bodyFrameReader = this.kinectSensor.BodyFrameSource.OpenReader();

            // wire handler for body frame arrival
            this.bodyFrameReader.FrameArrived += this.Reader_BodyFrameArrived;

            // set the maximum number of bodies that would be tracked by Kinect
            this.bodyCount = this.kinectSensor.BodyFrameSource.BodyCount;
            //this.bodyCount = 2;

            // allocate storage to store body objects
            this.bodies = new Body[this.bodyCount];

            // 建立儲存TrackingID的地方
            this.saveTrackingID = new ulong[this.bodyCount];

            // 建立儲存辨識結果的地方(依faceIndex)
            this.DetectAgeGenderResult = new string[this.bodyCount];
            this.DetectGenderResult = new string[this.bodyCount];
            this.DetectAgeResult = new string[this.bodyCount];

            // specify the required face frame results
            FaceFrameFeatures faceFrameFeatures =
                FaceFrameFeatures.BoundingBoxInColorSpace
                | FaceFrameFeatures.PointsInColorSpace
                | FaceFrameFeatures.RotationOrientation
                | FaceFrameFeatures.FaceEngagement
                | FaceFrameFeatures.Glasses
                | FaceFrameFeatures.Happy
                | FaceFrameFeatures.LeftEyeClosed
                | FaceFrameFeatures.RightEyeClosed
                | FaceFrameFeatures.LookingAway
                | FaceFrameFeatures.MouthMoved
                | FaceFrameFeatures.MouthOpen;

            // create a face frame source + reader to track each face in the FOV
            this.faceFrameSources = new FaceFrameSource[this.bodyCount];
            this.faceFrameReaders = new FaceFrameReader[this.bodyCount];
            for (int i = 0; i < this.bodyCount; i++)
            {
                // create the face frame source with the required face frame features and an initial tracking Id of 0
                this.faceFrameSources[i] = new FaceFrameSource(this.kinectSensor, 0, faceFrameFeatures);

                // open the corresponding reader
                this.faceFrameReaders[i] = this.faceFrameSources[i].OpenReader();
            }

            // allocate storage to store face frame results for each face in the FOV
            this.faceFrameResults = new FaceFrameResult[this.bodyCount];

            // populate face result colors - one for each face index
            this.faceBrush = new List<Brush>()
            {
                Brushes.White,
                Brushes.Orange,
                Brushes.LightGreen,
                Brushes.Red,
                Brushes.LightBlue,
                Brushes.Yellow
            };




            //---------ColorFrame----------//

            // open the reader for the color frames
            this.colorFrameReader = this.kinectSensor.ColorFrameSource.OpenReader();

            // wire handler for frame arrival
            this.colorFrameReader.FrameArrived += this.Reader_ColorFrameArrived;

            // create the colorFrameDescription from the ColorFrameSource using Bgra format
            FrameDescription colorFrameDescription = this.kinectSensor.ColorFrameSource.CreateFrameDescription(ColorImageFormat.Bgra);

            // create the bitmap to display
            this.colorBitmap = new WriteableBitmap(colorFrameDescription.Width, colorFrameDescription.Height, 96.0, 96.0, PixelFormats.Bgr32, null);




            //---------SkeletonFrame----------//


            // get the depth (display) extents
            FrameDescription DepthframeDescription = this.kinectSensor.DepthFrameSource.FrameDescription;

            this.DepthdisplayWidth = colorFrameDescription.Width;
            this.DepthdisplayHeight = colorFrameDescription.Height;

            // a bone defined as a line between two joints
            this.bones = new List<Tuple<JointType, JointType>>();

            // Torso
            this.bones.Add(new Tuple<JointType, JointType>(JointType.Head, JointType.Neck));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.Neck, JointType.SpineShoulder));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineShoulder, JointType.SpineMid));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineMid, JointType.SpineBase));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineShoulder, JointType.ShoulderRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineShoulder, JointType.ShoulderLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineBase, JointType.HipRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineBase, JointType.HipLeft));

            // Right Arm
            this.bones.Add(new Tuple<JointType, JointType>(JointType.ShoulderRight, JointType.ElbowRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.ElbowRight, JointType.WristRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.WristRight, JointType.HandRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.HandRight, JointType.HandTipRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.WristRight, JointType.ThumbRight));

            // Left Arm
            this.bones.Add(new Tuple<JointType, JointType>(JointType.ShoulderLeft, JointType.ElbowLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.ElbowLeft, JointType.WristLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.WristLeft, JointType.HandLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.HandLeft, JointType.HandTipLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.WristLeft, JointType.ThumbLeft));

            // Right Leg
            this.bones.Add(new Tuple<JointType, JointType>(JointType.HipRight, JointType.KneeRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.KneeRight, JointType.AnkleRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.AnkleRight, JointType.FootRight));

            // Left Leg
            this.bones.Add(new Tuple<JointType, JointType>(JointType.HipLeft, JointType.KneeLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.KneeLeft, JointType.AnkleLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.AnkleLeft, JointType.FootLeft));

            // populate body colors, one for each BodyIndex
            this.bodyColors = new List<Pen>();

            //this.bodyColors.Add(new Pen(Brushes.Red, 15));
            //this.bodyColors.Add(new Pen(Brushes.Orange, 15));
            //this.bodyColors.Add(new Pen(Brushes.Green, 15));
            //this.bodyColors.Add(new Pen(Brushes.Blue, 15));
            //this.bodyColors.Add(new Pen(Brushes.Indigo, 15));
            //this.bodyColors.Add(new Pen(Brushes.Violet, 15));

            this.bodyColors.Add(new Pen(Brushes.White, 12));
            this.bodyColors.Add(new Pen(Brushes.Orange, 12));
            this.bodyColors.Add(new Pen(Brushes.Indigo, 12));
            this.bodyColors.Add(new Pen(Brushes.Red, 12));
            this.bodyColors.Add(new Pen(Brushes.LightBlue, 12));
            this.bodyColors.Add(new Pen(Brushes.Yellow, 12));




            // set IsAvailableChanged event notifier
            this.kinectSensor.IsAvailableChanged += this.Sensor_IsAvailableChanged;

            // open the sensor
            this.kinectSensor.Open();

            // set the status text
            this.StatusText = this.kinectSensor.IsAvailable ? Properties.Resources.RunningStatusText
                                                            : Properties.Resources.NoSensorStatusText;

            // Create the drawing group we'll use for drawing
            this.drawingGroup = new DrawingGroup();

            // Create an image source that we can use in our image control
            this.imageSource = new DrawingImage(this.drawingGroup);

            // use the window object as the view model in this simple example
            this.DataContext = this;







            //---------CoordinateMappingBasics-WPF----------//
            this.multiFrameSourceReader = this.kinectSensor.OpenMultiSourceFrameReader(FrameSourceTypes.Depth | FrameSourceTypes.Color | FrameSourceTypes.BodyIndex);

            //this.multiFrameSourceReader.MultiSourceFrameArrived += this.Reader_MultiSourceFrameArrived;

            this.coordinateMapper = this.kinectSensor.CoordinateMapper;

            FrameDescription depthFrameDescription = this.kinectSensor.DepthFrameSource.FrameDescription;

            int depthWidth = depthFrameDescription.Width;
            int depthHeight = depthFrameDescription.Height;

            int colorWidth = colorFrameDescription.Width;
            int colorHeight = colorFrameDescription.Height;

            this.colorMappedToDepthPoints = new DepthSpacePoint[colorWidth * colorHeight];

            this.bitmap = new WriteableBitmap(colorWidth, colorHeight, 96.0, 96.0, PixelFormats.Bgra32, null);

            // Calculate the WriteableBitmap back buffer size
            this.bitmapBackBufferSize = (uint)((this.bitmap.BackBufferStride * (this.bitmap.PixelHeight - 1)) + (this.bitmap.PixelWidth * this.bytesPerPixel));


            // showClothes BitmampImage
            BitmapImage bi = new BitmapImage();




            // 啟動第三方程式，開啟WebServer_MicroHttpServer
            Process[] MyProcess = Process.GetProcessesByName("MicroHttpServer");
            if (MyProcess.Length == 0)
            {
                //Process.Start(@"D:\Documents\Visual Studio 2015\Projects\MicroHttpServer\MicroHttpServer\bin\Debug\MicroHttpServer.exe");
            }


            BodyFrameReader bfr = this.kinectSensor.BodyFrameSource.OpenReader();
            bfr.FrameArrived += Bfr_FrameArrived;

            // hand track
            for (int i = 0; i < HandLeftMotion.Length; i++)
            {
                HandLeftMotion[i] = new List<CameraSpacePoint>();
            }
            for (int i = 0; i < HandRightMotion.Length; i++)
            {
                HandRightMotion[i] = new List<CameraSpacePoint>();
            }

            
            // initialize the components (controls) of the window
            this.InitializeComponent();



            // Add in display content
            var sampleDataSource = SampleDataSource.GetGroup("Group-1");
            this.itemsControl.ItemsSource = sampleDataSource;
            
        }

        /// <summary>
        /// INotifyPropertyChangedPropertyChanged event to allow window controls to bind to changeable data
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the bitmap to display
        /// </summary>
        public ImageSource ImageSource
        {
            get
            {
                return this.imageSource;
                //return this.colorBitmap;
            }
        }

        /// <summary>
        /// Gets the bitmap to display
        /// </summary>
        public ImageSource ImageSource_coor
        {
            get
            {
                return this.bitmap;
            }
        }


        /// <summary>
        /// Gets the bitmap to display
        /// </summary>
        //public ImageSource ImageSource
        //{
        //    get
        //    {
        //        return this.colorBitmap;
        //    }
        //}




        /// <summary>
        /// Gets or sets the current status text to display
        /// </summary>
        public string StatusText
        {
            get
            {
                return this.statusText;
            }

            set
            {
                if (this.statusText != value)
                {
                    this.statusText = value;

                    // notify any bound elements that the text has changed
                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("StatusText"));
                    }
                }
            }
        }

        /// <summary>
        /// Converts rotation quaternion to Euler angles 
        /// And then maps them to a specified range of values to control the refresh rate
        /// </summary>
        /// <param name="rotQuaternion">face rotation quaternion</param>
        /// <param name="pitch">rotation about the X-axis</param>
        /// <param name="yaw">rotation about the Y-axis</param>
        /// <param name="roll">rotation about the Z-axis</param>
        private static void ExtractFaceRotationInDegrees(Vector4 rotQuaternion, out int pitch, out int yaw, out int roll)
        {
            double x = rotQuaternion.X;
            double y = rotQuaternion.Y;
            double z = rotQuaternion.Z;
            double w = rotQuaternion.W;

            // convert face rotation quaternion to Euler angles in degrees
            double yawD, pitchD, rollD;
            pitchD = Math.Atan2(2 * ((y * z) + (w * x)), (w * w) - (x * x) - (y * y) + (z * z)) / Math.PI * 180.0;
            yawD = Math.Asin(2 * ((w * y) - (x * z))) / Math.PI * 180.0;
            rollD = Math.Atan2(2 * ((x * y) + (w * z)), (w * w) + (x * x) - (y * y) - (z * z)) / Math.PI * 180.0;

            // clamp the values to a multiple of the specified increment to control the refresh rate
            double increment = FaceRotationIncrementInDegrees;
            pitch = (int)(Math.Floor((pitchD + ((increment / 2.0) * (pitchD > 0 ? 1.0 : -1.0))) / increment) * increment);
            yaw = (int)(Math.Floor((yawD + ((increment / 2.0) * (yawD > 0 ? 1.0 : -1.0))) / increment) * increment);
            roll = (int)(Math.Floor((rollD + ((increment / 2.0) * (rollD > 0 ? 1.0 : -1.0))) / increment) * increment);
        }

        /// <summary>
        /// Execute start up tasks
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < this.bodyCount; i++)
            {
                if (this.faceFrameReaders[i] != null)
                {
                    // wire handler for face frame arrival
                    this.faceFrameReaders[i].FrameArrived += this.Reader_FaceFrameArrived;
                }
            }

            if (this.bodyFrameReader != null)
            {
                // wire handler for body frame arrival
                this.bodyFrameReader.FrameArrived += this.Reader_BodyFrameArrived;
            }
        }

        /// <summary>
        /// Execute shutdown tasks
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            for (int i = 0; i < this.bodyCount; i++)
            {
                if (this.faceFrameReaders[i] != null)
                {
                    // FaceFrameReader is IDisposable
                    this.faceFrameReaders[i].Dispose();
                    this.faceFrameReaders[i] = null;
                }

                if (this.faceFrameSources[i] != null)
                {
                    // FaceFrameSource is IDisposable
                    this.faceFrameSources[i].Dispose();
                    this.faceFrameSources[i] = null;
                }
            }

            if (this.bodyFrameReader != null)
            {
                // BodyFrameReader is IDisposable
                this.bodyFrameReader.Dispose();
                this.bodyFrameReader = null;
            }

            if (this.kinectSensor != null)
            {
                this.kinectSensor.Close();
                this.kinectSensor = null;
            }



            if (this.colorFrameReader != null)
            {
                // ColorFrameReder is IDisposable
                this.colorFrameReader.Dispose();
                this.colorFrameReader = null;
            }

            if (this.kinectSensor != null)
            {
                this.kinectSensor.Close();
                this.kinectSensor = null;
            }


            if (this.multiFrameSourceReader != null)
            {
                // MultiSourceFrameReder is IDisposable
                this.multiFrameSourceReader.Dispose();
                this.multiFrameSourceReader = null;
            }


            // 關閉第三方程式，WebServer_MicroHttpServer
            Process[] MyProcess = Process.GetProcessesByName("MicroHttpServer");
            if (MyProcess.Length > 0)
            {
                MyProcess[0].Kill();
            }

            // 結束程式後，刪除jpg跟txt檔
            delete_file();

        }



        //---------ColorFrame----------//

        /// <summary>
        /// Handles the color frame data arriving from the sensor
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void Reader_ColorFrameArrived(object sender, ColorFrameArrivedEventArgs e)
        {
            // ColorFrame is IDisposable
            using (ColorFrame colorFrame = e.FrameReference.AcquireFrame())
            {
                if (colorFrame != null)
                {
                    FrameDescription colorFrameDescription = colorFrame.FrameDescription;

                    using (KinectBuffer colorBuffer = colorFrame.LockRawImageBuffer())
                    {
                        this.colorBitmap.Lock();

                        // verify data and write the new color frame data to the display bitmap
                        if ((colorFrameDescription.Width == this.colorBitmap.PixelWidth) && (colorFrameDescription.Height == this.colorBitmap.PixelHeight))
                        {
                            colorFrame.CopyConvertedFrameDataToIntPtr(
                                this.colorBitmap.BackBuffer,
                                (uint)(colorFrameDescription.Width * colorFrameDescription.Height * 4),
                                ColorImageFormat.Bgra);

                            this.colorBitmap.AddDirtyRect(new Int32Rect(0, 0, this.colorBitmap.PixelWidth, this.colorBitmap.PixelHeight));
                        }

                        this.colorBitmap.Unlock();
                    }
                }
            }
        }





        /// <summary>
        /// Handles the face frame data arriving from the sensor
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void Reader_FaceFrameArrived(object sender, FaceFrameArrivedEventArgs e)
        {
            using (FaceFrame faceFrame = e.FrameReference.AcquireFrame())
            {
                if (faceFrame != null)
                {
                    // get the index of the face source from the face source array
                    int index = this.GetFaceSourceIndex(faceFrame.FaceFrameSource);

                    // check if this face frame has valid face frame results
                    if (this.ValidateFaceBoxAndPoints(faceFrame.FaceFrameResult))
                    {
                        // store this face frame result to draw later
                        this.faceFrameResults[index] = faceFrame.FaceFrameResult;
                    }
                    else
                    {
                        // indicates that the latest face frame result from this reader is invalid
                        this.faceFrameResults[index] = null;
                    }
                }
            }
        }

        /// <summary>
        /// Returns the index of the face frame source
        /// </summary>
        /// <param name="faceFrameSource">the face frame source</param>
        /// <returns>the index of the face source in the face source array</returns>
        private int GetFaceSourceIndex(FaceFrameSource faceFrameSource)
        {
            int index = -1;

            for (int i = 0; i < this.bodyCount; i++)
            {
                if (this.faceFrameSources[i] == faceFrameSource)
                {
                    index = i;
                    break;
                }
            }

            return index;
        }


        // 手勢辨識
        private void Bfr_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            using (BodyFrame bf = e.FrameReference.AcquireFrame())
            {
                if (bf != null)
                {
                    Body[] bodies = new Body[bf.BodyCount];

                    bf.GetAndRefreshBodyData(bodies);

                    if (bodies[nowTrackIndex].IsTracked)
                    {
                        if (Prebody[nowTrackIndex] != null)
                        {

                            /*
                            ColorSpacePoint csp = coordinateMapper.MapCameraPointToColorSpace(Prebody[nowTrackIndex].Joints[JointType.HandRight].Position);
                            var point = PointToScreen(new Point { X = csp.X, Y=csp.Y });
                            */

                            //textBox2.Text = Prebody[nowTrackIndex].Joints[JointType.HandLeft].Position.X + "  " + Prebody[nowTrackIndex].Joints[JointType.HandLeft].Position.Y + "\n";
                            //textBox2.Text += bodies[nowTrackIndex].Joints[JointType.HandLeft].Position.X + "  " + bodies[nowTrackIndex].Joints[JointType.HandLeft].Position.Y;



                            double dis_HandRight = distance(Prebody[nowTrackIndex].Joints[JointType.HandRight].Position, bodies[nowTrackIndex].Joints[JointType.HandRight].Position) * 100;
                            if (dis_HandRight > 6)
                            {
                                if (HandRightMotion[nowTrackIndex].Count == 0)
                                {
                                    HandRightMotion[nowTrackIndex].Add(bodies[nowTrackIndex].Joints[JointType.HandRight].Position);
                                }
                            }
                            else
                            {
                                if (HandRightMotion[nowTrackIndex].Count != 0)
                                {
                                    HandRightMotion[nowTrackIndex].Add(bodies[nowTrackIndex].Joints[JointType.HandRight].Position);
                                    double subX = (HandRightMotion[nowTrackIndex][0].X - HandRightMotion[nowTrackIndex][1].X) * 100;
                                    double subY = (HandRightMotion[nowTrackIndex][0].Y - HandRightMotion[nowTrackIndex][1].Y) * 100;


                                    if (subX > 16 && subY < 8 && subY > -8)
                                    {
                                        //SendKeys.SendWait("{LEFT}");
                                        textBox2.Text = "LEFT";
                                        hand_left();
                                    }
                                    else if (subX < -18 && subY < 8 && subY > -8)
                                    {
                                        //SendKeys.SendWait("{RIGHT}");
                                        //textBox2.Text = "RIGHT";
                                        //hand_right();
                                    }

                                    HandRightMotion[nowTrackIndex].Clear();
                                }
                            }
                            //Prebody[nowTrackIndex] = bodies[nowTrackIndex];



                            double dis_HandLeft = distance(Prebody[nowTrackIndex].Joints[JointType.HandLeft].Position, bodies[nowTrackIndex].Joints[JointType.HandLeft].Position) * 100;
                            if (dis_HandLeft > 6)
                            {
                                if (HandLeftMotion[nowTrackIndex].Count == 0)
                                {
                                    HandLeftMotion[nowTrackIndex].Add(bodies[nowTrackIndex].Joints[JointType.HandLeft].Position);
                                }
                            }
                            else
                            {
                                if (HandLeftMotion[nowTrackIndex].Count != 0)
                                {
                                    HandLeftMotion[nowTrackIndex].Add(bodies[nowTrackIndex].Joints[JointType.HandLeft].Position);
                                    double subX = (HandLeftMotion[nowTrackIndex][0].X - HandLeftMotion[nowTrackIndex][1].X) * 100;
                                    double subY = (HandLeftMotion[nowTrackIndex][0].Y - HandLeftMotion[nowTrackIndex][1].Y) * 100;

                                    if (subX > 13 && subY < 8 && subY > -8)
                                    {
                                        //SendKeys.SendWait("{LEFT}");
                                        //textBox2.Text = "LEFT";
                                        //hand_left();
                                    }
                                    else if (subX < -9 && subY < 8 && subY > -8)
                                    {
                                        //SendKeys.SendWait("{RIGHT}");
                                        textBox2.Text = "RIGHT";
                                        hand_right();
                                    }

                                    HandLeftMotion[nowTrackIndex].Clear();
                                }
                            }
                            Prebody[nowTrackIndex] = bodies[nowTrackIndex];
                        }
                        else
                        {
                            Prebody[nowTrackIndex] = bodies[nowTrackIndex];
                        }
                    }

                }
            }
        }




        /// <summary>
        /// Handles the body frame data arriving from the sensor
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void Reader_BodyFrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            using (var bodyFrame = e.FrameReference.AcquireFrame())
            {
                if (bodyFrame != null)
                {
                    // update body data
                    bodyFrame.GetAndRefreshBodyData(this.bodies);

                    using (DrawingContext dc = this.drawingGroup.Open())
                    {
                        // draw the dark background
                        //dc.DrawRectangle(Brushes.Black, null, this.displayRect);



                        //---------ColorFrame----------//
                        //背景用ColorImage呈現
                        dc.DrawImage(this.colorBitmap, new Rect(0, 0, this.colorBitmap.PixelWidth, this.colorBitmap.PixelHeight));


                        bool drawFaceResult = false;

                        // iterate through each face source
                        for (int i = 0; i < this.bodyCount; i++)
                        {
                            // check if a valid face is tracked in this face source
                            if (this.faceFrameSources[i].IsTrackingIdValid)
                            {
                                // check if we have valid face frame results
                                if (this.faceFrameResults[i] != null)
                                {
                                    //只追蹤一人
                                    //if (nowTrackID == null)
                                    //{
                                    //    //nowTrackID = faceFrameResults[i].TrackingId;
                                    //    //nowTrackIndex = i;
                                    //    //doClothes = true;
                                    //}
                                    //else 



                                    // 雙手舉超過頭部，才會被追蹤
                                    // 顯示手部頭部座標
                                    //textBox3.Text = "HandRight: " + bodies[nowTrackIndex].Joints[JointType.HandRight].Position.Y + "\nHandLeft: " + bodies[nowTrackIndex].Joints[JointType.HandLeft].Position.Y + "\nHead: " + bodies[nowTrackIndex].Joints[JointType.Head].Position.Y;

                                    double hand_right = bodies[i].Joints[JointType.HandRight].Position.Y;
                                    double hand_left = bodies[i].Joints[JointType.HandLeft].Position.Y;
                                    double head = bodies[i].Joints[JointType.Head].Position.Y;

                                    if (hand_right > head && hand_left > head && nowTrackID != faceFrameSources[i].TrackingId)
                                    {
                                        nowTrackIndex = i;
                                        nowTrackID = bodies[i].TrackingId;
                                        this.faceFrameSources[i].TrackingId = this.bodies[i].TrackingId;

                                        textBox3.Text = "i: " + i + "\nnowTrackIndex: " + nowTrackIndex.ToString();

                                        doClothes = true;
                                        // hidden the gender image result when new body detect
                                        img_gender_girl.Visibility = Visibility.Hidden;
                                        img_gender_boy.Visibility = Visibility.Hidden;
                                        // empty the searchClothes Result
                                        clothes_label.Content = "";
                                        clothesIMG.Source = null;
                                    }





                                    if (nowTrackID == faceFrameResults[i].TrackingId)
                                    {
                                        textBox.Text = nowTrackID + " " + nowTrackIndex + " ";
                                        // draw face frame results                                        
                                        this.DrawFaceFrameResults(nowTrackIndex, this.faceFrameResults[nowTrackIndex], dc);
                                    }

                                    if (!drawFaceResult)
                                    {
                                        drawFaceResult = true;
                                    }
                                }
                            }
                            else
                            {
                                // check if the corresponding body is tracked 
                                if (this.bodies[i].IsTracked)
                                {
                                    ////nowTrackID = bodies[i].TrackingId;
                                    ////nowTrackIndex = i;
                                    //doClothes = true;
                                    //// hidden the gender image result when new body detect
                                    //img_gender_girl.Visibility = Visibility.Hidden;
                                    //img_gender_boy.Visibility = Visibility.Hidden;
                                    //// empty the searchClothes Result
                                    //clothes_label.Content = "";
                                    //// update the face frame source to track this body
                                    this.faceFrameSources[i].TrackingId = this.bodies[i].TrackingId;

                                }
                            }


















                        }


                        // 利用骨架切割上衣圖片
                        if (doClothes)
                        {
                            doClothes = false;

                            ColorSpacePoint ShoulderLeft_ColorSpacePoint = this.coordinateMapper.MapCameraPointToColorSpace(bodies[nowTrackIndex].Joints[JointType.ShoulderLeft].Position);
                            ColorSpacePoint ShoulderRight_ColorSpacePoint = this.coordinateMapper.MapCameraPointToColorSpace(bodies[nowTrackIndex].Joints[JointType.ShoulderRight].Position);
                            ColorSpacePoint HipLeft_ColorSpacePoint = this.coordinateMapper.MapCameraPointToColorSpace(bodies[nowTrackIndex].Joints[JointType.HipLeft].Position);

                            int clothes_width = (int)Math.Abs(ShoulderLeft_ColorSpacePoint.X - ShoulderRight_ColorSpacePoint.X);
                            int clothes_height = (int)Math.Abs(ShoulderLeft_ColorSpacePoint.Y - HipLeft_ColorSpacePoint.Y);
                            clothes(ShoulderLeft_ColorSpacePoint, clothes_width, clothes_height);
                        }


                        textBox2.Text = "nowTrackID: " + nowTrackID + "\nnowTrackIndex: " + nowTrackIndex;



                        if (!drawFaceResult)
                        {
                            // if no faces were drawn then this indicates one of the following:
                            // a body was not tracked 
                            // a body was tracked but the corresponding face was not tracked
                            // a body and the corresponding face was tracked though the face box or the face points were not valid
                            dc.DrawText(
                                this.textFaceNotTracked,
                                this.textLayoutFaceNotTracked);
                        }

                        this.drawingGroup.ClipGeometry = new RectangleGeometry(this.displayRect);






                        //---------SkeletonFrame----------//
                        int penIndex = 0;
                        foreach (Body body in this.bodies)
                        {
                            Pen drawPen = this.bodyColors[penIndex++];

                            if (body.IsTracked)
                            {
                                // 畫邊界
                                //this.DrawClippedEdges(body, dc);

                                IReadOnlyDictionary<JointType, Joint> joints = body.Joints;

                                // convert the joint points to depth (display) space
                                Dictionary<JointType, Point> jointPoints = new Dictionary<JointType, Point>();

                                foreach (JointType jointType in joints.Keys)
                                {
                                    // sometimes the depth(Z) of an inferred joint may show as negative
                                    // clamp down to 0.1f to prevent coordinatemapper from returning (-Infinity, -Infinity)
                                    CameraSpacePoint position = joints[jointType].Position;
                                    if (position.Z < 0)
                                    {
                                        position.Z = InferredZPositionClamp;
                                    }

                                    // 原本的
                                    //DepthSpacePoint depthSpacePoint = this.coordinateMapper.MapCameraPointToDepthSpace(position);
                                    //jointPoints[jointType] = new Point(depthSpacePoint.X, depthSpacePoint.Y);

                                    // 改寫讓骨架座標對應到ColorSpace座標
                                    ColorSpacePoint jointPointsInColor = this.coordinateMapper.MapCameraPointToColorSpace(position);
                                    jointPoints[jointType] = new Point(jointPointsInColor.X, jointPointsInColor.Y);
                                }
                                this.DrawBody(joints, jointPoints, dc, drawPen);

                                //this.DrawHand(body.HandLeftState, jointPoints[JointType.HandLeft], dc);
                                //this.DrawHand(body.HandRightState, jointPoints[JointType.HandRight], dc);
                            }
                        }





                    }
                }
            }
        }




        //---------SkeletonFrame----------//

        /// <summary>
        /// Draws a body
        /// </summary>
        /// <param name="joints">joints to draw</param>
        /// <param name="jointPoints">translated positions of joints to draw</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        /// <param name="drawingPen">specifies color to draw a specific body</param>
        private void DrawBody(IReadOnlyDictionary<JointType, Joint> joints, IDictionary<JointType, Point> jointPoints, DrawingContext drawingContext, Pen drawingPen)
        {
            // Draw the bones
            foreach (var bone in this.bones)
            {
                this.DrawBone(joints, jointPoints, bone.Item1, bone.Item2, drawingContext, drawingPen);
            }

            // Draw the joints
            foreach (JointType jointType in joints.Keys)
            {
                Brush drawBrush = null;

                TrackingState trackingState = joints[jointType].TrackingState;

                if (trackingState == TrackingState.Tracked)
                {
                    drawBrush = this.trackedJointBrush;
                }
                else if (trackingState == TrackingState.Inferred)
                {
                    drawBrush = this.inferredJointBrush;
                }

                if (drawBrush != null)
                {
                    //drawingContext.DrawEllipse(drawBrush, null, jointPoints[jointType], JointThickness, JointThickness);
                }
            }


            //// 利用骨架切割上衣圖片
            //if (doClothes)
            //{
            //    doClothes = false;

            //    ColorSpacePoint ShoulderLeft_ColorSpacePoint = this.coordinateMapper.MapCameraPointToColorSpace(joints[JointType.ShoulderLeft].Position);
            //    ColorSpacePoint ShoulderRight_ColorSpacePoint = this.coordinateMapper.MapCameraPointToColorSpace(joints[JointType.ShoulderRight].Position);
            //    ColorSpacePoint HipLeft_ColorSpacePoint = this.coordinateMapper.MapCameraPointToColorSpace(joints[JointType.HipLeft].Position);

            //    int clothes_width = (int)Math.Abs(ShoulderLeft_ColorSpacePoint.X - ShoulderRight_ColorSpacePoint.X);
            //    int clothes_height = (int)Math.Abs(ShoulderLeft_ColorSpacePoint.Y - HipLeft_ColorSpacePoint.Y);
            //    clothes(ShoulderLeft_ColorSpacePoint, clothes_width, clothes_height);
            //}

        }


        private void clothes(ColorSpacePoint clothesOrigin, int clothes_width, int clothes_height)
        {
            try
            {
                clothes_fileName = nowTrackID + "-00000.jpg";
                //textBox.Text += fileName+"11111111";
                textBox1.Text = clothes_fileName;
                if (!File.Exists(clothes_fileName))
                {


                    using (FileStream saveImage = new FileStream(clothes_fileName, FileMode.Create, FileAccess.Write))
                    {
                        //從ColorImage.Source處取出一張影像，轉為BitmapSource格式
                        //儲存到imageSource
                        BitmapSource imageSourceAPI = (BitmapSource)colorBitmap;
                        //挑選Joint Photographic Experts Group(JPEG)影像編碼器
                        JpegBitmapEncoder encoder = new JpegBitmapEncoder();

                        //將BitmapSource裁切成衣服大小，並add frames
                        if ((int)clothesOrigin.Y + clothes_height > 1080)
                        {
                            clothes_height = 1080 - (int)clothesOrigin.Y;
                        }
                        else if ((int)clothesOrigin.X + clothes_width > 1920)
                        {
                            clothes_width = 1920 - (int)clothesOrigin.X;
                        }

                        Int32Rect int32faceBox2 = new Int32Rect((int)clothesOrigin.X, (int)clothesOrigin.Y, clothes_width, clothes_height);
                        CroppedBitmap crop = new CroppedBitmap(this.colorBitmap, int32faceBox2);
                        encoder.Frames.Add(BitmapFrame.Create(crop));

                        //儲存影像與後續影像清除工作
                        encoder.Save(saveImage);
                        saveImage.Flush();
                        saveImage.Close();
                        saveImage.Dispose();

                        //searchClothes(fileName);

                        //showClothes();

                        ////顯示衣服圖檔
                        //string currentpath = Directory.GetCurrentDirectory() + "\\00000.jpg";
                        //BitmapImage bitmapSource2;
                        //Uri fileUri = new Uri(currentpath);
                        //bitmapSource2 = new BitmapImage();
                        //bitmapSource2.BeginInit();
                        //bitmapSource2.UriSource = fileUri;
                        //bitmapSource2.EndInit();
                        //clothesIMG.Source = bitmapSource2;
                    }
                }
            }
            catch (Exception e)
            {
                textBox1.Text += e.Message;
            }
        }

        private void showClothes()
        {

            string currentpath = Directory.GetCurrentDirectory() + "\\" + nowTrackID + "-00000.jpg";

            // Create the image element.
            //Image simpleImage = new Image();
            //simpleImage.Width = 200;
            //simpleImage.Margin = new Thickness(5);
            try
            {
                if (File.Exists(currentpath))
                {
                    // Create source.
                    BitmapImage bi = new BitmapImage();
                    // BitmapImage.UriSource must be in a BeginInit/EndInit block.
                    bi.BeginInit();
                    bi.UriSource = new Uri(currentpath, UriKind.RelativeOrAbsolute);
                    bi.EndInit();
                    // Set the image source.
                    clothesIMG.Source = bi;
                }
            }
            finally
            {
            }

            //try
            //{
            //    FileStream stream = new FileStream(currentpath, FileMode.Open, FileAccess.ReadWrite);
            //    BitmapImage src = new BitmapImage();
            //    src.BeginInit();
            //    src.StreamSource = stream;
            //    src.EndInit();
            //    clothesIMG.Source = src;
            //    //stream.Flush();
            //    //stream.Close();
            //    //stream.Dispose();
            //}
            //catch
            //{
            //}



            //MessageBox.Show(fileName);

            //using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            //{
            //    //BitmapSource src = (BitmapSource)ImageSource;
            //    BitmapImage src = new BitmapImage();
            //    src.BeginInit();
            //    src.StreamSource = stream;

            //    clothesIMG.Source = src;
            //    src.EndInit();

            //    //stream.Flush();
            //    //stream.Close();
            //    //stream.Dispose();

            //    //clothesIMG.Source = src;

            //}






        }

        private void showClothesFlush()
        {

        }



        // 把圖片丟到Google以圖搜圖，回傳結果_async
        public async Task searchClothes(string url)
        {
            //postData = "My Data To Post";

            var webRequest = WebRequest.Create(String.Format("http://www.google.com/searchbyimage?hl=zh-TW&site=search&image_url=http://163.18.42.211:1688/" + url)) as HttpWebRequest;


            webRequest.Method = "GET";
            webRequest.ProtocolVersion = HttpVersion.Version11;
            //webRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/13.0.782.220 Safari/535.1";
            webRequest.UserAgent = "Mozilla/5.0(Windows NT 10.0; Win64; x64) AppleWebKit/537.36(KHTML, like Gecko) Chrome/57.0.2987.133 Safari/537.36";


            //using (Stream postStream = await webRequest.GetRequestStreamAsync())
            //{
            //    byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            //    await postStream.WriteAsync(byteArray, 0, byteArray.Length);
            //    await postStream.FlushAsync();
            //}
            try
            {
                string Response;
                using (var response = (HttpWebResponse)await webRequest.GetResponseAsync())
                using (Stream streamResponse = response.GetResponseStream())
                using (StreamReader streamReader = new StreamReader(streamResponse))
                {
                    Response = await streamReader.ReadToEndAsync();

                    var regexId = new Regex(@"這個圖片最有可能的推測結果：(?<ID>.*?)搜尋結果", RegexOptions.IgnoreCase);
                    MatchCollection mcId = regexId.Matches(Response);

                    if (mcId.Count != 0)
                    {
                        string str = Regex.Replace(mcId[0].Groups["ID"].Value, @"<[^>]*>", String.Empty).Replace("&nbsp;", "");
                        //return Task <>

                        string[] clothes_keyword = str.Split('.');
                        //MessageBox.Show(clothes_keyword[0]);
                        //MessageBox.Show(keyword);
                        //File.WriteAllText("tmp2.txt", keyword);
                        clothes_label.Content = "上衣關鍵字：" + clothes_keyword[0];
                        clothes_keyword_result = clothes_keyword[0];
                    }
                }

            }
            catch (WebException)
            {
                //error    
            }
        }





        // 把圖片丟到Google以圖搜圖，回傳結果
        private string searchClothes_ERROR(string imagePath)
        {
            var webRequest = WebRequest.Create(String.Format("http://www.google.com/searchbyimage?hl=zh-TW&site=search&image_url=http://163.18.42.211:1688/" + imagePath)) as HttpWebRequest;

            webRequest.Method = "GET";
            webRequest.ProtocolVersion = HttpVersion.Version11;
            //webRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/13.0.782.220 Safari/535.1";
            webRequest.UserAgent = "Mozilla/5.0(Windows NT 10.0; Win64; x64) AppleWebKit/537.36(KHTML, like Gecko) Chrome/57.0.2987.133 Safari/537.36";

            var response = webRequest.GetResponse() as HttpWebResponse;
            var reader = new StreamReader(response.GetResponseStream());

            var str = reader.ReadToEnd();
            //File.WriteAllText("tmp3.txt", str + Environment.NewLine);

            //MessageBox.Show(str);
            var regexId = new Regex(@"這個圖片最有可能的推測結果：(?<ID>.*?)搜尋結果", RegexOptions.IgnoreCase);
            //var regexId = new Regex(@"這個圖片最有可能的推測結果：(?<ID>.*?)", RegexOptions.IgnoreCase);

            MatchCollection mcId = regexId.Matches(str);

            //File.WriteAllText("tmp.txt", mcId[0].Groups["ID"].Value + Environment.NewLine);

            if (mcId.Count != 0)
            {
                return Regex.Replace(mcId[0].Groups["ID"].Value, @"<[^>]*>", String.Empty).Replace("&nbsp;", "");
            }
            return "";
        }


        /// <summary>
        /// Draws one bone of a body (joint to joint)
        /// </summary>
        /// <param name="joints">joints to draw</param>
        /// <param name="jointPoints">translated positions of joints to draw</param>
        /// <param name="jointType0">first joint of bone to draw</param>
        /// <param name="jointType1">second joint of bone to draw</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        /// /// <param name="drawingPen">specifies color to draw a specific bone</param>
        private void DrawBone(IReadOnlyDictionary<JointType, Joint> joints, IDictionary<JointType, Point> jointPoints, JointType jointType0, JointType jointType1, DrawingContext drawingContext, Pen drawingPen)
        {
            Joint joint0 = joints[jointType0];
            Joint joint1 = joints[jointType1];

            // If we can't find either of these joints, exit
            if (joint0.TrackingState == TrackingState.NotTracked ||
                joint1.TrackingState == TrackingState.NotTracked)
            {
                return;
            }

            // We assume all drawn bones are inferred unless BOTH joints are tracked
            Pen drawPen = this.inferredBonePen;
            if ((joint0.TrackingState == TrackingState.Tracked) && (joint1.TrackingState == TrackingState.Tracked))
            {
                drawPen = drawingPen;
            }

            drawingContext.DrawLine(drawPen, jointPoints[jointType0], jointPoints[jointType1]);
        }

        /// <summary>
        /// Draws a hand symbol if the hand is tracked: red circle = closed, green circle = opened; blue circle = lasso
        /// </summary>
        /// <param name="handState">state of the hand</param>
        /// <param name="handPosition">position of the hand</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        private void DrawHand(HandState handState, Point handPosition, DrawingContext drawingContext)
        {
            switch (handState)
            {
                case HandState.Closed:
                    drawingContext.DrawEllipse(this.handClosedBrush, null, handPosition, HandSize, HandSize);
                    break;

                case HandState.Open:
                    drawingContext.DrawEllipse(this.handOpenBrush, null, handPosition, HandSize, HandSize);
                    break;

                case HandState.Lasso:
                    drawingContext.DrawEllipse(this.handLassoBrush, null, handPosition, HandSize, HandSize);
                    break;
            }
        }

        /// <summary>
        /// Draws indicators to show which edges are clipping body data
        /// </summary>
        /// <param name="body">body to draw clipping information for</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        private void DrawClippedEdges(Body body, DrawingContext drawingContext)
        {
            FrameEdges clippedEdges = body.ClippedEdges;

            if (clippedEdges.HasFlag(FrameEdges.Bottom))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(0, this.displayHeight - ClipBoundsThickness, this.DepthdisplayWidth, ClipBoundsThickness));
            }

            if (clippedEdges.HasFlag(FrameEdges.Top))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(0, 0, this.DepthdisplayWidth, ClipBoundsThickness));
            }

            if (clippedEdges.HasFlag(FrameEdges.Left))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(0, 0, ClipBoundsThickness, this.displayHeight));
            }

            if (clippedEdges.HasFlag(FrameEdges.Right))
            {
                drawingContext.DrawRectangle(
                    Brushes.Red,
                    null,
                    new Rect(this.DepthdisplayWidth - ClipBoundsThickness, 0, ClipBoundsThickness, this.displayHeight));
            }
        }








        /// <summary>
        /// Draws face frame results
        /// </summary>
        /// <param name="faceIndex">the index of the face frame corresponding to a specific body in the FOV</param>
        /// <param name="faceResult">container of all face frame results</param>
        /// <param name="drawingContext">drawing context to render to</param>
        private void DrawFaceFrameResults(int faceIndex, FaceFrameResult faceResult, DrawingContext drawingContext)
        {

            // choose the brush based on the face index
            Brush drawingBrush = this.faceBrush[faceIndex];
            if (faceIndex < this.bodyCount)
            {
                drawingBrush = this.faceBrush[faceIndex];

                //textBox.Text = this.faceFrameSources[faceIndex].TrackingId.ToString() + "\n" + saveTrackingID[faceIndex].ToString();

                //---------Microsoft Face Api----------// 
                //if(this.bodies[faceIndex].TrackingId != saveTrackingID[faceIndex])
                //{
                //    saveTrackingID[faceIndex] = this.bodies[faceIndex].TrackingId;
                //    textBox.Text = this.faceFrameSources[faceIndex].TrackingId.ToString() + "\n" + saveTrackingID[faceIndex].ToString();

                //    string fileName = "tmp.jpg";
                //    using (FileStream saveImage = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
                //    {
                //        //從ColorImage.Source處取出一張影像，轉為BitmapSource格式
                //        //儲存到imageSource
                //        BitmapSource imageSourceAPI = (BitmapSource)colorBitmap;
                //        //挑選Joint Photographic Experts Group(JPEG)影像編碼器
                //        JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                //        //將取出的影像加到編碼器的影像集
                //        encoder.Frames.Add(BitmapFrame.Create(imageSourceAPI.CopyPixels(faceBox,));
                //        //儲存影像與後續影像清除工作
                //        encoder.Save(saveImage);
                //        saveImage.Flush();
                //        saveImage.Close();
                //        saveImage.Dispose();

                //        DetectAgeGender(fileName);
                //    }

                //}






                //numFace = 6 - numFace;





                //string strr = this.faceFrameSources[faceIndex].TrackingId.ToString();
                string strr = numFace.ToString();
                FormattedText ft = new FormattedText(
                    strr,
                    CultureInfo.GetCultureInfo("en-us"),
                            FlowDirection.LeftToRight,
                            new Typeface("Microsoft JhengHei"),
                            DrawTextFontSize,
                            Brushes.Red);

                drawingContext.DrawText(ft, this.textLayoutFaceNotTracked);





                //MessageBox.Show(this.bodyCount.ToString(), "1", MessageBoxButton.OK);
                //if (nowBody != numFace)
                //{
                /*
                string fileName = "tmp.jpg";
                using (FileStream saveImage = new FileStream(fileName, FileMode.Open, FileAccess.Write))
                {
                    從ColorImage.Source處取出一張影像，轉為BitmapSource格式
                    儲存到imageSource
                    BitmapSource imageSourceAPI = (BitmapSource)colorBitmap;
                    挑選Joint Photographic Experts Group(JPEG)影像編碼器
                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    將取出的影像加到編碼器的影像集
                    encoder.Frames.Add(BitmapFrame.Create(imageSourceAPI));
                    儲存影像與後續影像清除工作
                    encoder.Save(saveImage);
                    saveImage.Flush();
                    saveImage.Close();
                    saveImage.Dispose();

                    nowBody = numFace;
                }*/
                //辨識
                //DetectAgeGender(fileName);
                //}

                numFace = 0;






            }

            Pen drawingPen = new Pen(drawingBrush, DrawFaceShapeThickness);

            // draw the face bounding box
            var faceBoxSource = faceResult.FaceBoundingBoxInColorSpace;
            Rect faceBox = new Rect(faceBoxSource.Left, faceBoxSource.Top, faceBoxSource.Right - faceBoxSource.Left, faceBoxSource.Bottom - faceBoxSource.Top);
            drawingContext.DrawRectangle(null, drawingPen, faceBox);

            //建立臉部box的Int32Rect
            //Int32Converter int32faceBox = new Int32Converter();
            //int32faceBox.ConvertFrom(faceBox);

            ////  修正臉部截圖放大20%
            //double x = faceBoxSource.Left * 0.9;
            //double y = faceBoxSource.Top * 0.7;
            //double width = (faceBoxSource.Right - faceBoxSource.Left) * 1.8;
            //double height = (faceBoxSource.Bottom - faceBoxSource.Top) * 1.9;
            //Int32Rect int32faceBox = new Int32Rect((int)x, (int)y, (int)width, (int)height);
            Int32Rect int32faceBox = new Int32Rect(faceBoxSource.Left, faceBoxSource.Top, faceBoxSource.Right - faceBoxSource.Left, faceBoxSource.Bottom - faceBoxSource.Top);

            // 取得頭部旋轉資料，當faceYaw介於-20~20之間才做辨識
            int pitch2, yaw2, roll2;
            ExtractFaceRotationInDegrees(faceResult.FaceRotationQuaternion, out pitch2, out yaw2, out roll2);


            //---------Microsoft Face Api----------// 
            if (this.bodies[faceIndex].TrackingId != saveTrackingID[faceIndex] && yaw2 > -20 && yaw2 < 20)
            {
                //textBox.Text = textBox.Text + yaw2.ToString();

                saveTrackingID[faceIndex] = this.bodies[faceIndex].TrackingId;
                //textBox.Text = this.faceFrameSources[faceIndex].TrackingId.ToString() + "\n" + saveTrackingID[faceIndex].ToString();


                // 另外儲存完整沒有裁切的影像
                string fileName2 = saveTrackingID[faceIndex].ToString() + "-ALL-" + faceIndex + ".jpg";
                using (FileStream saveImage = new FileStream(fileName2, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    //從ColorImage.Source處取出一張影像，轉為BitmapSource格式
                    //儲存到imageSource
                    BitmapSource imageSourceAPI = (BitmapSource)colorBitmap;
                    //挑選Joint Photographic Experts Group(JPEG)影像編碼器
                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    //將取出的影像加到編碼器的影像集
                    encoder.Frames.Add(BitmapFrame.Create(imageSourceAPI));
                    //儲存影像與後續影像清除工作
                    encoder.Save(saveImage);
                    saveImage.Flush();
                    saveImage.Close();
                    saveImage.Dispose();
                }


                string fileName = saveTrackingID[faceIndex].ToString() + "-" + faceIndex + ".jpg";
                using (FileStream saveImage = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    //從ColorImage.Source處取出一張影像，轉為BitmapSource格式
                    //儲存到imageSource
                    BitmapSource imageSourceAPI = (BitmapSource)colorBitmap;
                    //挑選Joint Photographic Experts Group(JPEG)影像編碼器
                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    //將取出的影像加到編碼器的影像集
                    //encoder.Frames.Add(BitmapFrame.Create(imageSourceAPI));

                    //將BitmapSource裁切成臉部大小，並add frames
                    CroppedBitmap crop = new CroppedBitmap(this.colorBitmap, int32faceBox);
                    encoder.Frames.Add(BitmapFrame.Create(crop));

                    //儲存影像與後續影像清除工作
                    encoder.Save(saveImage);
                    saveImage.Flush();
                    saveImage.Close();
                    saveImage.Dispose();

                    string faceRotate = "pitch: " + pitch2.ToString() + ", yaw: " + yaw2.ToString() + ", roll: " + roll2;
                    // 進行辨識
                    DetectAgeGender(fileName, faceIndex, faceRotate);

                }

            }


            //textBox.Text = JointType.ShoulderRight.ToString();



            // Rect faceBox2 = new Rect(faceBoxSource.Left, faceBoxSource.Top + 500, faceBoxSource.Right - faceBoxSource.Left, faceBoxSource.Bottom - faceBoxSource.Top);
            // dc2.DrawRectangle(Brushes.White, null, faceBox2);
            // DrawingContext drawingContext2 = new DrawingContext;

            string EyePosition = string.Empty;

            if (faceResult.FacePointsInColorSpace != null)
            {
                //textBox.Text = "";
                int i = 0;

                // draw each face point
                foreach (PointF pointF in faceResult.FacePointsInColorSpace.Values)
                {
                    // 臉部五點焦點標示
                    // The five points are the left eye, right eye, nose and, right and left mouth corners.
                    drawingContext.DrawEllipse(null, drawingPen, new Point(pointF.X, pointF.Y), FacePointRadius, FacePointRadius);
                    //textBox.Text =  textBox.Text + pointF.X + " " + pointF.Y + " ";

                    if (i < 2)
                    {
                        if (i == 0)
                        {
                            Point LeftEye = new Point(pointF.X, pointF.Y);
                            EyePosition += "LeftEye:\n" + pointF.X + "\n" + pointF.Y + "\n\n";
                            //textBox.Text = textBox.Text + LeftEye.X.ToString() + " " + LeftEye.Y + " ";

                        }
                        else
                        {
                            Point RightEye = new Point(pointF.X, pointF.Y);
                            EyePosition += "RightEye:\n" + pointF.X + "\n" + pointF.Y + "\n\n";
                            //textBox.Text = textBox.Text + RightEye.X + " " + RightEye.Y + " ";
                        }
                        i++;
                    }
                }
            }

            string faceText = string.Empty;
            string labelText = string.Empty;

            // extract each face property information and store it in faceText
            if (faceResult.FaceProperties != null)
            {
                //額外顯示faceIndex
                //int faceIndexShow = faceIndex + 1;
                int faceIndexShow = faceIndex;

                //增加顯示tracking id
                //faceText += "faceIndex：" + faceIndexShow + "\n" + "TrackingID=" + this.bodies[faceIndex].TrackingId + "\n" + DetectAgeGenderResult[faceIndex] + "\n\n" + EyePosition;
                faceText += "faceIndex：" + faceIndexShow + "\n" + "TrackingID=" + this.bodies[faceIndex].TrackingId + "\n" + DetectGenderResult[faceIndex] + ", " + DetectAgeResult[faceIndex];
                //labelText += faceText + "\n\n" + EyePosition;

                // 顯示性別圖示 show Gender img
                if (DetectGenderResult[faceIndex] != null)
                {
                    showGenderImg(DetectGenderResult[faceIndex]);
                }

                labelText += faceText + "\n\n";

                //// 臉部表情狀態(happy, engery)
                //foreach (var item in faceResult.FaceProperties)
                //{
                //    faceText += item.Key.ToString() + " : ";

                //    // consider a "maybe" as a "no" to restrict 
                //    // the detection result refresh rate
                //    if (item.Value == DetectionResult.Maybe)
                //    {
                //        faceText += DetectionResult.No + "\n";
                //    }
                //    else
                //    {
                //        faceText += item.Value.ToString() + "\n";
                //    }
                //}
            }

            // 頭部擺動角度
            // extract face rotation in degrees as Euler angles
            if (faceResult.FaceRotationQuaternion != null)
            {
                int pitch, yaw, roll;
                ExtractFaceRotationInDegrees(faceResult.FaceRotationQuaternion, out pitch, out yaw, out roll);
                //faceText += "FaceYaw : " + yaw + "\n" +
                //            "FacePitch : " + pitch + "\n" +
                //            "FacenRoll : " + roll + "\n";

                //labelText += "FaceYaw : " + yaw + "\n" +
                //            "FacePitch : " + pitch + "\n" +
                //            "FacenRoll : " + roll + "\n";
            }

            // render the face property and face rotation information
            Point faceTextLayout;
            if (this.GetFaceTextPositionInColorSpace(faceIndex, out faceTextLayout))
            {
                // 畫文字網底
                // 網底顏色FromArgb
                SolidColorBrush faceTextRectShading = new SolidColorBrush(Color.FromArgb(100, 0, 0, 0));
                // 網底位置
                Rect faceTextRect = new Rect(faceTextLayout.X, faceTextLayout.Y, 250, 150);
                drawingContext.DrawRectangle(faceTextRectShading, null, faceTextRect);

                // 顯示人臉偵測結果，說明文字
                drawingContext.DrawText(
                        new FormattedText(
                            faceText,
                            CultureInfo.GetCultureInfo("en-us"),
                            FlowDirection.LeftToRight,
                            new Typeface("Microsoft JhengHei"),
                            DrawTextFontSize,
                            drawingBrush),
                        faceTextLayout);

                //於label顯示
                label.Content = labelText;
            }
        }

        /// <summary>
        /// Computes the face result text position by adding an offset to the corresponding 
        /// body's head joint in camera space and then by projecting it to screen space
        /// </summary>
        /// <param name="faceIndex">the index of the face frame corresponding to a specific body in the FOV</param>
        /// <param name="faceTextLayout">the text layout position in screen space</param>
        /// <returns>success or failure</returns>
        private bool GetFaceTextPositionInColorSpace(int faceIndex, out Point faceTextLayout)
        {
            faceTextLayout = new Point();
            bool isLayoutValid = false;

            Body body = this.bodies[faceIndex];
            if (body.IsTracked)
            {
                //取得頭部座標，並轉換至ColorSpace座標
                var headJoint = body.Joints[JointType.Head].Position;

                CameraSpacePoint textPoint = new CameraSpacePoint()
                {
                    X = headJoint.X + TextLayoutOffsetX,
                    Y = headJoint.Y + TextLayoutOffsetY,
                    Z = headJoint.Z
                };

                ColorSpacePoint textPointInColor = this.coordinateMapper.MapCameraPointToColorSpace(textPoint);

                faceTextLayout.X = textPointInColor.X;
                faceTextLayout.Y = textPointInColor.Y;
                isLayoutValid = true;
            }

            return isLayoutValid;
        }

        /// <summary>
        /// Validates face bounding box and face points to be within screen space
        /// </summary>
        /// <param name="faceResult">the face frame result containing face box and points</param>
        /// <returns>success or failure</returns>
        private bool ValidateFaceBoxAndPoints(FaceFrameResult faceResult)
        {
            bool isFaceValid = faceResult != null;

            if (isFaceValid)
            {
                var faceBox = faceResult.FaceBoundingBoxInColorSpace;
                if (faceBox != null)
                {
                    // check if we have a valid rectangle within the bounds of the screen space
                    isFaceValid = (faceBox.Right - faceBox.Left) > 0 &&
                                  (faceBox.Bottom - faceBox.Top) > 0 &&
                                  faceBox.Right <= this.displayWidth &&
                                  faceBox.Bottom <= this.displayHeight;

                    if (isFaceValid)
                    {
                        var facePoints = faceResult.FacePointsInColorSpace;
                        if (facePoints != null)
                        {
                            foreach (PointF pointF in facePoints.Values)
                            {
                                // check if we have a valid face point within the bounds of the screen space
                                bool isFacePointValid = pointF.X > 0.0f &&
                                                        pointF.Y > 0.0f &&
                                                        pointF.X < this.displayWidth &&
                                                        pointF.Y < this.displayHeight;

                                if (!isFacePointValid)
                                {
                                    isFaceValid = false;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            return isFaceValid;
        }





        private async void DetectAgeGender(object sender, int faceIndex, string faceRotate)
        {
            //如果尚未指定欲偵測人臉圖檔，則甚麼都不做
            if (sender == null)
                return;

            //指定傳回诶個人臉之年齡、性別、微笑值三個屬性
            var requiredFaceAttributes = new FaceAttributeType[]
            {
                FaceAttributeType.Age,
                FaceAttributeType.Gender,
                FaceAttributeType.Smile
            };
            //FaceRectangle[] faceRects;
            FaceAttributes[] attributes;

            try
            {
                using (Stream imageFileStream = File.OpenRead(sender.ToString()))
                {
                    var faces = await faceServiceClent.DetectAsync(
                        imageFileStream,
                        returnFaceAttributes: requiredFaceAttributes);
                    //faceRects = faces.Select(Face => Face.FaceRectangle).ToArray();
                    attributes = faces.Select(Face => Face.FaceAttributes).ToArray();
                }

                //MessageBox.Show(attributes.ToString(), "1", MessageBoxButton.OK);

                //接收傳回資料後，加工處理年齡、性別、微笑值資料
                int female = 0, male = 0, adult = 0, child = 0;
                double youngest = 120, oldest = 0, smilest = 0;
                string gender = null;

                foreach (var attribute in attributes)
                {
                    if (attribute.Gender == "male")
                    {
                        gender = "男性";
                        //img_gender_boy.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        gender = "女性";
                        //img_gender_girl.Visibility = Visibility.Visible;
                    }

                    //var gender = attribute.Gender;
                    //if (gender == "male")
                    //    male++;
                    //else if (gender == "female")
                    //    female++;
                    //else
                    //    MessageBox.Show("Unknown Gender!");

                    //var age = attribute.Age;
                    //if (age >= 20)
                    //    adult++;
                    //else
                    //    child++;
                    //if (age < youngest)
                    //    youngest = age;
                    //if (age > oldest)
                    //    oldest = age;
                    //var smile = attribute.Smile;
                    //if (smile > smilest)
                    //    smilest = smile;



                    //MessageBox.Show(attribute.Gender + "  " + attribute.Age);
                    //DetectAgeGenderResult[faceIndex] = gender + ", " + attribute.Age;
                    DetectAgeResult[faceIndex] = attribute.Age.ToString();
                    DetectGenderResult[faceIndex] = gender;

                    //MessageBox.Show(DetectAgeGenderResult[faceIndex]);
                    //textBox.Text = textBox.Text + "\n" + DetectAgeGenderResult;


                    // textBox顯示
                    //textBox.Text = textBox.Text + "\nFaceIndex: " + faceIndex + "\nTrackingID: " + saveTrackingID[faceIndex].ToString() + "\n" + DetectAgeGenderResult[faceIndex] + ", " + faceRotate;

                    // 把辨識結果儲存到tmp.txt
                    DateTime mNow = DateTime.Now;
                    string path = @"tmp.txt";
                    File.AppendAllText(path, mNow.ToString("yyyy-MM-dd HH:mm:ss") + ", FaceIndex: " + faceIndex + ", TrackingID: " + saveTrackingID[faceIndex].ToString() + ", " + DetectGenderResult[faceIndex] + ", " + DetectAgeResult[faceIndex] + ", " + faceRotate + Environment.NewLine);
                    showClothes();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }


        }



        private void showGenderImg(string gender)
        {
            if (gender == "男性")
            {
                img_gender_boy.Visibility = Visibility.Visible;
                img_gender_girl.Visibility = Visibility.Hidden;
            }
            else
            {
                img_gender_boy.Visibility = Visibility.Hidden;
                img_gender_girl.Visibility = Visibility.Visible;
            }
        }



        //---------CoordinateMappingBasics-WPF----------//

        /// <summary>
        /// Handles the depth/color/body index frame data arriving from the sensor
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void Reader_MultiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {
            int depthWidth = 0;
            int depthHeight = 0;

            DepthFrame depthFrame = null;
            ColorFrame colorFrame = null;
            BodyIndexFrame bodyIndexFrame = null;
            bool isBitmapLocked = false;

            MultiSourceFrame multiSourceFrame = e.FrameReference.AcquireFrame();

            // If the Frame has expired by the time we process this event, return.
            if (multiSourceFrame == null)
            {
                return;
            }

            // We use a try/finally to ensure that we clean up before we exit the function.  
            // This includes calling Dispose on any Frame objects that we may have and unlocking the bitmap back buffer.
            try
            {
                depthFrame = multiSourceFrame.DepthFrameReference.AcquireFrame();
                colorFrame = multiSourceFrame.ColorFrameReference.AcquireFrame();
                bodyIndexFrame = multiSourceFrame.BodyIndexFrameReference.AcquireFrame();

                // If any frame has expired by the time we process this event, return.
                // The "finally" statement will Dispose any that are not null.
                if ((depthFrame == null) || (colorFrame == null) || (bodyIndexFrame == null))
                {
                    return;
                }

                // Process Depth
                FrameDescription depthFrameDescription = depthFrame.FrameDescription;

                depthWidth = depthFrameDescription.Width;
                depthHeight = depthFrameDescription.Height;

                // Access the depth frame data directly via LockImageBuffer to avoid making a copy
                using (KinectBuffer depthFrameData = depthFrame.LockImageBuffer())
                {
                    this.coordinateMapper.MapColorFrameToDepthSpaceUsingIntPtr(
                        depthFrameData.UnderlyingBuffer,
                        depthFrameData.Size,
                        this.colorMappedToDepthPoints);
                }

                // We're done with the DepthFrame 
                depthFrame.Dispose();
                depthFrame = null;

                // Process Color

                // Lock the bitmap for writing
                this.bitmap.Lock();
                isBitmapLocked = true;

                colorFrame.CopyConvertedFrameDataToIntPtr(this.bitmap.BackBuffer, this.bitmapBackBufferSize, ColorImageFormat.Bgra);

                // We're done with the ColorFrame 
                colorFrame.Dispose();
                colorFrame = null;

                // We'll access the body index data directly to avoid a copy
                using (KinectBuffer bodyIndexData = bodyIndexFrame.LockImageBuffer())
                {
                    unsafe
                    {
                        byte* bodyIndexDataPointer = (byte*)bodyIndexData.UnderlyingBuffer;

                        int colorMappedToDepthPointCount = this.colorMappedToDepthPoints.Length;

                        fixed (DepthSpacePoint* colorMappedToDepthPointsPointer = this.colorMappedToDepthPoints)
                        {
                            // Treat the color data as 4-byte pixels
                            uint* bitmapPixelsPointer = (uint*)this.bitmap.BackBuffer;

                            // Loop over each row and column of the color image
                            // Zero out any pixels that don't correspond to a body index
                            for (int colorIndex = 0; colorIndex < colorMappedToDepthPointCount; ++colorIndex)
                            {
                                float colorMappedToDepthX = colorMappedToDepthPointsPointer[colorIndex].X;
                                float colorMappedToDepthY = colorMappedToDepthPointsPointer[colorIndex].Y;

                                // The sentinel value is -inf, -inf, meaning that no depth pixel corresponds to this color pixel.
                                if (!float.IsNegativeInfinity(colorMappedToDepthX) &&
                                    !float.IsNegativeInfinity(colorMappedToDepthY))
                                {
                                    // Make sure the depth pixel maps to a valid point in color space
                                    int depthX = (int)(colorMappedToDepthX + 0.5f);
                                    int depthY = (int)(colorMappedToDepthY + 0.5f);

                                    // If the point is not valid, there is no body index there.
                                    if ((depthX >= 0) && (depthX < depthWidth) && (depthY >= 0) && (depthY < depthHeight))
                                    {
                                        int depthIndex = (depthY * depthWidth) + depthX;

                                        // If we are tracking a body for the current pixel, do not zero out the pixel
                                        if (bodyIndexDataPointer[depthIndex] != 0xff)
                                        {
                                            continue;
                                        }
                                    }
                                }

                                bitmapPixelsPointer[colorIndex] = 0;
                            }
                        }

                        this.bitmap.AddDirtyRect(new Int32Rect(0, 0, this.bitmap.PixelWidth, this.bitmap.PixelHeight));
                    }
                }
            }
            finally
            {
                if (isBitmapLocked)
                {
                    this.bitmap.Unlock();
                }

                if (depthFrame != null)
                {
                    depthFrame.Dispose();
                }

                if (colorFrame != null)
                {
                    colorFrame.Dispose();
                }

                if (bodyIndexFrame != null)
                {
                    bodyIndexFrame.Dispose();
                }
            }
        }











        private string DetectAgeGenderResult1(object sender, object faceIndex)
        {

            return ("");
        }


        /// <summary>
        /// Handles the event which the sensor becomes unavailable (E.g. paused, closed, unplugged).
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void Sensor_IsAvailableChanged(object sender, IsAvailableChangedEventArgs e)
        {
            if (this.kinectSensor != null)
            {
                // on failure, set the status text
                this.StatusText = this.kinectSensor.IsAvailable ? Properties.Resources.RunningStatusText
                                                                : Properties.Resources.SensorNotAvailableStatusText;
            }
        }

        private void resetID_Click(object sender, RoutedEventArgs e)
        {
            trackID = false;
            nowTrackIndex = 0;
            nowTrackID = null;
            //doClothes = false;
            clothesIMG.Source = null;

            for (int i = 0; i < 6; i++)
            {
                //faceFrameResults[i] = null;
                //this.faceFrameSources = new FaceFrameSource[this.bodyCount];
                //this.faceFrameReaders = new FaceFrameReader[this.bodyCount];
                //this.faceFrameResults = new FaceFrameResult[this.bodyCount];
                this.faceFrameResults[i] = null;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            showClothes();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //DefLoginAsync("v0ZHddN.jpg");

            clothesIMG.Source = null;
        }



        private void DoMove(DependencyProperty dp, double to, double ar, double dr, double duration)
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation();//创建双精度动画对象
            doubleAnimation.To = to;//设置动画的结束值
            doubleAnimation.Duration = TimeSpan.FromSeconds(duration);//设置动画时间线长度
            doubleAnimation.AccelerationRatio = ar;//动画加速
            doubleAnimation.DecelerationRatio = dr;//动画减速
            doubleAnimation.FillBehavior = FillBehavior.HoldEnd;//设置动画完成后执行的操作
            //grdTransfer.BeginAnimation(dp, doubleAnimation);//设置动画应用的属性并启动动画
        }

        int to = -4400;

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            to -= 1100;
            DoMove(Canvas.LeftProperty, to, 0.1, 0.5, 0.5);
        }

        private void hand_right()
        {
            if (to != 0)
            {
                to += 1100;
                DoMove(Canvas.LeftProperty, to, 0.1, 0.5, 0.5);
                textBox3.Text = to.ToString();
            }
        }

        private void hand_left()
        {
            if (to != -9900)
            {
                to -= 1100;
                DoMove(Canvas.LeftProperty, to, 0.1, 0.5, 0.5);
                textBox3.Text = to.ToString();
            }
        }

        private void delete_file()
        {
            string sourceDir = Directory.GetCurrentDirectory() + @"\";

            try
            {
                string[] txtList = Directory.GetFiles(sourceDir, @"*.txt");
                string[] imgList = Directory.GetFiles(sourceDir, @"*.jpg");

                foreach (string f in imgList)
                {
                    try
                    {
                        File.Delete(f);
                    }
                    // Catch exception if the file was already copied.
                    catch (IOException ex)
                    {
                    }
                }

                //foreach (string f in txtList)
                //{
                //    try
                //    {
                //        File.Delete(f);
                //    }
                //    // Catch exception if the file was already copied.
                //    catch (IOException ex)
                //    {
                //    }
                //}
            }
            catch (Exception ex)
            {
            }

        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
        }

        }
    }
