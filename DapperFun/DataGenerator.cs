using Dapper;
using DapperFun;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace DapperFun 
{
    public static class DataGenerator
    {
        public static async Task<int> GetRowCountOfPhilLevelDeveloper()
        {
            string sql =
                @"SELECT COUNT(*) FROM PhilLevelDeveloper";

            using (IDbConnection db = new SqlConnection(ConnectionStrings.LocalDatabase))
            {
                // Please uncomment & pass in philLevelDeveloper to the method call below... 
                return await db.ExecuteScalarAsync<int>(sql);
            }
           
        }

        public static async Task<PhilLevelDeveloper> GetFirstPhilLevelDeveloper()
        {
            string sql =
                @"Select * from PhilLevelDeveloper";

            using (IDbConnection db = new SqlConnection(ConnectionStrings.LocalDatabase))
            {
                // Please uncomment & pass in philLevelDeveloper to the method call below... 
                return await db.QueryFirstAsync<PhilLevelDeveloper>(sql);
            }
        }

        public static void ResetLessonOneData()
        {
            using (var connection = new SqlConnection(ConnectionStrings.LocalDatabase))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"IF OBJECT_ID('BenLevelBoardGamer') is not null BEGIN DROP TABLE [dbo].[BenLevelBoardGamer] END";
                    command.ExecuteNonQuery();

                    command.CommandText = @"IF OBJECT_ID('EliteDevTeam') is not null BEGIN DROP TABLE [dbo].[EliteDevTeam] END";
                    command.ExecuteNonQuery();
                    
                    command.CommandText = @"IF OBJECT_ID('PhilLevelDeveloper') is not null BEGIN DROP TABLE [dbo].[PhilLevelDeveloper] END";
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void CreateEliteDevTeamTable()
        {
            using (var connection = new SqlConnection(ConnectionStrings.LocalDatabase))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"   
                        CREATE TABLE EliteDevTeam (
                            EliteDevTeamId int NOT NULL,
                            PhilLevelDeveloperId int NOT NULL,
                            PRIMARY KEY (EliteDevTeamId),
                            CONSTRAINT FK_PhilLevelDeveloper FOREIGN KEY (PhilLevelDeveloperId)
                            REFERENCES PhilLevelDeveloper(PhilLevelDeveloperId)
                        )
                    ";
                    command.ExecuteNonQuery();
                }
            }
        }
        
        public static void CreatePhilLevelDeveloperTable()
        {
            using (var connection = new SqlConnection(ConnectionStrings.LocalDatabase))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"   
                    CREATE TABLE [dbo].[PhilLevelDeveloper] (
                        [PhilLevelDeveloperId] INT IDENTITY (1, 1) NOT NULL,
                        [YearsWorked]     INT NOT NULL,
                        [SeniorNessLevel] INT NOT NULL,
                        [DeveloperName]            NVARCHAR (MAX) NULL,
                        CONSTRAINT [PK_dbo.PhilLevelDeveloper] PRIMARY KEY CLUSTERED ([PhilLevelDeveloperID] ASC)
                    );
                    ";
                    command.ExecuteNonQuery();
                }
            }
        }
        
        public static void CreateBenLevelBoardGamerTable()
        {
            using (var connection = new SqlConnection(ConnectionStrings.LocalDatabase))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"   
                    CREATE TABLE [dbo].[BenLevelBoardGamer] (
                        [BenLevelBoardGamerId] INT IDENTITY (1, 1) NOT NULL,
                        [YearsPlayed]     INT NOT NULL,
                        [PlayerName]            NVARCHAR (MAX) NULL,
                    );
                    ";
                    command.ExecuteNonQuery();
                }
            }
        }
        
        public static void InsertFourBenLevelBoardGamer()
        {
            using (var connection = new SqlConnection(ConnectionStrings.LocalDatabase))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"   
                        INSERT INTO BenLevelBoardGamer ( YearsPlayed, PlayerName ) Values ( 100, 'Matt' );
                        INSERT INTO BenLevelBoardGamer ( YearsPlayed, PlayerName ) Values ( 300, 'Joseph' );
                        INSERT INTO BenLevelBoardGamer ( YearsPlayed, PlayerName ) Values ( 1300, 'ShaNeil' );
                        INSERT INTO BenLevelBoardGamer ( YearsPlayed, PlayerName ) Values ( 9999, 'Ben' );
                        ";
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void InsertFourPhilLevelDevelopers()
        {
            using (var connection = new SqlConnection(ConnectionStrings.LocalDatabase))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"   
                        INSERT INTO PhilLevelDeveloper ( YearsWorked, SeniorNessLevel, DeveloperName ) Values ( 0, 100, 'Matt' );
                        INSERT INTO PhilLevelDeveloper ( YearsWorked, SeniorNessLevel, DeveloperName ) Values ( 1, 100, 'Kip' );
                        INSERT INTO PhilLevelDeveloper ( YearsWorked, SeniorNessLevel, DeveloperName ) Values ( 2, 100, 'Joseph' );
                        INSERT INTO PhilLevelDeveloper ( YearsWorked, SeniorNessLevel, DeveloperName ) Values ( 3, 0, 'Phil' );
                        ";
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void InsertManyPhilLevelDevelopers(int count)
        {
            List <PhilLevelDeveloper> philLevelDevelopers = new List<PhilLevelDeveloper>();

            for (int i = 0; i < count; i++)
            {
                philLevelDevelopers.Add(
                    new PhilLevelDeveloper
                    {
                        DeveloperName = $"Phil_Clone_{i}",
                        YearsWorked = i,
                        SeniorNessLevel = i * 2
                    }
                );
            }

            string sql = @"   
                INSERT INTO PhilLevelDeveloper 
                       ( YearsWorked, SeniorNessLevel, DeveloperName ) 
                Values ( @YearsWorked, @SeniorNessLevel, @DeveloperName );
            ";
            using (IDbConnection db = new SqlConnection(ConnectionStrings.LocalDatabase))
            {
                db.Execute(sql, philLevelDevelopers);
            }
        }
    }
}
