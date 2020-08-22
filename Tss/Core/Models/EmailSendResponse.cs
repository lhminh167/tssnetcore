using System.Net;

namespace Tss.Core.Models
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
