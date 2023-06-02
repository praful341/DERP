using BLL;
using DevExpress.XtraReports.UI;
using System;

namespace DERP.DRPT
{
    public partial class rptDashbordMfgIn : DevExpress.XtraReports.UI.XtraReport
    {
        #region "Data Members"
        private decimal m_numSumInCarats;
        private decimal m_numSumInAmount;

        BLL.Validation Val = new Validation();
        #endregion

        #region "Constructors"
        public rptDashbordMfgIn()
        {
            InitializeComponent();

            m_numSumInCarats = 0;
            m_numSumInAmount = 0;
        }
        #endregion

        #region "Events"
        private void lblDetInCts_EvaluateBinding(object sender, BindingEventArgs e)
        {
            //if (General.ToDecimal(GetCurrentColumnValue("incarats")) == 0)
            //    e.Value = "-";
        }

        private void lblDetInRate_EvaluateBinding(object sender, BindingEventArgs e)
        {
            //if (General.ToDecimal(GetCurrentColumnValue("inrate")) == 0)
            //    e.Value = "-";
        }

        private void lblDetInAmt_EvaluateBinding(object sender, BindingEventArgs e)
        {
            //if (General.ToDecimal(GetCurrentColumnValue("inamount")) == 0)
            //    e.Value = "-";
        }

        private void lblDetOutCts_EvaluateBinding(object sender, BindingEventArgs e)
        {
            //if (General.ToDecimal(GetCurrentColumnValue("outcarats")) == 0)
            //    e.Value = "-";
        }

        private void lblDetOutRate_EvaluateBinding(object sender, BindingEventArgs e)
        {
            //if (General.ToDecimal(GetCurrentColumnValue("outrate")) == 0)
            //    e.Value = "-";
        }

        private void lblDetOutAmount_EvaluateBinding(object sender, BindingEventArgs e)
        {
            //if (General.ToDecimal(GetCurrentColumnValue("outamount")) == 0)
            //    e.Value = "-";
        }

        private void lblRptInCarats_SummaryReset(object sender, EventArgs e)
        {

        }

        private void lblRptInCarats_SummaryRowChanged(object sender, EventArgs e)
        {
            m_numSumInCarats += Val.ToDecimal(GetCurrentColumnValue("incarat"));
            m_numSumInAmount += Val.ToDecimal(GetCurrentColumnValue("inamount"));
            //m_numSumOutCarats += General.ToDecimal(GetCurrentColumnValue("outcarats"));
            //m_numSumOutAmount += General.ToDecimal(GetCurrentColumnValue("outamount"));
        }

        private void lblRptInCarats_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = 0;
            if (m_numSumInCarats > 0)
                e.Result = Math.Round((m_numSumInCarats), 2);
            e.Handled = true;
        }

        private void lblRptInRate_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = 0;
            if (m_numSumInCarats > 0)
                e.Result = Math.Round((m_numSumInAmount / m_numSumInCarats), 2);
            e.Handled = true;
        }

        private void lblRptInAmount_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = 0;
            if (m_numSumInAmount > 0)
                e.Result = Math.Round((m_numSumInAmount), 2);
            e.Handled = true;
        }

        private void lblRptOutCarats_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //e.Result = 0;
            //if (m_numSumOutCarats > 0)
            //    e.Result = Math.Round((m_numSumOutCarats), 2);
            //e.Handled = true;
        }

        private void lblRptOutRate_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //e.Result = 0;
            //if (m_numSumOutCarats > 0)
            //    e.Result = Math.Round((m_numSumOutAmount / m_numSumOutCarats), 2);
            //e.Handled = true;
        }

        private void lblRptOutAmount_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            //e.Result = 0;
            //if (m_numSumOutAmount > 0)
            //    e.Result = Math.Round((m_numSumOutAmount), 2);
            //e.Handled = true;
        }
        #endregion
    }
}
