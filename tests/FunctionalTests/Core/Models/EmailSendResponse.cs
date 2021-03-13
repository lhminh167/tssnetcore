using System.Net;

namespace Tss.FunctionalTests.Core.Models
{
    public class EmailSendResponse : Response
    {
        public EmailSendResponse(
            HttpStatusCode status,
            string title,
            string detail
        )
        : base(
            status,
            title,
            detail
        )
        {
        }
    }
}
