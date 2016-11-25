using Alidayu.Sms.Drivers;
using Alidayu.Sms.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Localization;
using Orchard.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Alidayu.Sms.Handlers
{
    public class AlidayuSettingsPartHandler : ContentHandler
    {
        private readonly Orchard.Security.IEncryptionService encryptionService;

        public new ILogger Logger { get; set; }
        public Localizer T { get; set; }

        public AlidayuSettingsPartHandler(Orchard.Data.IRepository<AlidayuSettingsPartRecord> repository, Orchard.Security.IEncryptionService encryptionService)
        {

            T = NullLocalizer.Instance;
            Logger = NullLogger.Instance;

            //this.encryptionService = encryptionService;
            //Filters.Add(new ActivatingFilter<AlidayuSettingsPart>("Site"));
           // Filters.Add(StorageFilter.For(repository));

            OnLoaded<AlidayuSettingsPart>(LazyLoadHandlers);

        }

        void LazyLoadHandlers(LoadContentContext context, AlidayuSettingsPart part)
        {

         }


        protected override void GetItemMetadata(GetContentItemMetadataContext context)
        {
            if (context.ContentItem.ContentType != "Site")
                return;
            base.GetItemMetadata(context);
            context.Metadata.EditorGroupInfo.Add(new GroupInfo(T("Alidayu")));
        }

    }
}