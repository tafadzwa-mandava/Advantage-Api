using System.Linq;
using Advantage.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Advantage.API.Controllers{

    [Route("api/[controller]")]
    public class ServerController : Controller
    {
        private readonly ApiContext _ctx;
        public ServerController(ApiContext ctx)
        {
            _ctx = ctx;
        }

        // Get localhost:5001/api/server
        [HttpGet]
        public IActionResult Get()
        {
            var data = _ctx.Servers.OrderBy(s => s.Id);

            return Ok(data);
        }

        // Get localhost:5001/api/server/5
        [HttpGet("{id}", Name="GetServer")]
        public IActionResult Get(int id)
        {
            var server = _ctx.Servers.Find(id);

            return Ok(server);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Server server)
        {
            if(server == null)
            {
                return BadRequest();
            }

            _ctx.Servers.Add(server);
            _ctx.SaveChanges();

            return CreatedAtRoute("GetServer", new {id = server.Id}, server);
        }

        // DELETE localhost:5001/api/servers/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var server = _ctx.Servers.Find(id);
            _ctx.Servers.Remove(server);
            _ctx.SaveChanges();

            return Ok();
        }

    }
}