using AppDev_Assignment2.Model;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace AppDev_Assignment2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ApiController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private NpgsqlConnection connect()
        {
            return new NpgsqlConnection(_configuration.GetConnectionString("productConnection").ToString());
        }

        [HttpGet]
        [Route("GetAllProducts")]
        public Response GetAllProducts()
        {
            return Operations.getProducts(connect());
        }

        [HttpPost]
        [Route("AddProduct")]
        public Response AddProduct(Product product)
        {
            return Operations.createProduct(connect(), product);
        }

        [HttpPut]
        [Route("UpdateProduct")]
        public Response UpdateProduct(Product product, String name, int weight, decimal price)
        {
            return Operations.updateProduct(connect(), product, name, weight, price);
        }

        [HttpDelete]
        [Route("DeleteProduct")]
        public Response DeleteProduct(Product product)
        { 
            return Operations.deleteProduct(connect(), product);
        }
    }
}