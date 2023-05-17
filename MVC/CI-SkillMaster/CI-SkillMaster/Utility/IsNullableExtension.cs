namespace CI_SkillMaster.Utility;

public static class IsNullableExtension
{
    public static bool IsNullableType(this Type type)
    {
        return Nullable.GetUnderlyingType(type) != null;
    }
}
