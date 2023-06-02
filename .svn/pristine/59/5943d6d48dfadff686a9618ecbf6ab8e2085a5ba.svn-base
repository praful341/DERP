using BLL.PropertyClasses.Transaction;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction
{
    public class AssortMappingMaster
    {
        InterfaceLayer Ope = new InterfaceLayer();
        BLL.Validation Val = new BLL.Validation();

        public AssortMappingMasterProperty Save(AssortMappingMasterProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@assort_mapping_id", pClsProperty.assort_mapping_id, DbType.Int64);
                Request.AddParams("@assort_union_id", pClsProperty.assort_union_id, DbType.Int64);
                Request.AddParams("@assort_mapping_date", pClsProperty.assort_mapping_date, DbType.Date);
                Request.AddParams("@from_assort_id", pClsProperty.from_assort_id, DbType.Int32);
                Request.AddParams("@to_assort_id", pClsProperty.to_assort_id, DbType.Int32);
                Request.AddParams("@from_sieve_id", pClsProperty.from_sieve_id, DbType.Int32);
                Request.AddParams("@to_sieve_id", pClsProperty.to_sieve_id, DbType.Int32);
                Request.AddParams("@percentage", pClsProperty.percentage, DbType.Decimal);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int32);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@entry_date", Val.DBDate(Val.DBDate(GlobalDec.gStr_SystemDate)), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

                Request.CommandText = BLL.TPV.SProc.TRN_Assort_Mapping_Save;
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
                        pClsProperty.assort_union_id = Val.ToInt64(p_dtbMasterId.Rows[0][0]);
                    }
                }
                else
                {
                    pClsProperty.assort_union_id = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }
        public DataTable GetData(string p_dtpFromDate, string p_dtpToDate)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.TRN_Assort_Mapping_Search_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@From_Date", p_dtpFromDate, DbType.Date);
            Request.AddParams("@To_Date", p_dtpToDate, DbType.Date);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);           
            return DTab;
        }
        public DataTable GetDataDetails(int p_numID)
        {
            DataTable DTab = new DataTable();

            try
            {
                Request Request = new Request();
                Request.CommandText = BLL.TPV.SProc.TRN_Assort_Mapping_GetDetailsData;
                Request.CommandType = CommandType.StoredProcedure;
                Request.AddParams("@p_numAssortunion_ID", p_numID, DbType.Int32);
                
                    Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);                
                return DTab;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return DTab;
            }
        }
    }
}
