using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDNumberValidation
{
    public interface IIDNumberValidator
    {
        IDNumberCategoryEnum Category { get;  }
        string Name { get; }
        IDNumberValidationResult Validate(string number);
    }
}
