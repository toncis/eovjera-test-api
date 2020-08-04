using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace eOvjera.Common
{
    /// <summary>
    /// Common - Application Assembly utils class.
    /// </summary>
    public static class AppAssemblyUtils 
    {
        /// <summary>
        /// Get linker timestamp in Utc from assembly.
        /// </summary>
        /// <remarks>
        /// Not to be used in .NET Core > v2.1
        /// Do use GetBuildDate method.
        /// </remarks>
        /// <param name="assembly"></param>
        /// <returns>The build datetime.</returns>
        public static DateTime GetLinkerTimestampUtc(Assembly assembly, TimeZoneInfo target = null)
        {
            var location = assembly.Location;
            return GetLinkerTimestampUtc(location);
        }

        /// <summary>
        /// Get Linker timestamp in utc from file path.
        /// </summary>
        /// <remarks>
        /// Not to be used in .NET Core > v2.1
        /// Do use GetBuildDate method.
        /// </remarks>
        /// <param name="filePath"></param>
        /// <returns>The build datetime.</returns>
        public static DateTime GetLinkerTimestampUtc(string filePath, TimeZoneInfo target = null)
        {
            const int peHeaderOffset = 60;
            const int linkerTimestampOffset = 8;
            var bytes = new byte[2048];

            using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                file.Read(bytes, 0, bytes.Length);
            }

            var headerPos = BitConverter.ToInt32(bytes, peHeaderOffset);
            var secondsSince1970 = BitConverter.ToInt32(bytes, headerPos + linkerTimestampOffset);
            var dt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            var linkTimeUtc = dt.AddSeconds(secondsSince1970);

            var tz = target ?? TimeZoneInfo.Local;
            var localTime = TimeZoneInfo.ConvertTimeFromUtc(linkTimeUtc, tz);

            return localTime;
        }

        /// <summary>
        /// Get msbuild timestamp in utc from assembly.
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns>The assembly build date.</returns>
        public static DateTime GetBuildDate(Assembly assembly, TimeZoneInfo target = null)
        {
            const string BuildVersionMetadataPrefix = "+build";

            var attribute = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
            if (attribute?.InformationalVersion != null)
            {
                var value = attribute.InformationalVersion;
                var index = value.IndexOf(BuildVersionMetadataPrefix);
                if (index > 0)
                {
                    value = value.Substring(index + BuildVersionMetadataPrefix.Length);
                    if (DateTime.TryParseExact(value, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out var result))
                    {
                        var tz = target ?? TimeZoneInfo.Local;
                        return TimeZoneInfo.ConvertTimeFromUtc(result, tz);
                        // return result;
                    }
                }
            }

            return default;
        }

        /// <summary>
        /// Get Net Core Version for older.
        /// </summary>
        /// <returns></returns>
        public static string GetNetCoreVersion()
        {
            var assembly = typeof(System.Runtime.GCSettings).GetTypeInfo().Assembly;
            var assemblyPath = assembly.CodeBase.Split(new[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries);
            int netCoreAppIndex = Array.IndexOf(assemblyPath, "Microsoft.NETCore.App");
            if (netCoreAppIndex > 0 && netCoreAppIndex < assemblyPath.Length - 2)
                return assemblyPath[netCoreAppIndex + 1];
            return String.Empty;
        }

        /// <summary>
        /// Return correct .NET Core product name like ".NET Core 2.1.0" instead of ".NET Core 4.6.26515.07" returning by RuntimeInformation.FrameworkDescription
        /// </summary>
        /// <returns>The .NET framework version description.</returns>
        public static string GetFrameworkDescription()
        {
            // ".NET Core 4.6.26515.07" => ".NET Core 2.1.0"
            var parts = System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var i = 0;
            for(; i < parts.Length; i++)
            {
                if (Char.IsDigit(parts[i][0]))
                {
                    break;
                }
            }
            var productName = String.Join(@" ", parts, 0, i);
            return String.Join(@" ", productName, "", GetNetCoreVersion());
        }
    }
}