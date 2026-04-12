using System.Windows;

namespace AnimePlayer
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = new MainWindow();
            mainWindow.Show();

            // ファイルの関連付けから起動された場合
            if (e.Args.Length > 0)
            {
                mainWindow.OpenFileFromArg(e.Args[0]);
            }
        }
    }
}