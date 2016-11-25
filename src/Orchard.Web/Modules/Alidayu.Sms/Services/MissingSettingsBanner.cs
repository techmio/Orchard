using System.Collections.Generic;
using System.Web.Mvc;
using Alidayu.Sms.Models;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Localization;
using Orchard.UI.Admin.Notification;
using Orchard.UI.Notify;
using Alidayu.Sms.Models;

namespace Alidayu.Sms.Services
{
    public class MissingSettingsBanner : INotificationProvider
    {
        private readonly IOrchardServices orchardServices;
        public Localizer T { get; set; }

        public MissingSettingsBanner(IOrchardServices orchardServices)
        {
            this.orchardServices = orchardServices;
            T = NullLocalizer.Instance;
        }

        public IEnumerable<NotifyEntry> GetNotifications()
        {
            /*
            var workContext = orchardServices.WorkContext;
            var smsSettings = workContext.CurrentSite.As<TwilioSettingsPart>();

            if (smsSettings == null || !smsSettings.IsValid())
            {
                var urlHelper = new UrlHelper(workContext.HttpContext.Request.RequestContext);
                var url = urlHelper.Action("Sms", "Admin", new { Area = "Settings" });
                yield return new NotifyEntry { Message = T("The <a href=\"{0}\">SMS settings</a> needs to be configured.", url), Type = NotifyType.Warning };
            }*/

            var workContext = orchardServices.WorkContext;
            var smsSettings = workContext.CurrentSite.As<AlidayuSettingsPart>();

            if (smsSettings == null || !smsSettings.IsValid())
            {
                var urlHelper = new UrlHelper(workContext.HttpContext.Request.RequestContext);
                var url = urlHelper.Action("Sms", "Admin", new { Area = "Settings" });
                yield return new NotifyEntry { Message = T("The <a href=\"{0}\">Alidayu SMS settings</a> needs to be configured.", url), Type = NotifyType.Warning };
            }
        }
    }
}