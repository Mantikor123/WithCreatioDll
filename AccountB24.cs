using System;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Globalization;
using Terrasoft.Common;
using Terrasoft.Core;

using Terrasoft.Core.DB;
using Terrasoft.Core.Entities;
using Terrasoft.Core.Process;
using Terrasoft.Core.Process.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WithCreatioDll
{
    class AccountB24
    {

        public UserConnection UserConnection { get; set; }
        public AccountB24(UserConnection userConnection)
        {
            UserConnection = userConnection;
        }

        public void Account()
        {
            string a = "{  \"ID\": \"42466\",  \"COMPANY_TYPE\": \"CUSTOMER\",  \"TITLE\": \"Станко Надія\",  \"LOGO\": null,  \"LEAD_ID\": null,  \"HAS_PHONE\": \"N\",  \"HAS_EMAIL\": \"N\",  \"HAS_IMOL\": \"N\",  \"ASSIGNED_BY_ID\": \"1\",  \"CREATED_BY_ID\": \"412\",  \"MODIFY_BY_ID\": \"412\",  \"BANKING_DETAILS\": null,  \"INDUSTRY\": null,  \"REVENUE\": null,  \"CURRENCY_ID\": null,  \"EMPLOYEES\": null,  \"COMMENTS\": \"\",  \"DATE_CREATE\": \"2021-07-06T11:00:39+03:00\",  \"DATE_MODIFY\": \"2021-08-27T20:54:38+03:00\",  \"OPENED\": \"N\",  \"IS_MY_COMPANY\": \"N\",  \"ORIGINATOR_ID\": \"ONE_C\",  \"ORIGIN_ID\": \"bd73ee95-dd91-11eb-ba6c-3497f65a1029\",  \"ORIGIN_VERSION\": null,  \"ADDRESS\": null,  \"ADDRESS_2\": null,  \"ADDRESS_CITY\": null,  \"ADDRESS_POSTAL_CODE\": null,  \"ADDRESS_REGION\": null,  \"ADDRESS_PROVINCE\": null,  \"ADDRESS_COUNTRY\": null,  \"ADDRESS_COUNTRY_CODE\": null,  \"ADDRESS_LOC_ADDR_ID\": null,  \"ADDRESS_LEGAL\": null,  \"REG_ADDRESS\": null,  \"REG_ADDRESS_2\": null,  \"REG_ADDRESS_CITY\": null,  \"REG_ADDRESS_POSTAL_CODE\": null,  \"REG_ADDRESS_REGION\": null,  \"REG_ADDRESS_PROVINCE\": null,  \"REG_ADDRESS_COUNTRY\": null,  \"REG_ADDRESS_COUNTRY_CODE\": null,  \"REG_ADDRESS_LOC_ADDR_ID\": null,  \"UTM_SOURCE\": null,  \"UTM_MEDIUM\": null,  \"UTM_CAMPAIGN\": null,  \"UTM_CONTENT\": null,  \"UTM_TERM\": null,  \"LAST_ACTIVITY_BY\": \"412\",  \"LAST_ACTIVITY_TIME\": \"2021-07-06T11:00:39+03:00\",  \"UF_CRM_1525786642\": null,  \"UF_CRM_1525786822\": null,  \"UF_CRM_1525786836\": null,  \"UF_CRM_1525786850\": null,  \"UF_CRM_1525787376\": null,  \"UF_CRM_1526026793\": null,  \"UF_CRM_1526027096\": null,  \"UF_CRM_1526476418\": null,  \"UF_CRM_1526546184\": null,  \"UF_CRM_1526550083\": null,  \"UF_CRM_1526550125\": null,  \"UF_CRM_1527230833566\": null,  \"UF_CRM_1530628519440\": [],  \"UF_CRM_5BCF2823455B4\": null,  \"UF_CRM_1553688931\": \"80\",  \"UF_CRM_5CCC93E2C47E8\": [],  \"UF_CRM_5D6CE520E2773\": null,  \"UF_CRM_1572945014\": null,  \"UF_CRM_1574079557\": \"Інтернет магазин Жива Земля\",  \"UF_CRM_1581346209\": null,  \"UF_CRM_1581347538\": \"686\",  \"UF_CRM_1581348031\": null,  \"UF_CRM_5E734023C8F33\": null,  \"UF_CRM_5E73402403936\": null,  \"UF_CRM_5E73402413EB8\": null,  \"UF_CRM_5E7DF4F58D9E6\": null,  \"UF_CRM_5E7DF4F5BA070\": null,  \"UF_CRM_1586337461\": null,  \"UF_CRM_5EB5162CB39E8\": null,  \"UF_CRM_5F50879F7C045\": null,  \"UF_CRM_1619698010\": \"1364\",  \"UF_CRM_1619698115\": null,  \"UF_CRM_AREA\": null,  \"UF_CRM_DISTRICT\": null,  \"UF_CRM_ADDRESS\": null,  \"UF_CRM_QUEDS\": null,  \"UF_CRM_SQUARE\": null,  \"UF_CRM_CULTURE\": false,  \"UF_CRM_ORG_STATUS\": false,  \"UF_CRM_LOAD_TRIPOLI2\": null,  \"UF_CRM_61FA63970AFC1\": null,  \"UF_CRM_61FA639758111\": null,  \"UF_CRM_631200F9CB3C1\": false,  \"UF_CRM_631200FA227CE\": null,  \"UF_CRM_631217DAC73A7\": false,  \"UF_CRM_6374DAAD42BE9\": null,  \"UF_CRM_638F4A8B5D41D\": null,  \"UF_CRM_641C5F399C771\": null}";

            ACCOUNT account = Newtonsoft.Json.JsonConvert.DeserializeObject<ACCOUNT>(a.ToString());

        }


        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class EMAIL
        {
            public string ID { get; set; }
            public string VALUE_TYPE { get; set; }
            public string VALUE { get; set; }
            public string TYPE_ID { get; set; }
        }

        public class PHONE
        {
            public string ID { get; set; }
            public string VALUE_TYPE { get; set; }
            public string VALUE { get; set; }
            public string TYPE_ID { get; set; }
        }

        public class ACCOUNT
        {
            public string ID { get; set; }
            public string COMPANY_TYPE { get; set; }
            public string TITLE { get; set; }
            public object LOGO { get; set; }
            public object LEAD_ID { get; set; }
            public string HAS_PHONE { get; set; }
            public string HAS_EMAIL { get; set; }
            public string HAS_IMOL { get; set; }
            public string ASSIGNED_BY_ID { get; set; }
            public string CREATED_BY_ID { get; set; }
            public string MODIFY_BY_ID { get; set; }
            public object BANKING_DETAILS { get; set; }
            public object INDUSTRY { get; set; }
            public object REVENUE { get; set; }
            public object CURRENCY_ID { get; set; }
            public object EMPLOYEES { get; set; }
            public string COMMENTS { get; set; }
            public DateTime DATE_CREATE { get; set; }
            public DateTime DATE_MODIFY { get; set; }
            public string OPENED { get; set; }
            public string IS_MY_COMPANY { get; set; }
            public string ORIGINATOR_ID { get; set; }
            public string ORIGIN_ID { get; set; }
            public object ORIGIN_VERSION { get; set; }
            public object ADDRESS { get; set; }
            public object ADDRESS_2 { get; set; }
            public object ADDRESS_CITY { get; set; }
            public object ADDRESS_POSTAL_CODE { get; set; }
            public object ADDRESS_REGION { get; set; }
            public object ADDRESS_PROVINCE { get; set; }
            public object ADDRESS_COUNTRY { get; set; }
            public object ADDRESS_COUNTRY_CODE { get; set; }
            public object ADDRESS_LOC_ADDR_ID { get; set; }
            public object ADDRESS_LEGAL { get; set; }
            public object REG_ADDRESS { get; set; }
            public object REG_ADDRESS_2 { get; set; }
            public object REG_ADDRESS_CITY { get; set; }
            public object REG_ADDRESS_POSTAL_CODE { get; set; }
            public object REG_ADDRESS_REGION { get; set; }
            public object REG_ADDRESS_PROVINCE { get; set; }
            public object REG_ADDRESS_COUNTRY { get; set; }
            public object REG_ADDRESS_COUNTRY_CODE { get; set; }
            public object REG_ADDRESS_LOC_ADDR_ID { get; set; }
            public object UTM_SOURCE { get; set; }
            public object UTM_MEDIUM { get; set; }
            public object UTM_CAMPAIGN { get; set; }
            public object UTM_CONTENT { get; set; }
            public object UTM_TERM { get; set; }
            public string LAST_ACTIVITY_BY { get; set; }
            public DateTime LAST_ACTIVITY_TIME { get; set; }
            public object UF_CRM_1525786642 { get; set; }
            public object UF_CRM_1525786822 { get; set; }
            public object UF_CRM_1525786836 { get; set; }
            public object UF_CRM_1525786850 { get; set; }
            public object UF_CRM_1525787376 { get; set; }
            public object UF_CRM_1526026793 { get; set; }
            public object UF_CRM_1526027096 { get; set; }
            public object UF_CRM_1526476418 { get; set; }
            public object UF_CRM_1526546184 { get; set; }
            public object UF_CRM_1526550083 { get; set; }
            public object UF_CRM_1526550125 { get; set; }
            public object UF_CRM_1527230833566 { get; set; }
            public List<object> UF_CRM_1530628519440 { get; set; }
            public object UF_CRM_5BCF2823455B4 { get; set; }
            public object UF_CRM_1553688931 { get; set; }
            public List<object> UF_CRM_5CCC93E2C47E8 { get; set; }
            public object UF_CRM_5D6CE520E2773 { get; set; }
            public object UF_CRM_1572945014 { get; set; }
            public string UF_CRM_1574079557 { get; set; }
            public object UF_CRM_1581346209 { get; set; }
            public string UF_CRM_1581347538 { get; set; }
            public object UF_CRM_1581348031 { get; set; }
            public object UF_CRM_5E734023C8F33 { get; set; }
            public object UF_CRM_5E73402403936 { get; set; }
            public object UF_CRM_5E73402413EB8 { get; set; }
            public object UF_CRM_5E7DF4F58D9E6 { get; set; }
            public object UF_CRM_5E7DF4F5BA070 { get; set; }
            public object UF_CRM_1586337461 { get; set; }
            public object UF_CRM_5EB5162CB39E8 { get; set; }
            public object UF_CRM_5F50879F7C045 { get; set; }
            public string UF_CRM_1619698010 { get; set; }
            public object UF_CRM_1619698115 { get; set; }
            public object UF_CRM_AREA { get; set; }
            public object UF_CRM_DISTRICT { get; set; }
            public object UF_CRM_ADDRESS { get; set; }
            public object UF_CRM_QUEDS { get; set; }
            public object UF_CRM_SQUARE { get; set; }
            public List<object> UF_CRM_CULTURE { get; set; }
            public List<object> UF_CRM_ORG_STATUS { get; set; }
            public object UF_CRM_LOAD_TRIPOLI2 { get; set; }
            public object UF_CRM_61FA63970AFC1 { get; set; }
            public object UF_CRM_61FA639758111 { get; set; }
            public List<object> UF_CRM_631200F9CB3C1 { get; set; }
            public object UF_CRM_631200FA227CE { get; set; }
            public List<object> UF_CRM_631217DAC73A7 { get; set; }
            public object UF_CRM_6374DAAD42BE9 { get; set; }
            public object UF_CRM_638F4A8B5D41D { get; set; }
            public object UF_CRM_641C5F399C771 { get; set; }
            public List<PHONE> PHONE { get; set; }
            public List<EMAIL> EMAIL { get; set; }
        }



    }
}
