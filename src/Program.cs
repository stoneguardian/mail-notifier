using System;
using System.Threading.Tasks;
using Tmds.DBus;
using OnlineAccounts.DBus;
using MailKit.Net.Imap;
using MailNotifier.AccountRepositories;

namespace MailNotifier
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Getting gnome accounts...");
            var gnomeRepo = new GnomeOnlineAccounts();
            var accounts = await gnomeRepo.GetImapAccounts();

            foreach (var account in accounts)
            {
                Console.WriteLine($"{account.Host}");
                Console.WriteLine($" - Username: {account.Username}");
                Console.WriteLine($" - Port: {account.Port} ");
                Console.WriteLine($" - UseSSL: {account.UseSsl} ");
                Console.WriteLine(" ");
            }

            Console.WriteLine("\n --- \nPasswords:");
            var sessionConnection = Connection.Session;

            // Works
            var passwordManager = sessionConnection.CreateProxy<IPasswordBased>("org.gnome.OnlineAccounts", "/org/gnome/OnlineAccounts/Accounts/account_1537887570_3");

            Console.WriteLine(await passwordManager.GetPasswordAsync("imap-password"));

            using (var client = new ImapClient())
            {
                //client.
            }

        }
    }
}
