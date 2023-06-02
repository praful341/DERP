using BLL.PropertyClasses.Transaction;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction
{
    public class MFGKapanCreate
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();

        public DataTable GetRoughStock()
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Rough_Stock_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }

        public MFGKapanCreateProperty KapanSave(MFGKapanCreateProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int64);
                Request.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int64);
                Request.AddParams("@kapan_no", pClsProperty.kapan_no, DbType.Int64);
                Request.AddParams("@kapan_date", pClsProperty.kapan_date == null ? "" : pClsProperty.kapan_date, DbType.Date);
                Request.AddParams("@rough_sieve_id", pClsProperty.rough_sieve_id == null ? 0 : pClsProperty.rough_sieve_id, DbType.Int32);
                Request.AddParams("@rough_shade_id", pClsProperty.rough_shade_id == null ? 0 : pClsProperty.rough_shade_id, DbType.Int32);
                Request.AddParams("@pcs", pClsProperty.pcs == null ? 0 : pClsProperty.pcs, DbType.Int32);
                Request.AddParams("@carat", pClsProperty.carat == null ? 0 : pClsProperty.carat, DbType.Decimal);
                Request.AddParams("@rate", pClsProperty.rate == null ? 0 : pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@amount", pClsProperty.amount == null ? 0 : pClsProperty.amount, DbType.Decimal);
                Request.AddParams("@team_id", pClsProperty.team_id == null ? 0 : pClsProperty.team_id, DbType.Int32);
                Request.AddParams("@group_id", pClsProperty.group_id == null ? 0 : pClsProperty.group_id, DbType.Int32);
                Request.AddParams("@manager_id", pClsProperty.manager_id == null ? 0 : pClsProperty.manager_id, DbType.Int32);
                Request.AddParams("@employee_id", pClsProperty.employee_id == null ? 0 : pClsProperty.employee_id, DbType.Int32);
                Request.AddParams("@currency_id", pClsProperty.currency_id == null ? 0 : pClsProperty.currency_id, DbType.Int32);
                Request.AddParams("@company_id", pClsProperty.company_id == null ? 0 : pClsProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", pClsProperty.branch_id == null ? 0 : pClsProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", pClsProperty.location_id == null ? 0 : pClsProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", pClsProperty.department_id == null ? 0 : pClsProperty.department_id, DbType.Int32);
                Request.AddParams("@remarks", pClsProperty.remarks == null ? "" : pClsProperty.remarks, DbType.String);
                Request.AddParams("@special_remarks", pClsProperty.special_remarks == null ? "" : pClsProperty.special_remarks, DbType.String);
                Request.AddParams("@client_remarks", pClsProperty.client_remarks == null ? "" : pClsProperty.client_remarks, DbType.String);
                Request.AddParams("@payment_remarks", pClsProperty.payment_remarks == null ? "" : pClsProperty.payment_remarks, DbType.String);
                Request.AddParams("@type", pClsProperty.type == null ? "" : pClsProperty.type, DbType.String);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);
                Request.AddParams("@history_union_id", pClsProperty.history_union_id, DbType.Int64);
                Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Kapan_SAVE;
                Request.CommandType = CommandType.StoredProcedure;

                DataTable p_dtbKapanId = new DataTable();
                if (Conn != null)
                    Conn.Inter1.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, p_dtbKapanId, Request, pEnum);
                else
                    Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, p_dtbKapanId, Request);

                if (p_dtbKapanId != null)
                {
                    if (p_dtbKapanId.Rows.Count > 0)
                    {
                        pClsProperty.kapan_id = Val.ToInt64(p_dtbKapanId.Rows[0][0]);
                        pClsProperty.history_union_id = Val.ToInt64(p_dtbKapanId.Rows[0][1]);
                        pClsProperty.lot_srno = Val.ToInt64(p_dtbKapanId.Rows[0][2]);
                    }
                }
                else
                {
                    pClsProperty.kapan_id = 0;
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

        public MFGKapanCreateProperty KapanSave_New(MFGKapanCreateProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int64);
                Request.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int64);
                Request.AddParams("@kapan_no", pClsProperty.kapan_no, DbType.Int64);
                Request.AddParams("@kapan_date", pClsProperty.kapan_date == null ? "" : pClsProperty.kapan_date, DbType.Date);
                Request.AddParams("@rough_sieve_id", pClsProperty.rough_sieve_id == null ? 0 : pClsProperty.rough_sieve_id, DbType.Int32);
                Request.AddParams("@rough_shade_id", pClsProperty.rough_shade_id == null ? 0 : pClsProperty.rough_shade_id, DbType.Int32);
                Request.AddParams("@pcs", pClsProperty.pcs == null ? 0 : pClsProperty.pcs, DbType.Int32);
                Request.AddParams("@carat", pClsProperty.carat == null ? 0 : pClsProperty.carat, DbType.Decimal);
                Request.AddParams("@rate", pClsProperty.rate == null ? 0 : pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@amount", pClsProperty.amount == null ? 0 : pClsProperty.amount, DbType.Decimal);
                Request.AddParams("@team_id", pClsProperty.team_id == null ? 0 : pClsProperty.team_id, DbType.Int32);
                Request.AddParams("@group_id", pClsProperty.group_id == null ? 0 : pClsProperty.group_id, DbType.Int32);
                Request.AddParams("@manager_id", pClsProperty.manager_id == null ? 0 : pClsProperty.manager_id, DbType.Int32);
                Request.AddParams("@employee_id", pClsProperty.employee_id == null ? 0 : pClsProperty.employee_id, DbType.Int32);
                Request.AddParams("@currency_id", pClsProperty.currency_id == null ? 0 : pClsProperty.currency_id, DbType.Int32);
                Request.AddParams("@company_id", pClsProperty.company_id == null ? 0 : pClsProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", pClsProperty.branch_id == null ? 0 : pClsProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", pClsProperty.location_id == null ? 0 : pClsProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", pClsProperty.department_id == null ? 0 : pClsProperty.department_id, DbType.Int32);
                Request.AddParams("@remarks", pClsProperty.remarks == null ? "" : pClsProperty.remarks, DbType.String);
                Request.AddParams("@special_remarks", pClsProperty.special_remarks == null ? "" : pClsProperty.special_remarks, DbType.String);
                Request.AddParams("@client_remarks", pClsProperty.client_remarks == null ? "" : pClsProperty.client_remarks, DbType.String);
                Request.AddParams("@payment_remarks", pClsProperty.payment_remarks == null ? "" : pClsProperty.payment_remarks, DbType.String);
                Request.AddParams("@type", pClsProperty.type == null ? "" : pClsProperty.type, DbType.String);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);
                Request.AddParams("@history_union_id", pClsProperty.history_union_id, DbType.Int64);
                Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Kapan_SAVE_New;
                Request.CommandType = CommandType.StoredProcedure;

                DataTable p_dtbKapanId = new DataTable();
                if (Conn != null)
                    Conn.Inter1.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, p_dtbKapanId, Request, pEnum);
                else
                    Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, p_dtbKapanId, Request);

                if (p_dtbKapanId != null)
                {
                    if (p_dtbKapanId.Rows.Count > 0)
                    {
                        pClsProperty.kapan_id = Val.ToInt64(p_dtbKapanId.Rows[0][0]);
                        pClsProperty.history_union_id = Val.ToInt64(p_dtbKapanId.Rows[0][1]);
                        pClsProperty.lot_srno = Val.ToInt64(p_dtbKapanId.Rows[0][2]);
                    }
                }
                else
                {
                    pClsProperty.kapan_id = 0;
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

        public MFGKapanCreateProperty Save_MfgStock(MFGKapanCreateProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request stkRequest = new Request();

                stkRequest.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int32);
                stkRequest.AddParams("@company_id", (pClsProperty.company_id) == null ? 0 : pClsProperty.company_id, DbType.Int32);
                stkRequest.AddParams("@branch_id", pClsProperty.branch_id == null ? 0 : pClsProperty.branch_id, DbType.Int32);
                stkRequest.AddParams("@location_id", pClsProperty.location_id == null ? 0 : pClsProperty.location_id, DbType.Int32);
                stkRequest.AddParams("@department_id", pClsProperty.department_id == null ? 0 : pClsProperty.department_id, DbType.Int32);
                stkRequest.AddParams("@rough_sieve_id", pClsProperty.rough_sieve_id == null ? 0 : pClsProperty.rough_sieve_id, DbType.Int32);
                stkRequest.AddParams("@rough_shade_id", pClsProperty.rough_shade_id == null ? 0 : pClsProperty.rough_shade_id, DbType.Int32);
                stkRequest.AddParams("@pcs", pClsProperty.pcs == null ? 0 : pClsProperty.pcs, DbType.Int32);
                stkRequest.AddParams("@carat", pClsProperty.carat == null ? 0 : pClsProperty.carat, DbType.Decimal);
                stkRequest.AddParams("@rate", pClsProperty.rate == null ? 0 : pClsProperty.rate, DbType.Decimal);
                stkRequest.AddParams("@amount", pClsProperty.amount == null ? 0 : pClsProperty.amount, DbType.Decimal);
                stkRequest.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                stkRequest.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                stkRequest.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                stkRequest.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                stkRequest.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);

                stkRequest.CommandText = BLL.TPV.SProc.MFG_TRN_Rough_Stock_SAVE;
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
                    }
                }
                else
                {
                    pClsProperty.lot_id = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }
        public MFGKapanCreateProperty Save_MfgStock_New(MFGKapanCreateProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request stkRequest = new Request();

                stkRequest.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int32);
                stkRequest.AddParams("@company_id", (pClsProperty.company_id) == null ? 0 : pClsProperty.company_id, DbType.Int32);
                stkRequest.AddParams("@branch_id", pClsProperty.branch_id == null ? 0 : pClsProperty.branch_id, DbType.Int32);
                stkRequest.AddParams("@location_id", pClsProperty.location_id == null ? 0 : pClsProperty.location_id, DbType.Int32);
                stkRequest.AddParams("@department_id", pClsProperty.department_id == null ? 0 : pClsProperty.department_id, DbType.Int32);
                stkRequest.AddParams("@rough_sieve_id", pClsProperty.rough_sieve_id == null ? 0 : pClsProperty.rough_sieve_id, DbType.Int32);
                stkRequest.AddParams("@rough_shade_id", pClsProperty.rough_shade_id == null ? 0 : pClsProperty.rough_shade_id, DbType.Int32);
                stkRequest.AddParams("@pcs", pClsProperty.pcs == null ? 0 : pClsProperty.pcs, DbType.Int32);
                stkRequest.AddParams("@carat", pClsProperty.carat == null ? 0 : pClsProperty.carat, DbType.Decimal);
                stkRequest.AddParams("@rate", pClsProperty.rate == null ? 0 : pClsProperty.rate, DbType.Decimal);
                stkRequest.AddParams("@amount", pClsProperty.amount == null ? 0 : pClsProperty.amount, DbType.Decimal);
                stkRequest.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                stkRequest.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                stkRequest.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                stkRequest.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                stkRequest.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);
                stkRequest.AddParams("@kapan_no", pClsProperty.kapan_no, DbType.String);

                stkRequest.CommandText = BLL.TPV.SProc.MFG_TRN_Rough_Stock_SAVE_New;
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
                    }
                }
                else
                {
                    pClsProperty.lot_id = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }

        public int Save_New(MFGKapanCreateProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
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
                NewRequest.AddParams("@from_rate", pClsProperty.from_rate, DbType.Decimal);
                NewRequest.AddParams("@from_amount", pClsProperty.from_amount, DbType.Decimal);
                NewRequest.AddParams("@to_rate", pClsProperty.to_rate, DbType.Decimal);
                NewRequest.AddParams("@to_amount", pClsProperty.to_amount, DbType.Decimal);
                NewRequest.AddParams("@mix_split_date", pClsProperty.kapan_date, DbType.Date);

                NewRequest.CommandText = BLL.TPV.SProc.MFG_TRN_Rough_MixSplit_SAVE;
                NewRequest.CommandType = CommandType.StoredProcedure;
                if (Conn != null)
                    IntRes = Conn.Inter1.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, NewRequest, pEnum);
                else
                    IntRes = Ope.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, NewRequest);
                return IntRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable CheckKapan(string KapanNo)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_KapanNo_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@kapan_no", KapanNo, DbType.String);
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }

        public DataTable GetData()
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Kapan_Mix_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public string ISExists(string CutNo, string KapanNo, Int64 Cut_Id)
        {
            Validation Val = new Validation();
            return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "MFG_TRN_Rough_Cut cut with (nolock) left join MFG_TRN_Kapan_Create kpn with (nolock) on kpn.kapan_id = cut.kapan_id", "rough_cut_no", "AND rough_cut_no = '" + CutNo + "' AND kpn.kapan_no = '" + KapanNo + "'"));
        }
        public int Kapan_Date_Update(MFGKapanCreateProperty pClsProperty)
        {
            try
            {
                int IntRes = 0;
                Request NewRequest = new Request();

                //NewRequest.AddParams("@kapan_id", KapanId, DbType.Int32);
                NewRequest.AddParams("@mixsplit_id", pClsProperty.mixsplit_id, DbType.Int64);
                NewRequest.AddParams("@kapan_date", pClsProperty.kapan_date, DbType.Date);
                NewRequest.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int64);
                NewRequest.AddParams("@rate", pClsProperty.rate, DbType.Decimal);

                NewRequest.CommandText = BLL.TPV.SProc.MFG_TRN_Kapan_Date_Update;
                NewRequest.CommandType = CommandType.StoredProcedure;

                IntRes = Ope.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, NewRequest);
                return IntRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public int Kapan_Date_Update(MFGKapanCreateProperty pClsProperty)
        //{
        //    try
        //    {
        //        int IntRes = 0;
        //        Request NewRequest = new Request();

        //        NewRequest.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int32);
        //        NewRequest.AddParams("@kapan_date", pClsProperty.kapan_date, DbType.Date);

        //        NewRequest.CommandText = BLL.TPV.SProc.MFG_TRN_Kapan_Date_Update;
        //        NewRequest.CommandType = CommandType.StoredProcedure;

        //        IntRes = Ope.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, NewRequest);
        //        return IntRes;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public DataTable GetSearchKapan(int kapanid)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_RoughMix_Update_Getdata;
            Request.AddParams("@kapan_id", kapanid, DbType.Int32);
            //Request.AddParams("@mixsplit_id", mixsplitId, DbType.String);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public MFGKapanCreateProperty Kapan_Delete(MFGKapanCreateProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request stkRequest = new Request();

                stkRequest.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int32);
                stkRequest.AddParams("@carat", pClsProperty.carat, DbType.Decimal);

                stkRequest.CommandText = BLL.TPV.SProc.MFG_TRN_Kapan_Delete;
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
