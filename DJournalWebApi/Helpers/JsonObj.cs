using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace DJournalWebApi.Helpers
{
    public class JsonObj
    {
        public static string FormJson(string _code, object _data, string _message)
        {
            var js = new {
                code = _code,
                data = _data,
                message = _message
            };
            var enjs = JsonConvert.SerializeObject(js, new JsonSerializerSettings { Formatting = Formatting.Indented });
            return enjs;
        }
    }
}
