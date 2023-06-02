using BLL.PropertyClasses.Master.MFG;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Master.MFG
{
    public class FrmMfgRussianDailyEntryMaster
    {
        InterfaceLayer Ope = new InterfaceLayer();
        //Validation Val = new Validation();
        BLL.Validation Val = new BLL.Validation();
        public FrmMfgRussianDailyEntryProperty Save(FrmMfgRussianDailyEntryProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();
                Request.AddParams("@id", pClsProperty.id, DbType.Int32);
                Request.AddParams("@union_id", pClsProperty.union_id, DbType.Int16);
                Request.AddParams("@process_name", pClsProperty.process_name, DbType.String);
                Request.AddParams("@party_id", pClsProperty.party_id, DbType.Int16);
                Request.AddParams("@date", pClsProperty.date, DbType.Date);
                Request.AddParams("@pcs", pClsProperty.pcs, DbType.Int16);
                Request.AddParams("@no_of_mach", pClsProperty.no_of_mach, DbType.Int16);
                Request.AddParams("@mach_avg", pClsProperty.mach_avg, DbType.Decimal);
                Request.AddParams("@no_of_emp", pClsProperty.no_of_emp, DbType.Int16);
                Request.AddParams("@emp_avg", pClsProperty.emp_avg, DbType.Decimal);
                Request.AddParams("@user_id", BLL.GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", BLL.GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(BLL.GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", BLL.GlobalDec.gStr_SystemTime, DbType.String);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Russian_FAC_Daily_Entry_Save;
                Request.CommandType = CommandType.StoredProcedure;
                DataTable p_dtbProcessUnionId = new DataTable();
                if (Conn != null)
                    Conn.Inter1.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, p_dtbProcessUnionId, Request, pEnum);
                else
                    Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, p_dtbProcessUnionId, Request);

                if (p_dtbProcessUnionId != null)
                {
                    if (p_dtbProcessUnionId.Rows.Count > 0)
                    {
                        pClsProperty.union_id = Val.ToInt(p_dtbProcessUnionId.Rows[0][0]);

                    }
                }
                else
                {
                    pClsProperty.union_id = 0;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }
        public DataTable GetData(string fdate, string tdate)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Russian_FAC_Daily_Entry_Get;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@dtpFromDate", fdate, DbType.Date);
            Request.AddParams("@dtpToDate", tdate, DbType.Date);
            if (BLL.GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            }
            else
            {
                Ope.GetDataTable(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, DTab, Request);
            }
            return DTab;
        }
        public string ISExists(string Company, Int64 CompanyId)
        {
            Validation Val = new Validation();
            if (BLL.GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "MFG_MST_Machine_Item_Company", "company_name", "AND company_name = '" + Company + "' AND NOT id =" + CompanyId));
            }
            else
            {
                return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, "MFG_MST_Machine_Item_Company", "company_name", "AND company_name = '" + Company + "' AND NOT id =" + CompanyId));
            }
        }
    }
}
