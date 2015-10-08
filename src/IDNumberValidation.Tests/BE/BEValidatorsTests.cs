using System;
using IDNumberValidation.Implementations.BE.Company;
using IDNumberValidation.Implementations.BE.Person;
using NUnit.Framework;

namespace IDNumberValidation.Tests.BE
{
    [TestFixture]
    public class BEValidatorsTests
    {
        [SetUp]
        public void Init()
        {
            nrNumberValidator = new NRNumberValidator();
            cbeNumberValidator = new CBENumberValidator();
        }

        private NRNumberValidator nrNumberValidator;
        private CBENumberValidator cbeNumberValidator;

        [Test]
        public void TestCBENumberValidation()
        {
            //enterprise CBE number
            var cbe1Result = cbeNumberValidator.Validate("0403.170.701");
            
            Assert.IsNotNull(cbe1Result);
            Assert.AreEqual(true, cbe1Result.IsValid);

            //site CBE number
            var cbe2Result = cbeNumberValidator.Validate("2.102.042.458");

            Assert.IsNotNull(cbe2Result);
            Assert.AreEqual(true, cbe2Result.IsValid);
        }

        [Test]
        public void TestNRNumberValidation()
        {
            var nr1Result = nrNumberValidator.Validate("72020290081");
            var nr1AdditionalData = nr1Result.AdditionalData as NRNumberAdditionalData;

            Assert.IsNotNull(nr1Result);
            Assert.AreEqual(true, nr1Result.IsValid);
            Assert.IsNotNull(nr1AdditionalData);
            Assert.AreEqual(new DateTime(1972, 02, 02), nr1AdditionalData.BirthDate);
            Assert.AreEqual(GenderEnum.Female, nr1AdditionalData.Gender);

            var nr2Result = nrNumberValidator.Validate("BE 00.01.25.567-77");
            var nr2AdditionalData = nr2Result.AdditionalData as NRNumberAdditionalData;

            Assert.IsNotNull(nr2Result);
            Assert.AreEqual(true, nr2Result.IsValid);
            Assert.IsNotNull(nr2AdditionalData);
            Assert.AreEqual(new DateTime(2000, 01, 25), nr2AdditionalData.BirthDate);
            Assert.AreEqual(GenderEnum.Male, nr2AdditionalData.Gender);
        }
    }
}