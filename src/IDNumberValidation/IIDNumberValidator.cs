using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDNumberValidation
{
    /// <summary>
    /// Interface to be implemented by each validator
    /// </summary>
    public interface IIDNumberValidator
    {
        IDNumberCategoryEnum Category { get;  }
        string Name { get; }
        IDNumberValidationResult Validate(string number);
    }
}
