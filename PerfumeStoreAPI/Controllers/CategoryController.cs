using Microsoft.AspNetCore.Mvc;
using PerfumeStore.API.RequestModel;
using PerfumeStore.API.ResponseModel;
using PerfumeStore.Service.BusinessModel;
using PerfumeStore.Service.Service;

namespace PerfumeStore.API.Controllers
{
    [Route("api/v1/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("all")]
        public IActionResult GetCategory([FromQuery] string? search, [FromQuery] string? sortBy, [FromQuery] bool desc, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            // Get paginated result from the service
            var categories = _categoryService.GetCategories(search, sortBy, desc, page, pageSize);
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<CategoryResponseModel>>> GetCategoryById(Guid id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();

            var response = new CategoryResponseModel
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
            };

            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> CreateCategory(string categoryName)
        {
            return await _categoryService.InsertCategoryAsync(categoryName);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerfume(Guid id, string categoryName)
        {
            return await _categoryService.UpdateCategoryAsync(id, categoryName);
        }
    }
}
