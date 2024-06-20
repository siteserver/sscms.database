using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Database.Core;

namespace SSCMS.Database.Controllers.Admin
{
    public partial class TablesController
    {
        [HttpGet, Route(Route)]
        public async Task<ActionResult<GetResult>> Get()
        {
            if (!await _authManager.HasAppPermissionsAsync(DatabaseManager.PermissionsTables))
            {
                return Unauthorized();
            }

            return new GetResult
            {
                TableNames = await _databaseManager.GetTableNamesAsync()
            };
        }
    }
}
