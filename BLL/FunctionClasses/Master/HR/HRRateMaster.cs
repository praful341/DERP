using BLL.PropertyClasses.Master.HR;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Master.HR
{
    public class HRRateMaster
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();

        public int Save(HRRate_MasterProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@rate_id", pClsProperty.rate_id, DbType.Int64);
            Request.AddParams("@rate", pClsProperty.rate, DbType.String);
            Request.AddParams("@factory_id", pClsProperty.factory_id, DbType.Int64);
            Request.AddParams("@fact_department_id", pClsProperty.fact_department_id, DbType.Int64);
            Request.AddParams("@rate_date", pClsProperty.rate_date, DbType.Date);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

            Request.CommandText = BLL.TPV.SProc.HR_MST_Rate_Save;
            Request.CommandType = CommandType.StoredProcedure;
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }
        public int Insert_Save(HRRate_MasterProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@rate_date", pClsProperty.rate_date, DbType.Date);
            Request.AddParams("@insert_date", pClsProperty.insert_date, DbType.Date);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

            Request.CommandText = BLL.TPV.SProc.HR_MST_Rate_Insert;
            Request.CommandType = CommandType.StoredProcedure;
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }

        public DataTable GetData()
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.CommandText = BLL.TPV.SProc.HR_MST_Rate_GetData;
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public string ISExists(string Insert_Date, string Insert_Date1)
        {
            Validation Val = new Validation();
            return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "HR_MST_Rate", "rate_date", "AND rate_date = '" + Insert_Date + "'"));
        }
        public int HR_Rate_Delete(HRRate_MasterProperty pClsProperty)
        {
            try
            {
                int IntRes = 0;
                Request Request = new Request();

                Request.AddParams("@rate_id", pClsProperty.rate_id, DbType.Int64);

                Request.CommandText = BLL.TPV.SProc.HR_MST_Rate_DeleteData;
                Request.CommandType = CommandType.StoredProcedure;

                IntRes = Ope.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request);

                return IntRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
