using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDNumberValidation
{
    public class IdentifierParsingResult<T> where T : Identifier
    {
        public string Number { get; internal set; }
        public bool? IsValid { get; internal set; }
        public IList<T> IdentifierList { get; internal set; }

        public IdentifierParsingResult()
        {
            this.IdentifierList = new List<T>();
        }

        public T GetValidIdentifier()
        {
            var q = from i in this.IdentifierList
                    where i.IsValid.HasValue && i.IsValid == true
                    select i;

            return q.FirstOrDefault();

        }
    }
}
