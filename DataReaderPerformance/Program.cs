using DapperFun;
using DapperFun.Lessons;
using System;

namespace DataReaderPerformance
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Beginning data reader test...");

            var dataReaderBeginning = DateTime.Now;

            DapperLessons.Data_Reader_Performance();

            Console.WriteLine($"Data reader test finished. Total time {(DateTime.Now - dataReaderBeginning).TotalMilliseconds} Milliseconds");
        }
    }
}
