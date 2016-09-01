using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Resources;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.Contrib;


namespace Api
{
    public class VersionHelper
    {
        public static string GetIdsrvVersion()
        {
            var ass = Assembly.GetAssembly(typeof (IdentityServerOptions));
            var attrs = ass.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false);
            var version = ((AssemblyFileVersionAttribute)attrs[0]).Version.TrimEnd('0').TrimEnd('.');
            return version;
        }
    }

    public static class TranslatorHelper
    {
        public static string Eventscategory = IdentityServer3.Core.Constants.LocalizationCategories.Events;
        public static string Messagescategory = IdentityServer3.Core.Constants.LocalizationCategories.Messages;
        public static string ScopesCategory = IdentityServer3.Core.Constants.LocalizationCategories.Scopes;

        public static TranslationsComposite GetAllTranslationsForLocale(HttpRequestMessage Request , string id)
        {
            IDictionary<string, object> env = Request.GetOwinEnvironment();
            var options = new LocaleOptions
            {
                LocaleProvider =  e => { return id; }
            };
            var localeService = new GlobalizedLocalizationService(new OwinEnvironmentService(env), options);

            var allTranslationsForLocale = (from eventId in AllEventIds let value = localeService.GetString(Eventscategory, eventId) select new IdTranslation { Id = eventId, Value = value }).ToList();
            allTranslationsForLocale.AddRange(from messageId in AllMessageIds let value = localeService.GetString(Messagescategory, messageId) select new IdTranslation { Id = messageId, Value = value });
            allTranslationsForLocale.AddRange(from scopeId in AllScopeIds let value = localeService.GetString(ScopesCategory, scopeId) select new IdTranslation { Id = scopeId, Value = value });

            
            var headerValue = Request.Headers.AcceptLanguage.OrderByDescending(s => s.Quality.GetValueOrDefault(1));
            var localeFromHeader = headerValue.FirstOrDefault();

            return new TranslationsComposite
            {
                Locale = id,
                HeaderLanguage = localeFromHeader.Value,
                Translations = allTranslationsForLocale ,
                IdSrvVersion = VersionHelper.GetIdsrvVersion()
            };
        }

        public static IEnumerable<string> AllMessageIds
        {
            get { return typeof(MessageIds).GetFields().Select(m => m.GetRawConstantValue().ToString()); }
        }

        public static IEnumerable<string> AllEventIds
        {
            get { return typeof(EventIds).GetFields().Select(m => m.GetRawConstantValue().ToString()); }
        }

        public static IEnumerable<string> AllScopeIds
        {
            get { return typeof(ScopeIds).GetFields().Select(m => m.GetRawConstantValue().ToString()); }
        }
    }
}