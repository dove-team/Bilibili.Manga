using Avalonia.Threading;
using Bilibili.Manga.Common;
using Bilibili.Manga.Model.Common;
using Bilibili.Manga.Model.Info;
using Bilibili.Manga.WebClient;
using Bilibili.Manga.WebClient.Api;
using DynamicData;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;

namespace Bilibili.Manga.ViewModels
{
    public sealed class UserViewModel : ReactiveObject, IDisposable
    {
        public UserClient Client { get; }
        private int Page { get; set; } = 1;
        private ObservableCollection<FollowMange> mangas = new ObservableCollection<FollowMange>();
        private ObservableCollection<HistoryManga> buyMangas = new ObservableCollection<HistoryManga>();
        private ObservableCollection<HistoryManga> historyMangas = new ObservableCollection<HistoryManga>();
        public ObservableCollection<FollowMange> Mangas
        {
            get => mangas;
            private set => this.RaiseAndSetIfChanged(ref mangas, value);
        }
        public ObservableCollection<HistoryManga> BuyMangas
        {
            get => buyMangas;
            private set => this.RaiseAndSetIfChanged(ref buyMangas, value);
        }
        public ObservableCollection<HistoryManga> HistoryMangas
        {
            get => historyMangas;
            private set => this.RaiseAndSetIfChanged(ref historyMangas, value);
        }
        public UserViewModel()
        {
            Client = new UserClient();
        }
        public void GetFollowData(int order)
        {
            Dispatcher.UIThread.InvokeAsync(async () =>
            {
                try
                {
                    Mangas.Clear();
                    var result = await Client.Follow(order, Page);
                    if (result.IsSuccess())
                        Mangas.AddRange(result.Data);
                }
                catch { }
            });
        }
        public void GetHistoryData()
        {
            Dispatcher.UIThread.InvokeAsync(async () =>
            {
                try
                {
                    HistoryMangas.Clear();
                    var result = await Client.History(Page);
                    if (result.IsSuccess())
                        HistoryMangas.AddRange(result.Data);
                }
                catch { }
            });
        }
        public void Init()
        {
            if (ApiProvider.IsLogin)
            {
                GetFollowData(1);
                GetHistoryData();
                GetBuyData();
            }
        }
        public void GetBuyData()
        {
            Dispatcher.UIThread.InvokeAsync(async () =>
            {
                try
                {
                    BuyMangas.Clear();
                    var result = await Client.Buy(Page);
                    if (result.IsSuccess())
                        BuyMangas.AddRange(result.Data);
                }
                catch { }
            });
        }
        public void Dispose()
        {

        }
    }
}