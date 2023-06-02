using BLL.PropertyClasses.Master.MFG;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Master.MFG
{
    public class MFGPartyLockMaster
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public int Save(PartyLock_MasterProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@lock_id", pClsProperty.lock_id, DbType.Int32);
            Request.AddParams("@company_id", pClsProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", pClsProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", pClsProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", pClsProperty.department_id, DbType.Int32);
            Request.AddParams("@total_pcs", pClsProperty.total_pcs, DbType.Int32);
            Request.AddParams("@from_date", pClsProperty.from_date, DbType.Date);
            Request.AddParams("@to_date", pClsProperty.to_date, DbType.Date);
            Request.AddParams("@party_id", pClsProperty.party_id, DbType.Int32);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

            Request.CommandText = BLL.TPV.SProc.MFG_MST_PartyLock_Save;
            Request.CommandType = CommandType.StoredProcedure;
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }
        public DataTable GetData(PartyLock_MasterProperty pClsProperty)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_MST_PartyLock_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@department_id", pClsProperty.department_id, DbType.Int32);
            Request.AddParams("@party_id", pClsProperty.party_id, DbType.Date);
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public int Delete(PartyLock_MasterProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@lock_id", pClsProperty.lock_id, DbType.Int32);

            Request.CommandText = BLL.TPV.SProc.MFG_MST_PartyLock_Delete;
            Request.CommandType = CommandType.StoredProcedure;
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }
        public string ISExists(int PartyId, Int64 lockId)
        {
            Validation Val = new Validation();
            return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "MFG_TRN_PartyLock", "lock_id", "AND party_id = " + PartyId + " AND NOT lock_id =" + lockId));
        }
    }
}
