using BLL.PropertyClasses.Transaction;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction
{
    public class MFGProcessReceive
    {
        InterfaceLayer Ope = new InterfaceLayer();
        BLL.Validation Val = new BLL.Validation();

        public MFGProcessReceiveProperty Save(MFGProcessReceiveProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            //int IntRes = 0;

            try
            {
                Request Request = new Request();

                Request.AddParams("@receive_date", pClsProperty.receive_date, DbType.Date);
                Request.AddParams("@issue_id", pClsProperty.Issue_id, DbType.Int32);
                Request.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int32);
                Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int32);
                Request.AddParams("@rough_cut_id", pClsProperty.rough_cut_id, DbType.Int32);
                Request.AddParams("@union_id", pClsProperty.union_id, DbType.Int64);

                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

                Request.AddParams("@manager_id", pClsProperty.manager_id, DbType.Int32);
                Request.AddParams("@employee_id", pClsProperty.employee_id, DbType.Int32);
                Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int32);
                Request.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int32);
                Request.AddParams("@rough_clarity_id", pClsProperty.rough_clarity_id, DbType.Int32);
                Request.AddParams("@rough_sieve_id", pClsProperty.rough_sieve_id, DbType.Int32);

                Request.AddParams("@pcs", pClsProperty.pcs, DbType.Int32);
                Request.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
                Request.AddParams("@is_net_rate", pClsProperty.is_net_rate, DbType.Int32);
                Request.AddParams("@net_rate", pClsProperty.net_rate, DbType.Decimal);
                Request.AddParams("@net_amount", pClsProperty.net_amount, DbType.Decimal);

                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);
                Request.AddParams("@receive_union_id", pClsProperty.receive_union_id, DbType.Int64);

                Request.AddParams("@loss_carat", pClsProperty.loss_carat, DbType.Decimal);
                Request.AddParams("@carat_plus", pClsProperty.carat_plus, DbType.Decimal);
                Request.AddParams("@loss_count", pClsProperty.loss_count, DbType.Int32);
                Request.AddParams("@history_union_id", pClsProperty.history_union_id, DbType.Int64);
                Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Process_Receive_Save;
                Request.CommandType = CommandType.StoredProcedure;

                DataTable p_dtbProcessRecId = new DataTable();
                if (Conn != null)
                    Conn.Inter1.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, p_dtbProcessRecId, Request, pEnum);
                else
                    Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, p_dtbProcessRecId, Request);

                if (p_dtbProcessRecId != null)
                {
                    if (p_dtbProcessRecId.Rows.Count > 0)
                    {
                        pClsProperty.union_id = Val.ToInt64(p_dtbProcessRecId.Rows[0][0]);
                        pClsProperty.receive_union_id = Val.ToInt64(p_dtbProcessRecId.Rows[0][1]);
                        pClsProperty.history_union_id = Val.ToInt64(p_dtbProcessRecId.Rows[0][2]);
                        pClsProperty.lot_srno = Val.ToInt64(p_dtbProcessRecId.Rows[0][3]);
                    }
                }
                else
                {
                    pClsProperty.union_id = 0;
                    pClsProperty.receive_union_id = 0;
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

        public MFGProcessReceiveProperty Save_Process_Receive_Split(MFGProcessReceiveProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            //int IntRes = 0;

            try
            {
                Request Request = new Request();

                Request.AddParams("@receive_date", pClsProperty.receive_date, DbType.Date);
                Request.AddParams("@issue_id", pClsProperty.Issue_id, DbType.Int32);
                Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int32);
                Request.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int32);
                Request.AddParams("@rough_cut_id", pClsProperty.rough_cut_id, DbType.Int32);
                Request.AddParams("@union_id", pClsProperty.union_id, DbType.Int64);

                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

                Request.AddParams("@manager_id", pClsProperty.manager_id, DbType.Int32);
                Request.AddParams("@employee_id", pClsProperty.employee_id, DbType.Int32);
                Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int32);
                Request.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int32);
                Request.AddParams("@rough_clarity_id", pClsProperty.rough_clarity_id, DbType.Int32);
                Request.AddParams("@rough_sieve_id", pClsProperty.rough_sieve_id, DbType.Int32);
                Request.AddParams("@rough_quality_id", pClsProperty.rough_quality_id, DbType.Int32);

                Request.AddParams("@balance_pcs", pClsProperty.balance_pcs, DbType.Int32);
                Request.AddParams("@balance_carat", pClsProperty.balance_carat, DbType.Decimal);
                Request.AddParams("@pcs", pClsProperty.pcs, DbType.Int32);
                Request.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
                Request.AddParams("@is_net_rate", pClsProperty.is_net_rate, DbType.Int32);
                Request.AddParams("@net_rate", pClsProperty.net_rate, DbType.Decimal);
                Request.AddParams("@net_amount", pClsProperty.net_amount, DbType.Decimal);

                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);
                Request.AddParams("@receive_union_id", pClsProperty.receive_union_id, DbType.Int64);
                Request.AddParams("@issue_union_id", pClsProperty.issue_union_id, DbType.Int64);
                Request.AddParams("@mix_union_id", pClsProperty.mix_union_id, DbType.Int64);
                Request.AddParams("@history_union_id", pClsProperty.history_union_id, DbType.Int64);
                Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Process_Receive_Split_Save;
                Request.CommandType = CommandType.StoredProcedure;

                DataTable p_dtbProcessRecId = new DataTable();
                if (Conn != null)
                    Conn.Inter1.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, p_dtbProcessRecId, Request, pEnum);
                else
                    Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, p_dtbProcessRecId, Request);

                if (p_dtbProcessRecId != null)
                {
                    if (p_dtbProcessRecId.Rows.Count > 0)
                    {
                        pClsProperty.union_id = Val.ToInt64(p_dtbProcessRecId.Rows[0][0]);
                        pClsProperty.receive_union_id = Val.ToInt64(p_dtbProcessRecId.Rows[0][1]);
                        pClsProperty.issue_union_id = Val.ToInt64(p_dtbProcessRecId.Rows[0][2]);
                        pClsProperty.mix_union_id = Val.ToInt64(p_dtbProcessRecId.Rows[0][3]);
                        pClsProperty.history_union_id = Val.ToInt64(p_dtbProcessRecId.Rows[0][4]);
                        pClsProperty.lot_srno = Val.ToInt64(p_dtbProcessRecId.Rows[0][5]);
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
        public int Update(MFGProcessReceiveProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            int IntRes = 0;

            try
            {
                Request Request = new Request();
                Request.AddParams("@receive_date", pClsProperty.receive_date, DbType.Date);
                //Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int32);
                Request.AddParams("@rough_cut_id", pClsProperty.rough_cut_id, DbType.Int64);
                Request.AddParams("@rough_clarity_id", pClsProperty.rough_clarity_id, DbType.Int32);
                Request.AddParams("@purity_id", pClsProperty.purity_id, DbType.Int32);
                Request.AddParams("@rough_sieve_id", pClsProperty.rough_sieve_id, DbType.Int32);
                Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int32);
                Request.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@total_rate", pClsProperty.total_rate, DbType.Decimal);
                Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
                Request.AddParams("@count", pClsProperty.count, DbType.Int32);
                Request.AddParams("@type", pClsProperty.type, DbType.String);

                Request.AddParams("@issue_id", pClsProperty.Issue_id, DbType.Int64);
                Request.AddParams("@prediction_id", pClsProperty.prediction_id, DbType.Int64);

                Request.AddParams("@quality_id", pClsProperty.rough_quality_id, DbType.Int32);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Process_Receive_Update;
                Request.CommandType = CommandType.StoredProcedure;

                if (Conn != null)
                    IntRes = Conn.Inter1.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request, pEnum);
                else
                    IntRes = Ope.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IntRes;
        }
        public int Rough_Makable_Rate_Update(MFGProcessReceiveProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            int IntRes = 0;

            try
            {
                Request Request = new Request();
                Request.AddParams("@transfer_date", pClsProperty.transfer_date, DbType.Date);
                Request.AddParams("@rough_cut_no", pClsProperty.rough_cut_no, DbType.String);
                Request.AddParams("@department_id", pClsProperty.department_id, DbType.Int64);
                Request.AddParams("@kapan_no", pClsProperty.kapan_no, DbType.String);
                Request.AddParams("@rough_sieve_id", pClsProperty.rough_sieve_id, DbType.Int32);
                Request.AddParams("@rough_clarity_id", pClsProperty.rough_clarity_id, DbType.Int32);
                Request.AddParams("@purity_id", pClsProperty.purity_id, DbType.Int32);
                Request.AddParams("@type", pClsProperty.type, DbType.String);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Update_Rough_To_Depttrf_Rate;
                Request.CommandType = CommandType.StoredProcedure;

                if (Conn != null)
                    IntRes = Conn.Inter1.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request, pEnum);
                else
                    IntRes = Ope.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IntRes;
        }
        public DataTable Process_Chipyo_Rec_GetData(string type, string Clarity, string Sieve)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Process_Chipyo_Rec_GetData;

            Request.AddParams("@type", type, DbType.String);
            // Request.AddParams("@quality_id", Quality, DbType.String);
            Request.AddParams("@clarity_id", Clarity, DbType.String);
            Request.AddParams("@sieve_id", Sieve, DbType.String);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }

        public DataTable GetData(Int64 lot_id, string type, string Clarity, string Sieve)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Process_Receive_GetData;
            Request.AddParams("@lot_id", lot_id, DbType.Int64);
            Request.AddParams("@type", type, DbType.String);
            // Request.AddParams("@quality_id", Quality, DbType.String);
            Request.AddParams("@clarity_id", Clarity, DbType.String);
            Request.AddParams("@sieve_id", Sieve, DbType.String);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetLottingMainData(int UnionId)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Main_Lotting_Update;
            Request.AddParams("@mixsplit_union_id", UnionId, DbType.Int32);
            //Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            // Request.AddParams("@quality_id", Quality, DbType.String);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetLottingSplitData(int UnionId, string TransType)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Split_Lotting_Update;
            Request.AddParams("@mixsplit_union_id", UnionId, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@transaction_type", TransType, DbType.Int32);
            // Request.AddParams("@quality_id", Quality, DbType.String);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetMixSplitMainData(int UnionId)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Main_MixSplit_Update;
            Request.AddParams("@mixsplit_union_id", UnionId, DbType.Int32);
            //Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            // Request.AddParams("@quality_id", Quality, DbType.String);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetMixSplitData(int UnionId, string TransType)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_MixSplit_Update;
            Request.AddParams("@mixsplit_union_id", UnionId, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@transaction_type", TransType, DbType.Int32);
            // Request.AddParams("@quality_id", Quality, DbType.String);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetKapanMainData(int UnionId)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Kapan_Mixing_Main_GetData;
            Request.AddParams("@mixsplit_union_id", UnionId, DbType.Int32);
            //Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            // Request.AddParams("@quality_id", Quality, DbType.String);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetSearchProcess(string fdate, string tdate, int kapanid, int CutId, int procId, int subProcId, string FormName)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_ProcessIssue_Get;
            Request.AddParams("@kapan_id", kapanid, DbType.Int32);
            Request.AddParams("@cut_id", CutId, DbType.Int32);
            Request.AddParams("@process_id", procId, DbType.Int32);
            Request.AddParams("@sub_process_id", subProcId, DbType.Int32);
            Request.AddParams("@form_name", FormName, DbType.String);
            Request.AddParams("@datFromDate", Val.DBDate(fdate), DbType.Date);
            Request.AddParams("@datToDate", Val.DBDate(tdate), DbType.Date);
            // Request.AddParams("@quality_id", Quality, DbType.String);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetSearchLotting(string fdate, string tdate, int kapanid, int CutId, int procId, int subProcId)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_MixSplit_Lotting_Search;
            Request.AddParams("@kapan_id", kapanid, DbType.Int32);
            Request.AddParams("@cut_id", CutId, DbType.Int32);
            Request.AddParams("@process_id", procId, DbType.Int32);
            Request.AddParams("@sub_process_id", subProcId, DbType.Int32);
            Request.AddParams("@datFromDate", Val.DBDate(fdate), DbType.Date);
            Request.AddParams("@datToDate", Val.DBDate(tdate), DbType.Date);
            // Request.AddParams("@quality_id", Quality, DbType.String);
            //Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            //Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            //Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetSearchMixSplit(string fdate, string tdate, int kapanid, int CutId, int procId, int subProcId)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_MixSplit_Search;
            Request.AddParams("@kapan_id", kapanid, DbType.Int32);
            Request.AddParams("@cut_id", CutId, DbType.Int32);
            Request.AddParams("@process_id", procId, DbType.Int32);
            Request.AddParams("@sub_process_id", subProcId, DbType.Int32);
            Request.AddParams("@datFromDate", Val.DBDate(fdate), DbType.Date);
            Request.AddParams("@datToDate", Val.DBDate(tdate), DbType.Date);
            // Request.AddParams("@quality_id", Quality, DbType.String);
            //Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            //Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            //Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable Process_Quality_GetData(string type, string Clarity, string Sieve, string Quality)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Process_Quality_Rec_GetData;
            Request.AddParams("@type", type, DbType.String);
            Request.AddParams("@quality_id", Quality, DbType.String);
            Request.AddParams("@clarity_id", Clarity, DbType.String);
            Request.AddParams("@sieve_id", Sieve, DbType.String);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable Process_Quality_LS_AssortFinal_GetData(string type, string Clarity, string Sieve, string Quality)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Process_LSAssort_Rec_GetData;
            Request.AddParams("@type", type, DbType.String);
            Request.AddParams("@quality_id", Quality, DbType.String);
            Request.AddParams("@clarity_id", Clarity, DbType.String);
            Request.AddParams("@sieve_id", Sieve, DbType.String);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable Process_LS_AssortFinal_GetData(int LotSrno)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_LSAssort_Receive_GetData;
            Request.AddParams("@lot_srno", LotSrno, DbType.Int32);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetShowData(string type)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Process_Receive_List_GetData;
            Request.AddParams("@type", type, DbType.String);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetShowFillData(string type, string Lot_id, Int32 Process_id)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Assort_Receive_Fill_GetData;
            Request.AddParams("@type", type, DbType.String);
            Request.AddParams("@lot_id", Lot_id, DbType.String);
            Request.AddParams("@process_id", Process_id, DbType.Int32);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetShowFillData_New(Int32 Kapan_Id, Int32 Rough_Cut_Id, string type, string Clarity, string Sieve, string Quality, Int32 Process_id)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_AssortFinal_Fill_GetData;
            Request.AddParams("@kapan_id", Kapan_Id, DbType.Int32);
            Request.AddParams("@rough_cut_id", Rough_Cut_Id, DbType.Int32);
            Request.AddParams("@type", type, DbType.String);
            //Request.AddParams("@lot_id", Lot_id, DbType.String);
            Request.AddParams("@process_id", Process_id, DbType.Int32);
            Request.AddParams("@quality_id", Quality, DbType.String);
            Request.AddParams("@clarity_id", Clarity, DbType.String);
            Request.AddParams("@sieve_id", Sieve, DbType.String);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetIssueProcess(Int64 Lot_id, string process)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Issue_Process_GetData;
            Request.AddParams("@lot_id", Lot_id, DbType.Int64);
            Request.AddParams("@process", process, DbType.String);
            //Request.AddParams("@cut_id", Cut_id, DbType.Int32);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable Carat_OutStanding_GetData(Int64 Lot_id, int process_id, int sub_process_id, int flag, string type)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Process_Outstanding;

            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@lot_id", Lot_id, DbType.Int64);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@process_id", process_id, DbType.Int32);
            Request.AddParams("@sub_process_id", sub_process_id, DbType.Int32);
            Request.AddParams("@type", type, DbType.String);
            Request.AddParams("@flag", flag, DbType.Int32);
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable Carat_OutStanding_Janged_GetData(Int64 Lot_id, int process_id, int sub_process_id, int flag, string type, Int64 Janged_No)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Process_Janged_Outstanding;

            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@lot_id", Lot_id, DbType.Int64);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@process_id", process_id, DbType.Int32);
            Request.AddParams("@sub_process_id", sub_process_id, DbType.Int32);
            Request.AddParams("@type", type, DbType.String);
            Request.AddParams("@flag", flag, DbType.Int32);
            Request.AddParams("@janged_no", Janged_No, DbType.Int64);
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable Janged_No_Receive_GetData(Int64 Lot_id, Int32 process_id)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Janged_No_GtData;

            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@lot_id", Lot_id, DbType.Int64);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@process_id", process_id, DbType.Int32);
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable Carat_Sarin_OutStanding_GetData(Int64 Lot_id, int process_id, int sub_process_id, int flag, string type)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Sarin_Process_Outstanding;

            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@lot_id", Lot_id, DbType.Int64);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@process_id", process_id, DbType.Int32);
            Request.AddParams("@sub_process_id", sub_process_id, DbType.Int32);
            Request.AddParams("@type", type, DbType.String);
            Request.AddParams("@flag", flag, DbType.Int32);
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable Carat_Chapka_OutStanding_GetData(Int64 Lot_id, int process_id, int sub_process_id, int flag, string type)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Chapka_Process_Outstanding;

            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@lot_id", Lot_id, DbType.Int64);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@process_id", process_id, DbType.Int32);
            Request.AddParams("@sub_process_id", sub_process_id, DbType.Int32);
            Request.AddParams("@type", type, DbType.String);
            Request.AddParams("@flag", flag, DbType.Int32);
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public int GetEmployee(Int64 Lot_id, int process_id, int sub_process_id)
        {
            int Emp_Id = 0;
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_IssueEmp_GetData;

            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@lot_id", Lot_id, DbType.Int64);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@process_id", process_id, DbType.Int32);
            Request.AddParams("@sub_process_id", sub_process_id, DbType.Int32);
            //Request.AddParams("@type", type, DbType.String);
            //Request.AddParams("@flag", flag, DbType.Int32);
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            if (DTab.Rows.Count > 0)
                Emp_Id = Val.ToInt(DTab.Rows[0]["employee_id"]);
            else
                Emp_Id = 0;
            return Emp_Id;
        }
        public DataTable GetBalanceCarat(Int64 LotId)
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
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable ProcessIssue_GetData(Int64 LotId, string process)
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
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable MFG_ProcessName_GetData(Int64 LotId)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Process_Name_Get;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@lot_id", LotId, DbType.Int64);
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable MFG_PendingLots_GetData(int CutId)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_PendingLots_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@cut_id", CutId, DbType.Int32);
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }

        public DataTable GetIssueID(Int64 LotId, int process_id, int sub_process_id)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_IssueDetail_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@lot_id", LotId, DbType.Int64);
            Request.AddParams("@process_id", process_id, DbType.Int32);
            Request.AddParams("@sub_process_id", sub_process_id, DbType.Int32);
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);

            return DTab;
        }
        public DataTable GetIssueData(string LotId, int process_id, int sub_process_id)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Issue_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@lot_id", LotId, DbType.String);
            Request.AddParams("@process_id", process_id, DbType.Int32);
            Request.AddParams("@sub_process_id", sub_process_id, DbType.Int32);
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);

            return DTab;
        }
        public DataTable GetSearchKapan(string fdate, string tdate, int kapanid, int CutId, int procId, int subProcId)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Kapan_Mixing_GetData;
            Request.AddParams("@kapan_id", kapanid, DbType.Int32);
            Request.AddParams("@cut_id", CutId, DbType.Int32);
            Request.AddParams("@process_id", procId, DbType.Int32);
            //Request.AddParams("@sub_process_id", subProcId, DbType.Int32);
            Request.AddParams("@datFromDate", Val.DBDate(fdate), DbType.Date);
            Request.AddParams("@datToDate", Val.DBDate(tdate), DbType.Date);
            // Request.AddParams("@quality_id", Quality, DbType.String);
            //Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            //Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            //Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetSearchProcessIssue(string fdate, string tdate, int kapanid, int CutId, int procId, int subProcId, string Form)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Process_Issue_Search_GetData;
            Request.AddParams("@kapan_id", kapanid, DbType.Int32);
            Request.AddParams("@cut_id", CutId, DbType.Int32);
            Request.AddParams("@process_id", procId, DbType.Int32);
            //Request.AddParams("@sub_process_id", subProcId, DbType.Int32);
            Request.AddParams("@form", Form, DbType.String);
            Request.AddParams("@datFromDate", Val.DBDate(fdate), DbType.Date);
            Request.AddParams("@datToDate", Val.DBDate(tdate), DbType.Date);
            // Request.AddParams("@quality_id", Quality, DbType.String);
            //Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            //Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            //Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetSearchDepartmentTransfer(string fdate, string tdate, int kapanid, int CutId, int procId)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Department_Transfer_Search_GetData;
            Request.AddParams("@kapan_id", kapanid, DbType.Int32);
            Request.AddParams("@cut_id", CutId, DbType.Int32);
            Request.AddParams("@process_id", procId, DbType.Int32);
            //Request.AddParams("@sub_process_id", subProcId, DbType.Int32);
            Request.AddParams("@datFromDate", Val.DBDate(fdate), DbType.Date);
            Request.AddParams("@datToDate", Val.DBDate(tdate), DbType.Date);
            // Request.AddParams("@quality_id", Quality, DbType.String);
            //Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            //Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            //Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }

        public int Makable_Pcs_Save(MFGProcessReceiveProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            int IntRes = 0;

            try
            {
                Request Request = new Request();
                Request.AddParams("@makable_id", pClsProperty.makable_id, DbType.Int64);
                Request.AddParams("@receive_date", pClsProperty.receive_date, DbType.Date);
                Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int32);
                Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int64);
                //Request.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
                //Request.AddParams("@size", pClsProperty.size, DbType.Decimal);
                //Request.AddParams("@pcs", pClsProperty.pcs, DbType.Int32);
                Request.AddParams("@mfg_pcs", pClsProperty.mfg_pcs, DbType.Decimal);
                //Request.AddParams("@type", pClsProperty.type, DbType.String);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Makable_Pcs_Save;
                Request.CommandType = CommandType.StoredProcedure;

                if (Conn != null)
                    IntRes = Conn.Inter1.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request, pEnum);
                else
                    IntRes = Ope.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IntRes;
        }
        public DataTable GetSearchBoilIssRec(string fdate, string tdate, int procId, int subProcId, string FormName)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_BoilIssueRec_Get;
            
            Request.AddParams("@process_id", procId, DbType.Int32);
            Request.AddParams("@sub_process_id", subProcId, DbType.Int32);
            Request.AddParams("@form_name", FormName, DbType.String);
            Request.AddParams("@datFromDate", Val.DBDate(fdate), DbType.Date);
            Request.AddParams("@datToDate", Val.DBDate(tdate), DbType.Date);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
    }
}
