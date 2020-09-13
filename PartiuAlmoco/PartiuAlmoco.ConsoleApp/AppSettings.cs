using PartiuAlmoco.Infra.Domain;
using System.IO;

namespace PartiuAlmoco.ConsoleApp
{
    public class AppSettings : IAppSettings
    {
        public string DatabaseConnectionString
        {
            get
            {
                var dir = Directory.GetCurrentDirectory();
                var dbPath = Path.Combine(dir, "data.sqlite");
#if DEBUG
                dbPath = @"C:\temp\data.sqlite";
#endif
                return @$"Data Source={dbPath}";
            }
        }
    }
}