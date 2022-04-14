using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreApi.DbOperations;
using StoreApi.Entities;

namespace StoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly StoreDbContext _context;
        public CategoryController(StoreDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetCategories() 
        {
            var categories = _context.Categories.OrderBy(x => x.Id).ToList<Category>();
            return Ok(categories);
        }

        [HttpGet("/Categories/{id}")] 
        public ActionResult GetProductById(int id)
        {
            var category = _context.Categories.Where(x => x.Id == id).SingleOrDefault();
            if (category != null)
            {
                return Ok(category);
            }
            return BadRequest("Kategori bulunamadı");
        }

        [HttpGet("{name}")] 
        public IActionResult GetCategoryByName(string name)
        {
            var category = _context.Categories.Where(category => category.Name == name).SingleOrDefault();
            if (category == null)
            {
                return NotFound("Kategori Bulunamadı");
            }
            else
            {
                return Ok(category);
            }
        }
        [HttpPost]
        public IActionResult AddCategory([FromBody] Category newCategory) 
        {
            var category = _context.Categories.SingleOrDefault(x => x.Id == newCategory.Id);

            if (category != null)
            {
                return NotFound("Eklenecek Kategori Zaten Var");
            }
            else
            {
                _context.Categories.Add(newCategory);
                _context.SaveChangesAsync();
                return Created("Kategori Eklendi", newCategory);
            }
        }
        [HttpPut("{id}")] 
        public IActionResult UpdateCategory(int id, [FromBody] Category newCategory)
        {
            var category = _context.Categories.SingleOrDefault(x => x.Id == id);

            if (category == null)
            {
                return NotFound("Eklenecek Kategori Bulunamadı");
            }

            category.Name = newCategory.Name != default ? newCategory.Name : category.Name;
            _context.SaveChanges();
            return Ok();

        }
        [HttpDelete("{id}")]
        public ActionResult DeleteCategory(int id)
        {
            var category = _context.Categories.SingleOrDefault(x => x.Id == id);

            if (category == null)
            {
                return NotFound("Silinecek Kategori Bulunamadı");
            }
            else
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
                return Ok("Ürün Silindi!");
            }

        }
    }
}
    

