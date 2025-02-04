using FluentValidation;

using Saal.Restaurant.Application.DTOs;

namespace Saal.Restaurant.Application.Validators
{
    public class UpdateBillStatusRequestValidator : AbstractValidator<UpdateBillStatusRequest>
    {
        public UpdateBillStatusRequestValidator()
        {
            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("Invalid status value.");
        }
    }
}
