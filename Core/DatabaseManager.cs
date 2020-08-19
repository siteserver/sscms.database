using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Datory;
using Newtonsoft.Json.Linq;
using SSCMS.Services;
using SSCMS.Utils;

namespace SSCMS.Database.Core
{
    public class DatabaseManager : Abstractions.IDatabaseManager
    {
        public const string PermissionsTables = "database_tables";
        public const string PermissionsQuery = "database_query";
        public const string PermissionsExecute = "database_execute";

        private readonly ISettingsManager _settingsManager;

        public DatabaseManager(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
        }

        public async Task ExecuteAsync(string sqlString)
        {
            if (string.IsNullOrEmpty(sqlString)) return;

            using (var connection = _settingsManager.Database.GetConnection())
            {
                await connection.ExecuteAsync(sqlString);
            }
        }

        public async Task<List<string>> GetTableNamesAsync()
        {
            return await _settingsManager.Database.GetTableNamesAsync();
        }

        public async Task<List<TableColumn>> GetTableColumnsAsync(string tableName)
        {
            return await _settingsManager.Database.GetTableColumnsAsync(tableName);
        }

        public async Task<int> CountAsync(string tableName)
        {
            var repository = new Repository(_settingsManager.Database, tableName);
            return await repository.CountAsync();
        }

        public async Task<List<dynamic>> QueryAsync(string query)
        {
            using (var connection = _settingsManager.Database.GetConnection())
            {
                return (await connection.QueryAsync(query)).ToList();
            }
        }

        public List<string> GetPropertyKeysForDynamic(dynamic dynamicToGetPropertiesFor)
        {
            var jObject = (JObject) JToken.FromObject(dynamicToGetPropertiesFor);
            var values = jObject.ToObject<Dictionary<string, object>>();
            var toReturn = new List<string>();
            foreach (var key in values.Keys)
            {
                if (StringUtils.EqualsIgnoreCase(key, "id"))
                {
                    toReturn.Add("id");
                }
                else if (StringUtils.EqualsIgnoreCase(key, "guid"))
                {
                    toReturn.Add("guid");
                }
                else
                {
                    toReturn.Add(StringUtils.ToCamelCase(key));
                }
            }

            return toReturn;
        }
    }
}
