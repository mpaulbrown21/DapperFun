using DapperFun;
using DapperFun.Lessons;
using System;

namespace DapperPerformance
{
    class Program
    {
        static void Main(string[] args)
        {
            // Dapper

            Console.WriteLine("Beginning dapper mapping test...");

            var dapperMappingBeging = DateTime.Now;

            DapperLessons.Extra_DapperMapping_Performance();

            Console.WriteLine($"Mapping finished (Buffered), total time {(DateTime.Now - dapperMappingBeging).TotalMilliseconds} Milliseconds");
        }
    }
}
