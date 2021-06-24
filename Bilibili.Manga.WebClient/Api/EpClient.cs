using Bilibili.Manga.Common;
using Bilibili.Manga.Model;
using Bilibili.Manga.Model.Common;
using Bilibili.Manga.Model.Info;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bilibili.Manga.WebClient.Api
{
    public sealed class EpClient
    {
        private HttpClientEx HttpClient => Current.HttpClient;
        public async Task<Respone<EpInfo>> GetImages(int id)
        {
            try
            {
                var url = "https://manga.bilibili.com/twirp/comic.v1.Comic/GetImageIndex";
                var content = "ep_id=" + id;
                string results = await HttpClient.PostResults(url, content);
                var m = results.ToObject<Respone<EpInfo>>();
                return m;
            }
            catch (Exception ex)
            {
                LogManager.Instance.LogError("GetImages", ex);
                return new Respone<EpInfo>
                {
                    Code = -1,
                    Msg = ex.Message
                };
            }
        }
        public async Task<Respone<BuyInfo>> BuyInfo(int id)
        {
            try
            {
                var url = "https://manga.bilibili.com/twirp/comic.v1.Comic/GetEpisodeBuyInfo";
                var content = "ep_id=" + id;
                string results = await HttpClient.PostResults(url, content);
                var m = results.ToObject<Respone<BuyInfo>>();
                return m;
            }
            catch (Exception ex)
            {
                LogManager.Instance.LogError("BuyInfo", ex);
                return new Respone<BuyInfo>
                {
                    Code = -1,
                    Msg = ex.Message
                };
            }
        }
        public async Task<Respone<object>> Buy(int id, int type, int cid, int pay)
        {
            try
            {
                var url = "https://manga.bilibili.com/twirp/comic.v1.Comic/BuyEpisode";
                string content = string.Empty;
                switch (type)
                {
                    case 1:
                        content = "buy_method=3&ep_id=" + id + "&pay_amount=" + pay + "&auto_pay_gold_status=2";
                        break;
                    case 2:
                        content = "buy_method=2&auto_pay_gold_status=2&ep_id=" + id + "&coupon_id=" + cid;
                        break;
                    case 3:
                        content = "buy_method=5&ep_id=" + id;
                        break;
                    case 4:
                        content = "buy_method=4&auto_pay_gold_status=2&ep_id=" + id + "&coupon_id=" + cid;
                        break;
                }
                string results = await HttpClient.PostResults(url, content);
                var m = results.ToObject<Respone<object>>();
                return m;
            }
            catch (Exception ex)
            {
                LogManager.Instance.LogError("Buy", ex);
                return new Respone<object>
                {
                    Code = -1,
                    Msg = ex.Message
                };
            }
        }
        public async Task<Respone<List<ImageToken>>> GetUrl(IEnumerable<string> urls)
        {
            try
            {
                var url = "https://manga.bilibili.com/twirp/comic.v1.Comic/ImageToken";
                var content = $"urls={urls.ToJson()}";
                string results = await HttpClient.PostResults(url, content);
                var m = results.ToObject<Respone<List<ImageToken>>>();
                return m;
            }
            catch (Exception ex)
            {
                LogManager.Instance.LogError("GetUrl", ex);
                return new Respone<List<ImageToken>>
                {
                    Code = -1,
                    Msg = ex.Message
                };
            }
        }
    }
}