using System.Collections;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using WarehouseManagementSystem;

namespace WarehouseManagementSystemTests;

public class WarehouseManagementServiceGetHistoryTests
{
    private IWarehouseManagementService warehouseManagementService;

    public WarehouseManagementServiceGetHistoryTests()
    {
        var provider = DependencyInjection.CreateServiceProvider();
        warehouseManagementService = provider.GetRequiredService<IWarehouseManagementService>();
    }
    
    [Fact]
    public void AssertIWarehouseManagementServiceHasGetHistoryMethod()
    {
        Assert.NotNull(warehouseManagementService.GetType().GetMethod("GetHistory"));
    }
    
    [Fact]
    public void AssertGetHistoryHasReturnType()
    {
        MethodInfo method = warehouseManagementService.GetType().GetMethod("GetHistory");

        Assert.NotEqual(typeof(void), method.ReturnType);
    }
    
    [Fact]
    public void AssertGetHistoryDoesNotThrowNotImplementedException()
    {
        Assert.IsNotType<NotImplementedException>(Record.Exception(() => warehouseManagementService.GetHistory()));
    }
    
    [Fact]
    public void AssertGetHistoryReturnsNotNull()
    {
        Assert.NotNull(warehouseManagementService.GetHistory());
    }
    
    [Fact]
    public void AssertGetHistoryReturnsList()
    {
        Assert.IsAssignableFrom<IEnumerable>(warehouseManagementService.GetHistory());
    }
}