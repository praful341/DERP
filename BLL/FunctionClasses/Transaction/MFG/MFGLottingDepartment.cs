using BLL.PropertyClasses.Transaction;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction
{
    public class MFGLottingDepartment
    {
        InterfaceLayer Ope = new InterfaceLayer();
        BLL.Validation Val = new BLL.Validation();

        public DataTable Carat_GetData(Int64 Kapan_ID, Int64 Rough_Cut_ID, string Lotting_Dept)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Final_Rate_GetData;

            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@kapan_id", Kapan_ID, DbType.Int64);
            Request.AddParams("@cut_id", Rough_Cut_ID, DbType.Int64);
            Request.AddParams("@lotting_dept", Lotting_Dept, DbType.String);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable CaratWise_GetData(Int64 Kapan_ID, Int64 Rough_Cut_ID, string Lotting_Dept)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Final_Rate_NewData;

            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@kapan_id", Kapan_ID, DbType.Int64);
            Request.AddParams("@cut_id", Rough_Cut_ID, DbType.Int64);
            Request.AddParams("@lotting_dept", Lotting_Dept, DbType.String);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetSarinData(MFGLottingDepartmentProperty pClsProperty)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Final_Rate_SarinData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int64);
            Request.AddParams("@cut_id", pClsProperty.rough_cut_id, DbType.Int64);
            Request.AddParams("@lotting_dept", pClsProperty.lotting_department_name, DbType.String);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public Int64 Save_MFGLottingDepartment(MFGLottingDepartmentProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            Int64 IntRes = 0;

            try
            {
                Request Request = new Request();

                Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int64);
                Request.AddParams("@cut_id", pClsProperty.rough_cut_id, DbType.Int64);
                Request.AddParams("@Loting_dept", pClsProperty.lotting_department_name, DbType.String);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@type", pClsProperty.type, DbType.String);

                Request.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
                Request.AddParams("@main_rate", pClsProperty.main_rate, DbType.Decimal);
                Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Update_Lotting_Rate;
                Request.CommandType = CommandType.StoredProcedure;

                //if (Conn != null)
                //    IntRes = Conn.Inter1.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request, pEnum);
                //else
                //    IntRes = Ope.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request);

                DataTable p_dtbProcessUnionId = new DataTable();
                if (Conn != null)
                    Conn.Inter1.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, p_dtbProcessUnionId, Request, pEnum);
                else
                    Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, p_dtbProcessUnionId, Request);

                if (p_dtbProcessUnionId != null)
                {
                    if (p_dtbProcessUnionId.Rows.Count > 0)
                    {
                        IntRes = Val.ToInt64(p_dtbProcessUnionId.Rows[0][0]);
                    }
                    else
                    {
                        IntRes = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IntRes;
        }
        public DataTable Kapan_Carat_GetData(Int64 Kapan_ID)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Kapan_Rate_GetData;

            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@kapan_id", Kapan_ID, DbType.Int64);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable CutNo_Carat_GetData(Int64 Kapan_ID, Int64 Cut_ID)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Cut_Rate_GetData;

            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@kapan_id", Kapan_ID, DbType.Int64);
            Request.AddParams("@cut_id", Cut_ID, DbType.Int64);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataSet GetSawableData(MFGLottingDepartmentProperty pClsProperty)
        {
            DataSet DS = new DataSet();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Cleaning_Rate_Data;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int64);
            Request.AddParams("@cut_id", pClsProperty.rough_cut_id, DbType.Int64);

            Ope.GetDataSet(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DS, "", Request);
            return DS;
        }
        public Int64 Save_MFGRough_CleningDepartment(MFGLottingDepartmentProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            Int64 IntRes = 0;
            try
            {
                Request Request = new Request();

                Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int64);
                Request.AddParams("@cut_id", pClsProperty.rough_cut_id, DbType.Int64);
                Request.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int64);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@type", pClsProperty.type, DbType.String);
                Request.AddParams("@type_sawable_purity", pClsProperty.type_sawable_purity, DbType.String);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Update_Cleaning_Rate;
                Request.CommandType = CommandType.StoredProcedure;

                if (Conn != null)
                    IntRes = Conn.Inter1.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request, pEnum);
                else
                    IntRes = Ope.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IntRes;
        }
    }
}
