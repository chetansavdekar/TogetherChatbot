using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using TogetherChatbot.Model;
using Microsoft.Bot.Builder.FormFlow.Advanced;

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
            List<string> RequestReasons = new List<string> { "Information Purposes","Litigation Action","Porting","Refinance","Sale Of Property","Not Known","Other" };
            List<string> RecipientTypes = new List<string> { "Customer", "Our Representative", "Third Party" };
            List<string> DeliveryModes = new List<string> { "Post", "Secure Email", "Fax" };

            OnCompletionAsyncDelegate<Redemption> processRedemption = async (context, state) =>
            {   
                // call web service, post an objet objRedemption would be state to create a queue 
                var queueNumber = "R12345";
                await context.PostAsync($"Your redemption request has been created and your reference number is {queueNumber}. Our customer service representative will get in touch with you shortly.");
            };

            return new FormBuilder<Redemption>()
                 .Field(nameof(Redemption.LoanAccountNumber))
                 .Field(nameof(Redemption.RedemptionRequestDate))
                 .Field(nameof(Redemption.SettlementDate))
                 .Field(
                    new FieldReflector<Redemption>(nameof(Redemption.Reason))
                    .SetType(null)
                    .SetDefine((state, field) =>
                    {
                        foreach (var reason in RequestReasons)
                            field
                                .AddDescription(reason, reason)
                                .AddTerms(reason, reason);

                        return Task.FromResult(true);
                    })
                )

                .Field(
                    new FieldReflector<Redemption>(nameof(Redemption.Recipient))
                    .SetType(null)
                    .SetDefine((state, field) =>
                    {
                        foreach (var recipient in RecipientTypes)
                            field
                                .AddDescription(recipient, recipient)
                                .AddTerms(recipient, recipient);

                        return Task.FromResult(true);
                    })
                )

                .Field(
                    new FieldReflector<Redemption>(nameof(Redemption.DeliveryMethod))
                    .SetType(null)
                    .SetDefine((state, field) =>
                    {
                        foreach (var deliverymode in DeliveryModes)
                            field
                                .AddDescription(deliverymode, deliverymode)
                                .AddTerms(deliverymode, deliverymode);

                        return Task.FromResult(true);
                    })
                )
                //.AddRemainingFields()
                .OnCompletion(processRedemption)
                .Build();
        }
    }
}