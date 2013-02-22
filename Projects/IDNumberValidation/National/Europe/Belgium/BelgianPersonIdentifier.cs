using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDNumberValidation.National.Europe.Belgium
{
    public static class BelgianPersonIdentifier
    {
        public static IdentifierParsingResult<PersonIdentifier> ParseNumber(string number)
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
    }
}
