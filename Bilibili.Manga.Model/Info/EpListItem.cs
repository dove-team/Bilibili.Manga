namespace Bilibili.Manga.Model.Info
{
    public sealed class EpListItem
    {
        public int Id { get; set; }
        public decimal Ord { get; set; }
        public decimal Read { get; set; }
        public decimal Pay_Mode { get; set; }
        public string Is_Locked { get; set; }
        public decimal Pay_Gold { get; set; }
        public decimal Size { get; set; }
        public string Short_Title { get; set; }
        public string Is_In_Free { get; set; }
        public string Title { get; set; }
        private string _cover;
        public string Cover
        {
            get => _cover;
            set => _cover = value + "@600w.jpg";
        }
        public string Pub_Time { get; set; }
        public decimal Comments { get; set; }
        public string Unlock_Expire_At { get; set; }
        public decimal Unlock_Type { get; set; }
        public string Allow_Wait_Free { get; set; }
        public string Progress { get; set; }
        public decimal Like_Count { get; set; }
        public decimal Chapter_Id { get; set; }
        public decimal Type { get; set; }
        public decimal Extra { get; set; }
    }
}