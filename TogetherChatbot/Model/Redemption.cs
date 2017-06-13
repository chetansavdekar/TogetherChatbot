﻿using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TogetherChatbot.Model
{
    public enum RequestReasons
    { InformationPurposes, LitigationAction, Porting, Refinance, SaleOfProperty, NotKnown, Other }

    public enum RecipientType
    { Customer, OurRepresentative, ThirdParty, Internal }

    public enum DeliveryMode
    { Post, SecureEmail, Fax }

    [Serializable]
    public class Redemption
    {
              
        [Prompt("Please enter your Loan Account Number: ")]
        public string LoanAccountNumber;

        //public string CustomerName;

        [Prompt("Please enter Redemption request received date: ")]
        public DateTime RedemptionRequestDate;

        [Prompt("Please provide redemption settlement date: ")]
        public DateTime SettlementDate;

        [Prompt("Please select a request reason: {||}")]
        public RequestReasons Reason;

        [Prompt("Please select a recipient type: {||}")]
        public RecipientType Recipient;

        [Prompt("Please select a delivery method: {||}")]
        public DeliveryMode DeliveryMethod;

    }
}