using BLL.PropertyClasses.Master.Store;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Master.Store
{
    public class MfgItemMaster
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

        public int SaveItem(MfgItem_MasterProperty pClsProperty)
        {
            int IntRes = 0;

            try
            {
                Request Request = new Request();

                Request.AddParams("@item_id", pClsProperty.item_id, DbType.Int64);
                Request.AddParams("@item_name", pClsProperty.item_name, DbType.String);
                Request.AddParams("@item_type_id", pClsProperty.item_type_id, DbType.Int64);
                Request.AddParams("@unit_id", pClsProperty.unit_id, DbType.Int64);
                Request.AddParams("@active", pClsProperty.active, DbType.Int64);
                Request.AddParams("@remark", pClsProperty.remark, DbType.String);
                Request.AddParams("@opening_qty", pClsProperty.opening_qty, DbType.Int64);
                Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
                Request.AddParams("@company_id", pClsProperty.company_id, DbType.Int64);
                Request.AddParams("@branch_id", pClsProperty.branch_id, DbType.Int64);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int64);

                Request.AddParams("@opening_date", Val.DBDate(pClsProperty.opening_date), DbType.Date);
                Request.AddParams("@model_no", pClsProperty.model_no, DbType.String);
                Request.AddParams("@item_code", pClsProperty.item_code, DbType.String);
                Request.AddParams("@ton", pClsProperty.ton, DbType.Decimal);
                Request.AddParams("@warranty_month", pClsProperty.warranty_month, DbType.String);
                Request.AddParams("@warranty_year", pClsProperty.warranty_year, DbType.Int32);
                Request.AddParams("@type", pClsProperty.type, DbType.String);
                Request.AddParams("@company_name", pClsProperty.company_name, DbType.String);

                Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
                Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
                Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
                Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
                Request.AddParams("@sequence_no", pClsProperty.sequence_no, DbType.Int32);

                Request.CommandText = BLL.TPV.SProc.Store_MST_Item_Master_Save;
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

        public string ISExists(string ItemName, Int64 ItemId)
        {
            Validation Val = new Validation();
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "STORE_MST_Item_Master", "item_name", "AND item_name = '" + ItemName + "' AND NOT item_id =" + ItemId));
            }
            else
            {
                return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, "STORE_MST_Item_Master", "item_name", "AND item_name = '" + ItemName + "' AND NOT item_id =" + ItemId));
            }
        }

        public DataTable GetData()
        {
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.Store_MST_Item_Master_GetData;
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

        public DataTable Item_TypeWise_GetData(string Item_Type)
        {
            Request Request = new Request();

            Request.AddParams("@item_type", Item_Type, DbType.String);

            Request.CommandText = BLL.TPV.SProc.Store_MST_ItemType_Master_GetData;
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
