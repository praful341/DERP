using BLL.PropertyClasses.Transaction.MFG;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction.MFG
{
    public class MFGTRNEstimation
    {
        InterfaceLayer Ope = new InterfaceLayer();
        BLL.Validation Val = new BLL.Validation();

        public Int64 Save_MFGTRNEstimation(MFGTRNEstimation_Property pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            Int64 IntRes = 0;

            try
            {
                Request Request = new Request();

                Request.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int32);
                Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int32);
                Request.AddParams("@rough_cut_id", pClsProperty.rough_cut_id, DbType.Int32);
                Request.AddParams("@estimation_date", pClsProperty.estimation_date, DbType.Date);
                Request.AddParams("@sarin_date", pClsProperty.sarin_date, DbType.Date);
                Request.AddParams("@russian_date", pClsProperty.russian_date, DbType.Date);
                Request.AddParams("@polish_date", pClsProperty.polish_date, DbType.Date);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
                Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int32);
                Request.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int32);
                Request.AddParams("@org_pcs", pClsProperty.org_pcs, DbType.Int32);
                Request.AddParams("@org_carat", pClsProperty.org_carat, DbType.Decimal);
                Request.AddParams("@pcs", pClsProperty.pcs, DbType.Int32);
                Request.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
                Request.AddParams("@previous_pcs", pClsProperty.previous_pcs, DbType.Int32);
                Request.AddParams("@previous_carat", pClsProperty.previous_carat, DbType.Decimal);
                Request.AddParams("@average_per", pClsProperty.average_per, DbType.Decimal);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);
                Request.AddParams("@flag", pClsProperty.flag, DbType.Int32);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Sarin_Estimation_Save;
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
        public Int64 Save_MFGPataLotPurityWise(MFGTRNEstimation_Property pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            Int64 IntRes = 0;

            try
            {
                Request Request = new Request();

                Request.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int64);
                Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int64);
                Request.AddParams("@rough_cut_id", pClsProperty.rough_cut_id, DbType.Int64);
                Request.AddParams("@patalot_date", pClsProperty.patalot_date, DbType.Date);
                Request.AddParams("@machine_name", pClsProperty.machine_name, DbType.String);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int64);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int64);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int64);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int64);
                Request.AddParams("@quality_id", pClsProperty.quality_id, DbType.Int64);
                Request.AddParams("@rough_clarity_id", pClsProperty.rough_clarity_id, DbType.Int64);
                Request.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
                Request.AddParams("@total_carat", pClsProperty.total_carat, DbType.Decimal);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);
                Request.AddParams("@manager_id", pClsProperty.manager_id, DbType.Int64);

                Request.AddParams("@prev_quality_id", pClsProperty.prev_quality_id, DbType.Int64);
                Request.AddParams("@prev_rough_sieve_id", pClsProperty.prev_rough_sieve_id, DbType.Int64);
                Request.AddParams("@prev_rough_clarity_id", pClsProperty.prev_rough_clarity_id, DbType.Int64);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_PataLot_PurityWise_Save;
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

        public DataTable GetSarinData(MFGTRNEstimation_Property pClsProperty)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Sarin_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@entry_date", pClsProperty.entry_date, DbType.Date);
            Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int32);
            Request.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int32);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int32);
            Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int32);
            Request.AddParams("@rough_cut_id", pClsProperty.rough_cut_id, DbType.Int32);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetPurityWiseData(MFGTRNEstimation_Property pClsProperty)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_PataLot_PurityWise_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@entry_date", pClsProperty.entry_date, DbType.Date);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int64);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int64);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int64);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int64);
            Request.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int64);
            Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int64);
            Request.AddParams("@rough_cut_id", pClsProperty.rough_cut_id, DbType.Int64);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public int CheckEstExist(Int64 LotId)
        {
            int Dept = 0;
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Estimation_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@lot_id", LotId, DbType.Int64);
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            if (DTab.Rows.Count > 0)
            {
                Dept = Val.ToInt32(DTab.Rows[0]["department_id"]);
            }
            else
            {
                Dept = 0;
            }
            return Dept;
        }
        public int Estimation_Data_Delete(MFGTRNEstimation_Property pClsProperty)
        {
            try
            {
                int IntRes = 0;
                Request Request = new Request();

                Request.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int64);
                Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Date);
                Request.AddParams("@rough_cut_id", pClsProperty.rough_cut_id, DbType.Date);
                Request.AddParams("@process_id", pClsProperty.process_id, DbType.Date);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Estimation_Date_Delete;
                Request.CommandType = CommandType.StoredProcedure;

                IntRes = Ope.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request);
                return IntRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int PataLot_PurityWise_Data_Delete(MFGTRNEstimation_Property pClsProperty)
        {
            try
            {
                int IntRes = 0;
                Request Request = new Request();

                Request.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int64);
                Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Date);
                Request.AddParams("@rough_cut_id", pClsProperty.rough_cut_id, DbType.Date);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_PataLot_PurityWise_Delete;
                Request.CommandType = CommandType.StoredProcedure;

                IntRes = Ope.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request);
                return IntRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
