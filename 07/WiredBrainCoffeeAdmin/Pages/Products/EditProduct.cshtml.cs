using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WiredBrainCoffeeAdmin.Data;

namespace WiredBrainCoffeeAdmin.Pages.Products
{
    public class EditProductModel : PageModel
    {
        private IProductRepository _productRepository;
        private IWebHostEnvironment _webHostEnvironment;

        public EditProductModel(IProductRepository productRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            _productRepository = productRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [FromRoute]
        public int Id { get; set; }

        [BindProperty]
        public Product EditProduct { get; set; }

        public void OnGet()
        {
            EditProduct = _productRepository.GetById(Id);
        }

        public async Task<IActionResult> OnPostEdit()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (EditProduct.Upload is not null)
            {
                EditProduct.ImageFileName = EditProduct.Upload.FileName;

                var file = Path.Combine(_webHostEnvironment.ContentRootPath,
                    "wwwroot/images/menu", EditProduct.Upload.FileName);

                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await EditProduct.Upload.CopyToAsync(fileStream);
                }
            }

            EditProduct.Created = DateTime.Now;
            EditProduct.Id = Id;
            _productRepository.Update(EditProduct);

            return RedirectToPage("ViewAllProducts");
        }

        public IActionResult OnPostDelete()
        {
            _productRepository.Delete(Id);

            return RedirectToPage("ViewAllProducts");
        }
    }
}
