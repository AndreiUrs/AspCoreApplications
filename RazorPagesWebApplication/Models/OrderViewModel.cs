using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesWebApplication.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        
        [Required] 
        [MaxLength(50,ErrorMessage ="You need to keep a name up to 50 characters")]
        [MinLength(2,ErrorMessage ="You need to enter at least 2 character name")]
        public string OrderName { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        
        [DisplayName("Meals")]
        [Range(1,int.MaxValue,ErrorMessage ="You need to select a meal from the list")]
        public int FoodId { get; set; }
        
        [Range(1,10,ErrorMessage ="Don't be greedy and order more than 10 items")]
        public int Quantity { get; set; }
        
        public decimal Total { get; set; }
    }
}

