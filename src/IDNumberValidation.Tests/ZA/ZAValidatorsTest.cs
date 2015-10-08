using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IDNumberValidation.Implementations.ZA.Person;
using NUnit.Framework;

namespace IDNumberValidation.Tests.ZA
{
    [TestFixture]
    public class ZAValidatorsTest
    {
        [SetUp]
        public void Init()
        {
            nationalIdValidator = new NationalIDValidator();
        }

        private NationalIDValidator nationalIdValidator;

        [Test]
        public void TestNationalIDValidation()
        {
            var nr1Result = nationalIdValidator.Validate("8501016184086");
            var nr1AdditionalData = nr1Result.AdditionalData as NationalIDAdditionalData;

            Assert.IsNotNull(nr1Result);
            Assert.AreEqual(true, nr1Result.IsValid);
            Assert.IsNotNull(nr1AdditionalData);
            Assert.AreEqual(new DateTime(1985, 01, 01), nr1AdditionalData.BirthDate);
            Assert.AreEqual(GenderEnum.Male, nr1AdditionalData.Gender);
        }
    }
}
