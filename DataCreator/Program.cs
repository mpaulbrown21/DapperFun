using DapperFun;
using DapperFun.Lessons;
using System;

namespace DataCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            DataGenerator.ResetLessonOneData();

            DataGenerator.CreatePhilLevelDeveloperTable();

            Console.WriteLine($"Beginning insertion of {DapperLessons.PerformanceTestRowInsertions} Phil Level Developers...");

            var insertionBegin = DateTime.Now;

            DataGenerator.InsertManyPhilLevelDevelopers(DapperLessons.PerformanceTestRowInsertions);

            Console.WriteLine($"Insertion Finish, total time {(insertionBegin - DateTime.Now).TotalMilliseconds * -1} Milliseconds");

        }
    }
}
