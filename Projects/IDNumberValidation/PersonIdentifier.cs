using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDNumberValidation
{
    public abstract class PersonIdentifier : Identifier
    {
        public PersonIdentifier(string identifierType, string number)
            : base(identifierType, number)
        {
            
        }

        
    }
}
