using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement.Handlers;
using Alidayu.Sms.Models;
using Orchard.Data;

namespace Contrib.Sms.Handlers
{
    public class MobilePartHandler : ContentHandler
    {
        public MobilePartHandler(IRepository<MobilePartRecord> repository) 
        {
            Filters.Add(StorageFilter.For(repository));
            Filters.Add(new ActivatingFilter<MobilePart>("User"));
        }
    }
}