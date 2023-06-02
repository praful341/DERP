using BLL;
using DERP.Class;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;


namespace DERP.Transaction.MFG
{
    public partial class frmBackup : DevExpress.XtraEditors.XtraForm
    {
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        string Host_Name = BLL.GlobalDec.Decrypt(System.Configuration.ConfigurationManager.AppSettings["ServerHostName"].ToString(), true);
        string Service_Name = BLL.GlobalDec.Decrypt(System.Configuration.ConfigurationManager.AppSettings["ServerServiceName"].ToString(), true);
        string User_Name = BLL.GlobalDec.Decrypt(System.Configuration.ConfigurationManager.AppSettings["ServerUserName"].ToString(), true);
        string PassWord = BLL.GlobalDec.Decrypt(System.Configuration.ConfigurationManager.AppSettings["ServerPassWord"].ToString(), true);

        SqlConnection con = new SqlConnection("Data Source = " + Global.gStrStrHostName + "; Initial Catalog = " + Global.gStrStrServiceName + "; User ID = " + Global.gStrStrUserName + "; Password=" + Global.gStrStrPasssword + ";");
        Timer tmr = new Timer();
        public frmBackup()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
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
            this.Show();
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

        string[] filename;
        public void Save()
        {
            try
            {
                if (txtBakPath.Text == "")
                {
                    MessageBox.Show("Please Select Backup Path..");
                    BtnDailyBackUP.Focus();
                    return;
                }
                //MessageBox.Show("3");
                con.Open();
                //MessageBox.Show("4");
                //this.Cursor = Cursors.WaitCursor;
                string query = "", path = "", time = "";
                time = DateTime.Now.ToString("HH:mm:ss");
                time = time.Replace(":", "");
                path = txtBakPath.Text + "DERP_DailyBackup" + "\\" + DateTime.Now.ToString("dd - MM - yyyy") + "\\" + DateTime.Now.ToString("dd - MM - yyyy") + " - " + time;
                //path = "E:\\DERP_DailyBackup" + "\\" + DateTime.Now.ToString("dd - MM - yyyy") + "\\" + DateTime.Now.ToString("dd - MM - yyyy") + " - " + time;
                // MessageBox.Show("5");
                Directory.CreateDirectory(path);
                // MessageBox.Show("6");

                try
                {

                    query = string.Concat(new string[]
                                {
                                "BACKUP DATABASE [",
                                "DERP",
                                "] To DISK=N'",
                                path,
                                "\\",
                                "DERP",
                                ".bak' WITH INIT, NOUNLOAD ,  NAME = N'",
                                "DERP",
                                " Backup',  NOSKIP ,  STATS = 10,  NOFORMAT "
                                });
                    //MessageBox.Show(query);
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.CommandTimeout = 20000;
                    cmd.ExecuteNonQuery();
                    //df.ExecuteQuery(query);
                    //MessageBox.Show("Backup Successfully..");
                    con.Close();
                    //MessageBox.Show("7");
                    filename = Directory.GetFiles(path);
                    using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
                    {
                        zip.AddFiles(filename, "file");
                        zip.UseZip64WhenSaving = Ionic.Zip.Zip64Option.Always;
                        zip.Save(path + "\\DERP.zip");
                    }

                    //foreach (string f in filename)
                    //{
                    //    File.Delete(f);
                    //    if (f != "")
                    //    {
                    //        MessageBox.Show("BackUp Upload Successfully");
                    //        PanelLoading.Visible = false;
                    //        txtBakPath.Text = "";
                    //        this.Cursor = Cursors.Default;
                    //    }
                    //}
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());
                    PanelLoading.Visible = false;
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                this.Cursor = Cursors.Default;
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtBakPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        //private void btnBackup_Click(object sender, EventArgs e)
        //{
        //    Save();
        //}

        private void frmBackup_Load(object sender, EventArgs e)
        {
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

        private void BtnDailyBackUP_Click(object sender, EventArgs e)
        {
            PanelLoading.Visible = true;

            if (this.backgroundWorker1.IsBusy)
            {
            }
            else
            {
                backgroundWorker1.RunWorkerAsync();
            }

        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT MAKABLE" || GlobalDec.gEmployeeProperty.role_name.ToUpper() == "SURAT ADMIN")
            {
                if (GlobalDec.gEmployeeProperty.user_name.ToUpper() == "PRAFUL" || GlobalDec.gEmployeeProperty.user_name.ToUpper() == "PRAGNESH")
                {
                    Save();
                }
                else
                {
                    Global.Message("Don't Have Permission BackUPData.. So Please Contact to Administrator");
                    return;
                }
            }
            else
            {
                Global.Message("Don't Have Permission BackUPData.. So Please Contact to Administrator");
                return;
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            try
            {
                PanelLoading.Visible = false;
                foreach (string f in filename)
                {
                    File.Delete(f);
                    if (f != "")
                    {
                        MessageBox.Show("BackUp Upload Successfully");
                        PanelLoading.Visible = false;
                        txtBakPath.Text = "";
                        this.Cursor = Cursors.Default;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error In Backup ");
                PanelLoading.Visible = false;
                this.Cursor = Cursors.Default;
            }
        }
    }
}
