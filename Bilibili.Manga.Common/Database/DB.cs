using Bilibili.Manga.Common.Database.Table;
using Bilibili.Manga.Model;
using PCLUntils;
using PCLUntils.Plantform;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;

namespace Bilibili.Manga.Common.Database
{
    public sealed class DB : IDisposable
    {
        public SQLiteConnection Connection { get; }
        private static string _DbPath;
        private static string DbPath
        {
            get
            {
                if (_DbPath.IsEmpty())
                {
                    if (PlantformUntils.System == Platforms.Android)
                        _DbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "data.db");
                    else
                        _DbPath = Path.Combine(Extension.GetRoot(true), "data.db");
                }
                return _DbPath;
            }
        }
        public DB()
        {
            Connection = new SQLiteConnection(DbPath);
            if (!HasTable())
            {
                Connection.CreateTable<Setting>(CreateFlags.AutoIncPK);
                try
                {
                    Connection.InsertAll(new List<Setting>
                    {
                        new Setting { Name = "UseDASH", Value = "1" },
                    });
                }
                catch { }
            }
        }
        private bool HasTable()
        {
            try
            {
                Connection.Table<Setting>().FirstOrDefault();
                return true;
            }
            catch { }
            return false;
        }
        public void Dispose()
        {
            try
            {
                Connection.Close();
                Connection.Dispose();
            }
            catch { }
        }
    }
}