using System;
using System.Collections.Generic;
using System.Net;
using Ardalis.GuardClauses;
using FluentValidation;
using Tss.Core.Constants;

namespace Tss.FunctionalTests.Core.Models
{
    public class Response<T>
    {
        public Response()
        {
        }

        public T Content { get; set; }
        public string Detail { get; set; }
        public string Instance { get; set; }
        public HttpStatusCode Status { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public Dictionary<string, string[]> Errors { get; set; }
    }

    public class Response : Response<object>
    {
        public Response()
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