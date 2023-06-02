using BLL.PropertyClasses.Transaction;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction
{
    public class BranchTransfer
    {
        #region "Data Member"
        InterfaceLayer Ope = new InterfaceLayer();
        BLL.Validation Val = new BLL.Validation();

        #endregion "Data Member"        

        #region "Functions" 
        public Branch_TransferProperty Save(Branch_TransferProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@bt_id", pClsProperty.bt_id, DbType.Int32);
                Request.AddParams("@from_company_id", pClsProperty.from_company_id, DbType.Int32);
                Request.AddParams("@to_company_id", pClsProperty.to_company_id, DbType.Int32);
                Request.AddParams("@from_branch_id", pClsProperty.from_branch_id, DbType.Int32);
                Request.AddParams("@to_branch_id", pClsProperty.to_branch_id, DbType.Int32);
                Request.AddParams("@from_location_id", pClsProperty.from_location_id, DbType.Int32);
                Request.AddParams("@to_location_id", pClsProperty.to_location_id, DbType.Int32);
                Request.AddParams("@from_department_id", pClsProperty.from_department_id, DbType.Int32);
                Request.AddParams("@to_department_id", pClsProperty.to_department_id, DbType.Int32);
                Request.AddParams("@bt_issue_date", pClsProperty.bt_issue_date, DbType.Date);
                Request.AddParams("@delivery_type_id", pClsProperty.delivery_type_id, DbType.Int32);
                Request.AddParams("@particulars", pClsProperty.particulars, DbType.String);
                Request.AddParams("@hsn", pClsProperty.hsn, DbType.Int32);
                Request.AddParams("@cgst_rate", pClsProperty.cgst_rate, DbType.Decimal);
                Request.AddParams("@cgst_amount", pClsProperty.cgst_amount, DbType.Decimal);
                Request.AddParams("@sgst_rate", pClsProperty.sgst_rate, DbType.Decimal);
                Request.AddParams("@sgst_amount", pClsProperty.sgst_amount, DbType.Decimal);
                Request.AddParams("@igst_rate", pClsProperty.igst_rate, DbType.Decimal);
                Request.AddParams("@igst_amount", pClsProperty.igst_amount, DbType.Decimal);
                Request.AddParams("@netamount", pClsProperty.netamount, DbType.Decimal);
                Request.AddParams("@remarks", pClsProperty.remarks, DbType.String);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@total_carat", pClsProperty.total_carat, DbType.Decimal);
                Request.AddParams("@currency_id", GlobalDec.gEmployeeProperty.currency_id, DbType.Int32);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);

                Request.CommandText = BLL.TPV.SProc.TRN_BT_Master_Save;
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
                        pClsProperty.bt_id = Convert.ToInt32(p_dtbMasterId.Rows[0][0]);
                    }
                }
                else
                {
                    pClsProperty.bt_id = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }

        public int Save_Detail(Branch_TransferProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                int IntRes = 0;

                Request RequestDetails = new Request();
                RequestDetails.AddParams("@bt_detail_id", pClsProperty.bt_detail_id, DbType.Int32);
                RequestDetails.AddParams("@bt_id", pClsProperty.bt_id, DbType.Int32);
                RequestDetails.AddParams("@assort_id", pClsProperty.assort_id, DbType.Int32);
                RequestDetails.AddParams("@sieve_id", pClsProperty.sieve_id, DbType.Int32);
                RequestDetails.AddParams("@from_company_id", pClsProperty.from_company_id, DbType.Int32);
                RequestDetails.AddParams("@from_branch_id", pClsProperty.from_branch_id, DbType.Int32);
                RequestDetails.AddParams("@from_location_id", pClsProperty.from_location_id, DbType.Int32);
                RequestDetails.AddParams("@from_department_id", pClsProperty.from_department_id, DbType.Int32);
                RequestDetails.AddParams("@to_company_id", pClsProperty.to_company_id, DbType.Int32);
                RequestDetails.AddParams("@to_branch_id", pClsProperty.to_branch_id, DbType.Int32);
                RequestDetails.AddParams("@to_location_id", pClsProperty.to_location_id, DbType.Int32);
                RequestDetails.AddParams("@to_department_id", pClsProperty.to_department_id, DbType.Int32);
                RequestDetails.AddParams("@pcs", pClsProperty.pcs, DbType.Int32);
                RequestDetails.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
                RequestDetails.AddParams("@discount", pClsProperty.discount, DbType.Decimal);
                RequestDetails.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                RequestDetails.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
                RequestDetails.AddParams("@currency_id", GlobalDec.gEmployeeProperty.currency_id, DbType.Int32);
                RequestDetails.AddParams("@rate_type_id", GlobalDec.gEmployeeProperty.rate_type_id, DbType.Int32);
                RequestDetails.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                RequestDetails.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                RequestDetails.AddParams("@current_rate", pClsProperty.current_rate, DbType.Decimal);
                RequestDetails.AddParams("@current_amount", pClsProperty.current_amount, DbType.Decimal);
                RequestDetails.AddParams("@old_carat", pClsProperty.old_carat, DbType.Decimal);
                RequestDetails.AddParams("@old_pcs", pClsProperty.old_pcs, DbType.Int32);
                RequestDetails.AddParams("@flag", pClsProperty.flag, DbType.Int32);
                RequestDetails.AddParams("@old_assort_id", pClsProperty.old_assort_id, DbType.Int32);
                RequestDetails.AddParams("@old_sieve_id", pClsProperty.old_sieve_id, DbType.Int32);
                RequestDetails.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                RequestDetails.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                RequestDetails.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);

                RequestDetails.CommandText = BLL.TPV.SProc.TRN_BT_Detail_Save;
                RequestDetails.CommandType = CommandType.StoredProcedure;
                
                    if (Conn != null)
                        IntRes = Conn.Inter1.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, RequestDetails, pEnum);
                    else
                        IntRes = Ope.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, RequestDetails);
                
                return IntRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string GetLetestPrice(int p_numAssort_ID, int p_numSieve_ID)
        {
            Request Request = new Request();
            DataTable DTable = new DataTable();
            string p_numLetest_Price = string.Empty;
            Request.AddParams("@numassort_id", p_numAssort_ID, DbType.Int32);
            Request.AddParams("@numsieve_id", p_numSieve_ID, DbType.Int32);
            Request.AddParams("@rate_type_id", GlobalDec.gEmployeeProperty.rate_type_id, DbType.Int32);

            Request.CommandText = BLL.TPV.SProc.MST_Get_LetestPrice;
            Request.CommandType = CommandType.StoredProcedure;
            
                p_numLetest_Price = Ope.ExecuteScalar(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
            
            return p_numLetest_Price;
        }
        public DataTable GetData(string p_dtpFromDate, string p_dtpToDate)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.TRN_BT_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@From_Date", p_dtpFromDate, DbType.Date);
            Request.AddParams("@To_Date", p_dtpToDate, DbType.Date);
            Request.AddParams("@from_company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@from_branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@from_location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@from_department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);           
            return DTab;
        }
        public DataTable GetDataDetails(int p_numID)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.TRN_BT_GetDetailsData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@p_numBT_ID", p_numID, DbType.Int32);
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);            
            return DTab;
        }

        public DataTable GetLetestNo(int p_numSieve_ID, int p_numColor_id, double from_rate, double to_rate)
        {
            Request Request = new Request();
            DataTable DTab = new DataTable();
            string p_numLetest_Price = string.Empty;
            Request.AddParams("@numsieve_id", p_numSieve_ID, DbType.Int32);
            Request.AddParams("@numcolor_id", p_numColor_id, DbType.Int32);
            Request.AddParams("@rate_type_id", GlobalDec.gEmployeeProperty.rate_type_id, DbType.Int32);
            Request.AddParams("@numfrom_rate", from_rate, DbType.Decimal);
            Request.AddParams("@numto_rate", to_rate, DbType.Decimal);

            Request.CommandText = BLL.TPV.SProc.MST_Get_SujestedNo;
            Request.CommandType = CommandType.StoredProcedure;
            
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);            
            return DTab;
        }

        public Branch_TransferProperty Branch_Transfer_Delete(Branch_TransferProperty pClsProperty)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@bt_id", pClsProperty.bt_id, DbType.String);

                Request.CommandText = BLL.TPV.SProc.TRN_BranchTransfer_Delete;
                Request.CommandType = CommandType.StoredProcedure;

                DataTable p_dtbMasterId = new DataTable();

                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, p_dtbMasterId, Request);

                if (p_dtbMasterId != null)
                {
                    if (p_dtbMasterId.Rows.Count > 0)
                    {
                        pClsProperty.bt_id = Val.ToInt32(p_dtbMasterId.Rows[0][0]);
                    }
                }
                else
                {
                    pClsProperty.bt_id = Val.ToInt32(0);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }

        #endregion"Functions"
    }
}
