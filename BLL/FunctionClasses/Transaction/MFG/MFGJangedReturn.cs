using BLL.PropertyClasses.Transaction.MFG;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction.MFG
{
    public class MFGJangedReturn
    {
        InterfaceLayer Ope = new InterfaceLayer();
        BLL.Validation Val = new BLL.Validation();

        public MFGJangedReturn_Property Save(MFGJangedReturn_Property pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@janged_no", pClsProperty.janged_no, DbType.Int32);
                Request.AddParams("@prev_janged_id", pClsProperty.previous_janged_id, DbType.Int64);
                Request.AddParams("@prev_janged_no", pClsProperty.previous_janged_no, DbType.Int64);
                Request.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int64);
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
                Request.AddParams("@from_manager_id", pClsProperty.from_manager_id, DbType.Int32);
                Request.AddParams("@to_manager_id", pClsProperty.to_manager_id, DbType.Int32);
                Request.AddParams("@employee_id", pClsProperty.employee_id, DbType.Int32);
                Request.AddParams("@from_process_id", pClsProperty.from_process_id, DbType.Int32);
                Request.AddParams("@to_process_id", pClsProperty.to_process_id, DbType.Int32);
                Request.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int32);
                Request.AddParams("@rough_sieve_id", pClsProperty.rough_sieve_id, DbType.Int32);
                Request.AddParams("@purity_id", pClsProperty.purity_id, DbType.Int32);
                Request.AddParams("@quality_id", pClsProperty.rough_quality_id, DbType.Int32);
                Request.AddParams("@rough_clarity_id", pClsProperty.rough_clarity_id, DbType.Int32);
                Request.AddParams("@party_id", pClsProperty.party_id, DbType.Int32);
                Request.AddParams("@pcs", pClsProperty.pcs, DbType.Int32);
                Request.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
                Request.AddParams("@rejection_pcs", pClsProperty.rejection_pcs, DbType.Int32);
                Request.AddParams("@rejection_carat", pClsProperty.rejection_carat, DbType.Decimal);
                Request.AddParams("@breakage_pcs", pClsProperty.breakage_pcs, DbType.Int32);
                Request.AddParams("@breakage_carat", pClsProperty.breakage_carat, DbType.Decimal);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
                Request.AddParams("@prediction_id", pClsProperty.prediction_id, DbType.Int64);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);
                Request.AddParams("@is_outside", pClsProperty.is_outside, DbType.Int32);
                Request.AddParams("@type", "R", DbType.String);
                Request.AddParams("@janged_union_id", pClsProperty.janged_union_id, DbType.Int64);
                Request.AddParams("@dept_union_id", pClsProperty.dept_union_id, DbType.Int64);
                Request.AddParams("@loss_carat", pClsProperty.loss_carat, DbType.Decimal);
                Request.AddParams("@carat_plus", pClsProperty.carat_plus, DbType.Decimal);
                Request.AddParams("@loss_count", pClsProperty.loss_count, DbType.Int32);
                Request.AddParams("@history_union_id", pClsProperty.history_union_id, DbType.Int64);
                Request.AddParams("@janged_srno", pClsProperty.janged_srno, DbType.Int64);
                Request.AddParams("@prev_lot_id", pClsProperty.prev_lot_id, DbType.Int64);
                Request.AddParams("@rr_pcs", pClsProperty.rr_pcs, DbType.Int32);
                Request.AddParams("@rr_carat", pClsProperty.rr_carat, DbType.Decimal);
                Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Janged_Return_Save;
                Request.CommandType = CommandType.StoredProcedure;

                DataTable p_dtbProcessJangedRecId = new DataTable();
                if (Conn != null)
                    Conn.Inter1.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, p_dtbProcessJangedRecId, Request, pEnum);
                else
                    Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, p_dtbProcessJangedRecId, Request);

                if (p_dtbProcessJangedRecId != null)
                {
                    if (p_dtbProcessJangedRecId.Rows.Count > 0)
                    {
                        pClsProperty.janged_union_id = Val.ToInt64(p_dtbProcessJangedRecId.Rows[0][0]);
                        pClsProperty.dept_union_id = Val.ToInt64(p_dtbProcessJangedRecId.Rows[0][1]);
                        pClsProperty.janged_no = Val.ToInt64(p_dtbProcessJangedRecId.Rows[0][2]);
                        pClsProperty.history_union_id = Val.ToInt64(p_dtbProcessJangedRecId.Rows[0][3]);
                        pClsProperty.janged_srno = Val.ToInt64(p_dtbProcessJangedRecId.Rows[0][4]);
                        pClsProperty.lot_srno = Val.ToInt64(p_dtbProcessJangedRecId.Rows[0][5]);
                    }
                }
                else
                {
                    pClsProperty.janged_union_id = 0;
                    pClsProperty.dept_union_id = 0;
                    pClsProperty.janged_no = 0;
                    pClsProperty.history_union_id = 0;
                    pClsProperty.janged_srno = 0;
                    pClsProperty.lot_srno = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }
        public int Update(MFGJangedReturn_Property pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            int IntRes = 0;
            try
            {

                Request Request = new Request();

                Request.AddParams("@janged_no", pClsProperty.janged_no, DbType.Int32);
                Request.AddParams("@to_company_id", pClsProperty.to_company_id, DbType.Int32);
                Request.AddParams("@to_branch_id", pClsProperty.to_branch_id, DbType.Int32);
                Request.AddParams("@to_location_id", pClsProperty.to_location_id, DbType.Int32);
                Request.AddParams("@to_department_id", pClsProperty.to_department_id, DbType.Int32);
                Request.AddParams("@to_manager_id", pClsProperty.to_manager_id, DbType.Int32);
                Request.AddParams("@to_process_id", pClsProperty.to_process_id, DbType.Int32);
                Request.AddParams("@janged_date", pClsProperty.janged_date, DbType.Date);
                //Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Janged_Return_Update;
                Request.CommandType = CommandType.StoredProcedure;

                DataTable p_dtbProcessJangedRecId = new DataTable();
                if (Conn != null)
                    IntRes = Conn.Inter1.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request, pEnum);
                else
                    IntRes = Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IntRes;
        }
        public DataTable ReturnStock_GetData()
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_JangedReturn_Stock_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable Stock_GetData(Int64 LotId, int Fac_Main_LotID, int Janged_No)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Janged_Stock_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@lot_id", LotId, DbType.Int64);
            Request.AddParams("@fac_main_lot_id", Fac_Main_LotID, DbType.Int64);
            Request.AddParams("@janged_no", Janged_No, DbType.Int64);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable Main_LotID_Stock_GetData(Int64 LotId, Int64 Fac_Main_LotID, int Janged_No)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Janged_Main_Stock_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@lot_id", LotId, DbType.Int64);
            Request.AddParams("@fac_main_lot_id", Fac_Main_LotID, DbType.Int64);
            Request.AddParams("@janged_no", Janged_No, DbType.Int64);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable Boiling_Stock_GetData(Int64 LotId, int Fac_Main_LotID, int Janged_No, Int64 Kapan_ID, Int64 Rough_Cut_ID)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Janged_Boiling_Stock_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@kapan_id", Kapan_ID, DbType.Int64);
            Request.AddParams("@rough_cut_id", Rough_Cut_ID, DbType.Int64);
            Request.AddParams("@lot_id", LotId, DbType.Int64);
            Request.AddParams("@fac_main_lot_id", Fac_Main_LotID, DbType.Int64);
            Request.AddParams("@janged_no", Janged_No, DbType.Int64);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable Process_CountData(MFGJangedReturn_Property pClsProperty)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Process_Lock;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int64);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable JangedReturn_GetData(int LotSrno)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Janged_Return_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@lot_srno", LotSrno, DbType.Int32);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
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
        public int JangedRecieveDelete(MFGJangedReturn_Property pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            int IntRes = 0;
            try
            {

                Request RequestDetails = new Request();

                RequestDetails.AddParams("@janged_no", pClsProperty.janged_no, DbType.Int64);

                RequestDetails.CommandText = BLL.TPV.SProc.MFG_TRN_Janged_Return_Delete;
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
    }
}
