using Avalonia.Controls;
using Avalonia.Extensions.Controls;
using Avalonia.Extensions.Event;
using Avalonia.Input;
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
        private GridView ListBox { get; set; }
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
            ListBox = this.FindControl<GridView>("listBox");
            BtnFollow = this.FindControl<Button>("btnFollow");
            BtnFollow.Click += BtnFollow_Click;
            ListBox.ItemClick += ListBox_ItemClick;
        }
        private async void ListBox_ItemClick(object? sender, ViewRoutedEventArgs e)
        {
            if (e.ClickMouse == MouseButton.Left && e.ClickItem is GridViewItem viewItem && viewItem.Content is EpListItem ep)
            {
                EpWindow epWindow = new EpWindow();
                epWindow.Init(comicId, ep.Id);
                this.Hide();
                await epWindow.ShowDialog(this);
                this.Show();
            }
        }
        private async void BtnFollow_Click(object? sender, RoutedEventArgs e)
        {
            switch (BtnFollow.Content.ToString())
            {
                case "取消追漫":
                    {
                        var result = await ViewModel.UserClient.UnFollow(comicId);
                        ViewModel.UpdateFollow(false);
                        if (result.IsSuccess())
                            await MessageBox.Show("提示", "取消追漫成功！", MessageBoxButtons.Ok);
                        break;
                    }
                case "追漫":
                    {
                        var result = await ViewModel.UserClient.Follow(comicId);
                        ViewModel.UpdateFollow(true);
                        if (result.IsSuccess())
                            await MessageBox.Show("提示", "追漫成功！", MessageBoxButtons.Ok);
                        break;
                    }
            }
        }
        public void InitData(decimal id)
        {
            comicId = id.ToInt32();
            ViewModel.GetData(id);
        }
    }
}