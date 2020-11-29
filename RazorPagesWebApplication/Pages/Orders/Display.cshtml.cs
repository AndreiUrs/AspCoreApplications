using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataLibrary.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesWebApplication.Models;

namespace RazorPagesWebApplication.Pages.Orders
{
    public class DisplayModel : PageModel
    {
        private readonly IFoodData _foodData;
        private readonly IOrderData _orderData;
        private readonly IMapper _mapper;

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        public OrderViewModel Order { get; set; }
        public string ItemPurchased { get; set; }
        
        public DisplayModel(IFoodData foodData, IOrderData orderData, IMapper mapper)
        {
            _foodData = foodData;
            _orderData = orderData;
            _mapper = mapper;
        }
        public async Task<IActionResult> OnGet()
        {
            await GetOrderToDisplay();
            return Page();
        }

        private async Task GetOrderToDisplay()
        {
            var orderDTO = await _orderData.GetOrderById(Id);

            if (orderDTO != null)
            {
                var foodDto = await _foodData.GetFood();
                ItemPurchased = foodDto.Where(x => x.Id == orderDTO.FoodId).FirstOrDefault()?.Title;
            }

            Order = _mapper.Map<OrderViewModel>(orderDTO);
        }
    }
}
