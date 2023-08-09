using ManageProductDatabase.NsApplication.Interfaces;

namespace ManageProductDatabase.NsApplication
{
    public class ProductRepositoryHandler
    {
        private readonly IProductRepository productRepository;

        public ProductRepositoryHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<List<Product>> GetAll()
        {
            return await productRepository.GetAll();
        }

        public async Task Add (string name, decimal price, decimal itemsCount)
        {
            await productRepository.Add(name, price, itemsCount);
        }

        public async Task Update (Product product)
        {
            await productRepository.Update(product);
        }

        public async Task Delete (int id)
        {
            await productRepository.Delete(id);
        }
    }
}
