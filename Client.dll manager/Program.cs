using System;
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Configuration;

namespace Client.dll_manager
{
    class Program
    {
        private static bool _did32Succeed;
        private static bool _did64Succeed;
        private static readonly string Dota2Directory = ConfigurationManager.AppSettings["directory"];
        private static readonly string Win32Path = Dota2Directory + @"\game\dota\bin\win32\client.dll";
        private static readonly string Win64Path = Dota2Directory + @"\game\dota\bin\win64\client.dll";
        private static readonly string Game32Path = Dota2Directory + @"\game\bin\win32\dota2.exe";
        private static readonly string Game64Path = Dota2Directory + @"\game\bin\win64\dota2.exe";
        static void Main()
        {
            Console.WriteLine("Dota 2 range changer!");
            Console.WriteLine(@"     ");
            Console.WriteLine(@"     _           _            ____   ___ ____   ___  ");
            Console.WriteLine(@"    | |_   _  __| | __ _  ___|___ \ / _ \___ \ / _ \ ");
            Console.WriteLine(@" _  | | | | |/ _` |/ _` |/ _ \ __) | | | |__) | | | |");
            Console.WriteLine(@"| |_| | |_| | (_| | (_| |  __// __/| |_| / __/| |_| |");
            Console.WriteLine(@" \___/ \__,_|\__,_|\__, |\___|_____|\___/_____|\___/ ");
            Console.WriteLine(@"                   |___/                             ");
            Console.WriteLine(@"     ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("I take no responsibility for any bans that may occur using this software. This is purely for exposing this exploit.");
            Console.ResetColor();
            
            if (!File.Exists(Win32Path))
            {
                //didn't find win32 dll. May handle this better in the future to allow inputing of the path by the user but im lazy
                Console.WriteLine("I can't seem to find the win32 client.dll. Change the filepath to the \"dota 2 beta\" folder in \"Client.dll manager.exe.config\".");
                Console.ReadLine();
                Environment.Exit(0);
            }
            if (!File.Exists(Win64Path))
            {
                //didn't find win64 dll. May handle this better in the futre to allow inputing of the path by the user but im lazy
                Console.WriteLine("I can't seem to find the win64 client.dll. Change the filepath to the \"dota 2 beta\" folder in \"Client.dll manager.exe.config\".");
                Console.ReadLine();
                Environment.Exit(0);
            }
            Console.WriteLine("found both client.dll files :)"); 

            Console.WriteLine("Enter the old vision range (Press enter to use the default 1134): "); //1134 default one in regular client.dll

            string input = Console.ReadLine(); //user input
            double inputAsNumber; 
            if (double.TryParse(input, out inputAsNumber))
            {
                Console.WriteLine("Using current vision range as: {0}", inputAsNumber);
            }
            else
            {
                Console.WriteLine("You either pressed enter or didn't input a number. Now using 1134");
                inputAsNumber = 1134;
            }

            Console.WriteLine("Enter the new vision range (just enter to use 1700): ");
            //end user input for start. begin user input for new range
            string input2 = Console.ReadLine();
            double inputAsNumber2;
            if (double.TryParse(input2, out inputAsNumber2))
            {
                Console.WriteLine("Using new vision range as: {0}", inputAsNumber2);
            }
            else
            {
                Console.WriteLine("You either pressed enter or didn't input a number. Now using 1700");
                inputAsNumber2 = 1700;
            }
            // convert inputs to strings
            var asstring = Convert.ToString(inputAsNumber);
            var asstring2 = Convert.ToString(inputAsNumber2);


            Console.WriteLine("Backing up client.dll files. To revert, rename \"Client.dll.old\" back to \"client.dll\"");

            File.Copy(Win32Path, Win32Path + ".old", true);
            File.Copy(Win64Path, Win64Path + ".old", true);


            Console.WriteLine("Now trying to edit the DLL files...");

            //try to replace the old with the new in win32
            string text = File.ReadAllText(Win32Path, Encoding.Default);
            text = text.Replace(asstring, asstring2);
            File.WriteAllText(Win32Path, text, Encoding.Default);
            //check for success win32
            System.Threading.Thread.Sleep(250); //wait so changes are made if it did succeed
            string newtext = File.ReadAllText(Win32Path, Encoding.Default);
            int newtextcheck = newtext.IndexOf(asstring2);

            if (newtextcheck == -1)
                Console.WriteLine(
                    "uh oh. something went wrong and I couldn't change the contents of the win32 client.dll. enter to exit. If the win64 change succeeds, run the 64-bit version of dota 2 instead.");
            else
            {
                Console.WriteLine("successfully changed value " + asstring + " to " + asstring2 + " in the win32 client.dll");
                _did32Succeed = true;
            }
                



            string text2 = File.ReadAllText(Win64Path, Encoding.Default);
            text2 = text2.Replace(asstring, asstring2);
            File.WriteAllText(Win64Path, text2, Encoding.Default);
            //check for success win64
            System.Threading.Thread.Sleep(250);
            string newtext2 = File.ReadAllText(Win64Path, Encoding.Default);
            int newtextcheck2 = newtext2.IndexOf(asstring2);

            if (newtextcheck2 == -1)
            {
                Console.WriteLine("uh oh. something went wrong and I couldn't change the contents of the win64 client.dll. If the win32 change succeeded, run the 32-bit version of dota 2 instead.");
                Console.ReadLine();
                
            }
            else
            {
                Console.WriteLine("successfully changed value " + asstring + " to " + asstring2 + " in the win64 client.dll");
                _did64Succeed = true;
            }

            if (_did32Succeed && _did64Succeed)
            {
                Console.WriteLine("Both changes succeeded, type \"1\" to run 32-bit dota, \"2\" to run 64-bit.");
                string input1 = Console.ReadLine(); 
                double inputAsNumber1;
                if (double.TryParse(input1, out inputAsNumber1))
                {
                    if (inputAsNumber1 == 1)
                        Process.Start(Game32Path);
                    if (inputAsNumber1 == 2)
                        Process.Start(Game64Path);
                }
                Environment.Exit(0);
            }
            if (_did64Succeed)
            {
                Console.WriteLine("the 32-bit change failed, but 64-bit change worked. Start the 64-bit client now by entering \"1\". ");
                string input1 = Console.ReadLine();
                double inputAsNumber1;
                if (double.TryParse(input1, out inputAsNumber1))
                {
                    if (inputAsNumber1 == 1)
                        Process.Start(Game64Path);
                }
                Environment.Exit(0);
            }
            if (_did32Succeed)
            {
                Console.WriteLine("the 64-bit change failed, but 32-bit change worked. Start the 32-bit client now by entering \"1\". ");
                string input1 = Console.ReadLine();
                double inputAsNumber1;
                if (double.TryParse(input1, out inputAsNumber1))
                {
                    if (inputAsNumber1 == 1)
                        Process.Start(Game32Path);
                }
                Environment.Exit(0);
            }
            Console.ReadLine();
            Environment.Exit(0);
        }
    }
}
