public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAll();
    Task<Product> GetByID(int id);
    Task Add(Product product);
    Task Update(Product product);
    Task Delete(int id);
}