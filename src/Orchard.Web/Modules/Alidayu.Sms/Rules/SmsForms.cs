using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Events;
using Orchard.Localization;
using Orchard.DisplayManagement;

namespace Contrib.Sms.Rules
{
    public interface IFormProvider : IEventHandler
    {
        void Describe(dynamic context);
    }


    public class SmsForms : IFormProvider
    {
        protected dynamic Shape { get; set; }
        public Localizer T { get; set; }

        public SmsForms(IShapeFactory shapeFactory)
        {
            Shape = shapeFactory;
            T = NullLocalizer.Instance;
        }

        public void Describe(dynamic context)
        {
            Func<IShapeFactory, dynamic> form =
                shape => Shape.Form(
                Id: "SmsAction",
                _Type: Shape.FieldSet(
                    Title: T("Send to"),
                    _RecipientOwner: Shape.Radio(
                        Id: "recipient-owner",
                        Name: "Recipient",
                        Value: "owner",
                        Title: T("Owner"),
                        Description: T("The owner of the content item in context, such as a blog post's author.")
                    ),
                    _RecipientAuthor: Shape.Radio(
                        Id: "recipient-author",
                        Name: "Recipient",
                        Value: "author",
                        Title: T("Author"),
                        Description: T("The current user when this action executes.")
                    ),
                    _RecipientAdmin: Shape.Radio(
                        Id: "recipient-admin",
                        Name: "Recipient",
                        Value: "admin",
                        Title: T("Site Admin"),
                        Description: T("The site administrator.")
                    ),
                    _RecipientOther: Shape.Radio(
                        Id: "recipient-other",
                        Name: "Recipient",
                        Value: "other",
                        Title: T("Other:")
                    ),
                    _OtherNumbers: Shape.Textbox(
                        Id: "recipient-other-sms",
                        Name: "RecipientOther",
                        Title: T("Mobile"),
                        Description: T("Specify a comma-separated list of mobile numbers. Pefix number with country code. e.g. +447977123456"),
                        Classes: new[] { "large", "text", "tokenized" }
                    )
                ),
                _Message: Shape.Textarea(
                    Id: "Body", Name: "Body",
                    Title: T("Body"),
                    Description: T("The body of the sms message."),
                    Classes: new[] { "tokenized" }
                    )
                );

            context.Form("ActionSms", form);
        }
    }

    public interface IFormEventHandler : IEventHandler
    {
        void Validating(dynamic context);
    }

    public class MailFormsValidator : IFormEventHandler
    {
        public Localizer T { get; set; }

        public void Validating(dynamic context)
        {
            if (context.FormName == "ActionSms")
            {
                if (context.ValueProvider.GetValue("Recipient").AttemptedValue == String.Empty)
                {
                    context.ModelState.AddModelError("Recipient", T("You must select at least one recipient").Text);
                }

                if (context.ValueProvider.GetValue("Body").AttemptedValue == String.Empty)
                {
                    context.ModelState.AddModelError("Body", T("You must provide a message body").Text);
                }

                if (context.ValueProvider.GetValue("RecipientOther").AttemptedValue == String.Empty &&
                    context.ValueProvider.GetValue("Recipient").AttemptedValue == "other")
                {
                    context.ModelState.AddModelError("Recipient", T("You must provide mobile phone number").Text);
                }
            }
        }
    }
}