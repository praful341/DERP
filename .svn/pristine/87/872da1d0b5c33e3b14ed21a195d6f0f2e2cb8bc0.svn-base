using BLL.PropertyClasses.Transaction;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction
{
    public class PriceImport
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();

        public int MasterSave(PriceImportProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@rate_date", pClsProperty.rate_date, DbType.Date);
                Request.AddParams("@rate_type_id", (object)pClsProperty.rate_type_id ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@currency_id", (object)pClsProperty.currency_id ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);
                Request.CommandText = BLL.TPV.SProc.TRN_Price_Import_Save;
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
                        pClsProperty.rate_id = Convert.ToInt32(p_dtbMasterId.Rows[0][0]);
                    }
                }
                else
                {
                    pClsProperty.rate_id = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty.rate_id;
        }

        public int Purity_Rate_MasterSave(PriceImportProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@rate_date", pClsProperty.rate_date, DbType.Date);
                Request.AddParams("@rate_type_id", (object)pClsProperty.rate_type_id ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@currency_id", (object)pClsProperty.currency_id ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);
                Request.CommandText = BLL.TPV.SProc.MFG_Purity_Price_Import_Save;
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
                        pClsProperty.rate_id = Convert.ToInt32(p_dtbMasterId.Rows[0][0]);
                    }
                }
                else
                {
                    pClsProperty.rate_id = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty.rate_id;
        }

        public int DetailSave(PriceImportProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                int IntRes = 0;
                Request Request = new Request();

                Request.AddParams("@rate_id", (object)pClsProperty.rate_id ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@assort_id", (object)pClsProperty.assort_id ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@sieve_id", (object)pClsProperty.sieve_id ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@sequence_no", (object)pClsProperty.sequence_no ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@count", (object)pClsProperty.count ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@rate_date", pClsProperty.rate_date, DbType.Date);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@rate_type_id", (object)pClsProperty.rate_type_id ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@currency_id", (object)pClsProperty.currency_id ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);
                Request.AddParams("@flag", (object)pClsProperty.flag ?? DBNull.Value, DbType.Int32);

                Request.CommandText = BLL.TPV.SProc.TRN_Price_Import_Detail_Save;
                Request.CommandType = CommandType.StoredProcedure;                
                    if (Conn != null)
                        IntRes = Conn.Inter1.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request, pEnum);
                    else
                        IntRes = Ope.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request);
                
                return IntRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int RangeMasterSave(PriceImportProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                int IntRes = 0;
                Request Request = new Request();

                Request.AddParams("@range_id", (object)pClsProperty.range_id ?? DBNull.Value, DbType.Int64);
                Request.AddParams("@color_id", (object)pClsProperty.color_id ?? DBNull.Value, DbType.Int64);
                Request.AddParams("@sieve_id", (object)pClsProperty.sieve_id ?? DBNull.Value, DbType.Int64);
                Request.AddParams("@from_rate", pClsProperty.from_rate, DbType.Decimal);
                Request.AddParams("@to_rate", pClsProperty.to_rate, DbType.Decimal);
                Request.AddParams("@range", pClsProperty.range, DbType.String);
                Request.AddParams("@color_name", pClsProperty.color_name, DbType.String);
                Request.AddParams("@size_name", pClsProperty.size_name, DbType.String);

                Request.CommandText = BLL.TPV.SProc.Range_Master_Save;
                Request.CommandType = CommandType.StoredProcedure;

                if (Conn != null)
                    IntRes = Conn.Inter1.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request, pEnum);
                else
                    IntRes = Ope.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request);
                return IntRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int PurityPrice_DetailSave(PriceImportProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                int IntRes = 0;
                Request Request = new Request();

                Request.AddParams("@rate_id", (object)pClsProperty.rate_id ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@purity_id", (object)pClsProperty.purity_id ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@sieve_id", (object)pClsProperty.sieve_id ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@sequence_no", (object)pClsProperty.sequence_no ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@per_pcs", pClsProperty.per_pcs, DbType.Decimal);
                Request.AddParams("@per_carat", pClsProperty.per_carat, DbType.Decimal);
                Request.AddParams("@janged_per_carat", pClsProperty.janged_per_carat, DbType.Decimal);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@rate_type_id", (object)pClsProperty.rate_type_id ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@currency_id", (object)pClsProperty.currency_id ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);

                Request.CommandText = BLL.TPV.SProc.MFG_Purity_Price_Import_Detail_Save;
                Request.CommandType = CommandType.StoredProcedure;                
                    if (Conn != null)
                        IntRes = Conn.Inter1.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request, pEnum);
                    else
                        IntRes = Ope.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request);                
                return IntRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetData(string p_dtpDate, int rate_type_id, int curr_id)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.TRN_Price_Import_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@Date", p_dtpDate, DbType.Int32);
            Request.AddParams("@rate_type_id", rate_type_id, DbType.Int32);
            Request.AddParams("@currency_id", curr_id, DbType.Int32);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
            
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);           
            return DTab;
        }
        public DataTable Price_GetData(string p_dtpDate, int rate_type_id, int curr_id)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.TRN_Price_Import_DataExists;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@Date", p_dtpDate, DbType.Int32);
            Request.AddParams("@rate_type_id", rate_type_id, DbType.Int32);
            Request.AddParams("@currency_id", curr_id, DbType.Int32);
            
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetOldData(int rate_type_id, int curr_id)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.TRN_Price_Import_GetOldData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@rate_type_id", rate_type_id, DbType.Int32);
            Request.AddParams("@currency_id", curr_id, DbType.Int32);
            
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetPriceData(string p_dtpDate, int rate_type_id, int curr_id)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.TRN_Price_Import_GetPriceData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@Date", p_dtpDate, DbType.String);
            Request.AddParams("@rate_type_id", rate_type_id, DbType.Int32);
            Request.AddParams("@currency_id", curr_id, DbType.Int32);
            
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetCostData(int numrate_type_id, int numcurrency_id, int numDays, decimal numPer, decimal numRate, string p_dtpOLD_date)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MST_CostRate_Generate;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@rate_type_id", numrate_type_id, DbType.Int32);
            Request.AddParams("@currency_id", numcurrency_id, DbType.Int32);

            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

            Request.AddParams("@days", numDays, DbType.Int32);
            Request.AddParams("@per", numPer, DbType.Decimal);
            Request.AddParams("@rate", numRate, DbType.Decimal);
            Request.AddParams("@old_rate_date", p_dtpOLD_date, DbType.Date);
            
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetSaleData(int numrate_type_id, int numcurrency_id)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.Get_Monthwise_SaleSummary;
            Request.CommandType = CommandType.StoredProcedure;

            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

            Request.AddParams("@rate_type_id", numrate_type_id, DbType.Int32);
            Request.AddParams("@currency_id", numcurrency_id, DbType.Int32);
            
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public int RoughPriceSave(PriceImportProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                int IntRes = 0;
                Request Request = new Request();

                Request.AddParams("@rough_rate_id", (object)pClsProperty.rough_rate_id ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@rate_date", (object)pClsProperty.rate_date ?? DBNull.Value, DbType.Date);
                Request.AddParams("@rough_clarity_id", (object)pClsProperty.rough_clarity_id ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@rough_sieve_id", (object)pClsProperty.rough_sieve_id ?? DBNull.Value, DbType.Int32);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@remarks", pClsProperty.remarks, DbType.String);

                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

                Request.CommandText = BLL.TPV.SProc.MFG_MST_RoughPrice_Import;
                Request.CommandType = CommandType.StoredProcedure;
                
                    if (Conn != null)
                        IntRes = Conn.Inter1.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request, pEnum);
                    else
                        IntRes = Ope.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request);               
                return IntRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int RoughPriceDelete(PriceImportProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                int IntRes = 0;
                Request Request = new Request();

                Request.AddParams("@rate_date", (object)pClsProperty.rate_date ?? DBNull.Value, DbType.Date);

                Request.CommandText = BLL.TPV.SProc.MFG_MST_RoughPrice_Delete;
                Request.CommandType = CommandType.StoredProcedure;
                
                    if (Conn != null)
                        IntRes = Conn.Inter1.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request, pEnum);
                    else
                        IntRes = Ope.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request);
                return IntRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetOld_PriceDate(int numrate_type_id, int numcurrency_id)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.TRN_Get_OLD_Price_Date;
            Request.CommandType = CommandType.StoredProcedure;

            Request.AddParams("@rate_type_id", numrate_type_id, DbType.Int32);
            Request.AddParams("@currency_id", numcurrency_id, DbType.Int32);
            
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);           
            return DTab;
        }

        public int PartyOpeningSave(StorePartyOpeningProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                int IntRes = 0;
                Request Request = new Request();

                Request.AddParams("@opening_date", pClsProperty.opening_date, DbType.Date);
                Request.AddParams("@party_id", pClsProperty.party_id, DbType.Int64);
                Request.AddParams("@item_id", pClsProperty.item_id, DbType.Int64);
                Request.AddParams("@sub_item_id", pClsProperty.sub_item_id, DbType.Int64);
                Request.AddParams("@qty", pClsProperty.qty, DbType.Decimal);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int64);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int64);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int64);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int64);

                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

                Request.CommandText = BLL.TPV.SProc.TRN_Store_Party_Opening_Save;
                Request.CommandType = CommandType.StoredProcedure;

                if (Conn != null)
                    IntRes = Conn.Inter1.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request, pEnum);
                else
                    IntRes = Ope.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request);

                return IntRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DeletePriceImportDetail(PriceImportProperty pClsProperty)
        {
            int IntRes = 0;
            Request Request = new Request();

            Request.AddParams("@rate_detail_id", pClsProperty.rate_detail_id, DbType.Int64, ParameterDirection.Input);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

            Request.CommandText = BLL.TPV.SProc.Price_Import_Detail_Delete;
            Request.CommandType = CommandType.StoredProcedure;
            IntRes += Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);

            return IntRes;
        }
        public int DeleteRangeMaster(PriceImportProperty pClsProperty)
        {
            int IntRes = 0;
            Request Request = new Request();

            Request.AddParams("@range_id", pClsProperty.range_id, DbType.Int64, ParameterDirection.Input);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

            Request.CommandText = BLL.TPV.SProc.Range_Master_Delete;
            Request.CommandType = CommandType.StoredProcedure;
            IntRes += Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);

            return IntRes;
        }
    }
}
