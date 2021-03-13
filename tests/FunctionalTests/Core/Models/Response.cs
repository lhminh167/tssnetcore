using System;
using System.Net;
using Ardalis.GuardClauses;
using FluentValidation;
using Tss.Core.Constants;

namespace Tss.FunctionalTests.Core.Models
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

    public class ResponseValidator<T> : AbstractValidator<Response<T>>
    {
        public ResponseValidator()
        {
            When(m => IsSuccessStatusCode(m.Status), () =>
            {
                RuleFor(m => m.Detail).Empty();
                RuleFor(m => m.Title).NotEmpty();
                RuleFor(m => m.Type).NotEmpty();
                RuleFor(m => m.Status).NotEmpty().IsInEnum()
                .Must(m => IsSuccessStatusCode(m)).WithMessage("Response status code should be lesser than 300");
                RuleFor(m => m.Content).NotEmpty();
                RuleFor(m => m.Instance).Empty();
            }).Otherwise(() =>
            {
                RuleFor(m => m.Detail).NotEmpty();
                RuleFor(m => m.Title).NotEmpty();
                RuleFor(m => m.Type).NotEmpty();
                RuleFor(m => m.Status).NotEmpty().IsInEnum()
                .Must(m => !IsSuccessStatusCode(m)).WithMessage("Response status code should be equal to or greater than 300");
                RuleFor(m => m.Content).Empty();
                RuleFor(m => m.Instance).NotEmpty();
            });
        }

        public bool IsSuccessStatusCode(HttpStatusCode input)
        {
            return (int)input < 300;
        }
    }
}