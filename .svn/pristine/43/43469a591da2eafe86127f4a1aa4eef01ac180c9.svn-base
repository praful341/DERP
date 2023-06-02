using BLL.PropertyClasses.Transaction.MFG;
using DLL;
using System;
using System.Data;

namespace BLL.FunctionClasses.Transaction.MFG
{
    public class MFGBoilingNo
    {
        InterfaceLayer Ope = new InterfaceLayer();
        BLL.Validation Val = new BLL.Validation();

        public MFG_Boiling_NoProperty Save(MFG_Boiling_NoProperty pClsProperty, DLL.GlobalDec.EnumTran pEnum = DLL.GlobalDec.EnumTran.WithCommit, BeginTranConnection Conn = null)
        {
            try
            {
                Request Request = new Request();

                Request.AddParams("@boiling_date", pClsProperty.boiling_date, DbType.Date);
                Request.AddParams("@boiling_no", pClsProperty.boiling_no, DbType.Int32);
                Request.AddParams("@lot_id", pClsProperty.lot_id, DbType.Int32);
                Request.AddParams("@kapan_id", pClsProperty.kapan_id, DbType.Int32);
                Request.AddParams("@cut_id", pClsProperty.rough_cut_id, DbType.Int32);
                Request.AddParams("@company_id", GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request.AddParams("@branch_id", GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request.AddParams("@location_id", GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request.AddParams("@department_id", GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

                Request.CommandText = BLL.TPV.SProc.MFG_TRN_Boiling_No_Save;
                Request.CommandType = CommandType.StoredProcedure;

                DataTable p_dtbProcessRecId = new DataTable();
                if (Conn != null)
                    Conn.Inter1.GetDataTable(DBConnections.ConnectionString, DBConnections.ProviderName, p_dtbProcessRecId, Request, pEnum);
                else
                    Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, p_dtbProcessRecId, Request);

                if (p_dtbProcessRecId != null)
                {
                    if (p_dtbProcessRecId.Rows.Count > 0)
                    {
                        pClsProperty.boiling_no = Val.ToInt64(p_dtbProcessRecId.Rows[0][0]);
                    }
                }
                else
                {
                    pClsProperty.boiling_no = 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pClsProperty;
        }
    }
}
