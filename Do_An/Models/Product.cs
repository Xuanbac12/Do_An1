﻿using System.ComponentModel.DataAnnotations;

namespace Do_An.Models
{
    public class Product
    {
		[Key]
		public int Id { get; set; }
		[Required, StringLength(100)]
		public string Name { get; set; }
		public string Slug { get; set; }
		[Range(0.01, 10000.00)]
		public decimal Price { get; set; }
		public string Description { get; set; }

        public string? ImageUrl { get; set; }
        public List<ProductImage>? Images { get; set; }

        public int CategoryId { get; set; }
		public Category? Category { get; set; }
		public int BrandId { get; set; }
		public Brand? Brand { get; set; }
	}
}
