using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.FormFlow;
using TogetherChatbot.Model;

namespace TogetherChatbot.Dialogs
{
    [Serializable]
    public class PreferredPaymentDueDateDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Welcome to the Preferred Payment Date Change! Your current date is 29/06/2017 and balance is 20");
            var RedemptionFormDialog = FormDialog.FromForm(this.BuildPreferredPaymentDueDateForm, FormOptions.PromptInStart);
            context.Call(RedemptionFormDialog, this.EndTask);
        }

        //private async Task ResumeAfterPreferredPaymentDueDateFormDialog(IDialogContext context, IAwaitable<PreferredPaymentDueDate> result)
        //{
        //    await context.PostAsync($"Preferred Payment Due Date process complete.");
        //}

        private async Task EndTask(IDialogContext context, IAwaitable<object> result)
        {
            context.Done<object>(result);
        }

        private IForm<PreferredPaymentDueDate> BuildPreferredPaymentDueDateForm()
        {
            OnCompletionAsyncDelegate<PreferredPaymentDueDate> processBalanceEnquiry = async (context, state) =>
            {
                var queueNumber = "Q123";
                await context.PostAsync($"Thanks for contacting us. Your case is created and assigned to respective queue. Your queue number is {queueNumber}.");
            };

            return new FormBuilder<PreferredPaymentDueDate>()
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
                .AddRemainingFields()
                .OnCompletion(processBalanceEnquiry)
                .Build();
        }
    }
}