using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace eOvjera.Common
{
    /// <summary>
    /// Application folder helper class.
    /// </summary>
    public static class ShellHelper
    {
        /// <summary>
        /// Execute bash schell script on linux.
        /// </summary>
        public static string Bash(this string cmd)
        {
            var escapedArgs = cmd.Replace("\"", "\\\"");
            
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    

                }
            };
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return result;
        }


        /// <summary>
        /// Open URL in browser..
        /// </summary>
        public static void OpenBrowser(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }


        /// <summary>
        /// Open specified word document.
        /// </summary>
        public static void OpenMicrosoftWord(string file)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "WINWORD.EXE";
            startInfo.Arguments = file;
            Process.Start(startInfo);
        }

        /// <summary>
        /// Execute windows command in command console.
        /// </summary>
        public static void ExecuteCommand(string command)
        {
            var processInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            // *** Redirect the output ***
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;

            var process = Process.Start(processInfo);

            // *** Read the streams ***
            process.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
                Console.WriteLine("output>>" + e.Data);
            process.BeginOutputReadLine();

            process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
                Console.WriteLine("error>>" + e.Data);
            process.BeginErrorReadLine();

            process.WaitForExit();

            Console.WriteLine("ExitCode: {0}" + process.ExitCode);
            process.Close();
        }

        // public static void ExecuteCommand2(string command)
        // {
        //     int ExitCode;
        //     ProcessStartInfo ProcessInfo;
        //     Process process;

        //     ProcessInfo = new ProcessStartInfo(Application.StartupPath + "\\txtmanipulator\\txtmanipulator.bat", command);
        //     ProcessInfo.CreateNoWindow = true;
        //     ProcessInfo.UseShellExecute = false;
        //     ProcessInfo.WorkingDirectory = Application.StartupPath + "\\txtmanipulator";
        //     // *** Redirect the output ***
        //     ProcessInfo.RedirectStandardError = true;
        //     ProcessInfo.RedirectStandardOutput = true;

        //     process = Process.Start(ProcessInfo);
        //     process.WaitForExit();

        //     // *** Read the streams ***
        //     string output = process.StandardOutput.ReadToEnd();
        //     string error = process.StandardError.ReadToEnd();

        //     ExitCode = process.ExitCode;

        //     MessageBox.Show("output>>" + (String.IsNullOrEmpty(output) ? "(none)" : output));
        //     MessageBox.Show("error>>" + (String.IsNullOrEmpty(error) ? "(none)" : error));
        //     MessageBox.Show("ExitCode: " + ExitCode.ToString(), "ExecuteCommand");
        //     process.Close();
        // }    
        
        /// <summary>
        /// Launch the legacy application with some options set.
        /// </summary>
        static void LaunchCommandLineApp()
        {
            // For the example.
            const string ex1 = "C:\\";
            const string ex2 = "C:\\Dir";

            // Use ProcessStartInfo class.
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = "dcm2jpg.exe";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = "-f j -o \"" + ex1 + "\" -z 1.0 -s y " + ex2;

            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using-statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }
            }
            catch
            {
                // Log error.
            }
        }
    }
}