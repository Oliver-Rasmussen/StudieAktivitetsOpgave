using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using REST_Service.Managers;
using Opgave_1;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace REST_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FootballPlayersController : ControllerBase
    {
        private readonly FootballPlayersManager _manager = new FootballPlayersManager();

        // GET: api/<FootballPlayersController>
        [HttpGet]
        public IEnumerable<FootballPlayer> Get()
        {
            return _manager.GetAll();
        }

        // GET api/<FootballPlayersController>/5
        [HttpGet("{id}")]
        public FootballPlayer Get(int id)
        {
            return _manager.GetById(id);
        }

        // POST api/<FootballPlayersController>
        [HttpPost]
        public IEnumerable<FootballPlayer> Post([FromBody] FootballPlayer value)
        {
            _manager.Add(value);
            return _manager.GetAll();
        }

        // PUT api/<FootballPlayersController>/5
        [HttpPut("{id}")]
        public IEnumerable<FootballPlayer> Put(int id, [FromBody] FootballPlayer value)
        {
            _manager.Update(id, value);
            return _manager.GetAll();
        }

        // DELETE api/<FootballPlayersController>/5
        [HttpDelete("{id}")]
        public IEnumerable<FootballPlayer> Delete(int id)
        {
            _manager.Delete(id);
            return _manager.GetAll();
        }
    }
}
