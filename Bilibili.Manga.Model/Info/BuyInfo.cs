using System.Collections.Generic;

namespace Bilibili.Manga.Model.Info
{
    public sealed class BuyInfo
    {
        public int Remain_Coupon { get; set; }
        public int Remain_Gold { get; set; }
        public int Pay_Gold { get; set; }
        public int Recommend_Coupon_Id { get; set; }
        public int Remain_Lock_Ep_Num { get; set; }
        public int Auto_Pay_Gold_Status { get; set; }
        public int Auto_Pay_Coupons_Status { get; set; }
        public int Remain_Lock_Ep_Gold { get; set; }
        public int Comic_Id { get; set; }
        public string Is_Locked { get; set; }
        public string Allow_Coupon { get; set; }
        public int After_Lock_Ep_Gold { get; set; }
        public int After_lock_Ep_num { get; set; }
        public string First_Image_path { get; set; }
        public string First_Image_Url { get; set; }
        public string First_Image_Token { get; set; }
        public string Last_Image_Path { get; set; }
        public string Last_Image_Url { get; set; }
        public string Last_Image_Token { get; set; }
        public int Discount_Type { get; set; }
        public int Discount { get; set; }
        public int Original_Gold { get; set; }
        public int First_Bonus_Percent { get; set; }
        public string Has_First_Bonus { get; set; }
        public int Ep_Discount_Type { get; set; }
        public int Ep_Discount { get; set; }
        public int Ep_Original_Gold { get; set; }
        public int Recommend_Item_Id { get; set; }
        public string Allow_Item { get; set; }
        public int Remain_Item { get; set; }
        public string Allow_Wait_Free { get; set; }
        public string Wait_Free_At { get; set; }
        public string Has_Newbie_Gift { get; set; }
        public int Recommend_Discount_Id { get; set; }
        public int Recommend_Discount { get; set; }
        public int Remain_Discount_Card { get; set; }
        public int Discount_Ep_Gold { get; set; }
        public int Discount_Remain_Gold { get; set; }
        public int Remain_Silver { get; set; }
        public int Ep_Silver { get; set; }
        public string Pay_Entry_Txt { get; set; }
        public int User_Card_State { get; set; }
        public int Price_Type { get; set; }
        public List<BatchBuy> Batch_Buy { get; set; }
    }
    public sealed class BatchBuy
    {
        public int Batch_Limit { get; set; }
        public int Amount { get; set; }
        public int Original_Gold { get; set; }
        public int Pay_Gold { get; set; }
        public int Discount_Type { get; set; }
        public int Discount { get; set; }
        public int Discount_Batch_Gold { get; set; }
        public string Usable { get; set; }
    }
}