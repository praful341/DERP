using DLL;
using System.Data;

namespace BLL.FunctionClasses.Transaction
{
    public class ProfitLossReport
    {
        #region "Data Member"
        InterfaceLayer Ope = new InterfaceLayer();
        BLL.Validation Val = new BLL.Validation();

        #endregion "Data Member"        

        #region "Functions" 
        public DataSet GetProfitLoss(string strCompanyID, string strBranchID, string strLocationID, string strFromDate, string strToDate)
        {
            DataSet DTab = new DataSet();
            Request Request = new Request();
            Request.CommandText = TPV.SProc.RPT_Trn_ProfitLoss;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@company_id", strCompanyID, DbType.String);
            Request.AddParams("@branch_id", strBranchID, DbType.String);
            Request.AddParams("@location_id", strLocationID, DbType.String);
            Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.String);

            Request.AddParams("@currency_id", GlobalDec.gEmployeeProperty.currency_id, DbType.Int32);
            Request.AddParams("@rate_type_id", GlobalDec.gEmployeeProperty.rate_type_id, DbType.Int32);
            Request.AddParams("@from_date", strFromDate, DbType.Date);
            Request.AddParams("@to_date", strToDate, DbType.Date);

            Ope.GetDataSet(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, "", Request);
            return DTab;
        }
        #endregion"Functions"
    }
}
