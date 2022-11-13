using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using ReactiveUI;
using System.Reactive;

namespace Bilibili.Manga
{
    public sealed class StaticValue
    {
        static StaticValue()
        {
            AssetLoader = AvaloniaLocator.Current.GetService<IAssetLoader>();
        }
        public static long CurrentUserId { get; set; }
        public static string CurrentUserName { get; set; }
        public static Bitmap CurrentUserIcon { get; set; }
        public static IAssetLoader AssetLoader { get; }
        public static ReactiveCommand<Unit, Unit> LoginEvent { get; set; }
    }
}