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

        public IList<Message> GetMessages(MessageType messageType)
        {
            var q = from m in this.Messages
                    where m.Type == messageType
                    select m;

            return q.ToList();
        }

        public string GetMessagesAsString()
        {
            if (this.Messages.Count == 0)
                return null;
            else
            {
                StringBuilder sb = new StringBuilder();

                foreach (Message m in this.Messages)
                {
                    sb.Append(m.Type.ToString() + ": "); 
                    sb.Append(m.Text + ";");
                }

                string s = sb.ToString();
                s = s.TrimEnd(';');

                return s;
            }
        }

        public string GetMessagesAsString(MessageType messageType)
        {
            if (this.Messages.Count == 0)
                return null;
            else
            {
                StringBuilder sb = new StringBuilder();

                foreach (Message m in this.Messages.Where(m => m.Type == messageType))
                {
                    sb.Append(m.Type.ToString() + ": ");
                    sb.Append(m.Text + ";");
                }

                string s = sb.ToString();
                s = s.TrimEnd(';');

                return s;
            }
        }

        public abstract void Validate();
    }
}
