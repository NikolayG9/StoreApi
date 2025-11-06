namespace Store.Application.Helpers
{
    public static class ValidationHelper
    {
        public static string GetRequiredMessage(string fieldName)
        {
            return $"{fieldName} is required";
        }

        public static string GetMaxLengthMessage(string fieldName, int maxLength)
        {
            return $"{fieldName} must be under {maxLength} chars";
        }
    }
}
