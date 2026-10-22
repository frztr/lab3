using System.Data;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using WarehouseManagementSystem;

namespace WarehouseManagementSystemTests;

public class WarehouseManagementServiceTests
{
    private IWarehouseManagementService warehouseManagementService;

    public WarehouseManagementServiceTests()
    {
        var provider = DependencyInjection.CreateServiceProvider();
        warehouseManagementService = provider.GetRequiredService<IWarehouseManagementService>();
    }

    [Fact]
    public void WarehouseManagementServiceHasHistoryAfterAddProductAndChangeCount()
    {
        IProduct product = warehouseManagementService.AddProduct("Milk");
        warehouseManagementService.ChangeCount(product.Id, 3);
        Assert.NotEmpty(warehouseManagementService.GetHistory());
    }
    
    [Fact]
    public void WarehouseManagementServiceHasTwoHistoryItemsAfterAddProductAndChangeCountTwice()
    {
        IProduct product = warehouseManagementService.AddProduct("Milk");
        warehouseManagementService.ChangeCount(product.Id, 3);
        warehouseManagementService.ChangeCount(product.Id, 5);
        Assert.Equal(2, warehouseManagementService.GetHistory().Count());
    }
    
    [Fact]
    public void WarehouseManagementServiceHistoryHasProductId()
    {
        IProduct product = warehouseManagementService.AddProduct("Milk");
        warehouseManagementService.ChangeCount(product.Id, 3);
        Assert.NotNull(((dynamic)warehouseManagementService.GetHistory().First()).ProductId);
    }
    
    [Fact]
    public void WarehouseManagementServiceHistoryHasChangesField()
    {
        IProduct product = warehouseManagementService.AddProduct("Milk");
        warehouseManagementService.ChangeCount(product.Id, 3);
        TypeInfo typeInfo = warehouseManagementService.GetHistory().First().GetType().GetTypeInfo();
        Assert.Contains("Changes",typeInfo.GetProperties().Select(x=>x.Name));
    }
    
    [Fact]
    public void WarehouseManagementServiceHistoryChangesNotNull()
    {
        IProduct product = warehouseManagementService.AddProduct("Milk");
        warehouseManagementService.ChangeCount(product.Id, 3);
        Assert.NotNull(warehouseManagementService.GetHistory().First().Changes);
    }
    
    [Fact]
    public void WarehouseManagementServiceHistoryChangesHasIntType()
    {
        IProduct product = warehouseManagementService.AddProduct("Milk");
        warehouseManagementService.ChangeCount(product.Id, 3);
        Assert.IsType<int>(warehouseManagementService.GetHistory().First().Changes);
    }
    
    [Fact]
    public void WarehouseManagementServiceHistoryChangesEqualsThree()
    {
        IProduct product = warehouseManagementService.AddProduct("Milk");
        warehouseManagementService.ChangeCount(product.Id, 3);
        Assert.Equal(3, warehouseManagementService.GetHistory().First().Changes);
    }
    
    [Fact]
    public void WarehouseManagementServiceHistoryChangesEqualsFour()
    {
        IProduct product = warehouseManagementService.AddProduct("Milk");
        warehouseManagementService.ChangeCount(product.Id, 4);
        Assert.Equal(4, warehouseManagementService.GetHistory().First().Changes);
    }
    
    [Fact]
    public void WarehouseManagementServiceHistoryProductIdNotEqualZero()
    {
        IProduct product = warehouseManagementService.AddProduct("Milk");
        warehouseManagementService.ChangeCount(product.Id, 4);
        Assert.NotEqual(0, warehouseManagementService.GetHistory().First().ProductId);
    }
    
    [Fact]
    public void WarehouseManagementServiceHistoryProductIdsNotEqual()
    {
        IProduct milk = warehouseManagementService.AddProduct("Milk");
        IProduct bread = warehouseManagementService.AddProduct("Bread");
        warehouseManagementService.ChangeCount(milk.Id, 4);
        warehouseManagementService.ChangeCount(bread.Id, 5);
        Assert.NotEqual(warehouseManagementService.GetHistory().First().ProductId,
            warehouseManagementService.GetHistory().Last().ProductId);
    }
    
    [Fact]
    public void WarehouseManagementServiceHistoryProductIdNotEqualLastValue()
    {
        IProduct product = warehouseManagementService.AddProduct("Milk");
        warehouseManagementService.ChangeCount(product.Id, 4);
        ArgumentException ex = 
            Assert.Throws<ArgumentException>(() => warehouseManagementService.ChangeCount(product.Id, 4));
        Assert.Equal("Новое значение должно отличаться от предыдущего.", ex.Message);
    }
    
    [Fact]
    public void WarehouseManagementServiceHistoriesHasDifferentAccounting()
    {
        IProduct milk = warehouseManagementService.AddProduct("Milk");
        IProduct bread = warehouseManagementService.AddProduct("Bread");
        warehouseManagementService.ChangeCount(milk.Id, 4);
        Exception? ex = Record.Exception(() => warehouseManagementService.ChangeCount(bread.Id, 4));
        Assert.Null(ex);
    }
    
    [Fact]
    public void WarehouseManagementServiceHistoryLastHistoryChangesEqualMinusTwo()
    {
        IProduct product = warehouseManagementService.AddProduct("Milk");
        warehouseManagementService.ChangeCount(product.Id, 4);
        warehouseManagementService.ChangeCount(product.Id, 2);
        IHistory history = warehouseManagementService.GetHistory().Last();
        Assert.Equal(-2, history.Changes);
    }
    
    [Fact]
    public void WarehouseManagementServiceCannotAddProductThatAlreadyExists()
    {
        warehouseManagementService.AddProduct("Milk");
        ArgumentException ex = Assert.Throws<ArgumentException>(() => warehouseManagementService.AddProduct("Milk"));
        Assert.Equal("Товар с именем Milk уже существует", ex.Message);
    }
    
    [Fact]
    public void WarehouseManagementServiceCannotAddProductThatAlreadyExistsForBread()
    {
        warehouseManagementService.AddProduct("Bread");
        ArgumentException ex = Assert.Throws<ArgumentException>(() => warehouseManagementService.AddProduct("Bread"));
        Assert.Equal("Товар с именем Bread уже существует", ex.Message);
    }
    
    [Fact]
    public void WarehouseManagementServiceCannotChangeCountForId101IfNotExists()
    {
        ArgumentException ex = Assert.Throws<ArgumentException>(() => warehouseManagementService.ChangeCount(101, 1));
        Assert.Equal("Товар с id = 101 не существует", ex.Message);
    }

    [Fact]
    public void WarehouseManagementServiceCannotChangeCountForNegativeValues()
    {
        IProduct milk = warehouseManagementService.AddProduct("Milk");
        warehouseManagementService.ChangeCount(milk.Id, 1);
        Exception ex = Assert.Throws<ArgumentException>(() => warehouseManagementService.ChangeCount(milk.Id, -1));
        Assert.Equal("Новое значение не может быть отрицательным", ex.Message);
    }
    
    [Fact]
    public void WarehouseManagementServiceAllowsReturnToEarlierValue()
    {
        IProduct product = warehouseManagementService.AddProduct("Milk");
        warehouseManagementService.ChangeCount(product.Id, 4);
        warehouseManagementService.ChangeCount(product.Id, 2);
        warehouseManagementService.ChangeCount(product.Id, 4);
        Assert.Equal(2, warehouseManagementService.GetHistory().Last().Changes);
    }
    
    [Fact]
    public void WarehouseManagementServiceHistorySnapshotDoesNotGrow()
    {
        IProduct product = warehouseManagementService.AddProduct("Milk");
        IEnumerable<IHistory> history = warehouseManagementService.GetHistory();
        warehouseManagementService.ChangeCount(product.Id, 3);
        Assert.Empty(history);
    }
    
    [Fact]
    public void WarehouseManagementServiceKeepsAcceptedName()
    {
        IProduct milk = warehouseManagementService.AddProduct("Milk");
        milk.Name = "Kefir";
        ArgumentException ex = Assert.Throws<ArgumentException>(
            () => warehouseManagementService.AddProduct("Milk"));
        Assert.Equal("Товар с именем Milk уже существует", ex.Message);
    }
    
    [Fact]
    public void WarehouseManagementServiceHistoryRowStaysIntact()
    {
        IProduct product = warehouseManagementService.AddProduct("Milk");
        warehouseManagementService.ChangeCount(product.Id, 4);
        warehouseManagementService.GetHistory().First().Changes = 100;
        Assert.Equal(4, warehouseManagementService.GetHistory().First().Changes);
    }
}