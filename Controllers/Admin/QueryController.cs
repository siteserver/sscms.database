using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSCMS.Configuration;
using SSCMS.Services;

namespace SSCMS.Database.Controllers.Admin
{
    [Authorize(Roles = Types.Roles.Administrator)]
    [Route(Constants.ApiAdminPrefix)]
    public partial class QueryController : ControllerBase
    {
        private const string Route = "database/query";
        private const string RouteExport = "database/query/actions/export";

        private readonly IAuthManager _authManager;
        private readonly IPathManager _pathManager;
        private readonly Abstractions.IDatabaseManager _databaseManager;

        public QueryController(IAuthManager authManager, IPathManager pathManager, Abstractions.IDatabaseManager databaseManager)
        {
            _authManager = authManager;
            _pathManager = pathManager;
            _databaseManager = databaseManager;
        }

        public class QueryRequest
        {
            public string Query { get; set; }
        }

        public class QueryResult
        {
            public List<dynamic> Results { get; set; }
            public List<string> Properties { get; set; }
            public int Count { get; set; }
        }
    }
}
