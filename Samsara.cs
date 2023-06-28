using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Terrasoft.Core;
using Terrasoft.Core.Entities;

namespace WithCreatioDll
{
    class MscSamsaraIntegrationHelper
    {
        public UserConnection UserConnection { get; set; }
        string Url = @"https://api.samsara.com/";

        public MscSamsaraIntegrationHelper(UserConnection userConnection)
        {
            UserConnection = userConnection;
        }

        public void getDataFromSamsara()
        {
            string url = Url;
            Uri baseURI = new Uri(url);
            Uri newURI = new Uri(baseURI, "fleet/vehicles/stats");

            string Query = "types=obdOdometerMeters,obdEngineSeconds,fuelPercents";
            string httpResponseResult = getResponseMessageBySamsara(newURI, Query);

            var arrV2 = JToken.Parse(httpResponseResult);

            var Cars = JToken.Parse(arrV2["data"].ToString());
            var CarItems = JToken.Parse(Cars.ToString()).AsEnumerable().ToArray();

            foreach (var item in CarItems)
            {
                Root root = Newtonsoft.Json.JsonConvert.DeserializeObject<Root>(item.ToString());

            }
        }

        private void CheckRecord(Root root)
        {
            string Vin = root.externalIds.samsaravin;

            Entity ent = null;


            var esqTable = new EntitySchemaQuery(UserConnection.EntitySchemaManager, "UsrTrucks");
            esqTable.AddAllSchemaColumns();
            esqTable.UseAdminRights = false;
            esqTable.Filters.Add(esqTable.CreateFilterWithParameters(FilterComparisonType.Equal, "UsrVin", Vin));
            var Collection = esqTable.GetEntityCollection(UserConnection);
            if (Collection.Count == 0)
            {
                ent = CreateTrucks(root);
            } else
            {
                ent = Collection[0];
            }
            SetDataInTruk(ent, root);

        }

        private void SetDataInTruk(Entity ent, Root root)
        {
            ent.SetColumnValue("UsrOdometer", root.obdOdometerMeters.value);
            ent.SetColumnValue("UsrEngineHours", root.obdEngineSeconds.value);
            ent.SetColumnValue("UsrFuelLevel", root.fuelPercent.value);
        }

        private Entity CreateTrucks(Root root)
        {
            Guid TrucksId = Guid.NewGuid();
            EntitySchema schemaWrite = UserConnection.EntitySchemaManager.GetInstanceByName("UsrTrucks");
            Entity entity = schemaWrite.CreateEntity(UserConnection);
            entity.SetDefColumnValues();
            entity.SetColumnValue("Id", TrucksId);
            entity.SetColumnValue("UsrVin", root.externalIds.samsaravin);
            entity.SetColumnValue("UsrName", root.name);
            entity.Save(false);
            return entity;
        }

        private string getResponseMessageBySamsara(Uri URI, string Query)
        {
            UriBuilder builder = new UriBuilder(URI);
            builder.Query = Query;

            string a = builder.Uri.ToString();

            HttpClient client2 = new HttpClient();

            var MscSamsaraToken = Terrasoft.Core.Configuration.SysSettings.GetValue(UserConnection, "MscSamsaraToken").ToString();

            client2.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", MscSamsaraToken);
            HttpResponseMessage response2 = client2.GetAsync(builder.Uri).ContinueWith(task => task.Result).Result;

            string httpResponseResult2 = null;
            if (response2.IsSuccessStatusCode)
            {
                return httpResponseResult2 = response2.Content.ReadAsStringAsync().ContinueWith(task => task.Result).Result;
            }
            else
            {
                return response2.StatusCode.ToString();
            }
            return "";
        }

        


        public class ExternalIds
        {
            [JsonProperty("samsara.serial")]
            public string samsaraserial { get; set; }

            [JsonProperty("samsara.vin")]
            public string samsaravin { get; set; }
        }

        public class FuelPercent
        {
            public DateTime time { get; set; }
            public int value { get; set; }
        }

        public class ObdEngineSeconds
        {
            public DateTime time { get; set; }
            public int value { get; set; }
        }

        public class ObdOdometerMeters
        {
            public DateTime time { get; set; }
            public int value { get; set; }
        }

        public class Root
        {
            public string id { get; set; }
            public string name { get; set; }
            public ExternalIds externalIds { get; set; }
            public FuelPercent fuelPercent { get; set; }
            public ObdOdometerMeters obdOdometerMeters { get; set; }
            public ObdEngineSeconds obdEngineSeconds { get; set; }
        }

    }
}
