using System.Collections.Generic;

namespace IDNumberValidation
{
    public interface IIDNumberValidator
    {
        /// <summary>
        /// The number to validate.
        /// </summary>
        string Number { get; set; }

        /// <summary>
        /// Indicates if the number has passed validation.
        /// A null value means the validation logic has not been run yet.
        /// </summary>
        bool? IsValid { get; }

        /// <summary>
        /// Collection containing informative messages about the validated number.
        /// </summary>
        IList<string> InfoMessages { get; }

        /// <summary>
        /// Collection containing error messages describing the reason(s) why the number is invalid.
        /// </summary>
        IList<string> ErrorMessages { get; }

        /// <summary>
        /// Validates the number.
        /// </summary>
        void Validate();
    }
}
