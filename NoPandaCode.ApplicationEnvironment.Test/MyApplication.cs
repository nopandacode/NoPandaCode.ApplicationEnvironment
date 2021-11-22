using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoPandaCode.ApplicationEnvironment;

namespace NoPandaCode.ApplicationEnvironment.Test
{
    [Application(Name = "TestApplication")]
    public class MyApplication
    {
        [Command(Type = CommandType.DefaultCommand)]
        public void Default()
        {
            Console.WriteLine("If you see this, then the application has runned the default command.");
        }

        [Command(Description = "This command prints a text.")]
        public void Test(List<string> flags, Dictionary<string, string> options)
        {
            Console.WriteLine("This is a test command!");
            Console.WriteLine("Flags:");
            foreach (var flag in flags)
            {
                Console.WriteLine($"- {flag}");
            }
            Console.WriteLine("Options:");
            foreach (var option in options)
            {
                Console.WriteLine($"- {option.Key} ~ {option.Value}");
            }

        }

        [Command(Description = "This command prints a another text.", Name = "test2")]
        public void AnotherCommand()
        {
            Console.WriteLine("This is another command!");
        }
    }
}
