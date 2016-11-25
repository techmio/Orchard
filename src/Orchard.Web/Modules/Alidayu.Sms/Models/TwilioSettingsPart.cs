using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Utilities;
using Alidayu.Sms.Models;

namespace Alidayu.Sms.Models
{
    public class TwilioSettingsPart : ContentPart<TwilioSettingsPartRecord>
    {
        private readonly ComputedField<string> authToken = new ComputedField<string>();

        public ComputedField<string> AuthTokenField
        {
            get { return authToken; }
        }

        public string AccountSID
        {
            get { return Record.AccountSID; }
            set { Record.AccountSID = value; }
        }

        public string AuthToken
        {
            get { return authToken.Value; }
            set { authToken.Value = value; }
        }

        public string FromNumber
        {
            get { return Record.FromNumber; }
            set { Record.FromNumber = value; }
        }

        public bool IsValid()
        {
            return !String.IsNullOrWhiteSpace(Record.AccountSID)
                && !String.IsNullOrWhiteSpace(Record.FromNumber)
                && !String.IsNullOrWhiteSpace(Record.AuthToken);
        }
    }
}