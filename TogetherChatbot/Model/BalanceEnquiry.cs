using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TogetherChatbot.Model
{
    //public enum PartyNames
    //{ Smith =1, Taylor =2, Paul=3 }

    [Serializable]
    public class BalanceEnquiry
    {
        [Prompt("Please enter your Loan Account Number: {||}")]
        public string LoanAccountNumber;

        [Prompt("Please select a loan party: {||}")]
        public string Party;
        //public PartyNames Party;


        //[Prompt("Please enter loan contact number: {||}")]
        //public string ContactNumber;


        //[Prompt("Please enter the OTP: {||}")]
        public string OTPNumber;

        //public PersonalQuestions PersonalQues;

        //public AccountSpecificQuestions AccSpecQues;

    }
}