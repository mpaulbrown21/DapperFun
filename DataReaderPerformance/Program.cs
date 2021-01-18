using DapperFun;
using DapperFun.Lessons;
using System;

namespace DataReaderPerformance
{
    class Program
    {
        static void Main(string[] args)
        {
            // Data Reader

            DataGenerator.ResetLessonOneData();
            
            DataGenerator.CreatePhilLevelDeveloperTable();

            Console.WriteLine($"Beginning insertion of {DapperLessons.PerformanceTestRowInsertions} Phil Level Developers...");

            var insertionBegin = DateTime.Now;

            DataGenerator.InsertManyPhilLevelDevelopers(DapperLessons.PerformanceTestRowInsertions);

            Console.WriteLine($"Insertion Finish, total time {(insertionBegin - DateTime.Now).TotalMilliseconds * -1} Milliseconds");
            // Performance Dapper

            Console.WriteLine("Beginning data reader test...");

            var dataReaderBeginning = DateTime.Now;

            DapperLessons.Data_Reader_Performance();

            Console.WriteLine($"Data reader test finished. Total time {(DateTime.Now - dataReaderBeginning).TotalMilliseconds} Milliseconds");
        }
    }
}
