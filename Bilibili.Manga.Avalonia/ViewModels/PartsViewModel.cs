using Avalonia.Threading;
using Bilibili.Manga.Common;
using Bilibili.Manga.Model.Common;
using Bilibili.Manga.Model.Home;
using Bilibili.Manga.WebClient.Api;
using DynamicData;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;

namespace Bilibili.Manga.ViewModels
{
    public sealed class PartsViewModel : ReactiveObject, IDisposable
    {
        public int Page { get; set; } = 1;
        public List<TagItem> Tags { get; }
        public ManagaClient Client { get; }
        public bool IsFinish { get; set; } = false;
        private ObservableCollection<PartManga> mangas = new ObservableCollection<PartManga>();
        public ObservableCollection<PartManga> Mangas
        {
            get => mangas;
            private set => this.RaiseAndSetIfChanged(ref mangas, value);
        }
        public ReactiveCommand<TagItem, Unit> OnSelectRadio { get; }
        public PartsViewModel()
        {
            Tags = new List<TagItem>
            {
                new TagItem ("styles"),
                new TagItem ("areas"),
                new TagItem ("status"),
                new TagItem ("orders"),
                new TagItem ("prices")
            };
            Client = new ManagaClient();
            OnSelectRadio = ReactiveCommand.Create<TagItem>(SelectRadio);
        }
        private void SelectRadio(TagItem param)
        {
            if (IsFinish)
            {
                var item = Tags.FirstOrDefault(x => x.Tag == param.Tag);
                item.Id = param.Id;
                item.Name = param.Name;
                GetList();
            }
        }
        public void GetList()
        {
            Dispatcher.UIThread.InvokeAsync(async () =>
             {
                 try
                 {
                     if (Page == 1)
                         Mangas.Clear();
                     var styleId = Tags.FirstOrDefault(x => x.Tag == "styles").Id;
                     var areaId = Tags.FirstOrDefault(x => x.Tag == "areas").Id;
                     var isFinish = Tags.FirstOrDefault(x => x.Tag == "status").Id;
                     var order = Tags.FirstOrDefault(x => x.Tag == "orders").Id;
                     var isFree = Tags.FirstOrDefault(x => x.Tag == "prices").Id;
                     var result = await Client.GetTypeList(styleId, areaId, isFree, order, isFinish, Page);
                     if (result.IsSuccess())
                         Mangas.AddRange(result.Data);
                 }
                 catch { }
             });
        }
        public void Dispose()
        {

        }
    }
}