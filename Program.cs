/* Создано в SharpDevelop.
 * Дата: 13.08.2019
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;

namespace ShowIPtoConsole
{
    class Program
    {
        public static string PrgTitle { get { return "ShowIP.exe"; } }

        static string DetectOption(string key)
        {
            switch (key)
            {
                case ("-h"): case ("--help"): { return "help"; }
                case ("-t"): case ("--txt"): { return "export txt"; }
                case ("--html"): { return "export html"; }
                case ("-f"): case ("--fb2"): { return "export fb2"; }
                default: { return "undefined"; }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="ip"></param>
        /// <returns>sukcessful or fail operation</returns>
        static bool CompleteCommand(string command, string ip)
        {
            if (command == "undefined") return false;
            if (command == "help")
            {
                Console.WriteLine("usage:");
                Console.WriteLine(PrgTitle);
                Console.WriteLine("\tShow ext. IP in this cmd");
                Console.WriteLine(PrgTitle + " -h || " + PrgTitle + " --help");
                Console.WriteLine("\tShow this help message");
                Console.WriteLine(PrgTitle + " -t || " + PrgTitle + " --txt");
                Console.WriteLine("\texport to ExternalIP.txt ");
                Console.WriteLine(PrgTitle + " -f || " + PrgTitle + " --fb2");
                Console.WriteLine("\tKaptain Obvious");
                Console.WriteLine(PrgTitle + " --html");
                Console.WriteLine("\tKaptain Obvious");
                return true;
            }
            if (command.Contains("export"))
            {
                if (ip == "undefined") return false;//FIXME a.b.c.d ip4 format
                var cse = CustomFileIO.SupportedExtensions.txt;
                switch (command.Split(' ')[1])
                {
                    case ("txt"): cse = CustomFileIO.SupportedExtensions.txt; break;
                    case ("html"): cse = CustomFileIO.SupportedExtensions.html; break;
                    case ("fb2"): cse = CustomFileIO.SupportedExtensions.fb2; break;
                }
                CustomFileIO.BrutforceWriteFile("ExternalIP", ip, cse);
                return true;
            }
            //I prevent in 1th line of function, that this line dont be execute
            throw new NotImplementedException();
        }

        public static void Main(string[] args)
        {
            if (args.Length > 1)
            {
                Console.WriteLine("too many arguments!\nSee --help (-h)");
                return;
            }
            string publicIp = "My public IP Addres is: " + (new System.Net.WebClient()).DownloadString("https://api.ipify.org");
            if (args.Length == 0)
            {
                Console.WriteLine(publicIp);
                System.Threading.Thread.Sleep(5 * 1000);// because user can click for launch program
            }
            else
                if (!CompleteCommand(DetectOption(args[0]), publicIp))
            		Console.WriteLine("runtime fail.\nSee --help");
        }
    }
}