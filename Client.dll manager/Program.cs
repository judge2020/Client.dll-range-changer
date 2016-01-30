using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Client.dll_manager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Dota 2 zoomhack range changer! joduska.me EnsageSharp community!");
            // gotta give credit to the coder :D
            Console.WriteLine(@"     ");
            Console.WriteLine(@"     _           _            ____   ___ ____   ___  ");
            Console.WriteLine(@"    | |_   _  __| | __ _  ___|___ \ / _ \___ \ / _ \ ");
            Console.WriteLine(@" _  | | | | |/ _` |/ _` |/ _ \ __) | | | |__) | | | |");
            Console.WriteLine(@"| |_| | |_| | (_| | (_| |  __// __/| |_| / __/| |_| |");
            Console.WriteLine(@" \___/ \__,_|\__,_|\__, |\___|_____|\___/_____|\___/ ");
            Console.WriteLine(@"                   |___/                             ");
            Console.WriteLine(@"     ");
            //path to client.dll's
            string win32path = @"C:\Program Files (x86)\Steam\steamapps\common\dota 2 beta\game\dota\bin\win32\client.dll";
            string win64path = @"C:\Program Files (x86)\Steam\steamapps\common\dota 2 beta\game\dota\bin\win64\client.dll";

            if (!File.Exists(win32path))
            {
                //didn't find win32 dll. May handle this better in the futre to allow inputing of the path by the user but im lazy
                Console.WriteLine("I can't seem to find the win32 client.dll. Exit by pressing enter");
                Console.ReadLine();
                Environment.Exit(0);
            }
            if (!File.Exists(win64path))
            {
                //didn't find win64 dll. May handle this better in the futre to allow inputing of the path by the user but im lazy
                Console.WriteLine("I can't seem to find the win64 client.dll. Exit by pressing enter");
                Console.ReadLine();
                Environment.Exit(0); //0 exit code means program exited successfully. Programmatic close so don't want windows freaking out if other codes are thrown
            }
            Console.WriteLine("found both client.dll files :)"); //HOORAY
            Console.WriteLine("Enter the old vision range (just enter to use the default 1134): "); //1134 default one in regular client.dll
            //user input for start range
            string input = Console.ReadLine(); //user input
            double inputAsNumber; 
            if (double.TryParse(input, out inputAsNumber) == true)
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
            if (double.TryParse(input2, out inputAsNumber2) == true)
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


            Console.WriteLine("Now trying to edit the DLL files...");
            
            //try to replace the old with the new in win32
            string text = File.ReadAllText(win32path);
            text = text.Replace(asstring, asstring2);
            File.WriteAllText(win32path, text);
            //check for success win32
            System.Threading.Thread.Sleep(250); //wait so changes are made if it did succeed
            string newtext = File.ReadAllText(win32path);
            int newtextcheck = newtext.IndexOf(asstring2);

            if (newtextcheck == -1)
            {
                Console.WriteLine("uh oh. something went wrong and I couldn't change the contents of the win32 client.dll. enter to exit");
                Console.ReadLine();
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("successfully changed value " + asstring + " to " + asstring2 + " in the win32 client.dll");
            }
            

            string text2 = File.ReadAllText(win64path);
            text = text2.Replace(asstring, asstring2);
            File.WriteAllText(win64path, text2);
            //check for success win64
            System.Threading.Thread.Sleep(250);
            string newtext2 = File.ReadAllText(win64path);
            int newtextcheck2 = newtext2.IndexOf(asstring2);

            if (newtextcheck2 == -1)
            {
                Console.WriteLine("uh oh. something went wrong and I couldn't change the contents of the win64 client.dll. enter to exit.");
                Console.ReadLine();
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("successfully changed value " + asstring + " to " + asstring2 + " in the win64 client.dll");

            }
            Console.WriteLine("end of program. Congrats!");
            Console.ReadLine();
            Environment.Exit(0);
        }
    }
}
