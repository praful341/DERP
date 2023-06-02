using BLL.PropertyClasses.Master.MFG;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Master.MFG
{
    public class MfgMiscellaneousEntryMaster
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public int Save(MfgMiscellaneousEntryMasterProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@miscellaneous_id", pClsProperty.miscellaneous_id, DbType.Int32);
            Request.AddParams("@party_name", pClsProperty.party_name, DbType.String);
            Request.AddParams("@purchase_date", pClsProperty.purchase_date, DbType.Date);
            Request.AddParams("@mobile_no", pClsProperty.mobile_number, DbType.Int64);
            Request.AddParams("@item_name", pClsProperty.item_name, DbType.String);
            Request.AddParams("@branch_id", pClsProperty.branch_id, DbType.Int16);
            Request.AddParams("@manager_id", pClsProperty.manager_id, DbType.Int16);
            Request.AddParams("@qty", pClsProperty.qty, DbType.Int32);
            Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
            Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
            Request.AddParams("@department", pClsProperty.department, DbType.String);
            Request.AddParams("@remark", pClsProperty.remark, DbType.String);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

            Request.CommandText = BLL.TPV.SProc.MFG_MST_MiscellaneousBillEntry_Save;
            Request.CommandType = CommandType.StoredProcedure;
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
            }
            else
            {
                return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, Request);
            }
        }
        public DataTable GetData(string fdate, string tdate)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_MST_MiscellaneousBillEntry_Get;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@datFromDate", fdate, DbType.Date);
            Request.AddParams("@datToDate", tdate, DbType.Date);
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
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
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
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
