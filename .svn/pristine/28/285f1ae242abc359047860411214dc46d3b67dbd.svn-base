using BLL.PropertyClasses.Transaction;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction
{
    public class MFGPurchase
    {
        InterfaceLayer Ope = new InterfaceLayer();
        BLL.Validation Val = new BLL.Validation();

        public MFG_PurchaseProperty Save(MFG_PurchaseProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@purchase_id", pClsProperty.purchase_id, DbType.Int32);
                Request.AddParams("@party_invoice_no", pClsProperty.party_invoice_no, DbType.String);
                Request.AddParams("@invoice_no", pClsProperty.invoice_no, DbType.String);
                Request.AddParams("@fin_year_id", pClsProperty.fin_year_id, DbType.Int32);
                Request.AddParams("@party_id", pClsProperty.party_id, DbType.Int32);
                Request.AddParams("@broker_id", pClsProperty.broker_id, DbType.Int32);
                Request.AddParams("@sight_type_id", pClsProperty.sight_type_id, DbType.Int32);
                Request.AddParams("@source_id", pClsProperty.source_id, DbType.Int32);
                Request.AddParams("@article_id", pClsProperty.article_id, DbType.Int32);
                Request.AddParams("@group_id", pClsProperty.group_id, DbType.Int32);
                Request.AddParams("@team_id", pClsProperty.team_id, DbType.Int32);
                Request.AddParams("@janged_sieve_id", pClsProperty.janged_sieve_id, DbType.Int32);
                Request.AddParams("@rough_type_id", pClsProperty.rough_type_id, DbType.Int32);
                Request.AddParams("@company_id", pClsProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", pClsProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", pClsProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", pClsProperty.department_id, DbType.Int32);
                Request.AddParams("@currency_id", pClsProperty.currency_id, DbType.Int32);
                Request.AddParams("@remarks", pClsProperty.remarks, DbType.String);
                Request.AddParams("@special_remarks", pClsProperty.special_remarks, DbType.String);
                Request.AddParams("@client_remarks", pClsProperty.client_remarks, DbType.String);
                Request.AddParams("@payment_remarks", pClsProperty.payment_remarks, DbType.String);
                Request.AddParams("@due_days", pClsProperty.due_days, DbType.Int16);
                Request.AddParams("@brokrage_per", pClsProperty.brokrage_per, DbType.String);
                Request.AddParams("@brokrage_amount", pClsProperty.brokrage_amount, DbType.String);
                Request.AddParams("@premium_per", pClsProperty.premium_per, DbType.String);
                Request.AddParams("@premium_amount", pClsProperty.premium_amount, DbType.String);
                Request.AddParams("@date", pClsProperty.date, DbType.Date);
                Request.AddParams("@other_expence", pClsProperty.other_expence, DbType.String);
                Request.AddParams("@net_amount", pClsProperty.net_amount, DbType.String);
                Request.AddParams("@exchange_rate", pClsProperty.exchange_rate, DbType.String);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int32);
                Request.AddParams("@type", pClsProperty.type, DbType.String);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Purchase_Master_SAVE;
                Request.CommandType = CommandType.StoredProcedure;

                DataTable p_dtbMasterId = new DataTable();

                if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                {
                    if (Conn != null)
                        Conn.Inter1.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, p_dtbMasterId, Request, pEnum);
                    else
                        Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, p_dtbMasterId, Request);
                }
                else
                {
                    if (Conn != null)
                        Conn.Inter2.GetDataTable(DBConnections.ConnectionDeveloper, DBConnections.ProviderDeveloper, p_dtbMasterId, Request, pEnum);
                    else
                        Ope.GetDataTable(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, p_dtbMasterId, Request);
                }
                if (p_dtbMasterId != null)
                {
                    if (p_dtbMasterId.Rows.Count > 0)
                    {
                        pClsProperty.purchase_id = Convert.ToInt32(p_dtbMasterId.Rows[0][0]);
                    }
                }
                else
                {
                    pClsProperty.purchase_id = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }
        public MFG_PurchaseProperty Save_MfgStock_Purchase(MFG_PurchaseProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request stkRequest = new Request();

                stkRequest.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int32);
                stkRequest.AddParams("@company_id", pClsProperty.company_id, DbType.Int32);
                stkRequest.AddParams("@branch_id", pClsProperty.branch_id, DbType.Int32);
                stkRequest.AddParams("@location_id", pClsProperty.location_id, DbType.Int32);
                stkRequest.AddParams("@department_id", pClsProperty.department_id, DbType.Int32);
                stkRequest.AddParams("@rough_sieve_id", pClsProperty.rough_sieve_id, DbType.Int32);
                stkRequest.AddParams("@rough_shade_id", pClsProperty.rough_shade_id, DbType.Int32);
                stkRequest.AddParams("@pcs", pClsProperty.pcs, DbType.Int32);
                stkRequest.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
                stkRequest.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                stkRequest.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
                stkRequest.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                stkRequest.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                stkRequest.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                stkRequest.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                stkRequest.AddParams("@form_id", pClsProperty.form_id, DbType.Int32);

                stkRequest.CommandText = BLL.TPV.SProc.MFG_TRN_RoughPurchase_Stock_SAVE;
                stkRequest.CommandType = CommandType.StoredProcedure;


                DataTable p_dtbRghLotId = new DataTable();

                if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                {
                    if (Conn != null)
                        Conn.Inter1.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, p_dtbRghLotId, stkRequest, pEnum);
                    else
                        Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, p_dtbRghLotId, stkRequest);
                }
                else
                {
                    if (Conn != null)
                        Conn.Inter2.GetDataTable(DBConnections.ConnectionDeveloper, DBConnections.ProviderDeveloper, p_dtbRghLotId, stkRequest, pEnum);
                    else
                        Ope.GetDataTable(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, p_dtbRghLotId, stkRequest);
                }
                if (p_dtbRghLotId != null)
                {
                    if (p_dtbRghLotId.Rows.Count > 0)
                    {
                        pClsProperty.lot_id = Convert.ToInt32(p_dtbRghLotId.Rows[0][0]);
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
        public int Save_Detail(MFG_PurchaseProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                int IntRes = 0;
                Request RequestDetails = new Request();

                RequestDetails.AddParams("@purchase_detail_id", pClsProperty.purchase_detail_id, DbType.Int32);
                RequestDetails.AddParams("@purchase_id", pClsProperty.purchase_id, DbType.Int32);
                RequestDetails.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int32);
                RequestDetails.AddParams("@rough_sieve_id", pClsProperty.rough_sieve_id, DbType.Int32);
                RequestDetails.AddParams("@rough_shade_id", pClsProperty.rough_shade_id, DbType.Int32);
                RequestDetails.AddParams("@pcs", pClsProperty.pcs, DbType.Int32);
                RequestDetails.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
                RequestDetails.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                RequestDetails.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
                RequestDetails.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                RequestDetails.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                RequestDetails.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                RequestDetails.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                RequestDetails.AddParams("@form_id", pClsProperty.form_id, DbType.Int32);
                RequestDetails.AddParams("@rough_type_id", pClsProperty.rough_type_id, DbType.Int32);
                RequestDetails.AddParams("@article_id", pClsProperty.article_id, DbType.Int32);

                RequestDetails.CommandText = BLL.TPV.SProc.MFG_TRN_Purchase_Detail_SAVE;
                RequestDetails.CommandType = CommandType.StoredProcedure;

                if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                {
                    if (Conn != null)
                        IntRes = Conn.Inter1.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, RequestDetails, pEnum);
                    else
                        IntRes = Ope.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, RequestDetails);
                }
                else
                {
                    if (Conn != null)
                        IntRes = Conn.Inter2.ExecuteNonQuery(DBConnections.ConnectionDeveloper, DBConnections.ProviderDeveloper, RequestDetails, pEnum);
                    else
                        IntRes = Ope.ExecuteNonQuery(DBConnections.ConnectionDeveloper, DBConnections.ProviderDeveloper, RequestDetails);
                }
                return IntRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(MFG_PurchaseProperty pClsProperty, DataTable p_dtbDetails)
        {
            Request Request = new Request();
            int IntRes = 0;
            try
            {
                foreach (DataRow drw in p_dtbDetails.Rows)
                {
                    Request RequestDetails = new Request();

                    RequestDetails.AddParams("@purchase_id", pClsProperty.purchase_id, DbType.Int32);
                    RequestDetails.AddParams("@lot_id", Val.ToInt(drw["lot_id"]), DbType.Int32);
                    RequestDetails.AddParams("@purchase_detail_id", Val.ToInt(drw["purchase_detail_id"]), DbType.Int32);
                    RequestDetails.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                    RequestDetails.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                    RequestDetails.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                    RequestDetails.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

                    RequestDetails.CommandText = BLL.TPV.SProc.MFG_TRN_Purchase_Delete;
                    RequestDetails.CommandType = CommandType.StoredProcedure;

                    if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                    {
                        IntRes += Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, RequestDetails);
                    }
                    else
                    {
                        IntRes += Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, RequestDetails);
                    }
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
            }
            return IntRes;
        }
        public DataTable GetData(string p_dtpFromDate, string p_dtpToDate, string invoice_no, int partyId)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Purchase_GetData;

            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@From_Date", p_dtpFromDate, DbType.Int32);
            Request.AddParams("@To_Date", p_dtpToDate, DbType.Int32);
            Request.AddParams("@invoice_no", invoice_no, DbType.String);
            Request.AddParams("@party_id", partyId, DbType.Int32);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            }
            else
            {
                Ope.GetDataTable(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, DTab, Request);
            }
            return DTab;
        }

        public DataTable GetData_RoughStock(string p_dtpFromDate, string p_dtpToDate, string invoice_no, int partyId)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.TRN_Save_Rough_Purchase_GetData;

            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@From_Date", p_dtpFromDate, DbType.Int32);
            Request.AddParams("@To_Date", p_dtpToDate, DbType.Int32);
            Request.AddParams("@invoice_no", invoice_no, DbType.String);
            Request.AddParams("@party_id", partyId, DbType.Int32);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            }
            else
            {
                Ope.GetDataTable(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, DTab, Request);
            }
            return DTab;
        }

        public DataTable GetDataDetails(int p_numID)
        {
            DataTable DTab = new DataTable();
            try
            {
                Request Request = new Request();
                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Purchase_GetDetailsData;
                Request.CommandType = CommandType.StoredProcedure;
                Request.AddParams("@p_numPur_ID", p_numID, DbType.Int32);

                if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                {
                    Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
                }
                else
                {
                    Ope.GetDataTable(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, DTab, Request);
                }
                return DTab;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return DTab;
            }
        }

        public DataTable GetDataDetails_Rough(Int64 p_numLot_srNo)
        {
            DataTable DTab = new DataTable();
            try
            {
                Request Request = new Request();
                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Purchase_GetDetailsRough;
                Request.CommandType = CommandType.StoredProcedure;
                Request.AddParams("@p_numLot_SRNo", p_numLot_srNo, DbType.Int64);

                if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                {
                    Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
                }
                else
                {
                    Ope.GetDataTable(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, DTab, Request);
                }
                return DTab;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return DTab;
            }
        }
        public DataTable GetPrintData(MFG_PurchaseProperty Property)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@purchase_date", Property.date, DbType.Date);
            Request.AddParams("@purchase_id", Property.purchase_id, DbType.String);
            Request.AddParams("@invoice_no", Property.invoice_no, DbType.Int32);

            Request.CommandText = BLL.TPV.SProc.RPT_TRN_Purchase_Print_GetData;
            Request.CommandType = CommandType.StoredProcedure;

            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            }
            else
            {
                Ope.GetDataTable(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, DTab, Request);
            }
            return DTab;
        }
        public DataTable CheckPurchaseToKapan(int PurId)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@purchase_id", PurId, DbType.Int32);

            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Purchase_Kapan_GetData;
            Request.CommandType = CommandType.StoredProcedure;

            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            }
            else
            {
                Ope.GetDataTable(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, DTab, Request);
            }
            return DTab;
        }

        public MFG_PurchaseProperty Save_Rough_Purchase(MFG_PurchaseProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();
                Request.AddParams("@invoice_no", pClsProperty.invoice_no, DbType.String);
                Request.AddParams("@rough_purchase_id", pClsProperty.rough_purchase_id, DbType.Int32);
                Request.AddParams("@purchase_date", pClsProperty.date, DbType.Date);
                Request.AddParams("@shiping_date", pClsProperty.shiping_date, DbType.Date);
                Request.AddParams("@rough_party_id", pClsProperty.party_id, DbType.Int32);
                Request.AddParams("@rough_broker_id", pClsProperty.broker_id, DbType.Int32);
                Request.AddParams("@rough_type_id", pClsProperty.rough_type_id, DbType.Int32);
                Request.AddParams("@currency_id", pClsProperty.currency_id, DbType.Int32);
                Request.AddParams("@exchange_rate", pClsProperty.exchange_rate, DbType.Decimal);
                Request.AddParams("@currency_type", pClsProperty.currency_type, DbType.String);
                Request.AddParams("@series", pClsProperty.series, DbType.Int32);
                Request.AddParams("@terms_days", pClsProperty.term_days, DbType.Int32);
                Request.AddParams("@due_date", pClsProperty.due_date, DbType.Date);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int32);
                Request.AddParams("@size_id", pClsProperty.rough_sieve_id, DbType.Int32);
                Request.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
                Request.AddParams("@shiping_per", pClsProperty.shiping_per, DbType.Decimal);
                Request.AddParams("@shiping_rate", pClsProperty.shiping_rate, DbType.Decimal);
                Request.AddParams("@net_rate", pClsProperty.net_rate, DbType.Decimal);
                Request.AddParams("@net_amount", pClsProperty.net_amount, DbType.Decimal);
                Request.AddParams("@remarks", pClsProperty.remarks, DbType.String);
                Request.AddParams("@is_inward", pClsProperty.is_inward, DbType.Int32);
                Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);

                Request.CommandText = BLL.TPV.SProc.TRN_Save_Rough_Purchase;
                Request.CommandType = CommandType.StoredProcedure;

                DataTable p_dtbMasterId = new DataTable();
                if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                {
                    if (Conn != null)
                        Conn.Inter1.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, p_dtbMasterId, Request, pEnum);
                    else
                        Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, p_dtbMasterId, Request);
                }
                else
                {
                    if (Conn != null)
                        Conn.Inter2.GetDataTable(DBConnections.ConnectionDeveloper, DBConnections.ProviderDeveloper, p_dtbMasterId, Request, pEnum);
                    else
                        Ope.GetDataTable(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, p_dtbMasterId, Request);
                }
                if (p_dtbMasterId != null)
                {
                    if (p_dtbMasterId.Rows.Count > 0)
                    {
                        pClsProperty.purchase_id = Convert.ToInt32(p_dtbMasterId.Rows[0][0]);
                        pClsProperty.lot_srno = Val.ToInt64(p_dtbMasterId.Rows[0][1]);
                    }
                }
                else
                {
                    pClsProperty.purchase_id = 0;
                    pClsProperty.lot_srno = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }
        public int Delete_RoughPur_Sales(MFG_PurchaseProperty pClsProperty, DataTable p_dtbDetails)
        {
            Request Request = new Request();
            int IntRes = 0;
            try
            {
                foreach (DataRow drw in p_dtbDetails.Rows)
                {
                    Request RequestDetails = new Request();

                    RequestDetails.AddParams("@rough_purchase_id", Val.ToInt(drw["rough_purchase_id"]), DbType.Int32);
                    //RequestDetails.AddParams("@invoice_no", pClsProperty.invoice_no, DbType.Int32);
                    //RequestDetails.AddParams("@purchase_detail_id", Val.ToInt(drw["purchase_detail_id"]), DbType.Int32);
                    //RequestDetails.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                    //RequestDetails.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                    //RequestDetails.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                    //RequestDetails.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

                    RequestDetails.CommandText = BLL.TPV.SProc.TRN_RoughPur_Delete;
                    RequestDetails.CommandType = CommandType.StoredProcedure;

                    if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                    {
                        IntRes += Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, RequestDetails);
                    }
                    else
                    {
                        IntRes += Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, RequestDetails);
                    }
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
            }
            return IntRes;
        }
        public string CheckInvoiceNo(string InvNo)
        {
            DataTable DTab = new DataTable();
            string Invoice = "";
            Request Request = new Request();

            Request.AddParams("@invoice_no", InvNo, DbType.String);

            Request.CommandText = BLL.TPV.SProc.TRN_Rough_InvoiceNo_GetData;
            Request.CommandType = CommandType.StoredProcedure;

            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            }
            else
            {
                Ope.GetDataTable(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, DTab, Request);
            }
            if (DTab.Rows.Count > 0)
            {
                Invoice = Val.ToString(DTab.Rows[0]["invoice_no"]);
            }

            return Invoice;
        }
        public string CheckPurchaseInvoiceNo(string InvNo)
        {
            DataTable DTab = new DataTable();
            string Invoice = "";
            Request Request = new Request();

            Request.AddParams("@invoice_no", InvNo, DbType.String);

            Request.CommandText = BLL.TPV.SProc.MFG_TRN_InvoiceNo_GetData;
            Request.CommandType = CommandType.StoredProcedure;

            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            }
            else
            {
                Ope.GetDataTable(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, DTab, Request);
            }
            if (DTab.Rows.Count > 0)
            {
                Invoice = Val.ToString(DTab.Rows[0]["invoice_no"]);
            }

            return Invoice;
        }
        public int DeleteRoughPurchase(Int64 RoughPurchaseID)
        {
            int IntRes = 0;
            Request Request = new Request();

            Request.AddParams("@RoughPurchaseID", RoughPurchaseID, DbType.Int64);


            Request.CommandText = BLL.TPV.SProc.TRN_Rough_Purchase_Delete;
            Request.CommandType = CommandType.StoredProcedure;

            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                IntRes += Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
            }
            else
            {
                IntRes += Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, Request);
            }

            return IntRes;
        }
    }
}
