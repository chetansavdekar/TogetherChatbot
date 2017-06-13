using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TogetherChatbot.Model
{
    [Serializable]
    public class AccountSpecificQuestions
    {
        [Prompt("Please enter your monthly installment amount: {||}")]
        public int MonthlyInstalment;

        [Prompt("Please enter your funding date: {||}")]
        public DateTime FundingDate;
    }
}