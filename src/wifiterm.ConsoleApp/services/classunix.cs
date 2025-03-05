using System;
using System.Diagnostics;

namespace wifiterm.services.utils
{
    public static class WifiHelper
    {
        public static string GetWifiInterface()
        {
            return CMD.RunCommand("networksetup -listallhardwareports | awk '/Wi-Fi/{getline; print $2}'");
        }
    }

    public static class CMD
    {
        public static string RunCommand(string command)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "/bin/bash",
                Arguments = $"-c \"{command}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true, // Captures errors as well
                UseShellExecute = false,
                CreateNoWindow = true // Prevents opening a terminal window
            };

            using Process process = Process.Start(psi);
            process.WaitForExit();

            string output = process.StandardOutput.ReadToEnd().Trim();
            string error = process.StandardError.ReadToEnd().Trim();

            if (!string.IsNullOrEmpty(error))
                throw new Exception($"Error executing command: {error}");

            return output;
        }
    }

    public static class WifiStatus
    {
        public static string GetStatus()
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "/usr/sbin/networksetup",
                    Arguments = "-getairportpower en0",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using Process process = new Process { StartInfo = psi };
                process.Start();
                string output = process.StandardOutput.ReadToEnd().Trim();
                process.WaitForExit();

                return output.Contains("On") ? "Enabled" : "Disabled";
            }
            catch (Exception ex)
            {
                return $"Error retrieving Wi-Fi status: {ex.Message}";
            }
        }
    }
}