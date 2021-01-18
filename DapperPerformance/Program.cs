using DapperFun;
using DapperFun.Lessons;
using System;
using System.Collections.Generic;

namespace DapperPerformance
{
    class Program
    {
        static void Main(string[] args)
        {
            // Dapper
            /*
             * 
             *  1088 milliseconds for 1 million rows. DAPPER False Buffer.
             *  1118 milliseconds for 1 million rows. DAPPER True Buffered.
             *  983  milliseconds for 1 million rows. ASP.NET
             *  https://github.com/StackExchange/Dapper
             * */
            List<float> totalTimes = new List<float>();

            Console.WriteLine("Beginning dapper mapping test...");

            for (int i = 0; i < 10; i++)
            {
                var startTime = DateTime.Now;

                DapperLessons.Extra_DapperMapping_Performance();

                var endTime = DateTime.Now;

                totalTimes.Add((float)(endTime - startTime).TotalMilliseconds);

                Console.WriteLine($"Mapping finished (Buffered), total time {(DateTime.Now - startTime).TotalMilliseconds} Milliseconds");
            }

            float average = 0;
            for (int i = 1; i < 10; i++)
            {
                average += totalTimes[i];
            }

            average = average / 9;

            Console.WriteLine($"Average time {average} (after cache)");

            Console.WriteLine("End Test");
        }
    }
}
