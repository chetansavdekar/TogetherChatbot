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
            //await context.PostAsync("Welcome to the Redemption!");
            var redemptionFormDialog = FormDialog.FromForm(this.BuildRedemptionForm, FormOptions.PromptInStart);
            context.Call(redemptionFormDialog, this.EndTask);
        }

        private async Task EndTask(IDialogContext context, IAwaitable<object> result)
        {
            context.Done<object>(result);
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
                var queueNumber = "R12345";
                await context.PostAsync($"Your redemption request has been created and your reference number is {queueNumber}. Our customer service representative will get in touch with you shortly.");
            };

            return new FormBuilder<Redemption>()
                .AddRemainingFields()
                .OnCompletion(processRedemption)
                .Build();
        }
    }
}