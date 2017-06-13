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
        [Prompt("Please enter new prefered payment date (e.g. 15 days): {||}")]
        public string PaymentDueDate;
    }
}