using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace DNZ.SEOChecker
{
    internal static class Utility
    {
        internal static bool HasValue(this string value, bool ignoreWhiteSpace = true)
        {
            return !(ignoreWhiteSpace ? string.IsNullOrWhiteSpace(value) : string.IsNullOrEmpty(value));
        }

        internal static string TrimStopWords(this string text, string[] stopwords)
        {
            var puretext = string.Join(" ", text.GetWords());
            foreach (var item in stopwords)
            {
                var arr = Regex.Split(puretext, " " + item + " ", RegexOptions.IgnoreCase).Select(p => p.Trim());
                puretext = string.Join(" ", arr);
            }
            return puretext;
        }

        internal static string[] GetWords(this string text)
        {
            return text.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        }

        internal static bool ContainsIgroneCase(this string text, string str)
        {
            return text.Contains(str, StringComparison.OrdinalIgnoreCase);
        }

        internal static string GetSentence(this IEnumerable<string> arr)
        {
            return string.Join(", ", arr.Select(p => "\"" + p + "\""));
        }

        internal static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }

        internal static string Fa2En(this string str)
        {
            return str
                .Replace("۰", "0")
                .Replace("۱", "1")
                .Replace("۲", "2")
                .Replace("۳", "3")
                .Replace("۴", "4")
                .Replace("۵", "5")
                .Replace("۶", "6")
                .Replace("۷", "7")
                .Replace("۸", "8")
                .Replace("۹", "9")
                //iphone numeric
                .Replace("٠", "0")
                .Replace("١", "1")
                .Replace("٢", "2")
                .Replace("٣", "3")
                .Replace("٤", "4")
                .Replace("٥", "5")
                .Replace("٦", "6")
                .Replace("٧", "7")
                .Replace("٨", "8")
                .Replace("٩", "9");
        }

        internal static string FixPersianChars(this string str)
        {
            return str
                .Replace("ﮎ", "ک")
                .Replace("ﮏ", "ک")
                .Replace("ﮐ", "ک")
                .Replace("ﮑ", "ک")
                .Replace("ك", "ک")
                .Replace("ي", "ی")
                .Replace(" ", " ")
                .Replace("‌", " ")
                .Replace("ھ", "ه");
        }

        internal static string CleanString(this string str)
        {
            return str.Trim().FixPersianChars().Fa2En();
        }

        internal static IEnumerable<HtmlNode> GetElemetns(this HtmlNode node, params string[] xpath)
        {
            var list = new List<HtmlNode>();
            foreach (var item in xpath)
                list = list.Concat(node.Descendants(item)).ToList();
            return list;
        }

        internal static void Add(this List<SeoMessage> seoMessages, ErrorMessage error, bool isError, params object[] values)
        {
            seoMessages.Add(new SeoMessage { Message = string.Format(error.GetDescription(), values), IsError = isError });
        }

        internal static string GetDescription(this Enum value)
        {
            var attribute = value.GetType().GetField(value.ToString()).GetCustomAttribute<MessageInfoAttribute>();
            if (attribute == null)
                throw new InvalidOperationException($"Enum {value} does not contain 'MessageInfoAttribute'");
            return attribute.Description;
        }

        internal static string GetGroupName<TEnum>(this TEnum value) where TEnum : struct, Enum
        {
            var attribute = value.GetType().GetField(value.ToString()).GetCustomAttribute<MessageInfoAttribute>();
            if (attribute == null)
                throw new InvalidOperationException($"Enum {value} does not contain 'MessageInfoAttribute'");
            return attribute.GroupName;
        }

        internal static bool UrlIsLocal(this string url)
        {
            try
            {
                //Use absolute links. Not only will it make your on-site link navigation less prone to problems (like links to and from https pages), but if someone scrapes your content, you’ll get backlink juice out of it.
                return !new Uri(url).IsAbsoluteUri;
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch
            {
                return true;
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }
    }
}