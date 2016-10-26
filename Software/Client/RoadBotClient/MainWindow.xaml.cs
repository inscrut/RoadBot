using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace RoadBotClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int Dfieldlength = 20;
        int Dfieldheight = 20;

        Bitmap BMP;
        DepthField DF;
        ToolTip DFtt;

        bool online = false;

        public MainWindow()
        {

            InitializeComponent();
            AdressInput1.Text = "rtsp://192.168.10.106:8095/live.mp4";
            AdressInput2.Text = "rtsp://192.168.10.106:8095/live2.mp4";
            this.Loaded += MainWindow_Loaded;
            maketooltip();
            if (Dfieldheight != 7 || Dfieldlength != 10) DFUsual.IsEnabled = false;
        }

        private void maketooltip()
        {
            DFtt = new System.Windows.Controls.ToolTip();
            DFtt.FontFamily = new System.Windows.Media.FontFamily("/RoadBot Client;component/Resources/#Roboto Bold");
            DFtt.FontSize = 40;
            DFtt.Placement = System.Windows.Controls.Primitives.PlacementMode.Left;
            depthImage.ToolTip = DFtt;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DF = new DepthField(Dfieldlength, Dfieldheight);
            BMP = DF.test(true);
            DepthRefresh();
        }

        private void AdressInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (AdressInput1.Text != "" && AdressInput2.Text != "") btt_connect.IsEnabled = true;
            else btt_connect.IsEnabled = false;
        }

        private void btt_connect_Click(object sender, RoutedEventArgs e)
        {
            var uri1 = new Uri(AdressInput1.Text);
            var uri2 = new Uri(AdressInput2.Text);
            _streamPlayerControl1.StartPlay(uri1, TimeSpan.FromSeconds(15));
            _streamPlayerControl2.StartPlay(uri2, TimeSpan.FromSeconds(15));
        }

        private void btt_stop_Click(object sender, RoutedEventArgs e)
        {
            _streamPlayerControl1.Stop();
            _streamPlayerControl2.Stop();
        }

        private void checkOnline()
        {
            if (online)
            {
                btt_stop.IsEnabled = true;
                img_conStatus.Source = new BitmapImage(new Uri("res/OnlineCircle.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                btt_stop.IsEnabled = false;
                img_conStatus.Source = new BitmapImage(new Uri("res/OfflineCircle.png", UriKind.RelativeOrAbsolute));
            }
        }

        private void btt_depth_Click(object sender, RoutedEventArgs e)
        {
            int[,] depthmass = new int[7, 10];
        }

        private void _streamPlayerControl1_StreamStarted(object sender, RoutedEventArgs e)
        {
            if (e.RoutedEvent.Name == "StreamStarted")
            {
                online = true;
                checkOnline();
            }
            else if (e.RoutedEvent.Name == "StreamStopped")
            {
                online = false;
                checkOnline();
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            sizing();
        }

        private void sizing()
        {
            double NW = (MWindow.ActualWidth) / 2 - 40;
            double NH = NW * 9 / 16;
            double LW = MWindow.ActualWidth - 380;
            //double LH = MWindow.Height - LogoandCon.Height - 20;
            //if (MWindow.Width < 1000 || MWindow.Height < 600) LH = 10; 
            
            if (NW > 250)
            {
                logs.MinWidth = LW;
                //logs.MinHeight = LH;
                logo.Width = 150;
                LogoandCon.HorizontalAlignment = HorizontalAlignment.Left;
                _streamPlayerControl1.Width = NW;
                _streamPlayerControl2.Width = NW;
                _streamPlayerControl1.Height = NH;
                _streamPlayerControl2.Height = NH;
            }
            else
            {
                logs.MinWidth = 300;
                //logs.MinHeight = 0;
                logo.Width = 300;
                LogoandCon.HorizontalAlignment = HorizontalAlignment.Center;
                _streamPlayerControl1.Width =300;
                _streamPlayerControl2.Width = 300;
                _streamPlayerControl1.Height = 168;
                _streamPlayerControl2.Height = 168;
            }
        }

        private void _streamPlayerControl1_StreamFailed(object sender, WebEye.StreamFailedEventArgs e)
        {
            MessageBox.Show(
                    ((WebEye.StreamFailedEventArgs)e).Error,
                    "RoadBot Client",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
        }

        private void DepthRefresh()
        {
            depthImage.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
            BMP.GetHbitmap(),
            IntPtr.Zero,
            Int32Rect.Empty,
            BitmapSizeOptions.FromEmptyOptions());
        }

        private void DFRandom_Click(object sender, RoutedEventArgs e)
        {
            BMP = DF.test(true);
            DepthRefresh();
        }

        private void DFUsual_Click(object sender, RoutedEventArgs e)
        {
            BMP = DF.test(false);
            DepthRefresh();
        }

        private void depthImage_MouseMove(object sender, MouseEventArgs e)
        {
            var buff = e.GetPosition(depthImage).Y;
            var buff2 = (((DepthPanel.ActualWidth - 10) * Dfieldlength / (Dfieldheight * Dfieldheight)));
            System.Drawing.Point coords = 
                new System.Drawing.Point(Convert.ToInt32(Math.Floor(e.GetPosition(depthImage).X / (((DepthPanel.ActualWidth-10)/ Dfieldlength)))), 
                Convert.ToInt32(Math.Floor(e.GetPosition(depthImage).Y / (((DepthPanel.ActualWidth - 10) / Dfieldlength)))));
            if (coords.X > Dfieldlength-1) coords.X = Dfieldlength - 1;
            if (coords.Y > Dfieldheight-1) coords.Y = Dfieldheight - 1;
            DFtt.Content = Convert.ToString(DF.depthmass[coords.X,coords.Y]);
        }
    }
}
