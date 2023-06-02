using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Utility;
using DERP.Class;
using DERP.MDI;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DERP
{
    public partial class FrmLogin_Kamala : DevExpress.XtraEditors.XtraForm
    {
        Validation Val = new Validation();
        public FrmLogin_Kamala()
        {
            InitializeComponent();
        }

        private void FrmLogin_Shown(object sender, EventArgs e)
        {

            //string Name1 = GlobalDec.Decrypt("CSvbX9QRe9KN9TCJSaR0oQ==", true);

            //string Name2 = GlobalDec.Decrypt("DTzFnEDVsdc=", true);
            //string Name4 = GlobalDec.Encrypt("122.170.104.175", true);
            //string Name4 = GlobalDec.Encrypt("192.168.0.80", true);
            //string Name2 = GlobalDec.Encrypt("ST-1", true);
            //string Name3 = GlobalDec.Encrypt("SERVER-A", true);
            //string Name4 = GlobalDec.Decrypt("vJSGhy22EvA=", true);
            //string Name5 = GlobalDec.Decrypt("wm5llzHlrj1BBUYWftZsuw==", true);

            //string Name4 = GlobalDec.Encrypt("192.168.49.1", true);

            Global.gStrStrHostName = BLL.GlobalDec.Decrypt(System.Configuration.ConfigurationManager.AppSettings["ServerHostName"].ToString(), true);
            Global.gStrStrServiceName = BLL.GlobalDec.Decrypt(System.Configuration.ConfigurationManager.AppSettings["ServerServiceName"].ToString(), true);
            Global.gStrStrUserName = BLL.GlobalDec.Decrypt(System.Configuration.ConfigurationManager.AppSettings["ServerUserName"].ToString(), true);
            Global.gStrStrPasssword = BLL.GlobalDec.Decrypt(System.Configuration.ConfigurationManager.AppSettings["ServerPassWord"].ToString(), true);

            BLL.DBConnections.ConnectionString = "Data Source=" + Global.gStrStrHostName + ";Initial Catalog=" + Global.gStrStrServiceName + ";User ID=" + Global.gStrStrUserName + ";Password=" + Global.gStrStrPasssword + ";";
            BLL.DBConnections.ProviderName = "System.Data.SqlClient";

            //BLL.PropertyClasses.Utility.Settings_Property Settings = new BLL.FunctionClasses.Utility.Settings().GetDataByPK();

            //Global.gStrDeveloperStrHostName = Settings.guest_hostname;
            //Global.gStrDeveloperStrServiceName = Settings.guest_servicename;
            //Global.gStrDeveloperStrUserName = Settings.guest_username;
            //Global.gStrDeveloperStrPasssword = Settings.guest_password;

            //BLL.DBConnections.ConnectionDeveloper = "Data Source=" + Global.gStrDeveloperStrHostName + ";Initial Catalog=" + Global.gStrDeveloperStrServiceName + ";User ID=" + Global.gStrDeveloperStrUserName + ";Password=" + Global.gStrDeveloperStrPasssword + ";";
            //BLL.DBConnections.ProviderDeveloper = "System.Data.SqlClient";

            if (GlobalDec.gStrComputerIP == "192.168.1.4" || GlobalDec.gStrComputerIP == "192.168.1.116" || GlobalDec.gStrComputerIP == "192.168.1.112" || GlobalDec.gStrComputerIP == "192.168.1.13" || GlobalDec.gStrComputerIP == "192.168.29.176" || GlobalDec.gStrComputerIP == "194.168.1.34" || GlobalDec.gStrComputerIP == "194.168.1.37" || GlobalDec.gStrComputerIP == "192.168.29.175")
            {
                //txtUserName.Text = "DEEPAK";
                //txtPassword.Text = "d@2007";
                //txtUserName.Text = "JAYESH";
                //txtPassword.Text = "123";
                //txtUserName.Text = "TUSHAR";
                //txtPassword.Text = "7888";
                //txtUserName.Text = "DAKSHAY";
                //txtPassword.Text = "7444";
                //txtUserName.Text = "PRAGNESH";
                //txtPassword.Text = "6611";
                //txtUserName.Text = "SUBHASH";
                //txtPassword.Text = "123";
                //txtUserName.Text = "RAHUL";
                //txtPassword.Text = "123";
                //txtUserName.Text = "RIYA";
                //txtPassword.Text = "123";
                txtUserName.Text = "PRAFUL";
                txtPassword.Text = "321";
                //txtUserName.Text = "HARDIK";
                //txtPassword.Text = "8055";
                //txtUserName.Text = "RAHUL";
                //txtPassword.Text = "123";
                //txtUserName.Text = "MAYUR";
                //txtPassword.Text = "123";
                //txtUserName.Text = "PIYUSH";
                //txtPassword.Text = "PIYUSH@123";
                //txtUserName.Text = "RIKITA";
                //txtPassword.Text = "6611";
                //txtUserName.Text = "PRINCEP";
                //txtPassword.Text = "123";
                //txtUserName.Text = "AYUSHI";
                //txtPassword.Text = "123";
                //txtUserName.Text = "SAHIL";
                //txtPassword.Text = "1234";
                //txtUserName.Text = "KETAN";
                //txtPassword.Text = "258";
                //txtUserName.Text = "SAHIL";
                //txtPassword.Text = "1234";
                //txtUserName.Text = "KANAIYA";
                //txtPassword.Text = "123";
                //btnLogin_Click(null, null);
            }
            else if (GlobalDec.gStrComputerIP == "192.168.1.116")
            {
                txtUserName.Text = "MAYUR";
                txtPassword.Text = "123";
            }
            try
            {
                String[] Str1 = Application.StartupPath.Split('\\');

                String FilePath = Str1[0].ToString();
                for (int i = 1; i < Str1.Length - 1; i++)
                {
                    FilePath = FilePath + "\\" + Str1[i].ToString();
                }

                string path = Application.StartupPath;
                DirectoryInfo di = new DirectoryInfo(path);
                FileInfo fi = di.Parent.GetFiles("DiamondVersion.txt").FirstOrDefault();
                string contents = File.ReadAllText(fi.FullName);
                string version = string.Join(".", contents.Split('\\').FirstOrDefault().Replace("D", "").ToCharArray());
                if (!string.IsNullOrEmpty(version))
                {
                    lblVersion.Text = version;
                }
                else
                {
                    string path_error = Application.StartupPath;
                    DirectoryInfo di_error = new DirectoryInfo(path);
                    FileInfo fi_error = di.Parent.GetFiles("DiamondVersion.txt").FirstOrDefault();
                    string contents_error = File.ReadAllText(fi.FullName);
                    string version_error = string.Join(".", contents.Split('\\').FirstOrDefault().Replace("D", "").ToCharArray());
                    lblVersion.Text = version_error;
                }
            }
            catch (Exception Ex)
            {
                if (System.IO.File.Exists(Application.StartupPath + "\\DiamondVersion.txt") == true)
                {
                    string[] Str = System.IO.File.ReadAllLines(Application.StartupPath + "\\DiamondVersion.txt");
                    if (Str.Length == 0)
                    {
                        return;
                    }
                    if (Str[0].Length == 0)
                    {
                        return;
                    }

                    this.lblVersion.Text = Val.ToString(Str[0]);
                }
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text.Length == 0)
            {
                Global.Confirm("Please Enter UserName");
                txtUserName.Focus();
                return;
            }
            if (txtPassword.Text.Length == 0)
            {
                Global.Confirm("Please Enter Password");
                txtPassword.Focus();
                return;
            }

            Global.gStrVersion = lblVersion.Text;

            this.Cursor = Cursors.WaitCursor;

            Login objLogin = new Login();
            int IntRes = objLogin.CheckLogin(txtUserName.Text, GlobalDec.Encrypt(txtPassword.Text, true));

            this.Cursor = Cursors.Default;
            if (IntRes == -1)
            {
                Global.Confirm("Enter Valid UserName And Password");
                txtUserName.Focus();
                return;
            }
            else
            {
                FinancialYearMaster ObjFinancial = new FinancialYearMaster();
                DataTable tdt = ObjFinancial.GetData();
                GlobalDec.gEmployeeProperty.gFinancialYear = Val.ToString(tdt.Rows[0]["financial_year"]);
                GlobalDec.gEmployeeProperty.gFinancialYear_Code = Val.ToInt64(tdt.Rows[0]["fin_year_id"]);

                MDIMain MainForm = new MDIMain();
                BLL.FormPer ObjPer = new BLL.FormPer();
                Global.gMainFormRef = MainForm;
                this.Hide();
                MainForm.ShowDialog();
                //FrmHomePage MainForm = new FrmHomePage();
                //BLL.FormPer ObjPer = new BLL.FormPer();
                //Global.gMainFormRef = MainForm;
                //this.Hide();
                //MainForm.ShowDialog();
                this.Close();
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }
        private void txtUserName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
            }
        }
    }
}
