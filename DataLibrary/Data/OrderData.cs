using Dapper;
using DataLibrary.Db;
using DataLibrary.Models;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DataLibrary.Data
{
    public class OrderData : IOrderData
    {
        private readonly IDataAccess _dataAccess;
        private readonly ConnectionStringData _connectionStringData;

        public OrderData(IDataAccess dataAccess, ConnectionStringData connectionStringData)
        {
            _dataAccess = dataAccess;
            _connectionStringData = connectionStringData;
        }

        public async Task<OrderModel> GetOrderById(int orderId)
        {
            var result = await _dataAccess.LoadData<OrderModel, dynamic>(storedProcedure: "dbo.spOrders_GetById",
                                                                   parameters: new { Id = orderId },
                                                                   connectionStringName: _connectionStringData.SqlConnectionName);
            return result.FirstOrDefault();
        }

        public async Task<int> CreateOrder(OrderModel order)
        {
            var orderParameters = CreateOrderParameters(order);

            await _dataAccess.SaveData(storedProcedure: "dbo.spOrders_Insert",
                                       parameters: orderParameters,
                                       connectionStringName: _connectionStringData.SqlConnectionName);
            return orderParameters.Get<int>("Id");
        }

        public Task<int> UpdateOrder(int orderId, string orderName)
        {
            return _dataAccess.SaveData(storedProcedure: "dbo.spOrders_UpdateName",
                                   parameters: new { Id = orderId, OrderName = orderName },
                                   connectionStringName: _connectionStringData.SqlConnectionName);
        }

        public Task<int> DeleteOrder(int orderId)
        {
            return _dataAccess.SaveData(storedProcedure: "dbo.spOrders_Delete",
                                  parameters: new { Id = orderId },
                                  connectionStringName: _connectionStringData.SqlConnectionName);
        }

        private static DynamicParameters CreateOrderParameters(OrderModel order)
        {
            var parameters = new DynamicParameters();

            parameters.Add("OrderName", order.OrderName);
            parameters.Add("OrderDate", order.OrderDate);
            parameters.Add("FoodId", order.FoodId);
            parameters.Add("Quantity", order.Quantity);
            parameters.Add("Total", order.Total);
            parameters.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            return parameters;
        }
    }
}
