using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDNumberValidation
{
    public abstract class CompanyIdentifier : Identifier
    {
       
        public CompanyIdentifier(string identifierType, string number)
            : base(identifierType, number)
        {
            
        }
    }
}
