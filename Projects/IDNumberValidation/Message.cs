using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDNumberValidation
{
    public class Message
    {
        public MessageType Type { get; private set; }
        public string Text { get; private set; }

        public Message(MessageType type, string text)
        {
            this.Type = type;
            this.Text = text;
        }
    }
}
