using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDNumberValidation.National.Europe.Belgium
{
    public class Parser : IPersonIDNumberParser, ICompanyIDNumberParser
    {
        public IdentifierParsingResult<PersonIdentifier> ParsePersonNumber(string number)
        {
            IdentifierParsingResult<PersonIdentifier> result = new IdentifierParsingResult<PersonIdentifier>();
            result.Number = number;
            result.IsValid = false;

            NationalNumber rrNumber = new NationalNumber(number);
            rrNumber.Validate();
            result.IdentifierList.Add(rrNumber);

            if (rrNumber.IsValid.HasValue && rrNumber.IsValid.Value == true)
            {
                result.IsValid = true;
                return result;
            }
            else
            {
                BISNumber bisNumber = new BISNumber(number);
                bisNumber.Validate();
                result.IdentifierList.Add(bisNumber);

                if (bisNumber.IsValid.HasValue && bisNumber.IsValid.Value == true)
                {
                    result.IsValid = true;
                }

                return result;
            }
        }

        

        public IdentifierParsingResult<CompanyIdentifier> ParseCompanyNumber(string number)
        {
            IdentifierParsingResult<CompanyIdentifier> result = new IdentifierParsingResult<CompanyIdentifier>();
            result.Number = number;
            result.IsValid = false;

            CBENumber cbeNumber = new CBENumber(number);
            cbeNumber.Validate();
            result.IdentifierList.Add(cbeNumber);

            if (cbeNumber.IsValid.HasValue && cbeNumber.IsValid.Value == true)
            {
                result.IsValid = true;            
            }

            return result;
            
        }

        
    }
}
