using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Advantage.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Advantage.API.Controllers
{
    [Route("api/[controller]")] 
    public class OrderController : Controller
    {
        private readonly ApiContext _ctx;

        public OrderController(ApiContext ctx)
        {
            _ctx = ctx;
        }

        // GET localhost:5001/api/order
        [HttpGet]
        public IActionResult Get()
        {
            var data = _ctx.Orders.Include(o => o.Customer).OrderBy(c => c.Id);

            return Ok(data);
        }

        // GET localhost:5001/api/pageNumber/pageSize (Server Side Pagination)
        [HttpGet("{pageIndex:int}/{pageSize:int}")]
        public IActionResult Get(int pageIndex, int pageSize)
        {
            var data = _ctx.Orders.Include(o => o.Customer).OrderByDescending(c => c.Placed);

            var page = new PaginatedResponse<Order>(data, pageIndex, pageSize);

            var totalCount = data.Count();
            var totalPages = Math.Ceiling((double)totalCount / pageSize);

            var response = new
            {
                Page = page,
                TotalPages = totalPages
            };

            return Ok(response);
        }

        // GET localhost:5001/api/order/5
        [HttpGet("{id}", Name="GetOrder")]
        public IActionResult Get(int id)
        {
            var order = _ctx.Orders.Include(c => c.Customer).Where(o => o.Id == id);

            return Ok(order);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Order order)
        {
            if(order == null)
            {
                return BadRequest();
            }

            _ctx.Orders.Add(order);
            _ctx.SaveChanges();

            return CreatedAtRoute("GetOrder", new {id = order.Id}, order); 
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var order = _ctx.Orders.Find(id);
            _ctx.Orders.Remove(order);
            _ctx.SaveChanges();
            
            return Ok();
        }

    }
}