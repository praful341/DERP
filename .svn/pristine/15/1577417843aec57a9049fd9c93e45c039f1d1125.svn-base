using BLL.PropertyClasses.Transaction.MFG;
using DLL;
using System;
using System.Data;


namespace BLL.FunctionClasses.Transaction.MFG
{
    public class MFGBoilRecieve
    {
        InterfaceLayer Ope = new InterfaceLayer();
        BLL.Validation Val = new BLL.Validation();
        public MFGBoilIssueProperty Save(MFGBoilIssueProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@issue_id", pClsProperty.Issue_id, DbType.Int32);
                Request.AddParams("@prev_id", pClsProperty.prev_id, DbType.Int32);
                Request.AddParams("@rough_purchase_id", pClsProperty.rough_purchase_id, DbType.Int32);
                Request.AddParams("@purchase_detail_id", pClsProperty.purchase_detail_id, DbType.Int32);
                Request.AddParams("@issue_date", pClsProperty.issue_date, DbType.Date);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
                Request.AddParams("@manager_id", pClsProperty.manager_id, DbType.Int32);
                Request.AddParams("@employee_id", pClsProperty.employee_id, DbType.Int32);
                Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int32);
                Request.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int32);
                Request.AddParams("@rough_sieve_id", pClsProperty.rough_sieve_id, DbType.Int32);
                Request.AddParams("@pcs", pClsProperty.pcs, DbType.Int32);
                Request.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
                Request.AddParams("@loss_carat", pClsProperty.loss_carat, DbType.Decimal);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);
                Request.AddParams("@union_id", pClsProperty.union_id, DbType.Int64);
                Request.AddParams("@issue_union_id", pClsProperty.issue_union_id, DbType.Int64);
                Request.AddParams("@history_union_id", pClsProperty.history_union_id, DbType.Int64);
                Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);
                Request.AddParams("@return_per", pClsProperty.return_per, DbType.Decimal);
                Request.AddParams("@loss_per", pClsProperty.loss_per, DbType.Decimal);
                Request.AddParams("@rough_type", pClsProperty.rough_type, DbType.String);
                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Boil_Receieve_Save;
                Request.CommandType = CommandType.StoredProcedure;

                DataTable p_dtbProcessUnionId = new DataTable();
                if (Conn != null)
                    Conn.Inter1.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, p_dtbProcessUnionId, Request, pEnum);
                else
                    Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, p_dtbProcessUnionId, Request);

                if (p_dtbProcessUnionId != null)
                {
                    if (p_dtbProcessUnionId.Rows.Count > 0)
                    {
                        pClsProperty.union_id = Val.ToInt64(p_dtbProcessUnionId.Rows[0][0]);
                        pClsProperty.issue_union_id = Val.ToInt64(p_dtbProcessUnionId.Rows[0][1]);
                        pClsProperty.history_union_id = Val.ToInt64(p_dtbProcessUnionId.Rows[0][2]);
                        pClsProperty.lot_srno = Val.ToInt64(p_dtbProcessUnionId.Rows[0][3]);
                    }
                }
                else
                {
                    pClsProperty.union_id = 0;
                    pClsProperty.issue_union_id = 0;
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
        public DataTable GetBoilRecievePending(int process_id, int sub_process_id, int purchase_id = 0)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_BoilRecievePending_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@process_id", process_id, DbType.Int32);
            Request.AddParams("@sub_process_id", sub_process_id, DbType.Int32);
            Request.AddParams("@purchase_id", purchase_id, DbType.Int32);
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);

            return DTab;
        }
        public DataTable GetIssRecDetails(Int64 p_numLotSrNo)
        {
            DataTable DTab = new DataTable();
            try
            {
                Request Request = new Request();
                Request.CommandText = BLL.TPV.SProc.MFG_TRN_BoilIssRec_GetDetailsData;
                Request.CommandType = CommandType.StoredProcedure;
                Request.AddParams("@p_numLotSrNo", p_numLotSrNo, DbType.Int64);
                Request.AddParams("@Type", "R", DbType.String);
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
                return DTab;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return DTab;
            }
        }
        public MFGBoilIssueProperty Boil_Delete(MFGBoilIssueProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request stkRequest = new Request();

                //stkRequest.AddParams("@issue_id", pClsProperty.Issue_id, DbType.Int64);
                stkRequest.AddParams("@union_id", pClsProperty.union_id, DbType.Decimal);
                stkRequest.AddParams("@process_id", pClsProperty.process_id, DbType.Int64);
                stkRequest.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int64);
                stkRequest.CommandText = BLL.TPV.SProc.MFG_TRN_BoilRecieve_Delete;
                stkRequest.CommandType = CommandType.StoredProcedure;

                DataTable p_dtbRghLotId = new DataTable();
                if (Conn != null)
                    Conn.Inter1.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, p_dtbRghLotId, stkRequest, pEnum);
                else
                    Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, p_dtbRghLotId, stkRequest);

                if (p_dtbRghLotId != null)
                {
                    if (p_dtbRghLotId.Rows.Count > 0)
                    {
                        pClsProperty.remarks = Val.ToString(p_dtbRghLotId.Rows[0][0]);
                    }
                }
                else
                {
                    pClsProperty.remarks = "";
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
