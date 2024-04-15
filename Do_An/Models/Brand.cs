using System.ComponentModel.DataAnnotations;

namespace Do_An.Models
{
    public class Brand
    {
		public int Id { get; set; }
		[Required(ErrorMessage = "Tên Brand là bắt buộc")]
		[StringLength(100, MinimumLength = 3, ErrorMessage = "Tên Brand phải từ 3 đến 100 ký tự")]
		public string Name { get; set; }

		public string Slug { get; set; }

		public string Status { get; set; }

		public List<Product>? Products { get; set; }

	}
}
