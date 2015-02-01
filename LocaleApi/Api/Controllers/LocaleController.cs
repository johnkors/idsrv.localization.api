using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace Api
{
    public class LocaleController : ApiController
    {
        public IEnumerable<TranslationsComposite> Get()
        {
            return TranslatorHelper.GetAllTranslations();
        }

        public object Get(string id)
        {
            try
            {
                var translations = TranslatorHelper.GetAllTranslationsForLocale(id);
                return Ok(translations);
            }
            catch (ApplicationException e)
            {   
                var msg = new HttpResponseMessage(HttpStatusCode.NotFound);
                msg.ReasonPhrase = e.Message;
                var errorObj = new ErrorResponse { Error = e.Message};
                var jsonContent = new ObjectContent(typeof(ErrorResponse), errorObj, new JsonMediaTypeFormatter());
             
                msg.Content = jsonContent;
                return msg;
            }
             
        }
    }

    public class ErrorResponse
    {
        public string Error { get; set; }
    }
}
