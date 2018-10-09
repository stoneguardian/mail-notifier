using System;
using System.Collections.Generic;
using System.IO;
using MailKit;
using Newtonsoft.Json;

namespace MailNotifier
{
    public class Cache
    {
        public readonly string CacheDirectory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "mail-notifier"
            );

        public Cache()
        {
            //Ensure created
            System.Console.WriteLine($"Cache stored in: {CacheDirectory}");
            Directory.CreateDirectory(CacheDirectory);
        }

        private string GetAccountPath(string account)
        {
            return Path.Combine(CacheDirectory, $"{account}.json");
        }

        public void CacheNotifiedMessages(string account, IEnumerable<uint> ids)
        {
            var outputPath = GetAccountPath(account);
            string output = JsonConvert.SerializeObject(ids);

            using (var file = new StreamWriter(outputPath, false))
            {
                file.WriteLine(output);
            }
        }

        public IEnumerable<uint> GetNotifiedMessages(string account)
        {
            var accountPath = GetAccountPath(account);

            if (!File.Exists(accountPath))
            {
                return new List<uint>();
            }

            using (var file = new StreamReader(accountPath))
            {
                var json = file.ReadToEnd();
                return JsonConvert.DeserializeObject<List<uint>>(json);
            }
        }
    }
}