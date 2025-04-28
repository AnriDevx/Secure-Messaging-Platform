using System;

namespace Sync.Common
{
    public static class Constants
    {
        public const string? DefaultCurrency = "USD";
        public const int MaxMessageLength = 1000;
        public static string? DefaultTimeZone => TimeZoneInfo.Utc.Id;
    }
}