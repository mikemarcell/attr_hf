namespace TodoApp.WebApi.Auth
{
    public static class PolicyNames
    {
        public const string AdminOnlyPolicy = nameof(AdminOnlyPolicy);
        public const string OwnerPolicy = nameof(OwnerPolicy);
        public const string UserPolicy = nameof(UserPolicy);
    }
}
