using System.Collections.Generic;

namespace Bilibili.Manga.Model.Common
{
    public sealed class VersionInfo
    {
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Build { get; set; }
        public string Message { get; set; }
        public List<VersionItem> Urls { get; set; }
    }
    public sealed class VersionItem
    {
        public string Ext { get; set; }
        public string Url { get; set; }
        public string Platform { get; set; }
    }
}