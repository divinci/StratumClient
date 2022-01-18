using System;
using System.Runtime.CompilerServices;

namespace StratumClient.Extensions
{
    internal static class LoggerExtensions
    {
        public static void EnteringMethod(this ILogger logger, [CallerMemberName] string memberName = "")
        {
            logger.Debug($"Entering method {memberName}");
        }

        public static void EnteringMethod(this ILogger logger, object t1, [CallerMemberName] string memberName = "")
        {
            logger.Debug($"Entering method {memberName} params {t1}");
        }

        public static void EnteringMethod(this ILogger logger, object t1, object t2, [CallerMemberName] string memberName = "")
        {
            logger.Debug($"Entering method {memberName} params {t1} {t2}");
        }

        public static void EnteringMethod(this ILogger logger, object t1, object t2, object t3, [CallerMemberName] string memberName = "")
        {
            logger.Debug($"Entering method {memberName} params {t1} {t2} {t3}");
        }

        public static void CallingMethod(this ILogger logger, string methodName)
        {
            logger.Debug($"Calling method {methodName}");
        }

        public static void CallingMethod(this ILogger logger, string methodName, object t1)
        {
            logger.Debug($"Calling method {methodName} params {t1}");
        }

        public static void CallingMethod(this ILogger logger, string methodName, object t1, object t2)
        {
            logger.Debug($"Calling method {methodName} params {t1} {t2}");
        }

        public static void CallingMethod(this ILogger logger, string methodName, object t1, object t2, object t3)
        {
            logger.Debug($"Calling method {methodName} params {t1} {t2} {t3}");
        }

        public static void LogAndThrow(this ILogger logger, Exception e)
        {
            logger.Error(e, e.Message);
            throw e;
        }
    }
}