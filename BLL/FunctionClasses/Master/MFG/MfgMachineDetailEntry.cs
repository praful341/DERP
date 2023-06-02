using BLL.PropertyClasses.Master.MFG;
using DLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.FunctionClasses.Master.MFG
{
    public class MfgMachineDetailEntry
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public int Save(MfgMachineDetailEntryProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@machine_detail_id", pClsProperty.machine_detail_id, DbType.Int32);
            Request.AddParams("@machine_item_id", pClsProperty.machine_item_id, DbType.Int32);
            Request.AddParams("@item_company_id", pClsProperty.item_company_id, DbType.Int32);
            Request.AddParams("@item_party_id", pClsProperty.item_party_id, DbType.Int32);
            Request.AddParams("@purchase_date", pClsProperty.purchase_date, DbType.Date);
            Request.AddParams("@install_date", pClsProperty.install_date, DbType.Date);
            Request.AddParams("@qty", pClsProperty.qty, DbType.Int32);
            Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
            Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
            Request.AddParams("@warranty_year", pClsProperty.warranty_year, DbType.Int32);
            Request.AddParams("@warranty_month", pClsProperty.warranty_month, DbType.Int32);
            Request.AddParams("@warranty_days", pClsProperty.warranty_days, DbType.Int32);
            Request.AddParams("@warranty_final_date", pClsProperty.warranty_final_date, DbType.Date);
            Request.AddParams("@srno", pClsProperty.srno, DbType.String);
            Request.AddParams("@remarks", pClsProperty.remarks, DbType.String);
            Request.AddParams("@branch_id", pClsProperty.branch_id, DbType.Int16);
            Request.AddParams("@manager_id", pClsProperty.manager_id, DbType.Int16);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

            Request.CommandText = BLL.TPV.SProc.MFG_MST_MachineDetail_Save;
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


        public DataTable GetData(int machine_item_id,int item_company_id, string datFromDate, string datToDate)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_Machine_Detail_Search_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@datFromDate", datFromDate, DbType.Date);
            Request.AddParams("@datToDate", datToDate,DbType.Date);
            Request.AddParams("@machine_item_id", machine_item_id,DbType.Int32);
            Request.AddParams("@item_company_id", item_company_id,DbType.Int32);
            //Request.AddParams("@Active", active, DbType.Int32);
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
        public string ISExists(string Item, Int64 ItemId)
        {
            Validation Val = new Validation();
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "MFG_MST_Machine_Item_Master", "item_name", "AND item_name = '" + Item + "' AND NOT id =" + ItemId));
            }
            else
            {
                return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, "MFG_MST_Machine_Item_Master", "item_name", "AND item_name = '" + Item + "' AND NOT id =" + ItemId));
            }
        }

    }
}
