using Bilibili.Manga.Common;
using Bilibili.Manga.Model;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Bilibili.Manga.WebClient
{
    public sealed class HttpClientEx : IDisposable
    {
        private HttpClient HttpClient { get; set; }
        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(8);
        public HttpClientEx()
        {
            var clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback += (sender, cert, chaun, ssl) => { return true; };
            HttpClient = new HttpClient(clientHandler) { Timeout = Timeout };
            HttpClient.DefaultRequestHeaders.Referrer = new Uri("http://www.bilibili.com/");
        }
        public async Task<string> GetResults(string url, ApiKeyInfo apiKeyInfo = null)
        {
            try
            {
                if (apiKeyInfo == null)
                    apiKeyInfo = ApiProvider.AndroidKey;
                if (url.IndexOf("?") > -1)
                    url += "&access_key=" + ApiProvider.AccessKey + "&appkey=" + apiKeyInfo.Appkey + "&platform=android&device=android&actionKey=appkey&build=5442100&mobi_app=android_comic&ts=" + ApiProvider.TimeSpanSeconds;
                else
                    url += "?access_key=" + ApiProvider.AccessKey + "&appkey=" + apiKeyInfo.Appkey + "&platform=android&device=android&actionKey=appkey&build=5442100&mobi_app=android_comic&ts=" + ApiProvider.TimeSpanSeconds;
                url += "&sign=" + ApiProvider.GetSign(url, apiKeyInfo);
                HttpResponseMessage hr = await HttpClient.GetAsync(url).ConfigureAwait(false);
                hr.EnsureSuccessStatusCode();
                var encodeResults = await hr.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                return Encoding.UTF8.GetString(encodeResults, 0, encodeResults.Length);
            }
            catch (Exception ex)
            {
                LogManager.Instance.LogError("GetResults", ex);
                return string.Empty;
            }
        }
        public async Task<Stream> GetImageStream(Uri url)
        {
            try
            {
                HttpResponseMessage hr = await HttpClient.GetAsync(url).ConfigureAwait(false);
                hr.EnsureSuccessStatusCode();
                return await hr.Content.ReadAsStreamAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                LogManager.Instance.LogError("GetBuffer", ex);
                return default;
            }
        }
        public async Task<byte[]> GetImageStream(string url)
        {
            try
            {
                HttpResponseMessage hr = await HttpClient.GetAsync(url).ConfigureAwait(false);
                hr.EnsureSuccessStatusCode();
                return await hr.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                LogManager.Instance.LogError("GetBuffer", ex);
                return default;
            }
        }
        public async Task<string> PostResults(string url, string postContent, ApiKeyInfo apiKeyInfo = null)
        {
            try
            {
                if (apiKeyInfo == null)
                    apiKeyInfo = ApiProvider.AndroidKey;
                if (!string.IsNullOrEmpty(postContent))
                    postContent += "&access_key=" + ApiProvider.AccessKey + "&appkey=" + apiKeyInfo.Appkey + "&mobi_app=android_comic&device=android&version="
                        + ApiProvider.version + "&actionKey=appkey&platform=android&ts=" + ApiProvider.TimeSpanSeconds;
                else
                    postContent += "access_key=" + SettingHelper.AccessKey + "&appkey=" + apiKeyInfo.Appkey + "&mobi_app=android_comic&device=android&version="
                        + ApiProvider.version + "&actionKey=appkey&platform=android&ts=" + ApiProvider.TimeSpanSeconds;
                postContent += "&sign=" + ApiProvider.GetSign(postContent, apiKeyInfo);
                StringContent stringContent = new StringContent(postContent, Encoding.UTF8, "application/x-www-form-urlencoded");
                var response = await HttpClient.PostAsync(url, stringContent);
                response.EnsureSuccessStatusCode();
                var encodeResults = await response.Content.ReadAsByteArrayAsync();
                return Encoding.UTF8.GetString(encodeResults, 0, encodeResults.Length);
            }
            catch (Exception ex)
            {
                LogManager.Instance.LogError("PostResults", ex);
                return string.Empty;
            }
        }
        public async Task<string> GetImageStream(string url, string fileName)
        {
            try
            {
                var bytes = await GetImageStream(url);
                if (fileName.IsNotEmpty())
                {
                    string savePath = string.Format("{0}{1}", fileName, Path.GetExtension(url));
                    FileInfo fileInfo = new FileInfo(savePath);
                    if (fileInfo.Exists)
                        fileInfo.Delete();
                    using (var fs = fileInfo.Create())
                        fs.Write(bytes, 0, bytes.Length);
                    return savePath;
                }
            }
            catch (Exception ex)
            {
                LogManager.Instance.LogError("GetImageStream", ex);
            }
            return string.Empty;
        }
        public void Dispose()
        {
            try
            {
                HttpClient.Dispose();
            }
            catch { }
        }
    }
}