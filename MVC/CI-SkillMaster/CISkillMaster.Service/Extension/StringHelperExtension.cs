namespace CISkillMaster.Services.Extension;

public static class StringHelperExtension
{
    public static bool ContainsCaseInsensitive(this string source, string substring) =>
         source?.IndexOf(substring, StringComparison.OrdinalIgnoreCase) > -1;
    
}
