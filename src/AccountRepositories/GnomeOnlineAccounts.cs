using System.Collections.Generic;
using System.Threading.Tasks;
using MailNotifier.Models;
using OnlineAccounts.DBus;
using Tmds.DBus;
using System.Linq;

namespace MailNotifier.AccountRepositories
{
    public class GnomeOnlineAccounts
    {
        private readonly string dbusServiceName = "org.gnome.OnlineAccounts";
        private readonly string dbusRootQueue = "/org/gnome/OnlineAccounts";

        private readonly string dbusMailAccountServiceName = "org.gnome.OnlineAccounts.Mail";

        private readonly string dbusPasswordBasedAuth = "org.gnome.OnlineAccounts.PasswordBased";
        private readonly string dbusOAuthBasedAuth = "org.gnome.OnlineAccounts.OAuthBased";

        private readonly string dbusOAuth2BasedAuth = "org.gnome.OnlineAccounts.OAuth2Based";

        public GnomeOnlineAccounts() { }

        public async Task<IEnumerable<ImapAccount>> GetImapAccounts()
        {
            var dbusSessionConnection = Connection.Session;
            var manager = dbusSessionConnection.CreateProxy<IObjectManager>(dbusServiceName, dbusRootQueue);
            var managedObjects = await manager.GetManagedObjectsAsync();
            var objectPaths = managedObjects.Keys;

            var result = new List<ImapAccount>();

            foreach (var path in objectPaths)
            {
                var obj = managedObjects[path];

                // Has no mail account so skip it
                if (!obj.Keys.Contains(dbusMailAccountServiceName))
                {
                    continue;
                }

                var auth = GetPassword(obj);

                System.Console.WriteLine(path);
                var mailAccount = await dbusSessionConnection.CreateProxy<IMail>(dbusServiceName, path).GetAllAsync();
                if (mailAccount.ImapSupported)
                {
                    result.Add(ConvertToImapAccount(mailAccount));
                }
            }

            return result;
        }

        private string GetPassword(IDictionary<string, IDictionary<string, object>> obj)
        {

        }



        private ImapAccount ConvertToImapAccount(MailProperties account)
        {
            int port = 143;
            if (account.ImapUseSsl) port = 993;

            return new ImapAccount(account.ImapUserName, "", account.ImapHost, port, account.ImapUseSsl);
        }
    }
}