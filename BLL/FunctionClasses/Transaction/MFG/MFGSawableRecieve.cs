using BLL.PropertyClasses.Transaction.MFG;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction.MFG
{
    public class MFGSawableRecieve
    {
        InterfaceLayer Ope = new InterfaceLayer();
        BLL.Validation Val = new BLL.Validation();

        public MFGSawableReceiveProperty Save(MFGSawableReceiveProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@receive_date", pClsProperty.receive_date, DbType.Date);
                Request.AddParams("@rough_lot_id", pClsProperty.rough_lot_id, DbType.Int32);
                Request.AddParams("@rough_cut_id", pClsProperty.rough_cut_id, DbType.Int32);
                Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int64);
                Request.AddParams("@union_id", pClsProperty.union_id, DbType.Int64);
                Request.AddParams("@receive_union_id", pClsProperty.receive_union_id, DbType.Int64);
                Request.AddParams("@issue_union_id", pClsProperty.issue_union_id, DbType.Int64);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
                Request.AddParams("@issue_id", pClsProperty.issue_id, DbType.Int32);
                Request.AddParams("@manager_id", pClsProperty.manager_id, DbType.Int32);
                Request.AddParams("@employee_id", pClsProperty.employee_id, DbType.Int32);
                Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int32);
                Request.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int32);
                Request.AddParams("@purity_id", pClsProperty.purity_id, DbType.Int64);
                Request.AddParams("@balance_pcs", pClsProperty.balance_pcs, DbType.Int32);
                Request.AddParams("@balance_carat", pClsProperty.balance_carat, DbType.Decimal);
                Request.AddParams("@pcs", pClsProperty.pcs, DbType.Int32);
                Request.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
                Request.AddParams("@loss_carat", pClsProperty.loss_carat, DbType.Decimal);
                Request.AddParams("@plus_carat", pClsProperty.plus_carat, DbType.Decimal);

                Request.AddParams("@rr_pcs", pClsProperty.rr_pcs, DbType.Int32);
                Request.AddParams("@rr_carat", pClsProperty.rr_carat, DbType.Decimal);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int32);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

                Request.AddParams("@flag", pClsProperty.flag, DbType.Int32);
                Request.AddParams("@recieve_carat", pClsProperty.recieve_carat, DbType.Decimal);
                Request.AddParams("@mix_union_id", pClsProperty.mix_union_id, DbType.Int64);
                Request.AddParams("@history_union_id", pClsProperty.history_union_id, DbType.Int64);
                Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);


                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Sawable_Receive_Save;
                Request.CommandType = CommandType.StoredProcedure;

                DataTable p_dtbProcessSawRecId = new DataTable();
                if (Conn != null)
                    Conn.Inter1.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, p_dtbProcessSawRecId, Request, pEnum);
                else
                    Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, p_dtbProcessSawRecId, Request);

                if (p_dtbProcessSawRecId != null)
                {
                    if (p_dtbProcessSawRecId.Rows.Count > 0)
                    {
                        pClsProperty.union_id = Val.ToInt64(p_dtbProcessSawRecId.Rows[0][0]);
                        pClsProperty.receive_union_id = Val.ToInt64(p_dtbProcessSawRecId.Rows[0][1]);
                        pClsProperty.issue_union_id = Val.ToInt64(p_dtbProcessSawRecId.Rows[0][2]);
                        pClsProperty.mix_union_id = Val.ToInt64(p_dtbProcessSawRecId.Rows[0][3]);
                        pClsProperty.history_union_id = Val.ToInt64(p_dtbProcessSawRecId.Rows[0][4]);
                        pClsProperty.lot_srno = Val.ToInt64(p_dtbProcessSawRecId.Rows[0][5]);
                    }
                }
                else
                {
                    pClsProperty.union_id = 0;
                    pClsProperty.receive_union_id = 0;
                    pClsProperty.issue_union_id = 0;
                    pClsProperty.mix_union_id = 0;
                    pClsProperty.history_union_id = 0;
                    pClsProperty.lot_srno = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }
        public int Update(MFGSawableReceiveProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            int IntRes = 0;

            try
            {
                Request Request = new Request();
                Request.AddParams("@is_outstanding", pClsProperty.is_outstanding, DbType.Boolean);
                Request.AddParams("@lot_id", pClsProperty.rough_lot_id, DbType.Int32);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Sawable_Receive_Update;
                Request.CommandType = CommandType.StoredProcedure;
                if (Conn != null)
                    IntRes = Conn.Inter1.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request, pEnum);
                else
                    IntRes = Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IntRes;
        }
        public DataTable GetBalanceCarat(Int64 LotId, int Cut_id)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Stock_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@lot_id", LotId, DbType.Int64);
            Request.AddParams("@cut_id", Cut_id, DbType.Int32);
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable ProcessIssue_GetData(Int64 LotId, string process, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Process_Issue_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@process", process, DbType.String);
            Request.AddParams("@lot_id", LotId, DbType.Int64);
            if (Conn != null)
                Conn.Inter1.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, DTab, Request, pEnum);
            else
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
    }
}
