using DapperFun;
using DapperFun.Lessons;
using System;
using System.Collections.Generic;

namespace DataReaderPerformance
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Beginning data reader test...");

            var dataReaderBeginning = DateTime.Now;
  
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

                DapperLessons.Data_Reader_Performance();

                var endTime = DateTime.Now;

                totalTimes.Add((float)(endTime - startTime).TotalMilliseconds);

                Console.WriteLine($"Data reader finished. Total time {totalTimes[i]} Milliseconds");
            }

            float average = 0;
            for (int i = 0; i < 10; i++)
            {
                average += totalTimes[i];
            }

            average = average / 10;

            Console.WriteLine($"Average time {average}");

        }
    }
}
