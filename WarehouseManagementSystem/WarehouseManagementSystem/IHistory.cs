namespace WarehouseManagementSystem;

public interface IHistory
{
    public int ProductId { get; set; }
    
    public int Changes { get; set; }
}