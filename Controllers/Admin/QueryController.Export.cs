using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Database.Core;
using SSCMS.Dto;
using SSCMS.Utils;

namespace SSCMS.Database.Controllers.Admin
{
    public partial class QueryController
    {
        [HttpPost, Route(RouteExport)]
        public async Task<ActionResult<StringResult>> Export([FromBody] QueryRequest request)
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
                    properties = _databaseManager.GetPropertyKeysForDynamic(dataInfo, false);
                }
            }

            var head = new List<string> { "编号" };
            foreach (var property in properties)
            {
                head.Add(property);
            }

            var rows = new List<List<string>>();
            var seq = 1;
            foreach (var result in results)
            {
                var data = (IDictionary<string, object>)result;

                var row = new List<string>
                {
                    seq++.ToString()
                };
                foreach (var property in properties)
                {
                    // var value = result.GetType().GetProperty(property).GetValue(result, null);
                    var value = data[property];
                    row.Add(value != null ? value.ToString() : string.Empty);
                }
                rows.Add(row);
            }

            var fileName = "数据库查询.xlsx";
            ExcelUtils.Write(_pathManager.GetTemporaryFilesPath(fileName), head, rows);
            var downloadUrl = _pathManager.GetTemporaryFilesUrl(fileName);

            return new StringResult
            {
                Value = downloadUrl
            };

            // return new StringResult
            // {
            //     Results = results,
            //     Properties = properties,
            //     Count = count
            // };
        }
    }
}