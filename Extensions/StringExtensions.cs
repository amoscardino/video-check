namespace VideoCheck.Extensions;

public static class StringExtensions
{
    public static string Truncate(this string? str, int length)
    {
        if (string.IsNullOrWhiteSpace(str))
            return string.Empty;

        if (str.Length > length)
            return str.Substring(0, length);

        return str;
    }
}