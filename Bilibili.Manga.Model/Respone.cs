using Newtonsoft.Json;

namespace Bilibili.Manga.Model
{
    public sealed class Respone<T>
    {
        public int Code { get; set; }
        private string msg;
        public string Msg
        {
            get
            {
                if (string.IsNullOrEmpty(msg))
                    msg = Messsage;
                return msg;
            }
            set => msg = value;
        }
        public string Messsage { get; set; }
        public T Data { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}