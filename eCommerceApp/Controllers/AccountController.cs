using eCommerceApp.Models;
using eCommerceApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using eCommerceApp.Controllers;
using eCommerceApp.Repos;

namespace eCommerceApp.Controllers
{
    public class AccountController : Controller
    {

        private readonly IProductRepository ProductRepository;
        private readonly IShoppingCart ShoppingCart;
        #region Manageres
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        #endregion

        #region CTOR
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IProductRepository _ProductRepository, IShoppingCart _ShoppingCart)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            ProductRepository = _ProductRepository;
            ShoppingCart = _ShoppingCart;
            
        }
        #endregion

        #region USERS Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel registerUser)
        {
            if (ModelState.IsValid == false)
            {
                return View(registerUser);

            }
            try
            {
                ApplicationUser userModel = new ApplicationUser();
                userModel.Email = registerUser.Email;
                userModel.UserName = registerUser.UserName;
                userModel.Address = registerUser.Address;
                userModel.Phone = registerUser.Phone;
                userModel.PasswordHash = registerUser.Password;
                IdentityResult result = await userManager.CreateAsync(userModel, registerUser.Password);
                if (result.Succeeded == true)
                {
                    await userManager.AddToRoleAsync(userModel, "User");
                    await signInManager.SignInAsync(userModel, false);  
                    return RedirectToAction("Index", "home");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(registerUser);
        }
        #endregion

        #region Admin Register
        //----------------------------For Admin -------------------\\
        //[Authorize(Roles = "Admin")]
        public IActionResult AdminRegister()
        {
            return View();
        }
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminRegister(RegisterUserViewModel registerUser)
        {
            if (ModelState.IsValid == false)
            {
                return View(registerUser);

            }
            try
            {
                ApplicationUser userModel = new ApplicationUser();
                userModel.Email = registerUser.Email;
                userModel.UserName = registerUser.UserName;
                userModel.Address = registerUser.Address;
                userModel.Phone = registerUser.Phone;
                userModel.PasswordHash = registerUser.Password;

                IdentityResult result = await userManager.CreateAsync(userModel, registerUser.Password);
                if (result.Succeeded == true)
                {
                    await userManager.AddToRoleAsync(userModel, "Admin");
                    await signInManager.SignInAsync(userModel, false); //create cookie , false 3shan lsa bn3ml register msh login 
                    //authoniticat create cookie
                    return RedirectToAction("Index", "home");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(registerUser);
        }
        //----------------------------For Admin -------------------\\
        #endregion

        #region Login
        [HttpGet]
        public IActionResult Login(string ReturnUrl = "~/home")
        {
            ViewData["RedirectUrl"] = ReturnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserViewModel loginUser, string ReturnUrl = "~/home")
        {
            if (ModelState.IsValid == false)
            {
                return View(loginUser);
            }
            ApplicationUser appUser = await userManager.FindByEmailAsync(loginUser.Email);
            if (appUser != null)
            {
                bool result = await userManager.CheckPasswordAsync(appUser, loginUser.Password);
                if (result == true)
                {
                    await signInManager.SignInAsync(appUser, loginUser.RememberMe);
                    return LocalRedirect(ReturnUrl);
                }
            }
            ModelState.AddModelError("", "Wrong UserName or Passwrod!");
            return View(loginUser);
        }

        #endregion

        #region SignOut
        public async Task<IActionResult> Signout()
        {
            
            await signInManager.SignOutAsync();
            var items = ProductRepository.getallproducts();
            if (items != null)
            {
                ShoppingCart.RemoveAllItemsFromCart(items);
            }
            return RedirectToAction("Login");
        }
        #endregion
    }
}
#region Claims
//List<Claim> customClaim = new List<Claim>();
//await signInManager.SignInWithClaimsAsync(appUser, loginUser.RememberMe);
#endregion