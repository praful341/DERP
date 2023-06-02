using BLL.PropertyClasses.Master.Store;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Master.Store
{
    public class MfgUnitMaster
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public int Save(MfgUnit_MasterProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@unit_id", pClsProperty.unit_id, DbType.Int32);
            Request.AddParams("@unit_name", pClsProperty.unit_name, DbType.String);
            Request.AddParams("@active", pClsProperty.active, DbType.Int32);
            Request.AddParams("@remarks", pClsProperty.remarks, DbType.String);
            Request.AddParams("@sequence_no", pClsProperty.sequence_no, DbType.Int32);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

            Request.CommandText = BLL.TPV.SProc.Store_MST_Unit_Save;
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
        public DataTable GetData(int active = 0)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.Store_MST_Unit_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@Active", active, DbType.Int32);
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
        public DataTable GetData_Assort()
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.Temp_Assort_GetData;
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);

            return DTab;
        }
        public int Save_Assort(MfgUnit_MasterProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@assortment_date", pClsProperty.assortment_date, DbType.Date);
            Request.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int64);
            Request.AddParams("@lot_srno", pClsProperty.lot_srno, DbType.Int64);
            Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int64);
            Request.AddParams("@rough_cut_id", pClsProperty.rough_cut_id, DbType.Int64);
            Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int64);
            Request.AddParams("@sub_process_id", pClsProperty.sub_process_id, DbType.Int64);
            Request.AddParams("@assort_id", pClsProperty.assort_id, DbType.Int64);
            Request.AddParams("@sieve_id", pClsProperty.sieve_id, DbType.Int64);
            Request.AddParams("@purity_id", pClsProperty.purity_id, DbType.Int64);
            Request.AddParams("@color_id", pClsProperty.color_id, DbType.Int64);
            Request.AddParams("@temp_quality_name", pClsProperty.temp_quality_name, DbType.String);
            Request.AddParams("@temp_sieve_name", pClsProperty.temp_sieve_name, DbType.String);
            Request.AddParams("@pcs", pClsProperty.pcs, DbType.Int32);
            Request.AddParams("@carat", pClsProperty.carat, DbType.Decimal);
            Request.AddParams("@percentage", pClsProperty.percentage, DbType.Decimal);
            Request.AddParams("@rate", pClsProperty.rate, DbType.Decimal);
            Request.AddParams("@amount", pClsProperty.amount, DbType.Decimal);
            Request.AddParams("@assort_total_carat", pClsProperty.assort_total_carat, DbType.Decimal);
            Request.AddParams("@company_id", pClsProperty.company_id, DbType.Int64);
            Request.AddParams("@branch_id", pClsProperty.branch_id, DbType.Int64);
            Request.AddParams("@location_id", pClsProperty.location_id, DbType.Int64);
            Request.AddParams("@department_id", pClsProperty.department_id, DbType.Int64);
            Request.AddParams("@form_id", pClsProperty.form_id, DbType.Int64);
            Request.AddParams("@user_id", pClsProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", pClsProperty.ip_address, DbType.String);
            Request.AddParams("@entry_date", pClsProperty.entry_date, DbType.Date);
            Request.AddParams("@entry_time", pClsProperty.entry_time, DbType.String);

            Request.CommandText = BLL.TPV.SProc.MFG_MST_Assort_Save;
            Request.CommandType = CommandType.StoredProcedure;

            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }
        public string ISExists(string Unit, Int64 UnitId)
        {
            Validation Val = new Validation();
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "STORE_MST_Unit", "unit_name", "AND unit_name = '" + Unit + "' AND NOT unit_id =" + UnitId));
            }
            else
            {
                return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, "STORE_MST_Unit", "unit_name", "AND unit_name = '" + Unit + "' AND NOT unit_id =" + UnitId));
            }
        }
    }
}
