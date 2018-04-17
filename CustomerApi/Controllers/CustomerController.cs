using System;
using System.Collections.Generic;
using CustomerApi.Data;
using CustomerApi.Models;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Net;

namespace CustomerApi.Controllers
{
    [Route("api/customer")]
    public class CustomerController : Controller
    {
        // GET
        private readonly IRepository<Customer> repository;

        public CustomerController(IRepository<Customer> repos)
        {
            repository = repos;
        }

        // GET: api/Customers
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return repository.GetAll();
        }

        // GET api/products/5
        //[HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult Get(int id)
        {
            var item = repository.Get(id);

            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // POST api/Customers
        [HttpPost]
        public IActionResult Post([FromBody]Customer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }
            try
            {
                repository.Add(customer);
                return new ObjectResult(customer);
            }
            catch
            {
                return NoContent();
            }

        }

        public IActionResult Delete(int regNo)
        {

            try
            {
                repository.Remove(regNo);
                return Ok();
            }
            catch
            {
                return NoContent();
            }
        }


        [HttpGet]
        [Route("CheckCreditStanding/{regNo}")]
        public IActionResult CheckCreditStanding(int regNo)
        {
            bool hasOutstanding = false;

            RestClient c = new RestClient();
            // You may need to change the port number in the BaseUrl below
            // before you can run the request.
            c.BaseUrl = new Uri("http://localhost:55556/api/Orders/GetAllFromCustomer/");
            var request = new RestRequest(regNo.ToString(), Method.GET);
            var response = c.Execute<List<Order>>(request);
            var orderHistory = response.Data;

            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                foreach (var item in orderHistory)
                {
                    if (item.OrderStatus != OrderStatuss.Paid)
                    {
                        hasOutstanding = true;
                        break;
                    }
                }
            }
            return new ObjectResult(hasOutstanding);
        }
    }
}