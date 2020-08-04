using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace eOvjera.Common
{
    /// <summary>
    /// Application folder helper class.
    /// </summary>
    public static class ApplicationFolderHelper
    {
        /// <summary>
        /// Get application running folder.
        /// </summary>
        public static string GetApplicationRoot()
        {
            var exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = appPathMatcher.Match(exePath).Value;
            return appRoot;
        }

        /// <summary>
        /// Get application running folder on linux.
        /// </summary>
        public static string GetApplicationRootOnLinux()
        {
            var exePath = Path.GetDirectoryName(Environment.CurrentDirectory);
            var appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = appPathMatcher.Match(exePath).Value;
            return appRoot;
        }

        /// <summary>
        /// Get the file name with the full path to the application running folder.
        /// </summary>
        public static string ToApplicationRootPath(this string fileName)
        {
            var exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = appPathMatcher.Match(exePath).Value;
            return Path.Combine(appRoot, fileName);
        }

        /// <summary>
        /// Get application running folder.
        /// </summary>
        public static string GetApplicationFolder()
        {
            var exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*");
            var appRoot = appPathMatcher.Match(exePath).Value;
            return appRoot;
        }

        /// <summary>
        /// Get application running folder on linux.
        /// </summary>
        public static string GetApplicationFolderOnLinux()
        {
            var exePath = Path.GetDirectoryName(System.AppContext.BaseDirectory);
            var appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*");
            var appRoot = appPathMatcher.Match(exePath).Value;
            return appRoot;
        }

        /// <summary>
        /// Get the file name with the full path to the application running folder.
        /// </summary>
        public static string ToApplicationPath(this string fileName)
        {
            var exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*");
            var appRoot = appPathMatcher.Match(exePath).Value;
            return Path.Combine(appRoot, fileName);
        }
    }
}