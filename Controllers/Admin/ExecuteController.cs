using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Configuration;
using SSCMS.Services;
using IDatabaseManager = SSCMS.Database.Abstractions.IDatabaseManager;

namespace SSCMS.Database.Controllers.Admin
{
    [Authorize(Roles = Types.Roles.Administrator)]
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

        public class ExecuteRequest
        {
            public string Execute { get; set; }
            public string SecurityKey { get; set; }
        }
    }
}