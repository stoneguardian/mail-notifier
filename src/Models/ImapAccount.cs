namespace MailNotifier.Models
{
    public class ImapAccount
    {
        public readonly string Username;
        public readonly string Password;
        public readonly string Host;
        public readonly int Port;
        public readonly bool UseSsl;

        public ImapAccount(string username, string password, string host, int port, bool useSsl)
        {
            Username = username;
            Password = password;
            Host = host;
            Port = port;
            UseSsl = useSsl;
        }
    }
}