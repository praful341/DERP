using BLL.PropertyClasses.Transaction.MFG;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction.MFG
{
    public class MFGDepartmentTransfer
    {
        InterfaceLayer Ope = new InterfaceLayer();
        BLL.Validation Val = new BLL.Validation();

        public MFGDepartmentTransferProperty Save(MFGDepartmentTransferProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();
                Request.AddParams("@union_id", pClsProperty.union_id, DbType.Int64);
                Request.AddParams("@transfer_date", pClsProperty.transfer_date, DbType.Date);
                Request.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int32);
                Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int32);
                Request.AddParams("@cut_id", pClsProperty.cut_id, DbType.Int32);

                Request.AddParams("@to_company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@to_branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@to_location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@to_department_id", pClsProperty.to_department_id, DbType.Int64);
                Request.AddParams("@to_manager_id", pClsProperty.to_manager_id, DbType.Int64);
                Request.AddParams("@pcs", pClsProperty.pcs, DbType.Int32);
                Request.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
                Request.AddParams("@rr_carat", pClsProperty.rr_carat, DbType.Decimal);
                Request.AddParams("@rr_pcs", pClsProperty.rr_pcs, DbType.Int32);

                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);
                Request.AddParams("@to_process_id", pClsProperty.to_process_id, DbType.Int64);
                Request.AddParams("@history_union_id", pClsProperty.history_union_id, DbType.Int64);
                Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Department_Transfer_Save;
                Request.CommandType = CommandType.StoredProcedure;

                DataTable p_dtbDeptTrfId = new DataTable();
                if (Conn != null)
                    Conn.Inter1.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, p_dtbDeptTrfId, Request, pEnum);
                else
                    Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, p_dtbDeptTrfId, Request);

                if (p_dtbDeptTrfId != null)
                {
                    if (p_dtbDeptTrfId.Rows.Count > 0)
                    {
                        pClsProperty.union_id = Val.ToInt64(p_dtbDeptTrfId.Rows[0][0]);
                        pClsProperty.history_union_id = Val.ToInt64(p_dtbDeptTrfId.Rows[0][1]);
                        pClsProperty.lot_srno = Val.ToInt64(p_dtbDeptTrfId.Rows[0][2]);
                    }
                }
                else
                {
                    pClsProperty.union_id = 0;
                    pClsProperty.history_union_id = 0;
                    pClsProperty.lot_srno = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }

        public DataTable GetDeptBalanceCarat(Int64 Lot_Id)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Dept_Trf_Outstanding;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@lot_id", Lot_Id, DbType.Int64);
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable GetDeptISSREC(Int64 Lot_Id, string process)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Dept_Trf_ISSREC_Check;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@lot_id", Lot_Id, DbType.Int64);
            Request.AddParams("@process_name", process, DbType.String);
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public int Delete(MFGDepartmentTransferProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            int IntRes = 0;
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_All_Janged_Utility_Delete;
            Request.CommandType = CommandType.StoredProcedure;
            //Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            //Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            //Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            //Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int32);
            Request.AddParams("@cut_id", pClsProperty.cut_id, DbType.Int32);
            IntRes = Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
            return IntRes;
        }
        public MFGDepartmentTransferProperty Get_LotSrNo(MFGDepartmentTransferProperty pClsProperty)
        {
            try
            {
                Request Request = new Request();
                Request.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int32);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Department_Lot_Srno_Get;
                Request.CommandType = CommandType.StoredProcedure;

                DataTable p_dtbDeptTrfId = new DataTable();

                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, p_dtbDeptTrfId, Request);

                if (p_dtbDeptTrfId != null)
                {
                    if (p_dtbDeptTrfId.Rows.Count > 0)
                    {
                        pClsProperty.lot_srno = Val.ToInt64(p_dtbDeptTrfId.Rows[0][0]);
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
        public int Dept_Lot_ID_Delete(MFGDepartmentTransferProperty pClsProperty)
        {
            int IntRes = 0;
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Dept_LotSrNoWise_Delete;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int64);
            Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
            IntRes = Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
            return IntRes;
        }
    }
}
