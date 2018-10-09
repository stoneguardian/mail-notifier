using System;
using System.Threading.Tasks;
using Tmds.DBus;
using OnlineAccounts.DBus;
using MailKit.Net.Imap;
using MailNotifier.AccountRepositories;
using MailKit;
using MailKit.Security;
using MailKit.Search;
using MailNotifier.Models;
using System.IO;
using System.Linq;

namespace MailNotifier
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // App Start
            var cache = new Cache();
            var gnomeRepo = new GnomeOnlineAccounts();
            var accounts = await gnomeRepo.GetAllMailAddresses();

            foreach (var account in accounts)
            {
                var messagesAlreadyNotified = cache.GetNotifiedMessages(account);

                Console.WriteLine($"Connecting to: {account}");
                using (var client = await gnomeRepo.GetImapClient(account))
                {
                    var inbox = client.Inbox;
                    await inbox.OpenAsync(FolderAccess.ReadOnly);

                    // Find all unread messages in inbox
                    var unread = await inbox.SearchAsync(SearchQuery.NotSeen);

                    // Filter out messages we have already sent notification for
                    var numMessagesToNotifyOf = unread.Where(m => !messagesAlreadyNotified.Contains(m.Id)).Count();

                    if (numMessagesToNotifyOf > 0)
                    {
                        var message = "";
                        if (numMessagesToNotifyOf == 1)
                        {
                            message = "There is 1 unread message";
                        }
                        else
                        {
                            message = $"Tere are {numMessagesToNotifyOf} unread messages";
                        }

                        Console.WriteLine("Sending notification");
                        NotificationHandler.Send(new Notification(account, message));

                        // Update cache with all unread messages
                        cache.CacheNotifiedMessages(account, unread.Select(m => m.Id));
                    }

                    Console.WriteLine("Disconnecting...\n");
                    await client.DisconnectAsync(true);
                }
            }
        }
    }
}
