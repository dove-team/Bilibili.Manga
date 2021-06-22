using Bilibili.Manga.Model.Common;
using System.Collections.Generic;

namespace Bilibili.Manga.Model.Info
{
    public sealed class EpInfo
    {
        public string Path { get; set; }
        public List<EpImage> Images { get; set; }
        public string Last_Modified { get; set; }
        public string Host { get; set; }
    }
}