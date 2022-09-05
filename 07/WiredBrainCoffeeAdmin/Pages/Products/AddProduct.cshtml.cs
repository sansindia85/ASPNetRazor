using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WiredBrainCoffeeAdmin.Data;

namespace WiredBrainCoffeeAdmin.Pages.Products
{
    public class AddProductModel : PageModel
    {
        private IProductRepository _productRepository;
        private IWebHostEnvironment _webHostEnvironment;

        public AddProductModel(IProductRepository productRepository, 
            IWebHostEnvironment webHostEnvironment)
        {
            _productRepository = productRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public Product NewProduct { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (NewProduct.Upload is not null)
            {
                NewProduct.ImageFileName = NewProduct.Upload.FileName;

                var file = Path.Combine(_webHostEnvironment.ContentRootPath,
                    "wwwroot/images/menu", NewProduct.Upload.FileName);

                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await NewProduct.Upload.CopyToAsync(fileStream);
                }
            }
            
            NewProduct.Created = DateTime.Now;
            _productRepository.Add(NewProduct);
            
            return RedirectToPage("ViewAllProducts");
        }
    }
}
