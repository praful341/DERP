using BLL.PropertyClasses.Transaction.MFG;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction.MFG
{
    public class MFGJangedReturnManual
    {
        InterfaceLayer Ope = new InterfaceLayer();
        BLL.Validation Val = new BLL.Validation();

        public int Save(MFGJangedReturn_Property pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@janged_no", pClsProperty.janged_no, DbType.Int32);
                Request.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int64);
                Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int32);
                Request.AddParams("@rough_cut_id", pClsProperty.rough_cut_id, DbType.Int32);
                Request.AddParams("@janged_date", pClsProperty.janged_date, DbType.Date);
                Request.AddParams("@to_company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@to_branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@to_location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@to_department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
                Request.AddParams("@company_id", pClsProperty.from_company_id, DbType.Int32);
                Request.AddParams("@branch_id", pClsProperty.from_branch_id, DbType.Int32);
                Request.AddParams("@location_id", pClsProperty.from_location_id, DbType.Int32);
                Request.AddParams("@department_id", pClsProperty.from_department_id, DbType.Int32);
                Request.AddParams("@manager_id", pClsProperty.from_manager_id, DbType.Int32);
                Request.AddParams("@to_manager_id", pClsProperty.to_manager_id, DbType.Int32);
                Request.AddParams("@employee_id", pClsProperty.employee_id, DbType.Int32);
                Request.AddParams("@process_id", pClsProperty.from_process_id, DbType.Int32);
                Request.AddParams("@to_process_id", pClsProperty.to_process_id, DbType.Int32);
                Request.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int32);
                Request.AddParams("@to_sub_process_id", pClsProperty.to_sub_process_id, DbType.Int32);
                Request.AddParams("@party_id", pClsProperty.party_id, DbType.Int32);
                Request.AddParams("@pcs", pClsProperty.pcs, DbType.Int32);
                Request.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
                Request.AddParams("@rr_pcs", pClsProperty.rr_pcs, DbType.Int32);
                Request.AddParams("@rr_carat", pClsProperty.rr_carat, DbType.Decimal);
                Request.AddParams("@rejection_pcs", pClsProperty.rejection_pcs, DbType.Int32);
                Request.AddParams("@rejection_carat", pClsProperty.rejection_carat, DbType.Decimal);
                Request.AddParams("@resoing_pcs", pClsProperty.resoing_pcs, DbType.Int32);
                Request.AddParams("@resoing_carat", pClsProperty.resoing_carat, DbType.Decimal);
                Request.AddParams("@breakage_pcs", pClsProperty.breakage_pcs, DbType.Int32);
                Request.AddParams("@breakage_carat", pClsProperty.breakage_carat, DbType.Decimal);
                Request.AddParams("@loss_pcs", pClsProperty.loss_pcs, DbType.Int32);
                Request.AddParams("@loss_carat", pClsProperty.loss_carat, DbType.Decimal);
                Request.AddParams("@lost_pcs", pClsProperty.lost_pcs, DbType.Int32);
                Request.AddParams("@lost_carat", pClsProperty.lost_carat, DbType.Decimal);
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
                Request.AddParams("@carat_plus", pClsProperty.carat_plus, DbType.Decimal);
                Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_JangedReturn_Save;
                Request.CommandType = CommandType.StoredProcedure;

                return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Update(MFGJangedReturn_Property pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@janged_no", pClsProperty.janged_no, DbType.Int32);
                Request.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int64);
                Request.AddParams("@janged_date", pClsProperty.janged_date, DbType.Date);
                Request.AddParams("@process_id", pClsProperty.from_process_id, DbType.Int32);
                Request.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int32);
                Request.AddParams("@pcs", pClsProperty.pcs, DbType.Int32);
                Request.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
                Request.AddParams("@breakage_pcs", pClsProperty.breakage_pcs, DbType.Int32);
                Request.AddParams("@breakage_carat", pClsProperty.breakage_carat, DbType.Decimal);
                Request.AddParams("@loss_pcs", pClsProperty.loss_pcs, DbType.Int32);
                Request.AddParams("@loss_carat", pClsProperty.loss_carat, DbType.Decimal);
                Request.AddParams("@lost_pcs", pClsProperty.lost_pcs, DbType.Int32);
                Request.AddParams("@lost_carat", pClsProperty.lost_carat, DbType.Decimal);
                Request.AddParams("@carat_plus", pClsProperty.carat_plus, DbType.Decimal);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
                Request.AddParams("@department_id", pClsProperty.from_department_id, DbType.Int32);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_JangedReturn_Update;
                Request.CommandType = CommandType.StoredProcedure;

                return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable JangedData(int JangedNo, Int64 LotId)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Janged_Detail;

            Request.AddParams("@lot_id", LotId, DbType.Int64);
            Request.AddParams("@janged_no", JangedNo, DbType.Int32);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetJangedOutstanding(Int64 LotId)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Janged_Outstanding;
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@lot_id", LotId, DbType.Int64);

            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }

        public DataTable JangedRecieveData(int KapanNo, int CutId)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Janged_Recieve_Data;

            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@kapan_id", KapanNo, DbType.Int32);
            Request.AddParams("@cut_id", CutId, DbType.Int32);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
    }
}
