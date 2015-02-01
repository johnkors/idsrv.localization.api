using System.Collections;
using System.Collections.Generic;
using System.Web.Http;

namespace Api
{
    public class LocaleController : ApiController
    {
        public LocaleController()
        {

        }


        public IEnumerable<TranslationsComposite> Get()
        {
            return TranslatorHelper.GetAllTranslations();
        }

        public TranslationsComposite Get(string id)
        {
            return TranslatorHelper.GetAllTranslationsForLocale(id);
        }
    }
}
