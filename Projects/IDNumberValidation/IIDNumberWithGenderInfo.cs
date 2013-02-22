using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDNumberValidation
{
    public interface IIDNumberWithGenderInfo
    {
        Gender? Gender { get; }
    }
}
