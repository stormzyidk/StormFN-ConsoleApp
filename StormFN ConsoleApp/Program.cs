using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Threading;

namespace Storm
{
    public class Program
    {
        static void Main() => new Program().Run().GetAwaiter().GetResult();
        public async Task Run()
        {


            //some code by ender
            string[] FNStuff = { "FortniteClient-Win64-Shipping_EAC.exe", "FortniteClient-Win64-Shipping_BE.exe", "FortniteLauncher.exe" };

            foreach (string procname in FNStuff)
            {
                var process = Process.GetProcessesByName(procname);
                foreach (var proc in process)
                {
                    proc.Kill();
                }
            }

            Console.Title = "StormFN Launcher";


            Storm.Log("Welcome to Storm!");
            Thread.Sleep(1000);

            string TempPath = Path.GetTempPath();
            var Path1 = "";
            var version = "1";

            var path1 = File.ReadAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Epic\\UnrealEngineLauncher\\LauncherInstalled.dat"));
            dynamic Json = JsonConvert.DeserializeObject(path1);

            foreach (var installion in Json.InstallationList)
            {
                if (installion.AppName == "Fortnite")
                {
                    Path1 = installion.InstallLocation.ToString() + "\\FortniteGame\\Binaries\\Win64";
                    version = installion.AppVersion.ToString().Split('-')[1];
                }
            }

            if (!File.Exists(Path1 + "\\FortniteClient-Win64-Shipping_EAC.old")) { }
            else
            {
                File.Move(Path1 + "\\FortniteClient-Win64-Shipping_EAC.exe", Path1 + "\\FortniteClient-Win64-Shipping_EAC.exe.old");
            }

            if (!File.Exists(Path1 + "\\FortniteClient-Win64-Shipping_BE.old")) { }
            else
            {
                File.Move(Path1 + "\\FortniteClient-Win64-Shipping_BE.exe", Path1 + "\\FortniteClient-Win64-Shipping_BE.exe.old");
            }

            WebClient webClient = new WebClient();

            await webClient.DownloadFileTaskAsync("https://cdn.discordapp.com/attachments/834565909401042954/841820732408332288/FortniteClient-Win64-Shipping_EAC.exe", TempPath + "\\FortniteClient-Win64-Shipping_EAC.exe");
            await webClient.DownloadFileTaskAsync("https://cdn.discordapp.com/attachments/834565909401042954/841820731876704266/FortniteClient-Win64-Shipping_BE.exe", TempPath + "\\FortniteClient-Win64-Shipping_BE.exe");
            if (!File.Exists(TempPath + "\\Storm.dll"))
            {
                await webClient.DownloadFileTaskAsync("https://cdn.discordapp.com/attachments/841307525860032542/842114024983232572/Aurora.Runtime.dll", TempPath + "\\Storm.dll");
            }

            if (!File.Exists(Path1 + "\\FortniteClient-Win64-Shipping_EAC.exe"))
            {
                File.Move(TempPath + "\\FortniteClient-Win64-Shipping_EAC.exe", Path1 + "\\FortniteClient-Win64-Shipping_EAC.exe");
            }
            else
            {
                File.Delete(Path1 + "\\FortniteClient-Win64-Shipping_EAC.exe");
                File.Move(TempPath + "\\FortniteClient-Win64-Shipping_EAC.exe", Path1 + "\\FortniteClient-Win64-Shipping_EAC.exe");
            }

            if (!File.Exists(Path1 + "\\FortniteClient-Win64-Shipping_BE.exe"))
            {
                File.Move(TempPath + "\\FortniteClient-Win64-Shipping_BE.exe", Path1 + "\\FortniteClient-Win64-Shipping_BE.exe");
            }
            else
            {
                File.Delete(Path1 + "\\FortniteClient-Win64-Shipping_BE.exe");
                File.Move(TempPath + "\\FortniteClient-Win64-Shipping_BE.exe", Path1 + "\\FortniteClient-Win64-Shipping_BE.exe");
            }

            if (!File.Exists(Path1 + "\\Storm.dll"))
            {
                File.Move(TempPath + "\\Storm.dll", Path1 + "\\Storm.dll");
            }
            else
            {
                File.Delete(Path1 + "\\Storm.dll");
                File.Move(TempPath + "\\Storm.dll", Path1 + "\\Storm.dll");
            }

            //by ender
            var Proc = new ProcessStartInfo();
            Proc.CreateNoWindow = true;
            Proc.FileName = "cmd.exe";
            Proc.Arguments = "/C start com.epicgames.launcher://apps/Fortnite?action=launch";
            Process.Start(Proc);


            Storm.Log("Storm working on v" + version);
            Thread.Sleep(1000);
            Storm.Log("Launching Fortnite...");

            Console.ReadLine();

        }
    }
}