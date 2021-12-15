using Newtonsoft.Json.Linq;

namespace Bilibili.Manga.Common
{
    public delegate void CallbackObject(object sender);
    public delegate void CallbackIntObject(object sender, int value);
    public delegate void CallbackBooleanObject(object sender, bool result);
    public delegate void CallbackJObjectObject(object sender, JObject data);
}