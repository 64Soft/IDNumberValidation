using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace IDNumberValidation.National.Europe.Belgium
{
    public class CBENumber : CompanyIdentifier
    {
        public CBENumber(string number) : base("Belgian CBE Number", number)
        {  }

        public override void Validate()
        {

            if (!String.IsNullOrEmpty(this.Number))
            {

                try
                {

                    //Regex nonAllowedCharacters = new Regex(@"[^0-9. -]"); --> TO DEFINE FOR BE PREFIX !
                    Regex nonNumeric = new Regex(@"[^0-9]");

                    string kbonumber = String.Copy(this.Number);

                    //STRIP THE NUMBER FROM ALL NON NUMERIC CHARS
                    kbonumber = nonNumeric.Replace(kbonumber, "");

                    if (kbonumber.Length != 10)
                    {
                        this.Messages.Add(new Message(MessageType.Error, "Length != 10"));
                        this.IsValid = false;
                    }
                    else
                    {
                        string numberBody = kbonumber.Substring(0, 8);
                        string controlNumber = kbonumber.Substring(8, 2);

                        if (!numberBody[0].Equals('0') && !numberBody[0].Equals('1'))
                        {
                            this.Messages.Add(new Message(MessageType.Error, "First digit must be 0 or 1"));
                            this.IsValid = false;
                        }
                        else
                        {
                            //CALCULATE CONTROLNUMER (= MOD 97 OF FIRST 9 DIGITS)
                            int calculatedControl = 97 - (int)(Int64.Parse(numberBody) % 97);

                            if (calculatedControl != Int32.Parse(controlNumber))
                            {
                                this.Messages.Add(new Message(MessageType.Error, "Invalid controlnumber"));
                                this.IsValid = false;
                            }
                        }
                    }

                    if (!this.IsValid.HasValue) //NOT SET TO FALSE, THUS NO ERRORS FOUND
                    {
                        this.IsValid = true;
                    }

                }
                catch (Exception ex)
                {
                    this.ValidationException = ex;
                }

            }
            else
                this.ValidationException = new Exception("Number is empty");
        }
    }
}
