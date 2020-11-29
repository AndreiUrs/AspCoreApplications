using AutoMapper;
using DataLibrary.Data;
using DataLibrary.Db;
using Microsoft.Extensions.DependencyInjection;

namespace RazorPagesWebApplication
{
    public class Container
    {

        public static void Configure(IServiceCollection services)
        {
            services.AddSingleton(new ConnectionStringData()
            {
                SqlConnectionName = "Default"
            });
            services.AddSingleton<IDataAccess, SqlDb>();
            services.AddSingleton<IFoodData, FoodData>();
            services.AddSingleton<IOrderData, OrderData>();

            services.AddAutoMapper(typeof(Startup));
        }
    }
}