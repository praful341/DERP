using BLL.PropertyClasses.Report;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Report
{
    public class ReportParams
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        ReportParams_Property ReportParams_Property = new ReportParams_Property();
        public DataTable GetLiveStock(ReportParams_Property ReportParams_Property, string pStrSPName)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = pStrSPName;
            Request.AddParams("@Group_By", ReportParams_Property.Group_By_Tag, DbType.String);
            Request.AddParams("@company_id", ReportParams_Property.company_id, DbType.String);
            Request.AddParams("@branch_id", ReportParams_Property.branch_id, DbType.String);
            Request.AddParams("@location_id", ReportParams_Property.location_id, DbType.String);
            Request.AddParams("@department_id", ReportParams_Property.department_id, DbType.String);
            Request.AddParams("@datFromDate", ReportParams_Property.From_Date, DbType.Date);
            Request.AddParams("@datToDate", ReportParams_Property.To_Date, DbType.Date);
            Request.AddParams("@rate_type_id", GlobalDec.gEmployeeProperty.rate_type_id, DbType.Int32);
            Request.AddParams("@currency_id", GlobalDec.gEmployeeProperty.currency_id, DbType.Int32);
            Request.AddParams("@type", ReportParams_Property.type, DbType.String);
            Request.AddParams("@transaction_type", ReportParams_Property.transaction_type, DbType.String);
            Request.AddParams("@price_date", ReportParams_Property.price_date, DbType.Date);

            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataSet GetDemandNotingData_RangeWise(ReportParams_Property ReportParams_Property, string pStrSPName)
        {
            DataSet DTab = new DataSet();
            Request Request = new Request();
            Request.CommandText = pStrSPName;
            Request.AddParams("@Group_By", ReportParams_Property.Group_By_Tag, DbType.String);
            Request.AddParams("@company_id", ReportParams_Property.company_id, DbType.String);
            Request.AddParams("@branch_id", ReportParams_Property.branch_id, DbType.String);
            Request.AddParams("@location_id", ReportParams_Property.location_id, DbType.String);
            Request.AddParams("@department_id", ReportParams_Property.department_id, DbType.String);
            Request.AddParams("@datFromDate", ReportParams_Property.From_Date, DbType.Date);
            Request.AddParams("@datToDate", ReportParams_Property.To_Date, DbType.Date);
            Request.AddParams("@rate_type_id", GlobalDec.gEmployeeProperty.rate_type_id, DbType.Int32);
            Request.AddParams("@currency_id", GlobalDec.gEmployeeProperty.currency_id, DbType.Int32);
            Request.AddParams("@type", ReportParams_Property.type, DbType.String);
            Request.AddParams("@transaction_type", ReportParams_Property.transaction_type, DbType.String);
            Request.AddParams("@price_date", ReportParams_Property.price_date, DbType.Date);

            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataSet(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, "", Request);
            return DTab;
        }
        public DataSet GetProfitLossReport(ReportParams_Property ReportParams_Property, string pStrSPName)
        {
            DataSet DTab = new DataSet();
            Request Request = new Request();
            Request.CommandText = pStrSPName;

            Request.AddParams("@Group_By", ReportParams_Property.Group_By_Tag, DbType.String);
            Request.AddParams("@company_id", ReportParams_Property.company_id, DbType.String);
            Request.AddParams("@branch_id", ReportParams_Property.branch_id, DbType.String);
            Request.AddParams("@location_id", ReportParams_Property.location_id, DbType.String);
            Request.AddParams("@department_id", ReportParams_Property.department_id, DbType.String);
            Request.AddParams("@datFromDate", ReportParams_Property.From_Date, DbType.Date);
            Request.AddParams("@datToDate", ReportParams_Property.To_Date, DbType.Date);
            Request.AddParams("@rate_type_id", GlobalDec.gEmployeeProperty.rate_type_id, DbType.Int32);
            Request.AddParams("@currency_id", GlobalDec.gEmployeeProperty.currency_id, DbType.Int32);
            Request.AddParams("@type", ReportParams_Property.type, DbType.String);
            Request.AddParams("@transaction_type", ReportParams_Property.transaction_type, DbType.String);

            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataSet(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, "Profit", Request);
            return DTab;
        }
        public DataTable GetMFGLiveStock(ReportParams_Property ReportParams_Property, string pStrSPName)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = pStrSPName;
            Request.AddParams("@Group_By", ReportParams_Property.Group_By_Tag, DbType.String);
            Request.AddParams("@company_id", ReportParams_Property.company_id, DbType.String);
            Request.AddParams("@branch_id", ReportParams_Property.branch_id, DbType.String);
            Request.AddParams("@location_id", ReportParams_Property.location_id, DbType.String);
            Request.AddParams("@department_id", ReportParams_Property.department_id, DbType.String);
            Request.AddParams("@datFromDate", ReportParams_Property.From_Date, DbType.Date);
            Request.AddParams("@datToDate", ReportParams_Property.To_Date, DbType.Date);
            Request.AddParams("@rate_type_id", GlobalDec.gEmployeeProperty.rate_type_id, DbType.Int32);
            Request.AddParams("@currency_id", GlobalDec.gEmployeeProperty.currency_id, DbType.Int32);
            Request.AddParams("@type", ReportParams_Property.type, DbType.String);
            Request.AddParams("@transaction_type", ReportParams_Property.transaction_type, DbType.String);

            Request.AddParams("@process_id", ReportParams_Property.process_id, DbType.String);
            Request.AddParams("@sub_process_id", ReportParams_Property.sub_process_id, DbType.String);
            Request.AddParams("@to_department_id", ReportParams_Property.to_department_id, DbType.String);
            Request.AddParams("@kapan_id", ReportParams_Property.kapan_id, DbType.String);
            Request.AddParams("@rough_cut_id", ReportParams_Property.cut_id, DbType.String);
            Request.AddParams("@quality_id", ReportParams_Property.quality_id, DbType.String);
            Request.AddParams("@rough_clarity_id", ReportParams_Property.rough_clarity_id, DbType.String);
            Request.AddParams("@rough_sieve_id", ReportParams_Property.rough_sieve_id, DbType.String);
            Request.AddParams("@purity_id", ReportParams_Property.purity_id, DbType.String);
            Request.AddParams("@dept_type", ReportParams_Property.Department_Type, DbType.String);

            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetMFGRoughStock(ReportParams_Property ReportParams_Property, string pStrSPName)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = pStrSPName;
            Request.AddParams("@Group_By", ReportParams_Property.Group_By_Tag, DbType.String);
            Request.AddParams("@datFromDate", ReportParams_Property.From_Date, DbType.Date);
            Request.AddParams("@datToDate", ReportParams_Property.To_Date, DbType.Date);
            Request.AddParams("@entry_type", ReportParams_Property.entry_type, DbType.String);
            Request.AddParams("@transaction_type", ReportParams_Property.transaction_type, DbType.String);
            Request.AddParams("@kapan_id", ReportParams_Property.kapan_id, DbType.String);
            Request.AddParams("@party_id", ReportParams_Property.party_id, DbType.String);
            Request.AddParams("@purity_id", ReportParams_Property.purity_id, DbType.String);

            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetMFGRejectionStock(ReportParams_Property ReportParams_Property, string pStrSPName)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = pStrSPName;
            Request.AddParams("@Group_By", ReportParams_Property.Group_By_Tag, DbType.String);
            Request.AddParams("@datFromDate", ReportParams_Property.From_Date, DbType.Date);
            Request.AddParams("@datToDate", ReportParams_Property.To_Date, DbType.Date);
            Request.AddParams("@entry_type", ReportParams_Property.entry_type, DbType.String);
            Request.AddParams("@transaction_type", ReportParams_Property.transaction_type, DbType.String);
            Request.AddParams("@kapan_id", ReportParams_Property.kapan_id, DbType.String);
            Request.AddParams("@party_id", ReportParams_Property.party_id, DbType.String);
            Request.AddParams("@purity_id", ReportParams_Property.purity_id, DbType.String);
            Request.AddParams("@type", ReportParams_Property.type, DbType.String);

            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetAvakJavakReport(ReportParams_Property ReportParams_Property, string pStrSPName)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = pStrSPName;
            Request.AddParams("@Group_By", ReportParams_Property.Group_By_Tag, DbType.String);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.String);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.String);
            Request.AddParams("@location_id", ReportParams_Property.location_id, DbType.String);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.String);
            Request.AddParams("@datFromDate", ReportParams_Property.From_Date, DbType.Date);
            Request.AddParams("@datToDate", ReportParams_Property.To_Date, DbType.Date);
            Request.AddParams("@from_party_id", ReportParams_Property.from_party_id, DbType.String);
            Request.AddParams("@to_party_id", ReportParams_Property.to_party_id, DbType.String);
            Request.AddParams("@party_group_id", ReportParams_Property.party_group_id, DbType.String);
            Request.AddParams("@party_id", ReportParams_Property.party_id, DbType.String);
            Request.AddParams("@entry_type", ReportParams_Property.entry_type, DbType.String);
            Request.AddParams("@user_id", ReportParams_Property.user_id, DbType.String);

            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }

        public DataSet GetAvakJavakReport_GetData(ReportParams_Property ReportParams_Property, string pStrSPName)
        {
            DataSet DTab = new DataSet();
            Request Request = new Request();
            Request.CommandText = pStrSPName;
            Request.AddParams("@Group_By", ReportParams_Property.Group_By_Tag, DbType.String);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.String);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.String);
            Request.AddParams("@location_id", ReportParams_Property.location_id, DbType.String);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.String);
            Request.AddParams("@datFromDate", ReportParams_Property.From_Date, DbType.Date);
            Request.AddParams("@datToDate", ReportParams_Property.To_Date, DbType.Date);
            Request.AddParams("@from_party_id", ReportParams_Property.from_party_id, DbType.String);
            Request.AddParams("@to_party_id", ReportParams_Property.to_party_id, DbType.String);
            Request.AddParams("@entry_type", ReportParams_Property.entry_type, DbType.String);
            Request.AddParams("@user_id", ReportParams_Property.user_id, DbType.String);

            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataSet(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, "", Request);
            return DTab;
        }

        public DataTable GetMFGJangedData(ReportParams_Property ReportParams_Property, string pStrSPName)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = pStrSPName;
            Request.AddParams("@Group_By", ReportParams_Property.Group_By_Tag, DbType.String);
            Request.AddParams("@datFromDate", ReportParams_Property.From_Date, DbType.Date);
            Request.AddParams("@datToDate", ReportParams_Property.To_Date, DbType.Date);
            Request.AddParams("@dept_type", ReportParams_Property.Department_Type, DbType.String);
            Request.AddParams("@kapan_id", ReportParams_Property.kapan_id, DbType.String);
            Request.AddParams("@cut_id", ReportParams_Property.cut_id, DbType.String);
            Request.AddParams("@party_id", ReportParams_Property.party_id, DbType.String);

            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetData_JangedReturn_Galaxy(ReportParams_Property ReportParams_Property, string pStrSPName)
        {
            DataTable DTab = new DataTable();
            try
            {
                Request Request = new Request();
                Request.CommandText = pStrSPName;
                Request.CommandType = CommandType.StoredProcedure;

                Request.AddParams("@cut_id", ReportParams_Property.cut_id, DbType.String);

                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
                return DTab;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return DTab;
            }
        }
        public DataTable GetMFGProcessData(ReportParams_Property ReportParams_Property, string pStrSPName)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = pStrSPName;
            Request.AddParams("@Group_By", ReportParams_Property.Group_By_Tag, DbType.String);
            Request.AddParams("@department_id", ReportParams_Property.department_id, DbType.String);
            Request.AddParams("@datFromDate", ReportParams_Property.From_Date, DbType.Date);
            Request.AddParams("@datToDate", ReportParams_Property.To_Date, DbType.Date);
            Request.AddParams("@FromTime", ReportParams_Property.From_Time, DbType.String);
            Request.AddParams("@ToTime", ReportParams_Property.To_Time, DbType.String);
            Request.AddParams("@process_id", ReportParams_Property.process_id, DbType.String);
            Request.AddParams("@sub_process_id", ReportParams_Property.sub_process_id, DbType.String);
            Request.AddParams("@kapan_id", ReportParams_Property.kapan_id, DbType.String);
            Request.AddParams("@rough_cut_id", ReportParams_Property.cut_id, DbType.String);

            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetMFGBoilCharniProcessData(ReportParams_Property ReportParams_Property, string pStrSPName)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = pStrSPName;
            Request.AddParams("@Group_By", ReportParams_Property.Group_By_Tag, DbType.String);
            Request.AddParams("@company_id", ReportParams_Property.company_id, DbType.String);
            Request.AddParams("@branch_id", ReportParams_Property.branch_id, DbType.String);
            Request.AddParams("@location_id", ReportParams_Property.location_id, DbType.String);
            Request.AddParams("@department_id", ReportParams_Property.department_id, DbType.String);
            Request.AddParams("@datFromDate", ReportParams_Property.From_Date, DbType.Date);
            Request.AddParams("@datToDate", ReportParams_Property.To_Date, DbType.Date);
            Request.AddParams("@FromTime", ReportParams_Property.From_Time, DbType.String);
            Request.AddParams("@ToTime", ReportParams_Property.To_Time, DbType.String);
            Request.AddParams("@process_id", ReportParams_Property.process_id, DbType.String);
            Request.AddParams("@sub_process_id", ReportParams_Property.sub_process_id, DbType.String);

            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetMFGMachineItemData(ReportParams_Property ReportParams_Property, string pStrSPName)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = pStrSPName;
            Request.AddParams("@Group_By", ReportParams_Property.Group_By_Tag, DbType.String);
            Request.AddParams("@company_id", ReportParams_Property.company_id, DbType.String);
            Request.AddParams("@branch_id", ReportParams_Property.branch_id, DbType.String);
            Request.AddParams("@location_id", ReportParams_Property.location_id, DbType.String);
            //Request.AddParams("@department_id", ReportParams_Property.department_id, DbType.String);
            Request.AddParams("@datFromDate", ReportParams_Property.From_Date, DbType.Date);
            Request.AddParams("@datToDate", ReportParams_Property.To_Date, DbType.Date);
            Request.AddParams("@datFromInsDate", ReportParams_Property.From_Install_Date, DbType.Date);
            Request.AddParams("@datToInsDate", ReportParams_Property.To_Install_Date, DbType.Date);
            Request.AddParams("@FromTime", ReportParams_Property.From_Time, DbType.String);
            Request.AddParams("@ToTime", ReportParams_Property.To_Time, DbType.String);
            //Request.AddParams("@process_id", ReportParams_Property.process_id, DbType.String);
            //Request.AddParams("@sub_process_id", ReportParams_Property.sub_process_id, DbType.String);

            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataSet GetMFGAssortment_AllINOne_Data(ReportParams_Property ReportParams_Property, string pStrSPName)
        {
            DataSet DTab = new DataSet();
            Request Request = new Request();
            Request.CommandText = pStrSPName;

            Request.AddParams("@from_date", ReportParams_Property.From_Date, DbType.Date);
            Request.AddParams("@to_date", ReportParams_Property.To_Date, DbType.Date);
            Request.AddParams("@kapan_id", ReportParams_Property.kapan_id, DbType.String);
            Request.AddParams("@cut_id", ReportParams_Property.cut_id, DbType.String);
            Request.AddParams("@sieve_id", ReportParams_Property.rough_sieve_id, DbType.String);
            Request.AddParams("@purity_id", ReportParams_Property.purity_id, DbType.String);
            Request.AddParams("@cut_no", ReportParams_Property.rough_cut_no, DbType.String);
            Request.AddParams("@location_id", ReportParams_Property.location_id, DbType.String);
            Request.AddParams("@kapan_no", ReportParams_Property.kapan_no, DbType.String);
            Request.AddParams("@rate_date", ReportParams_Property.rate_date, DbType.Date);
            Request.AddParams("@rate_type_id", GlobalDec.gEmployeeProperty.rate_type_id, DbType.Int32);
            Request.AddParams("@currency_id", GlobalDec.gEmployeeProperty.currency_id, DbType.Int32);

            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataSet(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, "", Request);
            return DTab;
        }
        public DataSet GetMFGAssortment_Cut_Comparision_Data(ReportParams_Property ReportParams_Property, string pStrSPName)
        {
            DataSet DTab = new DataSet();
            Request Request = new Request();
            Request.CommandText = pStrSPName;

            Request.AddParams("@from_date", ReportParams_Property.From_Date, DbType.Date);
            Request.AddParams("@to_date", ReportParams_Property.To_Date, DbType.Date);
            Request.AddParams("@kapan_id", ReportParams_Property.kapan_id, DbType.String);
            Request.AddParams("@cut_id", ReportParams_Property.cut_id, DbType.String);
            Request.AddParams("@from_rough_cut_id", ReportParams_Property.from_rough_cut_id, DbType.Int64);
            Request.AddParams("@to_rough_cut_id", ReportParams_Property.to_rough_cut_id, DbType.Int64);
            Request.AddParams("@cut_no", ReportParams_Property.rough_cut_no, DbType.String);
            Request.AddParams("@location_id", ReportParams_Property.location_id, DbType.String);
            Request.AddParams("@kapan_no", ReportParams_Property.kapan_no, DbType.String);
            Request.AddParams("@rate_date", ReportParams_Property.rate_date, DbType.Date);
            Request.AddParams("@rate_type_id", GlobalDec.gEmployeeProperty.rate_type_id, DbType.Int32);
            Request.AddParams("@currency_id", GlobalDec.gEmployeeProperty.currency_id, DbType.Int32);
            Request.AddParams("@from_rough_cut_no", ReportParams_Property.from_rough_cut_no, DbType.String);
            Request.AddParams("@to_rough_cut_no", ReportParams_Property.to_rough_cut_no, DbType.String);

            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataSet(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, "", Request);
            return DTab;
        }
        public DataSet Print_Assort_Final_Print3(ReportParams_Property ReportParams_Property, string pStrSPName)
        {
            DataSet DTab = new DataSet();
            Request Request = new Request();
            Request.CommandText = pStrSPName;

            Request.AddParams("@from_date", ReportParams_Property.From_Date, DbType.Date);
            Request.AddParams("@to_date", ReportParams_Property.To_Date, DbType.Date);
            Request.AddParams("@kapan_id", ReportParams_Property.kapan_id, DbType.String);
            Request.AddParams("@cut_id", ReportParams_Property.cut_id, DbType.String);
            Request.AddParams("@from_rough_cut_id", ReportParams_Property.from_rough_cut_id, DbType.Int64);
            Request.AddParams("@to_rough_cut_id", ReportParams_Property.to_rough_cut_id, DbType.Int64);
            Request.AddParams("@cut_no", ReportParams_Property.rough_cut_no, DbType.String);
            Request.AddParams("@location_id", ReportParams_Property.location_id, DbType.String);
            Request.AddParams("@kapan_no", ReportParams_Property.kapan_no, DbType.String);
            Request.AddParams("@rate_date", ReportParams_Property.rate_date, DbType.Date);
            Request.AddParams("@rate_type_id", GlobalDec.gEmployeeProperty.rate_type_id, DbType.Int32);
            Request.AddParams("@currency_id", GlobalDec.gEmployeeProperty.currency_id, DbType.Int32);
            Request.AddParams("@from_rough_cut_no", ReportParams_Property.from_rough_cut_no, DbType.String);
            Request.AddParams("@to_rough_cut_no", ReportParams_Property.to_rough_cut_no, DbType.String);

            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataSet(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, "", Request);
            return DTab;
        }
        public DataSet Print_Assort_Final_Print2_ALL(ReportParams_Property ReportParams_Property, string pStrSPName)
        {
            DataSet DTab = new DataSet();
            Request Request = new Request();
            Request.CommandText = pStrSPName;

            Request.AddParams("@from_date", ReportParams_Property.From_Date, DbType.Date);
            Request.AddParams("@to_date", ReportParams_Property.To_Date, DbType.Date);
            Request.AddParams("@kapan_id", ReportParams_Property.kapan_id, DbType.String);
            Request.AddParams("@cut_id", ReportParams_Property.cut_id, DbType.String);
            Request.AddParams("@sieve_id", ReportParams_Property.rough_sieve_id, DbType.String);
            Request.AddParams("@purity_id", ReportParams_Property.purity_id, DbType.String);
            Request.AddParams("@cut_no", ReportParams_Property.rough_cut_no, DbType.String);
            Request.AddParams("@location_id", ReportParams_Property.location_id, DbType.String);
            Request.AddParams("@kapan_no", ReportParams_Property.kapan_no, DbType.String);
            Request.AddParams("@rate_date", ReportParams_Property.rate_date, DbType.Date);
            Request.AddParams("@rate_type_id", GlobalDec.gEmployeeProperty.rate_type_id, DbType.Int32);
            Request.AddParams("@currency_id", GlobalDec.gEmployeeProperty.currency_id, DbType.Int32);


            Request.CommandType = CommandType.StoredProcedure;
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                Ope.GetDataSet(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, "", Request);
            }
            else
            {
                Ope.GetDataSet(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, DTab, "", Request);
            }
            return DTab;
        }
        public DataSet Print_Assort_Final_Mumbai(ReportParams_Property ReportParams_Property, string pStrSPName)
        {
            DataSet DTab = new DataSet();
            Request Request = new Request();
            Request.CommandText = pStrSPName;

            Request.AddParams("@from_date", ReportParams_Property.From_Date, DbType.Date);
            Request.AddParams("@to_date", ReportParams_Property.To_Date, DbType.Date);
            Request.AddParams("@kapan_id", ReportParams_Property.kapan_id, DbType.String);
            Request.AddParams("@cut_id", ReportParams_Property.cut_id, DbType.String);
            Request.AddParams("@sieve_id", ReportParams_Property.rough_sieve_id, DbType.String);
            Request.AddParams("@purity_id", ReportParams_Property.purity_id, DbType.String);
            Request.AddParams("@cut_no", ReportParams_Property.rough_cut_no, DbType.String);
            Request.AddParams("@location_id", ReportParams_Property.location_id, DbType.String);
            Request.AddParams("@kapan_no", ReportParams_Property.kapan_no, DbType.String);
            Request.AddParams("@rate_date", ReportParams_Property.rate_date, DbType.Date);
            Request.AddParams("@rate_type_id", GlobalDec.gEmployeeProperty.rate_type_id, DbType.Int32);
            Request.AddParams("@currency_id", GlobalDec.gEmployeeProperty.currency_id, DbType.Int32);

            Request.CommandType = CommandType.StoredProcedure;

            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                Ope.GetDataSet(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, "", Request);
            }
            else
            {
                Ope.GetDataSet(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, DTab, "", Request);
            }
            return DTab;
        }
        public DataSet Print_Semi_2(ReportParams_Property ReportParams_Property, string pStrSPName)
        {
            DataSet DTab = new DataSet();
            Request Request = new Request();
            Request.CommandText = pStrSPName;

            Request.AddParams("@from_date", ReportParams_Property.From_Date, DbType.Date);
            Request.AddParams("@to_date", ReportParams_Property.To_Date, DbType.Date);
            Request.AddParams("@kapan_id", ReportParams_Property.kapan_id, DbType.String);
            Request.AddParams("@cut_id", ReportParams_Property.cut_id, DbType.String);
            Request.AddParams("@temp_purity_name", ReportParams_Property.temp_quality_name, DbType.String);
            Request.AddParams("@temp_sieve_name", null, DbType.String);
            Request.AddParams("@cut_no", ReportParams_Property.rough_cut_no, DbType.String);
            Request.AddParams("@temp_quality_name_Trim", ReportParams_Property.temp_quality_name_Trim, DbType.String);
            Request.AddParams("@location_id", ReportParams_Property.location_id, DbType.String);
            Request.AddParams("@kapan_no", ReportParams_Property.kapan_no, DbType.String);

            Request.CommandType = CommandType.StoredProcedure;
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                Ope.GetDataSet(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, "Semi", Request);
            }
            else
            {
                Ope.GetDataSet(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, DTab, "Semi", Request);
            }
            return DTab;
        }
        public DataTable Print_Semi_1_Sub(ReportParams_Property ReportParams_Property, string pStrSPName)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = pStrSPName;

            Request.AddParams("@from_date", ReportParams_Property.From_Date, DbType.Date);
            Request.AddParams("@to_date", ReportParams_Property.To_Date, DbType.Date);
            Request.AddParams("@kapan_id", ReportParams_Property.kapan_id, DbType.String);
            Request.AddParams("@cut_id", ReportParams_Property.cut_id, DbType.String);
            Request.AddParams("@temp_purity_name", ReportParams_Property.temp_quality_name, DbType.String);
            Request.AddParams("@temp_sieve_name", null, DbType.String);
            Request.AddParams("@cut_no", ReportParams_Property.rough_cut_no, DbType.String);
            Request.AddParams("@location_id", ReportParams_Property.location_id, DbType.String);

            Request.CommandType = CommandType.StoredProcedure;
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            }
            else
            {
                Ope.GetDataTable(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, DTab, Request);
            }
            return DTab;
        }
        public DataTable Print_Semi_1(ReportParams_Property ReportParams_Property, string pStrSPName)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = pStrSPName;

            Request.AddParams("@from_date", ReportParams_Property.From_Date, DbType.Date);
            Request.AddParams("@to_date", ReportParams_Property.To_Date, DbType.Date);
            Request.AddParams("@kapan_id", ReportParams_Property.kapan_id, DbType.String);
            Request.AddParams("@cut_id", ReportParams_Property.cut_id, DbType.String);
            Request.AddParams("@temp_purity_name", ReportParams_Property.temp_quality_name, DbType.String);
            Request.AddParams("@temp_sieve_name", null, DbType.String);
            Request.AddParams("@cut_no", ReportParams_Property.rough_cut_no, DbType.String);
            Request.AddParams("@temp_quality_name_Trim", ReportParams_Property.temp_quality_name_Trim, DbType.String);
            Request.AddParams("@location_id", ReportParams_Property.location_id, DbType.String);
            Request.AddParams("@kapan_no", ReportParams_Property.kapan_no, DbType.String);

            Request.CommandType = CommandType.StoredProcedure;
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            }
            else
            {
                Ope.GetDataTable(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, DTab, Request);
            }
            return DTab;
        }
        public DataTable GetMFGLiveStockNew(ReportParams_Property ReportParams_Property, string pStrSPName)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = pStrSPName;
            Request.AddParams("@Group_By", ReportParams_Property.Group_By_Tag, DbType.String);
            Request.AddParams("@company_id", ReportParams_Property.company_id, DbType.String);
            Request.AddParams("@branch_id", ReportParams_Property.branch_id, DbType.String);
            Request.AddParams("@location_id", ReportParams_Property.location_id, DbType.String);
            Request.AddParams("@department_id", ReportParams_Property.department_id, DbType.String);
            Request.AddParams("@datFromDate", ReportParams_Property.From_Date, DbType.Date);
            Request.AddParams("@datToDate", ReportParams_Property.To_Date, DbType.Date);
            Request.AddParams("@rate_type_id", GlobalDec.gEmployeeProperty.rate_type_id, DbType.Int32);
            Request.AddParams("@currency_id", GlobalDec.gEmployeeProperty.currency_id, DbType.Int32);
            Request.AddParams("@type", ReportParams_Property.type, DbType.String);
            Request.AddParams("@transaction_type", ReportParams_Property.transaction_type, DbType.String);

            Request.AddParams("@process_id", ReportParams_Property.process_id, DbType.String);
            Request.AddParams("@sub_process_id", ReportParams_Property.sub_process_id, DbType.String);
            Request.AddParams("@kapan_id", ReportParams_Property.kapan_id, DbType.String);
            Request.AddParams("@rough_cut_id", ReportParams_Property.cut_id, DbType.String);
            Request.AddParams("@quality_id", ReportParams_Property.quality_id, DbType.String);
            Request.AddParams("@rough_clarity_id", ReportParams_Property.rough_clarity_id, DbType.String);
            Request.AddParams("@rough_sieve_id", ReportParams_Property.rough_sieve_id, DbType.String);
            Request.AddParams("@purity_id", ReportParams_Property.purity_id, DbType.String);

            Request.CommandType = CommandType.StoredProcedure;
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            }
            else
            {
                Ope.GetDataTable(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, DTab, Request);
            }
            return DTab;
        }
        public DataTable GetMFGLiveStockASOnDate(ReportParams_Property ReportParams_Property, string pStrSPName)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = pStrSPName;
            Request.AddParams("@Group_By", ReportParams_Property.Group_By_Tag, DbType.String);
            Request.AddParams("@company_id", ReportParams_Property.company_id, DbType.String);
            Request.AddParams("@branch_id", ReportParams_Property.branch_id, DbType.String);
            Request.AddParams("@location_id", ReportParams_Property.location_id, DbType.String);
            Request.AddParams("@department_id", ReportParams_Property.department_id, DbType.String);
            Request.AddParams("@datFromDate", ReportParams_Property.From_Date, DbType.Date);
            Request.AddParams("@datToDate", ReportParams_Property.To_Date, DbType.Date);
            Request.AddParams("@rate_type_id", GlobalDec.gEmployeeProperty.rate_type_id, DbType.Int32);
            Request.AddParams("@currency_id", GlobalDec.gEmployeeProperty.currency_id, DbType.Int32);
            Request.AddParams("@type", ReportParams_Property.type, DbType.String);
            Request.AddParams("@transaction_type", ReportParams_Property.transaction_type, DbType.String);
            Request.AddParams("@dept_type", ReportParams_Property.Department_Type, DbType.String);

            Request.AddParams("@process_id", ReportParams_Property.process_id, DbType.String);
            Request.AddParams("@sub_process_id", ReportParams_Property.sub_process_id, DbType.String);
            Request.AddParams("@kapan_id", ReportParams_Property.kapan_id, DbType.String);
            Request.AddParams("@rough_cut_id", ReportParams_Property.cut_id, DbType.String);
            Request.AddParams("@quality_id", ReportParams_Property.quality_id, DbType.String);
            Request.AddParams("@rough_clarity_id", ReportParams_Property.rough_clarity_id, DbType.String);
            Request.AddParams("@rough_sieve_id", ReportParams_Property.rough_sieve_id, DbType.String);
            Request.AddParams("@purity_id", ReportParams_Property.purity_id, DbType.String);

            Request.CommandType = CommandType.StoredProcedure;
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            }
            else
            {
                Ope.GetDataTable(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, DTab, Request);
            }
            return DTab;
        }
        public DataTable GetIssuePendingReport(ReportParams_Property ReportParams_Property, string pStrSPName)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = pStrSPName;
            Request.AddParams("@group_By", ReportParams_Property.Group_By_Tag, DbType.String);
            Request.AddParams("@company_id", ReportParams_Property.company_id, DbType.String);
            Request.AddParams("@branch_id", ReportParams_Property.branch_id, DbType.String);
            Request.AddParams("@location_id", ReportParams_Property.location_id, DbType.String);
            Request.AddParams("@department_id", ReportParams_Property.department_id, DbType.String);
            Request.AddParams("@rough_cut_id", ReportParams_Property.cut_id, DbType.String);
            Request.AddParams("@kapan_id", ReportParams_Property.kapan_id, DbType.String);
            Request.AddParams("@process_id", ReportParams_Property.process_id, DbType.String);
            Request.AddParams("@fromdate", ReportParams_Property.From_Date, DbType.Date);
            Request.AddParams("@todate", ReportParams_Property.To_Date, DbType.Date);
            Request.AddParams("@type", ReportParams_Property.type, DbType.String);

            Request.CommandType = CommandType.StoredProcedure;
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            }
            else
            {
                Ope.GetDataTable(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, DTab, Request);
            }
            return DTab;
        }
        public DataTable GetMFG4PReport(ReportParams_Property ReportParams_Property, string pStrSPName)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = pStrSPName;
            Request.AddParams("@Group_By", ReportParams_Property.Group_By_Tag, DbType.String);
            Request.AddParams("@company_id", ReportParams_Property.company_id, DbType.String);
            Request.AddParams("@branch_id", ReportParams_Property.branch_id, DbType.String);
            Request.AddParams("@location_id", ReportParams_Property.location_id, DbType.String);
            Request.AddParams("@department_id", ReportParams_Property.department_id, DbType.String);
            Request.AddParams("@datFromDate", ReportParams_Property.From_Date, DbType.Date);
            Request.AddParams("@datToDate", ReportParams_Property.To_Date, DbType.Date);
            Request.AddParams("@FromTime", ReportParams_Property.From_Time, DbType.String);
            Request.AddParams("@ToTime", ReportParams_Property.To_Time, DbType.String);
            Request.AddParams("@rate_type_id", GlobalDec.gEmployeeProperty.rate_type_id, DbType.Int32);
            Request.AddParams("@currency_id", GlobalDec.gEmployeeProperty.currency_id, DbType.Int32);
            Request.AddParams("@type", ReportParams_Property.type, DbType.String);
            Request.AddParams("@transaction_type", ReportParams_Property.transaction_type, DbType.String);
            Request.AddParams("@process_id", ReportParams_Property.process_id, DbType.String);
            Request.AddParams("@sub_process_id", ReportParams_Property.sub_process_id, DbType.String);

            Request.CommandType = CommandType.StoredProcedure;
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            }
            else
            {
                Ope.GetDataTable(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, DTab, Request);
            }
            return DTab;
        }
        public DataTable GetGalaxyKapanReport(ReportParams_Property ReportParams_Property, string pStrSPName)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = pStrSPName;
            Request.AddParams("@report_type", ReportParams_Property.report_type, DbType.String);
            Request.AddParams("@kapan_no", ReportParams_Property.kapan_no, DbType.String);
            Request.AddParams("@shape_id", ReportParams_Property.galaxy_shape_id, DbType.String);
            Request.AddParams("@color_id", ReportParams_Property.galaxy_color_id, DbType.String);
            Request.AddParams("@clarity_id", ReportParams_Property.galaxy_clarity_id, DbType.String);
            Request.AddParams("@datFromDate", ReportParams_Property.From_Date, DbType.Date);
            Request.AddParams("@datToDate", ReportParams_Property.To_Date, DbType.Date);
            Request.AddParams("@cut_id", ReportParams_Property.galaxy_cut_id, DbType.String);
            Request.AddParams("@sieve_name", ReportParams_Property.galaxy_sieve_name, DbType.String);
            Request.AddParams("@lot_no", ReportParams_Property.galaxy_lot_no, DbType.String);
            Request.AddParams("@machine_no", ReportParams_Property.galaxy_machine_no, DbType.String);

            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);

            return DTab;
        }
        public DataTable GetGalaxyKapanData_SaveReport(ReportParams_Property ReportParams_Property, string pStrSPName)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = pStrSPName;
            Request.AddParams("@kapan_no", ReportParams_Property.kapan_no, DbType.String);

            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);

            return DTab;
        }
        public DataTable GetGalaxy_XMLImport_Report(ReportParams_Property ReportParams_Property, string pStrSPName)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = pStrSPName;
            Request.AddParams("@datFromDate", ReportParams_Property.From_Date, DbType.Date);
            Request.AddParams("@datToDate", ReportParams_Property.To_Date, DbType.Date);

            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);

            return DTab;
        }
        public DataTable GetIssuePendingStock(ReportParams_Property ReportParams_Property, string pStrSPName)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = pStrSPName;
            Request.AddParams("@company_id", ReportParams_Property.company_id, DbType.String);
            Request.AddParams("@branch_id", ReportParams_Property.branch_id, DbType.String);
            Request.AddParams("@location_id", ReportParams_Property.location_id, DbType.String);
            Request.AddParams("@department_id", ReportParams_Property.department_id, DbType.String);
            Request.AddParams("@cut_id", ReportParams_Property.cut_id, DbType.String);
            Request.AddParams("@kapan_id", ReportParams_Property.kapan_id, DbType.String);
            Request.AddParams("@fromDate", ReportParams_Property.From_Date, DbType.Date);
            Request.AddParams("@toDate", ReportParams_Property.To_Date, DbType.Date);

            Request.CommandType = CommandType.StoredProcedure;
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            }
            else
            {
                Ope.GetDataTable(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, DTab, Request);
            }
            return DTab;
        }
        public DataTable GetPriceList(ReportParams_Property ReportParams_Property, string pStrSPName)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = pStrSPName;
            Request.AddParams("@datFromDate", ReportParams_Property.From_Date, DbType.Date);
            Request.AddParams("@datToDate", ReportParams_Property.To_Date, DbType.Date);
            Request.AddParams("@rate_date", ReportParams_Property.rate_date, DbType.Date);
            Request.AddParams("@rate_type_id", ReportParams_Property.rate_type_id, DbType.Int32);
            Request.AddParams("@currency_id", ReportParams_Property.currency_id, DbType.Int32);
            Request.AddParams("@last_sale", ReportParams_Property.last_sale, DbType.Int32);

            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataSet GetPriceListRPT(ReportParams_Property ReportParams_Property, string pStrSPName)
        {
            DataSet DTab = new DataSet();
            Request Request = new Request();
            Request.CommandText = pStrSPName;
            Request.AddParams("@rate_date", ReportParams_Property.rate_date, DbType.Date);
            Request.AddParams("@rate_type_id", ReportParams_Property.rate_type_id, DbType.Int32);
            Request.AddParams("@currency_id", ReportParams_Property.currency_id, DbType.Int32);
            Request.AddParams("@datFromDate", ReportParams_Property.From_Date, DbType.Date);
            Request.AddParams("@datToDate", ReportParams_Property.To_Date, DbType.Date);

            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataSet(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, "", Request);
            return DTab;
        }
        public DataSet GetPriceListRPT_Diff(ReportParams_Property ReportParams_Property, string pStrSPName)
        {
            DataSet DTab = new DataSet();
            Request Request = new Request();
            Request.CommandText = pStrSPName;
            Request.AddParams("@rate_date", ReportParams_Property.rate_date, DbType.Date);
            Request.AddParams("@rate_type_id", ReportParams_Property.rate_type_id, DbType.Int32);
            Request.AddParams("@currency_id", ReportParams_Property.currency_id, DbType.Int32);
            Request.AddParams("@datFromDate", ReportParams_Property.From_Date, DbType.Date);
            Request.AddParams("@datToDate", ReportParams_Property.To_Date, DbType.Date);
            Request.AddParams("@price_date_1", ReportParams_Property.price_date_1, DbType.Date);
            Request.AddParams("@price_date_2", ReportParams_Property.price_date_2, DbType.Date);

            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataSet(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, "", Request);
            return DTab;
        }
        public DataTable Get_Transaction_View_Report(ReportParams_Property pClsProperty, string pStrSPName)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@group_by_", pClsProperty.Group_By_Tag, DbType.String);
            Request.AddParams("@from_issue_date_", pClsProperty.From_Date, DbType.Date);
            Request.AddParams("@to_issue_date_", pClsProperty.To_Date, DbType.Date);
            Request.AddParams("@cash_type_", pClsProperty.Cash_Type, DbType.String);
            Request.AddParams("@ledger_id_", pClsProperty.ledger_id, DbType.String);
            Request.AddParams("@company_id", pClsProperty.company_id, DbType.String);
            Request.AddParams("@branch_id", pClsProperty.branch_id, DbType.String);
            Request.AddParams("@location_id", pClsProperty.location_id, DbType.String);
            Request.AddParams("@department_id", pClsProperty.department_id, DbType.String);
            Request.AddParams("@ledger_name", pClsProperty.ledger_name, DbType.String);

            Request.CommandText = pStrSPName;
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetLotingData(ReportParams_Property ReportParams_Property, string pStrSPName)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = pStrSPName;
            Request.AddParams("@Group_By", ReportParams_Property.Group_By_Tag, DbType.String);
            Request.AddParams("@company_id", ReportParams_Property.company_id, DbType.String);
            Request.AddParams("@branch_id", ReportParams_Property.branch_id, DbType.String);
            Request.AddParams("@location_id", ReportParams_Property.location_id, DbType.String);
            Request.AddParams("@department_id", ReportParams_Property.department_id, DbType.String);
            Request.AddParams("@datFromDate", ReportParams_Property.From_Date, DbType.Date);
            Request.AddParams("@datToDate", ReportParams_Property.To_Date, DbType.Date);
            Request.AddParams("@rate_type_id", GlobalDec.gEmployeeProperty.rate_type_id, DbType.Int32);
            Request.AddParams("@currency_id", GlobalDec.gEmployeeProperty.currency_id, DbType.Int32);
            Request.AddParams("@type", ReportParams_Property.type, DbType.String);
            Request.AddParams("@transaction_type", ReportParams_Property.transaction_type, DbType.String);
            Request.AddParams("@is_loting", ReportParams_Property.is_Loting, DbType.Int32);
            Request.AddParams("@cut_id", ReportParams_Property.cut_id, DbType.String);
            Request.AddParams("@kapan_id", ReportParams_Property.kapan_id, DbType.String);
            Request.AddParams("@process_id", ReportParams_Property.process_id, DbType.String);
            Request.AddParams("@sub_process_id", ReportParams_Property.sub_process_id, DbType.String);

            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetEmployeeDetail(ReportParams_Property ReportParams_Property, string pStrSPName)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = pStrSPName;
            Request.AddParams("@Group_By", ReportParams_Property.Group_By_Tag, DbType.String);
            Request.AddParams("@company_id", ReportParams_Property.company_id, DbType.String);
            Request.AddParams("@branch_id", ReportParams_Property.branch_id, DbType.String);
            Request.AddParams("@location_id", ReportParams_Property.location_id, DbType.String);
            Request.AddParams("@department_id", ReportParams_Property.department_id, DbType.String);
            Request.AddParams("@datJoiningFromDate", ReportParams_Property.joining_from_date, DbType.Date);
            Request.AddParams("@datJoiningToDate", ReportParams_Property.joining_to_date, DbType.Date);
            Request.AddParams("@datLeaveFromDate", ReportParams_Property.From_Date, DbType.Date);
            Request.AddParams("@datLeaveToDate", ReportParams_Property.To_Date, DbType.Date);
            Request.AddParams("@tillDate", ReportParams_Property.till_date, DbType.Date);
            Request.AddParams("@sub_process_id", ReportParams_Property.sub_process_id, DbType.String);

            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }

        public DataTable GetStoreReport(ReportParams_Property ReportParams_Property, string pStrSPName)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = pStrSPName;
            Request.AddParams("@Group_By", ReportParams_Property.Group_By_Tag, DbType.String);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.String);
            //Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.String);
            Request.AddParams("@location_id", ReportParams_Property.location_id, DbType.String);
            Request.AddParams("@department_id", ReportParams_Property.department_id, DbType.String);
            Request.AddParams("@datFromDate", ReportParams_Property.From_Date, DbType.Date);
            Request.AddParams("@datToDate", ReportParams_Property.To_Date, DbType.Date);
            Request.AddParams("@manager_id", ReportParams_Property.store_manager, DbType.String);
            Request.AddParams("@party_id", ReportParams_Property.store_party, DbType.String);
            Request.AddParams("@item_id", ReportParams_Property.store_item, DbType.String);
            Request.AddParams("@entry_type", ReportParams_Property.entry_type, DbType.String);
            Request.AddParams("@item_type", ReportParams_Property.item_type, DbType.String);
            Request.AddParams("@branch_id", ReportParams_Property.branch_id, DbType.String);
            Request.AddParams("@division_id", ReportParams_Property.division_id, DbType.String);

            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetHRReport(ReportParams_Property ReportParams_Property, string pStrSPName)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = pStrSPName;
            Request.AddParams("@year", ReportParams_Property.year, DbType.Int32);
            Request.AddParams("@month", ReportParams_Property.month, DbType.Int32);
            Request.AddParams("@manager_id", ReportParams_Property.manager_id, DbType.Int32);
            Request.AddParams("@factory_id", ReportParams_Property.factory_id, DbType.String);
            Request.AddParams("@fact_department_id", ReportParams_Property.fact_department_id, DbType.String);
            Request.AddParams("@book_no", ReportParams_Property.book_no, DbType.String);
            Request.AddParams("@from_date", ReportParams_Property.From_Date, DbType.Date);
            Request.AddParams("@to_date", ReportParams_Property.To_Date, DbType.Date);
            Request.AddParams("@report_type", ReportParams_Property.type, DbType.String);
            Request.AddParams("@rate_date", ReportParams_Property.rate_date, DbType.Date);

            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetUserWiseDetailReport(ReportParams_Property ReportParams_Property, string pStrSPName)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = pStrSPName;
            Request.AddParams("@Group_By", ReportParams_Property.Group_By_Tag, DbType.String);
            Request.AddParams("@company_id", ReportParams_Property.company_id, DbType.String);
            Request.AddParams("@branch_id", ReportParams_Property.branch_id, DbType.String);
            Request.AddParams("@location_id", ReportParams_Property.location_id, DbType.String);
            Request.AddParams("@department_id", ReportParams_Property.department_id, DbType.String);

            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public int All_In_One_AssortReport_Save(ReportParams_Property ReportParams_Property, string pStrSPName)
        {
            int IntRes = 0;
            Request Request = new Request();
            Request.CommandText = pStrSPName;

            Request.AddParams("@from_date", ReportParams_Property.From_Date, DbType.Date);
            Request.AddParams("@to_date", ReportParams_Property.To_Date, DbType.Date);
            Request.AddParams("@kapan_id", ReportParams_Property.kapan_id, DbType.String);
            Request.AddParams("@rough_cut_id", ReportParams_Property.cut_id, DbType.String);
            Request.AddParams("@sieve_id", ReportParams_Property.rough_sieve_id, DbType.String);
            Request.AddParams("@purity_id", ReportParams_Property.purity_id, DbType.String);
            Request.AddParams("@cut_no", ReportParams_Property.rough_cut_no, DbType.String);
            Request.AddParams("@location_id", ReportParams_Property.location_id, DbType.String);
            Request.AddParams("@temp_purity_name", ReportParams_Property.temp_quality_name, DbType.String);
            Request.AddParams("@temp_sieve_name", null, DbType.String);
            Request.AddParams("@temp_quality_name_Trim", ReportParams_Property.temp_quality_name_Trim, DbType.String);
            Request.AddParams("@kapan_no", ReportParams_Property.kapan_no, DbType.String);
            Request.AddParams("@rate_date", ReportParams_Property.rate_date, DbType.Date);
            Request.AddParams("@rate_type_id", GlobalDec.gEmployeeProperty.rate_type_id, DbType.Int32);
            Request.AddParams("@currency_id", GlobalDec.gEmployeeProperty.currency_id, DbType.Int32);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

            Request.CommandType = CommandType.StoredProcedure;

            IntRes = Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
            return IntRes;
        }
        public int DeleteGalaxyLot_Detail(ReportParams_Property pClsProperty)
        {
            int IntRes = 0;
            Request Request = new Request();

            Request.AddParams("@lot_no", pClsProperty.lot_no, DbType.Int64);
            Request.AddParams("@kapan_no", pClsProperty.kapan_no, DbType.String);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

            Request.CommandText = BLL.TPV.SProc.MFG_TRN_GalaxyLotNo_Save;
            Request.CommandType = CommandType.StoredProcedure;
            IntRes += Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);

            return IntRes;
        }
        public ReportParams_Property Save_Galaxy_Report(ReportParams_Property pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@union_id", pClsProperty.Union_ID, DbType.Int64);
                Request.AddParams("@summary", pClsProperty.Summary, DbType.String);
                Request.AddParams("@galaxy", pClsProperty.Galaxy, DbType.String);
                Request.AddParams("@summary_gujarati", pClsProperty.Summary_Gujarati, DbType.String);
                Request.AddParams("@seq_no", pClsProperty.seq_no, DbType.Int32);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

                Request.CommandText = BLL.TPV.SProc.Galaxy_Report_Data_Save;
                Request.CommandType = CommandType.StoredProcedure;

                DataTable p_dtbGalaxyUnionId = new DataTable();
                if (Conn != null)
                    Conn.Inter1.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, p_dtbGalaxyUnionId, Request, pEnum);
                else
                    Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, p_dtbGalaxyUnionId, Request);

                if (p_dtbGalaxyUnionId != null)
                {
                    if (p_dtbGalaxyUnionId.Rows.Count > 0)
                    {
                        pClsProperty.Union_ID = Val.ToInt64(p_dtbGalaxyUnionId.Rows[0][0]);
                    }
                }
                else
                {
                    pClsProperty.Union_ID = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }
    }
}
