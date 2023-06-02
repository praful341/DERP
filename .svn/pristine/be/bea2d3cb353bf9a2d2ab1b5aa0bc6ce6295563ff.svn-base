using BLL.PropertyClasses.Transaction.MFG;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction.MFG
{
    public class MFGFactorySplit
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public MFG_Factory_SplitProperty Save(MFG_Factory_SplitProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@lot_no", pClsProperty.lot_no, DbType.Int64);
                Request.AddParams("@sub_lot_no", pClsProperty.sub_lot_no, DbType.String);

                Request.AddParams("@rough_cut_id", pClsProperty.rough_cut_id, DbType.Int32);
                Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int32);
                Request.AddParams("@from_lot_id", pClsProperty.from_lot_id, DbType.Int32);
                Request.AddParams("@receive_date", pClsProperty.mix_split_date, DbType.Date);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
                Request.AddParams("@union_id", pClsProperty.union_id, DbType.Int64);
                Request.AddParams("@prediction_id", pClsProperty.prediction_id, DbType.Int64);
                Request.AddParams("@purity_id", pClsProperty.purity_id, DbType.Int64);
                Request.AddParams("@quality_id", pClsProperty.quality_id, DbType.Int64);
                Request.AddParams("@rough_clarity_id", pClsProperty.rough_clarity_id, DbType.Int64);
                Request.AddParams("@rough_sieve_id", pClsProperty.rough_sieve_id, DbType.Int64);
                Request.AddParams("@manager_id", pClsProperty.manager_id, DbType.Int32);
                Request.AddParams("@employee_id", pClsProperty.employee_id, DbType.Int32);
                Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int32);
                Request.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int32);
                Request.AddParams("@from_pcs", pClsProperty.from_pcs, DbType.Int32);
                Request.AddParams("@from_carat", pClsProperty.from_carat, DbType.Decimal);
                Request.AddParams("@pcs", pClsProperty.to_pcs, DbType.Int32);
                Request.AddParams("@carat", pClsProperty.to_carat, DbType.Decimal);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);

                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);
                Request.AddParams("@history_union_id", pClsProperty.history_union_id, DbType.Int64);
                Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);
                Request.AddParams("@flag", pClsProperty.flag, DbType.Int32);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Factory_Split_Save;
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
                        pClsProperty.union_id = Val.ToInt64(p_dtbProcessUnionId.Rows[0][0]);
                        pClsProperty.history_union_id = Val.ToInt64(p_dtbProcessUnionId.Rows[0][1]);
                        pClsProperty.to_lot_id = Val.ToInt64(p_dtbProcessUnionId.Rows[0][2]);
                        pClsProperty.lot_srno = Val.ToInt64(p_dtbProcessUnionId.Rows[0][3]);
                    }
                }
                else
                {
                    pClsProperty.union_id = 0;
                    pClsProperty.history_union_id = 0;
                    pClsProperty.to_lot_id = 0;
                    pClsProperty.lot_srno = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }
        
        public MFG_Factory_SplitProperty Russion_Save(MFG_Factory_SplitProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@lot_no", pClsProperty.lot_no, DbType.Int64);
                Request.AddParams("@sub_lot_no", pClsProperty.sub_lot_no, DbType.String);

                Request.AddParams("@rough_cut_id", pClsProperty.rough_cut_id, DbType.Int32);
                Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int32);
                Request.AddParams("@from_lot_id", pClsProperty.from_lot_id, DbType.Int32);
                Request.AddParams("@receive_date", pClsProperty.mix_split_date, DbType.Date);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
                Request.AddParams("@union_id", pClsProperty.union_id, DbType.Int64);
                Request.AddParams("@prediction_id", pClsProperty.prediction_id, DbType.Int64);
                Request.AddParams("@purity_id", pClsProperty.purity_id, DbType.Int64);
                Request.AddParams("@quality_id", pClsProperty.quality_id, DbType.Int64);
                Request.AddParams("@rough_clarity_id", pClsProperty.rough_clarity_id, DbType.Int64);
                Request.AddParams("@rough_sieve_id", pClsProperty.rough_sieve_id, DbType.Int64);
                Request.AddParams("@manager_id", pClsProperty.manager_id, DbType.Int32);
                Request.AddParams("@employee_id", pClsProperty.employee_id, DbType.Int32);
                Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int32);
                Request.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int32);
                Request.AddParams("@from_pcs", pClsProperty.from_pcs, DbType.Int32);
                Request.AddParams("@from_carat", pClsProperty.from_carat, DbType.Decimal);
                Request.AddParams("@pcs", pClsProperty.to_pcs, DbType.Int32);
                Request.AddParams("@carat", pClsProperty.to_carat, DbType.Decimal);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);

                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);
                Request.AddParams("@history_union_id", pClsProperty.history_union_id, DbType.Int64);
                Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);
                Request.AddParams("@flag", pClsProperty.flag, DbType.Int32);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Russion_Split_Save;
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
                        pClsProperty.union_id = Val.ToInt64(p_dtbProcessUnionId.Rows[0][0]);
                        pClsProperty.history_union_id = Val.ToInt64(p_dtbProcessUnionId.Rows[0][1]);
                        pClsProperty.to_lot_id = Val.ToInt64(p_dtbProcessUnionId.Rows[0][2]);
                        pClsProperty.lot_srno = Val.ToInt64(p_dtbProcessUnionId.Rows[0][3]);
                    }
                }
                else
                {
                    pClsProperty.union_id = 0;
                    pClsProperty.history_union_id = 0;
                    pClsProperty.to_lot_id = 0;
                    pClsProperty.lot_srno = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }
        public DataTable GetLotData(Int64 LotId)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_LotDetail_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@lot_id", LotId, DbType.Int64);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable CheckProcess(Int64 LotId, string process)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_LotProcess_Check;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@lot_id", LotId, DbType.Int64);
            Request.AddParams("@process", process, DbType.String);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public Int64 FindMaxLotNo(Int64 Lot_id)
        {
            Int64 NewLotNo = 0;
            NewLotNo = Ope.FindNewLotNo(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "MFG_TRN_Stock", "lot_no", " AND Lot_id=" + Lot_id);

            return NewLotNo;
        }
        public string FindMaxSubLotNo(Int64 Rough_Cut_ID)
        {
            string NewLotNo = "";
            NewLotNo = Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "MFG_TRN_Stock", "MAX(convert(int, PARSENAME(replace(sub_lot_no, '-', '.'), 1)))", " AND rough_cut_id=" + Rough_Cut_ID);

            return NewLotNo;
        }
        public int GetSrNo(int Cut_id, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            int Srno = 0;
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_CutWise_SrNo_GetData;

            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@cut_id", Cut_id, DbType.Int32);
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Conn.Inter1.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, DTab, Request, pEnum);
            if (DTab.Rows.Count > 0)
            {
                Srno = Val.ToInt(DTab.Rows[0]["sr_no"]);
            }
            else
            {
                Srno = 0;
            }
            return Srno;
        }
    }
}
