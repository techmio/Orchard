using Orchard.ContentManagement.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alidayu.Sms.Models
{

    public class AlidayuSettingsPartRecord : ContentPartRecord
    {
        public virtual string SmsFreeSignName { get; set; }


        public virtual string SmsParam { get; set; }


        public virtual string RecNum { get; set; }
        public virtual string SmsTemplateCode { get; set; }

    }
}