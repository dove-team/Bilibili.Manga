namespace Bilibili.Manga.Model.Common
{
    public sealed class TagItem
    {
        public TagItem() { }
        public TagItem(string tag)
        {
            Id = "-1";
            Name = "全部";
            Tag = tag;
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Tag { get; set; }
    }
}