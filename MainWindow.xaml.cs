using System;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Win32;
using LibVLCSharp.Shared;

namespace AnimePlayer
{
    public partial class MainWindow : Window
    {
        private LibVLC? _libVLC;
        private MediaPlayer? _mediaPlayer;
        private DispatcherTimer? _timer;
        private bool _isDraggingSeekBar = false;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Core.Initialize();
                _libVLC = new LibVLC();
                _mediaPlayer = new MediaPlayer(_libVLC);
                VideoView.MediaPlayer = _mediaPlayer;

                _mediaPlayer.EndReached += (s, ev) =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        PlayPauseButton.Content = "▶";
                        SeekBar.Value = 0;
                        TimeLabel.Text = "00:00 / 00:00";
                    });
                };
                InitializeTimer();
                _mediaPlayer.Volume = (int)VolumeBar.Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"初期化エラー: {ex.Message}");
            }
        }

        private void InitializeTimer()
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(500)
            };
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (_mediaPlayer == null || _isDraggingSeekBar) return;
            if (!_mediaPlayer.IsPlaying) return;

            var pos = _mediaPlayer.Position;
            var time = _mediaPlayer.Time;
            var duration = _mediaPlayer.Length;

            if (duration > 0)
            {
                SeekBar.Value = pos * 100;
                TimeLabel.Text = $"{FormatTime(TimeSpan.FromMilliseconds(time))} / {FormatTime(TimeSpan.FromMilliseconds(duration))}";
            }
        }

        private string FormatTime(TimeSpan t)
            => t.Hours > 0
                ? $"{t.Hours:D2}:{t.Minutes:D2}:{t.Seconds:D2}"
                : $"{t.Minutes:D2}:{t.Seconds:D2}";

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "動画ファイル|*.mp4;*.mkv;*.avi;*.mov;*.wmv;*.flv;*.webm;*.ts;*.m2ts|すべてのファイル|*.*"
            };

            if (dialog.ShowDialog() == true)
                PlayFile(dialog.FileName);
        }

        private void PlayFile(string filePath)
        {
            if (_mediaPlayer == null || _libVLC == null) return;

            var media = new Media(_libVLC, new Uri(filePath));
            _mediaPlayer.Media = media;
            _mediaPlayer.Play();
            PlayPauseButton.Content = "⏸";
            FileNameLabel.Text = FormatFileName(filePath);
        }

        private void PlayPauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (_mediaPlayer == null) return;

            if (_mediaPlayer.IsPlaying)
            {
                _mediaPlayer.Pause();
                PlayPauseButton.Content = "▶";
            }
            else
            {
                _mediaPlayer.Play();
                PlayPauseButton.Content = "⏸";
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            if (_mediaPlayer == null) return;
            _mediaPlayer.Stop();
            PlayPauseButton.Content = "▶";
            SeekBar.Value = 0;
            TimeLabel.Text = "00:00 / 00:00";
            // 黒画面に戻す
            Dispatcher.Invoke(() =>
            {
                VideoView.Background = System.Windows.Media.Brushes.Black;
            });
        }

        private void SeekBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_mediaPlayer == null || !_isDraggingSeekBar) return;
            _mediaPlayer.Position = (float)(e.NewValue / 100);
        }

        private void VolumeBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_mediaPlayer == null) return;
            _mediaPlayer.Volume = (int)e.NewValue;
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0)
                    PlayFile(files[0]);
            }
        }
        private void TitleBar_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Maximized
                ? WindowState.Normal
                : WindowState.Maximized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            _timer?.Stop();
            _mediaPlayer?.Dispose();
            _libVLC?.Dispose();
            base.OnClosed(e);
        }
        private void SeekBar_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _isDraggingSeekBar = true;
        }

        private void SeekBar_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _isDraggingSeekBar = false;
            if (_mediaPlayer == null) return;

            // クリック位置を計算してシーク
            var slider = (System.Windows.Controls.Slider)sender;
            var mousePos = e.GetPosition(slider);
            var ratio = mousePos.X / slider.ActualWidth;
            ratio = Math.Max(0, Math.Min(1, ratio));
            _mediaPlayer.Position = (float)ratio;
            SeekBar.Value = ratio * 100;
        }
        private string FormatFileName(string filePath)
        {
            var name = System.IO.Path.GetFileNameWithoutExtension(filePath);
            // [～]を削除
            name = System.Text.RegularExpressions.Regex.Replace(name, @"\[.*?\]", "");
            // (～)を削除
            name = System.Text.RegularExpressions.Regex.Replace(name, @"\(.*?\)", "");
            // 余分なスペースを整理
            name = name.Trim();
            return name;
        }
        public void OpenFileFromArg(string filePath)
        {
            // Loadedイベント後に実行されるよう少し待つ
            Dispatcher.BeginInvoke(new Action(() =>
            {
                PlayFile(filePath);
            }), System.Windows.Threading.DispatcherPriority.Loaded);
        }

        
    }
}