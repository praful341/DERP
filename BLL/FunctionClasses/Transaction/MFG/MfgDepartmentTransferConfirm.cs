using BLL.PropertyClasses.Transaction.MFG;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction.MFG
{
    public class MfgDepartmentTransferConfirm
    {
        InterfaceLayer Ope = new InterfaceLayer();
        BLL.Validation Val = new BLL.Validation();

        public int Save(MFGDepartmentTransferProperty pClsProperty)
        {
            int IntRes = 0;
            try
            {
                Request Request = new Request();
                Request.AddParams("@transfer_id", pClsProperty.transfer_id, DbType.Int32);
                Request.AddParams("@union_id", pClsProperty.union_id, DbType.Int64);
                Request.AddParams("@receive_user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@receive_ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@receive_date", pClsProperty.receive_date, DbType.Date);
                Request.AddParams("@receive_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@janged_no", pClsProperty.janged_no, DbType.Int64);
                Request.AddParams("@packet_type_wages_id", pClsProperty.packet_type_wages_id, DbType.Int64);
                Request.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int64);
                Request.AddParams("@manager_id", pClsProperty.manager_id, DbType.Int64);
                Request.AddParams("@wages_sieve_id", pClsProperty.wages_sieve_id, DbType.Int64);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Department_TransferCnf_Save;
                Request.CommandType = CommandType.StoredProcedure;

                IntRes = Ope.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IntRes;
        }
        public int ManagerCnfDate_Update(MFGDepartmentTransferProperty pClsProperty)
        {
            int IntRes = 0;
            try
            {
                Request Request = new Request();
                Request.AddParams("@transfer_id", pClsProperty.transfer_id, DbType.Int32);
                Request.AddParams("@receive_date", pClsProperty.receive_date, DbType.Date);
                Request.AddParams("@janged_no", pClsProperty.janged_no, DbType.Int64);
                Request.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int64);
                Request.AddParams("@manager_id", pClsProperty.manager_id, DbType.Int64);
                Request.AddParams("@is_confirm", pClsProperty.is_confirm, DbType.Int32);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_ManagerCnfDate_Update;
                Request.CommandType = CommandType.StoredProcedure;

                IntRes = Ope.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IntRes;
        }
        public int ManagerRussainJobWork_Update(MFGDepartmentTransferProperty pClsProperty)
        {
            int IntRes = 0;
            try
            {
                Request Request = new Request();
                Request.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int64);
                Request.AddParams("@manager_id", pClsProperty.manager_id, DbType.Int64);
                Request.AddParams("@to_manager_id", pClsProperty.to_manager_id, DbType.Int64);
                Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_ManagerRussJobWorkData_Update;
                Request.CommandType = CommandType.StoredProcedure;

                IntRes = Ope.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IntRes;
        }
        public DataTable GetData(Int64 LotId = 0)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Department_TransferCnf_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@lot_id", LotId, DbType.Int32);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable Confirm_GetData(Int64 Rough_Cut_ID = 0)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Department_Cnf_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@rough_cut_id", Rough_Cut_ID, DbType.Int64);
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable Changed_Janged_Party_GetData(Int64 Lot_ID = 0)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Changed_Janged_Party_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@lot_id", Lot_ID, DbType.Int64);
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable JobWork_Manager_GetData(Int64 LotId = 0, Int64 Manager_ID = 0)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Russian_JobworkManager_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@lot_id", LotId, DbType.Int64);
            Request.AddParams("@to_department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int64);
            Request.AddParams("@manager_id", Manager_ID, DbType.Int64);
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
    }
}
