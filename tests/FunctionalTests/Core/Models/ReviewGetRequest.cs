using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using FluentValidation;

namespace Tss.FunctionalTests.Core.Models
{
    public class ReviewGetRequest : PaginationRequest
    {
        public ReviewGetRequest()
        : base(0, 8, true, null, true)
        {
        }

        public int ProductId { get; set; }
    }

    public class ReviewGetRequestValidator : AbstractValidator<ReviewGetRequest>
    {
        public ReviewGetRequestValidator()
        {
            RuleFor(model => model.ProductId).GreaterThan(0);
        }
    }
}