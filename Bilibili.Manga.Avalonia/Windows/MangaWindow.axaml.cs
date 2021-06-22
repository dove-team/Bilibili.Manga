using Avalonia.Controls;
using Avalonia.Extensions.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Bilibili.Manga.Avalonia.ViewModels;
using Bilibili.Manga.Common;
using Bilibili.Manga.Model.Info;

namespace Bilibili.Manga.Avalonia.Windows
{
    public partial class MangaWindow : WindowBase
    {
        private int comicId;
        private Button BtnFollow { get; set; }
        private CellListView ListBox { get; set; }
        private MangaViewModel ViewModel { get; }
        public MangaWindow()
        {
            AvaloniaXamlLoader.Load(this);
            ViewModel = new MangaViewModel();
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            DataContext = ViewModel;
            ListBox = this.FindControl<CellListView>("listBox");
            BtnFollow = this.FindControl<Button>("btnFollow");
            BtnFollow.Click += BtnFollow_Click;
            ListBox.SelectionChanged += ListBox_SelectionChanged;
        }
        private async void BtnFollow_Click(object? sender, RoutedEventArgs e)
        {
            switch (BtnFollow.Content.ToString())
            {
                case "取消追漫":
                    {
                        var result = await ViewModel.UserClient.UnFollow(comicId);
                        if (result.IsSuccess())
                            await MessageBox.Show("提示", "取消追漫成功！", MessageBoxButtons.Ok);
                        ViewModel.UpdateFollow(false);
                        break;
                    }
                case "追漫":
                    {
                        var result = await ViewModel.UserClient.Follow(comicId);
                        if (result.IsSuccess())
                            await MessageBox.Show("提示", "追漫成功！", MessageBoxButtons.Ok);
                        ViewModel.UpdateFollow(true);
                        break;
                    }
            }
        }
        private void ListBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            var item = e.AddedItems.FindItem(0);
            if (item is EpListItem ep)
            {
                EpWindow epWindow = new EpWindow();
                epWindow.Init(ep.Id);
                epWindow.ShowDialog(this);
            }
        }
        public void InitData(decimal id)
        {
            comicId = id.ToInt32();
            ViewModel.GetData(id);
        }
    }
}