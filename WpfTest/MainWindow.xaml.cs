using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using ImageMagick;
using WpfTest.Extensions;
using WpfTest.Models;

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string CubePoints = "-1,-1,-1;1,-1,-1;1,-1,1;-1,-1,1;-1,-1,1;1,-1,1;1,1,1;-1,1,1;1,-1,1;1,-1,-1;1,1,-1;1,1,1;1,1,1;1,1,-1;-1,1,-1;-1,1,1;-1,-1,1;-1,1,1;-1,1,-1;-1,-1,-1;-1,-1,-1;-1,1,-1;1,1,-1;1,-1,-1";
        private const string CubeIndices = "0,1,2,2,3,0,4,5,6,6,7,4,8,9,10,10,11,8,12,13,14,14,15,12,16,17,18,18,19,16,20,21,22,22,23,20";
        
        private readonly GeometryModel _geometryModel = new GeometryModel();

        private readonly AngleShifter _angleShifter = new AngleShifter(-180);
        private readonly ColorShifter _baseColorShifter = new ColorShifter(0, -128, -254);

        private int _frameCounter = 0;
        private int _targetFrameCount = 1000;

        public MainWindow()
        {
            InitializeComponent();
            var timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_tick);
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Start();

            int counter = 0;
            for (int x = -15; x <= 15; x = x + 3)
            {
                for (int y = -15; y <= 15; y = y + 3)
                {
                    counter ++;
                    Model3DGroup.Children.Add(_geometryModel.Create(CubePoints, CubeIndices, GetCubeName(x, y), x, y, 0, GetCubeRotation(counter), -180, x, y, 0, 0, 0, 255));
                }
            }
        }

        private void timer_tick(object sender, EventArgs e)
        {
            var colorShifter = _baseColorShifter.Clone();
            for (int x = -15; x <= 15; x = x + 3)
            {
                for (int y = -15; y <= 15; y = y + 3)
                {
                    var cubeTransformRotation = _geometryModel.NameScope.FindName(GetCubeName(x, y) + "_Transform_Rotation") as AxisAngleRotation3D;
                    if (cubeTransformRotation != null)
                        cubeTransformRotation.Angle = _angleShifter.Angle;
                    var cubeColor = _geometryModel.NameScope.FindName(GetCubeName(x, y) + "_Color") as SolidColorBrush;
                    if (cubeColor != null)
                    {
                        cubeColor.Color = colorShifter.ColorFromRgb;
                    }
                    colorShifter.Increment();
                }
            }
            _baseColorShifter.Increment();
            _angleShifter.Increment(2);

            if(_frameCounter <= _targetFrameCount)
                Screenshot(++_frameCounter);
            //if (_frameCounter == _targetFrameCount)
            //    WriteGif(_targetFrameCount);
        }

        private Vector3D GetCubeRotation(int counter)
        {
            if (counter % 3 == 0)
                return new Vector3D(1, 0, 0);
            if (counter % 2 == 0)
                return new Vector3D(0, 1, 0);
            return new Vector3D(0, 0, 1);
        }

        private string GetCubeName(int x, int y)
        {
            return string.Format("Cube_{0}_{1}", x.ToNameFormat(), y.ToNameFormat());
        }

        private void Screenshot(int frame)
        {
            var bitmap = new RenderTargetBitmap(1280, 720, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(Viewport);
            var encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));
            using (Stream stream = File.Create(@"C:\Users\tlamb_000\Pictures\WPF\frame_" + frame.ToString("D3") + ".bmp"))
            {
                encoder.Save(stream);
            }
        }

        //private void WriteGif(int targetFrameCount)
        //{
        //    using (var collection = new MagickImageCollection())
        //    {
        //        for (int i = 1; i <= targetFrameCount; i++)
        //        {
        //            collection.Add(@"C:\Users\tlamb_000\Pictures\WPF\frame_" + i + ".bmp");
        //            collection[i - 1].AnimationDelay = 1;
        //        }
        //        collection.Optimize();
        //        collection.Write(@"C:\Users\tlamb_000\Pictures\WPF\cubes.gif");
        //    }
        //}
    }
}
