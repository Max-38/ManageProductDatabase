namespace ManageProductDatabase.NsApplication.Interfaces
{
    public interface IProductRepository
    {
        public Task<List<Product>> GetAll();
        public Task Add(string name, decimal price, decimal itemsCount);
        public Task Update(Product product);
        public Task Delete(int id);
    }
}
