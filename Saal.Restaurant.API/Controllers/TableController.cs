using Microsoft.AspNetCore.Mvc;

using Saal.Restaurant.Application.Interfaces;

using Swashbuckle.AspNetCore.Annotations;

namespace Saal.Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController(ITableService tableService) : ControllerBase
    {
        private readonly ITableService _tableService = tableService;

        [HttpGet]
        [SwaggerOperation(Summary = "Retrieves all tables.")]
        public async Task<IActionResult> GetTables() => Ok(await _tableService.GetTablesAsync());

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Retrieves a specific table by ID.")]
        public async Task<IActionResult> GetTable(int id)
        {
            var table = await _tableService.GetTableByIdAsync(id);

            if (table == null)
            {
                return NotFound();
            }

            return Ok(table);
        }
    }
}