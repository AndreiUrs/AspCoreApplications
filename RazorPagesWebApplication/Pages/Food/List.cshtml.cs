using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DataLibrary.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesWebApplication.Models;

namespace RazorPagesWebApplication.Pages.Food
{
    public class ListModel : PageModel
    {
        private readonly IFoodData _foodData;
        private readonly IOrderData _orderData;
        private readonly IMapper _mapper;

        public List<FoodViewModel> FoodList { get; set; }

        public ListModel(IFoodData foodData, IOrderData orderData, IMapper mapper)
        {
            _foodData = foodData;
            _orderData = orderData;
            _mapper = mapper;
        }

        public async Task OnGet()
        {
            var foodDtoList = await _foodData.GetFood();
            FoodList = _mapper.Map <List<FoodViewModel>>(foodDtoList);
        }
    }
}
