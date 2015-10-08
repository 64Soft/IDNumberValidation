using System;

namespace IDNumberValidation.AdditionalDataInterfaces
{
    /// <summary>
    /// Indicates that an ID number contains birth date information of a person
    /// </summary>
    public interface IHasBirthDate
    {
        /// <summary>
        /// The birth date of a person. If empty, it is unknown or could not be parsed
        /// </summary>
        DateTime? BirthDate { get; set; }
    }
}
