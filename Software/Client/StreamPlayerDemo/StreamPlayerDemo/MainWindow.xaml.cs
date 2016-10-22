using System;
using System.Windows;
using Microsoft.Win32;
using System.Threading;
using System.Windows.Media.Imaging;

namespace StreamPlayerDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        bool online = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btt_connect_Click(object sender, RoutedEventArgs e)
        {
            online = true;
            checkOnline();
        }

        private void btt_stop_Click(object sender, RoutedEventArgs e)
        {
            online = false;
            checkOnline();
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
            if (AdressInput.Text != "") btt_connect.IsEnabled = true;
            else btt_connect.IsEnabled = false;
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
