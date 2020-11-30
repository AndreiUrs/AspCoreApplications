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
    public class DeleteModel : PageModel
    {
        private readonly IOrderData _orderData;
        private readonly IMapper _mapper;

        public OrderViewModel Order { get; set; }
        
        [BindProperty(SupportsGet =true)]
        public int Id { get; set; }


        public DeleteModel(IOrderData orderData,IMapper mapper)
        {
            _orderData = orderData;
            _mapper = mapper;
        }
        
        public async Task OnGet()
        {
            var orderDto = await _orderData.GetOrderById(Id);
            Order = _mapper.Map<OrderViewModel>(orderDto);
        }

       public async Task<IActionResult> OnPost()
        {
            await _orderData.DeleteOrder(Id);
            return RedirectToPage("./Create");
        }
    }
}
