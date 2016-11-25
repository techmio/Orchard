using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Data.Migration;
﻿using Orchard.ContentManagement.MetaData;

namespace Alidayu.Sms
{
    public class Migrations : DataMigrationImpl
    {

        public int Create()
        {
            SchemaBuilder.CreateTable("MobilePartRecord",
                  table => table
                      .ContentPartRecord()
                      .Column<string>("MobileNumber")
            ); 
            
            ContentDefinitionManager.AlterTypeDefinition("User",
                cfg => cfg
                    .WithPart("MobilePart")
                );

            SchemaBuilder.CreateTable("TwilioSettingsPartRecord",
                table => table
                    .ContentPartRecord()
                    .Column<string>("AccountSID")
                    .Column<string>("AuthToken")
                    .Column<string>("FromNumber")
                );

            return 1;
        }

    }
}