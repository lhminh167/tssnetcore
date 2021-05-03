using System.ComponentModel.DataAnnotations;
using System.Linq;
using System;
using System.Threading.Tasks;
using Tss.FunctionalTests.Web.Fixtures;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.WebUtilities;
using System.Web;
using System.Reflection;
using System.Collections;

namespace Tss.FunctionalTests.Core.Helpers
{
    public class UrlHelper
    {
        public static string ToQueryString(object input)
        {
            var queryString = HttpUtility.ParseQueryString("");
            foreach (PropertyInfo prop in input.GetType().GetProperties())
            {
                var value = prop.GetValue(input);
                var isList = value is IList && value.GetType().IsGenericType;
                if (isList)
                {
                    (value as List<string>).ForEach(entry =>
                    {
                        queryString.Add(prop.Name, entry);
                    });
                }
                else
                {
                    queryString.Add(prop.Name, value?.ToString() ?? "");
                }
            }
            return queryString.ToString();
        }
    }
}
