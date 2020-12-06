using System;
using System.Net;
using Ardalis.GuardClauses;
using Tss.Core.Constants;

namespace Tss.Core.Models
{
    public class Response<T>
    {
        private Response()
        {

        }

        public Response(
            HttpStatusCode status,
            string title,
            string detail
        )
        {
            Guard.Against.NullOrWhiteSpace(title, nameof(title));
            Guard.Against.NullOrWhiteSpace(detail, nameof(detail));

            Status = status;
            Detail = detail;
            Instance = Guid.NewGuid().ToString();
            Title = title;
            Type = URL.GetErrorType(status);
        }

        public Response(
            HttpStatusCode status,
            string title,
            T content
        )
        {
            Guard.Against.NullOrWhiteSpace(title, nameof(title));
            Guard.Against.Null(content, nameof(content));

            Content = content;
            Status = status;
            Title = title;
            Type = URL.GetErrorType(status);
        }

        public T Content { get; set; }
        public string Detail { get; set; }
        public string Instance { get; set; }
        public HttpStatusCode Status { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
    }

    public class Response : Response<object>
    {
        public Response(
            HttpStatusCode status,
            string title,
            string detail
        ) : base(status, title, detail)
        {
        }

        public Response(
            HttpStatusCode status,
            string title,
            object content
        ) : base(status, title, content)
        {
        }
    }

}