using DataLibrary.Db;
using DataLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLibrary.Data
{
    public class FoodData : IFoodData
    {
        private readonly IDataAccess _dataAccess;
        private readonly ConnectionStringData _connectionStringData;

        public FoodData(IDataAccess dataAccess, ConnectionStringData connectionStringData)
        {
            _dataAccess = dataAccess;
            _connectionStringData = connectionStringData;
        }

        public Task<List<FoodModel>> GetFood()
        {
            return _dataAccess.LoadData<FoodModel, dynamic>(storedProcedure: "dbo.spFood_All",
                                                            parameters: new { },
                                                            connectionStringName: _connectionStringData.SqlConnectionName);
        }
    }
}
