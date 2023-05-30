using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Globalization;
using Terrasoft.Common;
using Terrasoft.Core;
using Terrasoft.Core.Configuration;
using Terrasoft.Core.DB;
using Terrasoft.Core.Entities;
using Terrasoft.Core.Process;
using Terrasoft.Core.Process.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Msc.Base.Log;


namespace Lsv.BAFServiceJobHelper
{

	public class LsvBAFServiceJobHelper
	{
		public UserConnection UserConnection { get; set; }

		public LsvBAFServiceJobHelper(UserConnection userConnection)
		{
			UserConnection = userConnection;
		}

		public void DeleteFromServicePostAppointment(Guid WorkPlaceId, DateTime Date)
		{
			DateTime MscDate = new DateTime(Date.Year, Date.Month, Date.Day, 0, 00, 00);

			var Sel = new Select(UserConnection);
			Sel.Column("Id")
				.From("PlanServiceJob")
				.Where("MscWorkPlaceId").IsEqual(Column.Parameter(WorkPlaceId))
				.And("MscDate").Equals(Column.Parameter(MscDate));

			Guid MscTypeServiceWorkplaceId = new Guid("2a7c74d1-4e9d-47cb-abd6-b94dfbaae770");
			var delete = new Delete(UserConnection)
				.From("MscServicePostAppointment")
				.Where("MscTypeServiceWorkplaceId").IsEqual(Column.Parameter(MscTypeServiceWorkplaceId))
				.And("MscPlanServiceJobId").In(Sel);

			var cnt = delete.Execute();
			MscLogHelperBase.AddRecordLog("DeleteFromServicePostAppointment Count deleted record", cnt.ToString(), UserConnection);
		}

		public void DeleteFromPlanServiceJob(Guid WorkPlaceId, DateTime Date)
		{
			DateTime MscDate = new DateTime(Date.Year, Date.Month, Date.Day, 0, 00, 00);
			Guid Free = new Guid("6b2cd1cf-a301-4ae1-aaf5-cc0a40e4208c");
			Guid Canceled = new Guid("7c450ed1-70ba-4192-9ee6-b125a5cbb358");

			var delete = new Delete(UserConnection);
			delete.From("MscPlanServiceJob")
			.Where("MscWorkPlaceId").IsEqual(Column.Parameter(WorkPlaceId))
			.And("MscDate").IsEqual(Column.Parameter(MscDate))
			.And("MscStatusServicePostAppointmentId").IsEqual(Column.Parameter(Free));
			var cnt = delete.Execute();
			MscLogHelperBase.AddRecordLog("DeleteFromPlanServiceJob Count deleted Free record", cnt.ToString(), UserConnection);

			var delete2 = new Delete(UserConnection);
			delete2.From("MscPlanServiceJob")
			.Where("MscWorkPlaceId").IsEqual(Column.Parameter(WorkPlaceId))
			.And("MscDate").IsEqual(Column.Parameter(MscDate))
			.And("MscStatusServicePostAppointmentId").IsEqual(Column.Parameter(Canceled));
			var cnt2 = delete2.Execute();
			MscLogHelperBase.AddRecordLog("DeleteFromPlanServiceJob Count deleted Canceled record", cnt2.ToString(), UserConnection);
		}

		public void SetPlanServiceJob(Guid MscServiceWorkplaceId, DateTime Date)
		{
			DateTime ats = Date;
			TimeSpan endTime = new TimeSpan(23, 0, 0);
			TimeSpan startTime = new TimeSpan(1, 0, 0);
			DateTime Start = ats.Date + startTime;
			DateTime End = ats.Date + endTime;
			Guid Canceled = new Guid("7c450ed1-70ba-4192-9ee6-b125a5cbb358");

			int endTimeSysValue = Convert.ToInt32(Terrasoft.Core.Configuration.SysSettings.GetValue(UserConnection, "MscServiceAppointmentScheduleTimingEnd"));
			int startTimeSysValue = Convert.ToInt32(Terrasoft.Core.Configuration.SysSettings.GetValue(UserConnection, "MscServiceAppointmentScheduleTimingStart"));
			TimeSpan TimingEnd = new TimeSpan(endTimeSysValue, 0, 0);
			TimeSpan TimingStart = new TimeSpan(startTimeSysValue, 0, 0);

			var esqPlanServiceJob = new EntitySchemaQuery(UserConnection.EntitySchemaManager, "MscPlanServiceJob");
			esqPlanServiceJob.AddAllSchemaColumns();
			esqPlanServiceJob.UseAdminRights = true;
			var StartDateCollumn = esqPlanServiceJob.AddColumn("MscStart");
			StartDateCollumn.OrderByAsc(0);
			esqPlanServiceJob.AddColumn("MscServiceJobSchedule.MscServiceWorkplace");
			esqPlanServiceJob.Filters.Add(esqPlanServiceJob.CreateFilterWithParameters(FilterComparisonType.LessOrEqual, "MscDate", End));
			esqPlanServiceJob.Filters.Add(esqPlanServiceJob.CreateFilterWithParameters(FilterComparisonType.GreaterOrEqual, "MscDate", Start));
			esqPlanServiceJob.Filters.Add(esqPlanServiceJob.CreateFilterWithParameters(FilterComparisonType.Equal, "MscServiceJobSchedule.MscServiceWorkplace", MscServiceWorkplaceId));

			var Collection = esqPlanServiceJob.GetEntityCollection(UserConnection);
			var QuantityRecord = Collection.Count;
			if (QuantityRecord > 0)
			{
				if (QuantityRecord == 1)
				{
					var ent = Collection[0];
					TimeSpan MscStart = ent.GetTypedColumnValue<TimeSpan>("MscStart");
					TimeSpan MscFinish = ent.GetTypedColumnValue<TimeSpan>("MscFinish");

					if (MscStart != TimingStart)
					{
						AddPlanServiceJob(MscServiceWorkplaceId, Date, TimingStart, MscStart, Canceled);
					}
					if (MscFinish != TimingEnd)
					{
						AddPlanServiceJob(MscServiceWorkplaceId, Date, MscFinish, TimingEnd, Canceled);
					}
				}
				if (QuantityRecord >= 2)
				{


					if (true)
					{
						#region firstRecord

						var entFirst = Collection[0];
						TimeSpan MscStartFirst = entFirst.GetTypedColumnValue<TimeSpan>("MscStart");

						if (MscStartFirst != TimingStart)
						{
							AddPlanServiceJob(MscServiceWorkplaceId, Date, TimingStart, MscStartFirst, Canceled);
						}

						#endregion firstRecord 
					}

					if (true)
					{
						#region lastRecord

						Entity entlast = Collection[Collection.Count - 1];
						TimeSpan MscFinishLast = entlast.GetTypedColumnValue<TimeSpan>("MscFinish");
						if (MscFinishLast != TimingEnd)
						{
							AddPlanServiceJob(MscServiceWorkplaceId, Date, MscFinishLast, TimingEnd, Canceled);
						}
						#endregion lastRecord
					}
					if (true)
					{
						#region allRecord

						for (int i = 1; i < Collection.Count; i++)
						{
							Entity entCurrent = Collection[i];
							Entity entPrevious = Collection[i - 1];

							TimeSpan currentStartDate = entCurrent.GetTypedColumnValue<TimeSpan>("MscStart");
							TimeSpan currentDueDate = entCurrent.GetTypedColumnValue<TimeSpan>("MscFinish");

							TimeSpan previousStartDate = entPrevious.GetTypedColumnValue<TimeSpan>("MscStart");
							TimeSpan previousDueDate = entPrevious.GetTypedColumnValue<TimeSpan>("MscFinish");

							if (currentStartDate != previousDueDate)
							{
								AddPlanServiceJob(MscServiceWorkplaceId, Date, previousDueDate, currentStartDate, Canceled);
							}


						}

						#endregion allRecord
					}
				}
			}
		}




		private void AddPlanServiceJob(Guid serviceJobSchedule, DateTime date, TimeSpan startDate, TimeSpan dueDate,
									   Guid statusServicePostAppointment)
		{
			EntitySchema schemaWrite = UserConnection.EntitySchemaManager.GetInstanceByName("MscServicePostAppointment");
			Entity entity = schemaWrite.CreateEntity(UserConnection);
			entity.SetDefColumnValues();
			entity.SetColumnValue("MscDate", date);
			entity.SetColumnValue("MscStart", startDate);
			entity.SetColumnValue("MscFinish", dueDate);
			entity.SetColumnValue("MscServiceJobScheduleId", serviceJobSchedule);
			entity.SetColumnValue("MscStatusServicePostAppointmentId", statusServicePostAppointment);
			entity.Save(false);
		}


	}

}