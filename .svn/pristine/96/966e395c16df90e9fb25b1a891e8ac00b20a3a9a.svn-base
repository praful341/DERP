using BLL.PropertyClasses.Master;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Master
{
    public class LedgerMaster
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public int Save(Ledger_MasterProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@ledger_id", pClsProperty.ledger_id, DbType.Int32);
            Request.AddParams("@ledger_name", pClsProperty.ledger_name, DbType.String);
            Request.AddParams("@ledger_shortname", pClsProperty.ledger_shortname, DbType.String);
            Request.AddParams("@party_id", pClsProperty.party_id, DbType.Int32);
            Request.AddParams("@broker_id", pClsProperty.broker_id, DbType.Int32);
            Request.AddParams("@account_type_id", pClsProperty.account_type_id, DbType.Int32);
            Request.AddParams("@location_id", pClsProperty.location_id, DbType.Int32);
            Request.AddParams("@crts", pClsProperty.crts, DbType.Decimal);
            Request.AddParams("@adat_per", pClsProperty.adat_per, DbType.Decimal);
            Request.AddParams("@broker_per", pClsProperty.broker_per, DbType.Decimal);
            Request.AddParams("@opening_date", pClsProperty.opening_date, DbType.Date);
            Request.AddParams("@opening_balance", pClsProperty.opening_balance, DbType.Decimal);
            Request.AddParams("@actual_opening_balance", pClsProperty.actual_opening_balance, DbType.Decimal);
            Request.AddParams("@reference_by", pClsProperty.reference_by, DbType.String);
            Request.AddParams("@is_inspection", pClsProperty.is_inspection, DbType.Int32);
            Request.AddParams("@is_adat", pClsProperty.is_adat, DbType.Int32);
            Request.AddParams("@is_broker", pClsProperty.is_broker, DbType.Int32);
            Request.AddParams("@is_imp_exp_party", pClsProperty.is_imp_exp_party, DbType.Int32);
            Request.AddParams("@factory", pClsProperty.factory, DbType.Int32);
            Request.AddParams("@is_outside", pClsProperty.is_outside, DbType.Int32);
            Request.AddParams("@is_expense", pClsProperty.is_expense, DbType.Int32);
            Request.AddParams("@office_address", pClsProperty.office_address, DbType.String);
            Request.AddParams("@office_country_id", pClsProperty.office_country_id, DbType.Int32);
            Request.AddParams("@office_state_id", pClsProperty.office_state_id, DbType.Int32);
            Request.AddParams("@office_city_id", pClsProperty.office_city_id, DbType.Int32);
            Request.AddParams("@office_pincode", pClsProperty.office_pincode, DbType.String);
            Request.AddParams("@office_email_id", pClsProperty.office_email_id, DbType.String);
            Request.AddParams("@office_phone_no", pClsProperty.office_phone_no, DbType.String);
            Request.AddParams("@office_fax", pClsProperty.office_fax, DbType.String);
            Request.AddParams("@factory_address", pClsProperty.factory_address, DbType.String);
            Request.AddParams("@factory_country_id", pClsProperty.factory_country_id, DbType.Int32);
            Request.AddParams("@factory_state_id", pClsProperty.factory_state_id, DbType.Int32);
            Request.AddParams("@factory_city_id", pClsProperty.factory_city_id, DbType.Int32);
            Request.AddParams("@factory_pincode", pClsProperty.factory_pincode, DbType.String);
            Request.AddParams("@factory_email_id", pClsProperty.factory_email_id, DbType.String);
            Request.AddParams("@factory_phone_no", pClsProperty.factory_phone_no, DbType.String);
            Request.AddParams("@factory_fax", pClsProperty.factory_fax, DbType.String);
            Request.AddParams("@contact_name", pClsProperty.contact_name, DbType.String);
            Request.AddParams("@address", pClsProperty.address, DbType.String);
            Request.AddParams("@designation_id", pClsProperty.designation_id, DbType.Int32);
            Request.AddParams("@email_id", pClsProperty.email_id, DbType.String);
            Request.AddParams("@birth_date", pClsProperty.birth_date, DbType.Date);
            Request.AddParams("@mobile_no", pClsProperty.mobile_no, DbType.String);
            Request.AddParams("@phone_no", pClsProperty.phone_no, DbType.String);
            Request.AddParams("@account_no", pClsProperty.account_no, DbType.String);
            Request.AddParams("@bank_email_id", pClsProperty.bank_email_id, DbType.String);
            Request.AddParams("@bank_birth_date", pClsProperty.bank_birth_date, DbType.Date);
            Request.AddParams("@bank_branch_name", pClsProperty.bank_branch_name, DbType.String);
            Request.AddParams("@bank_transaction_limit", pClsProperty.bank_transaction_limit, DbType.Decimal);
            Request.AddParams("@pan_no", pClsProperty.pan_no, DbType.String);
            Request.AddParams("@tin_vat_no", pClsProperty.tin_vat_no, DbType.String);
            Request.AddParams("@tin_cst_no", pClsProperty.tin_cst_no, DbType.String);
            Request.AddParams("@gstin", pClsProperty.gstin, DbType.String);
            Request.AddParams("@tan_no", pClsProperty.tan_no, DbType.String);
            Request.AddParams("@tin_vat_effective_date", pClsProperty.tin_vat_effective_date, DbType.Date);
            Request.AddParams("@tin_cst_effective_date", pClsProperty.tin_cst_effective_date, DbType.Date);
            Request.AddParams("@gstin_effective_date", pClsProperty.gstin_effective_date, DbType.Date);
            Request.AddParams("@sequence_no", pClsProperty.sequence_no, DbType.Int32);
            Request.AddParams("@active", pClsProperty.active, DbType.Int32);
            Request.AddParams("@remarks", pClsProperty.remarks, DbType.String);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);
            Request.AddParams("@ledger_type", pClsProperty.ledger_type, DbType.String);
            Request.AddParams("@is_checked", pClsProperty.is_checked, DbType.Int32);

            Request.AddParams("@is_pattycash", pClsProperty.is_pattycash, DbType.Int32);
            Request.AddParams("@outstanding_limit", pClsProperty.outstanding_limit, DbType.Int64);
            Request.AddParams("@is_party_lock_open", pClsProperty.is_party_lock_open, DbType.Int32);

            Request.CommandText = BLL.TPV.SProc.MST_Ledger_Save;
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
        public DataTable GetData(int active = 0, int num_LocationID = 0)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MST_Ledger_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@Active", active, DbType.Int32);
            Request.AddParams("@LocationID", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
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

        public string ISExists(string ShortName, string LedgerName, Int64 LedgerId, int LocationId)
        {
            Validation Val = new Validation();
            if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            {
                return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "MST_Ledger", "ledger_name", "AND (ledger_shortname = '" + ShortName + "' OR ledger_name = '" + LedgerName + "') AND NOT ledger_id =" + LedgerId + " AND location_id =" + LocationId));
            }
            else
            {
                return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, "MST_Ledger", "ledger_name", "AND (ledger_shortname = '" + ShortName + "' OR ledger_name = '" + LedgerName + "') AND NOT ledger_id =" + LedgerId + " AND location_id =" + LocationId));
            }
        }
    }
}
