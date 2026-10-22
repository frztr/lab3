using Microsoft.Extensions.DependencyInjection;
using WarehouseManagementSystem;

namespace WarehouseManagementSystemTests;

public class WarehouseManagementSystemTests
{
    [Fact]
    public void CorrectBuild()
    {
        
    }
    
    [Fact]
    public void CorrectDependencyInjectionServiceProviderCall()
    {
        DependencyInjection.CreateServiceProvider();
    }
    
    [Fact]
    public void AssertServiceProviderNotNull()
    {
        Assert.NotNull(DependencyInjection.CreateServiceProvider());
    }
    
    [Fact]
    public void AssertIWarehouseManagementServiceImplExists()
    {
        var provider = DependencyInjection.CreateServiceProvider();
        Assert.NotNull(provider.GetRequiredService<IWarehouseManagementService>());
    }
}