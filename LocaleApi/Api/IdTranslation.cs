using System.Collections.Generic;
using System.Net.Http.Headers;

namespace Api
{
    public class TranslationsComposite
    {
        public string Locale { get; set; }
        public string IdSrvVersion { get; set; }
        public IEnumerable<IdTranslation> Translations { get; set; }
        public string HeaderLanguage { get; set; }
    }

    public class IdTranslation
    {
        public string Id { get; set; }
        public string Value { get; set; }
    }
}