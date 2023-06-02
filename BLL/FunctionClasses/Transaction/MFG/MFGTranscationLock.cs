using BLL.PropertyClasses.Transaction;
using DLL;
using System.Data;

namespace BLL.FunctionClasses.Transaction
{
    public class MFGTranscationLock
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();

        #region Other Function

        public int Save(MFGTransactionLockProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@company_id", pClsProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", pClsProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", pClsProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", pClsProperty.department_id, DbType.Int32);
            Request.AddParams("@fromdate", pClsProperty.from_date, DbType.Date);
            Request.AddParams("@todate", pClsProperty.to_date, DbType.Date);
            Request.AddParams("@minute", pClsProperty.minutes, DbType.Int32);
            Request.AddParams("@active", pClsProperty.active, DbType.Int32);
            Request.AddParams("@remark", pClsProperty.remark, DbType.String);
            Request.AddParams("@transaction_type", pClsProperty.transction_type, DbType.String);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.employee_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

            Request.CommandText = BLL.TPV.SProc.MFG_Transaction_Lock_Save;
            Request.CommandType = CommandType.StoredProcedure;
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }

        public DataTable GetData(MFGTransactionLockProperty pClsProperty)
        {
            Request Request = new Request();
            DataTable DTab = new DataTable();

            Request.AddParams("@department_id", pClsProperty.department_id, DbType.Int32);
            Request.AddParams("@fromdate", pClsProperty.sfrom_date, DbType.Date);
            Request.AddParams("@todate", pClsProperty.sto_date, DbType.Date);

            Request.CommandText = BLL.TPV.SProc.MFG_Transaction_Lock_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }

        public int Delete(MFGTransactionLockProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@company_id", pClsProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", pClsProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", pClsProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", pClsProperty.department_id, DbType.Int32);
            Request.AddParams("@fromdate", pClsProperty.from_date, DbType.Date);
            Request.AddParams("@todate", pClsProperty.to_date, DbType.Date);

            Request.CommandText = BLL.TPV.SProc.MFG_Transaction_Lock_Delete;
            Request.CommandType = CommandType.StoredProcedure;
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }
        #endregion        
    }
}

