using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderApi2.Data;
using OrderApi2.Models;
using RestSharp;

namespace OrderApi2.Controllers
{
    [Route("api/Orders")]
    public class OrdersController : Controller
    {
        private readonly IRepository<Order> repository;

        public OrdersController(IRepository<Order> repos)
        {
            repository = repos;
        }

        // GET: api/orders
        [HttpGet]
        [Route("GetOrders")]
        public IEnumerable<Order> Get()
        {
            return repository.GetAll();
        }

        // GET api/orders/GetOrder/5
        [HttpGet]
        [Route("GetOrder/{id}")]
        public IActionResult Get(int id)
        {
            var item = repository.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        // POST api/orders
        [HttpPost]
        public IActionResult Post([FromBody]Order order)
        {
            if (order == null)
            {
                return BadRequest();
            }

            // Call ProductApi to get the product ordered
            RestClient c = new RestClient();
            // You may need to change the port number in the BaseUrl below
            // before you can run the request.

            foreach (var product in order.Items)
            {
                c.BaseUrl = new Uri("http://localhost:5000/api/products/");
                var request = new RestRequest(product.Id.ToString(), Method.GET);
                var response = c.Execute<Product>(request);
                var orderedProduct = response.Data;

                if (product.Quantity <= orderedProduct.ItemsInStock)
                {
                    // reduce the number of items in stock for the ordered product,
                    // and create a new order.
                    orderedProduct.ItemsInStock -= product.Quantity;
                    var updateRequest = new RestRequest(orderedProduct.Id.ToString(), Method.PUT);
                    updateRequest.AddJsonBody(orderedProduct);
                    var updateResponse = c.Execute(updateRequest);

                    if (updateResponse.IsSuccessful)
                    {
                        var newOrder = repository.Add(order);
                        return CreatedAtRoute("GetOrder", new { id = newOrder.Id }, newOrder);
                    }
                }
            }

            // If the order could not be created, "return no content".
            return NoContent();
        }
        [HttpGet]
        [Route("GetAllFromCustomer/{regNo}")]
        public IActionResult GetAllFromCustomer(int regNo)
        {
            var items = repository.GetAll().Where(x => x.CustomerRegNo == regNo);

            if (items.Count() == 0)
            {
                return NoContent();
            }

            return Ok(items);
        }

    }
}
