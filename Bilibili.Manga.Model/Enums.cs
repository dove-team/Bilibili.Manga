namespace Bilibili.Manga.Model
{
    public enum LoginStatus
    {
        NeedCaptcha,
        NeedValidate,
        Success,
        Fail,
        Error
    }
    public enum Platforms
    {
        UnSupport,
        Android,
        Linux,
        Windows,
        MacOS
    }
}