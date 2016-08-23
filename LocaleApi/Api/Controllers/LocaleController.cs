using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace Api
{
    public class LocaleController : ApiController
    {
        [Route("locale/{id}")]
        public object Get(string id)
        {
            try
            {
                var translations = TranslatorHelper.GetAllTranslationsForLocale(Request, id);
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

        [Route("language")]
        public object GetBrowserLanguage()
        {
            var headerValue = Request.Headers.AcceptLanguage.OrderByDescending(s => s.Quality.GetValueOrDefault(1));
            var localeFromHeader = headerValue.FirstOrDefault();
            return localeFromHeader.Value;
        }
    }

    public class ErrorResponse
    {
        public string Error { get; set; }
    }
}
