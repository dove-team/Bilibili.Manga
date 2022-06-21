using Avalonia;
using Avalonia.ReactiveUI;
using Avalonia.Threading;
using Avalonia.Extensions.Controls;
using Bilibili.Manga.WebClient;
using ReactiveUI;
using System;
using System.IO;
using System.Reflection;
using System.Threading;
using PCLUntils;
using PCLUntils.Plantform;

namespace Bilibili.Manga.Avalonia
{
    class Program
    {
        public static void Main(string[] args)
        {
            var name = Assembly.GetExecutingAssembly().GetName().Name;
            using Mutex mutex = new Mutex(true, name, out bool createNew);
            if (createNew)
            {
                if (PlantformUntils.Platform != Platforms.UnSupport)
                {
                    RxApp.MainThreadScheduler = AvaloniaScheduler.Instance;
                    var builder = AppBuilder.Configure<App>()
                        .UsePlatformDetect()
                        .UseDoveExtensions()
                        .UseChineseInputSupport()
                        .UseReactiveUI()
                        .LogToTrace();
                    Current.Init();
                    builder.StartWithClassicDesktopLifetime(args);
                }
                else
                {
                    var file = Path.Combine(Environment.CurrentDirectory, "!!!!!!!!!!!DONOT SUPPORT YOUR SYSTEM!!!!!!!!!!!");
                    if (!File.Exists(file))
                        File.Create(file).Dispose();
                    Environment.Exit(1);
                }
            }
            else
            {
                Environment.Exit(1);
            }
        }
    }
}