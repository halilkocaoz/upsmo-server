namespace UpMo.Common.Response
{
    public static class ResponseMessage
    {
        #region private consts
        private const string notFoundStarting = "Not Found";
        #endregion

        #region
        public static readonly string Forbid = "Forbid";
        public static readonly string Internal = "Internal Server Error";
        public static readonly string InvalidCredentials = "Invalid Credentials";
        #endregion


        #region Not Found
        public static readonly string NotFoundOrganization = $"{notFoundStarting} Organization";
        public static readonly string NotFoundManager = $"{notFoundStarting} Manager";

        public static readonly string NotFoundMonitor = $"{notFoundStarting} Monitor";
        public static readonly string NotFoundUser = $"{notFoundStarting} User";
        #endregion

        #region Bad Request
        public static readonly string AlreadyManager = "Given user is already manager at the given organization.";
        public static readonly string MonitorNotPost = "Monitor method is not POST.";
        #endregion
    }
}