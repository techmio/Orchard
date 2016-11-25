using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement.Records;

namespace Alidayu.Sms.Models
{
    public class MobilePartRecord : ContentPartRecord
    {
        public virtual string MobileNumber { get; set; }
    }
}