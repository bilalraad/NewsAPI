namespace NewsAPI.Extensions;

public static class EnumExtension
{
    public static string Name(this Enum enumValue)
    {
        return Enum.GetName(enumValue.GetType(), enumValue)!;
    }
}
