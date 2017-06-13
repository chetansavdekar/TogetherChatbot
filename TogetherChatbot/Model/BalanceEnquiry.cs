using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TogetherChatbot.Model
{
    [Serializable]
    public class BalanceEnquiry
    {
        [Prompt("Please enter your loan account number: {||}")]
        public string LoanAccountNumber;

        [Prompt("Please enter loan contact number: {||}")]
        public string ContactNumber;

        public PersonalQuestions PersonalQues;

        public AccountSpecificQuestions AccSpecQues;

    }
}