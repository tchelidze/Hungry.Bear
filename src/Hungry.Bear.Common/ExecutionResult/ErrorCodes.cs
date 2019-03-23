namespace Hungry.Bear.Common.ExecutionResult
{
    public static class ErrorCodes
    {
        public const string VALIDATION_ERROR = nameof(VALIDATION_ERROR);

        public const string UNHANDLED_EXCEPTION = nameof(UNHANDLED_EXCEPTION);

        public const string USER_HAS_OVERLAPPING_RUN = nameof(USER_HAS_OVERLAPPING_RUN);

        public const string USER_ISNT_ALLOWED_TO_DELETE_ITS_ACCOUNT = nameof(USER_ISNT_ALLOWED_TO_DELETE_ITS_ACCOUNT);

        public const string UNABLE_TO_CONNECT_WEATHER_API = nameof(UNABLE_TO_CONNECT_WEATHER_API);

        public const string RUN_TIME_IS_TOO_HIGH = nameof(RUN_TIME_IS_TOO_HIGH);
    }
}
