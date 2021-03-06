﻿using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.FormFlow;
using TogetherChatbot.Model;
using Microsoft.Bot.Builder.FormFlow.Advanced;

namespace TogetherChatbot.Dialogs
{
    [Serializable]
    public class BalanceEnquiryDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            //await context.PostAsync("Welcome to the Balance enquiry!");
            var balnaceEnquiryDialog = FormDialog.FromForm(this.BuildBalanceEnquiryForm, FormOptions.PromptInStart);
            context.Call(balnaceEnquiryDialog, this.EndTask);
        }

        private async Task EndTask(IDialogContext context, IAwaitable<object> result)
        {
            context.Done<object>(result);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
        }

        private IForm<BalanceEnquiry> BuildBalanceEnquiryForm()
        {
            List<string> LoanParty = new List<string> { "Paul", "Smith", "Taylor" };

            OnCompletionAsyncDelegate<BalanceEnquiry> processBalanceEnquiry = async (context, state) =>
            {
                var amount = "110,000";
                await context.PostAsync($"Your outstanding loan balance is £ {amount}.");
            };

            return new FormBuilder<BalanceEnquiry>()
                //.Field(nameof(BalanceEnquiry.LoanAccountNumber))
                //.Field(new FieldReflector<BalanceEnquiry>(nameof(BalanceEnquiry.ContactNumber))
                //.SetType(null)
                //.SetDefine((state, field) =>
                //{
                //    foreach (var contactNo in this.LoanContactNumbers)
                //        field
                //            .AddDescription(contactNo, contactNo)
                //            .AddTerms(contactNo, contactNo);
                //    return System.Threading.Tasks.Task.FromResult(true);
                //}))
                //.Field(nameof(BalanceEnquiry.PersonalQues))
                //.Field(nameof(BalanceEnquiry.AccSpecQues))
                .Field(nameof(BalanceEnquiry.LoanAccountNumber))
                .Field(
                    new FieldReflector<BalanceEnquiry>(nameof(BalanceEnquiry.Party))
                    .SetType(null)
                    .SetDefine((state, field) =>
                    {
                        foreach (var party in LoanParty)
                            field
                                .AddDescription(party, party)
                                .AddTerms(party, party);

                        return Task.FromResult(true);
                    })
                )
                //.Field(nameof(BalanceEnquiry.Party))
                .Message("OTP has been sent to the registered mobile number of the selected loan party.")
                .Field(nameof(BalanceEnquiry.OTPNumber), "Please enter the OTP: {||}")
                //.AddRemainingFields()
                .OnCompletion(processBalanceEnquiry)
                .Build();
        }
    }
}