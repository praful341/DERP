using BLL;
using DevExpress.XtraReports.UI;
using System;
using System.Data;

namespace DERP.DRPT
{
    public partial class rptMemoIssue : DevExpress.XtraReports.UI.XtraReport
    {
        #region " Data Members "

        string strSQL;
        private DataTable m_dtbDetails;
        decimal m_numCarats = 0;
        decimal m_numAmt = 0;

        Validation Val = new Validation();

        #endregion

        #region " Constructors "
        public rptMemoIssue(DataTable p_dtbDetails)
        {
            InitializeComponent();
            m_numCarats = 0;
            m_numAmt = 0;
            //m_numGrnWeightInCarats = 0;
            //m_numGrnTotalAmt = 0;
            //strSQL = string.Empty;
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

        private void PageFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {


        }

        private void rptMemoIssue_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            rptMemoIssueSub objMemoIssue = MemoIssueSubReport.ReportSource as rptMemoIssueSub;

            try
            {

                if (objMemoIssue != null)
                    objMemoIssue.DataSource = m_dtbDetails;

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

        private void xrLabel28_SummaryRowChanged(object sender, EventArgs e)
        {
            m_numCarats += Val.ToDecimal(GetCurrentColumnValue("issue_carat"));
            m_numAmt += Val.ToDecimal(GetCurrentColumnValue("amount"));
        }

        private void xrLabel28_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (m_numCarats > 0)
            {
                e.Result = Math.Round((m_numAmt / m_numCarats),0);
                e.Handled = true;
            }
        }
    }
}
