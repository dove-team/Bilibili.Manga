namespace Bilibili.Manga.Model.Info
{
    public sealed class FollowMange
    {
        public int Id { get; set; }
        public int Comic_Id { get; set; }
        public string Title { get; set; }
        public decimal Status { get; set; }
        public decimal Last_Ord { get; set; }
        public decimal Ord_Count { get; set; }
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
        public string Last_Ep_Publish_Time { get; set; }
        public decimal Last_Ep_Id { get; set; }
        public string Last_Ep_Short_Title { get; set; }
        public string Latest_Ep_Short_Title { get; set; }
        public string Allow_Wait_Free { get; set; }
        public decimal Type { get; set; }
        public string Rookie_Expire_Time { get; set; }
        public decimal Free_Type { get; set; }
        public decimal Latest_Ep_Id { get; set; }
        public string Has_Read_Latest_Ep { get; set; }
        public string Sub_Title
        {
            get
            {
                return $"看到 {Last_Ep_Short_Title} 话 / {Latest_Ep_Short_Title} 话";
            }
        }
    }
}