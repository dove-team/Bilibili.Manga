using Bilibili.Manga.Common;
using Bilibili.Manga.Model;
using PCLCrypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bilibili.Manga.WebClient
{
    public sealed class ApiProvider
    {
        public const string version = "3.0.0";
        public static ApiKeyInfo AndroidKey { get; } = new ApiKeyInfo("1d8b6e7d45233436", "560c52ccd288fed045859ed18bffd973");
        public static ApiKeyInfo AndroidTVKey { get; } = new ApiKeyInfo("4409e2ce8ffd12b8", "59b43e04ad6965f34319062b478f83dd");
        private static string _AccessKey;
        public static string AccessKey
        {
            get
            {
                if (_AccessKey.IsEmpty())
                    _AccessKey = SettingHelper.AccessKey;
                return _AccessKey;
            }
            set { _AccessKey = value; }
        }
        public static string GetSign(string url, ApiKeyInfo apiKeyInfo = null)
        {
            if (apiKeyInfo == null) apiKeyInfo = AndroidKey;
            string str = url.Substring(url.IndexOf("?", 4) + 1);
            List<string> list = str.Split('&').ToList();
            list.Sort();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string val in list)
            {
                stringBuilder.Append(stringBuilder.Length > 0 ? "&" : string.Empty);
                stringBuilder.Append(val);
            }
            stringBuilder.Append(apiKeyInfo.Secret);
            return stringBuilder.ToString().ToMD5().ToLower();
        }
        public static string GetSignWithUrl(string url, ApiKeyInfo apiKeyInfo)
        {
            string str = url.Substring(url.IndexOf("?", 4) + 1);
            List<string> list = str.Split('&').ToList();
            list.Sort();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string val in list)
            {
                stringBuilder.Append((stringBuilder.Length > 0 ? "&" : string.Empty));
                stringBuilder.Append(val);
            }
            stringBuilder.Append(apiKeyInfo.Secret);
            var result = stringBuilder.ToString().ToMD5().ToLower();
            return url += "&sign=" + result;
        }
        public static string GetMd5String(string result)
        {
            var objAlgProv = WinRTCrypto.HashAlgorithmProvider.OpenAlgorithm(HashAlgorithm.Md5);
            CryptographicHash objHash = objAlgProv.CreateHash();
            var buffMsg = WinRTCrypto.CryptographicBuffer.ConvertStringToBinary(result, Encoding.BigEndianUnicode);
            objHash.Append(buffMsg);
            var buffHash = objHash.GetValueAndReset();
            return WinRTCrypto.CryptographicBuffer.EncodeToHexString(buffHash);
        }
        public static long TimeSpanSeconds
        {
            get { return Convert.ToInt64((DateTime.Now - new DateTime(1970, 1, 1, 8, 0, 0, 0)).TotalSeconds); }
        }
        public static long TimeSpanMilliseconds
        {
            get { return Convert.ToInt64((DateTime.Now - new DateTime(1970, 1, 1, 8, 0, 0, 0)).TotalMilliseconds); }
        }
        public static long UserId
        {
            get
            {
                return IsLogin ? SettingHelper.UserID : 0;
            }
        }
        public static bool IsLogin
        {
            get
            {
                return !string.IsNullOrEmpty(SettingHelper.AccessKey);
            }
        }
    }
}