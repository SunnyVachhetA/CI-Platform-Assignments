namespace CIPlatform.Services.Utilities;
public static class StringContainsExtension
{
    public static bool ContainsCaseInsensitive(this string source, string substring)
    {
        return source?.IndexOf(substring, StringComparison.OrdinalIgnoreCase) > -1;
    }

    public static bool EqualsIgnoreCase(this string str1, string str2)
    {
        if (str1 == null || str2 == null)
        {
            return str1 == str2;
        }
        return str1.Equals(str2, StringComparison.OrdinalIgnoreCase);
    }
}