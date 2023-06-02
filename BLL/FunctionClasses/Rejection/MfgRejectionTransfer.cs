using BLL.PropertyClasses.Rejection;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Rejection
{
    public class MfgRejectionTransfer
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public MFGRejection_TransferProperty Save(MFGRejection_TransferProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();
                Request.AddParams("@transfer_id", pClsProperty.transfer_id, DbType.Int32);
                Request.AddParams("@transfer_date", pClsProperty.transfer_date, DbType.Date);
                Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int32);
                Request.AddParams("@cut_id", pClsProperty.cut_id, DbType.Int32);
                Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int32);
                Request.AddParams("@from_clarity_id", pClsProperty.from_clarity_id, DbType.Int32);
                Request.AddParams("@from_purity_id", pClsProperty.from_purity_id, DbType.Int32);
                Request.AddParams("@to_purity_id", pClsProperty.to_purity_id, DbType.Int32);
                Request.AddParams("@old_to_purity_id", pClsProperty.old_to_purity_id, DbType.Int32);
                Request.AddParams("@from_carat", pClsProperty.from_carat, DbType.Decimal);
                Request.AddParams("@from_rate", pClsProperty.from_rate, DbType.Decimal);
                Request.AddParams("@from_amount", pClsProperty.from_amount, DbType.Decimal);
                Request.AddParams("@to_carat", pClsProperty.to_carat, DbType.Decimal);
                Request.AddParams("@to_rate", pClsProperty.to_rate, DbType.Decimal);
                Request.AddParams("@to_amount", pClsProperty.to_amount, DbType.Decimal);
                Request.AddParams("@currency_id", GlobalDec.gEmployeeProperty.currency_id, DbType.Int32);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
                Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int32);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", pClsProperty.transfer_date, DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@prediction_id", pClsProperty.prediction_id, DbType.Int64);
                Request.AddParams("@dept_transfer_id", pClsProperty.dept_transfer_id, DbType.Int64);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Rejection_Transfer_Save;
                Request.CommandType = CommandType.StoredProcedure;

                DataTable p_dtbRejection = new DataTable();
                if (Conn != null)
                    Conn.Inter1.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, p_dtbRejection, Request, pEnum);
                else
                    Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, p_dtbRejection, Request);

                if (p_dtbRejection != null)
                {
                    if (p_dtbRejection.Rows.Count > 0)
                    {
                        pClsProperty.lot_srno = Val.ToInt64(p_dtbRejection.Rows[0][0]);
                        pClsProperty.transfer_id = Val.ToInt(p_dtbRejection.Rows[0][1]);
                    }
                }
                else
                {
                    pClsProperty.lot_srno = 0;
                    pClsProperty.transfer_id = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }
        public int Delete(MFGRejection_TransferProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            int IntRes = 0;
            try
            {
                Request Request = new Request();
                Request.AddParams("@transfer_id", pClsProperty.transfer_id, DbType.Int32);
                Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int32);
                Request.AddParams("@cut_id", pClsProperty.cut_id, DbType.Int32);
                Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int32);
                Request.AddParams("@from_clarity_id", pClsProperty.from_clarity_id, DbType.Int32);
                Request.AddParams("@from_purity_id", pClsProperty.from_purity_id, DbType.Int32);
                Request.AddParams("@to_purity_id", pClsProperty.to_purity_id, DbType.Int32);
                Request.AddParams("@from_carat", pClsProperty.from_carat, DbType.Decimal);
                Request.AddParams("@from_rate", pClsProperty.from_rate, DbType.Decimal);
                Request.AddParams("@from_amount", pClsProperty.from_amount, DbType.Decimal);
                Request.AddParams("@to_carat", pClsProperty.to_carat, DbType.Decimal);
                Request.AddParams("@to_rate", pClsProperty.to_rate, DbType.Decimal);
                Request.AddParams("@to_amount", pClsProperty.to_amount, DbType.Decimal);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
                Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int32);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Rejection_Transfer_Delete;
                Request.CommandType = CommandType.StoredProcedure;

                IntRes = Ope.ExecuteNonQuery(DBConnections.ConnectionString, DBConnections.ProviderName, Request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return IntRes;
        }
        public DataTable GetData(int kapanid, string cutid, string processid, string type)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_RejTransfer_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@kapan_id", kapanid, DbType.Int32);
            Request.AddParams("@cut_id", cutid, DbType.String);
            Request.AddParams("@process_id", processid, DbType.String);
            Request.AddParams("@type", type, DbType.Int32);
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetListData(string p_dtpFromDate, string p_dtpToDate)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Rejection_Transfer_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@From_Date", p_dtpFromDate, DbType.Int32);
            Request.AddParams("@To_Date", p_dtpToDate, DbType.Int32);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable FillListData(int LotSrno, int KapanId, int CutId, int ProcessId)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Rejection_Transfer_GetDetailData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@lot_srno", LotSrno, DbType.Int32);
            Request.AddParams("@kapan_id", KapanId, DbType.Int32);
            Request.AddParams("@cut_id", CutId, DbType.Int32);
            Request.AddParams("@process_id", ProcessId, DbType.Int32);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable FillSearchData(int KapanId, int CutId, int ProcessId)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Rejection_Transfer_SearchData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@kapan_id", KapanId, DbType.Int32);
            Request.AddParams("@cut_id", CutId, DbType.Int32);
            Request.AddParams("@process_id", ProcessId, DbType.Int32);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
    }
}
