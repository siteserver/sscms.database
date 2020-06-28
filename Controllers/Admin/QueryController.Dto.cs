using System.Collections.Generic;

namespace SSCMS.Database.Controllers.Admin
{
    public partial class QueryController
    {
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
