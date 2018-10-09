using System;
using System.Threading.Tasks;
using Tmds.DBus;
using OnlineAccounts.DBus;
using MailKit.Net.Imap;
using MailNotifier.AccountRepositories;
using MailKit;
using MailKit.Security;
using MailKit.Search;

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
                    Console.WriteLine($"Total messages in inbox: {inbox.Count}");

                    var unread = await inbox.SearchAsync(SearchQuery.NotSeen);
                    Console.WriteLine($"Total unread (1): {unread.Count}");

                    var unread2 = await inbox.SearchAsync(SearchQuery.New);
                    Console.WriteLine($"Total unread (2, new):  {unread2.Count}");

                    if (unread2.Count > 0)
                    {
                        var notificationMessage = "";

                        switch (unread2.Count)
                        {
                            case 1:
                                notificationMessage = string.Format("There is {0} unread message", unread2.Count);
                                break;

                            default:
                                notificationMessage = string.Format("There are {0} unread messages", unread2.Count);
                                break;
                        }

                        var process = new System.Diagnostics.Process
                        {
                            StartInfo = new System.Diagnostics.ProcessStartInfo
                            {
                                FileName = "/bin/sh",
                                Arguments = $"-c \"notify-send '{account}' '{notificationMessage}'\"",
                                CreateNoWindow = true
                            }
                        };

                        Console.WriteLine("Sending notification...");
                        process.Start();
                        process.WaitForExit();
                    }


                    Console.WriteLine("Disconnecting...\n");
                    await client.DisconnectAsync(true);

                }
            }
        }
    }
}
