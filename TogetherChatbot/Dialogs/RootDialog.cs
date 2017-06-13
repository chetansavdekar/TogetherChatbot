using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.FormFlow;
using TogetherChatbot.Model;
using System.Threading;

namespace TogetherChatbot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        private const string RedemptionOption = "Redemption";
        private const string BalanceOption = "Balance";
        private const string PrerferedPaymentDueDateOption = "Preferred Payment Due Date";

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(this.MessageReceivedAsync);
        }
        
        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            if ((message.Text.ToLower().Contains("hi")) || message.Text.ToLower().Contains("hello"))
            {
                await context.PostAsync("Hello there!");
                this.ShowOptions(context);
            }
        }

        private void ShowOptions(IDialogContext context)
        {
            PromptDialog.Choice(context, this.OnOptionSelected, new List<string>() { RedemptionOption, BalanceOption, PrerferedPaymentDueDateOption }, "Please select from below option to help you better", "Not a valid option", 3);
        }

        private async Task OnOptionSelected(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                string optionSelected = await result;

                switch (optionSelected)
                {
                    case RedemptionOption:
                        context.Call(new RedemptionProcessDialog(), this.ResumeAfterOptionDialog);
                        break;

                    case BalanceOption:
                        context.Call(new BalanceEnquiryDialog(), this.ResumeAfterOptionDialog);
                        break;

                    case PrerferedPaymentDueDateOption:
                        context.Call(new PreferredPaymentDueDateDialog(), this.ResumeAfterOptionDialog);
                        break;
                }
            }
            catch (TooManyAttemptsException ex)
            {
                await context.PostAsync($"Ooops! Too many attemps :(. But don't worry, I'm handling that exception and you can try again!");

                context.Wait(this.MessageReceivedAsync);
            }
        }

        private async Task ResumeAfterOptionDialog(IDialogContext context, IAwaitable<object> result)
        {
            try
            {
                var message = await result;
            }
            catch (Exception ex)
            {
                await context.PostAsync($"Failed with message: {ex.Message}");
            }
            finally
            {
                context.Wait(this.MessageReceivedAsync);
            }
        }
    }
}