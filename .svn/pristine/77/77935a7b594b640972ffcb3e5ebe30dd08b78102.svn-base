using BLL.PropertyClasses.Transaction.MFG;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction.MFG
{
    public class MultiEmployeeReturn
    {
        InterfaceLayer Ope = new InterfaceLayer();
        BLL.Validation Val = new BLL.Validation();

        public MFGProcessWeightLossRecieve_Property Save(MFGProcessWeightLossRecieve_Property pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@recieve_id", pClsProperty.recieve_id, DbType.Int32);
                Request.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int64);
                Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int32);
                Request.AddParams("@rough_cut_id", pClsProperty.rough_cut_id, DbType.Int32);
                Request.AddParams("@recieve_date", pClsProperty.receive_date, DbType.Date);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
                Request.AddParams("@issue_id", pClsProperty.issue_id, DbType.Int64);
                Request.AddParams("@manager_id", pClsProperty.manager_id, DbType.Int32);
                Request.AddParams("@employee_id", pClsProperty.employee_id, DbType.Int32);
                Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int32);
                Request.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int32);
                Request.AddParams("@machine_id", pClsProperty.machine_id, DbType.Int32);
                Request.AddParams("@pcs", pClsProperty.pcs, DbType.Int32);
                Request.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
                Request.AddParams("@loss_pcs", pClsProperty.loss_pcs, DbType.Int32);
                Request.AddParams("@loss_carat", pClsProperty.loss_carat, DbType.Decimal);
                Request.AddParams("@lost_pcs", pClsProperty.lost_pcs, DbType.Int32);
                Request.AddParams("@lost_carat", pClsProperty.lost_carat, DbType.Decimal);
                Request.AddParams("@rejection_pcs", pClsProperty.rejection_pcs, DbType.Int32);
                Request.AddParams("@rejection_carat", pClsProperty.rejection_carat, DbType.Decimal);
                Request.AddParams("@rr_pcs", pClsProperty.rr_pcs, DbType.Int32);
                Request.AddParams("@rr_carat", pClsProperty.rr_carat, DbType.Decimal);
                Request.AddParams("@is_outstanding", pClsProperty.is_outstanding, DbType.Int32);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);
                Request.AddParams("@receive_union_id", pClsProperty.receive_union_id, DbType.Int64);
                Request.AddParams("@history_union_id", pClsProperty.history_union_id, DbType.Int64);
                Request.AddParams("@union_id", pClsProperty.union_id, DbType.Int64);
                Request.AddParams("@resoing_pcs", pClsProperty.resoing_pcs, DbType.Int32);
                Request.AddParams("@resoing_carat", pClsProperty.resoing_carat, DbType.Decimal);
                Request.AddParams("@breakage_pcs", pClsProperty.braking_pcs, DbType.Int32);
                Request.AddParams("@breakage_carat", pClsProperty.braking_carat, DbType.Decimal);
                Request.AddParams("@carat_plus", pClsProperty.carat_plus, DbType.Decimal);
                Request.AddParams("@final_weight", pClsProperty.final_weight, DbType.Decimal);
                Request.AddParams("@total_flag", pClsProperty.total_flag, DbType.Int32);
                Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);
                Request.AddParams("@boiling_lot_id", pClsProperty.boiling_lot_id, DbType.Int64);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_MultiEmployee_Return_Save;
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
                        pClsProperty.receive_union_id = Val.ToInt64(p_dtbProcessUnionId.Rows[0][0]);
                        pClsProperty.history_union_id = Val.ToInt64(p_dtbProcessUnionId.Rows[0][1]);
                        pClsProperty.union_id = Val.ToInt64(p_dtbProcessUnionId.Rows[0][2]);
                        pClsProperty.lot_srno = Val.ToInt64(p_dtbProcessUnionId.Rows[0][3]);
                    }
                }
                else
                {
                    pClsProperty.receive_union_id = 0;
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
        public Int64 FindMaxBoilingLotID()
        {
            Int64 BoilingLotID = 0;
            BoilingLotID = Ope.FindNewIDInt64(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "MFG_TRN_Stock", "MAX(boiling_id)", "");
            return BoilingLotID;
        }
    }
}
