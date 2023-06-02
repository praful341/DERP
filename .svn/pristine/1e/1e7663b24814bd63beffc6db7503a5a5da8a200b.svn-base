using BLL.PropertyClasses.Transaction.Store;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction.Store
{
    public class StoreSales
    {
        InterfaceLayer Ope = new InterfaceLayer();
        BLL.Validation Val = new BLL.Validation();

        public Store_SalesProperty Save(Store_SalesProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@sales_id", pClsProperty.sales_id, DbType.Int64);
                Request.AddParams("@sales_date", pClsProperty.sales_date, DbType.Date);
                Request.AddParams("@bill_date", pClsProperty.bill_date, DbType.Date);
                Request.AddParams("@party_id", pClsProperty.party_id, DbType.Int64);
                Request.AddParams("@total_qty", pClsProperty.total_qty, DbType.Decimal);
                Request.AddParams("@total_rate", pClsProperty.total_rate, DbType.Decimal);
                Request.AddParams("@total_amount", pClsProperty.total_amount, DbType.Decimal);
                Request.AddParams("@sales_no", pClsProperty.sales_no, DbType.Int64);
                Request.AddParams("@s_bill_no", pClsProperty.s_bill_no, DbType.String);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int64);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int64);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int64);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int64);
                Request.AddParams("@entry_date", Val.DBDate(Val.DBDate(GlobalDec.gStr_SystemDate)), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

                Request.CommandText = BLL.TPV.SProc.Store_SalesMaster_Save;
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
                        pClsProperty.sales_id = Val.ToInt64(p_dtbMasterId.Rows[0][0]);
                    }
                }
                else
                {
                    pClsProperty.sales_id = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }
        public int Save_PurchaseDetail(Store_SalesProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                int IntRes = 0;
                Request stkRequest = new Request();
                stkRequest.AddParams("@sales_id", pClsProperty.sales_id, DbType.Int64);
                stkRequest.AddParams("@sales_detail_id", pClsProperty.sales_detail_id, DbType.Int64);
                stkRequest.AddParams("@item_id", pClsProperty.item_id, DbType.Int64);
                stkRequest.AddParams("@sub_item_id", pClsProperty.sub_item_id, DbType.Int64);
                stkRequest.AddParams("@remarks", pClsProperty.remarks, DbType.String);
                stkRequest.AddParams("@qty", pClsProperty.qty, DbType.Decimal);
                //stkRequest.AddParams("@unit", pClsProperty.unit, DbType.Int32);
                stkRequest.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                stkRequest.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
                stkRequest.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                stkRequest.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                stkRequest.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                stkRequest.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

                stkRequest.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int64);
                stkRequest.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int64);
                stkRequest.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int64);
                stkRequest.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int64);

                stkRequest.AddParams("@party_id", pClsProperty.party_id, DbType.Int64);
                stkRequest.AddParams("@item_condition", pClsProperty.item_condition, DbType.String);

                stkRequest.CommandText = BLL.TPV.SProc.Store_SalesDetail_Save;
                stkRequest.CommandType = CommandType.StoredProcedure;

                if (Conn != null)
                    IntRes = Conn.Inter1.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, stkRequest, pEnum);
                else
                    IntRes = Ope.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, stkRequest);

                return IntRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable GetData(string p_dtpFromDate, string p_dtpToDate, Int64 Party_ID)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.Store_SalesMaster_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@From_Date", p_dtpFromDate, DbType.Date);
            Request.AddParams("@To_Date", p_dtpToDate, DbType.Date);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@party_id", Party_ID, DbType.Int64);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);

            return DTab;
        }
        public DataTable GetDataDetails(int SalesId)
        {
            DataTable DTab = new DataTable();

            try
            {
                Request Request = new Request();
                Request.CommandText = BLL.TPV.SProc.Store_SalesDetail_GetDetail;
                Request.CommandType = CommandType.StoredProcedure;
                Request.AddParams("@sales_id", SalesId, DbType.Int32);

                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);

                return DTab;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return DTab;
            }
        }
        public DataTable GetRate(Int64 Item_ID, Int64 Sub_Item_ID)
        {
            DataTable DTab = new DataTable();

            try
            {
                Request Request = new Request();
                Request.CommandText = BLL.TPV.SProc.Store_Rate_SaleGetData;
                Request.CommandType = CommandType.StoredProcedure;
                Request.AddParams("@item_id", Item_ID, DbType.Int64);
                Request.AddParams("@sub_item_id", Sub_Item_ID, DbType.Int64);

                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);

                return DTab;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return DTab;
            }
        }
        public DataTable GetQty(Int64 Item_ID, Int64 Sub_Item_ID)
        {
            DataTable DTab = new DataTable();

            try
            {
                Request Request = new Request();
                Request.CommandText = BLL.TPV.SProc.STORE_Live_Stock_Get;
                Request.CommandType = CommandType.StoredProcedure;
                Request.AddParams("@item_id", Item_ID, DbType.Int64);
                Request.AddParams("@sub_item_id", Sub_Item_ID, DbType.Int64);

                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);

                return DTab;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return DTab;
            }
        }
        public string GetMaximumID(string StrIDType)
        {
            DataTable DtPreView = new DataTable();
            string RetMaxID = string.Empty;

            Request Request = new Request();
            Request.CommandType = CommandType.StoredProcedure;
            Request.CommandText = BLL.TPV.SProc.SL_Maximum_ID_GetData;
            Request.AddParams("@ID_NAME", StrIDType, DbType.String);
            Request.AddParams("@OUT_SRNO", "", DbType.String, ParameterDirection.Output);

            DataTable DTAB = new DataTable();
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTAB, Request);
            if (DTAB != null)
            {
                if (DTAB.Rows.Count > 0)
                {
                    RetMaxID = Convert.ToString(DTAB.Rows[0][0]);
                }
            }
            return RetMaxID;
        }
    }
}
