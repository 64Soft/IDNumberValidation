using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDNumberValidation
{
    public interface IIDNumberWithBirthDateInfo
    {
        DateTime? BirthDate { get; }
    }
}
