using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Messaging.Events;
using Orchard.ContentManagement;
using Orchard.Messaging.Models;
using Orchard.Security;
using Alidayu.Sms.Models;

namespace Alidayu.Sms.Services
{
    public class SmsMessageEventHandler : IMessageEventHandler
    {
        private readonly IContentManager contentManager;

        public SmsMessageEventHandler(IContentManager contentManager)
        {
            this.contentManager = contentManager;
        }

        public void Sending(MessageContext context)
        {
            if (context.Recipients != null)
            {
                foreach (var rec in context.Recipients)
                {
                    var contentItem = contentManager.Get(rec.Id);
                    if (contentItem == null)
                        return;

                    var recipient = contentItem.As<MobilePart>();
                    if (recipient == null)
                        return;

                    context.Addresses = context.Addresses.Concat<string>(new string[] { recipient.MobileNumber });
                }
            }
        }

        public void Sent(MessageContext context)
        {
        }
    }
}