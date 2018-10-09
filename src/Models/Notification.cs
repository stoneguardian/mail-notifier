namespace MailNotifier.Models
{
    class Notification
    {
        public readonly string Header;
        public readonly string Body;

        public Notification(string header, string body)
        {
            Header = header;
            Body = body;
        }
    }
}