using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using WarehouseManagementSystem;

namespace WarehouseManagementSystemTests;

public class WarehouseManagementServiceChangeCountTests
{
    private IWarehouseManagementService warehouseManagementService;

    public WarehouseManagementServiceChangeCountTests()
    {
        var provider = DependencyInjection.CreateServiceProvider();
        warehouseManagementService = provider.GetRequiredService<IWarehouseManagementService>();
    }
    
    [Fact]
    public void AssertIWarehouseManagementServiceHasChangeCountMethod()
    {
        Assert.NotNull(warehouseManagementService.GetType().GetMethod("ChangeCount"));
    }
    
    [Fact]
    public void AssertChangeCountMethodHasParameters()
    {
        Assert.NotEmpty(warehouseManagementService.GetType().GetMethod("ChangeCount").GetParameters());
    }
    
    [Fact]
    public void AssertChangeCountMethodHasTwoParameters()
    {
        MethodInfo method = warehouseManagementService.GetType().GetMethod("ChangeCount");
        
        Assert.Equal(2, method.GetParameters().Length);
    }
    
    [Fact]
    public void AssertChangeCountMethodFirstParamTypeEqualInt()
    {
        MethodInfo method = warehouseManagementService.GetType().GetMethod("ChangeCount");
        
        Assert.Equal(typeof(int), method.GetParameters().First().ParameterType);
    }
    
    [Fact]
    public void AssertChangeCountMethodSecondParamTypeEqualInt()
    {
        MethodInfo method = warehouseManagementService.GetType().GetMethod("ChangeCount");
        
        Assert.Equal(typeof(int), method.GetParameters().Last().ParameterType);
    }
    
    [Fact]
    public void AssertChangeCountNotThrowNotImplementedException()
    {
         Exception ex = Record.Exception(()=>warehouseManagementService.ChangeCount(1,1));
         Assert.IsNotType<NotImplementedException>(ex);
    }
}