using BLL.PropertyClasses.Transaction.Store;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction.Store
{
    public class StoreSalesReturn
    {
        InterfaceLayer Ope = new InterfaceLayer();
        BLL.Validation Val = new BLL.Validation();

        public Store_SalesReturnProperty Save(Store_SalesReturnProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@sales_return_id", pClsProperty.sales_return_id, DbType.Int64);
                Request.AddParams("@sales_return_date", pClsProperty.sales_return_date, DbType.Date);
                Request.AddParams("@party_id", pClsProperty.party_id, DbType.Int64);
                Request.AddParams("@total_return_qty", pClsProperty.total_return_qty, DbType.Decimal);
                Request.AddParams("@total_return_rate", pClsProperty.total_return_rate, DbType.Decimal);
                Request.AddParams("@total_return_amount", pClsProperty.total_return_amount, DbType.Decimal);
                Request.AddParams("@total_rejection_qty", pClsProperty.total_rejection_qty, DbType.Decimal);
                Request.AddParams("@total_rejection_rate", pClsProperty.total_rejection_rate, DbType.Decimal);
                Request.AddParams("@total_rejection_amount", pClsProperty.total_rejection_amount, DbType.Decimal);
                Request.AddParams("@sales_return_no", pClsProperty.sales_return_no, DbType.Int64);
                Request.AddParams("@s_ret_bill_no", pClsProperty.s_ret_bill_no, DbType.String);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int64);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int64);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int64);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int64);
                Request.AddParams("@entry_date", Val.DBDate(Val.DBDate(GlobalDec.gStr_SystemDate)), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

                Request.CommandText = BLL.TPV.SProc.Store_SalesReturnMaster_Save;
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
                        pClsProperty.sales_return_id = Val.ToInt64(p_dtbMasterId.Rows[0][0]);
                    }
                }
                else
                {
                    pClsProperty.sales_return_id = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }
        public int Save_SalesReturnDetail(Store_SalesReturnProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                int IntRes = 0;
                Request stkRequest = new Request();
                stkRequest.AddParams("@sales_return_id", pClsProperty.sales_return_id, DbType.Int64);
                stkRequest.AddParams("@sales_return_detail_id", pClsProperty.sales_return_detail_id, DbType.Int64);
                stkRequest.AddParams("@item_id", pClsProperty.item_id, DbType.Int64);
                stkRequest.AddParams("@sub_item_id", pClsProperty.sub_item_id, DbType.Int64);
                stkRequest.AddParams("@remarks", pClsProperty.remarks, DbType.String);
                stkRequest.AddParams("@return_qty", pClsProperty.retun_qty, DbType.Decimal);
                stkRequest.AddParams("@return_rate", pClsProperty.retun_rate, DbType.Decimal);
                stkRequest.AddParams("@return_amount", pClsProperty.retun_amount, DbType.Decimal);
                stkRequest.AddParams("@rejection_qty", pClsProperty.rejection_qty, DbType.Decimal);
                stkRequest.AddParams("@rejection_rate", pClsProperty.rejection_rate, DbType.Decimal);
                stkRequest.AddParams("@rejection_amount", pClsProperty.rejection_amount, DbType.Decimal);
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

                stkRequest.CommandText = BLL.TPV.SProc.Store_SalesReturnDetail_Save;
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
            Request.CommandText = BLL.TPV.SProc.Store_SalesReturnMaster_GetData;
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
        public DataTable GetDataDetails(int SalesReturnId)
        {
            DataTable DTab = new DataTable();

            try
            {
                Request Request = new Request();
                Request.CommandText = BLL.TPV.SProc.Store_SalesReturnDetail_GetDetail;
                Request.CommandType = CommandType.StoredProcedure;
                Request.AddParams("@sales_return_id", SalesReturnId, DbType.Int32);

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
