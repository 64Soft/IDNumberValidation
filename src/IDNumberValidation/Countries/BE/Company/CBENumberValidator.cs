using System;

namespace IDNumberValidation.Countries.BE.Company
{
    public class CBENumberValidator : IIDNumberValidator
    {
        public IDNumberCategoryEnum Category { get; private set; }
        public string Name { get; set; }

        public CBENumberValidator()
        {
            Category = IDNumberCategoryEnum.Company;
            Name = "BE_CBE_Number_Validator";
        }

        public IDNumberValidationResult Validate(string number)
        {

            var result = new IDNumberValidationResult(number);
            result.CleanProvidedValue = number.ToAlphaNumericOnly().TrimStart("BE").Trim();

            try
            {
                if (!string.IsNullOrEmpty(result.CleanProvidedValue))
                {
                    var cbenumber = string.Copy(result.CleanProvidedValue);

                    if (cbenumber.Length == 9) //possibly old VAT numbers consist of only 9 characters
                    {
                        cbenumber = "0" + cbenumber;
                    }

                    if (cbenumber.Length != 10)
                    {
                        result.IsValid = false;
                        result.ValidationErrors.Add("Length != 10");
                    }
                    else
                    {
                        var numberBody = cbenumber.Substring(0, 8);
                        var controlNumber = cbenumber.Substring(8, 2);

                        if (numberBody[0].Equals('9')) //must start by 0 - 8
                        {
                            result.IsValid = false;
                            result.ValidationErrors.Add("Number must start by 0 - 8");
                        }
                        else
                        {
                            //CALCULATE CONTROLNUMER (= MOD 97 OF FIRST 9 DIGITS)
                            var calculatedControl = 97 - (int)(long.Parse(numberBody) % 97);

                            if (calculatedControl != int.Parse(controlNumber))
                            {
                                result.IsValid = false;
                                result.ValidationErrors.Add("Controlnumber part not valid");
                            }
                        }
                    }

                    if (!result.IsValid.HasValue)
                    {
                        result.IsValid = true;
                        result.ValidatedValue = cbenumber;
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