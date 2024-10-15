using AmazonCrud.Models;
using AmazonCrud.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.IO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AmazonCrud.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService productService;
        public ProductsController(ProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<List<Product>> Get()
        {
            return await productService.GetAsync();
        }

        [HttpGet("{code}")]
        public async Task<ActionResult<Product>> Get(int code)
        {
            var product = await productService.GetAsync(code);

            if (product is null)
            {
                return NotFound();
            }

            return product;
        }
        [HttpPost]
        public async Task<IActionResult> Post(Product product)
        {
            await productService.CreateAsync(product);

            return CreatedAtAction(nameof(Get), new { id = product.code }, product);
        }

        [HttpPatch("{code}")]
        public async Task<IActionResult> Update(int code, Product updateProduct)
        {
            var product = await productService.GetAsync(code);

            if (product is null)
            {
                return NotFound();
            }

            product.code = updateProduct.code;
            product.name = updateProduct.name;
            product.ratings = updateProduct.ratings;
            product.no_of_ratings = updateProduct.no_of_ratings;
            product.actual_price = updateProduct.actual_price;
            product.discount_price = updateProduct.discount_price;

            await productService.UpdateAsync(code, product);

            return Ok(product);
        }

        [HttpDelete("{code}")]
        public async Task<IActionResult> Delete(int code)
        {
            var product = await productService.GetAsync(code);

            if (product is null)
            {
                return NotFound();
            }

            await productService.RemoveAsync(code);

            return Ok(product);
        }


        [HttpGet("MainCategories")]
        public async Task<IActionResult> MainCategories()
        {
            var grandTotal = 0;
            var nullTotal = 0;

            Dictionary<string, int> total = new Dictionary<string, int>();

            var products = await productService.GetAsync();

            foreach (var item in products)
            {
                if (!string.IsNullOrWhiteSpace(item.main_category))
                {
                    if (total.ContainsKey(item.main_category))
                    {

                        total[item.main_category] = total[item.main_category] + 1;
                    }
                    else
                    {
                        total.Add(item.main_category, 1);
                    }
                }
                else
                {
                    nullTotal++;
                }
                grandTotal++;
            }

            total.Add("Null", nullTotal);


            total.Add("GrandTotal", grandTotal);
            total.OrderBy(c=>c.Key);

            return Ok(total);

        }
    }
}
