using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TogetherChatbot.Model
{
    [Serializable]
    public class PersonalQuestions
    {
        [Prompt("Please enter first line of your correspondence address: {||}")]
        public string CorrespondenceAddress;

        [Prompt("Please enter postcode of your correspondence address: {||}")]
        public string Postcode;

        [Prompt("Please enter your birth date: {||}")]
        public DateTime BirthDate;
    }
}