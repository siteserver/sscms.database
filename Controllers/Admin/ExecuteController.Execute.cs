using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Database.Core;
using SSCMS.Dto;
using SSCMS.Utils;

namespace SSCMS.Database.Controllers.Admin
{
    public partial class ExecuteController
    {
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
