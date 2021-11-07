using System;
using System.Collections;
using System.Collections.Generic;
using DinnerHall.Models;
using DinnerHall.Services;
using Microsoft.AspNetCore.Mvc;

namespace DinnerHall.Controllers
{
    
    [Route("/")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        // [HttpGet("orders")]
        // public ActionResult<IEnumerable<Order>> Get()
        // {
        //     return Ok(_ordersService.Get());
        // }
        //
        // [HttpGet("orders/{id}")]
        // public ActionResult<IEnumerable<Order>> Get(Guid id)
        // {
        //     return Ok(_ordersService.Get(id));
        // }

        [HttpPost("distribution")]
        public ActionResult<Order> PostDistribution([FromBody] DistributionData distributionData)//Order order)
        {
            _ordersService.Distribute(distributionData.order_id, distributionData);
            return Ok();
        }
        
        [HttpPost("/v2/order")]
        public ActionResult<Order> PostOrder([FromBody] Order order)
        {
            return Ok(_ordersService.Create(order));
        }
        
        
        
    }
}