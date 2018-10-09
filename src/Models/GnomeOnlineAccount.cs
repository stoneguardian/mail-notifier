using System.Collections.Generic;

namespace MailNotifier.Models
{
    class GnomeOnlineAccount
    {
        public readonly string DbusObjectPath;
        public readonly IEnumerable<string> Interfaces;

        public GnomeOnlineAccount(string dbusObjectPath, IEnumerable<string> interfaces)
        {
            DbusObjectPath = dbusObjectPath;
            Interfaces = interfaces;
        }
    }
}