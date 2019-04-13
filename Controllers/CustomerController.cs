using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc; // To use Controller we bring in Microsoft.AspNetCore.Mvc
using Advantage.API.Models; // To bring ApiContext

namespace Advantage.API.Controllers
{
    [Route("api/[controller]")] // Adding attribute to define route.
    public class CustomerController : Controller
    {
        // Now we are going to create a private field for our DbContext
        // Which we need to access the entities in our DbContext
        // We are going to create a private field which we will use to access the data in our databbase

        private readonly ApiContext _ctx;

        // Using Dependency Injection to set out ApiContext field In the constructor we go ahead to set the value equal to our ApiContext ctx
        public CustomerController(ApiContext ctx)
        {
            _ctx = ctx;
        }

        // GET localhost:5001/api/customer
        [HttpGet] //Add HttpGet attribute 
        public IActionResult Get()
        {
            var data = _ctx.Customers.OrderBy(c => c.Id);

            return Ok(data); // i.e. return Http 200 response and results of our customer objects with an Ok response
        }
        
        // GET localhost:5001/api/customer/5
        [HttpGet("{id}", Name = "GetCustomer")]  //We give the route the name GetCustomer
        public IActionResult Get(int id)
        {
            var customer = _ctx.Customers.Find(id);
            return Ok(customer); 
        }

        // We will look at the post request when we turn the different servers on and off
        [HttpPost]
        public IActionResult Post([FromBody] Customer customer) //We use this FromBody attribute. From the popup message we should be able to see that a parameter should be bound using the request body. What we have is a request body that maps to the properties of our customer
        {
            if(customer == null)
            {
                return BadRequest();
            }

            _ctx.Customers.Add(customer);
            _ctx.SaveChanges();

            return CreatedAtRoute("GetCustomer", new {id = customer.Id}, customer);  //From popup we see that the first argument is the name of the route, then we need to pass it the value of the id of the customer that we just created and the value of the actual customer object itself. CreatedAtRoute returns a 201 response which is a http response when something is created successfully
        }
    }   
}