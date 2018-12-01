using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MessengerChatBot.Models
{
    public class NlpEntity
    {
        public double Confidence { get; set; }
        public string Value { get; set; }
    }

    public class Sentiment : NlpEntity
    {
    }

    public class Greeting : NlpEntity
    {
    }

    public class Email : NlpEntity
    {

    }
}