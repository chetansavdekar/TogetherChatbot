using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TogetherChatbot.Model
{
    [Serializable]
    public class PreferredPaymentDueDate
    {
        [Prompt("Please enter your preferred payment due date: {||}")]
        public DateTime PreferredPaymnetDueDate;

        //[Prompt("Please enter contact number: {||}")]
        //public string ContactNumber;

        //public PersonalQuestions PersonalQues;

        //public AccountSpecificQuestions AccSpecQues;
    }
}