using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace SupplierPO.Tools.Extensions;

public static class StringExtensions
{

    public static Boolean IsNullOrWhitespace([NotNullWhen(false)] this String? str)
    {
        return String.IsNullOrWhiteSpace(str);
    }

    public static Boolean IsNotNullOrWhitespace([NotNullWhen(true)] this String? str)
    {
        return !String.IsNullOrWhiteSpace(str);
    }

    public static String? GetRegex(this String str, String pattern)
    {
        Regex regex = new(pattern, RegexOptions.IgnoreCase);
        Match match = regex.Match(str);

        if (!match.Success) return null;

        return match.Value;
    }

    public static String? NormalizePhoneNumber(this String? value)
    {
        if (String.IsNullOrEmpty(value)) return null;

        value = new String(value.Where(Char.IsDigit).ToArray());
        if (String.IsNullOrEmpty(value)) return null;

        return "+" + value;
    }
}