using BLL.PropertyClasses.Master.MFG;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Master.MFG
{
    public class MFGInsuranceMaster
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public int Save(InsuranceRate_MasterProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@insurance_rate_id", pClsProperty.insurance_rate_id, DbType.Int32);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
            Request.AddParams("@rate_date", pClsProperty.rate_date, DbType.Date);
            Request.AddParams("@quality_id", pClsProperty.quality_id, DbType.Int32);
            Request.AddParams("@rough_sieve_id", pClsProperty.rough_sieve_id, DbType.Int32);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

            Request.CommandText = BLL.TPV.SProc.MFG_MST_InsuranceRate_Save;
            Request.CommandType = CommandType.StoredProcedure;
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }
        public int Delete(InsuranceRate_MasterProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@insurance_rate_id", pClsProperty.insurance_rate_id, DbType.Int32);

            Request.CommandText = BLL.TPV.SProc.MFG_MST_InsuranceRate_Delete;
            Request.CommandType = CommandType.StoredProcedure;
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }
        public DataTable GetData()
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_MST_InsuranceRate_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public string ISExists(int Company, int Branch, int Location, int Dept, int Quality, Int64 RoughSieve, Int64 RateId)
        {
            Validation Val = new Validation();
            return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "MFG_MST_Insurance_Rate", "insurance_rate_id", "AND company_id = " + Company + "AND branch_id = " + Branch + "AND location_id = " + Location + "AND department_id = " + Dept + "AND quality_id = " + Quality + " AND rough_sieve_id = " + RoughSieve + " AND NOT insurance_rate_id =" + RateId));
        }
    }
}
