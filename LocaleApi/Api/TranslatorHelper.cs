using System.Collections.Generic;
using System.Linq;
using Thinktecture.IdentityServer.Core.Resources;
using Thinktecture.IdentityServer.Core.Services.Contrib;

namespace Api
{
    public static class TranslatorHelper
    {
        public static string Eventscategory = Thinktecture.IdentityServer.Core.Constants.LocalizationCategories.Events;
        public static string Messagescategory = Thinktecture.IdentityServer.Core.Constants.LocalizationCategories.Messages;
        public static string ScopesCategory = Thinktecture.IdentityServer.Core.Constants.LocalizationCategories.Scopes;

        public static TranslationsComposite GetAllTranslationsForLocale(string locale)
        {
            var options = new LocaleOptions { Locale = locale };
            var localeService = new GlobalizedLocalizationService(options);

            var allTranslationsForLocale = (from eventId in AllEventIds let value = localeService.GetString(Eventscategory, eventId) select new IdTranslation { Id = eventId, Value = value }).ToList();
            allTranslationsForLocale.AddRange(from messageId in AllMessageIds let value = localeService.GetString(Messagescategory, messageId) select new IdTranslation { Id = messageId, Value = value });
            allTranslationsForLocale.AddRange(from scopeId in AllScopeIds let value = localeService.GetString(ScopesCategory, scopeId) select new IdTranslation { Id = scopeId, Value = value });
            return new TranslationsComposite { Locale = locale, Translations = allTranslationsForLocale };
        }

        public static IEnumerable<TranslationsComposite> GetAllTranslations()
        {
            var allTranslations = new GlobalizedLocalizationService().GetAvailableLocales();
            return allTranslations.Select(GetAllTranslationsForLocale);
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