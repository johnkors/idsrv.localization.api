using System.Collections.Generic;

namespace Api
{
    public class TranslationsComposite
    {
        public string Locale { get; set; }
        public IEnumerable<IdTranslation> Translations { get; set; }
    }

    public class IdTranslation
    {
        public string Id { get; set; }
        public string Value { get; set; }
    }
}