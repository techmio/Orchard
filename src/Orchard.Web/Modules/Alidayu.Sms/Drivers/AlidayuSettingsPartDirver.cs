using Alidayu.Sms.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alidayu.Sms.Drivers
{
    public class AlidayuSettingsPartDriver : ContentPartDriver<Models.AlidayuSettingsPart>
    {
        private const string TemplateName = "Parts/AlidayuSettings";
        public Localizer T { get; set; }

        public AlidayuSettingsPartDriver()
        {
            T = NullLocalizer.Instance;
        }

        protected override string Prefix { get { return "AlidayuSettings"; } }

        protected override DriverResult Editor(Models.AlidayuSettingsPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_AlidayuSettings_Edit",
                    () => shapeHelper.EditorTemplate(TemplateName: TemplateName, Model: part, Prefix: Prefix))
                    .OnGroup("alidayu");
        }

        protected override DriverResult Editor(Models.AlidayuSettingsPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            return ContentShape("Parts_AlidayuSettings_Edit", () =>
            {
               return shapeHelper.EditorTemplate(TemplateName: TemplateName, Model: part, Prefix: Prefix);
            }).OnGroup("Alidayu");
        }
    }
}