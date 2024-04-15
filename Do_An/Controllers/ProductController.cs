using Do_An.Models;
using Microsoft.AspNetCore.Mvc;

namespace Do_An.Controllers
{  
    


	public class ProductController : Controller
    {
		private readonly ApplicationDbContext _dataContext;

		public ProductController(ApplicationDbContext context)
		{
			_dataContext =context;
		}


		public IActionResult Index()
        {
            return View();
        }

		public async Task<IActionResult> Details(int Id)
		{
			if (Id == null) return RedirectToAction("Index");

			var productsById = _dataContext.Products.Where(p => p.Id == Id).FirstOrDefault();
			return View(productsById);
		}
	}
}
