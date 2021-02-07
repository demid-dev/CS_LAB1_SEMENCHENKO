using System;
using System.IO;

namespace CS_LAB1_SEMENCHENKO
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\nWrite \"info\" to get info about the file");
                Console.WriteLine("Write \"encode\" to encode the file\n");

                string line = Console.ReadLine();
                string path = "";

                if (line == "info")
                {
                    Console.WriteLine("\nWrite the name of file to get info from\n");
                    path = Console.ReadLine();
                    if (File.Exists($"texts/{path}"))
                    {
                        TextWorker.WorkWithText($"texts/{path}");
                    }
                }
                else if (line == "encode")
                {
                    Console.WriteLine("\nWrite the name of file to encode\n");
                    path = Console.ReadLine();
                    if (File.Exists($"texts/{path}"))
                    {
                        if (File.Exists($"texts/{path}"))
                        {
                            TextEncoder.B64Encode($"texts/{path}");
                        }
                    }
                }
            }
        }
    }
}
