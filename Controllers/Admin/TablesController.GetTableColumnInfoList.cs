using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Database.Core;

namespace SSCMS.Database.Controllers.Admin
{
    public partial class TablesController
    {
        [HttpPost, Route(Route)]
        public async Task<ActionResult<PostResult>> GetTableColumnInfoList([FromBody] PostRequest request)
        {
            if (!await _authManager.HasAppPermissionsAsync(DatabaseManager.PermissionsTables))
            {
                return Unauthorized();
            }

            return new PostResult
            {
                TableColumns = await _databaseManager.GetTableColumnsAsync(request.TableName),
                Count = await _databaseManager.CountAsync(request.TableName)
            };
        }
    }
}
