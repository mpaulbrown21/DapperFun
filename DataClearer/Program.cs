using DapperFun;
using System;

namespace DataClearer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Deleting data...");

            DataGenerator.ResetLessonOneData();

            Console.WriteLine("Finished");
        }
    }
}
