using System;
using System.Collections.Generic;

namespace IDNumberValidation
{
    /// <summary>
    /// An ID number validator class should implement this interface
    /// </summary>
    public interface IIDNumberValidator
    {
        /// <summary>
        /// Descriptive name of the type of the identifier
        /// </summary>
        string IdentifierType { get; }

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
        /// Collection containing messages describing the results of the validation.
        /// </summary>
        IList<Message> Messages { get; }

        /// <summary>
        /// Contains a possible exception that was thrown during validation
        /// </summary>
        Exception ValidationException { get; }

        /// <summary>
        /// Validates the number.
        /// </summary>
        void Validate();
    }
}
