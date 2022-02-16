using Bilibili.Manga.Common;
using Bilibili.Manga.Model;
using Bilibili.Manga.Model.Common;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Bilibili.Manga.WebClient.Api
{
    public sealed class UpdateClient
    {
        private HttpClientEx HttpClient => Current.HttpClient;
        public async Task<string> CheckUpdate()
        {
            var updatePath = string.Empty;
            try
            {
                var stringContent = await HttpClient.GetStringAsync("https://michael-eddy.github.io/config/avalonia-bilibili-manga.json");
                var model = stringContent.ToObject<VersionInfo>();
                var version = Assembly.GetExecutingAssembly().GetName().Version;
                if (version != null && model != null)
                {
                    if ((version.Major < model.Major || version.Minor < model.Minor || version.Build < model.Build) ||
                        Debugger.IsAttached)
                    {
                        var uri = model.Urls.FirstOrDefault(x => x.Platform.Equals(CurrentSystemTag, StringComparison.CurrentCultureIgnoreCase));
                        var url = uri?.Url;
                        if (!string.IsNullOrEmpty(url))
                        {
                            var content = await HttpClient.GetStringAsync($"https://api.pingping6.com/tools/lanzou/?url={url}");
                            var info = content.ToJObject();
                            if (info != null && Convert.ToInt32(info["code"].ToString()) == 1)
                            {
                                updatePath = info["file"].ToString();
                                LogManager.Instance.LogInfo($"DownloadPath:{updatePath}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.Instance.LogError("CheckUpdate", ex);
            }
            return updatePath;
        }
        public void ApplyNow(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    switch (Runtime.Platform)
                    {
                        case Platforms.Windows:
                            Process.Start(filePath);
                            break;
                        case Platforms.Linux:

                            break;
                        case Platforms.MacOS:

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.Instance.LogError("ApplyNow", ex);
            }
        }
        private string CurrentSystemTag
        {
            get
            {
                (string, string) tags = default;
                switch (Runtime.Platform)
                {
                    case Platforms.Windows:
                        {
                            tags.Item1 = "win";
                            switch (RuntimeInformation.ProcessArchitecture)
                            {
                                case Architecture.X86:
                                    tags.Item2 = "x86";
                                    break;
                                case Architecture.X64:
                                    tags.Item2 = "x64";
                                    break;
                                default:
                                    tags.Item2 = "arm64";
                                    break;
                            }
                            break;
                        }
                    case Platforms.MacOS:
                        {
                            tags.Item1 = "osx";
                            switch (RuntimeInformation.ProcessArchitecture)
                            {
                                case Architecture.X64:
                                case Architecture.X86:
                                    tags.Item2 = "x64";
                                    break;
                                default:
                                    tags.Item2 = "arm64";
                                    break;
                            }
                            break;
                        }
                    case Platforms.Linux:
                        {
                            tags.Item1 = "linux";
                            switch (RuntimeInformation.ProcessArchitecture)
                            {
                                case Architecture.X64:
                                case Architecture.X86:
                                    tags.Item2 = "x64";
                                    break;
                                case Architecture.Arm:
                                    tags.Item2 = "arm";
                                    break;
                                default:
                                    tags.Item2 = "arm64";
                                    break;
                            }
                            break;
                        }
                }
                return $"{tags.Item1}-{tags.Item2}";
            }
        }
    }
}