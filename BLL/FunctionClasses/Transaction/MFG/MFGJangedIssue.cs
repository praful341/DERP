using BLL.PropertyClasses.Transaction.MFG;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction.MFG
{
    public class MFGJangedIssue
    {
        InterfaceLayer Ope = new InterfaceLayer();
        BLL.Validation Val = new BLL.Validation();

        public MFGJangedIssue_Property Save(MFGJangedIssue_Property pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@janged_id", pClsProperty.janged_id, DbType.Int32);
                Request.AddParams("@janged_no", pClsProperty.janged_no, DbType.Int32);
                Request.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int64);
                Request.AddParams("@union_id", pClsProperty.union_id, DbType.Int64);
                Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int32);
                Request.AddParams("@rough_cut_id", pClsProperty.rough_cut_id, DbType.Int32);
                Request.AddParams("@janged_date", pClsProperty.janged_date, DbType.Date);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
                Request.AddParams("@to_company_id", pClsProperty.to_company_id, DbType.Int32);
                Request.AddParams("@to_branch_id", pClsProperty.to_branch_id, DbType.Int32);
                Request.AddParams("@to_location_id", pClsProperty.to_location_id, DbType.Int32);
                Request.AddParams("@to_department_id", pClsProperty.to_department_id, DbType.Int32);
                Request.AddParams("@manager_id", pClsProperty.manager_id, DbType.Int32);
                Request.AddParams("@to_manager_id", pClsProperty.to_manager_id, DbType.Int32);
                Request.AddParams("@employee_id", pClsProperty.employee_id, DbType.Int32);
                Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int32);
                Request.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int32);
                Request.AddParams("@rough_sieve_id", pClsProperty.rough_sieve_id, DbType.Int32);
                Request.AddParams("@purity_id", pClsProperty.purity_id, DbType.Int32);
                Request.AddParams("@quality_id", pClsProperty.quality_id, DbType.Int32);
                Request.AddParams("@rough_clarity_id", pClsProperty.rough_clarity_id, DbType.Int32);
                Request.AddParams("@party_id", pClsProperty.party_id, DbType.Int32);
                Request.AddParams("@pcs", pClsProperty.pcs, DbType.Int32);
                Request.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);
                Request.AddParams("@is_outside", pClsProperty.is_outside, DbType.Int32);
                Request.AddParams("@type", "I", DbType.String);
                Request.AddParams("@issue_union_id", pClsProperty.issue_union_id, DbType.Int64);
                Request.AddParams("@janged_union_id", pClsProperty.janged_union_id, DbType.Int64);
                Request.AddParams("@dept_union_id", pClsProperty.dept_union_id, DbType.Int64);
                Request.AddParams("@history_union_id", pClsProperty.history_union_id, DbType.Int64);
                Request.AddParams("@janged_srno", pClsProperty.janged_Srno, DbType.Int64);
                Request.AddParams("@is_process_setting_flag", pClsProperty.is_process_setting_flag, DbType.Int64);
                Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);
                Request.AddParams("@action", pClsProperty.action, DbType.Int32);
                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Janged_Issue_Save;
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
                        pClsProperty.janged_union_id = Val.ToInt64(p_dtbProcessUnionId.Rows[0][2]);
                        pClsProperty.dept_union_id = Val.ToInt64(p_dtbProcessUnionId.Rows[0][3]);
                        pClsProperty.janged_no = Val.ToInt64(p_dtbProcessUnionId.Rows[0][4]);
                        pClsProperty.history_union_id = Val.ToInt64(p_dtbProcessUnionId.Rows[0][5]);
                        pClsProperty.janged_Srno = Val.ToInt64(p_dtbProcessUnionId.Rows[0][6]);
                        pClsProperty.lot_srno = Val.ToInt64(p_dtbProcessUnionId.Rows[0][7]);
                    }
                }
                else
                {
                    pClsProperty.union_id = 0;
                    pClsProperty.issue_union_id = 0;
                    pClsProperty.janged_union_id = 0;
                    pClsProperty.dept_union_id = 0;
                    pClsProperty.janged_no = 0;
                    pClsProperty.history_union_id = 0;
                    pClsProperty.janged_Srno = 0;
                    pClsProperty.lot_srno = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }
        public DataTable GetData(string p_dtpFromDate, string p_dtpToDate)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Janged_Issue_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@From_Date", p_dtpFromDate, DbType.Int32);
            Request.AddParams("@To_Date", p_dtpToDate, DbType.Int32);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }

        public DataTable GetData_Process_Setting(MFGJangedIssue_Property pClsProperty)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_Janged_Process_Setting_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int32);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }

        public DataTable GetDataDetails(Int64 p_numJangedNo)
        {
            DataTable DTab = new DataTable();
            try
            {
                Request Request = new Request();
                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Janged_Issue_GetDetailsData;
                Request.CommandType = CommandType.StoredProcedure;
                Request.AddParams("@p_numJangedNo", p_numJangedNo, DbType.Int64);

                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
                return DTab;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return DTab;
            }
        }
        public DataTable Polish_GetDataDetails()
        {
            DataTable DTab = new DataTable();
            try
            {
                Request Request = new Request();
                Request.CommandText = "MFG_TRN_Stock_Daily_Mail";
                Request.CommandType = CommandType.StoredProcedure;
                Request.AddParams("@datFromDate", null, DbType.Date);
                Request.AddParams("@datToDate", null, DbType.Date);

                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
                return DTab;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return DTab;
            }
        }
        public DataTable GetProcessJanged_DataDetails(Int64 p_numJangedNo)
        {
            DataTable DTab = new DataTable();
            try
            {
                Request Request = new Request();
                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Process_Issue_GetDetailsData;
                Request.CommandType = CommandType.StoredProcedure;
                Request.AddParams("@p_numJangedNo", p_numJangedNo, DbType.Int64);

                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
                return DTab;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return DTab;
            }
        }
        public DataTable GetProcessBoil_DataDetails(Int64 p_numJangedNo)
        {
            DataTable DTab = new DataTable();
            try
            {
                Request Request = new Request();
                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Boil_Issue_PrintData;
                Request.CommandType = CommandType.StoredProcedure;
                Request.AddParams("@p_numJangedNo", p_numJangedNo, DbType.Int64);

                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
                return DTab;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return DTab;
            }
        }
        public DataTable GetDepartmentDataDetails(Int64 p_numLotSrNo)
        {
            DataTable DTab = new DataTable();
            try
            {
                Request Request = new Request();
                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Department_Issue_GetDetailsData;
                Request.CommandType = CommandType.StoredProcedure;
                Request.AddParams("@p_numLotSrNo", p_numLotSrNo, DbType.Int64);

                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
                return DTab;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return DTab;
            }
        }
        public DataTable GetDepartmentDetails(Int64 p_numLotSrNo)
        {
            DataTable DTab = new DataTable();
            try
            {
                Request Request = new Request();
                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Department_GetDetailsData;
                Request.CommandType = CommandType.StoredProcedure;
                Request.AddParams("@p_numLotSrNo", p_numLotSrNo, DbType.Int64);

                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
                return DTab;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return DTab;
            }
        }
        public DataTable GetIssueDetails(Int64 p_numLotSrNo)
        {
            DataTable DTab = new DataTable();
            try
            {
                Request Request = new Request();
                Request.CommandText = BLL.TPV.SProc.MFG_TRN_ProcessIssue_GetDetailsData;
                Request.CommandType = CommandType.StoredProcedure;
                Request.AddParams("@p_numLotSrNo", p_numLotSrNo, DbType.Int64);

                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
                return DTab;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return DTab;
            }
        }
        public DataTable Polish_GetDataDetails(Int64 p_numJangedNo)
        {
            DataTable DTab = new DataTable();
            try
            {
                Request Request = new Request();
                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Janged_Issue_GetPolish;
                Request.CommandType = CommandType.StoredProcedure;
                Request.AddParams("@p_numJangedNo", p_numJangedNo, DbType.Int64);

                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
                return DTab;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return DTab;
            }
        }
        public DataTable GetDataJanged(Int64 p_numJangedNo)
        {
            DataTable DTab = new DataTable();
            try
            {
                Request Request = new Request();
                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Janged_GetDetailsData;
                Request.CommandType = CommandType.StoredProcedure;
                Request.AddParams("@p_numJangedNo", p_numJangedNo, DbType.Int64);

                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
                return DTab;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return DTab;
            }
        }
        public DataTable GetDataDetails_JangedReturn(Int64 p_numJangedNo)
        {
            DataTable DTab = new DataTable();
            try
            {
                Request Request = new Request();
                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Janged_Return_GetDetailsData;
                Request.CommandType = CommandType.StoredProcedure;

                Request.AddParams("@role_name", GlobalDec.gEmployeeProperty.role_name, DbType.String);
                Request.AddParams("@p_numJangedNo", p_numJangedNo, DbType.Int64);

                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
                return DTab;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return DTab;
            }
        }
        public DataTable GetData_JangedReturn_Galaxy(Int64 p_numJangedNo)
        {
            DataTable DTab = new DataTable();
            try
            {
                Request Request = new Request();
                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Janged_Return_Galaxy;
                Request.CommandType = CommandType.StoredProcedure;

                Request.AddParams("@p_numJangedNo", p_numJangedNo, DbType.Int64);

                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
                return DTab;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return DTab;
            }
        }
        public DataTable Assort_Janged_GetDataDetails(Int64 p_numJangedNo)
        {
            DataTable DTab = new DataTable();
            try
            {
                Request Request = new Request();
                Request.CommandText = BLL.TPV.SProc.MFG_TRN_AssortJanged_Issue_GetData;
                Request.CommandType = CommandType.StoredProcedure;
                Request.AddParams("@p_numJangedNo", p_numJangedNo, DbType.Int64);

                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
                return DTab;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return DTab;
            }
        }

        public int SaveIssueJangedGoodsReceive(MFGJangedIssue_Property pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_Trn_Janged_AutoConfirm_Receive;
            Request.CommandType = CommandType.StoredProcedure;

            Request.AddParams("@janged_date", pClsProperty.janged_date, DbType.Date);
            Request.AddParams("@janged_srno", pClsProperty.janged_Srno, DbType.Int64);
            Request.AddParams("@department_id", pClsProperty.to_department_id, DbType.Int32);
            Request.AddParams("@receive_date", pClsProperty.receive_date, DbType.Date);
            Request.AddParams("@receive_time", GlobalDec.gStr_SystemTime, DbType.String);
            Request.AddParams("@receive_employee_id", GlobalDec.gEmployeeProperty.employee_id, DbType.String);
            Request.AddParams("@receive_ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@company_id", pClsProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", pClsProperty.branch_id, DbType.Int32);

            int IntRes = 0;
            if (Conn != null)
                IntRes = Conn.Inter1.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request, pEnum);
            else
                IntRes = Ope.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request);
            return IntRes;
        }
        public DataTable GetLottingDepartment(Int64 lotId)
        {
            DataTable DTab = new DataTable();
            try
            {
                Request Request = new Request();
                Request.CommandText = BLL.TPV.SProc.MFG_Trn_Lotting_Department_GetData;
                Request.CommandType = CommandType.StoredProcedure;
                Request.AddParams("@lot_id", lotId, DbType.Int32);

                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
                return DTab;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return DTab;
            }
        }

        public DataTable GetDataFactoryLock(Int64 p_numPartyID)
        {
            DataTable DTab = new DataTable();
            try
            {
                Request Request = new Request();
                Request.CommandText = BLL.TPV.SProc.MFG_FAC_LOCK_GetData;
                Request.CommandType = CommandType.StoredProcedure;

                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
                Request.AddParams("@party_id", p_numPartyID, DbType.Int64);

                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
                return DTab;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return DTab;
            }
        }
        public int JangedIssueDelete(MFGJangedIssue_Property pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            int IntRes = 0;
            try
            {

                Request RequestDetails = new Request();

                RequestDetails.AddParams("@janged_no", pClsProperty.janged_no, DbType.Int64);

                RequestDetails.CommandText = BLL.TPV.SProc.MFG_TRN_JangedIssue_Delete;
                RequestDetails.CommandType = CommandType.StoredProcedure;

                if (Conn != null)
                    IntRes += Conn.Inter1.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, RequestDetails, pEnum);
                else
                    IntRes += Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, RequestDetails);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IntRes;
        }
        public int CheckJanged(int JangedNo)
        {
            int Count = 0;
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Janged_lotCheck;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@janged_no", JangedNo, DbType.Int32);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            if (DTab.Rows.Count > 0)
            {
                Count = Val.ToInt(DTab.Rows[0][0]);
            }
            else
            {
                Count = 0;
            }
            return Count;
        }

        public DataTable GetJangedIssue_Validate(MFGJangedIssue_Property pClsProperty)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.TRN_Janged_Issue_Validate_Data;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@from_department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int64);
            Request.AddParams("@to_department_id", pClsProperty.to_department_id, DbType.Int64);
            Request.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int64);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
    }
}
