using DataLibrary.Data;
using DataLibrary.Db;
using Microsoft.Extensions.DependencyInjection;

namespace WebApiApplication
{
    public static class DiContainer
    {
        public static IServiceCollection AddContainer(this IServiceCollection services)
        {
            services.AddSingleton(new ConnectionStringData()
            {
                SqlConnectionName = "Default"
            });
            services.AddSingleton<IDataAccess, SqlDb>();
            services.AddSingleton<IFoodData, FoodData>();
            services.AddSingleton<IOrderData, OrderData>();

            return services;
        }
    }
}
