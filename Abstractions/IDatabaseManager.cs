using System.Collections.Generic;
using System.Threading.Tasks;
using Datory;

namespace SSCMS.Database.Abstractions
{
    public interface IDatabaseManager
    {
        Task ExecuteAsync(string sqlString);

        Task<List<string>> GetTableNamesAsync();

        Task<List<TableColumn>> GetTableColumnsAsync(string tableName);

        Task<int> CountAsync(string tableName);

        Task<List<dynamic>> QueryAsync(string query);

        List<string> GetPropertyKeysForDynamic(dynamic dynamicToGetPropertiesFor, bool toCamelCase);
    }
}
