using System;
using Xunit;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Tss;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Tss.FuntionalTests.Web
{
    public class WebTestFixture : WebApplicationFactory<Startup>
    {
    }
}
