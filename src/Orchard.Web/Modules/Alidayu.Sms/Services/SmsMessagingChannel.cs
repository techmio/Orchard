using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Localization;
using Orchard.Logging;
using Orchard.Messaging.Models;
using Orchard.Messaging.Services;
using Twilio;
using System.Linq;

using Top.Api;
using Top.Api.Request;
using Top.Api.Response;
using Alidayu.Sms.Models;
using Alidayu.Sms.Drivers;

namespace Alidayu.Sms.Services
{
    public class SmsMessagingChannel : IMessagingChannel
    {
        private readonly IOrchardServices orchardServices;
        public const string SmsService = "AlidayuSMS";

        public ILogger Logger { get; set; }
        public Localizer T { get; set; }

        public SmsMessagingChannel(IOrchardServices orchardServices)
        {
            this.orchardServices = orchardServices;
            Logger = NullLogger.Instance;
        }

        public void SendMessageAlidayu()
        {
            string url = "http://gw.api.taobao.com/router/rest", appkey = "23465281", secret = "93c1ed8e5be54eeacc06f48b5c90da37";
            ITopClient client = new DefaultTopClient(url, appkey, secret);
            AlibabaAliqinFcSmsNumSendRequest req = new AlibabaAliqinFcSmsNumSendRequest();
            req.Extend = "" ;
            req.SmsType = "normal" ;
            req.SmsFreeSignName = "菜篮知" ;
            req.SmsParam = "{code:'{123456}'}" ; 
            req.RecNum = "13128818478" ;
            req.SmsTemplateCode = "SMS_16225170" ;
            AlibabaAliqinFcSmsNumSendResponse rsp = client.Execute(req);
            if(rsp.Result.Success)
            Console.WriteLine(rsp.Body);
        }

        public void SendMessage(MessageContext context)
        {
            /*
            if (!context.Service.Equals(SmsService, StringComparison.InvariantCultureIgnoreCase))
                return;

            var twilioSettings = orchardServices.WorkContext.CurrentSite.As<TwilioSettingsPart>();

            // can't process sms's if the twilio settings have not yet been set
            if (twilioSettings == null || !twilioSettings.IsValid())
            {
                return;
            }

            if (context.Addresses.Count() == 0)
            {
                Logger.Error("Recipient is missing a mobile number");
                return;
            }

            TwilioRestClient smsClient = new TwilioRestClient(twilioSettings.AccountSID, twilioSettings.AuthToken);

            foreach (var number in context.Addresses)
	        {
		        try
                {
                    var msg = smsClient.SendSmsMessage(twilioSettings.FromNumber, number, context.MailMessage.Body);
                    Logger.Debug("Message sent to {0}: {1}", number, context.Type);
                }
                catch(Exception e)
                {
                    Logger.Error(e, "An unexpected error while sending a message to {0}: {1}", number, context.Type);
                }
	        }
            */

            if (!context.Service.Equals(SmsService, StringComparison.InvariantCultureIgnoreCase))
                return;

            var alidayuSettings = orchardServices.WorkContext.CurrentSite.As<Models.AlidayuSettingsPart>();

            // can't process sms's if the twilio settings have not yet been set
            if (alidayuSettings == null || !alidayuSettings.IsValid())
            {
                return;
            }

            if (context.Addresses.Count() == 0)
            {
                Logger.Error("Recipient is missing a mobile number");
                return;
            }



            try
            {
                string url = "http://gw.api.taobao.com/router/rest", appkey = "23465281", secret = "93c1ed8e5be54eeacc06f48b5c90da37";
                ITopClient client = new DefaultTopClient(url, appkey, secret);
                AlibabaAliqinFcSmsNumSendRequest req = new AlibabaAliqinFcSmsNumSendRequest();
                req.Extend = "";
                req.SmsType = "normal";
                req.SmsFreeSignName = alidayuSettings.SmsFreeSignName; //"菜篮知"
                req.SmsParam = alidayuSettings.SmsParam;// "{code:'{123456}'}";
                req.RecNum = alidayuSettings.RecNum;// "13128818478";
                req.SmsTemplateCode = alidayuSettings.SmsTemplateCode; //"SMS_16225170";
                AlibabaAliqinFcSmsNumSendResponse rsp = client.Execute(req);
                if (rsp.Result.Success)
                {
                    Logger.Debug("Message sent to {0}: {1}", alidayuSettings.RecNum, context.Type);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e, "An unexpected error while sending a message to {0}: {1}", alidayuSettings.RecNum, context.Type);
            }
        }

        public IEnumerable<string> GetAvailableServices()
        {
            return new[] { SmsService };
        }
    }
}