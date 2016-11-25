using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement.Handlers;
using Orchard.Security;
using Orchard.Data;
using Alidayu.Sms.Models;
using Orchard.Logging;
using Orchard.Localization;
using System.Text;
using Orchard.ContentManagement;
using Alidayu.Sms.Models;

namespace Contrib.Sms.Handlers
{
    public class TwilioSettingsPartHandler : ContentHandler
    {
        private readonly IEncryptionService encryptionService;
       
        public new ILogger Logger { get; set; }
        public Localizer T { get; set; }


        public TwilioSettingsPartHandler(IRepository<TwilioSettingsPartRecord> repository, IEncryptionService encryptionService)
        {
            T = NullLocalizer.Instance;
            Logger = NullLogger.Instance;

            this.encryptionService = encryptionService;
            Filters.Add(new ActivatingFilter<TwilioSettingsPart>("Site"));
            Filters.Add(StorageFilter.For(repository));

            OnLoaded<TwilioSettingsPart>(LazyLoadHandlers);
        }

        void LazyLoadHandlers(LoadContentContext context, TwilioSettingsPart part)
        {
            part.AuthTokenField.Getter(() =>
            {
                try
                {
                    return String.IsNullOrWhiteSpace(part.Record.AuthToken) ? String.Empty : Encoding.UTF8.GetString(encryptionService.Decode(Convert.FromBase64String(part.Record.AuthToken)));
                }
                catch
                {
                    Logger.Error("The auth token could not be decrypted. It might be corrupted, try to reset it.");
                    return null;
                }
            });

            part.AuthTokenField.Setter(value => part.Record.AuthToken = String.IsNullOrWhiteSpace(value) ? String.Empty : Convert.ToBase64String(encryptionService.Encode(Encoding.UTF8.GetBytes(value))));
        }

        protected override void GetItemMetadata(GetContentItemMetadataContext context)
        {
            if (context.ContentItem.ContentType != "Site")
                return;
            base.GetItemMetadata(context);
            context.Metadata.EditorGroupInfo.Add(new GroupInfo(T("sms")));
        }
    }
}