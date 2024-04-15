using Do_An.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Do_An.Repository.Components
{
	public class BrandsViewComponent : ViewComponent
	{
		private readonly ApplicationDbContext _dataContext;
		public BrandsViewComponent(ApplicationDbContext context)
		{
			_dataContext = context;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var categories = await _dataContext.Brands.ToListAsync();
			return View(categories);
		}
	}
}