using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WiredBrainCoffeeAdmin.Data;

namespace WiredBrainCoffeeAdmin.Pages.Products
{
    public class ViewAllProductsModel : PageModel
    {
        private readonly IProductRepository _productRepository;

        public List<Product> Products { get; set; }

        public ViewAllProductsModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void OnGet()
        {
            Products = _productRepository.GetAll();
        }
    }
}
