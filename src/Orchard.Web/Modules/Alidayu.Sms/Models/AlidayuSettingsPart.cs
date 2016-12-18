using Orchard.ContentManagement;
using Orchard.ContentManagement.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alidayu.Sms.Models
{
    public class AlidayuSettingsPart: ContentPart<AlidayuSettingsPartRecord>
    {

        string smsFreeSignName=string.Empty;
        string smsParam= string.Empty;
        string recNum = string.Empty;
        string smsTemplateCode = string.Empty;

        public string SmsFreeSignName
        {
            get { return smsFreeSignName; }
            set { smsFreeSignName = value; }
        }
        public string SmsParam
        {
            get { return smsParam; }
            set { smsParam = value; }
        }
        public string RecNum
        {
            get { return recNum; }
            set { recNum = value; }
        }

        public string SmsTemplateCode
        {
            get { return smsTemplateCode; }
            set { smsTemplateCode = value; }
        }

        public bool IsValid()
        {
            return !String.IsNullOrWhiteSpace(Record.SmsFreeSignName)
                && !String.IsNullOrWhiteSpace(Record.SmsParam)
                && !String.IsNullOrWhiteSpace(Record.RecNum)
                && !String.IsNullOrWhiteSpace(Record.SmsTemplateCode);
        }

    }
}