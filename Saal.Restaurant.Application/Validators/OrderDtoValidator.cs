using FluentValidation;

using Microsoft.EntityFrameworkCore;

using Saal.Restaurant.Application.DTOs;
using Saal.Restaurant.Domain;
using Saal.Restaurant.Infrastructure;

namespace Saal.Restaurant.Application.Validators
{
    public class OrderDtoValidator : AbstractValidator<OrderDto>
    {
        private readonly RestaurantDbContext _context;

        public OrderDtoValidator(RestaurantDbContext context)
        {
            _context = context;

            RuleFor(o => o.TableId)
                .GreaterThan(0)
                .MustAsync(async (tableId, cancellation) => await _context.Tables.AnyAsync(t => t.Id == tableId, cancellation))
                .WithMessage("Invalid TableId. Table does not exist.")
                .MustAsync(async (tableId, cancellation) =>
                    !await _context.Bills.AllAsync(b => b.TableId == tableId && b.Status != BillStatus.Open, cancellation))
                .WithMessage("Cannot add an order to a table without a open bill. Create a new bill first.");

            RuleFor(o => o.OrderMenuItems)
                .NotEmpty()
                .WithMessage("Order must contain at least one menu item.");
        }

    }
}
