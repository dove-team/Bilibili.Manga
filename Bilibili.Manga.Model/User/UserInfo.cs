using System;
using System.Collections.Generic;
using System.Text;

namespace Bilibili.Manga.Model.User
{
    public sealed class UserInfo
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public UserInfo Data { get; set; }
        public UserCardInfo Card { get; set; }
        public UserInfo Favourite { get; set; }
        public UserInfo Season { get; set; }
        public UserInfo Coin_Archive { get; set; }
        public UserInfo Live { get; set; }
        public List<UserInfo> Item { get; set; }
        public string Fid { get; set; }
        public int Cur_Count { get; set; }
        public int? Relation { get; set; }
        public string Param { get; set; }
        public string Title { get; set; }
        public string Coins { get; set; }
        public object Cover { get; set; }
        public int LiveStatus { get; set; }
        public string Roomid { get; set; }
        public string Play { get; set; }
        public string Danmaku { get; set; }
        public long Ctime { get; set; }
        public string CTime
        {
            get
            {
                DateTime dtStart = new DateTime(1970, 1, 1);
                long lTime = long.Parse(Ctime + "0000000");
                TimeSpan toNow = new TimeSpan(lTime);
                return dtStart.Add(toNow).ToString("d");
            }
        }
        public int Current_Min { get; set; }
        public int Current_Exp { get; set; }
        public string Next_Exp { get; set; }
        public int Current_Level { get; set; }
        public string Place { get; set; }
        public string Toutu { get; set; }
        public int VipType { get; set; }
        public int VipStatus { get; set; }
        public string VipDueDate { get; set; }
        public string AccessStatus { get; set; }
        public string VipSurplusMsec { get; set; }
        public int Type { get; set; }
        public string Desc { get; set; }
    }
}