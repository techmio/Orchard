using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement.Records;

namespace Alidayu.Sms.Models
{
    public class TwilioSettingsPartRecord : ContentPartRecord
    {
        public virtual string AccountSID { get; set; }

        public virtual string AuthToken { get; set; }

        public virtual string FromNumber { get; set; }
    }


    public class AlidayuSettingsPartRecord: ContentItemRecord
    {
        public virtual string SmsFreeSignName { get; set; }

        public virtual string SmsParam { get; set; }

        public virtual string RecNum { get; set; }
        public virtual string SmsTemplateCode { get; set; }

    }
}