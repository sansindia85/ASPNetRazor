using Microsoft.AspNetCore.Mvc;
using WiredBrainCoffeeAdmin.Data;

namespace WiredBrainCoffeeAdmin.Components
{
    public class ProductListViewComponent : ViewComponent
    {
        private IProductRepository _productRepository;

        public ProductListViewComponent(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IViewComponentResult Invoke(int count)
        {
            var items = _productRepository.GetAll();

            if (count > 0)
            {
                return View(items.Take(count).ToList());
            }

            return View(items);
        }
    }
}
