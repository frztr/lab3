namespace WarehouseManagementSystem;

public interface IWarehouseManagementService
{
    IProduct AddProduct(string arg1);
    
    void ChangeCount(int arg1, int arg2);

    IEnumerable<IHistory> GetHistory();
}