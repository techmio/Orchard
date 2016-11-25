using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;

namespace Alidayu.Sms.Models
{
    public class MobilePart : ContentPart<MobilePartRecord>
    {
        public string MobileNumber
        {
            get { return Record.MobileNumber; }
            set { Record.MobileNumber = value; }
        }
    }
}