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
    [ApiController]
    public class AdminDetailsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AdminDetailsController(IConfiguration config)
        {
            _configuration = config;
        }

        [HttpGet]
        public IActionResult Get()
        {
            AdminRepository adminRep = new AdminRepository(_configuration);
            List<AdminDetails> users = new List<AdminDetails>();
            users = adminRep.getAdmin();
            return Ok(users);
        }
        [HttpPost]

        public void Post(AdminDetails admin)
        {
            AdminRepository adminRep = new AdminRepository(_configuration);
            string msg=adminRep.addAdmin(admin);
        }
    }
}

