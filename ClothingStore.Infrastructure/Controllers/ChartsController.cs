using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClothingStore.Infrastructure;

namespace ClothingStore.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {
        private readonly ClothingStoreContext _context;

        public ChartsController(ClothingStoreContext context)
        {
            _context = context;
        }

        [HttpGet("productData")]
        public async Task<IActionResult> GetProductData()
        {
            var data = await _context.Products // Перевір, щоб у контексті було Products
                .Include(p => p.Category)
                .GroupBy(p => p.Category.CategoryName) // Використовуємо твою назву поля
                .Select(g => new {
                    category = g.Key ?? "Без категорії",
                    count = g.Count()
                })
                .ToListAsync();

            return Ok(data);
        }
    }
}