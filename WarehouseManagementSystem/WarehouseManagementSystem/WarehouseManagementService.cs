namespace WarehouseManagementSystem;

public class WarehouseManagementService : IWarehouseManagementService
{
    private int _productId = 1;

    private List<IHistory> list = new List<IHistory>();

    private Dictionary<int,int> lastValues = new Dictionary<int,int>();
    
    private List<IProduct> productList = new List<IProduct>();
    
    public IProduct AddProduct(string name)
    {
        if (productList.Select(x=>x.Name).Contains(name))
        {
            throw new ArgumentException($"Товар с именем {name} уже существует");
        }
        Product product = new Product();
        product.Id = _productId++;
        product.Name = name;
        productList.Add(product);
        return new Product
        {
            Id = product.Id,
            Name = product.Name,
            Count = product.Count
        };
    }

    public void ChangeCount(int productId, int newValue)
    {
        if (!productList.Select(x => x.Id).Contains(productId))
        {
            throw new ArgumentException($"Товар с id = {productId} не существует");
        }

        
        if (newValue < 0)
            throw new ArgumentException("Новое значение не может быть отрицательным");
        
        int oldValue = lastValues.GetValueOrDefault(productId);
        if (oldValue == newValue)
            throw new ArgumentException("Новое значение должно отличаться от предыдущего.");
        
        lastValues[productId] = newValue;
        list.Add(new History() { ProductId = productId, Changes = newValue - oldValue  });
    }

    public IEnumerable<IHistory> GetHistory()
    {
        return new List<IHistory>(list.Select(x=> new History()
        {
            ProductId = x.ProductId,
            Changes = x.Changes
        }));
    }
}