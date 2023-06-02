using BLL.FunctionClasses.Transaction.MFG;
using DERP.Class;
using DERP.Report;
using System;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;


namespace DERP.Transaction.MFG
{
    public partial class frmEmailUtility : DevExpress.XtraEditors.XtraForm
    {
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;

        Timer tmr = new Timer();
        public frmEmailUtility()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Save();
        }
        public void ShowForm()
        {
            ObjPer.FormName = this.Name.ToUpper();
            if (ObjPer.CheckPermission() == false)
            {
                Global.Message(BLL.GlobalDec.gStrPermissionViwMsg);
                return;
            }
            AttachFormEvents();
            //this.Show();
        }
        private void AttachFormEvents()
        {
            objBOFormEvents.CurForm = this;
            objBOFormEvents.FormKeyPress = true;
            objBOFormEvents.FormKeyDown = true;
            objBOFormEvents.FormResize = true;
            objBOFormEvents.FormClosing = true;
            objBOFormEvents.ObjToDispose.Add("");
            // objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }

        public void Save()
        {
            try
            {
                MailMessage mail = new MailMessage();
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                //mail.From = new MailAddress("mayurbodar525@gmail.com");
                //mail.To.Add("mayur_bodar@hotmail.com");
                mail.From = new MailAddress("vavadiya.praful23@gmail.com");
                mail.To.Add("vavadiya.heena23@gmail.com");
                mail.Subject = "Test Mail - 1";


                //string pStrBody = uchtmlEditor1.getHtml();

                mail.Body = "mail with attachment";

                MFGJangedIssue objMFGJangedIssue = new MFGJangedIssue();
                DataTable DTab_EmailData = objMFGJangedIssue.Polish_GetDataDetails();

                FrmReportViewer FrmReportViewer = new FrmReportViewer();
                FrmReportViewer.DS.Tables.Add(DTab_EmailData);
                FrmReportViewer.GroupBy = "";
                FrmReportViewer.RepName = "";
                FrmReportViewer.RepPara = "";
                this.Cursor = Cursors.Default;
                FrmReportViewer.AllowSetFormula = true;

                FrmReportViewer.Email_ShowForm("DailyStockReport", 120, FrmReportViewer.ReportFolder.ACCOUNT);



                DTab_EmailData = null;
                FrmReportViewer.DS.Tables.Clear();
                FrmReportViewer.DS.Clear();
                FrmReportViewer = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                this.Cursor = Cursors.Default;
            }
        }

        private void frmBackup_Load(object sender, EventArgs e)
        {
            Save();
            //btnBackup_Click(null, null);
            //tmr.Tick += new System.EventHandler(tmr_Tick);
            //tmr.Interval = 900000;
            //tmr.Enabled = true;            
        }

        private void tmr_Tick(object sender, EventArgs e)
        {
            //btnBackup_Click(null, null);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
