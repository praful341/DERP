using BLL.PropertyClasses.Transaction.MFG;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction.MFG
{
    public class MFGAssortFinalOkSizeWise
    {
        InterfaceLayer Ope = new InterfaceLayer();
        BLL.Validation Val = new BLL.Validation();

        public MFGAssortFinal_OKProperty Save(MFGAssortFinal_OKProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                //Request.AddParams("@receive_date", pClsProperty.receive_date, DbType.Date);
                Request.AddParams("@flag", pClsProperty.flag, DbType.Int32);
                Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int32);
                Request.AddParams("@rough_cut_id", pClsProperty.rough_cut_id, DbType.Int32);
                Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int32);
                Request.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int32);
                Request.AddParams("@assort_id", pClsProperty.assort_id, DbType.Int32);
                Request.AddParams("@sieve_id", pClsProperty.sieve_id, DbType.Int32);
                Request.AddParams("@carat", pClsProperty.to_carat, DbType.Decimal);
                Request.AddParams("@percentage", pClsProperty.percentage, DbType.Decimal);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);
                Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);
                //Request.AddParams("@temp_sieve_name", pClsProperty.temp_sieve_name, DbType.String);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_AssortFinalSize_OK_Save;
                Request.CommandType = CommandType.StoredProcedure;

                DataTable p_dtbProcessRecId = new DataTable();
                if (Conn != null)
                    Conn.Inter1.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, p_dtbProcessRecId, Request, pEnum);
                else
                    Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, p_dtbProcessRecId, Request);

                if (p_dtbProcessRecId != null)
                {
                    if (p_dtbProcessRecId.Rows.Count > 0)
                    {
                        pClsProperty.lot_srno = Val.ToInt64(p_dtbProcessRecId.Rows[0][0]);
                    }
                }
                else
                {
                    pClsProperty.lot_srno = 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }
        public DataTable AssortFinalSizeGetData(string Assort, string Sieve)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Assort_FinalOKSize_GetData;

            Request.AddParams("@assort_id", Assort, DbType.String);
            Request.AddParams("@sieve_id", Sieve, DbType.String);

            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetData(string FDate, string TDate)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Assort_FinalOKSize_GetSearchData;

            Request.AddParams("@from_date", FDate, DbType.Date);
            Request.AddParams("@to_date", TDate, DbType.Date);

            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public int CheckGetData(int KapanId, int CutId, int LotSrno)
        {
            DataTable DTab = new DataTable();
            int Status = 0;
            Request Request = new Request();
            Request.AddParams("@kapan_id", KapanId, DbType.Int32);
            Request.AddParams("@rough_cut_id", CutId, DbType.Int32);
            Request.AddParams("@lot_srno", LotSrno, DbType.Int32);
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Assort_FinalOKSize_Check_GetData;
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            if (DTab.Rows.Count > 0)
            {
                Status = 1;
            }
            else
            {
                Status = 0;
            }
            return Status;
        }
        public DataTable GetListData(int Lotsrno)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Assort_FinalOKSize_GetListData;

            Request.AddParams("@lot_srno", Lotsrno, DbType.Int32);

            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public int Delete(MFGAssortFinal_LottingProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            int Intres = 0;
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_AssortFinalSize_OK_Delete;

            Request.AddParams("@lot_srno", pClsProperty.Del_lot_srno, DbType.Int32);

            Request.CommandType = CommandType.StoredProcedure;
            Intres = Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
            return Intres;
        }
    }
}
