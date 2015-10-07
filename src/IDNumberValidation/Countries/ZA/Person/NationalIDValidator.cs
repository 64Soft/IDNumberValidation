using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using IDNumberValidation.Countries.BE.Person;

namespace IDNumberValidation.Countries.ZA.Person
{
    public class NationalIDValidator : IIDNumberValidator
    {
        public IDNumberCategoryEnum Category { get; private set; }
        public string Name { get; private set; }

        public NationalIDValidator()
        {
            Category = IDNumberCategoryEnum.Person;
            Name = "ZA_NationalID_Validator";
        }

        public IDNumberValidationResult Validate(string number)
        {
            var result = new IDNumberValidationResult(number);
            result.AdditionalData = new NationalIDAdditionalData();

            result.CleanProvidedValue = number.ToAlphaNumericOnly().TrimStart("ZA").TrimStart("za").Trim();

            try
            {
                if (!string.IsNullOrEmpty(result.CleanProvidedValue))
                {
                    var nonAllowedCharacters = new Regex(@"[^0-9. -]");

                    var idnumber = string.Copy(result.CleanProvidedValue);

                    if (nonAllowedCharacters.IsMatch(idnumber))
                    {
                        result.IsValid = false;
                        result.ValidationErrors.Add("Number contains non-allowed character");
                    }
                    else
                    {
                        //LENGTH MUST BE 11 DIGITS
                        if (idnumber.Length != 13)
                        {
                            result.IsValid = false;
                            result.ValidationErrors.Add("Length != 13");
                        }
                        else
                        {
                            var birthDateOK = false;
                            var counterOK = false;
                            var citizenshipOK = false;
                            var raceOK = false;
                            var controlOK = false;

                            DateTime birthDate = new DateTime();
                            var gender = "(unknown)";
                            int? citizenship;
                            int? race;

                            //FIRST 6 DIGITS ARE BIRTHDATE IN FORMAT YYMMDD
                            var birthDatePart = idnumber.Substring(0, 6);

                            //NEXT 4 ARE COUNTER
                            var counterPart = idnumber.Substring(6, 4);

                            //NEXT 1 IS CITIZENSHIP
                            var citizenshipPart = idnumber.Substring(10, 1);

                            //NEXT 1 IS RACE (prior to 1994)
                            var racePart = idnumber.Substring(11, 1);

                            //LAST 1 IS CONTROLNUMBER
                            var controlPart = idnumber.Substring(12, 1);


                            /* CONTROL NUMBER CHECKING */
                            /******************************/

                            /*
                             * The checksum digit is calculated using the Luhn algorithm:[3]

                                A = The sum of the digits in the ID number in the odd positions (Excluding Z)
                                B = The number formed by the concatenation of the digits in the ID number in the even positions
                                C = The sum of the digits in (2 * B)
                                D = A + C
                                Z = 10 - (D mod 10)
                             */

                            int A = 0;
                            string strB = "";
                            int B = 0;
                            string strBtimes2 = "";
                            int C = 0;
                            int D = 0;
                            int Z = 0;

                            for (int odd = 0; odd <= 10; odd = odd + 2)
                            {
                                A += (int) Char.GetNumericValue(result.CleanProvidedValue[odd]);
                            }

                            for (int even = 1; even <= 12; even = even + 2)
                            {
                                strB += result.CleanProvidedValue[even].ToString();
                            }

                            B = Int32.Parse(strB);
                            strBtimes2 = (B*2).ToString();

                            for (int i = 0; i < strBtimes2.Length; i++)
                            {
                                C += (int) Char.GetNumericValue(strBtimes2[i]);
                            }

                            D = A + C;

                            Z = 10 - (D%10);

                            if (Z == (Int32.Parse(controlPart)))
                                controlOK = true;

                            /* COUNTER CHECKING */
                            /***********************/

                            /* COUNTERPART MUST BE BETWEEN 0000 AND 9999
                                * 0000 to 4999 FOR FEMALE
                                * 5000 to 9999 ODD FOR MALE
                            */
                            var counter = int.Parse(counterPart);

                            if (counter < 5000)
                            {
                                counterOK = true;
                                gender = "F"; //FEMALE
                            }
                            else
                            {
                                counterOK = true;
                                gender = "M"; //MALE
                            }

                            /* CITIZENSHIP CHECKING */
                            /*************************/

                            citizenship = Int32.Parse(citizenshipPart);
                            citizenshipOK = true;

                            /* RACE CHECKING */
                            /*************************/

                            race = Int32.Parse(racePart);
                            raceOK = true;

                            /* BIRTHDATE CHECKING */
                            /*************************/

                            //remark: system below assumes provided national id numbers contain birthdates from today or earlier, and the cutoff limit is 100 years back

                            var d = birthDatePart;
                                
                            int currentYear = DateTime.Now.Year;
                            string currentYearCentury = currentYear.ToString().Substring(0, 2);
                            d = currentYearCentury + d;

                            var format = "yyyyMMdd";

                            birthDateOK = DateTime.TryParseExact(d, format, CultureInfo.CurrentCulture,
                                DateTimeStyles.None,
                                out birthDate);

                            if (birthDate > DateTime.Now.Date)
                            {
                                birthDate = birthDate.AddYears(-100);
                            }

                            /* 4. PROCESS RESULTS */
                            /**********************/


                            if (!birthDateOK)
                            {
                                result.IsValid = false;
                                result.ValidationErrors.Add("Birthdate part not valid");
                            }

                            if (!counterOK)
                            {
                                result.IsValid = false;
                                result.ValidationErrors.Add("Counter part not valid");
                            }

                            if (!citizenshipOK)
                            {
                                result.IsValid = false;
                                result.ValidationErrors.Add("Citizenship part not valid");
                            }

                            if (!raceOK)
                            {
                                result.IsValid = false;
                                result.ValidationErrors.Add("Race part not valid");
                            }

                            if (!controlOK)
                            {
                                result.IsValid = false;
                                result.ValidationErrors.Add("Controlnumber part not valid");
                            }


                            if (birthDateOK)
                                ((NationalIDAdditionalData)(result.AdditionalData)).BirthDate = birthDate;

                            if (gender == "F")
                                ((NationalIDAdditionalData)(result.AdditionalData)).Gender = GenderEnum.Female;
                            else if (gender == "M")
                                ((NationalIDAdditionalData)(result.AdditionalData)).Gender = GenderEnum.Male;
                        }
                    }

                    if (!result.IsValid.HasValue)
                    {
                        result.IsValid = true;
                        result.ValidatedValue = idnumber;
                    }
                    else //result.IsValid == false
                    {
                        result.AdditionalData = null;
                    }
                }
                else
                    throw new Exception("Number is empty");
            }
            catch (Exception ex)
            {
                result.ValidationException = ex;

                result.IsValid = null;
                result.ValidatedValue = null;
                result.AdditionalData = null;

            }

            return result;
        }
    }
}
