namespace Hungry.Bear.API.Configuration.Auth
{
    public static class PolicyNames
    {
        public static class Runs
        {
            public const string Read = nameof(Runs) + "." + nameof(Read);

            public const string Create = nameof(Runs) + "." + nameof(Create);

            public const string Update = nameof(Runs) + "." + nameof(Update);

            public const string Delete = nameof(Runs) + "." + nameof(Delete);
        }

        public static class Users
        {
            public const string Read = nameof(Users) + "." + nameof(Read);

            public const string ReadSingle = nameof(Users) + "." + nameof(ReadSingle);

            public const string Update = nameof(Users) + "." + nameof(Update);

            public const string Delete = nameof(Users) + "." + nameof(Delete);
        }

        public static class Report
        {
            public const string AverageSpeedAndDistance = nameof(Report) + "." + nameof(AverageSpeedAndDistance);
        }
    }
}
