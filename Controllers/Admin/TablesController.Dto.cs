using System.Collections.Generic;
using Datory;

namespace SSCMS.Database.Controllers.Admin
{
    public partial class TablesController
    {
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
