using BLL.PropertyClasses.Transaction;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction
{
    public class MFGMixSplit
    {
        InterfaceLayer Ope = new InterfaceLayer();
        BLL.Validation Val = new BLL.Validation();

        public MFGMixSplitProperty Save_MFGMixSplit(MFGMixSplitProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();
                Request.AddParams("@sr_no", pClsProperty.sr_no, DbType.Int32);
                Request.AddParams("@receive_date", pClsProperty.receive_date, DbType.Date);
                Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int64);
                Request.AddParams("@from_lot_id", pClsProperty.from_lot_id, DbType.Int64);
                Request.AddParams("@to_lot_id", pClsProperty.to_lot_id, DbType.Int64);
                Request.AddParams("@rough_cut_id", pClsProperty.rough_cut_id, DbType.Int64);
                Request.AddParams("@union_id", pClsProperty.union_id, DbType.Int64);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
                Request.AddParams("@manager_id", pClsProperty.manager_id, DbType.Int32);
                Request.AddParams("@employee_id", pClsProperty.employee_id, DbType.Int32);
                Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int32);
                Request.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int32);
                Request.AddParams("@rough_sieve_id", pClsProperty.rough_sieve_id, DbType.Int32);
                Request.AddParams("@purity_id", pClsProperty.purity_id, DbType.Int32);
                Request.AddParams("@rough_clarity_id", pClsProperty.rough_clarity_id, DbType.Int32);
                Request.AddParams("@quality_id", pClsProperty.quality_id, DbType.Int64);
                Request.AddParams("@k_carat", pClsProperty.k_carat, DbType.Decimal);
                Request.AddParams("@from_pcs", pClsProperty.from_pcs, DbType.Int32);
                Request.AddParams("@from_carat", pClsProperty.from_carat, DbType.Decimal);
                Request.AddParams("@from_rr_pcs", pClsProperty.from_rr_pcs, DbType.Int32);
                Request.AddParams("@from_rr_carat", pClsProperty.from_rr_carat, DbType.Decimal);
                Request.AddParams("@from_rejection_pcs", pClsProperty.from_rejection_pcs, DbType.Int32);
                Request.AddParams("@from_rejection_carat", pClsProperty.from_rejection_carat, DbType.Decimal);
                Request.AddParams("@to_rr_pcs", pClsProperty.to_rr_pcs, DbType.Int32);
                Request.AddParams("@to_rr_carat", pClsProperty.to_rr_carat, DbType.Decimal);
                Request.AddParams("@to_rejection_pcs", pClsProperty.to_rejection_pcs, DbType.Int32);
                Request.AddParams("@to_rejection_carat", pClsProperty.to_rejection_carat, DbType.Decimal);
                Request.AddParams("@pcs", pClsProperty.pcs, DbType.Int32);
                Request.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
                Request.AddParams("@flag", pClsProperty.flag, DbType.Int32);
                Request.AddParams("@in_mixgrid", pClsProperty.in_mixgrid, DbType.Int32);
                Request.AddParams("@lotting_department_id", pClsProperty.lotting_department_id, DbType.Int32);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);
                Request.AddParams("@mix_union_id", pClsProperty.mix_union_id, DbType.Int64);
                Request.AddParams("@receive_union_id", pClsProperty.receive_union_id, DbType.Int64);
                Request.AddParams("@issue_union_id", pClsProperty.issue_union_id, DbType.Int64);
                Request.AddParams("@history_union_id", pClsProperty.history_union_id, DbType.Int64);
                Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);
                Request.AddParams("@transaction_type", pClsProperty.transaction_type, DbType.String);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Lot_Mix_Split_Save;
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
                        pClsProperty.history_union_id = Val.ToInt64(p_dtbProcessRecId.Rows[0][3]);
                        pClsProperty.lot_id = Val.ToInt64(p_dtbProcessRecId.Rows[0][4]);
                        pClsProperty.lot_srno = Val.ToInt64(p_dtbProcessRecId.Rows[0][5]);
                    }
                }
                else
                {
                    pClsProperty.union_id = 0;
                    pClsProperty.receive_union_id = 0;
                    pClsProperty.issue_union_id = 0;
                    pClsProperty.history_union_id = 0;
                    pClsProperty.lot_id = 0;
                    pClsProperty.lot_srno = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }


        public MFGMixSplitProperty Update_RoughMFGMixSplit(MFGMixSplitProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();
                Request.AddParams("@sr_no", pClsProperty.sr_no, DbType.Int32);
                Request.AddParams("@receive_date", pClsProperty.receive_date, DbType.Date);
                Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int64);
                Request.AddParams("@from_lot_id", pClsProperty.from_lot_id, DbType.Int64);
                Request.AddParams("@to_lot_id", pClsProperty.to_lot_id, DbType.Int64);
                Request.AddParams("@rough_cut_id", pClsProperty.rough_cut_id, DbType.Int64);
                Request.AddParams("@union_id", pClsProperty.union_id, DbType.Int64);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
                Request.AddParams("@manager_id", pClsProperty.manager_id, DbType.Int32);
                Request.AddParams("@employee_id", pClsProperty.employee_id, DbType.Int32);
                Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int32);
                Request.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int32);
                Request.AddParams("@rough_sieve_id", pClsProperty.rough_sieve_id, DbType.Int32);
                Request.AddParams("@purity_id", pClsProperty.purity_id, DbType.Int32);
                Request.AddParams("@rough_clarity_id", pClsProperty.rough_clarity_id, DbType.Int32);
                Request.AddParams("@quality_id", pClsProperty.quality_id, DbType.Int64);
                Request.AddParams("@from_pcs", pClsProperty.from_pcs, DbType.Int32);
                Request.AddParams("@from_carat", pClsProperty.from_carat, DbType.Decimal);
                Request.AddParams("@from_rr_pcs", pClsProperty.from_rr_pcs, DbType.Int32);
                Request.AddParams("@from_rr_carat", pClsProperty.from_rr_carat, DbType.Decimal);
                Request.AddParams("@from_rejection_pcs", pClsProperty.from_rejection_pcs, DbType.Int32);
                Request.AddParams("@from_rejection_carat", pClsProperty.from_rejection_carat, DbType.Decimal);
                Request.AddParams("@to_rr_pcs", pClsProperty.to_rr_pcs, DbType.Int32);
                Request.AddParams("@to_rr_carat", pClsProperty.to_rr_carat, DbType.Decimal);
                Request.AddParams("@to_rejection_pcs", pClsProperty.to_rejection_pcs, DbType.Int32);
                Request.AddParams("@to_rejection_carat", pClsProperty.to_rejection_carat, DbType.Decimal);
                Request.AddParams("@pcs", pClsProperty.pcs, DbType.Int32);
                Request.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
                Request.AddParams("@flag", pClsProperty.flag, DbType.Int32);
                Request.AddParams("@in_mixgrid", pClsProperty.in_mixgrid, DbType.Int32);
                Request.AddParams("@lotting_department_id", pClsProperty.lotting_department_id, DbType.Int32);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);
                Request.AddParams("@mix_union_id", pClsProperty.mix_union_id, DbType.Int64);
                Request.AddParams("@receive_union_id", pClsProperty.receive_union_id, DbType.Int64);
                Request.AddParams("@issue_union_id", pClsProperty.issue_union_id, DbType.Int64);
                Request.AddParams("@history_union_id", pClsProperty.history_union_id, DbType.Int64);
                Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);
                Request.AddParams("@prediction_id", pClsProperty.prediction_id, DbType.Int64);
                Request.AddParams("@issue_id", pClsProperty.issue_id, DbType.Int64);
                Request.AddParams("@recieve_id", pClsProperty.recieve_id, DbType.Int64);
                Request.AddParams("@carat_plus", pClsProperty.plus_carat, DbType.Decimal);
                Request.AddParams("@loss_carat", pClsProperty.loss_carat, DbType.Decimal);
                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Rough_Mix_Split_Update;
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
                        pClsProperty.history_union_id = Val.ToInt64(p_dtbProcessRecId.Rows[0][3]);
                        pClsProperty.lot_id = Val.ToInt64(p_dtbProcessRecId.Rows[0][4]);
                        pClsProperty.lot_srno = Val.ToInt64(p_dtbProcessRecId.Rows[0][5]);
                    }
                }
                else
                {
                    pClsProperty.union_id = 0;
                    pClsProperty.receive_union_id = 0;
                    pClsProperty.issue_union_id = 0;
                    pClsProperty.history_union_id = 0;
                    pClsProperty.lot_id = 0;
                    pClsProperty.lot_srno = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }
        public MFGMixSplitProperty Update_MFGMixSplit(MFGMixSplitProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();
                Request.AddParams("@sr_no", pClsProperty.sr_no, DbType.Int32);
                Request.AddParams("@receive_date", pClsProperty.receive_date, DbType.Date);
                Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int64);
                Request.AddParams("@from_lot_id", pClsProperty.from_lot_id, DbType.Int64);
                Request.AddParams("@to_lot_id", pClsProperty.to_lot_id, DbType.Int64);
                Request.AddParams("@rough_cut_id", pClsProperty.rough_cut_id, DbType.Int64);
                Request.AddParams("@union_id", pClsProperty.union_id, DbType.Int64);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
                Request.AddParams("@manager_id", pClsProperty.manager_id, DbType.Int32);
                Request.AddParams("@employee_id", pClsProperty.employee_id, DbType.Int32);
                Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int32);
                Request.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int32);
                Request.AddParams("@rough_sieve_id", pClsProperty.rough_sieve_id, DbType.Int32);
                Request.AddParams("@purity_id", pClsProperty.purity_id, DbType.Int32);
                Request.AddParams("@rough_clarity_id", pClsProperty.rough_clarity_id, DbType.Int32);
                Request.AddParams("@quality_id", pClsProperty.quality_id, DbType.Int64);
                Request.AddParams("@from_pcs", pClsProperty.from_pcs, DbType.Int32);
                Request.AddParams("@from_carat", pClsProperty.from_carat, DbType.Decimal);
                Request.AddParams("@from_rr_pcs", pClsProperty.from_rr_pcs, DbType.Int32);
                Request.AddParams("@from_rr_carat", pClsProperty.from_rr_carat, DbType.Decimal);
                Request.AddParams("@from_rejection_pcs", pClsProperty.from_rejection_pcs, DbType.Int32);
                Request.AddParams("@from_rejection_carat", pClsProperty.from_rejection_carat, DbType.Decimal);
                Request.AddParams("@to_rr_pcs", pClsProperty.to_rr_pcs, DbType.Int32);
                Request.AddParams("@to_rr_carat", pClsProperty.to_rr_carat, DbType.Decimal);
                Request.AddParams("@to_rejection_pcs", pClsProperty.to_rejection_pcs, DbType.Int32);
                Request.AddParams("@to_rejection_carat", pClsProperty.to_rejection_carat, DbType.Decimal);
                Request.AddParams("@pcs", pClsProperty.pcs, DbType.Int32);
                Request.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
                Request.AddParams("@flag", pClsProperty.flag, DbType.Int32);
                Request.AddParams("@in_mixgrid", pClsProperty.in_mixgrid, DbType.Int32);
                Request.AddParams("@lotting_department_id", pClsProperty.lotting_department_id, DbType.Int32);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);
                Request.AddParams("@mix_union_id", pClsProperty.mix_union_id, DbType.Int64);
                Request.AddParams("@receive_union_id", pClsProperty.receive_union_id, DbType.Int64);
                Request.AddParams("@issue_union_id", pClsProperty.issue_union_id, DbType.Int64);
                Request.AddParams("@history_union_id", pClsProperty.history_union_id, DbType.Int64);
                Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);
                Request.AddParams("@prediction_id", pClsProperty.prediction_id, DbType.Int64);
                Request.AddParams("@issue_id", pClsProperty.issue_id, DbType.Int64);
                Request.AddParams("@recieve_id", pClsProperty.recieve_id, DbType.Int64);
                Request.AddParams("@carat_plus", pClsProperty.plus_carat, DbType.Decimal);
                Request.AddParams("@loss_carat", pClsProperty.loss_carat, DbType.Decimal);
                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Lot_Mix_Split_Update;
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
                        pClsProperty.history_union_id = Val.ToInt64(p_dtbProcessRecId.Rows[0][3]);
                        pClsProperty.lot_id = Val.ToInt64(p_dtbProcessRecId.Rows[0][4]);
                        pClsProperty.lot_srno = Val.ToInt64(p_dtbProcessRecId.Rows[0][5]);
                    }
                }
                else
                {
                    pClsProperty.union_id = 0;
                    pClsProperty.receive_union_id = 0;
                    pClsProperty.issue_union_id = 0;
                    pClsProperty.history_union_id = 0;
                    pClsProperty.lot_id = 0;
                    pClsProperty.lot_srno = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }
        public MFGMixSplitProperty Save_MFGMixSplitOneToMany(MFGMixSplitProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@receive_date", pClsProperty.receive_date, DbType.Date);
                Request.AddParams("@from_cut_id", pClsProperty.from_cut_id, DbType.Int64);
                Request.AddParams("@from_lot_id", pClsProperty.from_lot_id, DbType.Int64);
                Request.AddParams("@to_lot_id", pClsProperty.to_lot_id, DbType.Int64);
                Request.AddParams("@from_kapan_id", pClsProperty.from_kapan_id, DbType.Int64);
                Request.AddParams("@k_carat", pClsProperty.k_carat, DbType.Decimal);
                Request.AddParams("@from_pcs", pClsProperty.from_pcs, DbType.Int32);
                Request.AddParams("@from_carat", pClsProperty.from_carat, DbType.Decimal);
                Request.AddParams("@from_rr_pcs", pClsProperty.from_rr_pcs, DbType.Int32);
                Request.AddParams("@from_rr_carat", pClsProperty.from_rr_carat, DbType.Decimal);
                Request.AddParams("@from_rejection_pcs", pClsProperty.from_rejection_pcs, DbType.Int32);
                Request.AddParams("@from_rejection_carat", pClsProperty.from_rejection_carat, DbType.Decimal);
                Request.AddParams("@to_rr_pcs", pClsProperty.to_rr_pcs, DbType.Int32);
                Request.AddParams("@to_rr_carat", pClsProperty.to_rr_carat, DbType.Decimal);
                Request.AddParams("@to_rejection_pcs", pClsProperty.to_rejection_pcs, DbType.Int32);
                Request.AddParams("@to_rejection_carat", pClsProperty.to_rejection_carat, DbType.Decimal);
                Request.AddParams("@to_cut_id", pClsProperty.to_cut_id, DbType.Int64);
                Request.AddParams("@to_kapan_id", pClsProperty.to_kapan_id, DbType.Int64);
                Request.AddParams("@to_pcs", pClsProperty.to_pcs, DbType.Int32);
                Request.AddParams("@to_carat", pClsProperty.to_carat, DbType.Decimal);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);
                Request.AddParams("@manager_id", pClsProperty.manager_id, DbType.Int32);
                Request.AddParams("@employee_id", pClsProperty.employee_id, DbType.Int32);
                Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int32);
                Request.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int32);
                Request.AddParams("@rough_sieve_id", pClsProperty.rough_sieve_id, DbType.Int32);
                Request.AddParams("@purity_id", pClsProperty.purity_id, DbType.Int32);
                Request.AddParams("@rough_clarity_id", pClsProperty.rough_clarity_id, DbType.Int32);
                Request.AddParams("@quality_id", pClsProperty.quality_id, DbType.Int64);
                Request.AddParams("@loss_carat", pClsProperty.loss_carat, DbType.Decimal);
                Request.AddParams("@plus_carat", pClsProperty.plus_carat, DbType.Decimal);
                Request.AddParams("@from_rate", pClsProperty.from_rate, DbType.Decimal);
                Request.AddParams("@from_amount", pClsProperty.from_amount, DbType.Decimal);
                Request.AddParams("@union_id", pClsProperty.union_id, DbType.Int64);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@mix_union_id", pClsProperty.mix_union_id, DbType.Int64);
                Request.AddParams("@receive_union_id", pClsProperty.receive_union_id, DbType.Int64);
                Request.AddParams("@issue_union_id", pClsProperty.issue_union_id, DbType.Int64);
                Request.AddParams("@history_union_id", pClsProperty.history_union_id, DbType.Int64);
                Request.AddParams("@sr_no", pClsProperty.sr_no, DbType.Int32);
                Request.AddParams("@flag", pClsProperty.flag, DbType.Int32);
                Request.AddParams("@in_mixgrid", pClsProperty.in_mixgrid, DbType.Int32);
                Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);
                Request.AddParams("@lotting_department_id", pClsProperty.lotting_department_id, DbType.Int64);
                Request.AddParams("@is_repeat", pClsProperty.is_repeat, DbType.Int32);
                Request.AddParams("@transaction_type", pClsProperty.transaction_type, DbType.String);
                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Lotting_Mix_Split_Save;
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
                        pClsProperty.history_union_id = Val.ToInt32(p_dtbProcessRecId.Rows[0][3]);
                        pClsProperty.lot_id = Val.ToInt32(p_dtbProcessRecId.Rows[0][4]);
                        pClsProperty.mix_union_id = Val.ToInt32(p_dtbProcessRecId.Rows[0][5]);
                        pClsProperty.lot_srno = Val.ToInt32(p_dtbProcessRecId.Rows[0][6]);
                    }
                }
                else
                {
                    pClsProperty.union_id = 0;
                    pClsProperty.receive_union_id = 0;
                    pClsProperty.issue_union_id = 0;
                    pClsProperty.history_union_id = 0;
                    pClsProperty.lot_id = 0;
                    pClsProperty.mix_union_id = 0;
                    pClsProperty.lot_srno = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }

        public MFGMixSplitProperty Save_MFGMixSplitManyToOne(MFGMixSplitProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@receive_date", pClsProperty.receive_date, DbType.Date);
                Request.AddParams("@from_cut_id", pClsProperty.from_cut_id, DbType.Int64);
                Request.AddParams("@from_lot_id", pClsProperty.from_lot_id, DbType.Int64);
                Request.AddParams("@from_kapan_id", pClsProperty.from_kapan_id, DbType.Int64);
                Request.AddParams("@from_pcs", pClsProperty.from_pcs, DbType.Int32);
                Request.AddParams("@from_carat", pClsProperty.from_carat, DbType.Decimal);
                Request.AddParams("@from_rr_pcs", pClsProperty.from_rr_pcs, DbType.Int32);
                Request.AddParams("@from_rr_carat", pClsProperty.from_rr_carat, DbType.Decimal);
                Request.AddParams("@from_rejection_pcs", pClsProperty.from_rejection_pcs, DbType.Int32);
                Request.AddParams("@from_rejection_carat", pClsProperty.from_rejection_carat, DbType.Decimal);
                Request.AddParams("@to_lot_id", pClsProperty.to_lot_id, DbType.Int64);
                Request.AddParams("@to_cut_id", pClsProperty.to_cut_id, DbType.Int64);
                Request.AddParams("@to_kapan_id", pClsProperty.to_kapan_id, DbType.Int64);
                Request.AddParams("@k_carat", pClsProperty.k_carat, DbType.Decimal);
                Request.AddParams("@to_pcs", pClsProperty.to_pcs, DbType.Int32);
                Request.AddParams("@to_carat", pClsProperty.to_carat, DbType.Decimal);
                Request.AddParams("@to_rr_pcs", pClsProperty.to_rr_pcs, DbType.Int32);
                Request.AddParams("@to_rr_carat", pClsProperty.to_rr_carat, DbType.Decimal);
                Request.AddParams("@to_rejection_pcs", pClsProperty.to_rejection_pcs, DbType.Int32);
                Request.AddParams("@to_rejection_carat", pClsProperty.to_rejection_carat, DbType.Decimal);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);
                Request.AddParams("@manager_id", pClsProperty.manager_id, DbType.Int32);
                Request.AddParams("@employee_id", pClsProperty.employee_id, DbType.Int32);
                Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int32);
                Request.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int32);
                Request.AddParams("@rough_sieve_id", pClsProperty.rough_sieve_id, DbType.Int32);
                Request.AddParams("@purity_id", pClsProperty.purity_id, DbType.Int32);
                Request.AddParams("@rough_clarity_id", pClsProperty.rough_clarity_id, DbType.Int32);
                Request.AddParams("@quality_id", pClsProperty.quality_id, DbType.Int64);
                Request.AddParams("@loss_carat", pClsProperty.loss_carat, DbType.Decimal);
                Request.AddParams("@plus_carat", pClsProperty.plus_carat, DbType.Decimal);
                Request.AddParams("@from_rate", pClsProperty.from_rate, DbType.Decimal);
                Request.AddParams("@from_amount", pClsProperty.from_amount, DbType.Decimal);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
                Request.AddParams("@union_id", pClsProperty.union_id, DbType.Int64);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@mix_union_id", pClsProperty.mix_union_id, DbType.Int64);
                Request.AddParams("@receive_union_id", pClsProperty.receive_union_id, DbType.Int64);
                Request.AddParams("@issue_union_id", pClsProperty.issue_union_id, DbType.Int64);
                Request.AddParams("@history_union_id", pClsProperty.history_union_id, DbType.Int64);
                Request.AddParams("@sr_no", pClsProperty.sr_no, DbType.Int32);
                Request.AddParams("@lotting_department_id", pClsProperty.lotting_department_id, DbType.Int32);
                Request.AddParams("@flag", pClsProperty.flag, DbType.Int32);
                Request.AddParams("@in_mixgrid", pClsProperty.in_mixgrid, DbType.Int32);
                Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);
                Request.AddParams("@is_repeat", pClsProperty.is_repeat, DbType.Int32);
                Request.AddParams("@transaction_type", pClsProperty.transaction_type, DbType.String);
                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Lotting_Many_To_One_Save;
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
                        pClsProperty.history_union_id = Val.ToInt32(p_dtbProcessRecId.Rows[0][3]);
                        pClsProperty.to_lot_id = Val.ToInt32(p_dtbProcessRecId.Rows[0][4]);
                        pClsProperty.mix_union_id = Val.ToInt32(p_dtbProcessRecId.Rows[0][5]);
                        pClsProperty.sr_no = Val.ToInt32(p_dtbProcessRecId.Rows[0][6]);
                        pClsProperty.lot_srno = Val.ToInt32(p_dtbProcessRecId.Rows[0][7]);
                    }
                }
                else
                {
                    pClsProperty.union_id = 0;
                    pClsProperty.receive_union_id = 0;
                    pClsProperty.issue_union_id = 0;
                    pClsProperty.history_union_id = 0;
                    pClsProperty.lot_id = 0;
                    pClsProperty.mix_union_id = 0;
                    pClsProperty.sr_no = 0;
                    pClsProperty.lot_srno = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }
        public MFGMixSplitProperty Save_MFGStockMixData(MFGMixSplitProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@receive_date", pClsProperty.receive_date, DbType.Date);
                Request.AddParams("@from_lot_id", pClsProperty.from_lot_id, DbType.Int64);
                Request.AddParams("@to_lot_id", pClsProperty.new_lot_id, DbType.Int64);
                Request.AddParams("@from_pcs", pClsProperty.from_pcs, DbType.Int32);
                Request.AddParams("@from_carat", pClsProperty.from_carat, DbType.Decimal);
                Request.AddParams("@prediction_id", pClsProperty.prediction_id, DbType.Int64);
                Request.AddParams("@count", pClsProperty.count, DbType.Int32);
                Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int64);
                Request.AddParams("@rough_cut_id", pClsProperty.rough_cut_id, DbType.Int64);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
                Request.AddParams("@manager_id", pClsProperty.manager_id, DbType.Int32);
                Request.AddParams("@employee_id", pClsProperty.employee_id, DbType.Int32);
                Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int32);
                Request.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int32);
                Request.AddParams("@rough_sieve_id", pClsProperty.rough_sieve_id, DbType.Int32);
                Request.AddParams("@purity_id", pClsProperty.purity_id, DbType.Int32);
                Request.AddParams("@rough_clarity_id", pClsProperty.rough_clarity_id, DbType.Int32);
                Request.AddParams("@quality_id", pClsProperty.quality_id, DbType.Int64);
                Request.AddParams("@pcs", pClsProperty.pcs, DbType.Int32);
                Request.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
                Request.AddParams("@loss_carat", pClsProperty.loss_carat, DbType.Decimal);
                Request.AddParams("@carat_plus", pClsProperty.plus_carat, DbType.Decimal);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);
                Request.AddParams("@mix_union_id", pClsProperty.mix_union_id, DbType.Int64);
                Request.AddParams("@union_id", pClsProperty.union_id, DbType.Int64);
                Request.AddParams("@receive_union_id", pClsProperty.receive_union_id, DbType.Int64);
                Request.AddParams("@issue_union_id", pClsProperty.issue_union_id, DbType.Int64);
                Request.AddParams("@history_union_id", pClsProperty.history_union_id, DbType.Int64);
                Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);
                Request.AddParams("@transaction_type", pClsProperty.transaction_type, DbType.String);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Stock_Mix_Save;
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
                        pClsProperty.new_lot_id = Val.ToInt64(p_dtbProcessRecId.Rows[0][0]);
                        pClsProperty.mix_union_id = Val.ToInt64(p_dtbProcessRecId.Rows[0][1]);
                        pClsProperty.union_id = Val.ToInt64(p_dtbProcessRecId.Rows[0][2]);
                        pClsProperty.receive_union_id = Val.ToInt64(p_dtbProcessRecId.Rows[0][3]);
                        pClsProperty.issue_union_id = Val.ToInt64(p_dtbProcessRecId.Rows[0][4]);
                        pClsProperty.history_union_id = Val.ToInt32(p_dtbProcessRecId.Rows[0][5]);
                        pClsProperty.lot_srno = Val.ToInt64(p_dtbProcessRecId.Rows[0][6]);
                    }
                }
                else
                {
                    pClsProperty.new_lot_id = 0;
                    pClsProperty.mix_union_id = 0;
                    pClsProperty.union_id = 0;
                    pClsProperty.receive_union_id = 0;
                    pClsProperty.issue_union_id = 0;
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

        public MFGMixSplitProperty Save_MFGKapanMixData(MFGMixSplitProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@from_pcs", pClsProperty.from_pcs, DbType.Int32);
                Request.AddParams("@from_carat", pClsProperty.from_carat, DbType.Decimal);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
                Request.AddParams("@from_lot_id", pClsProperty.from_lot_id, DbType.Int64);
                Request.AddParams("@from_kapan_id", pClsProperty.from_kapan_id, DbType.Int64);
                Request.AddParams("@from_cut_id", pClsProperty.from_cut_id, DbType.Int64);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);
                Request.AddParams("@to_kapan_id", pClsProperty.to_kapan_id, DbType.Int64);
                Request.AddParams("@to_cut_id", pClsProperty.to_cut_id, DbType.Int64);
                Request.AddParams("@receive_date", pClsProperty.receive_date, DbType.Date);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
                Request.AddParams("@manager_id", pClsProperty.manager_id, DbType.Int32);
                Request.AddParams("@employee_id", pClsProperty.employee_id, DbType.Int32);
                Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int32);
                Request.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int32);
                Request.AddParams("@rough_sieve_id", pClsProperty.rough_sieve_id, DbType.Int32);
                Request.AddParams("@purity_id", pClsProperty.purity_id, DbType.Int32);
                Request.AddParams("@rough_clarity_id", pClsProperty.rough_clarity_id, DbType.Int32);
                Request.AddParams("@quality_id", pClsProperty.quality_id, DbType.Int32);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@mix_union_id", pClsProperty.mix_union_id, DbType.Int64);
                Request.AddParams("@union_id", pClsProperty.union_id, DbType.Int64);
                Request.AddParams("@history_union_id", pClsProperty.history_union_id, DbType.Int64);
                Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Kapan_Mix_Save;
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
                        pClsProperty.new_lot_id = Val.ToInt64(p_dtbProcessRecId.Rows[0][0]);
                        pClsProperty.mix_union_id = Val.ToInt64(p_dtbProcessRecId.Rows[0][1]);
                        pClsProperty.union_id = Val.ToInt64(p_dtbProcessRecId.Rows[0][2]);
                        pClsProperty.history_union_id = Val.ToInt32(p_dtbProcessRecId.Rows[0][3]);
                        pClsProperty.lot_srno = Val.ToInt64(p_dtbProcessRecId.Rows[0][4]);
                    }
                }
                else
                {
                    pClsProperty.new_lot_id = 0;
                    pClsProperty.mix_union_id = 0;
                    pClsProperty.union_id = 0;
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

        public MFGMixSplitProperty Save_MFGStockLotting_MixData_Issue(MFGMixSplitProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@receive_date", pClsProperty.receive_date, DbType.Date);
                Request.AddParams("@from_lot_id", pClsProperty.from_lot_id, DbType.Int64);
                Request.AddParams("@from_pcs", pClsProperty.from_pcs, DbType.Int32);
                Request.AddParams("@from_carat", pClsProperty.from_carat, DbType.Decimal);
                Request.AddParams("@from_rr_pcs", pClsProperty.from_rr_pcs, DbType.Int32);
                Request.AddParams("@from_rr_carat", pClsProperty.from_rr_carat, DbType.Decimal);
                Request.AddParams("@from_rejection_pcs", pClsProperty.from_rejection_pcs, DbType.Int32);
                Request.AddParams("@from_rejection_carat", pClsProperty.from_rejection_carat, DbType.Decimal);
                Request.AddParams("@quality_id", pClsProperty.quality_id, DbType.Int64);
                Request.AddParams("@prediction_id", pClsProperty.prediction_id, DbType.Int64);
                Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int64);
                Request.AddParams("@rough_cut_id", pClsProperty.rough_cut_id, DbType.Int64);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
                Request.AddParams("@manager_id", pClsProperty.manager_id, DbType.Int32);
                Request.AddParams("@employee_id", pClsProperty.employee_id, DbType.Int32);
                Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int32);
                Request.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int32);
                Request.AddParams("@rough_sieve_id", pClsProperty.rough_sieve_id, DbType.Int32);
                Request.AddParams("@purity_id", pClsProperty.purity_id, DbType.Int32);
                Request.AddParams("@rough_clarity_id", pClsProperty.rough_clarity_id, DbType.Int32);
                Request.AddParams("@loss_carat", pClsProperty.loss_carat, DbType.Decimal);
                Request.AddParams("@carat_plus", pClsProperty.plus_carat, DbType.Decimal);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);
                Request.AddParams("@issue_union_id", pClsProperty.issue_union_id, DbType.Int64);
                Request.AddParams("@history_union_id", pClsProperty.history_union_id, DbType.Int64);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Lotting_Mix_Issue_Save;
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
                        pClsProperty.issue_union_id = Val.ToInt64(p_dtbProcessRecId.Rows[0][0]);
                        pClsProperty.history_union_id = Convert.ToInt32(p_dtbProcessRecId.Rows[0][1]);
                        pClsProperty.Issue_id = Convert.ToInt32(p_dtbProcessRecId.Rows[0][2]);
                    }
                }
                else
                {
                    pClsProperty.issue_union_id = 0;
                    pClsProperty.history_union_id = 0;
                    pClsProperty.Issue_id = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }
        public MFGMixSplitProperty Save_MFGStockLotting_MixData(MFGMixSplitProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@receive_date", pClsProperty.receive_date, DbType.Date);
                Request.AddParams("@from_lot_id", pClsProperty.from_lot_id, DbType.Int64);
                Request.AddParams("@to_lot_id", pClsProperty.new_lot_id, DbType.Int64);
                Request.AddParams("@from_pcs", pClsProperty.from_pcs, DbType.Int32);
                Request.AddParams("@from_carat", pClsProperty.from_carat, DbType.Decimal);
                Request.AddParams("@from_rr_pcs", pClsProperty.from_rr_pcs, DbType.Int32);
                Request.AddParams("@from_rr_carat", pClsProperty.from_rr_carat, DbType.Decimal);
                Request.AddParams("@from_rejection_pcs", pClsProperty.from_rejection_pcs, DbType.Int32);
                Request.AddParams("@from_rejection_carat", pClsProperty.from_rejection_carat, DbType.Decimal);
                Request.AddParams("@quality_id", pClsProperty.quality_id, DbType.Int64);
                Request.AddParams("@prediction_id", pClsProperty.prediction_id, DbType.Int64);
                Request.AddParams("@count", pClsProperty.count, DbType.Int32);
                Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int64);
                Request.AddParams("@rough_cut_id", pClsProperty.rough_cut_id, DbType.Int64);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
                Request.AddParams("@manager_id", pClsProperty.manager_id, DbType.Int32);
                Request.AddParams("@employee_id", pClsProperty.employee_id, DbType.Int32);
                Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int32);
                Request.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int32);
                Request.AddParams("@rough_sieve_id", pClsProperty.rough_sieve_id, DbType.Int32);
                Request.AddParams("@purity_id", pClsProperty.purity_id, DbType.Int32);
                Request.AddParams("@rough_clarity_id", pClsProperty.rough_clarity_id, DbType.Int32);
                Request.AddParams("@pcs", pClsProperty.pcs, DbType.Int32);
                Request.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
                Request.AddParams("@loss_carat", pClsProperty.loss_carat, DbType.Decimal);
                Request.AddParams("@carat_plus", pClsProperty.plus_carat, DbType.Decimal);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
                Request.AddParams("@is_repeat", pClsProperty.is_repeat, DbType.Int32);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);
                Request.AddParams("@mix_union_id", pClsProperty.mix_union_id, DbType.Int64);
                Request.AddParams("@union_id", pClsProperty.union_id, DbType.Int64);
                Request.AddParams("@receive_union_id", pClsProperty.receive_union_id, DbType.Int64);
                Request.AddParams("@issue_union_id", pClsProperty.issue_union_id, DbType.Int64);
                Request.AddParams("@history_union_id", pClsProperty.history_union_id, DbType.Int64);
                Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);
                Request.AddParams("@transaction_type", pClsProperty.transaction_type, DbType.String);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Lotting_Mix_Save;
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
                        pClsProperty.new_lot_id = Val.ToInt64(p_dtbProcessRecId.Rows[0][0]);
                        pClsProperty.mix_union_id = Val.ToInt64(p_dtbProcessRecId.Rows[0][1]);
                        pClsProperty.union_id = Val.ToInt64(p_dtbProcessRecId.Rows[0][2]);
                        pClsProperty.receive_union_id = Val.ToInt64(p_dtbProcessRecId.Rows[0][3]);
                        pClsProperty.issue_union_id = Val.ToInt64(p_dtbProcessRecId.Rows[0][4]);
                        pClsProperty.history_union_id = Val.ToInt32(p_dtbProcessRecId.Rows[0][5]);
                        pClsProperty.lot_srno = Val.ToInt32(p_dtbProcessRecId.Rows[0][6]);
                    }
                }
                else
                {
                    pClsProperty.new_lot_id = 0;
                    pClsProperty.mix_union_id = 0;
                    pClsProperty.union_id = 0;
                    pClsProperty.receive_union_id = 0;
                    pClsProperty.issue_union_id = 0;
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

        public DataTable Live_Stock_GetData(string Lot_id)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_LiveStock_GetData;

            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@lot_id", Lot_id, DbType.String);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }

        public DataTable Shine_Issue_GetData(string Lot_id)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_ShineIssue_GetData;

            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@lot_id", Lot_id, DbType.String);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }

        public int Update_LottingDepartment(MFGMixSplitProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            int IntRes = 0;
            try
            {
                Request Request = new Request();

                Request.AddParams("@to_lot_id", pClsProperty.to_lot_id, DbType.Int64);
                Request.AddParams("@lotting_department_id", pClsProperty.lotting_department_id, DbType.Int64);
                Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);
                Request.AddParams("@is_repeat", pClsProperty.is_repeat, DbType.Int32);
                Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int32);
                Request.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int32);
                Request.AddParams("@upd_flag", pClsProperty.upd_flag, DbType.Int32);
                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Lotting_Department_Update;
                Request.CommandType = CommandType.StoredProcedure;

                DataTable p_dtbProcessRecId = new DataTable();
                if (Conn != null)
                    IntRes = Conn.Inter1.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request, pEnum);
                else
                    IntRes = Conn.Inter1.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IntRes;
        }
        //public int Update_LottingProcess(MFGMixSplitProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        //{

        //    int IntRes = 0;
        //    try
        //    {
        //        Request Request = new Request();

        //        Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);
        //        Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int64);
        //        Request.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int64);
        //        Request.AddParams("@old_process_id", pClsProperty.old_process_id, DbType.Int64);
        //        Request.AddParams("@old_sub_process_id", pClsProperty.old_sub_process_id, DbType.Int64);
        //        Request.CommandText = "MFG_TRN_Lotting_Process_Update";
        //        Request.CommandType = CommandType.StoredProcedure;

        //        DataTable p_dtbProcessRecId = new DataTable();
        //        if (Conn != null)
        //            IntRes = Conn.Inter1.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request, pEnum);
        //        else
        //            IntRes = Conn.Inter1.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);


        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return IntRes;
        //}
        public DataTable Live_Stock_Split_GetData(Int64 Lot_id)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_LiveStock_Split_GetData;

            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@lot_id", Lot_id, DbType.Int64);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public int GetSrNo(int Cut_id, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            int Srno = 0;
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_CutWise_SrNo_GetData;

            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@cut_id", Cut_id, DbType.Int32);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Conn.Inter1.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, DTab, Request, pEnum);
            if (DTab.Rows.Count > 0)
            {
                Srno = Val.ToInt(DTab.Rows[0]["sr_no"]);
            }
            else
            {
                Srno = 0;
            }
            return Srno;
        }
        public int GetLotNO(Int64 Lot_id, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            int Srno = 0;
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_GetLotno_GetData;

            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@lot_id", Lot_id, DbType.Int64);
            Conn.Inter1.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, DTab, Request, pEnum);
            if (DTab.Rows.Count > 0)
            {
                Srno = Val.ToInt(DTab.Rows[0]["lot_no"]);
            }
            else
            {
                Srno = 0;
            }
            return Srno;
        }
        public int Update_Balance_Carat(string Lot_ID, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            int IntRes = 0;
            try
            {
                Request Request = new Request();
                Request.AddParams("@lot_id", Lot_ID, DbType.String);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Balance_Carat_Update;
                Request.CommandType = CommandType.StoredProcedure;
                if (Conn != null)
                    IntRes = Conn.Inter1.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request, pEnum);
                else
                    IntRes = Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IntRes;
        }

        public MFGMixSplitProperty Save_MFGLotSplit(MFGMixSplitProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@receive_date", pClsProperty.receive_date, DbType.Date);
                Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int64);
                Request.AddParams("@from_lot_id", pClsProperty.from_lot_id, DbType.Int64);
                Request.AddParams("@rough_cut_id", pClsProperty.rough_cut_id, DbType.Int64);
                Request.AddParams("@union_id", pClsProperty.union_id, DbType.Int64);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
                Request.AddParams("@rough_clarity_id", pClsProperty.rough_clarity_id, DbType.Int64);
                Request.AddParams("@rough_sieve_id", pClsProperty.rough_sieve_id, DbType.Int64);
                Request.AddParams("@rough_quality_id", pClsProperty.quality_id, DbType.Int64);
                Request.AddParams("@from_pcs", pClsProperty.from_pcs, DbType.Int32);
                Request.AddParams("@from_carat", pClsProperty.from_carat, DbType.Decimal);
                Request.AddParams("@pcs", pClsProperty.pcs, DbType.Int32);
                Request.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);
                Request.AddParams("@mix_union_id", pClsProperty.mix_union_id, DbType.Int64);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Lot_Split_Save;
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
                        pClsProperty.mix_union_id = Val.ToInt64(p_dtbProcessRecId.Rows[0][1]);
                    }
                }
                else
                {
                    pClsProperty.union_id = 0;
                    pClsProperty.mix_union_id = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }

        public DataTable GetMixSplitView(MFGMixSplitProperty pClsProperty)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_MixSplit_ViewData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@from_rough_cut_id", pClsProperty.from_rough_cut_id, DbType.String);
            Request.AddParams("@to_rough_cut_id", pClsProperty.to_rough_cut_id, DbType.String);
            Request.AddParams("@f_lot_id", pClsProperty.f_lot_id, DbType.String);
            Request.AddParams("@t_lot_id", pClsProperty.t_lot_id, DbType.String);
            Request.AddParams("@mix_split_date", pClsProperty.mix_split_date, DbType.Date);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@trn_type", pClsProperty.trn_type, DbType.String);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }

        public DataTable GetLotCutWise(string CutId)
        {
            InterfaceLayer Ope = new InterfaceLayer();
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Lot_GetCutWise;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@cut_id", CutId, DbType.String);
            Request.AddParams("@company_id", BLL.GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", BLL.GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", BLL.GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", BLL.GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetLot()
        {
            InterfaceLayer Ope = new InterfaceLayer();
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Lot_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@company_id", BLL.GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", BLL.GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", BLL.GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", BLL.GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public int Delete_LsAssort(MFGMixSplitProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            int IntRes = 0;
            try
            {
                int count = 0;
                Request Request = new Request();
                Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int32);
                Request.AddParams("@company_id", BLL.GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", BLL.GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", BLL.GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", BLL.GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_LSAssort_Delete;
                Request.CommandType = CommandType.StoredProcedure;

                if (Conn != null)
                    IntRes += Conn.Inter1.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request, pEnum);
                else
                    IntRes += Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
                count = count + 1;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IntRes;
        }
        public int CheckLsAssort(int LotSrno)
        {
            int Count = 0;
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_LSAssort_lotCheck;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@lot_srno", LotSrno, DbType.Int32);
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
    }
}
