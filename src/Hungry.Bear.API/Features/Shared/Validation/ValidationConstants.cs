namespace Hungry.Bear.API.Features.Shared.Validation
{
    public static class ValidationConstants
    {
        public const string PhoneNumberValidatorRegEx =
            @"\s*(?:\+?(\d{1,3}))?([-. (]*(\d{3})[-. )]*)?((\d{3})[-. ]*(\d{2,4})(?:[-.x ]*(\d+))?)\s*";
    }
}
