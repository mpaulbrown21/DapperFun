using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperFun.Lessons
{
    // Welecome to DapperLessons! 
    // Please follow the comment instructions inorder to make the tests
    // run...
    public static class DapperLessons
    {
        // Please change this to the name of your choice...
        public static string DeveloperName = "Phil";

        // Please change this to the years of your choice...
        public const int YearsWorked = 1;

        /// <summary>
        /// The number of rows to insert into the database for the
        /// performance test.
        /// </summary>
        public static int PerformanceTestRowInsertions = 1000000;

        public static int SeniorNessLevel
        {
            get { return YearsWorked * YearsWorked; }
        }

        public static PhilLevelDeveloper PhilLevelDeveloper
        {
            get
            {
                return new PhilLevelDeveloper
                {
                    DeveloperName = DeveloperName,
                    YearsWorked = YearsWorked,
                    SeniorNessLevel = SeniorNessLevel
                };
            }
        }

        public static BenLevelBoardGamer BenLevelboardGamer
        {
            get
            {
                return new BenLevelBoardGamer
                {
                    PlayerName = DeveloperName,
                    YearsPlayer = YearsWorked
                };
            }
        }

        public static int LessonOne_Inserting(PhilLevelDeveloper newPhilLevelDeveloper)
        {
            // This is the first method for you to change! In this section
            // you will learn how to execute sql commands, such as inserts, 
            // deletes, and updates. 

            // The dapper Execute method is used for executing sql, and returning
            // the number of affected rows.

            string sql = @"
                INSERT INTO PhilLevelDeveloper 
                            ( YearsWorked, SeniorNessLevel, DeveloperName ) 
                    Values  ( @YearsWorked, @SeniorNessLevel, @DeveloperName );
            ";

            using (IDbConnection db = new SqlConnection(ConnectionStrings.LocalDatabase))
            {
                // Please call and return db.Execute(sql, newPhilLevelDeveloper) to make
                // the test pass.

                return db.Execute(sql, newPhilLevelDeveloper);

                // When this test passes, you will have successfuly inserted
                // into the database using dapper.

                // You will notice that you passed in an object to the execute
                // method, newPhilLevelDeveloper. Dapper uses paramatrized 
                // queries to protect against sql injection.
            }
        }

        public static List<PhilLevelDeveloper> LessonTwo_Querying()
        {
            // The dapper method Query is used for querying the database, and returning 
            // an object. Dapper maps the result of the query to an object,
            // behind the scenes, so you don't have to!

            string sql = @"
                Select * From PhilLevelDeveloper";

            List<PhilLevelDeveloper> philLevelDeveloperList = new List<PhilLevelDeveloper>();

            using (IDbConnection db = new SqlConnection(ConnectionStrings.LocalDatabase))
            {
                // Please set philLevelDeveloperList to db.Query<PhilLevelDeveloper>(sql).ToList();
                philLevelDeveloperList = db.Query<PhilLevelDeveloper>(sql).ToList();
            }

            return philLevelDeveloperList;
        }
        
        public static int LessonThree_Deleting(PhilLevelDeveloper philLevelDeveloper)
        {
            // As mentioned above, The execute command can be used for deleting
            // from a table.
            string sql =
                @"Delete From PhilLevelDeveloper 
                    WHERE PhilLevelDeveloperId = @PhilLevelDeveloperId";

            using (IDbConnection db = new SqlConnection(ConnectionStrings.LocalDatabase))
            {
                // Please delete the passed in Phil Level Developer, using the
                // db.Execute(sql, philLevelDeveloper) method call.
                return db.Execute(sql, philLevelDeveloper);
            }
        }

        public static async Task<int> LessonFour_ExecuteAsyncInsert(PhilLevelDeveloper philLevelDeveloper)
        {
            // ExecuteAsync is used for executing a command once or multiple times 
            // asynchronously. It returns the number of rows affected.

            string sql = @"
                INSERT INTO PhilLevelDeveloper 
                            ( YearsWorked, SeniorNessLevel, DeveloperName ) 
                    Values  ( @YearsWorked, @SeniorNessLevel, @DeveloperName );
            ";

            using (IDbConnection db = new SqlConnection(ConnectionStrings.LocalDatabase))
            {
                // Please execute the above sql using db.ExecuteAsync,
                // and then return the number of rows affected.

                return await db.ExecuteAsync(sql, philLevelDeveloper);
            }
        }
        
        public static async Task<PhilLevelDeveloper> LessonFive_QueryFirstAsync() 
        {
            // Sometimes we only want to map the first result from the queryset
            // to an object. We can use QueryFirstAsync to do this...

            string sql = @"
                Select * From PhilLevelDeveloper 
                    order by PhilLevelDeveloperId asc";

            using (IDbConnection db = new SqlConnection(ConnectionStrings.LocalDatabase))
            {
                // Please execute the above sql using
                // db.QueryFirstAsync<PhilLevelDeveloper>(sql). 
                // Return the result.
                
                return await db.QueryFirstAsync<PhilLevelDeveloper>(sql);
            }
        }

        public static async Task<PhilLevelDeveloper> LessonSix_QuerySingleOrDefaultAsync()
        {
            // We also have a method that provides an option for when we want
            // only the first value, or if no value is provided, a default
            // value will be returned.

            string sql = @"
                Select * From PhilLevelDeveloper";

            using (IDbConnection db = new SqlConnection(ConnectionStrings.LocalDatabase))
            {
                // Please execute the above sql, mapping only the first result, 
                // using db.QueryFirstOrDefaultAsync 

                return await db.QueryFirstOrDefaultAsync<PhilLevelDeveloper>(sql);
            }
        }
        
        public static async Task<SoftwareTeam> LessonSeven_QueryMultipleAsync()
        {
            // Sometimes we want to send multiple queries to the database,
            // and read in multiple objects.

            string sql = @"Select * from PhilLevelDeveloper;
                           Select * from BenLevelBoardGamer;";

            SoftwareTeam softwareTeam = new SoftwareTeam();

            using (IDbConnection db = new SqlConnection(ConnectionStrings.LocalDatabase))
            {
                using (var multiQuery = await db.QueryMultipleAsync(sql))
                {
                    // Please use multiQuery.ReadAsync<PhilLevelDeveloper>().Result.ToList();
                    // to read in philLevelDeveloper from the database.

                    // Please use multiQuery.ReadAsync<BenLevelBoardGamer>().Result.ToList(); to also
                    // read in a BenLevelBoardGamer object from the database.

                    softwareTeam.PhilLevelDevelopers = multiQuery.ReadAsync<PhilLevelDeveloper>().Result.ToList();
                    softwareTeam.BenLevelBoardGamers = multiQuery.ReadAsync<BenLevelBoardGamer>().Result.ToList();
                }
            }

            return softwareTeam;
        }

        public static Tuple<bool, List<PhilLevelDeveloper>> Extra_BufferedQueriesAsync()
        {
            string sql = @"Select * From PhilLevelDeveloper";

            List <PhilLevelDeveloper> philLevelDevelopers = new List<PhilLevelDeveloper>();

            // Dapper allows us to choose between a buffered or non buffered query.

            // A buffered query will read into a list, before mapping it to an object.
            // This minimizes the time that a connection is open, while increasing
            // memory usage. It is recommended that a buffered query be used in most cases.
            // However, when reading from a table with millions of rows, we should 
            // consider using a non-buffered query.

            // A non buffered query will process each item into an object, maximizing
            // the required time for the database connection to be open, while minimizing
            // the memory usage. 

            // Please use a buffered query, by setting the value below to true.
            bool UseBuffer = false;

            using (IDbConnection db = new SqlConnection(ConnectionStrings.LocalDatabase))
            {
                // Notice here that we are setting the buffered parameter to true.
                philLevelDevelopers = db.Query<PhilLevelDeveloper>(sql, buffered: UseBuffer).ToList();
            }

            return new Tuple<bool, List<PhilLevelDeveloper>>(UseBuffer, philLevelDevelopers );
        }

        public static int Extra_Transaction(PhilLevelDeveloper philLevelDeveloper, BenLevelBoardGamer benLevelBoardGamer)
        {
            // Dapper provides functionality for a concept called "transactions".
            // Transactions make it so that all the sql must successfuly execute,
            // before any data is actually updated in the Database. 

            // Transactions can also increase performance of sql operations
            // according to this stack overflow q/a 
            // https://stackoverflow.com/questions/10689779/bulk-inserts-taking-longer-than-expected-using-dapper

            int affectedPhilRows;
            int affectedBenRows;

            string sqlPhil = @"
                INSERT INTO PhilLevelDeveloper 
                            ( YearsWorked, SeniorNessLevel, DeveloperName ) 
                    Values  ( @YearsWorked, @SeniorNessLevel, @DeveloperName );
            ";

            string sqlBen = @"
                INSERT INTO BenLevelBoardGamer
                            ( YearsPlayed, PlayerName ) 
                     Values ( 100, 'Matt' )";
            
            using (IDbConnection db = new SqlConnection(ConnectionStrings.LocalDatabase))
            {
                db.Open();

                using (var transaction = db.BeginTransaction())
                {
                    // Using db.Execute, insert philLevelDeveloper & benLevelDeveloper
                    // into the database.
                    affectedPhilRows = db.Execute(sqlPhil, philLevelDeveloper, transaction: transaction);
                    affectedBenRows = db.Execute(sqlBen, benLevelBoardGamer, transaction: transaction);

                    transaction.Commit();
                }
            }

            return affectedBenRows + affectedPhilRows;
        }

        public static void Extra_DapperMapping_Performance()
        {
            // The purpose of this method is to determine how fast it is
            // for reading in 25,000 Phil Level Developers using Dapper. 

            string sql = @"Select 
                        philLevelDeveloperId, -- 0
                        YearsWorked, -- 1
                        SeniorNessLevel, -- 2
                        DeveloperName -- 3
                    From PhilLevelDeveloper;";

            using (IDbConnection db = new SqlConnection(ConnectionStrings.LocalDatabase))
            {
                db.Query<PhilLevelDeveloper>(sql, buffered: false).AsList();
            }
        }

        public static void Data_Reader_Performance()
        {
            // The purpose of this method is to determine how fast it is
            // for reading in 25,000 Phil Level Developers using Data Reader.

            List<PhilLevelDeveloper> philLevelDevelopers = new List<PhilLevelDeveloper>();

            using (var connection = new SqlConnection(ConnectionStrings.LocalDatabase))
            {
                SqlCommand command = new SqlCommand(
                    @"Select 
                        philLevelDeveloperId, -- 0
                        YearsWorked, -- 1
                        SeniorNessLevel, -- 2
                        DeveloperName -- 3
                    From PhilLevelDeveloper;",
                    connection
                );

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    philLevelDevelopers.Add(
                        new PhilLevelDeveloper
                        {
                            PhilLevelDeveloperId = reader.GetInt32(0),
                            YearsWorked = reader.GetInt32(1),
                            SeniorNessLevel = reader.GetInt32(2),
                            DeveloperName = reader.GetString(3)
                        }
                    );
                }
            }
        }

        // One to One relation ship
        //public static async SoftwareTeam LessonEight_MultiMapping()
        //{
        //    string sql = @" SELECT * FROM EliteDevTeam e
        //                    left join PhilLevelDeveloper p
        //                        on p.PhilLevelDeveloperId = e.PhilLevelDeveloperId
        //                  ";

        //    using (IDbConnection db = new SqlConnection(ConnectionStrings.LocalDatabase))
        //    {
        //        using (var multiQuery = await db.QueryMultipleAsync(sql))
        //        {
        //        }
        //    }
        //}

        // Transaction


        // Buffered

        // Mapping Performance
    }
}
