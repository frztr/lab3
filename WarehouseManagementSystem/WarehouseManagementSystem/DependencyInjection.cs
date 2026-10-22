using Microsoft.Extensions.DependencyInjection;

namespace WarehouseManagementSystem;

public class DependencyInjection
{
    public static ServiceProvider CreateServiceProvider()
    {
        return new ServiceCollection()
            .AddSingleton<IWarehouseManagementService, WarehouseManagementService>()
            .BuildServiceProvider();
    }
}