using System;
using System.Collections.Generic;
using System.Linq;
using NoPandaCode.ApplicationEnvironment;

namespace NoPandaCode.ApplicationEnvironment.Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine($"\nAppStatus: {AppRunner.Run<MyApplication>("test", new string[] {"flag", "option=yes"})}");
        }
    }
}