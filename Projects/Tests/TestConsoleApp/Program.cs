using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IDNumberValidation;
using IDNumberValidation.National.Europe.Belgium;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser belgianParser = new Parser();

            IdentifierParsingResult<PersonIdentifier> personParsingResult = belgianParser.ParsePersonNumber(""); //WHEN CHECKING IN CENTRAL REPOSITORY, LEAVE NUMBER BLANK FOR CONFIDENTIALITY
            PersonIdentifier pi = personParsingResult.GetValidIdentifier();

            IdentifierParsingResult<CompanyIdentifier> companyParsingResult = belgianParser.ParseCompanyNumber("");
            CompanyIdentifier ci = companyParsingResult.GetValidIdentifier();
        }
    }
}
