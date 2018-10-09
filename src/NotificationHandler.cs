using MailNotifier.Models;

namespace MailNotifier
{
    static class NotificationHandler
    {
        public static void Send(Notification notification)
        {
            var process = new System.Diagnostics.Process
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "/bin/sh",
                    Arguments = $"-c \"notify-send '{notification.Header}' '{notification.Body}'\"",
                    CreateNoWindow = true
                }
            };

            process.Start();
            process.WaitForExit();
        }
    }
}