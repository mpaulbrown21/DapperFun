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

            DataGenerator.ResetLessonOneData();

            DataGenerator.CreatePhilLevelDeveloperTable();

            Console.WriteLine($"Beginning insertion of {DapperLessons.PerformanceTestRowInsertions} Phil Level Developers...");

            var insertionBegin = DateTime.Now;

            DataGenerator.InsertManyPhilLevelDevelopers(DapperLessons.PerformanceTestRowInsertions);

            Console.WriteLine($"Insertion Finish, total time {(insertionBegin - DateTime.Now).TotalMilliseconds * -1} Milliseconds");

            Console.WriteLine("Beginning dapper mapping test...");

            var dapperMappingBeging = DateTime.Now;

            DapperLessons.Extra_DapperMapping_Performance();

            Console.WriteLine($"Mapping finished (Buffered), total time {(DateTime.Now - dapperMappingBeging).TotalMilliseconds} Milliseconds");
        }
    }
}
