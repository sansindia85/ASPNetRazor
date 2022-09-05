using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WiredBrainCoffeeAdmin.Data;

namespace WiredBrainCoffeeAdmin.Pages.Products
{
    public class AddProductModel : PageModel
    {
        private WiredContext _wiredContext;
        private IWebHostEnvironment _webHostEnvironment;

        public AddProductModel(WiredContext wiredContext, 
            IWebHostEnvironment webHostEnvironment)
        {
            _wiredContext = wiredContext;
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
            _wiredContext.Products.Add(NewProduct);
            var test = await _wiredContext.SaveChangesAsync();
            
            return RedirectToPage("ViewAllProducts");
        }
    }
}
