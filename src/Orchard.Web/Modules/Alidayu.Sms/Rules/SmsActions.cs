using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Events;
using Orchard.Messaging.Services;
using Orchard;
using Orchard.Security;
using Orchard.Localization;
using Orchard.ContentManagement;
using Orchard.Core.Common.Models;
using Orchard.Messaging.Events;
using Orchard.Messaging.Models;
using Alidayu.Sms.Models;

namespace Contrib.Sms.Rules
{
    public interface IActionProvider : IEventHandler
    {
        void Describe(dynamic describe);
    }


    public class SmsActions : IActionProvider
    {
        private readonly IMessageManager messageManager;
        private readonly IOrchardServices orchardServices;
        private readonly IMembershipService membershipService;
        public const string MessageType = "ActionSms";

        public Localizer T { get; set; }

        public SmsActions(IMessageManager messageManager, IOrchardServices orchardServices, IMembershipService membershipService) 
        {
            this.messageManager = messageManager;
            this.orchardServices = orchardServices;
            this.membershipService = membershipService;
            T = NullLocalizer.Instance;
        }

        public void Describe(dynamic describe)
        {
            Func<dynamic, LocalizedString> display = context => T("Send an SMS");

            describe.For("Messaging", T("Messaging"), T("Messages"))
                .Element(
                    "SendSms", T("Send SMS"), T("Sends an SMS to a specific user."), (Func<dynamic, bool>)Send,
                    display, "ActionSms");
        }

        private bool Send(dynamic context)
        {
            var recipient = context.Properties["Recipient"];
            var properties = new Dictionary<string, string>(context.Properties);

            if (recipient == "owner")
            {
                var content = context.Tokens["Content"] as IContent;
                if (content.Has<CommonPart>())
                {
                    var owner = content.As<CommonPart>().Owner;
                    if (owner != null && owner.ContentItem != null && owner.ContentItem.Record != null)
                    {
                        messageManager.Send(owner.ContentItem.Record, MessageType, "sms", properties);
                    }
                    
                    messageManager.Send(SplitNumbers(owner.As<MobilePart>().MobileNumber), MessageType, "sms", properties);
                }
            }
            else if (recipient == "author")
            {
                var user = orchardServices.WorkContext.CurrentUser.As<MobilePart>();

                // can be null if user is anonymous
                if (user != null && String.IsNullOrWhiteSpace(user.MobileNumber))
                {
                    messageManager.Send(user.ContentItem.Record, MessageType, "sms", properties);
                }
            }
            else if (recipient == "admin")
            {
                var username = orchardServices.WorkContext.CurrentSite.SuperUser;
                var user = membershipService.GetUser(username).As<MobilePart>();

                 // can be null if user is no super user is defined
                if (user != null && !String.IsNullOrWhiteSpace(user.MobileNumber))
                {
                    messageManager.Send(user.ContentItem.Record, MessageType, "sms", properties);
                }
            }
            else if (recipient == "other")
            {
                var mobile = properties["RecipientOther"];
                messageManager.Send(SplitNumbers(mobile), MessageType, "sms", properties);
            }
            return true;
        }

        private static IEnumerable<string> SplitNumbers(string commaSeparated)
        {
            return commaSeparated.Split(new[] { ',', ';' });
        }
    }

    public class SmsActionsHandler : IMessageEventHandler
    {
        public SmsActionsHandler()
        {
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public void Sending(MessageContext context)
        {
            if (context.MessagePrepared)
                return;

            if ((context.Recipients == null || !context.Recipients.Any()) &&
                (context.Addresses == null || !context.Addresses.Any()))
            {
                return;
            }

            switch (context.Type)
            {
                case SmsActions.MessageType:

                    //context.MailMessage.Subject = context.Properties["Subject"];
                    context.MailMessage.Body = context.Properties["Body"];
                    //FormatEmailBody(context);
                    context.MessagePrepared = true;
                    break;
            }
        }

        //private static void FormatEmailBody(MessageContext context)
        //{
        //    context.MailMessage.Body = "<p style=\"font-family:Arial, Helvetica; font-size:10pt;\">" + context.MailMessage.Body + "</p>";
        //}

        public void Sent(MessageContext context)
        {
        }
    }
}