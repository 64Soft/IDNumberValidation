using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDNumberValidation
{
    /// <summary>
    /// If an ID number contains gender information, it should implement this interface
    /// </summary>
    public interface IIDNumberWithGenderInfo
    {
        /// <summary>
        /// The gender of the person. If empty, it is unknown or could not be parsed
        /// </summary>
        Gender? Gender { get; }
    }
}
