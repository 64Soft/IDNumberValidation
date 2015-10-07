using System;
using System.Globalization;
using System.Text.RegularExpressions;
using IDNumberValidation.AdditionalDataInterfaces;

namespace IDNumberValidation.Countries.BE.Person
{
    public class NRNumberValidator : IIDNumberValidator
    {
        
        public IDNumberCategoryEnum Category { get; private set; }
        public string Name { get; private set; }

        public NRNumberValidator()
        {
            Category = IDNumberCategoryEnum.Person;
            Name = "BE_NR_Number_Validator";
        }

        public IDNumberValidationResult Validate(string number)
        {
            var result = new IDNumberValidationResult(number);
            result.AdditionalData = new NRNumberAdditionalData();

            result.CleanProvidedValue = number.ToAlphaNumericOnly().TrimStart("BE").TrimStart("be").Trim();

            try
            {
                if (!string.IsNullOrEmpty(result.CleanProvidedValue))
                {
                    var nonAllowedCharacters = new Regex(@"[^0-9. -]");

                    var rrnumber = string.Copy(result.CleanProvidedValue);

                    if (nonAllowedCharacters.IsMatch(rrnumber))
                    {
                        result.IsValid = false;
                        result.ValidationErrors.Add("Number contains non-allowed character");
                    }
                    else
                    {
                        //LENGTH MUST BE 11 DIGITS
                        if (rrnumber.Length != 11)
                        {
                            result.IsValid = false;
                            result.ValidationErrors.Add("Length != 11");
                        }
                        else
                        {
                            var birthDateOK = false;
                            var counterOK = false;
                            var controlOK = false;

                            var unknownBirthDay = false;
                            var born2kOrLater = false;

                            var gender = "(unknown)";

                            //FIRST 6 DIGITS ARE BIRTHDATE IN FORMAT YYMMDD
                            var birthDatePart = rrnumber.Substring(0, 6);

                            //NEXT 3 ARE COUNTER
                            var counterPart = rrnumber.Substring(6, 3);

                            //LAST 2 ARE CONTROLNUMBER
                            var controlPart = rrnumber.Substring(9, 2);


                            /* 1. CONTROL NUMBER CHECKING */
                            /******************************/

                            //CALCULATE CONTROLNUMER (= MOD 97 OF FIRST 9 DIGITS)
                            var calculatedControl = 97 - (int) (long.Parse(birthDatePart + counterPart)%97);

                            if (calculatedControl != int.Parse(controlPart))
                            {
                                /* IF THE CALCULATED CONTROL PART IS DIFFERENT THAN THE ONE IN THE INPUTSTRING
                                    * ADD A "2" IN FRONT OF THE BIRTHDATEPART AND RECALCULATE. THIS WAS INTRODUCED TO
                                    * ALLOW BIRTHDATES OF YEAR 2000 AND LATER
                                */

                                calculatedControl = 97 - (int) (long.Parse("2" + birthDatePart + counterPart)%97);

                                if (calculatedControl != int.Parse(controlPart))
                                {
                                    /* THE CALCULATION STILL DOESN'T MATCH THE CONTROLNUMER, SO THIS IS AN INVALID
                                        * REGISTRY NUMBER
                                    */

                                    controlOK = false;
                                }
                                else
                                {
                                    born2kOrLater = true;
                                    controlOK = true;
                                }
                            }
                            else
                                controlOK = true;


                            /* 2. BIRTHDATE CHECKING */
                            /*************************/

                            var d = birthDatePart;
                            //BUILD THE BIRTHDATE TO CHECK
                            if (born2kOrLater)
                                d = "20" + d;
                            else
                                d = "19" + d;
                            //END BUILD

                            var format = "yyyyMMdd";
                            DateTime birthDate;

                            birthDateOK = DateTime.TryParseExact(d, format, CultureInfo.CurrentCulture,
                                DateTimeStyles.None,
                                out birthDate);

                            if (!birthDateOK)
                            {
                                //MONTH AND/OR DAY CAN BE 00 IF THESE ARE UNKNOWN. IF THIS IS THE CASE, FLAG THE BIRTHDATE AS VALID ANYWAY
                                if (birthDatePart.Substring(2, 2).Equals("00") ||
                                    birthDatePart.Substring(4, 2).Equals("00"))
                                {
                                    unknownBirthDay = true;
                                    birthDateOK = true;
                                }
                            }


                            /* 3. COUNTER CHECKING */
                            /***********************/

                            /* COUNTERPART MUST BE BETWEEN 001 AND 997
                                * EVEN FOR FEMALE
                                * ODD FOR MALE
                            */
                            var counter = int.Parse(counterPart);

                            if (counter < 1 || counter > 997)
                                counterOK = false;
                            else if (counter%2 == 0) //EVEN
                            {
                                counterOK = true;
                                gender = "F"; //FEMALE
                            }
                            else
                            {
                                counterOK = true;
                                gender = "M"; //MALE
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

                            if (!controlOK)
                            {
                                result.IsValid = false;
                                result.ValidationErrors.Add("Controlnumber part not valid");
                            }
                                

                            if (!unknownBirthDay && birthDateOK)
                                ((NRNumberAdditionalData)(result.AdditionalData)).BirthDate = birthDate;

                            if (gender == "F")
                                ((NRNumberAdditionalData)(result.AdditionalData)).Gender = GenderEnum.Female;
                            else if (gender == "M")
                                ((NRNumberAdditionalData)(result.AdditionalData)).Gender = GenderEnum.Male;
                        }
                    }

                    if (!result.IsValid.HasValue)
                    {
                        result.IsValid = true;
                        result.ValidatedValue = rrnumber;
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