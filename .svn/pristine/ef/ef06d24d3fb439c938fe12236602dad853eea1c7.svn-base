using DLL;
using System.Data;

namespace BLL
{
    public class FillCombo
    {
        InterfaceLayer Ope = new InterfaceLayer();
        public enum TABLE
        {
            Company_Master = 1,
            Branch_Master = 2,
            Location_Master = 3,
            Department_Master = 4,
            Form_Master = 5,
            Ledger_Master = 6,
            Menu_Master = 7,
            Kapan_Master = 8,
            Cut_Master = 9,
            Rough_Sieve_Master = 10,
            Purity_Master = 11,
            Company_Master_New = 12,
            Branch_Master_New = 13,
            Location_Master_New = 14,
            Department_Master_New = 15,
            Process_Master = 16,
            Sub_Process_Master = 17,
            Quality_Master = 18,
            Rough_Clarity_Master = 20,
            Process_Master_New = 21,
            Employee_Master = 22,
            Manager_Master = 23,
            Party_Master = 24,
            User_Master = 25,
            Rejection_Kapan_Master = 26,
            Division_Master = 27,
            Galaxy_Shape = 28,
            Galaxy_Color = 29,
            Galaxy_Clarity = 30,
            Galaxy_Cut = 31,
            Galaxy_Sieve = 32,
            Galaxy_Kapan = 33,
            Copy_User_Master = 34,
            Galaxy_Distinct_Kapan = 35,
            Item_Company = 36,
            Item_Party = 37,
            Machine_Item = 38
        }

        public int user_id;
        public int company_id;
        public int branch_id;
        public int location_id;
        public int department_id;
        public int kapan_id;
        public int cut_id;
        public int rough_sieve_id;
        public int purity_id;

        public DataTable FillCmb(TABLE tenum)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.AddParams("@user_id", user_id, DbType.Int32);
            Request.AddParams("@company_id", company_id, DbType.Int32);
            Request.AddParams("@branch_id", branch_id, DbType.Int32);
            Request.AddParams("@location_id", location_id, DbType.Int32);
            Request.AddParams("@department_id", department_id, DbType.Int32);
            Request.AddParams("@Ope", tenum.ToString(), DbType.String);

            Request.CommandText = BLL.TPV.SProc.MST_Get_All_Data;
            Request.CommandType = CommandType.StoredProcedure;

            //if (GlobalDec.gEmployeeProperty.Allow_Developer == 0)
            //{
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            //}
            //else
            //{
            //    Ope.GetDataTable(BLL.DBConnections.ConnectionDeveloper, BLL.DBConnections.ProviderDeveloper, DTab, Request);
            //}
            return DTab;
        }
        public DataTable DTab_Transaction_Type()
        {
            DataTable DTab = new DataTable();
            DTab.Columns.Add("Transaction_Type", typeof(string));
            DataRow Dr = DTab.NewRow();
            Dr[0] = "Cash";
            DTab.Rows.Add(Dr);
            Dr = DTab.NewRow();
            Dr[0] = "Bank";
            DTab.Rows.Add(Dr);
            return DTab;
        }
    }
}
