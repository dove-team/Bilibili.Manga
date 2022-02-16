using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Extensions.Controls;
using Avalonia.Markup.Xaml;
using Bilibili.Manga.Common;
using Bilibili.Manga.WebClient;
using Bilibili.Manga.WebClient.Api;
using ReactiveUI;
using System;
using System.IO;
using System.Reactive;

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
                await MessageBox.Show("提示", "下载新版本完毕，由于系统限制需要手动进行安装。", MessageBoxButtons.Ok);
                new UpdateClient().ApplyNow(UpdatePackage);
                Exit();
            }
        }
        public static void Exit()
        {
            if (App.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                desktop.Shutdown();
        }
        private void ExceptionHandler(Exception exception)
        {
            LogManager.Instance.LogError("DefaultException", exception);
        }
    }
}