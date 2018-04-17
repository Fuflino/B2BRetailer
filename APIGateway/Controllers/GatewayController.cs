using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIGateway.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using SimpleJson;

namespace APIGateway.Controllers
{
    [Produces("application/json")]
    [Route("api/Gateway")]
    public class GatewayController : Controller
    {
        [HttpPost]
        [Route("PlaceOrder")]
        public IActionResult PlaceOrder(Customer customer, Order order)
        {
            RestClient c = new RestClient();
            // You may need to change the port number in the BaseUrl below
            // before you can run the request.
            c.BaseUrl = new Uri("http://localhost:64921/api/Customer/");
            var request = new RestRequest(customer.RegNo.ToString(), Method.GET);
            var response = c.Execute<Customer>(request);
            var existingCustomer = response.Data;

            if(existingCustomer == null)
            {
                return NotFound();
            }
            
            // You may need to change the port number in the BaseUrl below
            // before you can run the request.
            c.BaseUrl = new Uri("http://localhost:55556/api/Orders/GetAllFromCustomer/");
            var request2 = new RestRequest(customer.RegNo.ToString(), Method.GET);
            var response2 = c.Execute<bool>(request2);
            bool hasOutstanding = response2.Data;

            if(hasOutstanding == false)
            {
                return new ObjectResult(hasOutstanding);
            }

            // You may need to change the port number in the BaseUrl below
            // before you can run the request.
            c.BaseUrl = new Uri("http://localhost:55556/api/Orders/");
            var request3 = new RestRequest(SimpleJson.SimpleJson.SerializeObject(order), Method.POST);
            var response3 = c.Execute<Order>(request2);
            var newOrder = response2.Data;

            if(response3.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return NoContent();
            }

            return new ObjectResult(newOrder);
        }
    }
}