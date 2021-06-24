using Bilibili.Manga.Common;
using Bilibili.Manga.Model;
using Bilibili.Manga.Model.Common;
using Bilibili.Manga.Model.Home;
using Bilibili.Manga.Model.Info;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bilibili.Manga.WebClient.Api
{
    public sealed class ManagaClient
    {
        private HttpClientEx HttpClient => Current.HttpClient;
        public async Task<Respone<List<PartManga>>> GetTypeList(string styleId, string areaId, string isFree, string order, string isFinish, int page)
        {
            try
            {
                var url = "https://manga.bilibili.com/twirp/comic.v1.Comic/ClassPage";
                var content = "style_id=" + styleId + "&area_id=" + areaId + "&is_finish=" + isFinish + "&order=" + order + "&is_free=" + isFree + "&page_num=" +
                    page + "&page_size=15";
                string results = await HttpClient.PostResults(url, content);
                var m = results.ToObject<Respone<List<PartManga>>>();
                return m;
            }
            catch (Exception ex)
            {
                LogManager.Instance.LogError("GetTypeList", ex);
                return new Respone<List<PartManga>>
                {
                    Code = -1,
                    Msg = ex.Message
                };
            }
        }
        public async Task<Respone<MangaInfo>> GetInfo(decimal id)
        {
            try
            {
                var url = "https://manga.bilibili.com/twirp/comic.v1.Comic/ComicDetail";
                var content = "comic_id=" + id;
                string results = await HttpClient.PostResults(url, content);
                var m = results.ToObject<Respone<MangaInfo>>();
                return m;
            }
            catch (Exception ex)
            {
                LogManager.Instance.LogError("GetInfo", ex);
                return new Respone<MangaInfo>
                {
                    Code = -1,
                    Msg = ex.Message
                };
            }
        }
        public async Task<Respone<Recommand>> Recommand()
        {
            try
            {
                var url = "https://manga.bilibili.com/twirp/comic.v1.Comic/HomeRecommend";
                var content = "page_num=1&omit_cards=1&drag=0&new_fall_into_trap=0";
                string results = await HttpClient.PostResults(url, content);
                var m = results.ToObject<Respone<Recommand>>();
                return m;
            }
            catch (Exception ex)
            {
                LogManager.Instance.LogError("Recommand", ex);
                return new Respone<Recommand>
                {
                    Code = -1,
                    Msg = ex.Message
                };
            }
        }
        public async Task<Respone<List<Mange>>> Rank()
        {
            try
            {
                var url = "https://manga.bilibili.com/twirp/comic.v1.Comic/HomeHot";
                string results = await HttpClient.PostResults(url, string.Empty);
                var m = results.ToObject<Respone<List<Mange>>>();
                return m;
            }
            catch (Exception ex)
            {
                LogManager.Instance.LogError("Rank", ex);
                return new Respone<List<Mange>>
                {
                    Code = -1,
                    Msg = ex.Message
                };
            }
        }
        public async Task<Respone<List<PartManga>>> New(int page)
        {
            try
            {
                var url = "https://manga.bilibili.com/twirp/comic.v1.Comic/ClassPage";
                var content = "style_id=-1&area_id=-1&is_finish=-1&order=3&is_free=-1&page_num=" + page + "&page_size=20";
                string results = await HttpClient.PostResults(url, content);
                var m = results.ToObject<Respone<List<PartManga>>>();
                return m;
            }
            catch (Exception ex)
            {
                LogManager.Instance.LogError("New", ex);
                return new Respone<List<PartManga>>
                {
                    Code = -1,
                    Msg = ex.Message
                };
            }
        }
        public async Task<Respone<Search>> Search(string keywords, int page)
        {
            try
            {
                var url = "https://manga.bilibili.com/twirp/comic.v1.Comic/Search";
                var content = "key_word=" + Uri.EscapeUriString(keywords) + "&page_num=" + page + "&page_size=20";
                string results = await HttpClient.PostResults(url, content);
                var m = results.ToObject<Respone<Search>>();
                return m;
            }
            catch (Exception ex)
            {
                LogManager.Instance.LogError("Search", ex);
                return new Respone<Search>
                {
                    Code = -1,
                    Msg = ex.Message
                };
            }
        }
        public new async Task<Respone<Types>> GetType()
        {
            try
            {
                var url = "https://manga.bilibili.com/twirp/comic.v1.Comic/AllLabel";
                string results = await HttpClient.PostResults(url, string.Empty);
                var m = results.ToObject<Respone<Types>>();
                return m;
            }
            catch (Exception ex)
            {
                LogManager.Instance.LogError("GetType", ex);
                return new Respone<Types>
                {
                    Code = -1,
                    Msg = ex.Message
                };
            }
        }
    }
}