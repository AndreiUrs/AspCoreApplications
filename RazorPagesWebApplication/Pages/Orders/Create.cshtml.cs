using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataLibrary.Data;
using DataLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPagesWebApplication.Models;

namespace RazorPagesWebApplication.Pages.Orders
{
    public class CreateModel : PageModel
    {
        private readonly IFoodData _foodData;
        private readonly IOrderData _orderData;
        private readonly IMapper _mapper;

        public List<SelectListItem> FoodItems { get; set; }
        
        [BindProperty]
        public OrderViewModel Order { get; set; }

        public CreateModel(IFoodData foodData,IOrderData orderData, IMapper mapper)
        {
            _foodData = foodData;
            _orderData = orderData;
            _mapper = mapper;
        }

        public async Task OnGet()
        {
            await GetFoodItemsSelectionList();
        }


        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid == false) return Page();

            var orderId = await PlaceOrder();

            return RedirectToPage("./Display",new { Id=orderId});          
        }

        private async Task<int> PlaceOrder()
        {
            var food = await _foodData.GetFood();
            Order.Total = Order.Quantity * food.Where(x => x.Id == Order.FoodId).First().Price;

            var orderDto = _mapper.Map<OrderModel>(Order);

            return await _orderData.CreateOrder(orderDto);
        }

        private async Task GetFoodItemsSelectionList()
        {
            FoodItems = new List<SelectListItem>();

            var foodList = await _foodData.GetFood();

            foreach (var foodItem in foodList)
            {
                FoodItems.Add(new SelectListItem() { Value = foodItem.Id.ToString(), Text = foodItem.Title });
            }
        }
    }
}
