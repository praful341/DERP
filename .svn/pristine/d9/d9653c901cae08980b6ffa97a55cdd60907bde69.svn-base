using BLL;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DERP.Class;
using System;
using System.Data;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace DERP.Report
{
    public partial class FrmReportViewer : DevExpress.XtraEditors.XtraForm
    {
        Validation Val = new Validation();
        private DataSet _DS = new DataSet();

        PrinterSettings printerSettings = new PrinterSettings();
        public DataSet DS
        {
            get { return _DS; }
            set { _DS = value; }
        }
        public string mStrRefreshFormType = "";

        public enum ReportFolder
        {

            NONE = 0,
            JANGED = 1,
            EMP = 2,
            PURCHASE_ROUGH = 3,
            MIX = 4,
            HRACTS = 5,
            HR_REPORT = 6,
            ACCOUNT = 7,
            PRICE_LIST = 8,
            FINAL_OK_SUB = 9,
            FINAL_MAIN_MUMBAI = 10,
            FINAL_MAIN = 11,
            SEMI2_SURAT = 12,
            SURAT_PRICE_LIST = 13,
            SURAT_OLD_PRICE_LIST = 14,
            FINAL_MAIN_ALL_MUMBAI = 15,
            PATA_LOT_ENTRY = 16,
            MINUS2_MUMBAI_ASSORTMENT = 17,
            MUMBAI_PRICE_LIST = 18,
            CUT_COMPARISON = 19,
            CUT_COMPARISON_AVG = 20,
            FINAL_DIFFERENCE_CUT = 21,
            CUT_COMPARISON_DIFF = 22,
            MUMBAI_CUT_COMPARISON_DIFF = 23,
            CUT_COMPARISON_DIFF_SUMMARY = 24,
            DEPARTMENT_COSTING = 25,
            PARTY_NAME_PRINT = 26,
            WEEKLY_PRICE_LIST = 27
        }

        public string RepHead = string.Empty;
        public string RepName = string.Empty;
        public string PROCESS = string.Empty;
        public string RepPara = string.Empty;
        public string RepType = string.Empty;

        public string AddressType = string.Empty;

        public string GroupBy = string.Empty;
        public string FromDate = string.Empty;
        public string ToDate = string.Empty;

        public string Address = string.Empty;
        public string PhoneNo = string.Empty;

        public double DollarRate = 0.000;
        public double ProcessCost = 0.000;
        public double OverHead = 0.000;
        public bool IsPrintDate = false;

        /// <summary>
        /// Property For Set Dynamic Grouping Formulas
        /// </summary>
        public bool AllowSetFormula = false;
        public FrmReportViewer()
        {
            InitializeComponent();
        }
        public void ShowForm(string pStrRPTName, int pIntZoom = 120, ReportFolder pEnumReportFolder = ReportFolder.NONE)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                string StrPath = Application.StartupPath + "\\RPT\\";

                StrPath = StrPath + pEnumReportFolder.ToString() + "\\";

                StrPath = StrPath + pStrRPTName + ".rpt";

                RepDoc.Load(StrPath);

                if (DS != null)
                {
                    if (DS.Tables.Count > 0)
                        RepDoc.SetDataSource(DS.Tables[0]);
                }
                //TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                //TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                //ConnectionInfo crConnectionInfo = new ConnectionInfo();

                for (int IntI = 0; IntI < RepDoc.Subreports.Count; IntI++)
                {
                    RepDoc.Subreports[IntI].SetDataSource(DS.Tables[IntI + 1]);
                }
                CryViewer.ReportSource = RepDoc;

                CryViewer.ShowParameterPanelButton = false;
                CryViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;

                CryViewer.Refresh();
                CryViewer.Zoom(pIntZoom);

                SetParaMeter();
                this.Cursor = Cursors.Default;
                this.Show();
            }
            catch (Exception Ex)
            {
                this.Cursor = Cursors.Default;
                Global.Confirm(Ex.ToString());
            }
        }
        public void Email_ShowForm(string pStrRPTName, int pIntZoom = 120, ReportFolder pEnumReportFolder = ReportFolder.NONE)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                string StrPath = Application.StartupPath + "\\RPT\\";

                StrPath = StrPath + pEnumReportFolder.ToString() + "\\";

                StrPath = StrPath + pStrRPTName + ".rpt";

                RepDoc.Load(StrPath);

                if (DS != null)
                {
                    if (DS.Tables.Count > 0)
                        RepDoc.SetDataSource(DS.Tables[0]);
                }
                //TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                //TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                //ConnectionInfo crConnectionInfo = new ConnectionInfo();

                for (int IntI = 0; IntI < RepDoc.Subreports.Count; IntI++)
                {
                    RepDoc.Subreports[IntI].SetDataSource(DS.Tables[IntI + 1]);
                }
                CryViewer.ReportSource = RepDoc;

                CryViewer.ShowParameterPanelButton = false;
                CryViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;

                CryViewer.Refresh();
                CryViewer.Zoom(pIntZoom);

                SetParaMeter();
                this.Cursor = Cursors.Default;

                ExportOptions rptExportOption;
                DiskFileDestinationOptions rptFileDestOption = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions rptFormatOption = new PdfRtfWordFormatOptions();

                string DestinationFinalPath_Email = @"C:\";
                CultureInfo culture = new CultureInfo("es-ES");
                DateTime Date = new DateTime();
                String myDate = Convert.ToString(DateTime.Now.ToShortDateString());
                Date = DateTime.Parse(myDate, culture);
                string Month_Format = Date.ToString("MMM-yyyy");
                string Date_Format = Date.ToString("dd");

                if (!Directory.Exists(DestinationFinalPath_Email + "\\" + Month_Format))
                {
                    Directory.CreateDirectory(DestinationFinalPath_Email + "\\" + Month_Format);
                }
                if (!Directory.Exists(DestinationFinalPath_Email + "\\" + Month_Format + "\\" + Date_Format))
                {
                    Directory.CreateDirectory(DestinationFinalPath_Email + "\\" + Month_Format + "\\" + Date_Format);
                }

                //string reportFileName = @"C:\12.pdf";

                string Time = System.DateTime.Now.ToString().Replace(":", ".");

                string reportFileName = DestinationFinalPath_Email + "\\" + Month_Format + "\\" + Date_Format + "\\" + Time + ".pdf";
                rptFileDestOption.DiskFileName = reportFileName;
                rptExportOption = RepDoc.ExportOptions;
                {
                    rptExportOption.ExportDestinationType = ExportDestinationType.DiskFile;
                    //if we want to generate the report as PDF, change the ExportFormatType as "ExportFormatType.PortableDocFormat"
                    //if we want to generate the report as Excel, change the ExportFormatType as "ExportFormatType.Excel"
                    rptExportOption.ExportFormatType = ExportFormatType.PortableDocFormat;
                    rptExportOption.ExportDestinationOptions = rptFileDestOption;
                    rptExportOption.ExportFormatOptions = rptFormatOption;
                }

                RepDoc.Export();

                MailMessage mail = new MailMessage();
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("vavadiya.praful23@gmail.com");
                mail.To.Add("vavadiya.heena23@gmail.com");
                mail.Subject = "Test Mail - 1";
                mail.Body = "mail with attachment";

                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(reportFileName);
                mail.Attachments.Add(attachment);

                SmtpServer.Port = 587;

                SmtpServer.EnableSsl = true;
                SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                SmtpServer.UseDefaultCredentials = false;

                //SmtpServer.Credentials = new System.Net.NetworkCredential("mayurbodar525@gmail.com", "edouiqehliyfjclz");.
                SmtpServer.Credentials = new System.Net.NetworkCredential("vavadiya.praful23@gmail.com", "nxbdyzzjsclqpwow");
                SmtpServer.Send(mail);
                MessageBox.Show("Mail Sent Successfully");

            }
            catch (Exception Ex)
            {
                this.Cursor = Cursors.Default;
                Global.Confirm(Ex.ToString());
            }
        }
        public void ShowForm_SubReport(string pStrRPTName, int pIntZoom = 120, ReportFolder pEnumReportFolder = ReportFolder.NONE)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                string StrPath = Application.StartupPath + "\\RPT\\";

                StrPath = StrPath + pEnumReportFolder.ToString() + "\\";

                StrPath = StrPath + pStrRPTName + ".rpt";


                RepDoc.Load(StrPath);

                if (DS != null)
                {
                    if (DS.Tables.Count > 0)
                    {
                        if (pStrRPTName == "CrtPolishGrading_Semi2_Main")
                        {
                            RepDoc.SetDataSource(DS.Tables[5]);
                            RepDoc.Subreports[0].SetDataSource(DS.Tables[0]);
                            RepDoc.Subreports[1].SetDataSource(DS.Tables[1]);
                            RepDoc.Subreports[2].SetDataSource(DS.Tables[2]);
                            RepDoc.Subreports[3].SetDataSource(DS.Tables[3]);
                            RepDoc.Subreports[4].SetDataSource(DS.Tables[4]);
                            RepDoc.Subreports[5].SetDataSource(DS.Tables[6]);
                            RepDoc.Subreports[6].SetDataSource(DS.Tables[7]);
                            RepDoc.Subreports[7].SetDataSource(DS.Tables[8]);
                            RepDoc.Subreports[8].SetDataSource(DS.Tables[9]);
                            RepDoc.Subreports[9].SetDataSource(DS.Tables[10]);
                            RepDoc.Subreports[10].SetDataSource(DS.Tables[11]);
                        }
                        else if (pStrRPTName == "CrtPolishGrading_Semi2_Surat_Main")
                        {
                            //RepDoc.SetDataSource(DS.Tables[5]);
                            //RepDoc.Subreports[0].SetDataSource(DS.Tables[0]);
                            //RepDoc.Subreports[1].SetDataSource(DS.Tables[1]);
                            //RepDoc.Subreports[2].SetDataSource(DS.Tables[2]);
                            //RepDoc.Subreports[3].SetDataSource(DS.Tables[3]);
                            //RepDoc.Subreports[4].SetDataSource(DS.Tables[4]);
                            //RepDoc.Subreports[5].SetDataSource(DS.Tables[6]);
                            //RepDoc.Subreports[6].SetDataSource(DS.Tables[7]);
                            //RepDoc.Subreports[7].SetDataSource(DS.Tables[8]);
                            //RepDoc.Subreports[8].SetDataSource(DS.Tables[9]);
                            //RepDoc.Subreports[9].SetDataSource(DS.Tables[10]);
                            //RepDoc.Subreports[10].SetDataSource(DS.Tables[11]);


                            RepDoc.SetDataSource(DS.Tables[5]);
                            RepDoc.Subreports["CrtPolishGrading_Semi2_Sub_A.rpt"].SetDataSource(DS.Tables[1]);
                            RepDoc.Subreports["CrtPolishGrading_Semi2_Sub_B.rpt"].SetDataSource(DS.Tables[2]);
                            RepDoc.Subreports["CrtPolishGrading_Semi2_Sub_C.rpt"].SetDataSource(DS.Tables[3]);
                            RepDoc.Subreports["CrtPolishGrading_Semi2_Sub_D.rpt"].SetDataSource(DS.Tables[8]);
                            RepDoc.Subreports["CrtPolishGrading_Semi2_Sub_E.rpt"].SetDataSource(DS.Tables[10]);
                            RepDoc.Subreports["CrtPolishGrading_Semi2_Sub_F.rpt"].SetDataSource(DS.Tables[6]);
                            RepDoc.Subreports["CrtPolishGrading_Semi2_Sub_G.rpt"].SetDataSource(DS.Tables[0]);
                            RepDoc.Subreports["CrtPolishGrading_Semi2_Sub_H.rpt"].SetDataSource(DS.Tables[11]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_OK_B.rpt"].SetDataSource(DS.Tables[1]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_OK_C.rpt"].SetDataSource(DS.Tables[2]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_OK_D.rpt"].SetDataSource(DS.Tables[3]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_OK_E.rpt"].SetDataSource(DS.Tables[4]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_OK_F.rpt"].SetDataSource(DS.Tables[6]);

                        }
                        else if (pStrRPTName == "CrtPolishGrading_Semi2_Main_Mumbai")
                        {
                            RepDoc.SetDataSource(DS.Tables[5]);
                            RepDoc.Subreports[0].SetDataSource(DS.Tables[0]);
                            RepDoc.Subreports[1].SetDataSource(DS.Tables[1]);
                            RepDoc.Subreports[2].SetDataSource(DS.Tables[2]);
                            RepDoc.Subreports[3].SetDataSource(DS.Tables[3]);
                            RepDoc.Subreports[4].SetDataSource(DS.Tables[4]);
                            RepDoc.Subreports[5].SetDataSource(DS.Tables[6]);
                            RepDoc.Subreports[6].SetDataSource(DS.Tables[7]);
                            RepDoc.Subreports[7].SetDataSource(DS.Tables[8]);
                            RepDoc.Subreports[8].SetDataSource(DS.Tables[9]);
                            RepDoc.Subreports[9].SetDataSource(DS.Tables[10]);
                        }
                        else if (pStrRPTName == "CrtPolishGrading_Janged_IsuReturn_Main")
                        {
                            RepDoc.SetDataSource(DS.Tables[10]);
                            RepDoc.Subreports[0].SetDataSource(DS.Tables[0]);
                            RepDoc.Subreports[1].SetDataSource(DS.Tables[1]);
                            RepDoc.Subreports[2].SetDataSource(DS.Tables[2]);
                            RepDoc.Subreports[3].SetDataSource(DS.Tables[3]);
                            RepDoc.Subreports[4].SetDataSource(DS.Tables[0]);
                            RepDoc.Subreports[5].SetDataSource(DS.Tables[1]);
                            RepDoc.Subreports[6].SetDataSource(DS.Tables[2]);
                            RepDoc.Subreports[7].SetDataSource(DS.Tables[3]);
                            RepDoc.Subreports[8].SetDataSource(DS.Tables[6]);
                            RepDoc.Subreports[9].SetDataSource(DS.Tables[7]);
                            RepDoc.Subreports[10].SetDataSource(DS.Tables[8]);
                            RepDoc.Subreports[11].SetDataSource(DS.Tables[9]);
                            RepDoc.Subreports[12].SetDataSource(DS.Tables[6]);
                            RepDoc.Subreports[13].SetDataSource(DS.Tables[7]);
                            RepDoc.Subreports[14].SetDataSource(DS.Tables[8]);
                            RepDoc.Subreports[15].SetDataSource(DS.Tables[9]);
                        }
                        else if (pStrRPTName == "CrtPolishGrading_Final_Main")
                        {
                            DataTable DTab = DS.Tables[0];

                            DataTable d = new DataTable();

                            d = DTab.Copy();

                            for (int i = 0; i < DTab.Rows.Count; i++)
                            {
                                if (i != 0)
                                {
                                    d.Rows[i].Delete();
                                }
                            }

                            d.AcceptChanges();

                            RepDoc.SetDataSource(d);
                            //RepDoc.Subreports[0].SetDataSource(DS.Tables[0]);
                            //RepDoc.Subreports[2].SetDataSource(DS.Tables[1]);
                            //RepDoc.Subreports[1].SetDataSource(DS.Tables[2]);
                            //RepDoc.Subreports[3].SetDataSource(DS.Tables[3]);
                            //RepDoc.Subreports[4].SetDataSource(DS.Tables[4]);
                            //RepDoc.Subreports[5].SetDataSource(DS.Tables[6]);
                            RepDoc.Subreports["CrtPolishGrading_Final_OK_A.rpt"].SetDataSource(DS.Tables[0]);
                            RepDoc.Subreports["CrtPolishGrading_Final_OK_B.rpt"].SetDataSource(DS.Tables[1]);
                            RepDoc.Subreports["CrtPolishGrading_Final_OK_C.rpt"].SetDataSource(DS.Tables[2]);
                            RepDoc.Subreports["CrtPolishGrading_Final_OK_D.rpt"].SetDataSource(DS.Tables[3]);
                            RepDoc.Subreports["CrtPolishGrading_Final_OK_E.rpt"].SetDataSource(DS.Tables[4]);
                            RepDoc.Subreports["CrtPolishGrading_Final_OK_F.rpt"].SetDataSource(DS.Tables[6]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_Entry_Date.rpt"].SetDataSource(DS.Tables[7]);
                        }
                        else if (pStrRPTName == "CrtPolishGrading_Final_Main_New")
                        {
                            DataTable DTab = DS.Tables[0];

                            DataTable d = new DataTable();

                            d = DTab.Copy();

                            for (int i = 0; i < DTab.Rows.Count; i++)
                            {
                                if (i != 0)
                                {
                                    d.Rows[i].Delete();
                                }
                            }

                            d.AcceptChanges();

                            RepDoc.SetDataSource(d);
                            RepDoc.Subreports[0].SetDataSource(DS.Tables[0]);
                            RepDoc.Subreports[2].SetDataSource(DS.Tables[1]);
                            RepDoc.Subreports[1].SetDataSource(DS.Tables[2]);
                            RepDoc.Subreports[3].SetDataSource(DS.Tables[3]);
                            RepDoc.Subreports[4].SetDataSource(DS.Tables[4]);
                            RepDoc.Subreports[5].SetDataSource(DS.Tables[6]);
                        }
                        else if (pStrRPTName == "CrtPolishGrading_Final_OK_Main")
                        {
                            DataTable DTab = DS.Tables[0];

                            DataTable d = new DataTable();

                            d = DTab.Copy();

                            for (int i = 0; i < DTab.Rows.Count; i++)
                            {
                                if (i != 0)
                                {
                                    d.Rows[i].Delete();
                                }
                            }

                            d.AcceptChanges();

                            RepDoc.SetDataSource(d);

                            RepDoc.Subreports[0].SetDataSource(DS.Tables[0]);
                            RepDoc.Subreports[2].SetDataSource(DS.Tables[1]);
                            RepDoc.Subreports[1].SetDataSource(DS.Tables[2]);
                            RepDoc.Subreports[3].SetDataSource(DS.Tables[3]);
                            RepDoc.Subreports[4].SetDataSource(DS.Tables[4]);
                            RepDoc.Subreports[5].SetDataSource(DS.Tables[6]);

                            //RepDoc.Subreports["CrtPolishGrading_Final_OK_A.rpt"].SetDataSource(DS.Tables[0]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_OK_B.rpt"].SetDataSource(DS.Tables[1]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_OK_C.rpt"].SetDataSource(DS.Tables[2]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_OK_D.rpt"].SetDataSource(DS.Tables[3]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_OK_E.rpt"].SetDataSource(DS.Tables[4]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_OK_F.rpt"].SetDataSource(DS.Tables[6]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_FW_Color_Sub.rpt"].SetDataSource(DS.Tables[7]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_FW_Size_Sub.rpt"].SetDataSource(DS.Tables[8]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_FW_Cutting_Part1_Sub.rpt"].SetDataSource(DS.Tables[9]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_FW_Cutting_Part2_Sub.rpt"].SetDataSource(DS.Tables[10]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_FW_Cutting_Part3_Sub.rpt"].SetDataSource(DS.Tables[11]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_FW_Cutting_Part4_Sub.rpt"].SetDataSource(DS.Tables[12]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_FW_Purity_VVS.rpt"].SetDataSource(DS.Tables[13]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_FW_Purity_VS.rpt"].SetDataSource(DS.Tables[14]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_FW_Purity_SI.rpt"].SetDataSource(DS.Tables[15]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_FW_Purity_I.rpt"].SetDataSource(DS.Tables[16]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_FW_Purity_PK.rpt"].SetDataSource(DS.Tables[17]);
                        }
                        else if (pStrRPTName == "CrtPolishGrading_Final_OK_Main_Print2")
                        {
                            DataTable DTab = DS.Tables[0];

                            DataTable d = new DataTable();

                            d = DTab.Copy();

                            for (int i = 0; i < DTab.Rows.Count; i++)
                            {
                                if (i != 0)
                                {
                                    d.Rows[i].Delete();
                                }
                            }

                            d.AcceptChanges();

                            RepDoc.SetDataSource(d);

                            RepDoc.Subreports["CrtPolishGrading_Final_OK_A.rpt"].SetDataSource(DS.Tables[7]);
                            RepDoc.Subreports["CrtPolishGrading_Final_OK_B.rpt"].SetDataSource(DS.Tables[9]);
                            RepDoc.Subreports["CrtPolishGrading_Final_OK_C.rpt"].SetDataSource(DS.Tables[2]);
                            RepDoc.Subreports["CrtPolishGrading_Final_OK_D.rpt"].SetDataSource(DS.Tables[3]);
                            RepDoc.Subreports["CrtPolishGrading_Final_OK_E.rpt"].SetDataSource(DS.Tables[4]);
                            RepDoc.Subreports["CrtPolishGrading_Final_OK_F.rpt"].SetDataSource(DS.Tables[6]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_Entry_Date.rpt"].SetDataSource(DS.Tables[21]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_FW_Color_Sub.rpt"].SetDataSource(DS.Tables[10]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_FW_Size_Sub.rpt"].SetDataSource(DS.Tables[11]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_FW_Cutting_Part1_Sub.rpt"].SetDataSource(DS.Tables[12]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_FW_Cutting_Part2_Sub.rpt"].SetDataSource(DS.Tables[13]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_FW_Cutting_Part3_Sub.rpt"].SetDataSource(DS.Tables[14]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_FW_Cutting_Part4_Sub.rpt"].SetDataSource(DS.Tables[15]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_FW_Purity_VVS.rpt"].SetDataSource(DS.Tables[16]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_FW_Purity_VS.rpt"].SetDataSource(DS.Tables[17]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_FW_Purity_SI.rpt"].SetDataSource(DS.Tables[18]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_FW_Purity_I.rpt"].SetDataSource(DS.Tables[19]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_FW_Purity_PK.rpt"].SetDataSource(DS.Tables[20]);
                        }
                        else if (pStrRPTName == "CrtPolishGrading_Final_OK_Main_Print3")
                        {
                            DataTable DTab = DS.Tables[0];

                            DataTable d = new DataTable();

                            d = DTab.Copy();

                            for (int i = 0; i < DTab.Rows.Count; i++)
                            {
                                if (i != 0)
                                {
                                    d.Rows[i].Delete();
                                }
                            }

                            d.AcceptChanges();

                            RepDoc.SetDataSource(d);

                            //RepDoc.Subreports["CrtPolishGrading_Final_OK_A.rpt"].SetDataSource(DS.Tables[7]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_OK_B.rpt"].SetDataSource(DS.Tables[9]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_OK_C.rpt"].SetDataSource(DS.Tables[2]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_OK_D.rpt"].SetDataSource(DS.Tables[3]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_OK_E.rpt"].SetDataSource(DS.Tables[4]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_OK_F.rpt"].SetDataSource(DS.Tables[6]);
                            RepDoc.Subreports["CrtPolishGrading_Final_FW_Color_Sub.rpt"].SetDataSource(DS.Tables[10]);
                            RepDoc.Subreports["CrtPolishGrading_Final_FW_Size_Sub.rpt"].SetDataSource(DS.Tables[11]);
                            RepDoc.Subreports["CrtPolishGrading_Final_FW_Cutting_Part1_Sub.rpt"].SetDataSource(DS.Tables[12]);
                            RepDoc.Subreports["CrtPolishGrading_Final_FW_Cutting_Part2_Sub.rpt"].SetDataSource(DS.Tables[13]);
                            RepDoc.Subreports["CrtPolishGrading_Final_FW_Cutting_Part3_Sub.rpt"].SetDataSource(DS.Tables[14]);
                            RepDoc.Subreports["CrtPolishGrading_Final_FW_Cutting_Part4_Sub.rpt"].SetDataSource(DS.Tables[15]);
                            RepDoc.Subreports["CrtPolishGrading_Final_FW_Purity_VVS.rpt"].SetDataSource(DS.Tables[16]);
                            RepDoc.Subreports["CrtPolishGrading_Final_FW_Purity_VS.rpt"].SetDataSource(DS.Tables[17]);
                            RepDoc.Subreports["CrtPolishGrading_Final_FW_Purity_SI.rpt"].SetDataSource(DS.Tables[18]);
                            RepDoc.Subreports["CrtPolishGrading_Final_FW_Purity_I.rpt"].SetDataSource(DS.Tables[19]);
                            RepDoc.Subreports["CrtPolishGrading_Final_FW_Purity_PK.rpt"].SetDataSource(DS.Tables[20]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_Entry_Date.rpt"].SetDataSource(DS.Tables[21]);
                        }
                        else if (pStrRPTName == "CrtPolishGrading_Final_Difference_Main_Print3")
                        {
                            DataTable DTab = DS.Tables[0];

                            DataTable d = new DataTable();

                            d = DTab.Copy();

                            for (int i = 0; i < DTab.Rows.Count; i++)
                            {
                                if (i != 0)
                                {
                                    d.Rows[i].Delete();
                                }
                            }

                            d.AcceptChanges();

                            RepDoc.SetDataSource(d);

                            RepDoc.Subreports["CrtPolishGrading_Final_FW_Color_Sub.rpt"].SetDataSource(DS.Tables[0]);
                            RepDoc.Subreports["CrtPolishGrading_Final_FW_Cutting_Part1_Sub.rpt"].SetDataSource(DS.Tables[1]);
                            RepDoc.Subreports["CrtPolishGrading_Final_FW_Cutting_Part2_Sub.rpt"].SetDataSource(DS.Tables[2]);
                            RepDoc.Subreports["CrtPolishGrading_Final_FW_Cutting_Part3_Sub.rpt"].SetDataSource(DS.Tables[3]);
                            RepDoc.Subreports["CrtPolishGrading_Final_FW_Cutting_Part4_Sub.rpt"].SetDataSource(DS.Tables[4]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_FW_Size_Sub.rpt"].SetDataSource(DS.Tables[11]);                           
                            RepDoc.Subreports["CrtPolishGrading_Final_FW_Purity_VVS.rpt"].SetDataSource(DS.Tables[5]);
                            RepDoc.Subreports["CrtPolishGrading_Final_FW_Purity_VS.rpt"].SetDataSource(DS.Tables[6]);
                            RepDoc.Subreports["CrtPolishGrading_Final_FW_Purity_SI.rpt"].SetDataSource(DS.Tables[7]);
                            RepDoc.Subreports["CrtPolishGrading_Final_FW_Purity_I.rpt"].SetDataSource(DS.Tables[8]);
                            RepDoc.Subreports["CrtPolishGrading_Final_FW_Purity_PK.rpt"].SetDataSource(DS.Tables[9]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_Entry_Date.rpt"].SetDataSource(DS.Tables[10]);
                        }
                        else if (pStrRPTName == "CrtPolishGrading_Final_Main_Mumbai")
                        {
                            DataTable DTab = DS.Tables[1];

                            DataTable d = new DataTable();

                            d = DTab.Copy();

                            for (int i = 0; i < DTab.Rows.Count; i++)
                            {
                                if (i != 0)
                                {
                                    d.Rows[i].Delete();
                                }
                            }

                            d.AcceptChanges();

                            RepDoc.SetDataSource(d);
                            RepDoc.Subreports["CrtPolishGrading_Final_Main_Mumbai_A.rpt"].SetDataSource(DS.Tables[1]);
                            RepDoc.Subreports["CrtPolishGrading_Final_Main_Mumbai_B.rpt"].SetDataSource(DS.Tables[7]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_Entry_Date.rpt"].SetDataSource(DS.Tables[18]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_FW_Size_Sub.rpt"].SetDataSource(DS.Tables[8]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_FW_Color_Sub.rpt"].SetDataSource(DS.Tables[9]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_FW_Cutting_Sub.rpt"].SetDataSource(DS.Tables[10]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_FW_Purity_IF.rpt"].SetDataSource(DS.Tables[11]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_FW_Purity_VVS.rpt"].SetDataSource(DS.Tables[12]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_FW_Purity_VS.rpt"].SetDataSource(DS.Tables[13]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_FW_Purity_SI.rpt"].SetDataSource(DS.Tables[14]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_FW_Purity_I.rpt"].SetDataSource(DS.Tables[15]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_Main_Mumbai_C.rpt"].SetDataSource(DS.Tables[16]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_Main_Mumbai_D.rpt"].SetDataSource(DS.Tables[17]);
                        }
                        else if (pStrRPTName == "CrtPolishGrading_Minus2_Mumbai")
                        {
                            DataTable DTab = DS.Tables[1];

                            DataTable d = new DataTable();

                            d = DTab.Copy();

                            for (int i = 0; i < DTab.Rows.Count; i++)
                            {
                                if (i != 0)
                                {
                                    d.Rows[i].Delete();
                                }
                            }

                            d.AcceptChanges();

                            RepDoc.SetDataSource(d);
                            RepDoc.Subreports["CrtPolishGrading_Final_Main_Mumbai_A.rpt"].SetDataSource(DS.Tables[1]);
                            RepDoc.Subreports["CrtPolishGrading_Final_Main_Mumbai_B.rpt"].SetDataSource(DS.Tables[2]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_Entry_Date.rpt"].SetDataSource(DS.Tables[3]);
                        }
                        else if (pStrRPTName == "CrtPolishGrading_Final_Main_Mumbai_Print2")
                        {
                            DataTable DTab = DS.Tables[1];

                            DataTable d = new DataTable();

                            d = DTab.Copy();

                            for (int i = 0; i < DTab.Rows.Count; i++)
                            {
                                if (i != 0)
                                {
                                    d.Rows[i].Delete();
                                }
                            }

                            d.AcceptChanges();

                            RepDoc.SetDataSource(d);
                            //RepDoc.Subreports["CrtPolishGrading_Final_Main_Mumbai_A.rpt"].SetDataSource(DS.Tables[1]);
                            //RepDoc.Subreports["CrtPolishGrading_Final_Main_Mumbai_B.rpt"].SetDataSource(DS.Tables[7]);
                            RepDoc.Subreports["CrtPolishGrading_Final_FW_Size_Sub.rpt"].SetDataSource(DS.Tables[8]);
                            RepDoc.Subreports["CrtPolishGrading_Final_FW_Color_Sub.rpt"].SetDataSource(DS.Tables[9]);
                            RepDoc.Subreports["CrtPolishGrading_Final_FW_Cutting_Sub.rpt"].SetDataSource(DS.Tables[10]);
                            RepDoc.Subreports["CrtPolishGrading_Final_FW_Purity_IF.rpt"].SetDataSource(DS.Tables[11]);
                            RepDoc.Subreports["CrtPolishGrading_Final_FW_Purity_VVS.rpt"].SetDataSource(DS.Tables[12]);
                            RepDoc.Subreports["CrtPolishGrading_Final_FW_Purity_VS.rpt"].SetDataSource(DS.Tables[13]);
                            RepDoc.Subreports["CrtPolishGrading_Final_FW_Purity_SI.rpt"].SetDataSource(DS.Tables[14]);
                            RepDoc.Subreports["CrtPolishGrading_Final_FW_Purity_I.rpt"].SetDataSource(DS.Tables[15]);
                            RepDoc.Subreports["CrtPolishGrading_Final_Main_Mumbai_C.rpt"].SetDataSource(DS.Tables[16]);
                            RepDoc.Subreports["CrtPolishGrading_Final_Main_Mumbai_D.rpt"].SetDataSource(DS.Tables[17]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_Entry_Date.rpt"].SetDataSource(DS.Tables[18]);
                        }
                        else if (pStrRPTName == "CrtPolishGrading_Final_Main_Mumbai_New")
                        {
                            DataTable DTab = DS.Tables[1];

                            DataTable d = new DataTable();

                            d = DTab.Copy();

                            for (int i = 0; i < DTab.Rows.Count; i++)
                            {
                                if (i != 0)
                                {
                                    d.Rows[i].Delete();
                                }
                            }

                            d.AcceptChanges();

                            RepDoc.SetDataSource(d);

                            RepDoc.Subreports["CrtPolishGrading_Final_Group_Sub.rpt"].SetDataSource(DS.Tables[1]);
                            RepDoc.Subreports["CrtPolishGrading_Final_Group_Sub1.rpt"].SetDataSource(DS.Tables[7]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_Entry_Date.rpt"].SetDataSource(DS.Tables[8]);

                            //RepDoc.Subreports[0].SetDataSource(DS.Tables[7]);
                            //RepDoc.Subreports[2].SetDataSource(DS.Tables[1]);
                            //RepDoc.Subreports[1].SetDataSource(DS.Tables[2]);
                            //RepDoc.Subreports[3].SetDataSource(DS.Tables[3]);
                            //RepDoc.Subreports[4].SetDataSource(DS.Tables[4]);
                            //RepDoc.Subreports[5].SetDataSource(DS.Tables[6]);
                        }
                        else if (pStrRPTName == "CrtPolishGrading_Final_Main_Print2")
                        {
                            DataTable DTab = DS.Tables[0];

                            DataTable d = new DataTable();

                            d = DTab.Copy();

                            for (int i = 0; i < DTab.Rows.Count; i++)
                            {
                                if (i != 0)
                                {
                                    d.Rows[i].Delete();
                                }
                            }

                            d.AcceptChanges();

                            RepDoc.SetDataSource(d);
                            RepDoc.Subreports[0].SetDataSource(DS.Tables[7]);
                            RepDoc.Subreports[2].SetDataSource(DS.Tables[9]);
                            RepDoc.Subreports[1].SetDataSource(DS.Tables[2]);
                            RepDoc.Subreports[3].SetDataSource(DS.Tables[3]);
                            RepDoc.Subreports[4].SetDataSource(DS.Tables[4]);
                            RepDoc.Subreports[5].SetDataSource(DS.Tables[6]);
                        }
                        else if (pStrRPTName == "Sales_Profit_Loss_Amount")
                        {
                            RepDoc.SetDataSource(DS.Tables[1]);
                            RepDoc.Subreports[0].SetDataSource(DS.Tables[0]);
                            RepDoc.Subreports[1].SetDataSource(DS.Tables[1]);
                            RepDoc.Subreports[2].SetDataSource(DS.Tables[2]);
                            RepDoc.Subreports[3].SetDataSource(DS.Tables[3]);
                            RepDoc.Subreports[4].SetDataSource(DS.Tables[4]);
                            RepDoc.Subreports[5].SetDataSource(DS.Tables[5]);
                        }
                        else if (pStrRPTName == "CrtCashTransfer_Cr_Dr_Main")
                        {
                            RepDoc.SetDataSource(DS.Tables[0]);
                            RepDoc.Subreports[0].SetDataSource(DS.Tables[0]);
                            RepDoc.Subreports[1].SetDataSource(DS.Tables[1]);
                        }
                        else if (pStrRPTName == "CrtPolishGrading_Price_List_RPT")
                        {
                            RepDoc.SetDataSource(DS.Tables[0]);

                            RepDoc.Subreports["CrtPolishGrading_Price_List_RPT_Sub.rpt"].SetDataSource(DS.Tables[0]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_RPT_Sub_N.rpt"].SetDataSource(DS.Tables[1]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_RPT_Sub_AIRLB.rpt"].SetDataSource(DS.Tables[2]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_RPT_Sub_TTLB.rpt"].SetDataSource(DS.Tables[3]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_RPT_Sub_NWLB.rpt"].SetDataSource(DS.Tables[4]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_RPT_Sub_OWLB.rpt"].SetDataSource(DS.Tables[5]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_RPT_Sub_AIRLC.rpt"].SetDataSource(DS.Tables[6]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_RPT_Sub_TTLC.rpt"].SetDataSource(DS.Tables[7]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_RPT_Sub_NWLC.rpt"].SetDataSource(DS.Tables[8]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_RPT_Sub_LBLC.rpt"].SetDataSource(DS.Tables[9]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_RPT_Sub_Full_White.rpt"].SetDataSource(DS.Tables[10]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_RPT_Sub_LC.rpt"].SetDataSource(DS.Tables[11]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_Entry_Date.rpt"].SetDataSource(DS.Tables[12]);
                        }
                        else if (pStrRPTName == "CrtPolishGrading_Weekly_Price_List_RPT")
                        {
                            RepDoc.SetDataSource(DS.Tables[0]);

                            RepDoc.Subreports["CrtPolishGrading_Price_List_RPT_Sub.rpt"].SetDataSource(DS.Tables[0]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_RPT_Sub_N.rpt"].SetDataSource(DS.Tables[1]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_RPT_Sub_AIRLB.rpt"].SetDataSource(DS.Tables[2]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_RPT_Sub_TTLB.rpt"].SetDataSource(DS.Tables[3]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_RPT_Sub_NWLB.rpt"].SetDataSource(DS.Tables[4]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_RPT_Sub_OWLB.rpt"].SetDataSource(DS.Tables[5]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_RPT_Sub_AIRLC.rpt"].SetDataSource(DS.Tables[6]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_RPT_Sub_TTLC.rpt"].SetDataSource(DS.Tables[7]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_RPT_Sub_NWLC.rpt"].SetDataSource(DS.Tables[8]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_RPT_Sub_LBLC.rpt"].SetDataSource(DS.Tables[9]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_RPT_Sub_Full_White.rpt"].SetDataSource(DS.Tables[10]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_RPT_Sub_LC.rpt"].SetDataSource(DS.Tables[11]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_Entry_Date.rpt"].SetDataSource(DS.Tables[12]);
                        }
                        else if (pStrRPTName == "CrtPolishGrading_Price_List_Mumbai_RPT")
                        {
                            RepDoc.SetDataSource(DS.Tables[0]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_RPT_Sub_Full_White.rpt"].SetDataSource(DS.Tables[0]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_RPT_Sub_LC.rpt"].SetDataSource(DS.Tables[1]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_Entry_Date.rpt"].SetDataSource(DS.Tables[2]);
                        }
                        else if (pStrRPTName == "CrtPolishGrading_Price_List_RPT_Diff")
                        {
                            RepDoc.SetDataSource(DS.Tables[0]);

                            RepDoc.Subreports["CrtPolishGrading_Price_List_RPT_Sub.rpt"].SetDataSource(DS.Tables[0]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_RPT_Sub_N.rpt"].SetDataSource(DS.Tables[1]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_RPT_Sub_AIRLB.rpt"].SetDataSource(DS.Tables[2]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_RPT_Sub_TTLB.rpt"].SetDataSource(DS.Tables[3]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_RPT_Sub_NWLB.rpt"].SetDataSource(DS.Tables[4]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_RPT_Sub_OWLB.rpt"].SetDataSource(DS.Tables[5]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_RPT_Sub_AIRLC.rpt"].SetDataSource(DS.Tables[6]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_RPT_Sub_TTLC.rpt"].SetDataSource(DS.Tables[7]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_RPT_Sub_NWLC.rpt"].SetDataSource(DS.Tables[8]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_RPT_Sub_LBLC.rpt"].SetDataSource(DS.Tables[9]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_RPT_Sub_Full_White.rpt"].SetDataSource(DS.Tables[10]);
                            RepDoc.Subreports["CrtPolishGrading_Price_List_RPT_Sub_LC.rpt"].SetDataSource(DS.Tables[11]);
                        }
                        else if (pStrRPTName == "Demand_Noting_RangeWise")
                        {
                            DataTable DTab = DS.Tables[0];

                            DataTable d = new DataTable();

                            d = DTab.Copy();

                            for (int i = 0; i < DTab.Rows.Count; i++)
                            {
                                if (i != 0)
                                {
                                    d.Rows[i].Delete();
                                }
                            }

                            d.AcceptChanges();

                            RepDoc.SetDataSource(d);

                            RepDoc.Subreports["Demand_Noting_RangeWise_Plus2.rpt"].SetDataSource(DS.Tables[0]);
                            RepDoc.Subreports["Demand_Noting_RangeWise_Minus2.rpt"].SetDataSource(DS.Tables[1]);
                            RepDoc.Subreports["Demand_Noting_RangeWise_Total.rpt"].SetDataSource(DS.Tables[2]);
                            RepDoc.Subreports["Demand_Noting_RangeWise_+2Total.rpt"].SetDataSource(DS.Tables[3]);
                            RepDoc.Subreports["Demand_Noting_RangeWise_-2Total.rpt"].SetDataSource(DS.Tables[4]);
                        }
                        else if (pStrRPTName == "Demand_Noting_Dahisar_RangeWise")
                        {
                            DataTable DTab = DS.Tables[0];

                            DataTable d = new DataTable();

                            d = DTab.Copy();

                            for (int i = 0; i < DTab.Rows.Count; i++)
                            {
                                if (i != 0)
                                {
                                    d.Rows[i].Delete();
                                }
                            }

                            d.AcceptChanges();

                            RepDoc.SetDataSource(d);

                            RepDoc.Subreports["Demand_Noting_Dahisar_RangeWise_Plus2.rpt"].SetDataSource(DS.Tables[0]);
                            RepDoc.Subreports["Demand_Noting_Dahisar_RangeWise_Minus2.rpt"].SetDataSource(DS.Tables[1]);
                            RepDoc.Subreports["Demand_Noting_Dahisar_RangeWise_Total.rpt"].SetDataSource(DS.Tables[2]);
                            RepDoc.Subreports["Demand_Noting_Dahisar_RangeWise_+2Total.rpt"].SetDataSource(DS.Tables[3]);
                            RepDoc.Subreports["Demand_Noting_Dahisar_RangeWise_-2Total.rpt"].SetDataSource(DS.Tables[4]);
                            RepDoc.Subreports["Demand_Noting_Dahisar_RangeWise_-00.rpt"].SetDataSource(DS.Tables[5]);
                        }
                        else if (pStrRPTName == "PataLot_Entry_MainRPT")
                        {
                            DataTable DTab = DS.Tables[0];

                            DataTable d = new DataTable();

                            d = DTab.Copy();

                            for (int i = 0; i < DTab.Rows.Count; i++)
                            {
                                if (i != 0)
                                {
                                    d.Rows[i].Delete();
                                }
                            }

                            d.AcceptChanges();

                            RepDoc.SetDataSource(d);
                            RepDoc.Subreports["PataLot_Entry_Part1.rpt"].SetDataSource(DS.Tables[0]);
                            RepDoc.Subreports["PataLot_Entry_Part2.rpt"].SetDataSource(DS.Tables[1]);
                        }
                        else if (pStrRPTName == "Cut_Comparison")
                        {
                            DataTable DTab = DS.Tables[0];

                            DataTable d = new DataTable();

                            d = DTab.Copy();

                            for (int i = 0; i < DTab.Rows.Count; i++)
                            {
                                if (i != 0)
                                {
                                    d.Rows[i].Delete();
                                }
                            }

                            d.AcceptChanges();
                            RepDoc.SetDataSource(d);

                            RepDoc.Subreports["Cut_Comparison_A.rpt"].SetDataSource(DS.Tables[0]);
                            RepDoc.Subreports["Cut_Comparison_B.rpt"].SetDataSource(DS.Tables[1]);
                            RepDoc.Subreports["Cut_Comparison_C.rpt"].SetDataSource(DS.Tables[2]);
                            RepDoc.Subreports["Cut_Comparison_D.rpt"].SetDataSource(DS.Tables[3]);
                            RepDoc.Subreports["Cut_Comparison_E.rpt"].SetDataSource(DS.Tables[4]);
                            RepDoc.Subreports["Cut_Comparison_F.rpt"].SetDataSource(DS.Tables[5]);
                            RepDoc.Subreports["Cut_Comparison_AVG.rpt"].SetDataSource(DS.Tables[6]);
                            RepDoc.Subreports["Cut_Comparison_To_AVG.rpt"].SetDataSource(DS.Tables[7]);
                        }
                        else if (pStrRPTName == "Cut_Comparison_New")
                        {
                            DataTable DTab = DS.Tables[0];

                            DataTable d = new DataTable();

                            d = DTab.Copy();

                            for (int i = 0; i < DTab.Rows.Count; i++)
                            {
                                if (i != 0)
                                {
                                    d.Rows[i].Delete();
                                }
                            }

                            d.AcceptChanges();

                            if (pEnumReportFolder.ToString() == "CUT_COMPARISON_DIFF")
                            {
                                RepDoc.SetDataSource(d);

                                RepDoc.Subreports["Cut_Comparison_A.rpt"].SetDataSource(DS.Tables[0]);
                                //RepDoc.Subreports["Cut_Comparison_B.rpt"].SetDataSource(DS.Tables[1]);
                                RepDoc.Subreports["Cut_Comparison_C.rpt"].SetDataSource(DS.Tables[1]);
                                //RepDoc.Subreports["Cut_Comparison_D.rpt"].SetDataSource(DS.Tables[2]);
                                RepDoc.Subreports["Cut_Comparison_E.rpt"].SetDataSource(DS.Tables[2]);
                                //RepDoc.Subreports["Cut_Comparison_F.rpt"].SetDataSource(DS.Tables[3]);
                                RepDoc.Subreports["Cut_Comparison_AVG.rpt"].SetDataSource(DS.Tables[3]);
                                //RepDoc.Subreports["Cut_Comparison_To_AVG.rpt"].SetDataSource(DS.Tables[4]);
                            }
                            else if (pEnumReportFolder.ToString() == "CUT_COMPARISON_DIFF_SUMMARY")
                            {
                                RepDoc.SetDataSource(d);

                                RepDoc.Subreports["Cut_Comparison_A.rpt"].SetDataSource(DS.Tables[0]);
                                //RepDoc.Subreports["Cut_Comparison_B.rpt"].SetDataSource(DS.Tables[1]);
                                RepDoc.Subreports["Cut_Comparison_C.rpt"].SetDataSource(DS.Tables[1]);
                                //RepDoc.Subreports["Cut_Comparison_D.rpt"].SetDataSource(DS.Tables[2]);
                                RepDoc.Subreports["Cut_Comparison_E.rpt"].SetDataSource(DS.Tables[2]);
                                //RepDoc.Subreports["Cut_Comparison_F.rpt"].SetDataSource(DS.Tables[3]);
                                RepDoc.Subreports["Cut_Comparison_AVG.rpt"].SetDataSource(DS.Tables[3]);
                                //RepDoc.Subreports["Cut_Comparison_To_AVG.rpt"].SetDataSource(DS.Tables[4]);
                            }
                            else if (pEnumReportFolder.ToString() == "MUMBAI_CUT_COMPARISON_DIFF")
                            {
                                RepDoc.SetDataSource(d);
                                RepDoc.Subreports["Cut_Comparison_A.rpt"].SetDataSource(DS.Tables[0]);
                            }
                        }
                        else if (pStrRPTName == "Cut_Comparison_Average")
                        {
                            DataTable DTab = DS.Tables[0];

                            DataTable d = new DataTable();

                            d = DTab.Copy();

                            for (int i = 0; i < DTab.Rows.Count; i++)
                            {
                                if (i != 0)
                                {
                                    d.Rows[i].Delete();
                                }
                            }

                            d.AcceptChanges();

                            RepDoc.SetDataSource(d);
                            RepDoc.Subreports["Cut_Comparison_A.rpt"].SetDataSource(DS.Tables[0]);
                            RepDoc.Subreports["Cut_Comparison_B.rpt"].SetDataSource(DS.Tables[1]);
                            RepDoc.Subreports["Cut_Comparison_C.rpt"].SetDataSource(DS.Tables[2]);
                        }
                        else if (pStrRPTName == "Main_Report")
                        {
                            DataTable DTab = DS.Tables[0];

                            DataTable d = new DataTable();

                            d = DTab.Copy();

                            for (int i = 0; i < DTab.Rows.Count; i++)
                            {
                                if (i != 0)
                                {
                                    d.Rows[i].Delete();
                                }
                            }

                            d.AcceptChanges();

                            RepDoc.SetDataSource(d);
                            RepDoc.Subreports["Main_Report_Rough.rpt"].SetDataSource(DS.Tables[0]);
                            RepDoc.Subreports["Main_Report_Sarin.rpt"].SetDataSource(DS.Tables[1]);
                            RepDoc.Subreports["Main_Report_4P.rpt"].SetDataSource(DS.Tables[2]);
                            RepDoc.Subreports["Main_Report_Russian.rpt"].SetDataSource(DS.Tables[3]);
                            RepDoc.Subreports["Main_Report_Polish.rpt"].SetDataSource(DS.Tables[4]);
                            RepDoc.Subreports["Main_Report_Polish_Repairing.rpt"].SetDataSource(DS.Tables[5]);
                            RepDoc.Subreports["Main_Report_Polish_Assortment.rpt"].SetDataSource(DS.Tables[6]);
                        }
                        else if (pStrRPTName == "Party_Main_Report")
                        {
                            RepDoc.SetDataSource(DS.Tables[0]);
                            RepDoc.Subreports["Party_1.rpt"].SetDataSource(DS.Tables[0]);
                            RepDoc.Subreports["Party_2.rpt"].SetDataSource(DS.Tables[0]);
                            RepDoc.Subreports["Party_3.rpt"].SetDataSource(DS.Tables[0]);
                            RepDoc.Subreports["Party_4.rpt"].SetDataSource(DS.Tables[0]);
                            RepDoc.Subreports["Party_5.rpt"].SetDataSource(DS.Tables[0]);
                            RepDoc.Subreports["Party_6.rpt"].SetDataSource(DS.Tables[0]);
                            RepDoc.Subreports["Party_7.rpt"].SetDataSource(DS.Tables[0]);
                            RepDoc.Subreports["Party_8.rpt"].SetDataSource(DS.Tables[0]);
                            RepDoc.Subreports["Party_9.rpt"].SetDataSource(DS.Tables[0]);
                            RepDoc.Subreports["Party_10.rpt"].SetDataSource(DS.Tables[0]);
                            RepDoc.Subreports["Party_11.rpt"].SetDataSource(DS.Tables[0]);
                            RepDoc.Subreports["Party_12.rpt"].SetDataSource(DS.Tables[0]);
                            RepDoc.Subreports["Party_13.rpt"].SetDataSource(DS.Tables[0]);
                            RepDoc.Subreports["Party_14.rpt"].SetDataSource(DS.Tables[0]);
                            RepDoc.Subreports["Party_15.rpt"].SetDataSource(DS.Tables[0]);
                        }
                        else
                        {
                            RepDoc.Subreports[0].SetDataSource(DS.Tables[0]);
                            RepDoc.Subreports[1].SetDataSource(DS.Tables[0]);
                        }
                    }
                }

                RepDoc.SetDatabaseLogon(Global.gStrStrUserName, Global.gStrStrPasssword);
                CryViewer.ReportSource = RepDoc;

                CryViewer.ShowParameterPanelButton = false;
                CryViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;

                CryViewer.Refresh();
                CryViewer.Zoom(pIntZoom);

                this.Cursor = Cursors.Default;

                //if (pStrRPTName == "Janged_Issue_Main" || pStrRPTName == "Janged_Issue_Lalubhai_Main")
                //{
                //    if (GlobalDec.gEmployeeProperty.role_name == "SURAT MAKABLE")
                //    {
                //        SetParaMeter();
                //        this.Cursor = Cursors.Default;
                //        PrinterSettings getprinterName = new PrinterSettings();
                //        RepDoc.PrintOptions.PrinterName = getprinterName.PrinterName;
                //        RepDoc.PrintToPrinter(1, false, printerSettings.FromPage, printerSettings.ToPage);
                //    }
                //    else
                //    {
                //        this.Show();
                //    }
                //}
                //else
                //{
                this.Show();
                //}
            }
            catch (Exception Ex)
            {
                this.Cursor = Cursors.Default;
                Global.Confirm(Ex.ToString());
            }
        }

        public void ShowForm_New(string pStrRPTName, int pIntZoom = 120, ReportFolder pEnumReportFolder = ReportFolder.NONE, string[,] Formulas = null)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                string StrPath = Application.StartupPath + "\\RPT\\";

                StrPath = StrPath + pEnumReportFolder.ToString() + "\\";

                StrPath = StrPath + pStrRPTName + ".rpt";

                RepDoc.Load(StrPath);

                if (DS != null)
                {
                    RepDoc.SetDataSource(DS.Tables[0]);
                }
                TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                ConnectionInfo crConnectionInfo = new ConnectionInfo();

                if (Formulas != null)
                {
                    for (int x = 0; x < Formulas.GetLength(0); x++)
                    {
                        RepDoc.DataDefinition.FormulaFields[Formulas[x, 0].ToString()].Text = " '" + Formulas[x, 1].ToString() + "'";
                    }
                }
                for (int IntI = 0; IntI < RepDoc.Subreports.Count; IntI++)
                {
                    RepDoc.Subreports[IntI].SetDataSource(DS.Tables[IntI]);
                }

                CryViewer.ReportSource = RepDoc;
                CryViewer.ShowParameterPanelButton = false;
                CryViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
                CryViewer.Refresh();
                CryViewer.Zoom(pIntZoom);
                SetParaMeter();
                this.Cursor = Cursors.Default;
                this.Show();
            }
            catch (Exception Ex)
            {
                this.Cursor = Cursors.Default;
                Global.Confirm(Ex.ToString());
            }
        }
        private void SetFormula()
        {
            if (AllowSetFormula == false)
            {
                return;
            }
            FormulaFieldDefinitions FormulaFieldDefs;
            FormulaFieldDefs = RepDoc.DataDefinition.FormulaFields;

            int IntFormula = 0;

            string[] splitStr = GroupBy.Split(',');

            foreach (string Str in splitStr)
            {
                if (Str != "")
                {
                    IntFormula++;
                    FormulaFieldDefs["Grp" + IntFormula.ToString()].Text = "{MIX_MACHINE_WORKDOWN." + Str + "}";
                    FormulaFieldDefs["Str" + IntFormula.ToString()].Text = "{MIX_MACHINE_WORKDOWN." + Str + "}";
                }
            }

            IntFormula = 0;
            for (int IntI = 0; IntI < splitStr.Length; IntI++)
            {
                IntFormula++;
                FormulaFieldDefs["showGroup" + IntFormula.ToString()].Text = "0";
            }

            for (int IntI = splitStr.Length; IntI > 1; IntI--)
            {
                FormulaFieldDefs["showGroup" + IntI.ToString()].Text = "1";
            }

            IntFormula = 0;
            foreach (string Str in splitStr)
            {
                if (Str != "")
                {
                    IntFormula++;
                }
            }
        }

        private void SetParaMeter()
        {
            RepDoc.DataDefinition.ParameterFields.Reset();
            ParameterFieldDefinitions ParamFieldDefs;
            ParameterValues ParamValues = new ParameterValues();
            ParameterDiscreteValue ParamDisValue;
            ParamFieldDefs = RepDoc.DataDefinition.ParameterFields;
            ParamDisValue = new ParameterDiscreteValue();

            foreach (ParameterFieldDefinition ParamFieldDef in ParamFieldDefs)
            {
                switch (ParamFieldDef.ParameterFieldName.ToUpper())
                {
                    case "REPHEAD":
                        if (Val.Val(RepHead.Length) != 0)
                        {
                            ParamDisValue.Value = RepHead;
                        }
                        else
                        {
                            ParamDisValue.Value = "  ";
                        }
                        break;
                    case "REPNAME":
                        if (Val.Val(RepName.Length) != 0)
                        {
                            ParamDisValue.Value = RepName;
                        }
                        else
                        {
                            ParamDisValue.Value = "  ";
                        }
                        break;
                    case "REPPARA":
                        if (Val.Val(RepPara.Length) != 0)
                        {
                            ParamDisValue.Value = RepPara;
                        }
                        else
                        {
                            ParamDisValue.Value = "  ";
                        }
                        break;
                    case "FROMDATE":
                        if (Val.Val(FromDate.Length) != 0)
                        {
                            ParamDisValue.Value = FromDate;
                        }
                        else
                        {
                            ParamDisValue.Value = "  ";
                        }
                        break;
                    case "TODATE":
                        if (Val.Val(ToDate.Length) != 0)
                        {
                            ParamDisValue.Value = ToDate;
                        }
                        else
                        {
                            ParamDisValue.Value = "  ";
                        }
                        break;
                    case "PROCESS":
                        if (Val.Val(ToDate.Length) != 0)
                        {
                            ParamDisValue.Value = PROCESS;
                        }
                        else
                        {
                            ParamDisValue.Value = "  ";
                        }
                        break;
                    case "DOLLARRATE":
                        if (Val.Val(DollarRate) != 0)
                        {
                            ParamDisValue.Value = DollarRate;
                        }
                        else
                        {
                            ParamDisValue.Value = "  ";
                        }
                        break;

                    case "COMPANYNAME":
                        if (Val.Val(CompanyName.Length) != 0)
                        {
                            ParamDisValue.Value = CompanyName;
                        }
                        else
                        {
                            ParamDisValue.Value = "  ";
                        }
                        break;
                    case "ADDRESS":
                        if (Val.Val(Address.Length) != 0)
                        {
                            ParamDisValue.Value = Address;
                        }
                        else
                        {
                            ParamDisValue.Value = "  ";
                        }
                        break;
                    case "PHONENO":
                        if (Val.Val(PhoneNo.Length) != 0)
                        {
                            ParamDisValue.Value = PhoneNo;
                        }
                        else
                        {
                            ParamDisValue.Value = "  ";
                        }
                        break;
                    case "REPTYPE":
                        if (Val.Val(RepType.Length) != 0)
                        {
                            ParamDisValue.Value = RepType;
                        }
                        else
                        {
                            ParamDisValue.Value = "  ";
                        }
                        break;

                    case "ADDRESSTYPE":
                        if (Val.Val(AddressType.Length) != 0)
                        {
                            ParamDisValue.Value = AddressType;
                        }
                        else
                        {
                            ParamDisValue.Value = "  ";
                        }
                        break;
                    case "ISPRINTDATE":
                        ParamDisValue.Value = IsPrintDate;
                        break;
                }
                if (Val.Left(ParamFieldDef.ParameterFieldName.ToUpper(), 1) != "@")
                {
                    ParamValues.Add(ParamDisValue);
                    ParamFieldDef.ApplyCurrentValues(ParamValues);
                }
            }
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            CryViewer.PrintReport();
        }
        private void FrmReportViewer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Escape))
            {
                this.Close();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.PrinterSettings = printerSettings;
            printDialog.AllowPrintToFile = false;
            printDialog.AllowSomePages = true;
            printDialog.UseEXDialog = true;
            printDialog.AllowSelection = true;
            printDialog.AllowCurrentPage = true;
            DialogResult result = printDialog.ShowDialog();

            if (result == DialogResult.Cancel)
            {
                return;
            }
            RepDoc.PrintOptions.PrinterName = printerSettings.PrinterName;
            RepDoc.PrintToPrinter(printerSettings.Copies, false, printerSettings.FromPage, printerSettings.ToPage);
        }

        private void FrmReportViewer_Load(object sender, EventArgs e)
        {
            if (mStrRefreshFormType != "")
            {
                BtnRefresh.Visible = true;
            }
            else
            {
                BtnRefresh.Visible = false;
            }
            CryViewer.Refresh();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            CryViewer.ExportReport();


            //FrmReportViewer FrmReportViewer = new FrmReportViewer();
            //string pStrRPTName = "CrtCashTransfer_Cr_Dr_Main";
            // string pEnumReportFolder =  DERP.Report.FrmReportViewer.ReportFolder.ACCOUNT;

            //ReportDocument rdReport = new ReportDocument();

            //string StrPath = Application.StartupPath + "\\RPT\\";

            //StrPath = StrPath + "FINAL_OK_SUB" + "\\";

            //StrPath = StrPath + "CrtPolishGrading_Final_Main_Mumbai_A" + ".rpt";




            //rdReport.Load(StrPath);

            //rdReport.Database.Tables["abc"].SetDataSource(DS.Tables[1]);



            //
            //DiskFileDestinationOptions DiskFileDestinationOption = new DiskFileDestinationOptions();
            //SaveFileDialog Sfg = new SaveFileDialog();
            //Sfg.Filter = "Excel|.xlsx";
            //if (Sfg.ShowDialog() == DialogResult.OK)
            //{
            //    DiskFileDestinationOption.DiskFileName = Sfg.FileName;
            //}
            //ExportOption.ExportDestinationType = ExportDestinationType.DiskFile;
            //ExportOption.ExportFormatType = ExportFormatType.Excel;
            //ExportOption.ExportDestinationOptions = DiskFileDestinationOption;
            //ExportOption.ExportFormatOptions = new ExcelFormatOptions();

            //rdReport.Export();
            //CryViewer.ReportSource = RepDoc;
            //Global.Export("xlsx", RepDoc);

            //ExportOptions ExportOption = new ExportOptions();
            //DiskFileDestinationOptions DiskFileDestinationOption = new DiskFileDestinationOptions();
            //SaveFileDialog Sfg = new SaveFileDialog();
            //Sfg.Filter = "Excel|.xlsx";
            //if (Sfg.ShowDialog() == DialogResult.OK)
            //{
            //    DiskFileDestinationOption.DiskFileName = Sfg.FileName;
            //}
            ////ExportOption = RepDoc.exportop
            ////ExportOption = CryViewer.exportop

            //ExportOption.ExportDestinationType = ExportDestinationType.DiskFile;
            //ExportOption.ExportFormatType = ExportFormatType.Excel;
            //ExportOption.ExportDestinationOptions = DiskFileDestinationOption;
            //ExportOption.ExportFormatOptions = new ExcelFormatOptions();
            //RepDoc.Export();
        }
    }
}
