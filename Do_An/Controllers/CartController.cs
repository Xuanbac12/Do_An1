using Do_An.Models;
using Do_An.Models.ViewModels;
using Do_An.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Do_An.Controllers
{

	[Authorize(Roles = "User")]
	public class CartController : Controller
	{
		private readonly ApplicationDbContext _dataContext;

		public CartController(ApplicationDbContext _context)
		{
			_dataContext = _context;
		}
		public IActionResult Index()
		{
			List<CartItem> cartItems = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
			CartItemViewModel cartVM = new()
			{
				CartItems = cartItems,
				GrandTotal = cartItems.Sum(x => x.Quantity * x.Price)

			};
			return View(cartVM);
		}

		public ActionResult CheckOut()
		{
			return View("~/Views/Checkout/Index.cshtml");
		}

		public async Task<IActionResult> Add(int Id)
		{
			Product product = await _dataContext.Products.FindAsync(Id);
			List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();
			CartItem cartItem = cart.FirstOrDefault(c => c.ProductId == Id);

			if (cartItem == null)
			{
				cart.Add(new CartItem(product));
			}
			else
			{
				cartItem.Quantity += 1;
			}

			HttpContext.Session.SetJson("Cart", cart);
			TempData["success"] = "Them don hang thanh cong.";
			return Redirect(Request.Headers["Referer"].ToString());
		}

		public async Task<IActionResult> Decrease(int Id)
		{

			List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

			CartItem cartItem = cart.Where(c => c.ProductId == Id).FirstOrDefault();

			if (cartItem.Quantity > 1)
			{
				--cartItem.Quantity;
			}
			else
			{
				cart.RemoveAll(p => p.ProductId == Id);
			}
			if (cart.Count == 0)
			{
				HttpContext.Session.Remove("Cart");
			}

			else
			{
				HttpContext.Session.SetJson("Cart", cart);

			}




			return RedirectToAction("Index");


		}

		public async Task<IActionResult> Increase(int Id)
		{

			List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

			CartItem cartItem = cart.Where(c => c.ProductId == Id).FirstOrDefault();

			if (cartItem.Quantity >= 1)
			{
				++cartItem.Quantity;
			}
			else
			{
				cart.RemoveAll(p => p.ProductId == Id);
			}
			if (cart.Count == 0)
			{
				HttpContext.Session.Remove("Cart");
			}

			else
			{
				HttpContext.Session.SetJson("Cart", cart);

			}




			return RedirectToAction("Index");


		}


		public async Task<IActionResult> Remove(int Id)
		{
			List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

			cart.RemoveAll(p => p.ProductId == Id);

			if (cart.Count == 0)
			{
				HttpContext.Session.Remove("Cart");
			}
			else
			{
				HttpContext.Session.SetJson("Cart", cart);
			}
			TempData["success"] = "Da xoa don hang thanh cong.";
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> ClearCart(int Id)
		{
			HttpContext.Session.Remove("Cart");
			TempData["success"] = "Da xoa toan bo don hang.";
			return RedirectToAction("Index");

		}


	}
}
