using BLL.PropertyClasses.Transaction.MFG;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction.MFG
{
    public class MFGFactoryMix
    {
        InterfaceLayer Ope = new InterfaceLayer();
        BLL.Validation Val = new BLL.Validation();

        public MFGFactoryMix_Property Save(MFGFactoryMix_Property pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@fac_main_lot_id", pClsProperty.fac_main_lot_id, DbType.Int64);
                Request.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int64);
                Request.AddParams("@union_id", pClsProperty.union_id, DbType.Int64);
                Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int32);
                Request.AddParams("@rough_cut_id", pClsProperty.rough_cut_id, DbType.Int32);
                Request.AddParams("@fac_mix_date", pClsProperty.Fac_Mix_date, DbType.Date);
                Request.AddParams("@company_id", pClsProperty.from_company_id, DbType.Int32);
                Request.AddParams("@branch_id", pClsProperty.from_branch_id, DbType.Int32);
                Request.AddParams("@location_id", pClsProperty.from_location_id, DbType.Int32);
                Request.AddParams("@department_id", pClsProperty.from_department_id, DbType.Int32);
                Request.AddParams("@to_company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@to_branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@to_location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@to_department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
                Request.AddParams("@manager_id", pClsProperty.manager_id, DbType.Int32);
                Request.AddParams("@to_manager_id", pClsProperty.to_manager_id, DbType.Int32);
                Request.AddParams("@employee_id", pClsProperty.employee_id, DbType.Int32);
                Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int32);
                Request.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int32);
                Request.AddParams("@rough_sieve_id", pClsProperty.rough_sieve_id, DbType.Int32);
                Request.AddParams("@purity_id", pClsProperty.purity_id, DbType.Int32);
                Request.AddParams("@quality_id", pClsProperty.rough_quality_id, DbType.Int32);
                Request.AddParams("@rough_clarity_id", pClsProperty.rough_clarity_id, DbType.Int32);
                Request.AddParams("@party_id", pClsProperty.party_id, DbType.Int32);
                Request.AddParams("@total_pcs", pClsProperty.total_pcs, DbType.Int32);
                Request.AddParams("@total_carat", pClsProperty.total_carat, DbType.Decimal);
                Request.AddParams("@total_breakage_pcs", pClsProperty.total_breakage_pcs, DbType.Int32);
                Request.AddParams("@total_breakage_carat", pClsProperty.total_breakage_carat, DbType.Decimal);
                Request.AddParams("@total_loss_carat", pClsProperty.total_loss_carat, DbType.Decimal);
                Request.AddParams("@pcs", pClsProperty.pcs, DbType.Int32);
                Request.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
                Request.AddParams("@breakage_pcs", pClsProperty.breakage_pcs, DbType.Int32);
                Request.AddParams("@breakage_carat", pClsProperty.breakage_carat, DbType.Decimal);
                Request.AddParams("@loss_carat", pClsProperty.loss_carat, DbType.Decimal);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
                Request.AddParams("@prediction_id", pClsProperty.prediction_id, DbType.Int64);

                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);

                Request.AddParams("@history_union_id", pClsProperty.history_union_id, DbType.Int64);
                Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Factory_Mix_Save;
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
                        pClsProperty.history_union_id = Val.ToInt64(p_dtbProcessJangedRecId.Rows[0][0]);
                        pClsProperty.union_id = Val.ToInt64(p_dtbProcessJangedRecId.Rows[0][1]);
                        pClsProperty.lot_srno = Val.ToInt64(p_dtbProcessJangedRecId.Rows[0][2]);
                    }
                }
                else
                {
                    pClsProperty.history_union_id = 0;
                    pClsProperty.union_id = 0;
                    pClsProperty.lot_srno = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }

        public DataTable Stock_GetData(Int64 LotId, int Fac_Main_LotID)
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
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
    }
}
