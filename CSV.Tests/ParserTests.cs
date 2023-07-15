using CSV.Tests.Models;
using Zanella.CSV;
using Zanella.CSV.Exceptions;

namespace CSV.Tests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void GenerateCSV()
        {
            var list = new List<CSVData> {
                new CSVData()
                {
                    Description = "description 1",
                    Id = 1,
                    Name = "Joe Test",
                }
                , new CSVData()
                {
                    Description = "description 2",
                    Id = 2,
                    Name = "Mari Test",
                },
            };

            var csvContent = Helper.Write(list);
            Assert.IsNotNull(csvContent);

            var data = Helper.Read<CSVDataRead>(csvContent);
            Assert.IsTrue(data.FirstOrDefault()?.Name == list.FirstOrDefault()?.Name);
        }

        [TestMethod]
        public void ReadCSV()
        {
            var content = @"Joe Test;description 1;1
Mari Test;description 2;2";
            var list = Helper.Read<CSVDataRead>(content);

            Assert.IsTrue(list.Any(x => !x.Exceptions.Any()));
            Assert.IsTrue(list.FirstOrDefault(x => x.Line == 2)?.Name == "Mari Test");
        }

        [TestMethod]
        public void ReadCSVWithError()
        {
            var content = @"Joe Test;description 1;ErrorId
Mari Test;description 2;2";
            var list = Helper.Read<CSVDataRead>(content);

            var errorIdException = list.FirstOrDefault(x => x.Line == 1)?.Exceptions.FirstOrDefault();

            Assert.IsNotNull(errorIdException);
            Assert.IsInstanceOfType(errorIdException, typeof(InvalidValueException));
        }
    }
}