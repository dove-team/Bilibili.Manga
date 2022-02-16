using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Extensions.Controls;
using Avalonia.Markup.Xaml;
using Bilibili.Manga.Common;
using Bilibili.Manga.WebClient;
using Bilibili.Manga.WebClient.Api;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.IO;
using System.Reactive;
using System.Threading.Tasks;

namespace Bilibili.Manga.Avalonia
{
    public class App : ApplicationBase
    {
        public static string UpdatePackage { get; set; }
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            DownloadManager.Instance.Init(Environment.CurrentDirectory);
            RxApp.DefaultExceptionHandler = Observer.Create<Exception>(ExceptionHandler);
        }
        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
                desktop.Exit += Desktop_Exit;
            }
            base.OnFrameworkInitializationCompleted();
        }
        private async void Desktop_Exit(object? sender, ControlledApplicationLifetimeExitEventArgs e)
        {
            if (File.Exists(UpdatePackage))
            {
                new UpdateClient().ApplyNow(UpdatePackage);
                await Task.Delay(800);
                Process.GetCurrentProcess().Kill();
            }
        }
        private void ExceptionHandler(Exception exception)
        {
            LogManager.Instance.LogError("DefaultException", exception);
        }
    }
}