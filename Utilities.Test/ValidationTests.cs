using System.Diagnostics;
using Utilities.Test.Models;
using Zanella.Utilities.Models;
using Zanella.Utilities.Validation;

namespace Utilities.Test
{
    [TestClass]
    public class ValidationTests
    {
        [TestMethod]
        public void ObjectIsValid()
        {
            var test = new Example()
            {
                Name = "Test",
                Age = 1,
            };
            var result = DataAnnotationsValidation.Validate(test);
            PrintResults(result);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void ObjectIsValidWithCPF()
        {
            var test = new Example()
            {
                Name = "Test",
                Age = 1,
                CPF = "12345678909",
            };
            var result = DataAnnotationsValidation.Validate(test);
            PrintResults(result);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void ObjectWithoutAttributesIsValid()
        {
            var test = new Example2();
            var result = DataAnnotationsValidation.Validate(test);
            PrintResults(result);

            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void ObjectIsInvalid()
        {
            var test = new Example()
            {
                Name = "Te",
                Email = "te",
            };
            var result = DataAnnotationsValidation.Validate(test);
            PrintResults(result);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void ObjectIsInvalidWithCPF()
        {
            var test = new Example()
            {
                Name = "Test",
                Age = 1,
                CPF = "12345678901",
            };
            var result = DataAnnotationsValidation.Validate(test);
            PrintResults(result);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void ObjectIsNull()
        {
            var result = DataAnnotationsValidation.Validate(null);

            PrintResults(result);

            Assert.IsFalse(result.IsSuccess);
        }

        [TestMethod]
        public void ValidCNPJ()
        {
            Assert.IsTrue(DocumentValidation.IsValidCPNJ("34.461.455/0001-26"));
        }

        [TestMethod]
        public void InvalidCNPJ()
        {
            Assert.IsFalse(DocumentValidation.IsValidCPNJ("34.461.455/0001-00"));
        }

        [Conditional("DEBUG")]
        private static void PrintResults(Result result)
        {
            Debug.WriteLine("IsSucess: {0}", result.IsSuccess);
            foreach (var item in result.Messages)
            {
                Debug.WriteLine(item);
            }
        }
    }
}
