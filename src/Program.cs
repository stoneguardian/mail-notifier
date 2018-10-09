using System;
using System.Threading.Tasks;
using Tmds.DBus;
using OnlineAccounts.DBus;
using MailKit.Net.Imap;
using MailNotifier.AccountRepositories;
using MailKit;
using MailKit.Security;

namespace MailNotifier
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Getting gnome accounts...");
            var gnomeRepo = new GnomeOnlineAccounts();
            var accounts = await gnomeRepo.GetAllMailAddresses();

            foreach (var account in accounts)
            {
                Console.WriteLine($"Connecting to: {account}");
                using (var client = await gnomeRepo.GetImapClient(account))
                {
                    var inbox = client.Inbox;
                    await inbox.OpenAsync(FolderAccess.ReadOnly);
                    Console.WriteLine($"Total messages in inbox: {inbox.Count}\n");

                    await client.DisconnectAsync(true);
                }
            }
        }
    }
}
