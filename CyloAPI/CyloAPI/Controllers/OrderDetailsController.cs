using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using CyloAPI.Model;
using System.Collections.Generic;
using CyloAPI.Respository;

namespace CyloAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public OrderDetailsController(IConfiguration config)
        {
            _configuration = config;
        }

        [HttpGet]
        public IActionResult Get()
        {
            OrderRepository orderRep = new OrderRepository(_configuration);
            List<OrderDetails> orders = new List<OrderDetails>();
            orders = orderRep.getOrder();
            return Ok(orders);
        }
        [HttpPost]

        public void Post(OrderDetails order)
        {
            OrderRepository orderRep = new OrderRepository(_configuration);
            string msg=orderRep.addOrder(order);

        }
       
    }
}

