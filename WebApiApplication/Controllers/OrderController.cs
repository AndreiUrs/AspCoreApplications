using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using DataLibrary.Data;
using DataLibrary.Models;
using WebApiApplication.Filters;
using WebApiApplication.Models;

namespace WebApiApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderData _orderData;
        private readonly IFoodData _foodData;

        public OrderController(IOrderData orderData,IFoodData foodData)
        {
            _orderData = orderData;
            _foodData = foodData;
        }

        [HttpPost]
        [ValidateModel]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(OrderModel order)
        {
            var food = await _foodData.GetFood();
            order.Total = order.Quantity * food.First(x => x.Id == order.FoodId).Price;

            var id =await _orderData.CreateOrder(order);

            return Ok(new {Id = id});
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            if (id == 0) return BadRequest();

           var order = await _orderData.GetOrderById(id);

           if (order is null)
               return NotFound();

           var food = await _foodData.GetFood();

           return Ok(new
           {
               Order = order,
               ItemOrdered = food.First(x=>x.Id == order.FoodId).Title
           });

        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromBody] OrderUpdateDto updateModel)
        {
            await _orderData.UpdateOrder(updateModel.OrderId, updateModel.OrderName);
            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderData.DeleteOrder(id);
            return Ok();
        }

    }
}
