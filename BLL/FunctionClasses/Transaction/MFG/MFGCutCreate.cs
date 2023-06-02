using BLL.PropertyClasses.Transaction;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction
{
    public class MFGCutCreate
    {
        InterfaceLayer Ope = new InterfaceLayer();
        BLL.Validation Val = new BLL.Validation();
        public MFGCutCreateProperty Save_RoughCutStock(MFGCutCreateProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request stkRequest = new Request();

                stkRequest.AddParams("@lot_id", pClsProperty.rough_lot_id, DbType.Int32);
                stkRequest.AddParams("@manager_id", pClsProperty.manager_id == null ? 0 : pClsProperty.manager_id, DbType.Int32);
                stkRequest.AddParams("@employee_id", pClsProperty.employee_id == null ? 0 : pClsProperty.employee_id, DbType.Int32);
                stkRequest.AddParams("@rough_cut_id", pClsProperty.rough_cut_id == null ? 0 : pClsProperty.rough_cut_id, DbType.Int32);
                stkRequest.AddParams("@company_id", pClsProperty.company_id == null ? 0 : pClsProperty.company_id, DbType.Int32);
                stkRequest.AddParams("@branch_id", pClsProperty.branch_id == null ? 0 : pClsProperty.branch_id, DbType.Int32);
                stkRequest.AddParams("@location_id", pClsProperty.location_id == null ? 0 : pClsProperty.location_id, DbType.Int32);
                stkRequest.AddParams("@department_id", pClsProperty.department_id == null ? 0 : pClsProperty.department_id, DbType.Int32);
                stkRequest.AddParams("@pcs", pClsProperty.pcs == null ? 0 : pClsProperty.pcs, DbType.Int32);
                stkRequest.AddParams("@carat", pClsProperty.carat == null ? 0 : pClsProperty.carat, DbType.Decimal);
                stkRequest.AddParams("@rate", pClsProperty.rate == null ? 0 : pClsProperty.rate, DbType.Decimal);
                stkRequest.AddParams("@amount", pClsProperty.amount == null ? 0 : pClsProperty.amount, DbType.Decimal);
                stkRequest.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                stkRequest.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                stkRequest.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                stkRequest.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                stkRequest.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);
                stkRequest.AddParams("@dept_union_id", pClsProperty.department_union_id, DbType.Int64);
                stkRequest.AddParams("@history_union_id", pClsProperty.history_union_id, DbType.Int64);
                stkRequest.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);

                stkRequest.CommandText = BLL.TPV.SProc.MFG_TRN_Stock_SAVE;
                stkRequest.CommandType = CommandType.StoredProcedure;


                DataTable p_dtbRghLotId = new DataTable();
                if (Conn != null)
                    Conn.Inter1.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, p_dtbRghLotId, stkRequest, pEnum);
                else
                    Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, p_dtbRghLotId, stkRequest);

                if (p_dtbRghLotId != null)
                {
                    if (p_dtbRghLotId.Rows.Count > 0)
                    {
                        pClsProperty.lot_id = Val.ToInt64(p_dtbRghLotId.Rows[0][0]);
                        pClsProperty.department_union_id = Val.ToInt64(p_dtbRghLotId.Rows[0][1]);
                        pClsProperty.lot_srno = Val.ToInt64(p_dtbRghLotId.Rows[0][2]);
                    }
                }
                else
                {
                    pClsProperty.lot_id = 0;
                    pClsProperty.department_union_id = 0;
                    pClsProperty.lot_srno = 0;
                }
            }
            catch (Exception ex)
            {
                //General.ShowErrors(ex.ToString());
                throw ex;
            }
            return pClsProperty;
        }

        public int Save_New(MFGCutCreateProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                int IntRes = 0;
                Request NewRequest = new Request();

                NewRequest.AddParams("@mixsplit_id", pClsProperty.mixsplit_id, DbType.Int64);
                NewRequest.AddParams("@mix_type_id", pClsProperty.mix_type_id, DbType.Int64);
                NewRequest.AddParams("@company_id", pClsProperty.company_id, DbType.Int32);
                NewRequest.AddParams("@branch_id", pClsProperty.branch_id, DbType.Int32);
                NewRequest.AddParams("@location_id", pClsProperty.location_id, DbType.Int32);
                NewRequest.AddParams("@department_id", pClsProperty.department_id, DbType.Int32);
                NewRequest.AddParams("@from_lot_id", pClsProperty.from_lot_id, DbType.Int32);
                NewRequest.AddParams("@to_lot_id", pClsProperty.to_lot_id, DbType.Int32);
                NewRequest.AddParams("@from_kapan_id", pClsProperty.from_kapan_id, DbType.Int32);
                NewRequest.AddParams("@to_kapan_id", pClsProperty.to_kapan_id, DbType.Int32);
                NewRequest.AddParams("@transaction_type_id", pClsProperty.transaction_type_id, DbType.Int32);
                NewRequest.AddParams("@from_pcs", pClsProperty.from_pcs, DbType.Int32);
                NewRequest.AddParams("@from_carat", pClsProperty.from_carat, DbType.Decimal);
                NewRequest.AddParams("@to_pcs", pClsProperty.to_pcs, DbType.Int32);
                NewRequest.AddParams("@to_carat", pClsProperty.to_carat, DbType.Decimal);
                NewRequest.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                NewRequest.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                NewRequest.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                NewRequest.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                NewRequest.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);
                NewRequest.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);
                NewRequest.AddParams("@mix_split_date", pClsProperty.rough_cut_date, DbType.Date);

                NewRequest.CommandText = BLL.TPV.SProc.MFG_TRN_RoughCut_MixSplit_SAVE;
                NewRequest.CommandType = CommandType.StoredProcedure;
                if (Conn != null)
                    IntRes = Conn.Inter1.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, NewRequest, pEnum);
                else
                    IntRes = Ope.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, NewRequest);
                return IntRes;
            }
            catch (Exception ex)
            {
                //General.ShowErrors(ex.ToString());
                throw ex;
            }
            // return pClsProperty;
        }
        public MFGCutCreateProperty CutSave(MFGCutCreateProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                //int IntRes = 0;
                Request Request = new Request();

                Request.AddParams("@rough_cut_id", pClsProperty.rough_cut_id, DbType.Int64);
                Request.AddParams("@lot_id", pClsProperty.lot_id == null ? 0 : pClsProperty.lot_id, DbType.Int64);
                Request.AddParams("@old_lotid", pClsProperty.from_lot_id == null ? 0 : pClsProperty.from_lot_id, DbType.Int64);
                Request.AddParams("@rough_cut_no", pClsProperty.rough_cut_no == null ? "" : pClsProperty.rough_cut_no, DbType.Int64);
                Request.AddParams("@rough_cut_date", pClsProperty.rough_cut_date == null ? "" : pClsProperty.rough_cut_date, DbType.Date);
                Request.AddParams("@rough_cuttype_id", pClsProperty.rough_cuttype_id == null ? 0 : pClsProperty.rough_cuttype_id, DbType.Int32);
                //Request.AddParams("@rough_shade_id", pClsProperty.rough_shade_id, DbType.Int32);
                Request.AddParams("@pcs", pClsProperty.pcs == null ? 0 : pClsProperty.pcs, DbType.Int32);
                Request.AddParams("@carat", pClsProperty.carat == null ? 0 : pClsProperty.carat, DbType.Decimal);
                Request.AddParams("@rate", pClsProperty.rate == null ? 0 : pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@amount", pClsProperty.amount == null ? 0 : pClsProperty.amount, DbType.Decimal);
                Request.AddParams("@manager_id", pClsProperty.manager_id == null ? 0 : pClsProperty.manager_id, DbType.Int32);
                Request.AddParams("@employee_id", pClsProperty.employee_id == null ? 0 : pClsProperty.employee_id, DbType.Int32);
                Request.AddParams("@currency_id", pClsProperty.currency_id == null ? 0 : pClsProperty.currency_id, DbType.Int32);
                Request.AddParams("@kapan_id", pClsProperty.kapan_id == null ? 0 : pClsProperty.kapan_id, DbType.Int32);
                Request.AddParams("@company_id", pClsProperty.company_id == null ? 0 : pClsProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", pClsProperty.branch_id == null ? 0 : pClsProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", pClsProperty.location_id == null ? 0 : pClsProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", pClsProperty.department_id == null ? 0 : pClsProperty.department_id, DbType.Int32);
                Request.AddParams("@remarks", pClsProperty.remarks == null ? "" : pClsProperty.remarks, DbType.String);
                Request.AddParams("@special_remarks", pClsProperty.special_remarks == null ? "" : pClsProperty.special_remarks, DbType.String);
                Request.AddParams("@client_remarks", pClsProperty.client_remarks == null ? "" : pClsProperty.client_remarks, DbType.String);
                Request.AddParams("@payment_remarks", pClsProperty.payment_remarks == null ? "" : pClsProperty.payment_remarks, DbType.String);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);
                Request.AddParams("@history_union_id", pClsProperty.history_union_id, DbType.Int64);
                Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Rough_Cut_SAVE;
                Request.CommandType = CommandType.StoredProcedure;


                DataTable p_dtbMasterId = new DataTable();
                if (Conn != null)
                    Conn.Inter1.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, p_dtbMasterId, Request, pEnum);
                else
                    Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, p_dtbMasterId, Request);

                if (p_dtbMasterId != null)
                {
                    if (p_dtbMasterId.Rows.Count > 0)
                    {
                        pClsProperty.rough_cut_id = Val.ToInt64(p_dtbMasterId.Rows[0][0]);
                        pClsProperty.history_union_id = Val.ToInt64(p_dtbMasterId.Rows[0][1]);
                        pClsProperty.lot_srno = Val.ToInt64(p_dtbMasterId.Rows[0][2]);
                    }
                }
                else
                {
                    pClsProperty.rough_cut_id = 0;
                    pClsProperty.history_union_id = 0;
                    pClsProperty.lot_srno = 0;
                }
            }
            catch (Exception ex)
            {
                //General.ShowErrors(ex.ToString());
                throw ex;
            }
            return pClsProperty;
        }
        public MFGCutCreateProperty CutSave_New(MFGCutCreateProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                //int IntRes = 0;
                Request Request = new Request();

                Request.AddParams("@rough_cut_id", pClsProperty.rough_cut_id, DbType.Int64);
                Request.AddParams("@lot_id", pClsProperty.lot_id == null ? 0 : pClsProperty.lot_id, DbType.Int64);
                Request.AddParams("@old_lotid", pClsProperty.from_lot_id == null ? 0 : pClsProperty.from_lot_id, DbType.Int64);
                Request.AddParams("@rough_cut_no", pClsProperty.rough_cut_no == null ? "" : pClsProperty.rough_cut_no, DbType.Int64);
                Request.AddParams("@rough_cut_date", pClsProperty.rough_cut_date == null ? "" : pClsProperty.rough_cut_date, DbType.Date);
                Request.AddParams("@rough_cuttype_id", pClsProperty.rough_cuttype_id == null ? 0 : pClsProperty.rough_cuttype_id, DbType.Int32);
                //Request.AddParams("@rough_shade_id", pClsProperty.rough_shade_id, DbType.Int32);
                Request.AddParams("@pcs", pClsProperty.pcs == null ? 0 : pClsProperty.pcs, DbType.Int32);
                Request.AddParams("@carat", pClsProperty.carat == null ? 0 : pClsProperty.carat, DbType.Decimal);
                Request.AddParams("@rate", pClsProperty.rate == null ? 0 : pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@amount", pClsProperty.amount == null ? 0 : pClsProperty.amount, DbType.Decimal);
                Request.AddParams("@manager_id", pClsProperty.manager_id == null ? 0 : pClsProperty.manager_id, DbType.Int32);
                Request.AddParams("@employee_id", pClsProperty.employee_id == null ? 0 : pClsProperty.employee_id, DbType.Int32);
                Request.AddParams("@currency_id", pClsProperty.currency_id == null ? 0 : pClsProperty.currency_id, DbType.Int32);
                Request.AddParams("@kapan_id", pClsProperty.kapan_id == null ? 0 : pClsProperty.kapan_id, DbType.Int32);
                Request.AddParams("@company_id", pClsProperty.company_id == null ? 0 : pClsProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", pClsProperty.branch_id == null ? 0 : pClsProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", pClsProperty.location_id == null ? 0 : pClsProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", pClsProperty.department_id == null ? 0 : pClsProperty.department_id, DbType.Int32);
                Request.AddParams("@remarks", pClsProperty.remarks == null ? "" : pClsProperty.remarks, DbType.String);
                Request.AddParams("@special_remarks", pClsProperty.special_remarks == null ? "" : pClsProperty.special_remarks, DbType.String);
                Request.AddParams("@client_remarks", pClsProperty.client_remarks == null ? "" : pClsProperty.client_remarks, DbType.String);
                Request.AddParams("@payment_remarks", pClsProperty.payment_remarks == null ? "" : pClsProperty.payment_remarks, DbType.String);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);
                Request.AddParams("@history_union_id", pClsProperty.history_union_id, DbType.Int64);
                Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Rough_Cut_SAVE_New;
                Request.CommandType = CommandType.StoredProcedure;


                DataTable p_dtbMasterId = new DataTable();
                if (Conn != null)
                    Conn.Inter1.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, p_dtbMasterId, Request, pEnum);
                else
                    Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, p_dtbMasterId, Request);

                if (p_dtbMasterId != null)
                {
                    if (p_dtbMasterId.Rows.Count > 0)
                    {
                        pClsProperty.rough_cut_id = Val.ToInt64(p_dtbMasterId.Rows[0][0]);
                        pClsProperty.history_union_id = Val.ToInt64(p_dtbMasterId.Rows[0][1]);
                        pClsProperty.lot_srno = Val.ToInt64(p_dtbMasterId.Rows[0][2]);
                    }
                }
                else
                {
                    pClsProperty.rough_cut_id = 0;
                    pClsProperty.history_union_id = 0;
                    pClsProperty.lot_srno = 0;
                }
            }
            catch (Exception ex)
            {
                //General.ShowErrors(ex.ToString());
                throw ex;
            }
            return pClsProperty;
        }

        public DataTable GetRoughCut(string RoughCut)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_RoughCut_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@rough_cut_no", RoughCut, DbType.String);
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetKapan()
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Kapan_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetDataDetails(int p_numID)
        {
            DataTable DTab = new DataTable();
            try
            {

                Request Request = new Request();
                Request.CommandText = BLL.TPV.SProc.TRN_RoughCut_GetDetailsData;
                Request.CommandType = CommandType.StoredProcedure;
                Request.AddParams("@rough_cut_id", p_numID, DbType.Int32);

                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
                return DTab;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return DTab;
            }
        }
        public int Update(MFGCutCreateProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            Request Request = new Request();
            int IntRes = 0;
            Request.AddParams("@rough_cut_date", pClsProperty.rough_cut_date, DbType.Date);
            Request.AddParams("@rough_cut_id", pClsProperty.rough_cut_id, DbType.Int32);
            Request.AddParams("@rough_cut_no", pClsProperty.rough_cut_no, DbType.String);
            Request.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
            Request.AddParams("@old_carat", pClsProperty.old_carat, DbType.Decimal);
            Request.AddParams("@diff_carat", pClsProperty.diff_carat, DbType.Decimal);
            Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
            Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);

            Request.CommandText = BLL.TPV.SProc.MFG_TRN_RoughCut_Update;
            Request.CommandType = CommandType.StoredProcedure;
            IntRes = Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
            return IntRes;
        }
        public int LockSave(MFGCutCreateProperty pClsProperty)
        {
            Request Request = new Request();
            Request.AddParams("@lock_date", pClsProperty.lock_date, DbType.Date);
            Request.AddParams("@lock_type_id", pClsProperty.lock_type_id, DbType.Int32);
            Request.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int32);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.Date);

            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Lot_Lock_Save;
            Request.CommandType = CommandType.StoredProcedure;
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }
        public int LockDelete(Int32 pLock_ID, Int64 pStrKapn, string Lot_ID)
        {
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Lot_Lock_Delete;
            Request.CommandType = CommandType.StoredProcedure;

            Request.AddParams("@lock_type_id", pLock_ID, DbType.Int32);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@lot_id", Lot_ID, DbType.String);

            int IntRes = 0;

            IntRes = Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);

            return IntRes;
        }

        public DataTable LockGetData(MFGCutCreateProperty pClsProperty)
        {
            DataTable DTab = new DataTable();
            try
            {
                Request Request = new Request();
                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Lot_Lock_GetData;
                Request.CommandType = CommandType.StoredProcedure;
                Request.AddParams("@lock_date", pClsProperty.lock_date, DbType.Date);
                Request.AddParams("@lock_type_id", pClsProperty.lock_type_id, DbType.Int32);
                Request.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int32);

                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
                return DTab;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return DTab;
            }
        }
        public DataTable GetSearchData(string FDate, string TDate, int kapanid)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_RoughCut_SearchData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@kapan_id", kapanid, DbType.Int32);
            Request.AddParams("@datFromDate", Val.DBDate(FDate), DbType.Date);
            Request.AddParams("@datToDate", Val.DBDate(TDate), DbType.Date);
            // Request.AddParams("@quality_id", Quality, DbType.String);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public string ISExists(string CutNo, Int64 Cut_Id)
        {
            Validation Val = new Validation();
            return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "MFG_TRN_Rough_Cut", "rough_cut_no", "AND rough_cut_no = '" + CutNo + "'"));
        }
        public MFGCutCreateProperty Cut_Delete(MFGCutCreateProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request stkRequest = new Request();

                stkRequest.AddParams("@rough_cut_id", pClsProperty.rough_cut_id, DbType.Int64);
                stkRequest.AddParams("@carat", pClsProperty.carat, DbType.Decimal);

                stkRequest.CommandText = BLL.TPV.SProc.MFG_TRN_RoughCut_Delete;
                stkRequest.CommandType = CommandType.StoredProcedure;

                DataTable p_dtbRghLotId = new DataTable();
                if (Conn != null)
                    Conn.Inter1.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, p_dtbRghLotId, stkRequest, pEnum);
                else
                    Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, p_dtbRghLotId, stkRequest);

                if (p_dtbRghLotId != null)
                {
                    if (p_dtbRghLotId.Rows.Count > 0)
                    {
                        pClsProperty.remarks = Val.ToString(p_dtbRghLotId.Rows[0][0]);
                    }
                }
                else
                {
                    pClsProperty.remarks = "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }
    }
}
