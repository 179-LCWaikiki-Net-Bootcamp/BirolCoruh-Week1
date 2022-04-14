using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreApi.DbOperations;
using StoreApi.Entities;

namespace StoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly StoreDbContext _context;
        public ProductController(StoreDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _context.Products.OrderBy(x => x.Id).ToList<Product>();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _context.Products.Where(Product => Product.Id == id).SingleOrDefault();
            if (product == null)
            {
                return BadRequest("Ürün Bulunamadı");
            }
            else
            {
                return Ok(product);
            }
        }
        [HttpGet("{name}")]
        public IActionResult GetProductByName(string name)
        {
            var product = _context.Products.Where(Product => Product.Name == name).SingleOrDefault();
            if (product == null)
            {
                return NotFound("Ürün Bulunamadı");
            }
            else
            {
                return Ok(product);
            }
        }
        [HttpPost]
        public IActionResult AddProduct([FromBody] Product newProduct)
        {
            var product = _context.Products.SingleOrDefault(x => x.Id == newProduct.Id);

            if (product != null)
            {
                return NotFound("Eklenecek Ürün Bulunamadı");
            }
            else
            {
                _context.Products.Add(product);
                _context.SaveChangesAsync();
                return Created("Ürün Eklendi", product);
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product newProduct)
        {
            var product = _context.Products.SingleOrDefault(x => x.Id == newProduct.Id);

            if (product != null)
            {
                return NotFound("Eklenecek Ürün Bulunamadı");
            }
            else
            {
                _context.Products.Add(product);
                return Created("Ürün Eklendi", product);
            }
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var product = _context.Products.SingleOrDefault(x => x.Id == id);

            if (product == null)
            {
                return NotFound("Silinecek Ürün Bulunamadı");
            }
            else
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
                return Ok("Ürün Silindi!");
            }

        }
    }
}
