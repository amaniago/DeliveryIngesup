using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Interop;

namespace DataAccess
{
    public static class ConnectionHelper
    {
        public static SQLiteAsyncConnection GetConnection(ISQLitePlatform platform, string path)
        {
            return
                new SQLiteAsyncConnection(
                    () =>
                        new SQLiteConnectionWithLock(platform,
                            new SQLiteConnectionString(path, false)));
        }
    }
}
