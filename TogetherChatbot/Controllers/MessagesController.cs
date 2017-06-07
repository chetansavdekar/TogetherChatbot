﻿using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;

using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using TogetherChatbot.Model;

namespace TogetherChatbot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                await InitiateTogetherActivity(activity);
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private static async Task InitiateTogetherActivity(Activity activity)
        {
            //string[] strArray = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };
            ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));

            if ((activity.Text.ToLower().Equals("hi")) || activity.Text.ToLower().Equals("hello"))
            {
                Activity reply = activity.CreateReply("Hello there! How can I help you today?");
                await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else if (activity.Text.ToLower().Contains("ensure") || activity.Text.ToLower().Contains("call"))
            {
                Activity reply = activity.CreateReply("Noted! Is there anything else I can help you with?");
                await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else if ((activity.Text.ToLower().Contains("nope")) || activity.Text.ToLower().Contains("ok") || (activity.Text.ToLower().Contains("thanks")) || (activity.Text.ToLower().Contains("thank")) || (activity.Text.ToLower().Contains("nothing")))
            {
                Activity reply = activity.CreateReply("Ok. Have a good day!");
                await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else if (activity.Text.ToLower().Contains("complaint") || activity.Text.ToLower().Contains("notice") || activity.Text.ToLower().Contains("arrears") || activity.Text.ToLower().Contains("credit") || activity.Text.ToLower().Contains("rating"))
            {
                if (activity.Text.ToLower().Contains("complaint"))
                {
                    Activity reply = activity.CreateReply("Please tell me. How can i help you?");
                    await connector.Conversations.ReplyToActivityAsync(reply);
                }
                else if (activity.Text.ToLower().Contains("notice") || activity.Text.ToLower().Contains("arrears") || activity.Text.ToLower().Contains("credit"))
                {
                    if (activity.Text.ToLower().Contains("receive"))
                    {
                        Activity reply = activity.CreateReply("We will need to discuss your account in order to answer your query. Find out how to contact us to discuss your account at https://togethermoney.com/get-in-touch/personal-lending/.");
                        await connector.Conversations.ReplyToActivityAsync(reply);
                    }
                    else if (activity.Text.ToLower().Contains("affect") || activity.Text.ToLower().Contains("impact") || activity.Text.ToLower().Contains("credit") || activity.Text.ToLower().Contains("rating"))
                    {
                        Activity reply = activity.CreateReply("No, the Notice of Arrears letter simply details any outstanding instalments on your mortgage/loan account, and in itself has no impact on your credit rating. However, having late or missed payments on your mortgage/loan account will affect your credit rating.");
                        await connector.Conversations.ReplyToActivityAsync(reply);
                    }
                }

            }
            else 
            {
                await Conversation.SendAsync(activity, MakeLoanDialog);
            }
        }

        internal static IDialog<Loans> MakeLoanDialog()
        {
            return Chain.From(() => FormDialog.FromForm(Loans.BuildForm));
        }


        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}