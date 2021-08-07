using Electro.Model.Database;
using Electro.Model.Database.Entities;
using Electro.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web;

namespace Electro.WebApplication.Components
{
    public class Cart : ViewComponent
    {
        private readonly DataManager _dataManager;

        public Cart(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public IViewComponentResult Invoke()
        {
            var viewModel = new CartViewModel();

            if (User.Identity.IsAuthenticated)
            {

            }
            else
            {
                //if (Request.Cookies["cart"] != null)
                //{
                //    //var cartJson = HttpUtility.UrlDecode(Request.Cookies["cart"]);
                //    //var cart = JsonConvert.DeserializeObject<CartViewModel>(cartJson);

                //    //viewModel.Content = cart;
                //}
                //else
                //{
                //    //viewModel.Content = new Dictionary<Product, int>();
                //}
            }

            return View("Default", viewModel);
        }
    }
}
