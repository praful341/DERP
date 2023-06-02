using DevExpress.XtraReports.UI;
using System;
using System.Data;

namespace DERP.DRPT
{
    public partial class rptDashbord : DevExpress.XtraReports.UI.XtraReport
    {
        #region " Data Members "
        string strSQL;
        private DataSet m_dtbDetails;

        #endregion

        #region " Constructors "
        public rptDashbord(DataSet p_dtbDetails)
        {
            InitializeComponent();

            m_dtbDetails = p_dtbDetails;

        }
        #endregion

        #region " Events "
        private void lblTotRate_SummaryRowChanged(object sender, EventArgs e)
        {
            //m_numGrpWeightInCarats += General.ToDecimal(GetCurrentColumnValue(Resources.dbColumns.Carats));
            //m_numGrpTotalAmt += General.ToDecimal(GetCurrentColumnValue(Resources.dbColumns.Amount));

            //m_numGrnWeightInCarats += General.ToDecimal(GetCurrentColumnValue(Resources.dbColumns.Carats));
            //m_numGrnTotalAmt += General.ToDecimal(GetCurrentColumnValue(Resources.dbColumns.Amount));
        }

        private void lblGrpRate_SummaryReset(object sender, EventArgs e)
        {
            //m_numGrpWeightInCarats = 0;
            //m_numGrpTotalAmt = 0;
        }

        private void lblGrpRate_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //if (m_numGrpWeightInCarats > 0)
            //{
            //    e.Result = (m_numGrpTotalAmt / m_numGrpWeightInCarats);
            //    e.Handled = true;
            //}
        }

        private void lblTotRate_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //if (m_numGrnWeightInCarats > 0)
            //{
            //    e.Result = (m_numGrnTotalAmt / m_numGrnWeightInCarats);
            //    e.Handled = true;
            //}
        }
        #endregion


        private void rptMemoIssue_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            rptAssortRegister objSale = RptDashbordSaleReport.ReportSource as rptAssortRegister;
            rptDashbordMfgIn objMfgIn = RptDashbordMFGIn.ReportSource as rptDashbordMfgIn;
            rptDashbordBalance objBalance = RptDashbordBalance.ReportSource as rptDashbordBalance;

            try
            {
                if (objSale != null)
                    objSale.DataSource = m_dtbDetails.Tables[0];

                if (objMfgIn != null)
                    objMfgIn.DataSource = m_dtbDetails.Tables[1];

                if (objBalance != null)
                    objBalance.DataSource = m_dtbDetails.Tables[2];

                strSQL = string.Empty;
                m_dtbDetails = null;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
            finally
            {
                strSQL = string.Empty;
                m_dtbDetails = null;
            }
        }
    }
}
