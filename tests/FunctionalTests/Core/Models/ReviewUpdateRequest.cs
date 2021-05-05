using FluentValidation;

namespace Tss.FunctionalTests.Core.Models
{
    public class ReviewUpdateRequest
    {
        public int ReviewId { get; set; }
        public string Review1 { get; set; }
        public short Rating { get; set; }
    }

    public class ReviewUpdateRequestValidator : AbstractValidator<ReviewUpdateRequest>
    {
        public ReviewUpdateRequestValidator()
        {
            RuleFor(model => model.ReviewId).GreaterThan(0);
            RuleFor(model => model.Review1).NotEmpty().MinimumLength(50);
            RuleFor(model => (int)model.Rating).GreaterThan(0).LessThan(5);
        }
    }
}
