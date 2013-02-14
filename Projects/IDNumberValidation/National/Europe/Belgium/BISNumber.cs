using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace IDNumberValidation.National.Europe.Belgium
{
    public class BISNumber : IIDNumberValidator
    {
        public string Number { get; set; }
        public bool? IsValid { get; private set; }
        public IList<string> ErrorMessages { get; private set; }
        public IList<string> InfoMessages { get; private set; }

        public DateTime? BirthDate { get; private set; }
        public string Gender { get; private set; }
        public Exception ValidationException { get; private set; }

        public BISNumber()
        {
            this.ErrorMessages = new List<string>();
        }

        public BISNumber(string number)
            : this()
        {
            this.Number = number;
        }

        public void Validate()
        {
            if (!String.IsNullOrEmpty(this.Number))
            {

                Regex nonAllowedCharacters = new Regex(@"[^0-9. -]");
                Regex nonNumeric = new Regex(@"[^0-9]");

                string bisnumber = String.Copy(this.Number);


                if (nonAllowedCharacters.IsMatch(bisnumber))
                {
                    this.ErrorMessages.Add("Non allowed character");
                    this.IsValid = false;
                }
                else
                {
                    //STRIP THE NUMBER FROM ALL NON NUMERIC CHARS
                    bisnumber = nonNumeric.Replace(bisnumber, "");

                    //LENGTH MUST BE 11 DIGITS
                    if (bisnumber.Length != 11)
                    {
                        this.ErrorMessages.Add("Length != 11");
                        this.IsValid = false;
                    }
                    else
                    {

                        try
                        {
                            bool birthDateOK = false;
                            bool counterOK = false;
                            bool controlOK = false;

                            bool unknownBirthDay = false;
                            bool born2kOrLater = false;

                            string gender = "(unknown)";
                            bool genderKnownAtTimeOfRegistration = true;

                            //FIRST 6 DIGITS ARE BIRTHDATE IN FORMAT YYMMDD
                            string birthDatePart = bisnumber.Substring(0, 6);

                            //NEXT 3 ARE COUNTER
                            string counterPart = bisnumber.Substring(6, 3);

                            //LAST 2 ARE CONTROLNUMBER
                            string controlPart = bisnumber.Substring(9, 2);



                            /* 1. CONTROL NUMBER CHECKING */
                            /******************************/

                            //CALCULATE CONTROLNUMER (= MOD 97 OF FIRST 9 DIGITS)
                            int calculatedControl = 97 - (int)(Int64.Parse(birthDatePart + counterPart) % 97);

                            if (calculatedControl != Int32.Parse(controlPart))
                            {
                                /* IF THE CALCULATED CONTROL PART IS DIFFERENT THAN THE ONE IN THE INPUTSTRING
                                    * ADD A "2" IN FRONT OF THE BIRTHDATEPART AND RECALCULATE. THIS WAS INTRODUCED TO
                                    * ALLOW BIRTHDATES OF YEAR 2000 AND LATER
                                */

                                calculatedControl = 97 - (int)(Int64.Parse("2" + birthDatePart + counterPart) % 97);

                                if (calculatedControl != Int32.Parse(controlPart))
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

                            string d = birthDatePart;
                            //BUILD THE BIRTHDATE TO CHECK
                            if (born2kOrLater == true)
                                d = "20" + birthDatePart;
                            else
                                d = "19" + birthDatePart;

                            string bYear = d.Substring(0, 4);
                            string bMonth = d.Substring(4, 2);
                            string bDay = d.Substring(6, 2);

                            int bMonthValue = Int32.Parse(bMonth);

                            bMonthValue -= 20;

                            if (bMonthValue > 12)
                            {
                                genderKnownAtTimeOfRegistration = true;
                                bMonthValue -= 20;
                            }
                            else
                            {
                                genderKnownAtTimeOfRegistration = false;
                            }

                            //END BUILD

                            string correctedBirthDateString = bYear + bMonthValue.ToString().AddLeadCharacters('0', 2) + bDay;
                            string format = "yyyyMMdd";
                            DateTime birthDate;

                            birthDateOK = DateTime.TryParseExact(correctedBirthDateString, format, CultureInfo.CurrentCulture, DateTimeStyles.None, out birthDate);

                            if (!birthDateOK)
                            {
                                //MONTH AND/OR DAY CAN BE 00 IF THESE ARE UNKNOWN. IF THIS IS THE CASE, FLAG THE BIRTHDATE AS VALID ANYWAY
                                if (d.Substring(4, 2).Equals("00") || d.Substring(6, 2).Equals("00"))
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
                            int counter = Int32.Parse(counterPart);

                            if (counter < 1 || counter > 997)
                                counterOK = false;
                            else if (counter % 2 == 0) //EVEN
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
                                this.ErrorMessages.Add("Invalid birthdate");
                            if (!counterOK)
                                this.ErrorMessages.Add("Invalid counter (= the three digits after birthdate)");
                            if (!controlOK)
                                this.ErrorMessages.Add("Invalid control number (= the last two digits)");


                            if (this.ErrorMessages.Count == 0) //NO ERRORS FOUND
                            {
                                this.IsValid = true;

                                if (!unknownBirthDay)
                                    this.BirthDate = birthDate;

                                this.Gender = gender;
                            }
                            else
                                this.IsValid = false;
                        }
                        catch (Exception ex)
                        {
                            this.ValidationException = ex;
                        }
                    }
                }
            }
            else
                this.ValidationException = new Exception("Number is empty");
        }

        public string GetErrorMessages()
        {
            if (this.ErrorMessages.Count == 0)
                return null;
            else
            {
                string s = "";

                foreach (string m in this.ErrorMessages)
                {
                    s += m + ";";
                }

                s = s.TrimEnd(';');

                return s;
            }
        }
    }
}
