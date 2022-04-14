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

        [HttpGet("/Products/{id}")]
        public ActionResult GetProductById(int id)
        {
            var product = _context.Products.Where(x => x.Id == id).SingleOrDefault();
            if (product != null)
            {
                return Ok(product);
            }
            return BadRequest("Ürün bulunamadı");
        }

        [HttpGet("{name}")]
        public IActionResult GetProductByName(string name)
        {
            var product = _context.Products.Where(category => category.Name == name).SingleOrDefault();
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

            if (product is not null)
            {
                return NotFound("Eklenecek Ürün Zaten Var");
            }
            else
            {
                _context.Products.Add(newProduct);
                _context.SaveChanges();
                return Ok("Ürün Eklendi");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product newProduct)
        {
            var product = _context.Products.SingleOrDefault(x => x.Id == id);

            if (product == null)
            {
                return NotFound("Eklenecek Ürün Bulunamadı");
            }

            product.Name = newProduct.Name != default ? newProduct.Name : product.Name;
            product.Description = newProduct.Description != default ? newProduct.Description : product.Description;
            product.Price = newProduct.Price != default ? newProduct.Price : product.Price;
            product.Stock = newProduct.Stock != default ? newProduct.Stock : product.Stock;
            _context.SaveChanges();
            return Ok();

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
