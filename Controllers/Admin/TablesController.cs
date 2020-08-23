using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Configuration;
using SSCMS.Database.Core;
using SSCMS.Services;
using SSCMS.Utils;
using IDatabaseManager = SSCMS.Database.Abstractions.IDatabaseManager;

namespace SSCMS.Database.Controllers.Admin
{
    [Authorize(Roles = Types.Roles.Administrator)]
    [Route(Constants.ApiAdminPrefix)]
    public partial class TablesController : ControllerBase
    {
        private const string Route = "database/tables";

        private readonly IAuthManager _authManager;
        private readonly IDatabaseManager _databaseManager;

        public TablesController(IAuthManager authManager, IDatabaseManager databaseManager)
        {
            _authManager = authManager;
            _databaseManager = databaseManager;
        }

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