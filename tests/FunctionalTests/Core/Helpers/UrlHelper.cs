using System.ComponentModel.DataAnnotations;
using System.Linq;
using System;
using System.Threading.Tasks;
using Tss.FunctionalTests.Web.Fixtures;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Tss.FunctionalTests.Core.Helpers
{
    public class UrlHelper
    {
        public static string ToQueryString(object model)
        {
            var serialized = JsonConvert.SerializeObject(model);
            var deserialized = JsonConvert.DeserializeObject<Dictionary<string, string>>(serialized);
            var result = deserialized
                .Select((kvp) =>
                    kvp.Key.ToString() +
                    "=" +
                    Uri.EscapeDataString(kvp.Value?? "")
                )
                .Aggregate((p1, p2) => p1 + "&" + p2);
            return result;
        }

    }
}
