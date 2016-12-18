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


}