using BLL.PropertyClasses.Transaction;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction
{
    public class MFGTargetConfirm
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();

        public DataTable GetTargetConfirm(MFGTargetConfirmProperty pClsProperty)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_Target_Confirm_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@entry_date", pClsProperty.entry_date, DbType.Date);
            Request.AddParams("@department_id", pClsProperty.department_id, DbType.Int32);
            Request.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int32);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@confirm_type", pClsProperty.confirm_type, DbType.String);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }

        public int Target_Confirm_Save(MFGTargetConfirmProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                int IntRes = 0;
                Request Request = new Request();

                Request.AddParams("@emp_performance_id", pClsProperty.emp_performance_id, DbType.Int32);
                Request.AddParams("@is_done", pClsProperty.is_done, DbType.Int32);
                Request.AddParams("@done_remarks", pClsProperty.done_remarks, DbType.String);
                Request.AddParams("@is_confirm", pClsProperty.is_confirm, DbType.Int32);
                Request.AddParams("@confirm_remarks", pClsProperty.confirm_remarks, DbType.String);
                Request.AddParams("@confirm_type", pClsProperty.confirm_type, DbType.String);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int64);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@done_date", pClsProperty.done_date, DbType.Date);
                Request.AddParams("@done_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@confirm_difference_days", pClsProperty.confirm_difference_days, DbType.Int32);

                Request.CommandText = BLL.TPV.SProc.MFG_Target_Confirm_Save;
                Request.CommandType = CommandType.StoredProcedure;
                if (Conn != null)
                    IntRes = Conn.Inter1.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request, pEnum);
                else
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
