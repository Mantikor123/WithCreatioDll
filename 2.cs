namespace Terrasoft.Configuration
{

	using DataContract = Terrasoft.Nui.ServiceModel.DataContract;
	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Data;
	using System.Drawing;
	using System.Globalization;
	using System.IO;
	using System.Linq;
	using Terrasoft.Common;
	using Terrasoft.Common.Json;
	using Terrasoft.Core;
	using Terrasoft.Core.Configuration;
	using Terrasoft.Core.DB;
	using Terrasoft.Core.DcmProcess;
	using Terrasoft.Core.Entities;
	using Terrasoft.Core.Factories;
	using Terrasoft.Core.Process;
	using Terrasoft.Core.Process.Configuration;
	using Terrasoft.GlobalSearch.Indexing;
	using Terrasoft.UI.WebControls.Controls;
	using Terrasoft.UI.WebControls.Utilities.Json.Converters;
	using Msc.Base.Log;

	#region Class: MscWorks_MscAutoGeneralEventsProcess

	public partial class MscWorks_MscAutoGeneralEventsProcess<TEntity>
	{

		#region Methods: Public


		public virtual void RecalculateServiceAmount()
		{
			try
			{
				Guid MscServiceRecordId = Entity.GetTypedColumnValue<Guid>("MscServiceRecordId");

				if ((MscServiceRecordId == null) || (MscServiceRecordId == Guid.Empty))
					return;

				var esqWorks = new EntitySchemaQuery(UserConnection.EntitySchemaManager, "MscWorks");
				var AmountWorksColumn = esqWorks.AddColumn(esqWorks.CreateAggregationFunction(AggregationTypeStrict.Sum, "MscAmount"));
				esqWorks.Filters.Add(esqWorks.CreateFilterWithParameters(FilterComparisonType.Equal, "MscServiceRecord", MscServiceRecordId));
				var entityWorks = esqWorks.GetEntityCollection(UserConnection).FirstOrDefault();
				if (entityWorks == null)
				{
					return;
				}

				var esqProductInService = new EntitySchemaQuery(UserConnection.EntitySchemaManager, "MscProductInService");
				var AmountProductInServiceColumn = esqProductInService.AddColumn(esqProductInService.CreateAggregationFunction(AggregationTypeStrict.Sum, "MscAmount"));
				esqProductInService.Filters.Add(esqProductInService.CreateFilterWithParameters(FilterComparisonType.Equal, "MscWorkRecord", MscServiceRecordId));
				var entityProductInService = esqProductInService.GetEntityCollection(UserConnection).FirstOrDefault();
				if (entityProductInService == null)
				{
					return;
				}

				var AmountWorks = entityWorks.GetTypedColumnValue<decimal>(AmountWorksColumn.Name);
				var AmountProductInService = entityProductInService.GetTypedColumnValue<decimal>(AmountProductInServiceColumn.Name);




				var MscService = UserConnection.EntitySchemaManager.GetInstanceByName("MscService").CreateEntity(UserConnection);
				if (MscService.FetchFromDB(MscServiceRecordId))
				{
					MscService.SetColumnValue("MscServiceSum", AmountWorks + AmountProductInService);
					MscService.Save(false);
				}

			}
			catch (Exception ex)
			{
				MscLogHelperBase.AddRecordLogEx("MscWorks RecalculateServiceAmount", ex, UserConnection);
			}
		}

		#endregion

	}

	#endregion

}

