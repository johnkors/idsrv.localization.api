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

            var allTranslationsForLocale = (from eventId in _possibleEventIds let value = localeService.GetString(Eventscategory, eventId) select new IdTranslation { Id = eventId, Value = value }).ToList();
            allTranslationsForLocale.AddRange(from messageId in _possibleMessageIds let value = localeService.GetString(Messagescategory, messageId) select new IdTranslation { Id = messageId, Value = value });
            allTranslationsForLocale.AddRange(from scopeId in _possibleScopeIds let value = localeService.GetString(ScopesCategory, scopeId) select new IdTranslation { Id = scopeId, Value = value });
            return new TranslationsComposite{ Locale = locale, Translations = allTranslationsForLocale};
        }

        private static readonly IEnumerable<string> _possibleMessageIds = new List<string>
        {
            MessageIds.ClientIdRequired,
            MessageIds.ExternalProviderError,
            MessageIds.InvalidUsernameOrPassword,
            MessageIds.Invalid_scope,
            MessageIds.MissingClientId,
            MessageIds.MissingToken,
            MessageIds.MustSelectAtLeastOnePermission,
            MessageIds.NoExternalProvider,
            MessageIds.NoMatchingExternalAccount,
            MessageIds.NoSignInCookie,
            MessageIds.NoSubjectFromExternalProvider,
            MessageIds.PasswordRequired,
            MessageIds.SslRequired,
            MessageIds.Unauthorized_client,
            MessageIds.UnexpectedError,
            MessageIds.UnsupportedMediaType,
            MessageIds.Unsupported_response_type,
            MessageIds.UsernameRequired
        };

        private static readonly IEnumerable<string> _possibleEventIds = new List<string>
        {
            EventIds.ExternalLoginFailure,
            EventIds.ExternalLoginSuccess,
            EventIds.LocalLoginFailure,
            EventIds.LocalLoginSuccess,
            EventIds.LogoutEvent,
            EventIds.PartialLogin,
            EventIds.PartialLoginComplete,
            EventIds.PreLoginFailure,
            EventIds.PreLoginSuccess
        };


        private static readonly IEnumerable<string> _possibleScopeIds = new List<string>
        {
            ScopeIds.Address_DisplayName,
            ScopeIds.All_claims_DisplayName,
            ScopeIds.Email_DisplayName,
            ScopeIds.Offline_access_DisplayName,
            ScopeIds.Openid_DisplayName,
            ScopeIds.Phone_DisplayName,
            ScopeIds.Profile_Description,
            ScopeIds.Profile_DisplayName,
            ScopeIds.Roles_DisplayName
        };

        public static IEnumerable<TranslationsComposite> GetAllTranslations()
        {
            var allTranslations = LocalizationServiceFactory.AvailableLocalizationServices.Keys;
            return allTranslations.Select(GetAllTranslationsForLocale);
        }
    }
}