namespace Bilibili.Manga.Model.Common
{
    public sealed class HistoryManga
    {
        public int Id { get; set; }
        public int Comic_Id { get; set; }
        public string Title { get; set; }
        public string Comic_Title { get; set; }
        public decimal Status { get; set; }
        public decimal Last_Ord { get; set; }
        public string Last_Ord_Title => "更新至 "+ Last_Ord+" 话";
        public decimal Ord_Count { get; set; }
        public string Read_Time { get; set; }
        public int Bought_Ep_Count { get; set; }
        public string Bought_Ep_Count_Title => "已购 " + Bought_Ep_Count + " 话";
        private string _hcover;
        public string Hcover
        {
            get => _hcover;
            set => _hcover = value + "@600w.jpg";
        }
        private string _scover;
        public string Scover
        {
            get => _scover;
            set => _scover = value + "@600w.jpg";
        }
        private string _vcover;
        public string Vcover
        {
            get => _vcover;
            set => _vcover = value + "@600w.jpg";
        }
        public string Publish_Time { get; set; }
        public decimal Last_Ep_Id { get; set; }
        public string Last_Ep_Short_Title { get; set; }
        public string Latest_Ep_Short_Title { get; set; }
        public decimal Type { get; set; }
        public string Sub_Title
        {
            get
            {
                return $"看到 {Last_Ep_Short_Title} 话 / {Latest_Ep_Short_Title} 话";
            }
        }
    }
}