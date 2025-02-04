using FluentValidation;

using Microsoft.AspNetCore.Mvc;

using Saal.Restaurant.Application.DTOs;
using Saal.Restaurant.Application.Interfaces;

using Swashbuckle.AspNetCore.Annotations;

namespace Saal.Restaurant.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BillController(IBillService billService, 
    IValidator<GenerateBillRequest> billValidator, 
    IValidator<UpdateBillStatusRequest> statusValidator) : ControllerBase
{
    private readonly IBillService _billService = billService;
    private readonly IValidator<GenerateBillRequest> _billValidator = billValidator;
    private readonly IValidator<UpdateBillStatusRequest> _statusValidator = statusValidator;

    [HttpGet]
    [SwaggerOperation(Summary = "Retrieves all bills.")]
    public async Task<IActionResult> GetBills() => Ok(await _billService.GetBillsAsync());

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Retrieves a bill by ID.")]
    public async Task<IActionResult> GetBillById(int id)
    {
        var bill = await _billService.GetBillByIdAsync(id);

        if (bill == null)
        {
            return NotFound(); 
        }

        return Ok(bill);
    }

    [HttpPost("create")]
    [SwaggerOperation(Summary = "Creates a bill for a table.")]
    public async Task<IActionResult> CreateBill([FromBody] GenerateBillRequest request)
    {
        var validationResult = await _billValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var bill = await _billService.CreateBillAsync(request.TableId);

        return Ok(bill);
    }

    [HttpPut("{tableId}/update-total")]
    [SwaggerOperation(Summary = "Updates the total of an existing open bill for a table.")]
    public async Task<IActionResult> UpdateBillTotal(int tableId)
    {
        var updatedBill = await _billService.UpdateBillTotalAsync(tableId);

        if (updatedBill == null)
        {
            return NotFound("Bill is not open.");
        }

        return Ok(updatedBill);
    }

    [HttpPut("{id}/status")]
    [SwaggerOperation(Summary = "Updates the status of a bill.")]
    public async Task<IActionResult> UpdateBillStatus(int id, [FromBody] UpdateBillStatusRequest request)
    {
        var validationResult = await _statusValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var updatedBill = await _billService.UpdateBillStatusAsync(id, request.Status);

        if (updatedBill == null)
        {
            return NotFound();
        }

        return Ok(updatedBill);
    }
}