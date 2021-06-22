using Avalonia.Threading;
using Bilibili.Manga.Common;
using Bilibili.Manga.Model.Info;
using Bilibili.Manga.WebClient.Api;
using DynamicData;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;

namespace Bilibili.Manga.Avalonia.ViewModels
{
    public sealed class MangaViewModel : ReactiveObject, IDisposable
    {
        private ObservableCollection<EpListItem> epList = new ObservableCollection<EpListItem>();
        public ObservableCollection<EpListItem> EpList
        {
            get => epList;
            private set => this.RaiseAndSetIfChanged(ref epList, value);
        }
        private string follow;
        public string Follow
        {
            get => follow;
            private set => this.RaiseAndSetIfChanged(ref follow, value);
        }
        private MangaInfo info;
        public MangaInfo Info
        {
            get => info;
            private set => this.RaiseAndSetIfChanged(ref info, value);
        }
        public ManagaClient Client { get; }
        public UserClient UserClient { get; }
        public MangaViewModel()
        {
            Client = new ManagaClient();
            UserClient = new UserClient();
        }
        public void GetData(decimal id)
        {
            Dispatcher.UIThread.InvokeAsync(async () =>
            {
                try
                {
                    var result = await Client.GetInfo(id);
                    if (result.IsSuccess())
                    {
                        Info = result.Data;
                        Follow = info.Fav == 1 ? "取消追漫" : "追漫";
                        EpList.AddRange(result.Data.Ep_List);
                    }
                }
                catch { }
            });
        }
        public void UpdateFollow(bool follow)
        {
            Follow = follow ? "追漫" : "取消追漫";
        }
        public void Dispose()
        {

        }
    }
}