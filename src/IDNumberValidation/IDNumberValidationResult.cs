using System;
using System.Collections.Generic;
using IDNumberValidation.AdditionalDataInterfaces;

namespace IDNumberValidation
{
    public class IDNumberValidationResult
    {
        public IDNumberValidationResult(string providedValue)
        {
            ProvidedValue = providedValue;
        }

        /// <summary>
        ///     The provide number value
        /// </summary>
        public string ProvidedValue { get; private set; }

        /// <summary>
        ///     Returns a clean version of the provided value (depending on the number format)
        /// </summary>
        public string CleanProvidedValue { get; set; }

        /// <summary>
        ///     Indicates if the number has passed validation.
        ///     A null value means the validation logic has not been run yet
        /// </summary>
        public bool? IsValid { get; set; }

        /// <summary>
        ///     Collection containing error messages detected during validation
        /// </summary>
        public IList<string> ValidationErrors { get; set; }

        /// <summary>
        ///     Contains a possible exception that was thrown during validation
        /// </summary>
        public Exception ValidationException { get; set; }

        /// <summary>
        ///     The validated value as it should be officially formatted
        /// </summary>
        public string ValidatedValue { get; set; }

        public IAdditionalData AdditionalData { get; set; }
  
    }
}