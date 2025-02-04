using Microsoft.AspNetCore.Mvc;

using Saal.Restaurant.Application.Interfaces;
using Saal.Restaurant.Domain;

using Swashbuckle.AspNetCore.Annotations;

namespace Saal.Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController(IMenuService menuService) : ControllerBase
    {
        private readonly IMenuService _menuService = menuService;

        [HttpGet]
        [SwaggerOperation(Summary = "Retrieves the menu.")]
        public async Task<IActionResult> GetMenu()
        {
            return Ok(await _menuService.GetMenuAsync());
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Adds a new menu item.")]
        public async Task<IActionResult> AddMenuItem([FromBody] MenuItem menuItem)
        {
            return Ok(await _menuService.AddMenuItemAsync(menuItem));
        }
    }
}