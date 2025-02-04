using FluentValidation;

using Microsoft.EntityFrameworkCore;

using Saal.Restaurant.Application.DTOs;
using Saal.Restaurant.Domain;
using Saal.Restaurant.Infrastructure;

namespace Saal.Restaurant.Application.Validators
{
    public class GenerateBillRequestValidator : AbstractValidator<GenerateBillRequest>
    {
        public GenerateBillRequestValidator(RestaurantDbContext context)
        {
            RuleFor(x => x.TableId)
                .GreaterThan(0)
                .MustAsync(async (tableId, cancellation) => await context.Tables.AnyAsync(t => t.Id == tableId, cancellation))
                .WithMessage("Table does not exist.");



            RuleFor(b => b.TableId)
                .MustAsync(async (tableId, cancellation) =>
                {
                    var existingBill = await context.Bills.AnyAsync(b => b.TableId == tableId && b.Status == BillStatus.Open, cancellation);
                    return !existingBill;
                })
                .WithMessage("A table can only have one open bill at a time.");
        }
    }
}
