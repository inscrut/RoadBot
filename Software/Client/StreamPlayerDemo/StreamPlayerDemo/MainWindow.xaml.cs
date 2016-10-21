using System;
using System.Windows;
using Microsoft.Win32;
using System.Threading;

namespace StreamPlayerDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void HandlePlayButtonClick(object sender, RoutedEventArgs e)
        {
            var uri = new Uri(_urlTextBox.Text);
            _streamPlayerControl.StartPlay(uri, TimeSpan.FromSeconds(15));
            _streamPlayerControl2.StartPlay(uri, TimeSpan.FromSeconds(15));
            _statusLabel.Text = "Connecting...";
        }

        private void HandleStopButtonClick(object sender, RoutedEventArgs e)
        {
            _streamPlayerControl.Stop();
            _streamPlayerControl2.Stop();
        }

        private void HandleImageButtonClick(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog { Filter = "Bitmap Image|*.bmp" };
            if (dialog.ShowDialog() == true)
            {
                _streamPlayerControl.GetCurrentFrame().Save(dialog.FileName);
            }
        }

        private void UpdateButtons()
        {
            _playButton.IsEnabled = !_streamPlayerControl.IsPlaying; 
            _stopButton.IsEnabled = _streamPlayerControl.IsPlaying;
            _imageButton.IsEnabled = _streamPlayerControl.IsPlaying;
        }

        private void HandlePlayerEvent(object sender, RoutedEventArgs e)
        {
            UpdateButtons();

            if (e.RoutedEvent.Name == "StreamStarted")
            {
                _statusLabel.Text = "Playing";
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
        }

        private void TRIGGERED(object sender, System.Windows.Input.MouseEventArgs e)
        {
            MessageBoxResult Check = MessageBox.Show("TRIGGERED?", "TRIGGERED?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (Check == MessageBoxResult.Yes)
            {
                MessageBox.Show("(ง ͠° ͟ل͜ ͡°)ง");
            }
            if (Check == MessageBoxResult.No)
            {
                MessageBox.Show("( ͡° ͜ʖ ͡°)");
            }
        }
    }
}
