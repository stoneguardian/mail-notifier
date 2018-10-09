using System.Collections.Generic;
using System.Threading.Tasks;
using MailNotifier.Models;
using OnlineAccounts.DBus;
using Tmds.DBus;
using System.Linq;
using MailKit;
using MailKit.Security;
using MailKit.Net.Imap;
using System;

namespace MailNotifier.AccountRepositories
{
    public class GnomeOnlineAccounts
    {
        private readonly string dbusServiceName = "org.gnome.OnlineAccounts";
        private readonly string dbusRootObjectPath = "/org/gnome/OnlineAccounts";

        private readonly string dbusMailAccountServiceName = "org.gnome.OnlineAccounts.Mail";

        private readonly string dbusPasswordBasedAuth = "org.gnome.OnlineAccounts.PasswordBased";
        private readonly string dbusOAuthBasedAuth = "org.gnome.OnlineAccounts.OAuthBased";

        private readonly string dbusOAuth2BasedAuth = "org.gnome.OnlineAccounts.OAuth2Based";

        public GnomeOnlineAccounts() { }

        private async Task<IEnumerable<GnomeOnlineAccount>> GetAll()
        {
            var result = new List<GnomeOnlineAccount>();
            var dbusSessionConnection = Connection.Session;
            var manager = dbusSessionConnection.CreateProxy<IObjectManager>(dbusServiceName, dbusRootObjectPath);
            var managedObjects = await manager.GetManagedObjectsAsync();

            foreach (var key in managedObjects.Keys)
            {
                result.Add(new GnomeOnlineAccount(key.ToString(), managedObjects[key].Keys));
            }

            return result;
        }

        public async Task<IEnumerable<string>> GetAllMailAddresses()
        {
            var mailAccounts = (await GetAll()).Where(a => a.Interfaces.Contains(dbusMailAccountServiceName));
            var result = new List<string>(mailAccounts.Count() + 1);
            var dbusSessionConnection = Connection.Session;

            foreach (var account in mailAccounts)
            {
                var dbusAccountManager = dbusSessionConnection.CreateProxy<IMail>(dbusServiceName, account.DbusObjectPath);
                result.Add(await dbusAccountManager.GetEmailAddressAsync());
            }

            return result;
        }

        public async Task<ImapClient> GetImapClient(string emailAddress)
        {
            IMail accountManager = null;
            GnomeOnlineAccount gnomeAccount = null;


            var mailAccounts = (await GetAll()).Where(a => a.Interfaces.Contains(dbusMailAccountServiceName));

            var dbusSessionConnection = Connection.Session;

            foreach (var account in mailAccounts)
            {
                var dbusAccountManager = dbusSessionConnection.CreateProxy<IMail>(dbusServiceName, account.DbusObjectPath);
                if (emailAddress == await dbusAccountManager.GetEmailAddressAsync())
                {
                    accountManager = dbusAccountManager;
                    gnomeAccount = account;
                }
            }

            if (null == accountManager)
            {
                throw new ArgumentException("emailAddress not found in Gnome OnlineAccounts");
            }


            var imapClient = new ImapClient();
            var port = 143;

            if (await accountManager.GetImapUseSslAsync())
            {
                port = 993;
            }

            await imapClient.ConnectAsync(
                await accountManager.GetImapHostAsync(),
                port
            );

            // Determine authentication-scheme
            var sessionConnection = Connection.Session;
            var username = await accountManager.GetImapUserNameAsync();
            SaslMechanism cred = null;

            if (gnomeAccount.Interfaces.Contains(dbusOAuth2BasedAuth) || gnomeAccount.Interfaces.Contains(dbusOAuthBasedAuth))
            {
                var oAuth2Manager = sessionConnection.CreateProxy<IOAuth2Based>(dbusServiceName, gnomeAccount.DbusObjectPath);
                (var token, var expiry) = await oAuth2Manager.GetAccessTokenAsync();

                cred = new SaslMechanismOAuth2(username, token);
            }
            else if (gnomeAccount.Interfaces.Contains(dbusPasswordBasedAuth))
            {
                var passwordManager = sessionConnection.CreateProxy<IPasswordBased>(dbusServiceName, gnomeAccount.DbusObjectPath);
                var password = await passwordManager.GetPasswordAsync("imap-password");

                cred = new SaslMechanismLogin(username, password);
            }
            else
            {
                throw new Exception("Could not extract credentials from Gnome");
            }

            await imapClient.AuthenticateAsync(cred);

            return imapClient;
        }
    }
}