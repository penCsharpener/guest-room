using System;

namespace GuestRoom.Api.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string text)
        {
            return string.IsNullOrEmpty(text);
        }

        public static bool IsNotNullOrEmpty(this string text)
        {
            return !string.IsNullOrEmpty(text);
        }

        public static string ToEmailForLogging(this string email)
        {
            if (!email.Contains('@'))
            {
                return email;
            }

            var emailParts = email.Split('@', StringSplitOptions.RemoveEmptyEntries);

            if (emailParts.Length == 1)
            {
                return email;
            }

            var beforeAt = emailParts[0];
            var domainName = emailParts[1];

            var beforeAtHalfLength = (int) Math.Floor(beforeAt.Length / (double) 2);
            beforeAtHalfLength = beforeAtHalfLength == 0 ? 1 : beforeAtHalfLength;
            var shortenedBeforeAt = beforeAt.Substring(0, beforeAtHalfLength);

            var domainNameHalfLength = (int) Math.Floor(domainName.Length / (double) 2);
            domainNameHalfLength = domainNameHalfLength == 0 ? 1 : domainNameHalfLength;
            var shortenedDomainName = domainName.Substring(0, domainNameHalfLength);

            return $"{shortenedBeforeAt}...@{shortenedDomainName}...";
        }
    }
}