using FluentValidation;

using Microsoft.AspNetCore.Mvc;

using Saal.Restaurant.Application.DTOs;
using Saal.Restaurant.Application.Interfaces;
using Saal.Restaurant.Domain;

using Swashbuckle.AspNetCore.Annotations;

namespace Saal.Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IOrderService orderService, IBillService billService, IValidator<OrderDto> validator) : ControllerBase
    {
        private readonly IOrderService _orderService = orderService;
        private readonly IBillService _billService = billService;
        private readonly IValidator<OrderDto> _validator = validator;

        [HttpGet]
        [SwaggerOperation(Summary = "Retrieves all orders.")]
        public async Task<IActionResult> GetOrders() => Ok(await _orderService.GetOrdersAsync());

        [HttpPost]
        [SwaggerOperation(Summary = "Creates a new order, associating it with a table and menu items.")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDto orderDto)
        {
            var validationResult = await _validator.ValidateAsync(orderDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var bill = await _billService.GetBillByStatusAsync(orderDto.TableId, BillStatus.Open);

            var order = new Order
            {
                TableId = orderDto.TableId,
                BillId = bill!.Id,
                OrderMenuItems = orderDto.OrderMenuItems.Select(omi => new OrderMenuItem
                {
                    MenuItemId = omi.MenuItemId,
                    Quantity = omi.Quantity
                }).ToList()
            };

            return Ok(await _orderService.CreateOrderAsync(order));
        }
    }
}