using BLL.PropertyClasses.Transaction;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction.MFG
{
    public class MFGAssortFirst
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
                Request.AddParams("@assort_total_carat", pClsProperty.assort_total_carat, DbType.Int64);
                Request.AddParams("@temp_sieve_name", pClsProperty.temp_sieve_name, DbType.String);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_AssortFirst_Receive_Save;
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
                        pClsProperty.Issue_id = Val.ToInt32(p_dtbProcessRecId.Rows[0][3]);
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
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }
        public DataTable AssortFirstGetData(string Color, string Sieve, Int32 kapan_id, Int32 rough_cut_id, Int32 process_id, Int32 sub_process_id, string temp_quality_name, string temp_sieve_name, Int64 Lot_SRNo, Int64 Location_ID)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Assort_First_GetData;

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
        public DataTable Print_Semi_1(int numkapan_id, int numcut_id, int numprocess_id, int numsubProcess_id, string temp_purity_name, string temp_sieve_name, Int64 Lot_SRNo, Int64 Location_ID)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.RPT_MFG_TRN_Assortment_Semi_1;

            Request.AddParams("@kapan_id", numkapan_id, DbType.Int32);
            Request.AddParams("@cut_id", numcut_id, DbType.Int32);
            Request.AddParams("@process_id", numprocess_id, DbType.Int32);
            Request.AddParams("@sub_process_id", numsubProcess_id, DbType.Int32);
            Request.AddParams("@temp_purity_name", temp_purity_name, DbType.String);
            Request.AddParams("@temp_sieve_name", temp_sieve_name, DbType.String);
            Request.AddParams("@lot_srno", Lot_SRNo, DbType.Int64);
            Request.AddParams("@location_id", Location_ID, DbType.Int64);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }

        public DataTable Print_Semi_1_Sub(int numkapan_id, int numcut_id, int numprocess_id, int numsubProcess_id, string temp_purity_name, string temp_sieve_name, Int64 Lot_SRNo, Int64 Location_ID)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.RPT_MFG_TRN_Assortment_Semi_1_Sub;

            Request.AddParams("@kapan_id", numkapan_id, DbType.Int32);
            Request.AddParams("@cut_id", numcut_id, DbType.Int32);
            Request.AddParams("@process_id", numprocess_id, DbType.Int32);
            Request.AddParams("@sub_process_id", numsubProcess_id, DbType.Int32);
            Request.AddParams("@temp_purity_name", temp_purity_name, DbType.String);
            Request.AddParams("@temp_sieve_name", temp_sieve_name, DbType.String);
            Request.AddParams("@lot_srno", Lot_SRNo, DbType.Int64);
            Request.AddParams("@location_id", Location_ID, DbType.Int64);

            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }

        public DataSet Print_Semi_2(int numkapan_id, int numcut_id, int numprocess_id, int numsubProcess_id, string temp_purity_name, string temp_sieve_name, Int64 Lot_SRNo, Int64 Location_ID)
        {
            DataSet DTab = new DataSet();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.RPT_MFG_TRN_Assortment_Semi_2;

            Request.AddParams("@kapan_id", numkapan_id, DbType.Int32);
            Request.AddParams("@cut_id", numcut_id, DbType.Int32);
            Request.AddParams("@process_id", numprocess_id, DbType.Int32);
            Request.AddParams("@sub_process_id", numsubProcess_id, DbType.Int32);
            Request.AddParams("@temp_purity_name", temp_purity_name, DbType.String);
            Request.AddParams("@temp_sieve_name", temp_sieve_name, DbType.String);
            Request.AddParams("@lot_srno", Lot_SRNo, DbType.Int64);
            Request.AddParams("@location_id", Location_ID, DbType.Int64);

            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataSet(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, "Semi", Request);
            return DTab;
        }
        public DataTable Print_Shadding(int numkapan_id, int numcut_id, int numprocess_id, int numsubProcess_id)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.RPT_MFG_TRN_Assortment_Sheding;

            Request.AddParams("@kapan_id", numkapan_id, DbType.Int32);
            Request.AddParams("@cut_id", numcut_id, DbType.Int32);
            Request.AddParams("@process_id", numprocess_id, DbType.Int32);
            Request.AddParams("@sub_process_id", numsubProcess_id, DbType.Int32);

            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }

        public int GetColorId(string color)
        {
            DataTable DTab = new DataTable();
            int ColorId = 0;
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_Color_GetId;

            Request.AddParams("@color", color, DbType.Int32);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            if (DTab.Rows.Count > 0)
            {
                ColorId = Val.ToInt(DTab.Rows[0]["color_id"]);
            }
            return ColorId;
        }
        public DataTable CheckEntry(MFGProcessReceiveProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Assort_First_CheckEntry;

            Request.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int32);
            Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int32);
            Request.AddParams("@rough_cut_id", pClsProperty.rough_cut_id, DbType.Int32);
            Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int32);
            Request.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int32);

            Request.CommandType = CommandType.StoredProcedure;
            if (Conn != null)
                Conn.Inter1.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, DTab, Request, pEnum);
            else
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);

            return DTab;
        }

        public int GetDeleteLot_ID(MFGProcessReceiveProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            DataTable DTab = new DataTable();
            int IntRes = 0;
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Assort_Lot_Delete;

            Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int32);
            Request.AddParams("@cut_id", pClsProperty.rough_cut_id, DbType.Int32);
            Request.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int32);
            Request.AddParams("@temp_quality_name", pClsProperty.temp_quality_name, DbType.String);
            Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int32);
            Request.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int32);
            Request.AddParams("@temp_sieve_name", pClsProperty.temp_sieve_name, DbType.String);
            Request.AddParams("@flag", pClsProperty.flag, DbType.Int32);
            Request.AddParams("@del_lot_srno", pClsProperty.Del_lot_srno, DbType.Int64);
            Request.CommandType = CommandType.StoredProcedure;

            IntRes = Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
            return IntRes;
        }
        public int GetUpdateLot_ID_Flag(MFGProcessReceiveProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            DataTable DTab = new DataTable();
            int IntRes = 0;
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Assort_Lot_Flag_Update;
            Request.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int32);
            Request.AddParams("@flag", 1, DbType.Int32);
            Request.AddParams("@flag_update", pClsProperty.flag_update, DbType.Int32);
            Request.CommandType = CommandType.StoredProcedure;

            IntRes = Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
            return IntRes;
        }
        public int GetDeleteFinalLot_ID(MFGProcessReceiveProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            DataTable DTab = new DataTable();
            int IntRes = 0;
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Assort_Lot_Final_Delete;

            Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int32);
            Request.AddParams("@cut_id", pClsProperty.rough_cut_id, DbType.Int32);
            Request.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int32);
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
