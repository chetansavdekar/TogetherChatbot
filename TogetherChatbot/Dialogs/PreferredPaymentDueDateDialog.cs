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
            await context.PostAsync("Your preferred payment due date is " + DateTime.Now.ToString("dd/MM/yyyy") + ".");
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
                //var queueNumber = "R12345";
                await context.PostAsync($"Your preferred payment due date has been changed to "+ DateTime.Now.AddDays(state.PaymentDueDate).ToString("dd/MM/yyyy") + ", and your outstanding balance is £250.");
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