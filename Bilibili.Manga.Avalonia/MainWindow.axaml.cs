using Avalonia.Markup.Xaml;

namespace Bilibili.Manga.Avalonia
{
    public partial class MainWindow : WindowBase
    {
        public MainWindow()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}