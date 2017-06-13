using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using TogetherChatbot.Model;

namespace TogetherChatbot.Dialogs
{
    [Serializable]
    public class RedemptionProcessDialog : IDialog<object>
    {
        public Redemption objRedemption;
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Welcome to the Redemption!");
            var redemptionFormDialog = FormDialog.FromForm(this.BuildRedemptionForm, FormOptions.PromptInStart);
            context.Call(redemptionFormDialog, this.ResumeAfterRedemptionFormDialog);
        }

        private  Task ResumeAfterRedemptionFormDialog(IDialogContext context, IAwaitable<Redemption> result)
        {
            //await context.PostAsync($"Redemption process complete.");
            return Task.CompletedTask;
        }      

        private IForm<Redemption> BuildRedemptionForm()
        {
            OnCompletionAsyncDelegate<Redemption> processRedemption = async (context, state) =>
            {
                //this.objRedemption.RedemptionRequestDate = state.RedemptionRequestDate;
                //this.objRedemption.SettlementDate = state.SettlementDate;
                //this.objRedemption.Reason = state.Reason;
                //this.objRedemption.Recipient = state.Recipient;
                //this.objRedemption.DeliveryMethod = state.DeliveryMethod;

                // call web service, post an objet objRedemption would be state to create a queue 
                var queueNumber = "Q123";
                await context.PostAsync($"Thanks for contacting us. Your case is created and assigned to respective queue. Your queue number is {queueNumber}.");
            };

            return new FormBuilder<Redemption>()
                .Field(nameof(Redemption.RedemptionRequestDate))
                .Field(nameof(Redemption.SettlementDate))
                .Field(nameof(Redemption.Reason))
                .Field(nameof(Redemption.Recipient))
                .Field(nameof(Redemption.DeliveryMethod))
                .OnCompletion(processRedemption)
                .Build();
        }
    }
}