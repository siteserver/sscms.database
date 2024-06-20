using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Database.Core;
using SSCMS.Utils;

namespace SSCMS.Database.Controllers.Admin
{
    public partial class QueryController
    {
        [HttpPost, Route(Route)]
        public async Task<ActionResult<QueryResult>> Query([FromBody] QueryRequest request)
        {
            if (!await _authManager.HasAppPermissionsAsync(DatabaseManager.PermissionsQuery))
            {
                return Unauthorized();
            }

            if (!StringUtils.StartsWithIgnoreCase(request.Query, "SELECT"))
            {
                return this.Error("请输入有效的查询SQL语句！");
            }

            var results = await _databaseManager.QueryAsync(request.Query);
            List<string> properties = null;
            var count = 0;
            if (results != null)
            {
                count = results.Count;
                if (count > 0)
                {
                    var dataInfo = results.FirstOrDefault();
                    properties = _databaseManager.GetPropertyKeysForDynamic(dataInfo, true);
                }
            }

            return new QueryResult
            {
                Results = results,
                Properties = properties,
                Count = count
            };
        }
    }
}