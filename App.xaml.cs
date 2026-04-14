using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;

namespace AnimePlayer
{
    public partial class App : Application
    {
        private static Mutex? _mutex;
        private const string MutexName = "AnimePlayerSingleInstance";

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        protected override void OnStartup(StartupEventArgs e)
        {
            _mutex = new Mutex(true, MutexName, out bool createdNew);

            if (!createdNew)
            {
                // 既に起動している場合は既存のウィンドウにファイルパスを送る
                if (e.Args.Length > 0)
                {
                    SendFileToExistingInstance(e.Args[0]);
                }
                // 既存のウィンドウを前面に表示
                BringExistingWindowToFront();
                Shutdown();
                return;
            }

            base.OnStartup(e);

            var mainWindow = new MainWindow();
            mainWindow.Show();

            if (e.Args.Length > 0)
            {
                mainWindow.OpenFileFromArg(e.Args[0]);
            }
        }

        private void SendFileToExistingInstance(string filePath)
        {
            try
            {
                using var client = new System.IO.Pipes.NamedPipeClientStream(".", "AnimePlayerPipe", System.IO.Pipes.PipeDirection.Out);
                client.Connect(1000);
                using var writer = new System.IO.StreamWriter(client);
                writer.WriteLine(filePath);
            }
            catch { }
        }

        private void BringExistingWindowToFront()
        {
            foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcessesByName("AnimePlayer"))
            {
                if (process.Id != System.Diagnostics.Process.GetCurrentProcess().Id)
                {
                    ShowWindow(process.MainWindowHandle, 9); // SW_RESTORE
                    SetForegroundWindow(process.MainWindowHandle);
                    break;
                }
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _mutex?.ReleaseMutex();
            base.OnExit(e);
        }
    }
}