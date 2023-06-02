using BLL.PropertyClasses.Transaction;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction
{
    public class MFGProcessIssue
    {
        InterfaceLayer Ope = new InterfaceLayer();
        BLL.Validation Val = new BLL.Validation();

        public MFGProcessIssueProperty Save(MFGProcessIssueProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@issue_id", pClsProperty.Issue_id, DbType.Int32);
                Request.AddParams("@rough_cut_id", pClsProperty.rough_cut_id, DbType.Int32);
                Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int32);
                Request.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int32);
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
                Request.AddParams("@purity_id", pClsProperty.purity_id, DbType.Int32);
                Request.AddParams("@quality_id", pClsProperty.quality_id, DbType.Int32);
                Request.AddParams("@rough_clarity_id", pClsProperty.rough_clarity_id, DbType.Int32);
                Request.AddParams("@prd_id", pClsProperty.prd_id, DbType.Int32);
                Request.AddParams("@pcs", pClsProperty.pcs, DbType.Int32);
                Request.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
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
                Request.AddParams("@rejection_pcs", pClsProperty.rejection_pcs, DbType.Int32);
                Request.AddParams("@rejection_carat", pClsProperty.rejection_carat, DbType.Decimal);
                Request.AddParams("@rr_pcs", pClsProperty.rr_pcs, DbType.Int32);
                Request.AddParams("@rr_carat", pClsProperty.rr_carat, DbType.Decimal);
                Request.AddParams("@resoing_pcs", pClsProperty.resoing_pcs, DbType.Int32);
                Request.AddParams("@resoing_carat", pClsProperty.resoing_carat, DbType.Decimal);
                Request.AddParams("@breakage_pcs", pClsProperty.breakage_pcs, DbType.Int32);
                Request.AddParams("@breakage_carat", pClsProperty.breakage_carat, DbType.Decimal);
                Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Process_Issue_Save;
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
        public MFGProcessIssueProperty Issue_Factory_Save(MFGProcessIssueProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@issue_id", pClsProperty.Issue_id, DbType.Int32);
                Request.AddParams("@rough_cut_id", pClsProperty.rough_cut_id, DbType.Int32);
                Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int32);
                Request.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int32);
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
                Request.AddParams("@purity_id", pClsProperty.purity_id, DbType.Int32);
                Request.AddParams("@quality_id", pClsProperty.quality_id, DbType.Int32);
                Request.AddParams("@rough_clarity_id", pClsProperty.rough_clarity_id, DbType.Int32);
                Request.AddParams("@prd_id", pClsProperty.prd_id, DbType.Int32);
                Request.AddParams("@pcs", pClsProperty.pcs, DbType.Int32);
                Request.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);
                Request.AddParams("@issue_union_id", pClsProperty.issue_union_id, DbType.Int64);
                Request.AddParams("@history_union_id", pClsProperty.history_union_id, DbType.Int64);
                Request.AddParams("@rejection_pcs", pClsProperty.rejection_pcs, DbType.Int32);
                Request.AddParams("@rejection_carat", pClsProperty.rejection_carat, DbType.Decimal);
                Request.AddParams("@rr_pcs", pClsProperty.rr_pcs, DbType.Int32);
                Request.AddParams("@rr_carat", pClsProperty.rr_carat, DbType.Decimal);
                Request.AddParams("@resoing_pcs", pClsProperty.resoing_pcs, DbType.Int32);
                Request.AddParams("@resoing_carat", pClsProperty.resoing_carat, DbType.Decimal);
                Request.AddParams("@breakage_pcs", pClsProperty.breakage_pcs, DbType.Int32);
                Request.AddParams("@breakage_carat", pClsProperty.breakage_carat, DbType.Decimal);
                Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Process_Factory_Issue_Save;
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
                        pClsProperty.issue_union_id = Val.ToInt64(p_dtbProcessUnionId.Rows[0][0]);
                        pClsProperty.history_union_id = Val.ToInt64(p_dtbProcessUnionId.Rows[0][1]);
                        pClsProperty.lot_srno = Val.ToInt64(p_dtbProcessUnionId.Rows[0][2]);
                    }
                }
                else
                {
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
        public int Delete(MFGEmployeeTargetProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            Request Request = new Request();
            int IntRes = 0;
            try
            {
                Request RequestDetails = new Request();

                RequestDetails.AddParams("@performance_date", Val.DBDate(pClsProperty.performance_date), DbType.Date);
                RequestDetails.AddParams("@department_id", pClsProperty.department_id, DbType.Int32);
                RequestDetails.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int32);
                RequestDetails.AddParams("@manager_id", pClsProperty.manager_id, DbType.Int32);
                RequestDetails.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                RequestDetails.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                RequestDetails.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);

                RequestDetails.CommandText = BLL.TPV.SProc.MFG_TRN_Employee_Performance_Delete;
                RequestDetails.CommandType = CommandType.StoredProcedure;

                if (Conn != null)
                    IntRes = Conn.Inter1.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, RequestDetails, pEnum);
                else
                    IntRes = Ope.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, RequestDetails);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IntRes;

        }
        public DataTable GetData(string p_dtpFromDate, string p_dtpToDate, int p_department_id, int p_process_id, int p_manager_id)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_EMPTarget_GetData;

            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@From_Date", Val.DBDate(p_dtpFromDate), DbType.Date);
            Request.AddParams("@To_Date", p_dtpToDate, DbType.Date);
            Request.AddParams("@department_id", p_department_id, DbType.Int32);
            Request.AddParams("@process_id", p_process_id, DbType.Int32);
            Request.AddParams("@manager_id", p_manager_id, DbType.Int32);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetOSRate(int cut_id, Int64 lot_id, string strprocess)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_OSRate_GetData;

            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@cut_id", cut_id, DbType.Int32);
            Request.AddParams("@lot_id", lot_id, DbType.Int64);
            Request.AddParams("@process", strprocess, DbType.String);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetAssortRate(Int64 lot_id, int count)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_AssortFinalRate_GetData;

            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@lot_id", lot_id, DbType.Int64);
            Request.AddParams("@count", count, DbType.Int32);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetWagesRate(Int64 lot_id, string strDepartmentName, int num_process_id, int num_subprocess_id)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_WagesRate_GetData;

            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@lot_id", lot_id, DbType.Int64);
            Request.AddParams("@department_name", strDepartmentName, DbType.String);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@process_id", num_process_id, DbType.Int32);
            Request.AddParams("@sub_process_id", num_subprocess_id, DbType.Int32);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetPendingStock(MFGProcessIssueProperty PClsProperty)
        {
            DataTable DTabVal = new DataTable();
            Request Request = new Request();

            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@cut_id", PClsProperty.rough_cut_id, DbType.Int32);
            Request.AddParams("@kapan_id", PClsProperty.kapan_id, DbType.Int32);

            Request.CommandText = BLL.TPV.SProc.MFG_IssRet_GetPendingStock;
            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, DTabVal, Request);

            return DTabVal;
        }

        public DataTable GetBarcodePrint(MFGProcessIssueProperty PClsProperty, int AllLot = 0)
        {
            DataTable DTabVal = new DataTable();
            Request Request = new Request();

            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@cut_id", PClsProperty.rough_cut_id, DbType.Int32);
            Request.AddParams("@kapan_id", PClsProperty.kapan_id, DbType.Int32);
            Request.AddParams("@all_lot", AllLot, DbType.Int32);

            Request.CommandText = BLL.TPV.SProc.MFG_IssRet_GetBarcode_Print;
            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, DTabVal, Request);

            return DTabVal;
        }
        public DataTable GetCharniPendingStock(MFGProcessIssueProperty PClsProperty, int Flag)
        {
            DataTable DTabVal = new DataTable();
            Request Request = new Request();

            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@cut_id", PClsProperty.rough_cut_id, DbType.Int32);
            Request.AddParams("@kapan_id", PClsProperty.kapan_id, DbType.Int32);
            Request.AddParams("@flag", Flag, DbType.Int32);

            Request.CommandText = BLL.TPV.SProc.MFG_IssRet_GetCharniPendingStock;
            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, DTabVal, Request);

            return DTabVal;
        }

        public DataTable GetPendingIssueStock(MFGProcessIssueProperty PClsProperty)
        {
            DataTable DTabVal = new DataTable();
            Request Request = new Request();

            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@cut_id", PClsProperty.rough_cut_id, DbType.Int32);
            Request.AddParams("@kapan_id", PClsProperty.kapan_id, DbType.Int32);

            Request.CommandText = BLL.TPV.SProc.MFG_IssRet_GetIssuePendingStock;
            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, DTabVal, Request);

            return DTabVal;
        }
        public DataTable GetPendingLSIssueStock(MFGProcessIssueProperty PClsProperty)
        {
            DataTable DTabVal = new DataTable();
            Request Request = new Request();

            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@cut_id", PClsProperty.rough_cut_id, DbType.Int32);
            Request.AddParams("@kapan_id", PClsProperty.kapan_id, DbType.Int32);

            Request.CommandText = BLL.TPV.SProc.MFG_IssRet_GetLSIssuePendingStock;
            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, DTabVal, Request);

            return DTabVal;
        }
        public DataTable GetKapanMixLiveStock(MFGProcessIssueProperty PClsProperty)
        {
            DataTable DTabVal = new DataTable();
            Request Request = new Request();

            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@rough_cut_id", PClsProperty.rough_cut_id, DbType.Int32);
            Request.AddParams("@kapan_id", PClsProperty.kapan_id, DbType.Int32);

            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Kapan_Mix_LiveStock_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, DTabVal, Request);

            return DTabVal;
        }
        public DataTable GetKapanMixLiveStockData(MFGProcessIssueProperty PClsProperty)
        {
            DataTable DTabVal = new DataTable();
            Request Request = new Request();

            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@rough_cut_id", PClsProperty.rough_cut_id, DbType.Int32);
            Request.AddParams("@kapan_id", PClsProperty.kapan_id, DbType.Int32);
            Request.AddParams("@lot_id", PClsProperty.lot_id, DbType.Int64);

            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Kapan_Mix_LiveStock_LotWiseData;
            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, DTabVal, Request);

            return DTabVal;
        }

        public DataTable GetPendingDeptStock(Int64 lotid, MFGProcessIssueProperty PClsProperty)
        {
            DataTable DTabVal = new DataTable();
            Request Request = new Request();

            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@cut_id", PClsProperty.rough_cut_id, DbType.Int32);
            Request.AddParams("@kapan_id", PClsProperty.kapan_id, DbType.Int32);
            Request.AddParams("@lot_id", lotid, DbType.Int64);
            Request.AddParams("@falg", PClsProperty.flag, DbType.Int32);

            Request.CommandText = BLL.TPV.SProc.MFG_IssRet_GetDeptPendingStock;
            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, DTabVal, Request);

            return DTabVal;
        }
        public DataTable RPTBarcodeGetdata(string pStrBarcodeList, string pBARCODENAME)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@lot_id", pStrBarcodeList, DbType.String);

            Request.CommandText = BLL.TPV.SProc.TRN_LotID_Print_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, DTab, Request);

            return DTab;
        }

        public DataTable GetEmployeeMapping(int numProcess_id, int numSubProcess_id)
        {
            DataTable DTabVal = new DataTable();
            Request Request = new Request();

            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@process_id", numProcess_id, DbType.Int32);
            Request.AddParams("@subprocess_id", numSubProcess_id, DbType.Int32);

            Request.CommandText = BLL.TPV.SProc.MFG_IssRet_Get_ProcesswiseEmp;
            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, DTabVal, Request);

            return DTabVal;
        }
        public decimal GetLatestRate(Int64 LotId)
        {
            decimal rate = 0;
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@lot_id", LotId, DbType.Int64);

            Request.CommandText = BLL.TPV.SProc.MFG_TRN_LastRate_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, DTab, Request);
            if (DTab.Rows.Count > 0)
            {
                rate = Val.ToDecimal(DTab.Rows[0]["rate"]);
            }
            else
            {
                rate = 0;
            }

            return rate;
        }

        public MFGProcessIssueProperty GetData_PrevProcess(MFGProcessIssueProperty pClsProperty)
        {
            try
            {
                Request Request = new Request();
                Request.CommandText = BLL.TPV.SProc.MFG_TRN_PrevProcess_Seq_GetData;

                Request.CommandType = CommandType.StoredProcedure;

                Request.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int64);
                Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int32);
                Request.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int32);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

                DataTable p_dtbProcessSequence = new DataTable();

                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, p_dtbProcessSequence, Request);

                if (p_dtbProcessSequence != null)
                {
                    if (p_dtbProcessSequence.Rows.Count > 0)
                    {
                        pClsProperty.Messgae = Val.ToString(p_dtbProcessSequence.Rows[0][0]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }
        public MFGProcessIssueProperty GetData_PrevProcessRec(MFGProcessIssueProperty pClsProperty)
        {
            try
            {
                Request Request = new Request();
                Request.CommandText = BLL.TPV.SProc.MFG_TRN_PrevProcessRec_Seq_GetData;

                Request.CommandType = CommandType.StoredProcedure;

                Request.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int64);
                Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int32);
                Request.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int32);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

                DataTable p_dtbProcessSequence = new DataTable();

                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, p_dtbProcessSequence, Request);

                if (p_dtbProcessSequence != null)
                {
                    if (p_dtbProcessSequence.Rows.Count > 0)
                    {
                        pClsProperty.Messgae = Val.ToString(p_dtbProcessSequence.Rows[0][0]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }
        public int GetOsCheck(Int64 LotId)
        {
            int IsOs = 0;
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@lot_id", LotId, DbType.Int64);

            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Galaxy_Outstanding;
            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, DTab, Request);
            if (DTab.Rows.Count > 0)
            {
                IsOs = Val.ToInt32(DTab.Rows[0]["IsOs"]);
            }
            else
            {
                IsOs = 0;
            }

            return IsOs;
        }
    }
}
