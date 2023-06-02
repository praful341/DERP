using BLL.PropertyClasses.Transaction;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction.MFG
{
    public class MFGAssortSecond
    {
        InterfaceLayer Ope = new InterfaceLayer();
        BLL.Validation Val = new BLL.Validation();
        public MFGProcessReceiveProperty Save(MFGProcessReceiveProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
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
                Request.AddParams("@location_id", pClsProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
                Request.AddParams("@manager_id", pClsProperty.manager_id, DbType.Int32);
                Request.AddParams("@employee_id", pClsProperty.employee_id, DbType.Int32);
                Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int32);
                Request.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int32);
                Request.AddParams("@purity_id", pClsProperty.purity_id, DbType.Int32);
                Request.AddParams("@color_id", pClsProperty.color_id, DbType.Int32);
                Request.AddParams("@rough_sieve_id", pClsProperty.rough_sieve_id, DbType.Int32);
                Request.AddParams("@pcs", pClsProperty.pcs, DbType.Int32);
                Request.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
                Request.AddParams("@percentage", pClsProperty.percentage, DbType.Decimal);
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
                Request.AddParams("@temp_quality_name", pClsProperty.temp_quality_name, DbType.String);
                Request.AddParams("@issue_union_id", pClsProperty.issue_union_id, DbType.Int64);
                Request.AddParams("@temp_sieve_name", pClsProperty.temp_sieve_name, DbType.String);
                Request.AddParams("@assort_total_carat", pClsProperty.assort_total_carat, DbType.Int64);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_AssortSecond_Receive_Save;
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
                        pClsProperty.Issue_id = Val.ToInt64(p_dtbProcessRecId.Rows[0][3]);
                        pClsProperty.lot_srno = Val.ToInt64(p_dtbProcessRecId.Rows[0][4]);
                        pClsProperty.issue_union_id = Val.ToInt64(p_dtbProcessRecId.Rows[0][5]);
                    }
                }
                else
                {
                    pClsProperty.union_id = 0;
                    pClsProperty.receive_union_id = 0;
                    pClsProperty.history_union_id = 0;
                    pClsProperty.Issue_id = 0;
                    pClsProperty.lot_srno = 0;
                    pClsProperty.issue_union_id = 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }
        public DataTable AssortSecondGetData(string Purity, string Color, string Sieve, Int32 kapan_id, Int32 rough_cut_id, Int32 process_id, Int32 sub_process_id, string temp_quality_name, string temp_sieve_name, Int64 Lot_SRNo, Int64 Location_ID)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Assort_Second_GetData;
            Request.AddParams("@purity_id", Purity, DbType.String);
            Request.AddParams("@color_id", Color, DbType.String);
            Request.AddParams("@sieve_id", Sieve, DbType.String);
            Request.AddParams("@kapan_id", kapan_id, DbType.Int32);
            Request.AddParams("@cut_id", rough_cut_id, DbType.Int32);
            Request.AddParams("@process_id", process_id, DbType.Int32);
            Request.AddParams("@sub_process_id", sub_process_id, DbType.Int32);
            Request.AddParams("@temp_purity_name", temp_quality_name, DbType.String);
            Request.AddParams("@temp_sieve_name", temp_sieve_name, DbType.String);
            Request.AddParams("@lot_srno", Lot_SRNo, DbType.Int64);
            Request.AddParams("@location_id", Location_ID, DbType.Int64);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable AssortJangedGetData(Int64 Lot_SrNo, Int64 Kapan_ID, Int64 Rough_Cut_ID)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Assort_Janged_GetData;
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@lot_srno", Lot_SrNo, DbType.Int64);
            Request.AddParams("@kapan_id", Kapan_ID, DbType.Int64);
            Request.AddParams("@rough_cut_id", Rough_Cut_ID, DbType.Int64);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public decimal AssortGetRate(int PurityID, int SieveId)
        {
            decimal rate = 0;
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Purity_GetRate;
            Request.AddParams("@purity_id", PurityID, DbType.Int32);
            Request.AddParams("@sieve_id", SieveId, DbType.Int32);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            if (DTab.Rows.Count > 0)
            {
                rate = Val.ToDecimal(DTab.Rows[0]["per_carat"]);
            }
            else
                rate = 0;
            return rate;
        }
        public int GetPurityId(string purity)
        {
            DataTable DTab = new DataTable();
            int PurityId = 0;
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_Purity_GetId;

            Request.AddParams("@purity", purity, DbType.String);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            if (DTab.Rows.Count > 0)
            {
                PurityId = Val.ToInt(DTab.Rows[0]["purity_id"]);
            }
            return PurityId;
        }
    }
}
