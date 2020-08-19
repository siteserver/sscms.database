using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Database.Core;
using SSCMS.Dto;
using SSCMS.Extensions;
using SSCMS.Services;
using SSCMS.Utils;
using IDatabaseManager = SSCMS.Database.Abstractions.IDatabaseManager;

namespace SSCMS.Database.Controllers.Admin
{
    [Authorize(Roles = AuthTypes.Roles.Administrator)]
    [Route(Constants.ApiAdminPrefix)]
    public partial class ExecuteController : ControllerBase
    {
        private const string Route = "database/execute";

        private readonly ISettingsManager _settingsManager;
        private readonly IAuthManager _authManager;
        private readonly IDatabaseManager _databaseManager;

        public ExecuteController(ISettingsManager settingsManager, IAuthManager authManager, IDatabaseManager databaseManager)
        {
            _settingsManager = settingsManager;
            _authManager = authManager;
            _databaseManager = databaseManager;
        }

        [HttpPost, Route(Route)]
        public async Task<ActionResult<BoolResult>> Execute([FromBody] ExecuteRequest request)
        {
            if (!await _authManager.HasAppPermissionsAsync(DatabaseManager.PermissionsExecute))
            {
                return Unauthorized();
            }

            if (request.SecurityKey != _settingsManager.SecurityKey)
            {
                return this.Error("SecurityKey 输入错误！");
            }

            await _databaseManager.ExecuteAsync(request.Execute);

            return new BoolResult
            {
                Value = true
            };
        }
    }
}