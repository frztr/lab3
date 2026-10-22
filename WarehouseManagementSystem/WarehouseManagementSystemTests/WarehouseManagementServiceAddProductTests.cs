using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using WarehouseManagementSystem;

namespace WarehouseManagementSystemTests;

public class WarehouseManagementServiceAddProductTests
{
    private IWarehouseManagementService warehouseManagementService;

    public WarehouseManagementServiceAddProductTests()
    {
        var provider = DependencyInjection.CreateServiceProvider();
        warehouseManagementService = provider.GetRequiredService<IWarehouseManagementService>();
    }
    
    [Fact]
    public void AssertIWarehouseManagementServiceHasAddProductMethod()
    {
        Assert.NotNull(warehouseManagementService.GetType().GetMethod("AddProduct"));
    }
    
    [Fact]
    public void AssertAddProductMethodHasParameters()
    {
        Assert.NotEmpty(warehouseManagementService.GetType().GetMethod("AddProduct").GetParameters());
    }
    
    [Fact]
    public void AssertAddProductMethodHasOneStringParameter()
    {
        MethodInfo method = warehouseManagementService.GetType().GetMethod("AddProduct");
        
        Assert.Single(method.GetParameters());
        Assert.Equal(typeof(string), method.GetParameters().First().ParameterType);
    }
    
    [Fact]
    public void AssertAddProductNotThrowException()
    {
        warehouseManagementService.AddProduct("TestProduct");
    }
    
    [Fact]
    public void AssertAddProductHasReturnType()
    {
        MethodInfo method = warehouseManagementService.GetType().GetMethod("AddProduct");

        Assert.NotEqual(typeof(void), method.ReturnType);
    }
    
    [Fact]
    public void AssertAddProductHasProductContract()
    {
        MethodInfo method = warehouseManagementService.GetType().GetMethod("AddProduct");

        Type methodReturnType = method.ReturnType;
        PropertyInfo[] props = methodReturnType.GetProperties();
        Assert.Equal(3, props.Length);
        Assert.Equal("Id",props[0].Name);
        Assert.Equal(typeof(int),props[0].PropertyType);
        Assert.Equal("Name",props[1].Name);
        Assert.Equal(typeof(string),props[1].PropertyType);
        Assert.Equal("Count",props[2].Name);
        Assert.Equal(typeof(int),props[2].PropertyType);
    }

    [Fact]
    public void AssertAddProductNotNull()
    {
        Assert.NotNull(warehouseManagementService.AddProduct("TestProduct"));
    }
    
    [Fact]
    public void AssertProductNameNotNullOrWhiteSpace()
    {
        IProduct product = warehouseManagementService.AddProduct("TestProduct");
        Assert.NotNull(product.Name);
        Assert.NotEqual("",product.Name);
    }
    
    [Fact]
    public void AssertProductNameEqualsMethodParam()
    {
        string productName = "TestProduct";
        IProduct product = warehouseManagementService.AddProduct(productName);
        Assert.Equal(productName,product.Name);
    }
    
    [Fact]
    public void AssertProductNameEqualsMethodParam2()
    {
        string productName = "TestProduct2";
        IProduct product = warehouseManagementService.AddProduct(productName);
        Assert.Equal(productName,product.Name);
    }
    
    [Fact]
    public void AssertAddProductCountNotZero()
    {
        IProduct product = warehouseManagementService.AddProduct("TestProduct");
        Assert.NotEqual(0,product.Id);
    }
    
    [Fact]
    public void AssertAddProductIdDoesNotRepeat()
    {
        IProduct product = warehouseManagementService.AddProduct("TestProduct");
        IProduct product2 = warehouseManagementService.AddProduct("TestProduct2");
        Assert.NotEqual(product2.Id,product.Id);
    }
}