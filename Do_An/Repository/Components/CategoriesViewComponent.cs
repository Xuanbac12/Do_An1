using Do_An.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Do_An.Repository.Components
{
	public class CategoriesViewComponent : ViewComponent
	{
		private readonly ApplicationDbContext _dataContext;
		public CategoriesViewComponent(ApplicationDbContext context)
		{
			_dataContext = context;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var categories = await _dataContext.Categories.ToListAsync();
			return View(categories);
		}
	}
}