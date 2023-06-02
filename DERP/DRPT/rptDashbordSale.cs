using DevExpress.XtraReports.UI;
using System;

namespace DERP.DRPT
{
    public partial class rptDashbordSale : DevExpress.XtraReports.UI.XtraReport
    {
        #region " Data Members "
        private decimal m_numCarat;
        private decimal m_numAmount;

        #endregion

        #region " Constructors "
        public rptDashbordSale()
        {
            InitializeComponent();

            m_numCarat = 0;
            m_numAmount = 0;

        }
        #endregion

        #region " Events "

        private void lblMonth_SummaryRowChanged(object sender, EventArgs e)
        {
            m_numCarat += Convert.ToDecimal(GetCurrentColumnValue("salecarat"));
            m_numAmount += Convert.ToDecimal(GetCurrentColumnValue("saleamount"));
        }

        private void lblGrnRate_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            if (m_numCarat > 0)
            {
                e.Result = Math.Round((m_numAmount / m_numCarat), 2);
                e.Handled = true;
            }
        }
        #endregion
    }
}
