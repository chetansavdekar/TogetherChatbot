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

        //[Prompt("Please enter loan contact number: {||}")]
        //public string ContactNumber;

        [Prompt("Please enter OTP, sent on your registerd mobile number: {||}")]
        public string OTPNumber;

        //public PersonalQuestions PersonalQues;

        //public AccountSpecificQuestions AccSpecQues;

    }
}