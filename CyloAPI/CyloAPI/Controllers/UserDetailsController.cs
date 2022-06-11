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
    public class UserDetailsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public UserDetailsController(IConfiguration config)
        {
            _configuration = config;
        }

        [HttpGet]
        public IActionResult Get()
        {
            UserRepository userRep = new UserRepository(_configuration);
            List<UserDetails> users = new List<UserDetails>();
            users = userRep.getUser();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            UserRepository userRep = new UserRepository(_configuration);
            List<UserDetails> users = new List<UserDetails>();
            users = userRep.getUser();
            foreach (var user in users)
            {
                if (user.id == id)
                {
                    return Ok(user);
                }

            }
            return Ok();
        }
        [HttpPost]

        public void Post(UserDetails user)
        {
            UserRepository userRep = new UserRepository(_configuration);
            string msg = userRep.addUser(user);
        }
        [HttpPut("{id}")]

        public void Put(UserDetails user)
        {
            UserRepository userRep = new UserRepository(_configuration);
            string msg = userRep.updateUser(user);
        }
    }
}
