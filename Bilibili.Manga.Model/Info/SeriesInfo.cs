using System.Collections.Generic;

namespace Bilibili.Manga.Model.Info
{
    public sealed class SeriesInfo
    {
        public int Id { get; set; }
        public List<SeriesComic> Comics { get; set; }
    }
    public sealed class SeriesComic
    {
        public int Comic_Id { get; set; }
        public string Title { get; set; }
    }
}