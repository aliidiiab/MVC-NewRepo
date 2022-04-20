using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.ViewModels
{
    public class CheckOutViewModel
    {
        public ShoppingCartViewModel s1 { get; set; }
        public  eCommerceApp.Models.ApplicationUser userInfo { get; set; }



    }
}
