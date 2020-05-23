using System;
using ZipContent.GoogleDrive;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {


                Console.WriteLine("Hello World!");

                Class1 o = new Class1();

                o.Test().Wait();
            }
            catch (Exception exc)
            {

                Console.WriteLine(exc.Message);
            }
        }
    }
}
