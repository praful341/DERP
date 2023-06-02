using BLL.PropertyClasses.Master.MFG;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Master.MFG
{
    public class MfgWeightValidationMaster
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public int Save(MfgWeightValidation_MasterProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@weight_id", pClsProperty.weight_id, DbType.Int32);
            Request.AddParams("@company_id", pClsProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", pClsProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", pClsProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", pClsProperty.department_id, DbType.Int32);
            Request.AddParams("@process_id", pClsProperty.process_id, DbType.Int32);
            Request.AddParams("@validation_type", pClsProperty.validation_type, DbType.String);
            Request.AddParams("@value_from", pClsProperty.value_from, DbType.Decimal);
            Request.AddParams("@value_to", pClsProperty.value_to, DbType.Decimal);
            Request.AddParams("@active", pClsProperty.active, DbType.Int32);
            Request.AddParams("@remarks", pClsProperty.remarks, DbType.String);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

            Request.CommandText = BLL.TPV.SProc.MFG_MST_WtValidation_Save;
            Request.CommandType = CommandType.StoredProcedure;
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }
        public DataTable GetData(int active = 0)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_MST_WtValidation_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@Active", active, DbType.Int32);
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public string ISExists(int Company, int Branch, int Location, int Dept, int Process, Int64 WtId)
        {
            Validation Val = new Validation();
            return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "MFG_MST_Weight_Validation", "validation_type", "AND company_id = " + Company + "AND branch_id = " + Branch + "AND location_id = " + Location + "AND department_id = " + Dept + "AND process_id = " + Process + " AND NOT weight_id =" + WtId));
        }
    }
}
