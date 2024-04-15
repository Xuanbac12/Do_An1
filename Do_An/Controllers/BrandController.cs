using Do_An.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Do_An.Controllers
{
	public class BrandController : Controller
	{
		private readonly ApplicationDbContext _dataContext;

		public BrandController(ApplicationDbContext context)
		{
			_dataContext = context;
		}

		public async Task<IActionResult> Index(string Slug = "")
		{
			// Truy vấn thương hiệu dựa trên Slug
			Brand brand = await _dataContext.Brands.FirstOrDefaultAsync(b => b.Slug == Slug);

			// Kiểm tra xem thương hiệu có tồn tại hay không
			if (brand == null)
			{
				// Nếu không tìm thấy thương hiệu, chuyển hướng hoặc xử lý lỗi tùy theo logic của bạn
				return RedirectToAction("Index", "Home"); // Ví dụ: chuyển hướng về trang chủ
			}

			// Truy vấn sản phẩm thuộc thương hiệu đã chọn
			var productsByBrand = _dataContext.Products
				.Where(p => p.BrandId == brand.Id)
				.OrderByDescending(p => p.Id);

			// Trả về view với danh sách sản phẩm thuộc thương hiệu đã chọn
			return View(await productsByBrand.ToListAsync());
		}
	}
}