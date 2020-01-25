using System;
using System.Net;
using Tss.Core.Constants;

namespace Tss.Core.Models
{
    public class Response : PaginationInfo
    {
        // error
        public Response(
            HttpStatusCode status,
            string title,
            string detail
        )
        {
            Status = status;
            Detail = detail;
            Instance = Guid.NewGuid().ToString();
            Title = title + $" Error Instance ID: {Instance}";
            Type = URL.GetErrorType(status);
        }

        // non-pagination
        public Response(
            HttpStatusCode status,
            string title,
            object content
        )
        {
            Content = content;
            Status = status;
            Title = title;
            Type = URL.GetErrorType(status);
        }

        // pagination
        public Response(
            HttpStatusCode status,
            string title,
            PaginatedList<object> paginatedList
        )
        : base(
            paginatedList.Count, 
            paginatedList.PageIndex, 
            paginatedList.PageSize,
            true
        )
        {
            Content = paginatedList.Items;
            Status = status;
            Title = title;
            Type = URL.GetErrorType(status);
        }

        public object Content { get; private set; }
        public string Detail { get; private set; }
        public string Instance { get; private set; }
        public HttpStatusCode Status { get; private set; }
        public string Title { get; private set; }
        public string Type { get; private set; }
    }
}