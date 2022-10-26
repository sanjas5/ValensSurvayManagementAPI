using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System. Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using ValensSurveyManagementAPI;

namespace Valens_Survey_Management_API.Controllers
{
    //public override string ConnectionString { get; set; }
  
       
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        //public List<UserInfo> userList = new List<UserInfo>();

        private readonly  ILogger<ValuesController>  _logger;
        private readonly IDbConnection _context;
        private readonly IConfiguration _config;

        public ValuesController(IConfiguration config)
        {
            _config = config;
        }


        // GET: api/values
        [HttpGet]
        public async Task<ActionResult<List<User>>>Get()
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DatabaseConnection"));
            var users = await connection.QueryAsync<User>("select * from user");
            return Ok(users);
           
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return $"value {id}";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

