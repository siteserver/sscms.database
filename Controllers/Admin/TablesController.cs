using System.Collections.Generic;
using Datory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Configuration;
using SSCMS.Services;
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

        public class GetResult
        {
            public List<string> TableNames { get; set; }
        }

        public class PostRequest
        {
            public string TableName { get; set; }
        }

        public class PostResult
        {
            public List<TableColumn> TableColumns { get; set; }
            public int Count { get; set; }
        }
    }
}