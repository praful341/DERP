using BLL.PropertyClasses.Transaction.MFG;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction.MFG
{
    public class MFGDepartmentCosting
    {
        InterfaceLayer Ope = new InterfaceLayer();
        BLL.Validation Val = new BLL.Validation();

        public MFGDepartmentCostingProperty Save(MFGDepartmentCostingProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@department_costing_id", pClsProperty.department_costing_id, DbType.Int64);
                Request.AddParams("@costing_date", pClsProperty.costing_date, DbType.Date);
                Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);
                Request.AddParams("@department_id", pClsProperty.department_id, DbType.Int64);
                Request.AddParams("@opening_pcs", pClsProperty.opening_pcs, DbType.Int64);
                Request.AddParams("@in_pcs", pClsProperty.in_pcs, DbType.Int64);
                Request.AddParams("@out_pcs", pClsProperty.out_pcs, DbType.Int64);
                Request.AddParams("@closing_pcs", pClsProperty.closing_pcs, DbType.Int64);
                Request.AddParams("@target", pClsProperty.target, DbType.Decimal);
                Request.AddParams("@year", pClsProperty.year, DbType.Int32);
                Request.AddParams("@month", pClsProperty.month, DbType.Int32);
                Request.AddParams("@ledger_id", pClsProperty.ledger_id, DbType.Int64);
                Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
                Request.AddParams("@yearmonth", pClsProperty.yearmonth, DbType.Int32);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Department_Costing_Save;
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
        public DataTable MFGDepartmentCostingGetData(MFGDepartmentCostingProperty pClsProperty)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Department_Costing_GetData;

            Request.AddParams("@department_id", pClsProperty.department_id, DbType.Int64);
            Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable MFGDepartmentWiseCostingData(MFGDepartmentCostingProperty pClsProperty)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_DeptWise_Target_GetData;

            Request.AddParams("@department_id", pClsProperty.department_id, DbType.Int64);

            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public DataTable MFGPataLotEntryGetDataList(MFGDepartmentCostingProperty PClsProperty)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Department_Costing_GetDataList;

            Request.AddParams("@department_id", PClsProperty.department_id, DbType.Int64);
            Request.AddParams("@year", PClsProperty.year, DbType.Int32);
            Request.AddParams("@month", PClsProperty.month, DbType.Int32);
            Request.AddParams("@lot_srno", PClsProperty.lot_srno, DbType.Int64);

            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }

        public DataTable GetDepartmentCostingData(MFGDepartmentCostingProperty PClsProperty)
        {
            DataTable DTabVal = new DataTable();
            Request Request = new Request();

            Request.AddParams("@department_id", PClsProperty.department_id, DbType.Int64);
            Request.AddParams("@year", PClsProperty.year, DbType.Int32);
            Request.AddParams("@month", PClsProperty.month, DbType.Int32);
            Request.AddParams("@from_date", PClsProperty.from_date, DbType.Date);
            Request.AddParams("@to_date", PClsProperty.to_date, DbType.Date);

            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Department_Costing_Stock;
            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, DTabVal, Request);
            return DTabVal;
        }
        public int GetDeleteDepartmentCostingEntry(MFGDepartmentCostingProperty pClsProperty)
        {
            DataTable DTab = new DataTable();
            int IntRes = 0;
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Department_Costing_Delete;

            Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);

            Request.CommandType = CommandType.StoredProcedure;
            IntRes = Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
            return IntRes;
        }
        public DataSet MFGDepartmentCostingPrintGetData(MFGDepartmentCostingProperty pClsProperty)
        {
            DataSet DTab = new DataSet();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.RPT_MFG_TRN_Department_Costing_GetData;

            Request.AddParams("@year", pClsProperty.year, DbType.Int32);
            Request.AddParams("@month", pClsProperty.month, DbType.Int32);
            Request.AddParams("@from_date", pClsProperty.from_date, DbType.Date);
            Request.AddParams("@to_date", pClsProperty.to_date, DbType.Date);
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataSet(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, "", Request);
            return DTab;
        }
        public string ISExists(Int64 Department_ID, Int32 Year, Int32 Month, Int64 Lot_SRNo)
        {
            Validation Val = new Validation();
            return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "MFG_TRN_Department_Costing", "department_id", "AND department_id = '" + Department_ID + "' AND year =" + Year + " AND month =" + Month + "AND NOT lot_srno = " + Lot_SRNo));
        }
    }
}
