using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Tss.FunctionalTests.Core.Models
{
    public class ReviewCreateRequest
    {
        public int ProductId { get; set; }
        public string Review1 { get; set; }
        public short Rating { get; set; }
    }

    public class ReviewCreateRequestValidator : AbstractValidator<ReviewCreateRequest>
    {
        public ReviewCreateRequestValidator()
        {
            RuleFor(model => model.ProductId).GreaterThan(0);
            RuleFor(model => model.Review1).NotEmpty().MinimumLength(50);
            RuleFor(model => (int)model.Rating).LessThanOrEqualTo(1).GreaterThanOrEqualTo(5);
        }
    }
}
