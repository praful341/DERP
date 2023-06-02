using BLL.PropertyClasses.Master;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Master
{
    public class AssortMaster
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public int Save(Assort_MasterProperty pClsProperty)
        {
            int assortId = 0;
            DataTable DTAssort = new DataTable();
            Request Request = new Request();

            Request.AddParams("@assort_id", pClsProperty.assort_id, DbType.Int32);
            Request.AddParams("@assort_name", pClsProperty.assortname, DbType.String);
            Request.AddParams("@active", pClsProperty.active, DbType.Int32);
            Request.AddParams("@remarks", pClsProperty.remarks, DbType.String);
            Request.AddParams("@sequence_no", pClsProperty.sequence_no, DbType.Int32);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

            Request.CommandText = BLL.TPV.SProc.MST_Assort_Save;
            Request.CommandType = CommandType.StoredProcedure;

            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTAssort, Request);
            }
            else
            {
                Ope.GetDataTable(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, DTAssort, Request);
            }
            if (DTAssort.Rows.Count > 0)
            {
                assortId = Val.ToInt(DTAssort.Rows[0][0]);
            }
            else
            {
                assortId = Val.ToInt(0);
            }
            return assortId;
        }
        public int StockSave(Assort_MasterProperty pClsProperty)
        {
            DataTable DTAssort = new DataTable();
            Request Request = new Request();

            Request.AddParams("@assort_id", pClsProperty.assort_id, DbType.Int32);
            Request.AddParams("@sieve_id", pClsProperty.sieve_id, DbType.Int32);
            Request.AddParams("@company_id", pClsProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", pClsProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", pClsProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", pClsProperty.department_id, DbType.Int32);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

            Request.CommandText = BLL.TPV.SProc.MST_AssortStock_Save;
            Request.CommandType = CommandType.StoredProcedure;
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
            }
            else
            {
                return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, Request);
            }
        }
        public DataTable GetData(int active = 0)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MST_Assort_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@Active", active, DbType.Int32);
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

        public string ISExists(string AssortName, Int64 AssortId)
        {
            Validation Val = new Validation();
            return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "MST_Assorts", "assort_name", "AND assort_name = '" + AssortName + "' AND NOT assort_id =" + AssortId));
        }
    }
}
