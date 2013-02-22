using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDNumberValidation
{
    public abstract class Identifier : IIDNumberValidator
    {
        public string IdentifierType { get; private set; }
        public string Number { get; set; }
        public bool? IsValid { get; protected set; }
        public IList<Message> Messages { get; private set; }
        public Exception ValidationException { get; protected set; }

        public Identifier(string identifierType, string number)
        {
            this.IdentifierType = identifierType;
            this.Number = number;
            this.Messages = new List<Message>();

        }

        public abstract void Validate();
    }
}
