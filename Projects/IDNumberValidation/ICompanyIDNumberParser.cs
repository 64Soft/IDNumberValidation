using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDNumberValidation
{
    /// <summary>
    /// Interface to allow parsing of a given company identifier number
    /// </summary>
    public interface ICompanyIDNumberParser
    {
        /// <summary>
        /// Parses the given company ID number
        /// </summary>
        /// <param name="number">The number to parse</param>
        /// <returns>An IdentifierParsingResult object containing a list of CompanyIdentifier objects.</returns>
        IdentifierParsingResult<CompanyIdentifier> ParseCompanyNumber(string number);
    }
}
