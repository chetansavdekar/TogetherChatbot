using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TogetherChatbot.Model
{
    [Serializable]
    public class PreferredPaymentDate
    {
        [Prompt("Please enter a new prefered payment date in days (e.g. 5):")]
        public int PaymentDueDate;
    }
}