using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using CyloAPI.Model;
using CyloAPI.Respository;
using System.Collections.Generic;

namespace CyloAPI.Controllers
{
    [Route("api/[controller]")]
    [Route("api/[controller]/id")]
    [ApiController]
    public class ProductDetailsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ProductDetailsController(IConfiguration config)
        {
            _configuration = config;
        }

        [HttpGet]
        public IActionResult Get()
        {
            ProductRepository prodRep = new ProductRepository(_configuration); 
            return Ok(prodRep.getProduct());
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            ProductRepository productRep = new ProductRepository(_configuration);
            List<ProductDetails> products = new List<ProductDetails>();
            products = productRep.getProduct();
            foreach (var product in products)
            {
                if (product.id == id)
                {
                    return Ok(product);
                }

            }
            return Ok();
        }
        [HttpPost]

        public void Post(ProductDetails product)
        {
            ProductRepository prodRep = new ProductRepository(_configuration);
          string msg=prodRep.addProduct(product);

        }
        [HttpPut("{id}")]

        public  void Put(ProductDetails product)
        {
            ProductRepository prodRep = new ProductRepository(_configuration);
            string msg= prodRep.updateProduct(product);
        }
        [HttpDelete("{id}")]

        public void Delete(int id)
        {
            ProductRepository prodRep = new ProductRepository(_configuration);
            string msg=prodRep.deleteProduct(id);
        }
    }
}

