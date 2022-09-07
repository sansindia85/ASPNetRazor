using Microsoft.EntityFrameworkCore;

namespace WiredBrainCoffeeAdmin.Data
{
    public class ProductRepository : IProductRepository
    {
        private WiredContext _wiredContext;
        public ProductRepository(WiredContext context)
        {
            _wiredContext = context;
        }

        public void Add(Product product)
        {
            _wiredContext.Products.Add(product);
            _wiredContext.SaveChanges();
        }

        public void Update(Product product)
        {
            _wiredContext.Entry(product).State = EntityState.Modified;
            _wiredContext.SaveChanges();
        }

        public Product GetById(int id)
        {
            return _wiredContext.Products.FirstOrDefault(x => x.Id == id);
        }

        public List<Product> GetAll()
        {
            return _wiredContext.Products.ToList();
        }

        public void Delete(int id)
        {
            var deleteItem = _wiredContext.Products.First(x => x.Id == id);
            _wiredContext.Products.Remove(deleteItem);
            _wiredContext.SaveChanges();
        }
    }
}
