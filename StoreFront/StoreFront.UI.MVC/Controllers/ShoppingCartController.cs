using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StoreFront.DATA.EF.Models;
using StoreFront.UI.MVC.Models;

namespace StoreFront.UI.MVC.Controllers
{
    public class ShoppingCartController : Controller
    {
        //props for Context & UserManager
        private readonly StoreFrontContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        // Create ctor
        public ShoppingCartController(StoreFrontContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            //retrieve the session cart
            var sessionCart = HttpContext.Session.GetString("cart");

            //Create the local cart instance
            Dictionary<int, CartIremViewModel> localCart = null;

            if(sessionCart == null || sessionCart.Count() == 0)
            {
                ViewBag.Message = "There are no items in your cart.";

                localCart = new Dictionary<int, CartIremViewModel>();
            }
            else
            {
                ViewBag.Message = null;
                localCart = JsonConvert.DeserializeObject<Dictionary<int, CartIremViewModel>>(sessionCart);
            }

            return View(localCart);
        }

        public IActionResult AddToCart(int id)
        {
            //local cart instance
            Dictionary<int, CartIremViewModel> localCart = null;

            //retrieve the session instace of the cart to see if it exists
            var sessionCart = HttpContext.Session.GetString("cart");

            if(sessionCart == null)
            {
                localCart = new Dictionary<int, CartIremViewModel>();
            }
            else
            {
                localCart = JsonConvert.DeserializeObject<Dictionary<int, CartIremViewModel>>(sessionCart); 
            }

            Pokemon pokemon = _context.Pokemons.Find(id);

            CartIremViewModel civm = new CartIremViewModel(1, pokemon);

            //if the product was already in the cart increase qty by 1
            //otherwise, add the new item into the local cart
            if(localCart.ContainsKey(pokemon.PokemonId))
            {
                localCart[pokemon.PokemonId].Qty++;
            }
            else
            {
                localCart.Add(pokemon.PokemonId, civm);
            }

            string jsonCart = JsonConvert.SerializeObject(localCart);

            HttpContext.Session.SetString("cart", jsonCart);

            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int id)
        {
            //retrieve our cart from session
            var sessionCart = HttpContext.Session.GetString("cart");

            Dictionary<int, CartIremViewModel> localCart = JsonConvert.DeserializeObject<Dictionary<int, CartIremViewModel>>(sessionCart);

            localCart.Remove(id);

            if (localCart.Count == 0)
            {
                HttpContext.Session.Remove("cart");
            }
            else
            {
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(localCart));
            }

            return RedirectToAction("Index");
        }
    }
}
