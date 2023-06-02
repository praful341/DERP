using BLL;
using DevExpress.XtraReports.UI;
using System;

namespace DERP.DRPT
{
    public partial class rptMemoIssueSub : DevExpress.XtraReports.UI.XtraReport
    {
        #region " Data Members "
        decimal m_numCarats = 0;
        decimal m_numAmt = 0;

        Validation Val = new Validation();
        #endregion

        #region " Constructors "
        public rptMemoIssueSub()
        {
            InitializeComponent();
            //m_numGrpWeightInCarats = 0;
            //m_numGrpTotalAmt = 0;
            //m_numGrnWeightInCarats = 0;
            //m_numGrnTotalAmt = 0;

            //DataTable dtb = new DataTable();
            //dtb = ((DataTable)this.DataSource).Copy();

            m_numCarats = 0;
            m_numAmt = 0;
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
            //rptMemoIssueSub objFinalAssort = SubFinalAssort.ReportSource as rptMemoIssueSub;
        }

        private void xrLabel28_SummaryCalculated(object sender, TextFormatEventArgs e)
        {

        }

        private void xrLabel28_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (m_numCarats > 0)
            {
                e.Result = Math.Round((m_numAmt / m_numCarats), 0);
                e.Handled = true;
            }
        }

        private void xrLabel28_SummaryRowChanged(object sender, EventArgs e)
        {
            m_numCarats += Val.ToDecimal(GetCurrentColumnValue("issue_carat"));
            m_numAmt += Val.ToDecimal(GetCurrentColumnValue("amount"));
        }
    }
}
