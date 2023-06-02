using BLL.PropertyClasses.Rejection;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Rejection
{
    public class MFGRejectionSectionMaster
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();
        public int Save(MFGRejectionSection_MasterProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@section_id", pClsProperty.section_id, DbType.Int64);
            Request.AddParams("@section_name", pClsProperty.section_name, DbType.String);
            Request.AddParams("@active", pClsProperty.active, DbType.Int32);
            Request.AddParams("@remarks", pClsProperty.remarks, DbType.String);
            Request.AddParams("@sequence_no", pClsProperty.sequence_no, DbType.Int32);
            Request.AddParams("@user_id", GlobalDec.gEmployeeProperty.user_id, DbType.Int32);
            Request.AddParams("@ip_address", GlobalDec.gStrComputerIP, DbType.String);
            Request.AddParams("@entry_date", Val.DBDate(GlobalDec.gStr_SystemDate), DbType.Date);
            Request.AddParams("@entry_time", GlobalDec.gStr_SystemTime, DbType.String);

            Request.CommandText = BLL.TPV.SProc.MST_Rejection_Section_Save;
            Request.CommandType = CommandType.StoredProcedure;

            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }
        public DataTable GetData(int active = 0)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MST_Rejection_Section_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@active", active, DbType.Int32);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public string ISExists(string SectionName, Int64 SectionId)
        {
            Validation Val = new Validation();
            return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "MFG_MST_Rejection_Section", "section_name", "AND section_name = '" + SectionName + "' AND NOT section_id =" + SectionId));
        }
    }
}
