using BLL.PropertyClasses.Transaction.MFG;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction.MFG
{
    public class MFGCostingManual
    {
        InterfaceLayer Ope = new InterfaceLayer();
        BLL.Validation Val = new BLL.Validation();

        public int Save(MFGCostingManualProperty pClsProperty)
        {
            int IntRes = 0;

            try
            {
                Request Request = new Request();

                Request.AddParams("@cost_id", pClsProperty.cost_id, DbType.Int64);

                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", pClsProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
                Request.AddParams("@cost_date", pClsProperty.cost_date, DbType.Date);
                Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int64);
                Request.AddParams("@rough_cut_no", pClsProperty.rough_cut_no, DbType.String);
                Request.AddParams("@kapan_carat", pClsProperty.kapan_carat, DbType.Decimal);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@labour_rate", pClsProperty.labour_rate, DbType.Decimal);
                Request.AddParams("@polish_carat", pClsProperty.polish_carat, DbType.Decimal);
                Request.AddParams("@polish_per", pClsProperty.polish_per, DbType.Decimal);
                Request.AddParams("@costing", pClsProperty.costing, DbType.Decimal);
                Request.AddParams("@average", pClsProperty.average, DbType.Decimal);
                Request.AddParams("@costing_amt", pClsProperty.costing_amt, DbType.Decimal);
                Request.AddParams("@kapan_pcs", pClsProperty.kapan_pcs, DbType.Int32);
                Request.AddParams("@polish_pcs", pClsProperty.polish_pcs, DbType.Int32);
                Request.AddParams("@diff_pcs", pClsProperty.diff_pcs, DbType.Int32);
                Request.AddParams("@diff_per", pClsProperty.diff_per, DbType.Decimal);
                Request.AddParams("@r_ghat", pClsProperty.r_ghat, DbType.Decimal);
                Request.AddParams("@b_ghat", pClsProperty.b_ghat, DbType.Decimal);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Costing_Manual_Save;
                Request.CommandType = CommandType.StoredProcedure;

                IntRes = Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IntRes;
        }

        public DataTable GetCostingManualStock(MFGCostingManualProperty PClsProperty)
        {
            DataTable DTabVal = new DataTable();
            Request Request = new Request();

            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", PClsProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@from_date", PClsProperty.from_date, DbType.String);
            Request.AddParams("@to_date", PClsProperty.to_date, DbType.String);
            Request.AddParams("@kapan_id", PClsProperty.kapan_id, DbType.Int32);

            Request.CommandText = BLL.TPV.SProc.MFG_Costing_Manual_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, DTabVal, Request);

            return DTabVal;
        }
        public int Delete(MFGCostingManualProperty pClsProperty)
        {
            int IntRes = 0;
            try
            {
                Request Request = new Request();

                Request.AddParams("@cost_id", pClsProperty.cost_id, DbType.Int64);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Costing_Manual_Delete;
                Request.CommandType = CommandType.StoredProcedure;

                IntRes = Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IntRes;
        }
    }
}
