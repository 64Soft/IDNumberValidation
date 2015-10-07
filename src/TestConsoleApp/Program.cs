using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDNumberValidation;
using IDNumberValidation.AdditionalDataInterfaces;
using IDNumberValidation.Countries.ZA.Person;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string number = "8511015050088";

            IIDNumberValidator nrNumberValidator = new NationalIDValidator();
            var result = nrNumberValidator.Validate(number);

            Console.WriteLine("Provided Number: " + result.ProvidedValue);
            Console.WriteLine("Clean Number: " + result.CleanProvidedValue);
            Console.WriteLine("Valid: " + result.IsValid);

            if (result.IsValid.HasValue && result.IsValid.Value)
            {
                Console.WriteLine("Official Number: " + result.ValidatedValue);
            }

            if(result.AdditionalData is IHasBirthDate)
                Console.WriteLine("BirthDate: " + ((IHasBirthDate)(result.AdditionalData)).BirthDate);

            if (result.AdditionalData is IHasGender)
                Console.WriteLine("Gender: " + ((IHasGender)(result.AdditionalData)).Gender);

            if (result.ValidationErrors.Any())
            {
                Console.WriteLine("Errors: ");
                foreach (var error in result.ValidationErrors)
                    Console.WriteLine(error);
            }

            if (result.ValidationException != null)
            {
                Console.WriteLine("Validation exception: " + result.ValidationException.Message);
            }
            

            Console.WriteLine("Press any key to continue...");
            Console.Read();
        }
    }
}
