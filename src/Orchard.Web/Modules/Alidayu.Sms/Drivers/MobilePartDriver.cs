using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement.Drivers;
using Alidayu.Sms.Models;
using Orchard.ContentManagement;

namespace Alidayu.Sms.Drivers
{
    public class MobilePartDriver : ContentPartDriver<MobilePart>
    {
        private const string TemplateName = "Parts/Mobile"; 
        protected override string Prefix { get { return "Mobile"; } }

        protected override DriverResult Editor(MobilePart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_Mobile_Edit", () => shapeHelper.EditorTemplate(TemplateName: TemplateName, Model: part, Prefix: Prefix));
        }

        protected override DriverResult Editor(MobilePart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }


    }
}