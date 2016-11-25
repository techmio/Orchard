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


        public string SmsFreeSignName="";
        public string SmsParam = "";
        public string RecNum = "";
        public string SmsTemplateCode = "";

        public bool IsValid()
        {
            return !String.IsNullOrWhiteSpace(Record.SmsFreeSignName)
                && !String.IsNullOrWhiteSpace(Record.SmsParam)
                && !String.IsNullOrWhiteSpace(Record.RecNum)
                && !String.IsNullOrWhiteSpace(Record.SmsTemplateCode);
        }

    }
}