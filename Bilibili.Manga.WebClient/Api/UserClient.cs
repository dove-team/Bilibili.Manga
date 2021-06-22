using Bilibili.Manga.Common;
using Bilibili.Manga.Model;
using Bilibili.Manga.Model.Common;
using Bilibili.Manga.Model.Info;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bilibili.Manga.WebClient.Api
{
    public sealed class UserClient
    {
        private HttpClientEx HttpClient => Current.HttpClient;
        public async Task<Respone<List<FollowMange>>> Follow(int order, int page)
        {
            try
            {
                var url = "https://manga.bilibili.com/twirp/bookshelf.v1.Bookshelf/ListFavorite";
                var content = "page_num=" + page + "&page_size=15&order=" + order;
                string results = await HttpClient.PostResults(url, content);
                var m = results.ToObject<Respone<List<FollowMange>>>();
                return m;
            }
            catch (Exception ex)
            {
                return new Respone<List<FollowMange>>
                {
                    Code = -1,
                    Msg = ex.Message
                };
            }
        }
        public async Task<Respone<List<HistoryManga>>> Buy(int page)
        {
            try
            {
                var url = "https://manga.bilibili.com/twirp/user.v1.User/GetAutoBuyComics";
                var content = "page_num=" + page + "&page_size=15";
                string results = await HttpClient.PostResults(url, content);
                var m = results.ToObject<Respone<List<HistoryManga>>>();
                return m;
            }
            catch (Exception ex)
            {
                return new Respone<List<HistoryManga>>
                {
                    Code = -1,
                    Msg = ex.Message
                };
            }
        }
        public async Task<Respone<List<HistoryManga>>> History(int page)
        {
            try
            {
                var url = "https://manga.bilibili.com/twirp/bookshelf.v1.Bookshelf/ListHistory";
                var content = "page_num=" + page + "&page_size=15";
                string results = await HttpClient.PostResults(url, content);
                var m = results.ToObject<Respone<List<HistoryManga>>>();
                return m;
            }
            catch (Exception ex)
            {
                return new Respone<List<HistoryManga>>
                {
                    Code = -1,
                    Msg = ex.Message
                };
            }
        }
        public async Task<Respone<JObject>> Follow(int id)
        {
            try
            {
                var url = "https://manga.bilibili.com/twirp/bookshelf.v1.Bookshelf/AddFavorite";
                var content = "comic_ids=" + id;
                string results = await HttpClient.PostResults(url, content);
                var m = results.ToObject<Respone<JObject>>();
                return m;
            }
            catch (Exception ex)
            {
                return new Respone<JObject>
                {
                    Code = -1,
                    Msg = ex.Message
                };
            }
        }
        public async Task<Respone<JObject>> UnFollow(int id)
        {
            try
            {
                var url = "https://manga.bilibili.com/twirp/bookshelf.v1.Bookshelf/DeleteFavorite";
                var content = "comic_ids=" + id;
                string results = await HttpClient.PostResults(url, content);
                var m = results.ToObject<Respone<JObject>>();
                return m;
            }
            catch (Exception ex)
            {
                return new Respone<JObject>
                {
                    Code = -1,
                    Msg = ex.Message
                };
            }
        }
        public async Task<Respone<JObject>> Delete(int id)
        {
            try
            {
                var url = "https://manga.bilibili.com/twirp/bookshelf.v1.Bookshelf/DeleteHistory";
                var content = "comic_ids=" + id;
                string results = await HttpClient.PostResults(url, content);
                var m = results.ToObject<Respone<JObject>>();
                return m;
            }
            catch (Exception ex)
            {
                return new Respone<JObject>
                {
                    Code = -1,
                    Msg = ex.Message
                };
            }
        }
    }
}