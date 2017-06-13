using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.FormFlow;
using TogetherChatbot.Model;

namespace TogetherChatbot.Dialogs
{
    [Serializable]
    public class BalanceEnquiryDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Welcome to the Balance enquiry!");
            var balnaceEnquiryDialog = FormDialog.FromForm(this.BuildBalanceEnquiryForm, FormOptions.PromptInStart);
            context.Call(balnaceEnquiryDialog, this.ResumeAfterBalanceEnquiryFormDialog);
        }

        private async Task ResumeAfterBalanceEnquiryFormDialog(IDialogContext context, IAwaitable<BalanceEnquiry> result)
        {
            await context.PostAsync($"Balance enquiry process complete.");
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
        }

        private IForm<BalanceEnquiry> BuildBalanceEnquiryForm()
        {
            OnCompletionAsyncDelegate<BalanceEnquiry> processBalanceEnquiry = async (context, state) =>
            {
                var queueNumber = "Q123";
                await context.PostAsync($"Thanks for contacting us. Your case is created and assigned to respective queue. Your queue number is {queueNumber}.");
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
                .AddRemainingFields()
                .OnCompletion(processBalanceEnquiry)
                .Build();
        }
    }
}