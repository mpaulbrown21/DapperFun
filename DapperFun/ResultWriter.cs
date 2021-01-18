using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DapperFun
{
    public static class ResultWriter
    {
        public static void WriteLessonOneResults( int numberRowsReturn )
        {
            if (numberRowsReturn == 2)
                WriteLessonSuccess("1");
            else
                WriteLessonFailure("1");
        }

        public static void WriteLessonTwoResults( string expectedName, List<PhilLevelDeveloper> actual )
        {
            string devName = string.Empty;
            if (actual.Count > 0)
                devName = actual[0].DeveloperName;

            if (string.Equals(expectedName, devName))
                WriteLessonSuccess("2");
            else
                WriteLessonFailure("2");
        }

        public static void WriteLessonThreeResults( PhilLevelDeveloper philLevelDeveloper )
        {
            string sql = @"
                Select * From PhilLevelDeveloper 
                    where PhilLevelDeveloperId = @PhilLevelDeveloperId";

            List<PhilLevelDeveloper> result = new List<PhilLevelDeveloper>();

            using (IDbConnection db = new SqlConnection(ConnectionStrings.LocalDatabase))
            {
                result = db.Query<PhilLevelDeveloper>(sql, philLevelDeveloper).ToList(); 
            }

            if (result.Count == 0)
                WriteLessonSuccess("3");
            else
                WriteLessonFailure("3");
        }

        public static void WriteLessonSuccess( string Lesson )
        {
            Console.WriteLine($"LESSON [{Lesson}] SUCCESS");
            ResultWriter.WriteTable();
        }
        public static void WriteLessonFailure( string Lesson )
        {
            Console.WriteLine($"LESSON [{Lesson}] FAILURE");
            ResultWriter.WriteTable();
        }
        
        public static void WriteLessonIntro(string lesson, string Intro)
        {
            Console.WriteLine($"Lesson - {lesson} {Intro}");
        }
        
        public static void WriteTable()
        {
            string sql = "Select * From PhilLevelDeveloper";

            var philLevelDeveloperList = new List<PhilLevelDeveloper>(); 

            using (IDbConnection db = new SqlConnection(ConnectionStrings.LocalDatabase))
            {
                philLevelDeveloperList = db.Query<PhilLevelDeveloper>(sql).ToList();
            }

            Console.WriteLine("\n");
            Console.WriteLine("TABLE");
            Console.WriteLine(
                $"{"PhilLevelDeveloperId".PadRight(30, '-')}" +
                $"{"DeveloperName".PadRight(30, '-')}" +
                $"{"YearsWorked".PadRight(30, '-')}" +
                $"{"SeniorNessLevel".PadRight(30, '-')}"
            );

            foreach ( PhilLevelDeveloper developer in philLevelDeveloperList )
            {
                Console.WriteLine(
                    $"{developer.PhilLevelDeveloperId.ToString().PadRight(30)}" +
                    $"{developer.DeveloperName.ToString().PadRight(30)}" +
                    $"{developer.YearsWorked.ToString().PadRight(30)}" +
                    $"{developer.SeniorNessLevel.ToString().PadRight(30)}" +
                    "\n" + 
                    $"{new string('-', 120)}"
                );
            }

            Console.WriteLine("\n");
        }
    }
}
