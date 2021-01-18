using DapperFun;
using DapperFun.Lessons;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace LessonTesting
{
    [TestClass]
    public class LessonTests
    {

        [TestInitialize]
        public void Initialize()
        {
            DataGenerator.ResetLessonOneData();
            DataGenerator.CreatePhilLevelDeveloperTable();
            DataGenerator.CreateBenLevelBoardGamerTable();
            DataGenerator.CreateEliteDevTeamTable();
        }

        [TestMethod]
        public void LessonOne_Inserting_OneRowReturned()
        {
            PhilLevelDeveloper philLevelDeveloper = new PhilLevelDeveloper
            {
                DeveloperName = DapperLessons.DeveloperName,
                YearsWorked = DapperLessons.YearsWorked,
                SeniorNessLevel = DapperLessons.SeniorNessLevel
            };

            var result = DapperLessons.LessonOne_Inserting(philLevelDeveloper);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void LessonTwo_Querying_FourObjectsReturned()
        {
            DataGenerator.InsertFourPhilLevelDevelopers();

            var result = DapperLessons.LessonTwo_Querying();

            Assert.AreEqual(4, result.Count);
        }

        [TestMethod]
        public async Task LessonThree_Deleting_OneDeveloperDeletedAsync()
        {
            DataGenerator.InsertFourPhilLevelDevelopers();

            PhilLevelDeveloper philLevelDeveloper = await DataGenerator.GetFirstPhilLevelDeveloper();

            DapperLessons.LessonThree_Deleting(philLevelDeveloper);

            int rowCount = await DataGenerator.GetRowCountOfPhilLevelDeveloper();
            Assert.AreEqual(3, rowCount);
        }

        [TestMethod]
        public async Task LessonFour_ExecuteAsyncInsert_OneDeveloperInsertedAsync()
        {
            var result = await DapperLessons.LessonFour_ExecuteAsyncInsert(DapperLessons.PhilLevelDeveloper);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public async Task LessonFive_ExecuteSingleOrDefaultAsync_OneDeveloper()
        {
            DataGenerator.InsertFourPhilLevelDevelopers();

            var result = await DapperLessons.LessonFive_QueryFirstAsync();

            Assert.AreEqual(1, result.PhilLevelDeveloperId);
        }

        [TestMethod]
        public async Task LessonSix_QuerySingleOrDefaultAsync_DefaultUsed()
        {
            var result = await DapperLessons.LessonSix_QuerySingleOrDefaultAsync();

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task LessonSeven_QueryMultipleAsync_ReadingFromTwoTables()
        {
            DataGenerator.InsertFourPhilLevelDevelopers();
            DataGenerator.InsertFourBenLevelBoardGamer();

            var result = await DapperLessons.LessonSeven_QueryMultipleAsync();

            Assert.AreEqual(4, result.PhilLevelDevelopers.Count);
            Assert.AreEqual(4, result.BenLevelBoardGamers.Count);
        }

        [TestMethod]
        public void Extra_BufferedQuery_BufferFalse()
        {
            DataGenerator.InsertFourPhilLevelDevelopers(); 
            DataGenerator.InsertFourPhilLevelDevelopers();
            DataGenerator.InsertFourPhilLevelDevelopers();

            var result = DapperLessons.Extra_BufferedQueriesAsync();

            Assert.IsFalse(result.Item1);
            Assert.AreEqual(12, result.Item2.Count);
        }

        [TestMethod]
        public void Extra_Transaction_NoTransactionsOccured()
        {
            var result = DapperLessons.Extra_Transaction(
                DapperLessons.PhilLevelDeveloper,
                DapperLessons.BenLevelboardGamer
            );

            Assert.AreEqual(2, result);
        }
    }
}
