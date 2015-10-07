using System;

namespace IDNumberValidation.AdditionalDataInterfaces
{
    /// <summary>
    /// If an ID number contains birth date information, it should implement this interface
    /// </summary>
    public interface IHasBirthDate
    {
        /// <summary>
        /// The birth date of a person. If empty, it is unknown or could not be parsed
        /// </summary>
        DateTime? BirthDate { get; }
    }
}
