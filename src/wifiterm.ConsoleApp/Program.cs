using System;
using wifiterm.services.utils;

class WifiController
{
    static void Main(string[] args)
    {
        if (args.Length == 0 || args[0] == "--help")
        {
            ShowHelp();
            return;
        }

        string wifiInterface = WifiHelper.GetWifiInterface();
        if (string.IsNullOrEmpty(wifiInterface))
        {
            Console.WriteLine("Error: Wi-Fi interface not found.");
            return;
        }

        string action = args[0].ToLower();
        if (action != "on" && action != "off" && action != "status")
        {
            Console.WriteLine("Invalid command. Use '--help' for usage information.");
            return;
        }

        try
        {
            if (action == "status")
            {
                Console.WriteLine($"Fetching Wi-Fi status...{WifiStatus.GetStatus()}");
            }

            CMD.RunCommand($"networksetup -setairportpower {wifiInterface} {action}");
            Console.WriteLine($"Wi-Fi {action} successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error changing Wi-Fi state: {ex.Message}");
        }
    }

    static void ShowHelp()
    {
        Console.WriteLine(@"wifiterm CLI - v1.0.0
======================
Developer: Romerito Morais
Github: https://github.com/ROM-mm
Usage:
    wifiterm [command] [options]

Commands:
    on      Turn Wi-Fi on
    off     Turn Wi-Fi off
    status  Show Wi-Fi status

Examples:
    wifiterm on
    wifiterm off
    wifiterm status
");
    }
}
