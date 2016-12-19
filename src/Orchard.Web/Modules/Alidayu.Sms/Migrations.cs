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

            SchemaBuilder.CreateTable("AlidayuSettingsPartRecord",
                table => table
                    .ContentPartRecord()
                    .Column<string>("SmsFreeSignName")
                    .Column<string>("SmsParam")
                    .Column<string>("RecNum")
                    .Column<string>("SmsTemplateCode")
                );

            return 1;
        }

    }
}