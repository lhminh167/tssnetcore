using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Tss.FuntionalTests.Web.Extensions
{
    public static class ToJsonHttpContentExtension
    {
        public static HttpContent ToJsonHttpContent(this object input)
        {
            return new StringContent(
                JsonConvert.SerializeObject(input),
                Encoding.UTF8,
                "application/json"
            );
        }

    }

}