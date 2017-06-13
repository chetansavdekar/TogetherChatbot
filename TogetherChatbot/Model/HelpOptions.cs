using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TogetherChatbot.Model
{
    public enum BasicOptions
    { Test, RedeemLoan, BalanceEnquiry, PreferedPaymentDueDate, PaymentPlan }

    [Serializable]
    public class HelpOptions
    {
        [Prompt("Please select from the below options to help you better: {||}")]
        public BasicOptions Options;
    }
}