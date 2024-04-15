@section scripts {
	<script>
		$(document).ready(function () {
			$('.increase-quantity').click(function () {
				var productId = $(this).data('product-id');

				$.ajax({
					url: '@Url.Action("Increase", "Cart")',
					type: 'POST',
					data: { id: productId },
					success: function () {
						// Cập nhật số lượng sản phẩm hiển thị trên giao diện người dùng (nếu cần)
					},
					error: function (xhr, status, error) {
						console.error(error);
					}
				});
			});
		});
	</script>
}