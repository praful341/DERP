using DevExpress.XtraReports.UI;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace DERP.DRPT
{
    public partial class XtraReportViewer : DevExpress.XtraEditors.XtraForm
    {
        #region Declaration
        BLL.FormEvents objBOFormEvents = new BLL.FormEvents();
        BLL.Validation Val = new BLL.Validation();
        private DataTable m_dtbReport;
        private DataSet m_dtbDS;
        private XtraReport objrpt;
        private string m_strReportTitle = string.Empty;
        private string m_strReportName = string.Empty;
        #endregion

        #region Constructor
        public XtraReportViewer(string p_strReportName, string p_strReportTitle, DataTable p_dtbReport, DataSet p_DS)
        {
            InitializeComponent();

            m_dtbReport = p_dtbReport;
            m_dtbDS = p_DS;

            m_strReportTitle = p_strReportTitle;
            m_strReportName = p_strReportName;
        }
        #endregion

        #region "Events"
        private void XtraReportViewer_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            if (objrpt != null)
            {
                objrpt.Dispose();
            }
        }

        private void XtraReportViewer_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void XtraReportViewer_Shown(object sender, EventArgs e)
        {
            BLL.General.FullSizeForm(this);
            ShowForm();
        }
        #endregion

        #region " Functions / Procedures "
        public void ShowForm()
        {
            try
            {
                switch (m_strReportName)
                {
                    case "Memo Issue":
                        {
                            //objrpt = new MemoIssueReport();
                            objrpt = new rptMemoIssue(m_dtbReport);
                            break;
                        }
                    case "Stock Dashbord":
                        {
                            objrpt = new rptDashbord(m_dtbDS);
                            break;
                        }
                    case "Semi-1":
                        {
                            objrpt = new rptSemi1(m_dtbReport);
                            break;
                        }
                    case "Monthly Sale":
                        {
                            objrpt = new rptSaleGraph(m_dtbDS);
                            break;
                        }

                }
                objrpt.DataSource = m_dtbReport;

                //objrpt.Parameters["TitleCompanyName"].Value = "";
                objrpt.Parameters["TitleReportName"].Value = m_strReportTitle;

                objrpt.CreateDocument();

                this.PrintControl.PrintingSystem = objrpt.PrintingSystem;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
            }
        }

        #endregion

        #region Background Worker
        private void DT_Show_Estimation_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //Panel_Show_Estimation_ProgressBar.Value = e.ProgressPercentage;
        }

        private void DT_Show_Estimation_DoWork(object sender, DoWorkEventArgs e)
        {
            //try
            //{
            //    //XtraReport report = XtraReport.FromFile(strPath, true);
            //    //report.DataSource = dsReport.Tables[0];
            //    //report.DataMember = string.Empty;
            //    //ReportPrintTool tool = new ReportPrintTool(report);
            //    //tool.ShowPreview(); 

            //    XtraReport report = XtraReport.FromFile(strPath, true);
            //    report.DataSource = dsReport.Tables[0];
            //    report.DataMember = string.Empty;

            //    // for display subreport in disconnected mode

            //    //XtraReport Subreport = XtraReport.FromFile("ColorSummaryNew.repx", true);
            //    //Subreport.DataSource = dsReport.Tables[0];
            //    //Subreport.DataMember = string.Empty;

            //    //foreach (XRControl control in report.Bands[BandKind.ReportHeader].Controls)
            //    //{
            //    //    if (control is XRSubreport)
            //    //    {
            //    //        if (control.Name == "xrSubreport6")
            //    //        {
            //    //            (control as XRSubreport).ReportSource = Subreport;
            //    //            (control as XRSubreport).ReportSource.DataSource = dsReport.Tables[0];
            //    //            (control as XRSubreport).ReportSource.DataMember = string.Empty;
            //    //        }
            //    //        // For the DetailReportBands
            //    //    }
            //    //    else if (control is Band)
            //    //    {
            //    //       // PreloadSubreports(control as Band);
            //    //    }
            //    //}

            //    XRSubreport xrSubreport1 = new XRSubreport();
            //    xrSubreport1 = report.Bands[BandKind.ReportHeader].FindControl("xrSubreport1", true) as XRSubreport;
            //    xrSubreport1.ReportSource.DataSource = dsReport.Tables[0];
            //    xrSubreport1.ReportSource.DataMember = string.Empty;
            //    xrSubreport1.ReportSource.FilterString = string.Empty;

            //    XRSubreport xrSubreport2 = new XRSubreport();
            //    xrSubreport2 = report.Bands[BandKind.ReportHeader].FindControl("xrSubreport2", true) as XRSubreport;
            //    xrSubreport2.ReportSource.DataSource = dsReport.Tables[0];
            //    xrSubreport2.ReportSource.DataMember = string.Empty;
            //    xrSubreport2.ReportSource.FilterString = string.Empty;

            //    XRSubreport xrSubreport3 = new XRSubreport();
            //    xrSubreport3 = report.Bands[BandKind.ReportHeader].FindControl("xrSubreport3", true) as XRSubreport;
            //    xrSubreport3.ReportSource.DataSource = dsReport.Tables[0];
            //    xrSubreport3.ReportSource.DataMember = string.Empty;
            //    xrSubreport3.ReportSource.FilterString = string.Empty;

            //    XRSubreport xrSubreport4 = new XRSubreport();
            //    xrSubreport4 = report.Bands[BandKind.ReportHeader].FindControl("xrSubreport4", true) as XRSubreport;
            //    xrSubreport4.ReportSource.DataSource = dsReport.Tables[0];
            //    xrSubreport4.ReportSource.DataMember = string.Empty;
            //    xrSubreport4.ReportSource.FilterString = string.Empty;

            //    XRSubreport xrSubreport5 = new XRSubreport();
            //    xrSubreport5 = report.Bands[BandKind.ReportHeader].FindControl("xrSubreport5", true) as XRSubreport;
            //    xrSubreport5.ReportSource.DataSource = dsReportSS.Tables[0];
            //    xrSubreport5.ReportSource.DataMember = string.Empty;
            //    xrSubreport5.ReportSource.FilterString = string.Empty;

            //    XRSubreport xrSubreport7 = new XRSubreport();
            //    xrSubreport7 = report.Bands[BandKind.ReportHeader].FindControl("xrSubreport7", true) as XRSubreport;
            //    xrSubreport7.ReportSource.DataSource = dsReport.Tables[0];
            //    xrSubreport7.ReportSource.DataMember = string.Empty;
            //    xrSubreport7.ReportSource.FilterString = string.Empty;

            //    XRSubreport xrSubreport6 = new XRSubreport();  // size
            //    xrSubreport6 = report.Bands[BandKind.ReportHeader].FindControl("xrSubreport6", true) as XRSubreport;
            //    xrSubreport6.ReportSource.DataSource = dsReport.Tables[0];
            //    xrSubreport6.ReportSource.DataMember = string.Empty;
            //    xrSubreport6.ReportSource.FilterString = string.Empty;

            //    XRSubreport xrSubreport8 = new XRSubreport(); // florosence
            //    xrSubreport8 = report.Bands[BandKind.ReportHeader].FindControl("xrSubreport8", true) as XRSubreport;
            //    xrSubreport8.ReportSource.DataSource = dsReport.Tables[0];
            //    xrSubreport8.ReportSource.DataMember = string.Empty;
            //    xrSubreport8.ReportSource.FilterString = string.Empty;

            //    XRSubreport xrSubreport9 = new XRSubreport(); // CPS -Cut-Polish-Symmetry
            //    xrSubreport9 = report.Bands[BandKind.ReportHeader].FindControl("xrSubreport9", true) as XRSubreport;
            //    xrSubreport9.ReportSource.DataSource = dsReport.Tables[0];
            //    xrSubreport9.ReportSource.DataMember = string.Empty;
            //    xrSubreport9.ReportSource.FilterString = string.Empty;

            //    XRSubreport xrSubreport10 = new XRSubreport(); // pOINTER-Symmetry
            //    xrSubreport10 = report.Bands[BandKind.ReportHeader].FindControl("xrSubreport10", true) as XRSubreport;
            //    xrSubreport10.ReportSource.DataSource = dsReport.Tables[0];
            //    xrSubreport10.ReportSource.DataMember = string.Empty;
            //    xrSubreport10.ReportSource.FilterString = string.Empty;


            //    XRSubreport xrSubreport11 = new XRSubreport(); // Emp-Symmetry
            //    xrSubreport11 = report.Bands[BandKind.ReportHeader].FindControl("xrSubreport11", true) as XRSubreport;
            //    xrSubreport11.ReportSource.DataSource = dsReport.Tables[0];
            //    xrSubreport11.ReportSource.DataMember = string.Empty;
            //    xrSubreport11.ReportSource.FilterString = string.Empty;


            //    XRSubreport xrSubreport12 = new XRSubreport(); // Cert Type-Symmetry
            //    xrSubreport12 = report.Bands[BandKind.ReportHeader].FindControl("xrSubreport12", true) as XRSubreport;
            //    xrSubreport12.ReportSource.DataSource = dsReport.Tables[0];
            //    xrSubreport12.ReportSource.DataMember = string.Empty;
            //    xrSubreport12.ReportSource.FilterString = string.Empty;

            //    XRSubreport xrSubreport13 = new XRSubreport(); // Order-Symmetry
            //    xrSubreport13 = report.Bands[BandKind.ReportHeader].FindControl("xrSubreport13", true) as XRSubreport;
            //    xrSubreport13.ReportSource.DataSource = dsReport.Tables[0];
            //    xrSubreport13.ReportSource.DataMember = string.Empty;
            //    xrSubreport13.ReportSource.FilterString = string.Empty;


            //    //dfgdfgd

            //    report.Parameters["SelectionCriteria"].Value = strReportParaselectioncriteria.ToString();

            //    report.RequestParameters = false;

            //    ReportPrintTool tool = new ReportPrintTool(report);
            //    tool.AutoShowParametersPanel = false;
            //    tool.ShowPreviewDialog();



            //    //ReportPrintTool tool = new ReportPrintTool(report);
            //    //tool.ShowPreview();

            //    //report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.Print, CommandVisibility.None);
            //    //report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.PrintDirect, CommandVisibility.None);
            //    //report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.Save, CommandVisibility.None);
            //    //report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.SendFile, CommandVisibility.None);
            //    //report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportFile, CommandVisibility.None);
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
        }

        private void DT_Show_Estimation_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        #endregion

        public void GenReport_Estimation()
        {
            //BackgroundWorker DT_Show_Estimation = new BackgroundWorker();
            //DT_Show_Estimation.DoWork += DT_Show_Estimation_DoWork;
            //DT_Show_Estimation.RunWorkerCompleted += DT_Show_Estimation_RunWorkerCompleted;
            //DT_Show_Estimation.ProgressChanged += DT_Show_Estimation_ProgressChanged;
            //DT_Show_Estimation.WorkerReportsProgress = true;
            //DT_Show_Estimation.WorkerSupportsCancellation = true;
            //DT_Show_Estimation.RunWorkerAsync();
        }
    }
}