using Bilibili.Manga.Model;
using Bilibili.Manga.Model.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PCLCrypto;
using PCLUntils;
using PCLUntils.Plantform;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Bilibili.Manga.Common
{
    public static class Extension
    {
        public static string Root
        {
            get
            {
                switch (PlantformUntils.System)
                {
                    case Platforms.Windows:
                        {
                            var assemblyLocation = Assembly.GetEntryAssembly()?.Location ?? Assembly.GetExecutingAssembly()?.Location;
                            if (!string.IsNullOrEmpty(assemblyLocation))
                                return Path.GetDirectoryName(assemblyLocation);
                            return AppDomain.CurrentDomain.BaseDirectory;
                        }
                    default:
                        {
                            var root = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                            return Path.Combine(root, ".michael_bilibili");
                        }
                }
            }
        }
        public static int ToInt32(this object obj)
        {
            try
            {
                if (obj is int value)
                    return value;
                else
                {
                    if (int.TryParse(obj.ToString(), out value))
                        return value;
                    else
                        return Convert.ToInt32(obj.ToString());
                }
            }
            catch {  }
            return 0;
        }
        public static long ToInt64(this object obj)
        {
            try
            {
                if (long.TryParse(obj.ToString(), out long result))
                    return result;
                else
                    return Convert.ToInt64(obj.ToString());
            }
            catch { }
            return 0;
        }
        public static bool IsSuccess<T>(this Respone<T> respone, int code = 0)
        {
            if (respone.IsNotEmpty())
                return respone.Code == code;
            return false;
        }
        public static List<TagItem> SetAllTag(this List<TagItem> list, string tag)
        {
            if (list == null)
                list = new List<TagItem>();
            if (!list.Any(x => x.Name.Contains("全部")))
                list.Insert(0, new TagItem(tag));
            return list;
        }
        public static string ToJson<T>(this T obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj);
            }
            catch { }
            return string.Empty;
        }
        public static long ToTimeStamp(this DateTime time)
        {
            DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(time.AddHours(-8) - Jan1st1970).TotalMilliseconds;
        }
        public static DateTime ToDateTime(this long timeStamp)
        {
            var start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return start.AddMilliseconds(timeStamp).AddHours(8);
        }
        public static bool IsEmpty<T>(this T obj)
        {
            try
            {
                if (obj == null) return true;
                if (obj is string) return string.IsNullOrEmpty(obj.ToString());
                else return obj.Equals(default(T));
            }
            catch { }
            return true;
        }
        public static bool IsNotEmpty<T>(this T obj)
        {
            return !IsEmpty(obj);
        }
        public static JObject ToJObject(this string json)
        {
            try
            {
                return JObject.Parse(json);
            }
            catch { }
            return default;
        }
        public static T ToObject<T>(this string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return default;
        }
        public static string ToMD5(this string input)
        {
            try
            {
                var asymmetricKeyAlgorithmProvider = WinRTCrypto.HashAlgorithmProvider.OpenAlgorithm(HashAlgorithm.Md5);
                var cryptographicKey = WinRTCrypto.CryptographicBuffer.ConvertStringToBinary(input, Encoding.UTF8);
                var hashData = asymmetricKeyAlgorithmProvider.HashData(cryptographicKey);
                return WinRTCrypto.CryptographicBuffer.EncodeToHexString(hashData);
            }
            catch { }
            return input;
        }
    }
}