using Do_An.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Do_An.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ApplicationDbContext _dataContext;

		public CategoryController(ApplicationDbContext context)
		{
			_dataContext = context;
		}

		public async Task<IActionResult> Index(string Slug = "")
		{
			// Truy vấn danh mục dựa trên Slug
			Category category = await _dataContext.Categories.FirstOrDefaultAsync(c => c.Slug == Slug);

			// Kiểm tra xem danh mục có tồn tại hay không
			if (category == null)
			{
				// Nếu không tìm thấy danh mục, chuyển hướng hoặc xử lý lỗi tùy theo logic của bạn
				return RedirectToAction("Index", "Home"); // Ví dụ: chuyển hướng về trang chủ
			}

			// Truy vấn sản phẩm thuộc danh mục đã chọn
			var productsByCategory = _dataContext.Products
				.Where(p => p.CategoryId == category.Id)
				.OrderByDescending(p => p.Id);

			// Trả về view với danh sách sản phẩm thuộc danh mục đã chọn
			return View(await productsByCategory.ToListAsync());
		}
	}
}