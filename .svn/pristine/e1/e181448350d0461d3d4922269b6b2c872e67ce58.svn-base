using BLL.PropertyClasses.Transaction;
using BLL.PropertyClasses.Transaction.MFG;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction.MFG
{
    public class MFGJangedIsuRecAssortment
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
                Request.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int32);
                Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int32);
                Request.AddParams("@rough_cut_id", pClsProperty.rough_cut_id, DbType.Int32);
                Request.AddParams("@janged_date", pClsProperty.janged_date, DbType.Date);
                Request.AddParams("@company_id", pClsProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", pClsProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", pClsProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", pClsProperty.department_id, DbType.Int32);
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
                Request.AddParams("@party_id", pClsProperty.party_id, DbType.Int32);
                Request.AddParams("@prd_id", pClsProperty.prediction_id, DbType.Int32);
                Request.AddParams("@pcs", pClsProperty.pcs, DbType.Int32);
                Request.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
                Request.AddParams("@percentage", pClsProperty.percentage, DbType.Decimal);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);
                Request.AddParams("@is_outside", pClsProperty.is_outside, DbType.Int32);
                Request.AddParams("@type", "I", DbType.String);
                Request.AddParams("@janged_union_id", pClsProperty.janged_union_id, DbType.Int64);
                Request.AddParams("@history_union_id", pClsProperty.history_union_id, DbType.Int64);
                Request.AddParams("@janged_srno", pClsProperty.janged_Srno, DbType.Int64);
                Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);
                Request.AddParams("@temp_quality_name", pClsProperty.temp_quality_name, DbType.String);
                Request.AddParams("@color_id", pClsProperty.color_id, DbType.Int32);
                Request.AddParams("@minus2_amt", pClsProperty.minus2_amt, DbType.Int32);
                Request.AddParams("@plus2_amt", pClsProperty.plus2_amt, DbType.Int32);
                Request.AddParams("@temp_sieve_name", pClsProperty.temp_sieve_name, DbType.String);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Janged_Issue_Assorting_Save;
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
                        pClsProperty.janged_union_id = Val.ToInt64(p_dtbProcessUnionId.Rows[0][0]);
                        pClsProperty.janged_no = Val.ToInt64(p_dtbProcessUnionId.Rows[0][1]);
                        pClsProperty.history_union_id = Val.ToInt64(p_dtbProcessUnionId.Rows[0][2]);
                        pClsProperty.janged_Srno = Val.ToInt64(p_dtbProcessUnionId.Rows[0][3]);
                        pClsProperty.lot_srno = Val.ToInt64(p_dtbProcessUnionId.Rows[0][4]);
                    }
                }
                else
                {
                    pClsProperty.janged_union_id = 0;
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
        public MFGJangedIssue_Property RecSave(MFGJangedIssue_Property pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@janged_id", pClsProperty.janged_id, DbType.Int32);
                Request.AddParams("@janged_no", pClsProperty.janged_no, DbType.Int32);
                Request.AddParams("@prev_janged_id", pClsProperty.previous_janged_id, DbType.Int32);
                Request.AddParams("@prev_janged_no", pClsProperty.previous_janged_no, DbType.Int32);
                Request.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int32);
                Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int32);
                Request.AddParams("@rough_cut_id", pClsProperty.rough_cut_id, DbType.Int32);
                Request.AddParams("@janged_date", pClsProperty.janged_date, DbType.Date);
                Request.AddParams("@company_id", pClsProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", pClsProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", pClsProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", pClsProperty.department_id, DbType.Int32);
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
                Request.AddParams("@party_id", pClsProperty.party_id, DbType.Int32);
                Request.AddParams("@prd_id", pClsProperty.prediction_id, DbType.Int32);
                Request.AddParams("@pcs", pClsProperty.pcs, DbType.Int32);
                Request.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
                Request.AddParams("@percentage", pClsProperty.percentage, DbType.Decimal);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);
                Request.AddParams("@is_outside", pClsProperty.is_outside, DbType.Int32);
                Request.AddParams("@type", "R", DbType.String);
                Request.AddParams("@janged_union_id", pClsProperty.janged_union_id, DbType.Int64);
                Request.AddParams("@history_union_id", pClsProperty.history_union_id, DbType.Int64);
                Request.AddParams("@janged_srno", pClsProperty.janged_Srno, DbType.Int64);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Janged_Recieve_Assorting_Save;
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
                        pClsProperty.janged_union_id = Val.ToInt64(p_dtbProcessUnionId.Rows[0][0]);
                        pClsProperty.janged_no = Val.ToInt64(p_dtbProcessUnionId.Rows[0][1]);
                        pClsProperty.history_union_id = Val.ToInt64(p_dtbProcessUnionId.Rows[0][2]);
                        pClsProperty.janged_Srno = Val.ToInt64(p_dtbProcessUnionId.Rows[0][3]);
                    }
                }
                else
                {
                    pClsProperty.janged_union_id = 0;
                    pClsProperty.janged_no = 0;
                    pClsProperty.history_union_id = 0;
                    pClsProperty.janged_Srno = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }
        public DataTable GetPendingStock(MFGProcessIssueProperty PClsProperty)
        {
            DataTable DTabVal = new DataTable();
            Request Request = new Request();

            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", PClsProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@cut_id", PClsProperty.rough_cut_id, DbType.Int32);
            Request.AddParams("@kapan_id", PClsProperty.kapan_id, DbType.Int32);
            Request.AddParams("@flag", PClsProperty.flag, DbType.Int32);
            Request.AddParams("@process_id", PClsProperty.process_id, DbType.Int32);
            Request.AddParams("@sub_process_id", PClsProperty.sub_process_id, DbType.Int32);
            Request.AddParams("@temp_purity_name", PClsProperty.temp_purity_name, DbType.String);
            Request.AddParams("@temp_sieve_name", PClsProperty.temp_sieve_name, DbType.String);
            Request.AddParams("@from_date", PClsProperty.from_date, DbType.Date);
            Request.AddParams("@to_date", PClsProperty.to_date, DbType.Date);

            Request.CommandText = BLL.TPV.SProc.MFG_IssRet_GetAssortmentStock;
            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, DTabVal, Request);

            return DTabVal;
        }
        public DataTable GetJangedDetail(int JangedNo)
        {
            DataTable DTabVal = new DataTable();
            Request Request = new Request();
            Request.AddParams("@janged_no", JangedNo, DbType.Int32);

            Request.CommandText = BLL.TPV.SProc.MFG_IssRet_GetJangedDetail;
            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, DTabVal, Request);

            return DTabVal;
        }
        public DataTable GetJangedReturnData(MFGProcessIssueProperty PClsProperty)
        {
            DataTable DTabVal = new DataTable();
            Request Request = new Request();

            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@cut_id", PClsProperty.rough_cut_id, DbType.Int32);
            Request.AddParams("@kapan_id", PClsProperty.kapan_id, DbType.Int32);

            Request.CommandText = BLL.TPV.SProc.MFG_IssRet_GetReturn_JangedData;
            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, DTabVal, Request);

            return DTabVal;
        }
        public DataSet Print_Janged(int numkapan_id, int numcut_id, int numprocess_id, int numsubProcess_id, string temp_purity_name, Int64 Lot_SRNo)
        {
            DataSet DTab = new DataSet();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.RPT_MFG_TRN_Assortment_Janged;

            Request.AddParams("@kapan_id", numkapan_id, DbType.Int32);
            Request.AddParams("@cut_id", numcut_id, DbType.Int32);
            Request.AddParams("@process_id", numprocess_id, DbType.Int32);
            Request.AddParams("@sub_process_id", numsubProcess_id, DbType.Int32);
            Request.AddParams("@temp_purity_name", temp_purity_name, DbType.String);
            Request.AddParams("@lot_srno", Lot_SRNo, DbType.Int64);

            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataSet(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, "Semi", Request);
            return DTab;
        }

        public int GetDeleteAssort_ID(MFGJangedIssue_Property pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            DataTable DTab = new DataTable();
            int IntRes = 0;
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_AssortJanged_Lot_Delete;

            Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int32);
            Request.AddParams("@cut_id", pClsProperty.rough_cut_id, DbType.Int32);
            Request.AddParams("@lot_id", 0, DbType.Int32);
            Request.AddParams("@temp_quality_name", pClsProperty.temp_quality_name, DbType.String);
            Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int32);
            Request.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int32);
            Request.AddParams("@del_lot_srno", pClsProperty.Del_lot_srno, DbType.Int64);

            Request.CommandType = CommandType.StoredProcedure;
            IntRes = Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
            return IntRes;
        }
        public int GetDeleteFinalLot_ID(MFGJangedIssue_Property pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            DataTable DTab = new DataTable();
            int IntRes = 0;
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Assort_Lot_Final_Delete;

            Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int32);
            Request.AddParams("@cut_id", pClsProperty.rough_cut_id, DbType.Int32);
            Request.AddParams("@lot_id", 0, DbType.Int32);
            Request.AddParams("@temp_quality_name", pClsProperty.temp_quality_name, DbType.String);
            Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int32);
            Request.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int32);
            Request.AddParams("@temp_sieve_name", pClsProperty.temp_sieve_name, DbType.String);
            Request.AddParams("@flag", pClsProperty.flag, DbType.Int32);
            Request.AddParams("@del_lot_srno", pClsProperty.Del_lot_srno, DbType.Int64);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
            Request.AddParams("@assort_total_carat", pClsProperty.assort_total_carat, DbType.Decimal);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);

            Request.CommandType = CommandType.StoredProcedure;

            IntRes = Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
            return IntRes;
        }
    }
}
