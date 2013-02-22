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
            IdentifierParsingResult<PersonIdentifier> parsingResult = BelgianPersonIdentifier.ParseNumber(""); //WHEN CHECKING IN CENTRAL REPOSITORY, LEAVE NUMBER BLANK FOR CONFIDENTIALITY
            PersonIdentifier pi = parsingResult.GetValidIdentifier();
        }
    }
}
