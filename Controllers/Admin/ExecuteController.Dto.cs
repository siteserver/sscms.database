namespace SSCMS.Database.Controllers.Admin
{
    public partial class ExecuteController
    {
        public class ExecuteRequest
        {
            public string Execute { get; set; }
            public string SecurityKey { get; set; }
        }
    }
}
