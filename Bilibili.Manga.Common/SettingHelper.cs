using Bilibili.Manga.Common.Database;
using Bilibili.Manga.Common.Database.Table;
using Bilibili.Manga.Model.User;
using System;

namespace Bilibili.Manga.Common
{
    public sealed class SettingHelper
    {
        public static string AccessKey
        {
            get
            {
                return GetData("AccessKey", out string value) ? value : string.Empty;
            }
            set
            {
                SaveData("AccessKey", value);
            }
        }
        public static long UserID
        {
            get
            {
                try
                {
                    if (GetData("UserID", out string value))
                        return Convert.ToInt64(value);
                    else return 0;
                }
                catch { return 0; }
            }
            set
            {
                SaveData("UserID", value.ToString());
            }
        }
        public static DateTime LoginExpires
        {
            get
            {
                try
                {
                    return GetData("LoginExpires", out string value) ? Convert.ToInt64(value).ToDateTime() : DateTime.MinValue;
                }
                catch { return DateTime.MinValue; }
            }
            set
            {
                SaveData("LoginExpires", value.ToTimeStamp().ToString());
            }
        }
        public static string RefreshToken
        {
            get
            {
                try
                {
                    return GetData("RefreshToken", out string value) ? value : string.Empty;
                }
                catch { return string.Empty; }
            }
            set
            {
                SaveData("RefreshToken", value.ToString());
            }
        }
        public static UserCardInfo UserInfo
        {
            get
            {
                try
                {
                    if (GetData("UserInfo", out string value))
                        return value.ToObject<UserCardInfo>();
                    else return default;
                }
                catch { return default; }
            }
            set
            {
                SaveData("UserInfo", value.ToJson());
            }
        }
        public static string UserHead
        {
            get
            {
                try
                {
                    return GetData("UserHead", out string value) ? value : string.Empty;
                }
                catch { return string.Empty; }
            }
            set
            {
                SaveData("UserHead", value);
            }
        }
        public static bool EnableLogging
        {
            get
            {
                try
                {
                    GetData("EnableLogging", out string value);
                    return value == "1";
                }
                catch { return true; }
            }
            set
            {
                SaveData("EnableLogging", value ? "1" : "0");
            }
        }
        public static bool DarkTheme
        {
            get
            {
                try
                {
                    GetData("DarkTheme", out string value);
                    return value == "1";
                }
                catch { return false; }
            }
            set
            {
                SaveData("DarkTheme", value ? "1" : "0");
            }
        }
        private static bool GetData(string name, out string value)
        {
            try
            {
                using (DB db = new DB())
                {
                    var entity = db.Connection.Table<Setting>().FirstOrDefault(x => x.Name == name);
                    value = entity?.Value;
                }
                if (string.IsNullOrEmpty(value))
                    value = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                value = string.Empty;
                LogManager.Instance.LogError("GetData", ex);
                return false;
            }
        }
        private static void SaveData(string name, string value)
        {
            try
            {
                using (DB db = new DB())
                {
                    var entity = db.Connection.Table<Setting>().FirstOrDefault(x => x.Name == name);
                    if (entity.IsNotEmpty())
                    {
                        entity.Value = value;
                        db.Connection.Update(entity);
                    }
                    else
                    {
                        entity = new Setting
                        {
                            Value = value,
                            Name = name
                        };
                        db.Connection.Insert(entity);
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.Instance.LogError("SaveData", ex);
            }
        }
    }
}