using BLL.PropertyClasses.Transaction.MFG;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction.MFG
{
    public class MFGJangedReceive
    {
        InterfaceLayer Ope = new InterfaceLayer();
        BLL.Validation Val = new BLL.Validation();

        public MFGJangedReceive_Property Save(MFGJangedReceive_Property pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@janged_no", pClsProperty.janged_no, DbType.Int32);
                Request.AddParams("@issue_id", pClsProperty.Issue_id, DbType.Int32);
                Request.AddParams("@prev_janged_no", pClsProperty.previous_janged_no, DbType.Int64);
                Request.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int32);
                Request.AddParams("@union_id", pClsProperty.union_id, DbType.Int64);
                Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int32);
                Request.AddParams("@rough_cut_id", pClsProperty.rough_cut_id, DbType.Int32);
                Request.AddParams("@janged_date", pClsProperty.janged_date, DbType.Date);
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
                Request.AddParams("@pcs", pClsProperty.pcs, DbType.Int32);
                Request.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
                Request.AddParams("@prev_janged_id", pClsProperty.previous_janged_id, DbType.Int64);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);
                Request.AddParams("@is_outside", pClsProperty.is_outside, DbType.Int32);
                Request.AddParams("@type", "R", DbType.String);
                Request.AddParams("@receive_union_id", pClsProperty.receive_union_id, DbType.Int64);
                Request.AddParams("@janged_union_id", pClsProperty.janged_union_id, DbType.Int64);
                Request.AddParams("@dept_union_id", pClsProperty.dept_union_id, DbType.Int64);
                Request.AddParams("@loss_carat", pClsProperty.loss_carat, DbType.Decimal);
                Request.AddParams("@carat_plus", pClsProperty.carat_plus, DbType.Decimal);
                Request.AddParams("@loss_count", pClsProperty.loss_count, DbType.Int32);
                Request.AddParams("@history_union_id", pClsProperty.history_union_id, DbType.Int64);
                Request.AddParams("@janged_srno", pClsProperty.janged_srno, DbType.Int64);
                Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Janged_Receive_Save;
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
                        pClsProperty.union_id = Val.ToInt64(p_dtbProcessJangedRecId.Rows[0][0]);
                        pClsProperty.receive_union_id = Val.ToInt64(p_dtbProcessJangedRecId.Rows[0][1]);
                        pClsProperty.janged_union_id = Val.ToInt64(p_dtbProcessJangedRecId.Rows[0][2]);
                        pClsProperty.dept_union_id = Val.ToInt64(p_dtbProcessJangedRecId.Rows[0][3]);
                        pClsProperty.janged_no = Val.ToInt64(p_dtbProcessJangedRecId.Rows[0][4]);
                        pClsProperty.history_union_id = Val.ToInt64(p_dtbProcessJangedRecId.Rows[0][5]);
                        pClsProperty.janged_srno = Val.ToInt64(p_dtbProcessJangedRecId.Rows[0][6]);
                        pClsProperty.lot_srno = Val.ToInt64(p_dtbProcessJangedRecId.Rows[0][7]);
                    }
                }
                else
                {
                    pClsProperty.union_id = 0;
                    pClsProperty.issue_union_id = 0;
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

        public DataTable Janged_Bal_GetData(Int64 Janged_No)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Janged_issue_BalGetdata;

            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@janged_no", Janged_No, DbType.Int64);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public int Janged_ID_GetData(Int64 Janged_No)
        {
            int Janged_Id = 0;
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Janged_issue_IDGetdata;

            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@janged_no", Janged_No, DbType.Int64);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            if (DTab.Rows.Count > 0)
            {
                Janged_Id = Val.ToInt(DTab.Rows[0]["janged_id"]);
            }

            return Janged_Id;
        }
    }
}
