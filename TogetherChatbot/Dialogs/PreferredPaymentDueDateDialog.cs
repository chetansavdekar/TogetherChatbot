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
            await context.PostAsync("Your preferred payment due date is 29.06.2017 and the balance amount is $110");
            var RedemptionFormDialog = FormDialog.FromForm(this.BuildPreferredPaymentDueDateForm, FormOptions.PromptInStart);
            context.Call(RedemptionFormDialog, this.EndTask);
        }

        private async Task EndTask(IDialogContext context, IAwaitable<object> result)
        {
            context.Done<object>(result);
        }

        private IForm<PreferredPaymentDate> BuildPreferredPaymentDueDateForm()
        {
            OnCompletionAsyncDelegate<PreferredPaymentDate> processBalanceEnquiry = async (context, state) =>
            {
                var queueNumber = "R123";
                await context.PostAsync($"Your prefered payment due date has been changed. Your reference number is {queueNumber}.");
            };

            return new FormBuilder<PreferredPaymentDate>()
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