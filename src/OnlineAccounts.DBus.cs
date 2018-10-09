using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Tmds.DBus;

[assembly: InternalsVisibleTo(Tmds.DBus.Connection.DynamicAssemblyName)]
namespace OnlineAccounts.DBus
{
    [DBusInterface("org.freedesktop.DBus.ObjectManager")]
    interface IObjectManager : IDBusObject
    {
        Task<IDictionary<ObjectPath, IDictionary<string, IDictionary<string, object>>>> GetManagedObjectsAsync();
        Task<IDisposable> WatchInterfacesAddedAsync(Action<(ObjectPath objectPath, IDictionary<string, IDictionary<string, object>> interfacesAndProperties)> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchInterfacesRemovedAsync(Action<(ObjectPath objectPath, string[] interfaces)> handler, Action<Exception> onError = null);
    }

    [DBusInterface("org.gnome.OnlineAccounts.Manager")]
    interface IManager : IDBusObject
    {
        Task<ObjectPath> AddAccountAsync(string Provider, string Identity, string PresentationIdentity, IDictionary<string, object> Credentials, IDictionary<string, string> Details);
    }

    [DBusInterface("org.gnome.OnlineAccounts.PasswordBased")]
    interface IPasswordBased : IDBusObject
    {
        Task<string> GetPasswordAsync(string Id);
    }

    [DBusInterface("org.gnome.OnlineAccounts.Mail")]
    interface IMail : IDBusObject
    {
        Task<T> GetAsync<T>(string prop);
        Task<MailProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class MailProperties
    {
        private string _EmailAddress = default (string);
        public string EmailAddress
        {
            get
            {
                return _EmailAddress;
            }

            set
            {
                _EmailAddress = (value);
            }
        }

        private string _Name = default (string);
        public string Name
        {
            get
            {
                return _Name;
            }

            set
            {
                _Name = (value);
            }
        }

        private bool _ImapSupported = default (bool);
        public bool ImapSupported
        {
            get
            {
                return _ImapSupported;
            }

            set
            {
                _ImapSupported = (value);
            }
        }

        private bool _ImapAcceptSslErrors = default (bool);
        public bool ImapAcceptSslErrors
        {
            get
            {
                return _ImapAcceptSslErrors;
            }

            set
            {
                _ImapAcceptSslErrors = (value);
            }
        }

        private string _ImapHost = default (string);
        public string ImapHost
        {
            get
            {
                return _ImapHost;
            }

            set
            {
                _ImapHost = (value);
            }
        }

        private bool _ImapUseSsl = default (bool);
        public bool ImapUseSsl
        {
            get
            {
                return _ImapUseSsl;
            }

            set
            {
                _ImapUseSsl = (value);
            }
        }

        private bool _ImapUseTls = default (bool);
        public bool ImapUseTls
        {
            get
            {
                return _ImapUseTls;
            }

            set
            {
                _ImapUseTls = (value);
            }
        }

        private string _ImapUserName = default (string);
        public string ImapUserName
        {
            get
            {
                return _ImapUserName;
            }

            set
            {
                _ImapUserName = (value);
            }
        }

        private bool _SmtpSupported = default (bool);
        public bool SmtpSupported
        {
            get
            {
                return _SmtpSupported;
            }

            set
            {
                _SmtpSupported = (value);
            }
        }

        private bool _SmtpAcceptSslErrors = default (bool);
        public bool SmtpAcceptSslErrors
        {
            get
            {
                return _SmtpAcceptSslErrors;
            }

            set
            {
                _SmtpAcceptSslErrors = (value);
            }
        }

        private string _SmtpHost = default (string);
        public string SmtpHost
        {
            get
            {
                return _SmtpHost;
            }

            set
            {
                _SmtpHost = (value);
            }
        }

        private bool _SmtpUseAuth = default (bool);
        public bool SmtpUseAuth
        {
            get
            {
                return _SmtpUseAuth;
            }

            set
            {
                _SmtpUseAuth = (value);
            }
        }

        private bool _SmtpAuthLogin = default (bool);
        public bool SmtpAuthLogin
        {
            get
            {
                return _SmtpAuthLogin;
            }

            set
            {
                _SmtpAuthLogin = (value);
            }
        }

        private bool _SmtpAuthPlain = default (bool);
        public bool SmtpAuthPlain
        {
            get
            {
                return _SmtpAuthPlain;
            }

            set
            {
                _SmtpAuthPlain = (value);
            }
        }

        private bool _SmtpAuthXoauth2 = default (bool);
        public bool SmtpAuthXoauth2
        {
            get
            {
                return _SmtpAuthXoauth2;
            }

            set
            {
                _SmtpAuthXoauth2 = (value);
            }
        }

        private bool _SmtpUseSsl = default (bool);
        public bool SmtpUseSsl
        {
            get
            {
                return _SmtpUseSsl;
            }

            set
            {
                _SmtpUseSsl = (value);
            }
        }

        private bool _SmtpUseTls = default (bool);
        public bool SmtpUseTls
        {
            get
            {
                return _SmtpUseTls;
            }

            set
            {
                _SmtpUseTls = (value);
            }
        }

        private string _SmtpUserName = default (string);
        public string SmtpUserName
        {
            get
            {
                return _SmtpUserName;
            }

            set
            {
                _SmtpUserName = (value);
            }
        }
    }

    static class MailExtensions
    {
        public static Task<string> GetEmailAddressAsync(this IMail o) => o.GetAsync<string>("EmailAddress");
        public static Task<string> GetNameAsync(this IMail o) => o.GetAsync<string>("Name");
        public static Task<bool> GetImapSupportedAsync(this IMail o) => o.GetAsync<bool>("ImapSupported");
        public static Task<bool> GetImapAcceptSslErrorsAsync(this IMail o) => o.GetAsync<bool>("ImapAcceptSslErrors");
        public static Task<string> GetImapHostAsync(this IMail o) => o.GetAsync<string>("ImapHost");
        public static Task<bool> GetImapUseSslAsync(this IMail o) => o.GetAsync<bool>("ImapUseSsl");
        public static Task<bool> GetImapUseTlsAsync(this IMail o) => o.GetAsync<bool>("ImapUseTls");
        public static Task<string> GetImapUserNameAsync(this IMail o) => o.GetAsync<string>("ImapUserName");
        public static Task<bool> GetSmtpSupportedAsync(this IMail o) => o.GetAsync<bool>("SmtpSupported");
        public static Task<bool> GetSmtpAcceptSslErrorsAsync(this IMail o) => o.GetAsync<bool>("SmtpAcceptSslErrors");
        public static Task<string> GetSmtpHostAsync(this IMail o) => o.GetAsync<string>("SmtpHost");
        public static Task<bool> GetSmtpUseAuthAsync(this IMail o) => o.GetAsync<bool>("SmtpUseAuth");
        public static Task<bool> GetSmtpAuthLoginAsync(this IMail o) => o.GetAsync<bool>("SmtpAuthLogin");
        public static Task<bool> GetSmtpAuthPlainAsync(this IMail o) => o.GetAsync<bool>("SmtpAuthPlain");
        public static Task<bool> GetSmtpAuthXoauth2Async(this IMail o) => o.GetAsync<bool>("SmtpAuthXoauth2");
        public static Task<bool> GetSmtpUseSslAsync(this IMail o) => o.GetAsync<bool>("SmtpUseSsl");
        public static Task<bool> GetSmtpUseTlsAsync(this IMail o) => o.GetAsync<bool>("SmtpUseTls");
        public static Task<string> GetSmtpUserNameAsync(this IMail o) => o.GetAsync<string>("SmtpUserName");
    }

    [DBusInterface("org.gnome.OnlineAccounts.Account")]
    interface IAccount : IDBusObject
    {
        Task RemoveAsync();
        Task<int> EnsureCredentialsAsync();
        Task<T> GetAsync<T>(string prop);
        Task<AccountProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class AccountProperties
    {
        private string _ProviderType = default (string);
        public string ProviderType
        {
            get
            {
                return _ProviderType;
            }

            set
            {
                _ProviderType = (value);
            }
        }

        private string _ProviderName = default (string);
        public string ProviderName
        {
            get
            {
                return _ProviderName;
            }

            set
            {
                _ProviderName = (value);
            }
        }

        private string _ProviderIcon = default (string);
        public string ProviderIcon
        {
            get
            {
                return _ProviderIcon;
            }

            set
            {
                _ProviderIcon = (value);
            }
        }

        private string _Id = default (string);
        public string Id
        {
            get
            {
                return _Id;
            }

            set
            {
                _Id = (value);
            }
        }

        private bool _IsLocked = default (bool);
        public bool IsLocked
        {
            get
            {
                return _IsLocked;
            }

            set
            {
                _IsLocked = (value);
            }
        }

        private bool _IsTemporary = default (bool);
        public bool IsTemporary
        {
            get
            {
                return _IsTemporary;
            }

            set
            {
                _IsTemporary = (value);
            }
        }

        private bool _AttentionNeeded = default (bool);
        public bool AttentionNeeded
        {
            get
            {
                return _AttentionNeeded;
            }

            set
            {
                _AttentionNeeded = (value);
            }
        }

        private string _Identity = default (string);
        public string Identity
        {
            get
            {
                return _Identity;
            }

            set
            {
                _Identity = (value);
            }
        }

        private string _PresentationIdentity = default (string);
        public string PresentationIdentity
        {
            get
            {
                return _PresentationIdentity;
            }

            set
            {
                _PresentationIdentity = (value);
            }
        }

        private bool _MailDisabled = default (bool);
        public bool MailDisabled
        {
            get
            {
                return _MailDisabled;
            }

            set
            {
                _MailDisabled = (value);
            }
        }

        private bool _CalendarDisabled = default (bool);
        public bool CalendarDisabled
        {
            get
            {
                return _CalendarDisabled;
            }

            set
            {
                _CalendarDisabled = (value);
            }
        }

        private bool _ContactsDisabled = default (bool);
        public bool ContactsDisabled
        {
            get
            {
                return _ContactsDisabled;
            }

            set
            {
                _ContactsDisabled = (value);
            }
        }

        private bool _ChatDisabled = default (bool);
        public bool ChatDisabled
        {
            get
            {
                return _ChatDisabled;
            }

            set
            {
                _ChatDisabled = (value);
            }
        }

        private bool _DocumentsDisabled = default (bool);
        public bool DocumentsDisabled
        {
            get
            {
                return _DocumentsDisabled;
            }

            set
            {
                _DocumentsDisabled = (value);
            }
        }

        private bool _MapsDisabled = default (bool);
        public bool MapsDisabled
        {
            get
            {
                return _MapsDisabled;
            }

            set
            {
                _MapsDisabled = (value);
            }
        }

        private bool _MusicDisabled = default (bool);
        public bool MusicDisabled
        {
            get
            {
                return _MusicDisabled;
            }

            set
            {
                _MusicDisabled = (value);
            }
        }

        private bool _PrintersDisabled = default (bool);
        public bool PrintersDisabled
        {
            get
            {
                return _PrintersDisabled;
            }

            set
            {
                _PrintersDisabled = (value);
            }
        }

        private bool _PhotosDisabled = default (bool);
        public bool PhotosDisabled
        {
            get
            {
                return _PhotosDisabled;
            }

            set
            {
                _PhotosDisabled = (value);
            }
        }

        private bool _FilesDisabled = default (bool);
        public bool FilesDisabled
        {
            get
            {
                return _FilesDisabled;
            }

            set
            {
                _FilesDisabled = (value);
            }
        }

        private bool _TicketingDisabled = default (bool);
        public bool TicketingDisabled
        {
            get
            {
                return _TicketingDisabled;
            }

            set
            {
                _TicketingDisabled = (value);
            }
        }

        private bool _TodoDisabled = default (bool);
        public bool TodoDisabled
        {
            get
            {
                return _TodoDisabled;
            }

            set
            {
                _TodoDisabled = (value);
            }
        }

        private bool _ReadLaterDisabled = default (bool);
        public bool ReadLaterDisabled
        {
            get
            {
                return _ReadLaterDisabled;
            }

            set
            {
                _ReadLaterDisabled = (value);
            }
        }
    }

    static class AccountExtensions
    {
        public static Task<string> GetProviderTypeAsync(this IAccount o) => o.GetAsync<string>("ProviderType");
        public static Task<string> GetProviderNameAsync(this IAccount o) => o.GetAsync<string>("ProviderName");
        public static Task<string> GetProviderIconAsync(this IAccount o) => o.GetAsync<string>("ProviderIcon");
        public static Task<string> GetIdAsync(this IAccount o) => o.GetAsync<string>("Id");
        public static Task<bool> GetIsLockedAsync(this IAccount o) => o.GetAsync<bool>("IsLocked");
        public static Task<bool> GetIsTemporaryAsync(this IAccount o) => o.GetAsync<bool>("IsTemporary");
        public static Task<bool> GetAttentionNeededAsync(this IAccount o) => o.GetAsync<bool>("AttentionNeeded");
        public static Task<string> GetIdentityAsync(this IAccount o) => o.GetAsync<string>("Identity");
        public static Task<string> GetPresentationIdentityAsync(this IAccount o) => o.GetAsync<string>("PresentationIdentity");
        public static Task<bool> GetMailDisabledAsync(this IAccount o) => o.GetAsync<bool>("MailDisabled");
        public static Task<bool> GetCalendarDisabledAsync(this IAccount o) => o.GetAsync<bool>("CalendarDisabled");
        public static Task<bool> GetContactsDisabledAsync(this IAccount o) => o.GetAsync<bool>("ContactsDisabled");
        public static Task<bool> GetChatDisabledAsync(this IAccount o) => o.GetAsync<bool>("ChatDisabled");
        public static Task<bool> GetDocumentsDisabledAsync(this IAccount o) => o.GetAsync<bool>("DocumentsDisabled");
        public static Task<bool> GetMapsDisabledAsync(this IAccount o) => o.GetAsync<bool>("MapsDisabled");
        public static Task<bool> GetMusicDisabledAsync(this IAccount o) => o.GetAsync<bool>("MusicDisabled");
        public static Task<bool> GetPrintersDisabledAsync(this IAccount o) => o.GetAsync<bool>("PrintersDisabled");
        public static Task<bool> GetPhotosDisabledAsync(this IAccount o) => o.GetAsync<bool>("PhotosDisabled");
        public static Task<bool> GetFilesDisabledAsync(this IAccount o) => o.GetAsync<bool>("FilesDisabled");
        public static Task<bool> GetTicketingDisabledAsync(this IAccount o) => o.GetAsync<bool>("TicketingDisabled");
        public static Task<bool> GetTodoDisabledAsync(this IAccount o) => o.GetAsync<bool>("TodoDisabled");
        public static Task<bool> GetReadLaterDisabledAsync(this IAccount o) => o.GetAsync<bool>("ReadLaterDisabled");
        public static Task SetIsTemporaryAsync(this IAccount o, bool val) => o.SetAsync("IsTemporary", val);
        public static Task SetMailDisabledAsync(this IAccount o, bool val) => o.SetAsync("MailDisabled", val);
        public static Task SetCalendarDisabledAsync(this IAccount o, bool val) => o.SetAsync("CalendarDisabled", val);
        public static Task SetContactsDisabledAsync(this IAccount o, bool val) => o.SetAsync("ContactsDisabled", val);
        public static Task SetChatDisabledAsync(this IAccount o, bool val) => o.SetAsync("ChatDisabled", val);
        public static Task SetDocumentsDisabledAsync(this IAccount o, bool val) => o.SetAsync("DocumentsDisabled", val);
        public static Task SetMapsDisabledAsync(this IAccount o, bool val) => o.SetAsync("MapsDisabled", val);
        public static Task SetMusicDisabledAsync(this IAccount o, bool val) => o.SetAsync("MusicDisabled", val);
        public static Task SetPrintersDisabledAsync(this IAccount o, bool val) => o.SetAsync("PrintersDisabled", val);
        public static Task SetPhotosDisabledAsync(this IAccount o, bool val) => o.SetAsync("PhotosDisabled", val);
        public static Task SetFilesDisabledAsync(this IAccount o, bool val) => o.SetAsync("FilesDisabled", val);
        public static Task SetTicketingDisabledAsync(this IAccount o, bool val) => o.SetAsync("TicketingDisabled", val);
        public static Task SetTodoDisabledAsync(this IAccount o, bool val) => o.SetAsync("TodoDisabled", val);
        public static Task SetReadLaterDisabledAsync(this IAccount o, bool val) => o.SetAsync("ReadLaterDisabled", val);
    }

    [DBusInterface("org.gnome.OnlineAccounts.Documents")]
    interface IDocuments : IDBusObject
    {
    }

    [DBusInterface("org.gnome.OnlineAccounts.OAuth2Based")]
    interface IOAuth2Based : IDBusObject
    {
        Task<(string accessToken, int expiresIn)> GetAccessTokenAsync();
        Task<T> GetAsync<T>(string prop);
        Task<OAuth2BasedProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class OAuth2BasedProperties
    {
        private string _ClientId = default (string);
        public string ClientId
        {
            get
            {
                return _ClientId;
            }

            set
            {
                _ClientId = (value);
            }
        }

        private string _ClientSecret = default (string);
        public string ClientSecret
        {
            get
            {
                return _ClientSecret;
            }

            set
            {
                _ClientSecret = (value);
            }
        }
    }

    static class OAuth2BasedExtensions
    {
        public static Task<string> GetClientIdAsync(this IOAuth2Based o) => o.GetAsync<string>("ClientId");
        public static Task<string> GetClientSecretAsync(this IOAuth2Based o) => o.GetAsync<string>("ClientSecret");
    }

    [DBusInterface("org.gnome.OnlineAccounts.Printers")]
    interface IPrinters : IDBusObject
    {
    }

    [DBusInterface("org.gnome.OnlineAccounts.Files")]
    interface IFiles : IDBusObject
    {
        Task<T> GetAsync<T>(string prop);
        Task<FilesProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class FilesProperties
    {
        private bool _AcceptSslErrors = default (bool);
        public bool AcceptSslErrors
        {
            get
            {
                return _AcceptSslErrors;
            }

            set
            {
                _AcceptSslErrors = (value);
            }
        }

        private string _Uri = default (string);
        public string Uri
        {
            get
            {
                return _Uri;
            }

            set
            {
                _Uri = (value);
            }
        }
    }

    static class FilesExtensions
    {
        public static Task<bool> GetAcceptSslErrorsAsync(this IFiles o) => o.GetAsync<bool>("AcceptSslErrors");
        public static Task<string> GetUriAsync(this IFiles o) => o.GetAsync<string>("Uri");
    }

    [DBusInterface("org.gnome.OnlineAccounts.Contacts")]
    interface IContacts : IDBusObject
    {
        Task<T> GetAsync<T>(string prop);
        Task<ContactsProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class ContactsProperties
    {
        private bool _AcceptSslErrors = default (bool);
        public bool AcceptSslErrors
        {
            get
            {
                return _AcceptSslErrors;
            }

            set
            {
                _AcceptSslErrors = (value);
            }
        }

        private string _Uri = default (string);
        public string Uri
        {
            get
            {
                return _Uri;
            }

            set
            {
                _Uri = (value);
            }
        }
    }

    static class ContactsExtensions
    {
        public static Task<bool> GetAcceptSslErrorsAsync(this IContacts o) => o.GetAsync<bool>("AcceptSslErrors");
        public static Task<string> GetUriAsync(this IContacts o) => o.GetAsync<string>("Uri");
    }

    [DBusInterface("org.gnome.OnlineAccounts.Calendar")]
    interface ICalendar : IDBusObject
    {
        Task<T> GetAsync<T>(string prop);
        Task<CalendarProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class CalendarProperties
    {
        private bool _AcceptSslErrors = default (bool);
        public bool AcceptSslErrors
        {
            get
            {
                return _AcceptSslErrors;
            }

            set
            {
                _AcceptSslErrors = (value);
            }
        }

        private string _Uri = default (string);
        public string Uri
        {
            get
            {
                return _Uri;
            }

            set
            {
                _Uri = (value);
            }
        }
    }

    static class CalendarExtensions
    {
        public static Task<bool> GetAcceptSslErrorsAsync(this ICalendar o) => o.GetAsync<bool>("AcceptSslErrors");
        public static Task<string> GetUriAsync(this ICalendar o) => o.GetAsync<string>("Uri");
    }

    [DBusInterface("org.gnome.OnlineAccounts.Photos")]
    interface IPhotos : IDBusObject
    {
    }
}