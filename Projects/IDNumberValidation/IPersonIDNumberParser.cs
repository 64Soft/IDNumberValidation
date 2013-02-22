using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDNumberValidation
{
    /// <summary>
    /// Interface to allow parsing of a given person identifier number
    /// </summary>
    public interface IPersonIDNumberParser
    {
        /// <summary>
        /// Parses the given person ID number
        /// </summary>
        /// <param name="number">The number to parse</param>
        /// <returns>An IdentifierParsingResult object containing a list of PersonIdentifier objects.</returns>
        IdentifierParsingResult<PersonIdentifier> ParsePersonNumber(string number);
    }
}
