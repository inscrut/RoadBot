using System;
using System.Windows;
using Microsoft.Win32;
using System.Threading;
using System.Windows.Media.Imaging;

namespace StreamPlayerDemo
{
    public partial class MainWindow
    {
        bool online = false;

        public MainWindow()
        {
            InitializeComponent();
            AdressInput1.Text = "rtsp://184.72.239.149/vod/mp4:BigBuckBunny_115k.mov";
            AdressInput2.Text = "rtsp://184.72.239.149/vod/mp4:BigBuckBunny_115k.mov";
        }

        private void btt_connect_Click(object sender, RoutedEventArgs e)
        {
            var uri1 = new Uri(AdressInput1.Text);
            var uri2 = new Uri(AdressInput1.Text);
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
                img_conStatus.Source = new BitmapImage(new Uri("Resources/OnlineCircle.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                btt_stop.IsEnabled = false;
                img_conStatus.Source = new BitmapImage(new Uri("Resources/OfflineCircle.png", UriKind.RelativeOrAbsolute));
            }
        }

        private void AdressInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (AdressInput1.Text != "" && AdressInput2.Text != "") btt_connect.IsEnabled = true;
            else btt_connect.IsEnabled = false;
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

        private void _streamPlayerControl1_StreamFailed(object sender, WebEye.StreamFailedEventArgs e)
        {
            MessageBox.Show(
                    ((WebEye.StreamFailedEventArgs)e).Error,
                    "RoadBot Client",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
        }

        /*private void HandlePlayerEvent(object sender, RoutedEventArgs e)
        {

            if (e.RoutedEvent.Name == "StreamStarted")
            {
                //_statusLabel.Text = "Playing";
            }
            else if (e.RoutedEvent.Name == "StreamFailed")
            {

                MessageBox.Show(
                    ((WebEye.StreamFailedEventArgs)e).Error,
                    "Stream Player Demo",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            else if (e.RoutedEvent.Name == "StreamStopped")
            {
            }
        }*/
        /*var uri = new Uri(_urlTextBox.Text);
        _streamPlayerControl.StartPlay(uri, TimeSpan.FromSeconds(15));
        _streamPlayerControl2.StartPlay(uri, TimeSpan.FromSeconds(15));*/
        //_streamPlayerControl.Stop();
        //_streamPlayerControl2.Stop();

        /*
            var dialog = new SaveFileDialog { Filter = "Bitmap Image|*.bmp" };
            if (dialog.ShowDialog() == true)
            {
                _streamPlayerControl.GetCurrentFrame().Save(dialog.FileName);
            }*/

        //_imageButton.IsEnabled = _streamPlayerControl.IsPlaying;


        /*private void HandlePlayerEvent(object sender, RoutedEventArgs e)
        {
            UpdateButtons();

            if (e.RoutedEvent.Name == "StreamStarted")
            {
                //_statusLabel.Text = "Playing";
            }
            else if (e.RoutedEvent.Name == "StreamFailed")
            {
                _statusLabel.Text = "Failed";

                MessageBox.Show(
                    ((WebEye.StreamFailedEventArgs)e).Error,
                    "Stream Player Demo",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            else if (e.RoutedEvent.Name == "StreamStopped")
            {
                _statusLabel.Text = "Stopped";
            }
        }*/
    }
}
