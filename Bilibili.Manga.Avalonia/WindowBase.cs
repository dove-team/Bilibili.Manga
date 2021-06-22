using Avalonia;
using Avalonia.Controls;
using System;
using System.Diagnostics;
using System.IO;

namespace Bilibili.Manga.Avalonia
{
    public class WindowBase : Window
    {
        public WindowBase()
        {
            if (Debugger.IsAttached)
                this.AttachDevTools();

            this.MinWidth = 640;
            this.MinHeight = 480;
        }
        public void SetSize(Size size)
        {
            Width = size.Width;
            Height = size.Height;
            this.MinWidth = double.NaN;
            this.MinHeight = double.NaN;
        }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            try
            {
                var fileName = Path.Combine(AppContext.BaseDirectory, "Assets", "icon.ico");
                Icon = new WindowIcon(fileName);
            }
            catch { }
        }
    }
}