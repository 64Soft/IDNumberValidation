using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDNumberValidation
{
    /// <summary>
    /// If an ID number contains birth date information, it should implement this interface
    /// </summary>
    public interface IIDNumberWithBirthDateInfo
    {
        /// <summary>
        /// The birth date of the person. If empty, it is unknown or could not be parsed
        /// </summary>
        DateTime? BirthDate { get; }
    }
}
