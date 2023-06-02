using BLL.PropertyClasses.Master.Store;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Master.Store
{
    public class MfgSubItemMaster
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();

        #region Property Settings

        private DataTable _DTab = new DataTable("Item_Master");

        public DataTable DTab
        {
            get { return _DTab; }
            set { _DTab = value; }
        }

        #endregion

        #region Other Function

        public int SaveSubItem(MfgSubItem_MasterProperty pClsProperty)
        {
            int IntRes = 0;

            try
            {
                Request Request = new Request();

                Request.AddParams("@sub_item_id", pClsProperty.sub_item_id, DbType.Int64);
                Request.AddParams("@item_id", pClsProperty.item_id, DbType.Int64);
                Request.AddParams("@sub_item_name", pClsProperty.sub_item_name, DbType.String);
                Request.AddParams("@opening_date", pClsProperty.opening_date, DbType.Date);
                Request.AddParams("@opening_quantity", pClsProperty.opening_quantity, DbType.Decimal);
                Request.AddParams("@opening_rate", pClsProperty.opening_rate, DbType.Decimal);
                Request.AddParams("@opening_amt", pClsProperty.opening_amt, DbType.Decimal);
                Request.AddParams("@model_no", pClsProperty.model_no, DbType.String);
                Request.AddParams("@warranty_year", pClsProperty.warranty_year, DbType.Int32);
                Request.AddParams("@warranty_month", pClsProperty.warranty_month, DbType.Int32);
                Request.AddParams("@company_name", pClsProperty.company_name, DbType.String);
                Request.AddParams("@item_code", pClsProperty.item_code, DbType.String);
                Request.AddParams("@ton", pClsProperty.ton, DbType.Decimal);
                Request.AddParams("@type", pClsProperty.type, DbType.String);
                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@sequence_no", pClsProperty.sequence_no, DbType.Int32);

                Request.CommandText = BLL.TPV.SProc.Store_MST_Sub_Item_Master_Save;
                Request.CommandType = CommandType.StoredProcedure;
                if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
                {
                    IntRes = Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
                }
                else
                {
                    IntRes = Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, Request);
                }
            }
            catch
            {
                IntRes = 0;
            }
            return IntRes;
        }

        public string ISExists(string SubItemName, Int64 SubItemId)
        {
            Validation Val = new Validation();
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "STORE_MST_Sub_Item_Master", "sub_item_name", "AND sub_item_name = '" + SubItemName + "' AND NOT sub_item_id =" + SubItemId));
            }
            else
            {
                return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, "STORE_MST_Sub_Item_Master", "sub_item_name", "AND sub_item_name = '" + SubItemName + "' AND NOT sub_item_id =" + SubItemId));
            }
        }

        public DataTable GetData()
        {
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.Store_MST_Sub_Item_Master_GetData;
            Request.CommandType = CommandType.StoredProcedure;
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
        public DataTable ItemWise_Sub_GetData(Int64 item_id)
        {
            Request Request = new Request();

            Request.AddParams("@item_id", item_id, DbType.Int64);

            Request.CommandText = BLL.TPV.SProc.Store_MST_ItemWise_Sub_GetData;
            Request.CommandType = CommandType.StoredProcedure;
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

        #endregion
    }
}
