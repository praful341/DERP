using BLL;
using BLL.FunctionClasses.Master;
using BLL.PropertyClasses.Master;
using DERP.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Master
{ 
    public partial class FrmBankMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        FormEvents objBOFormEvents;
        Validation Val;
        BLL.FormPer ObjPer;



        BankMaster objBank;

        #endregion

        #region Constructor
        public FrmBankMaster()
        {
            InitializeComponent();

            objBOFormEvents = new FormEvents();
            Val = new Validation();
            ObjPer = new BLL.FormPer();

            objBank = new BankMaster();
        }

        public void ShowForm()
        {
            ObjPer.FormName = this.Name.ToUpper();
            if (ObjPer.CheckPermission() == false)
            {
                Global.Message(BLL.GlobalDec.gStrPermissionViwMsg);
                return;
            }
            Val.frmGenSet(this);
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
            objBOFormEvents.ObjToDispose.Add(objBank);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion 

        #region Events     
        private void FrmBankMaster_Load(object sender, EventArgs e)
        {
            try
            {
                GetData();
                btnClear_Click(btnClear, null);
                chkActive.Checked = true;
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            //DateTime date_month = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"));
            //string text_days = date_month.ToString("dd");
            //string text_month = date_month.ToString("MM");
            //int count = 1;
            //string Kapan_No = "ABC";
            //Int64 JangedNo_IntRes = 23432;

            //String Todaysdate = DateTime.Now.ToString("dd-MM-yyyy");
            //if (!Directory.Exists("\\Galaxy_XML\\" + Todaysdate))
            //{
            //    Directory.CreateDirectory("\\Galaxy_XML\\" + Todaysdate);
            //}

            //var newValue = "\\Galaxy_XML\\" + Todaysdate + "\\";

            ////Global.Message(@"" + newValue + Kapan_No + "(" + text_days + "-" + text_month + ")(" + count + "-" + JangedNo_IntRes + ")" + ".xml");
            //// XmlTextWriter xmlWriter = new XmlTextWriter(@"C:\MyDataset.xml", Encoding.UTF8);
            ////XmlTextWriter xmlWriter = new XmlTextWriter(@"D:\\DERP_Software\\Galaxy_XML\\ " + Todaysdate + "\\" + m_dtbIssueProcess.Rows[0]["kapan_no"].ToString() + "(" + text_days + "-" + text_month + ")(" + count + "-" + JangedNo_IntRes + ")" + ".xml", Encoding.UTF8);
            //XmlTextWriter xmlWriter = new XmlTextWriter(newValue + Kapan_No + "(" + text_days + "-" + text_month + ")(" + count + "-" + JangedNo_IntRes + ")" + ".xml", Encoding.UTF8);
            ////XmlTextWriter xmlWriter = new XmlTextWriter(@"\\server\DERP_Software\Galaxy_XML\" + Todaysdate + "\\" + m_dtbIssueProcess.Rows[0]["kapan_no"].ToString() + "(" + text_days + "-" + text_month + ")(" + count + "-" + JangedNo_IntRes + ")" + ".xml", Encoding.UTF8);
            //// XmlTextWriter xmlWriter = new XmlTextWriter(@"\\server\DERP_Software\Galaxy_XML\" + Kapan_No + "(" + text_days + "-" + text_month + ")(" + count + "-" + JangedNo_IntRes + ")" + ".xml", Encoding.UTF8);

            //xmlWriter.WriteStartDocument(true);
            //xmlWriter.WriteStartElement("Galaxy"); //Root Element
            //xmlWriter.WriteStartElement("StoneCollection"); //Department Element

            //xmlWriter.WriteStartAttribute("version"); //Attribute "Name"
            //xmlWriter.WriteString("1.0.0"); //Attribute Value 
            //xmlWriter.WriteEndAttribute();

            //xmlWriter.WriteStartElement("stones"); //Started Employees Element

            //xmlWriter.WriteStartElement("stone"); //Started Employee Element

            //DateTime dt = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy"));
            //string text = dt.ToString("dd");

            //xmlWriter.WriteStartAttribute("weight");//Attribute "Age"
            //xmlWriter.WriteString(Val.ToString(1.23));//Attribute Value
            //xmlWriter.WriteEndAttribute();

            //xmlWriter.WriteStartAttribute("process_type");//Attribute "Age"
            //xmlWriter.WriteString(Val.ToString("Galaxy 1000"));//Attribute Value
            //xmlWriter.WriteEndAttribute();

            //xmlWriter.WriteStartAttribute("stone_name");//Attribute "Age"
            //xmlWriter.WriteString(Val.ToString(4525455));//Attribute Value
            //xmlWriter.WriteEndAttribute();

            //xmlWriter.WriteStartAttribute("package_name"); //Attribute "Name"
            //xmlWriter.WriteString(Val.ToString(Kapan_No + "-" + JangedNo_IntRes + "(" + text + ")" + count)); //Attribute Value 
            //xmlWriter.WriteEndAttribute();

            //xmlWriter.WriteEndElement(); //End of Employee Element                    


            //xmlWriter.WriteEndElement(); //End of Employees Element
            //xmlWriter.WriteEndElement(); //End of Department Element
            //xmlWriter.WriteEndElement(); //End of Root Element

            //xmlWriter.WriteEndDocument();
            //xmlWriter.Flush();
            //xmlWriter.Close();

            ObjPer.FormName = this.Name.ToUpper();
            ObjPer.SetFormPer();
            if (ObjPer.AllowUpdate == false || ObjPer.AllowInsert == false)
            {
                Global.Message(BLL.GlobalDec.gStrPermissionInsUpdMsg);
                return;
            }
            btnSave.Enabled = false;
            if (SaveDetails())
            {
                GetData();
                btnClear_Click(sender, e);
            }
            btnSave.Enabled = true;

        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                lblMode.Tag = 0;
                lblMode.Text = "Add Mode";
                txtBankName.Text = "";
                txtRemark.Text = "";
                txtSequenceNo.Text = "";
                chkActive.Checked = true;
                txtBankName.Focus();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }

        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region GridEvents
        private void dgvBankMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvBankMaster.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt32(Drow["bank_id"]);
                        txtBankName.Text = Val.ToString(Drow["bank_name"]);
                        txtRemark.Text = Val.ToString(Drow["remarks"]);
                        txtSequenceNo.Text = Val.ToString(Drow["sequence_no"]);
                        chkActive.Checked = Val.ToBoolean(Drow["active"]);
                        txtBankName.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        #endregion

        #endregion

        #region Functions
        private bool SaveDetails()
        {
            bool blnReturn = true;
            Bank_MasterProperty BankMasterProperty = new Bank_MasterProperty();

            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }
                List<ListError> lstError = new List<ListError>();


                BankMasterProperty.bank_id = Val.ToInt32(lblMode.Tag);
                BankMasterProperty.bank_name = txtBankName.Text.ToUpper();
                BankMasterProperty.active = Val.ToBoolean(chkActive.Checked);
                BankMasterProperty.remarks = txtRemark.Text.ToUpper();
                BankMasterProperty.sequence_no = Val.ToInt(txtSequenceNo.Text);

                int IntRes = objBank.Save(BankMasterProperty);
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save Bank Details");
                    txtBankName.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Bank Details Data Save Successfully");
                    }
                    else
                    {
                        Global.Confirm("Bank Details Data Update Successfully");
                    }
                }

            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                blnReturn = false;
            }
            finally
            {
                BankMasterProperty = null;
            }

            return blnReturn;
        }
        private bool ValidateDetails()
        {

            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (txtBankName.Text.Length == 0)
                {
                    lstError.Add(new ListError(12, "Bank Name"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtBankName.Focus();
                    }
                }
                if (txtSequenceNo.Text.Length == 0)
                {
                    lstError.Add(new ListError(12, "Sequence No"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtBankName.Focus();
                    }
                }
                if (!objBank.ISExists(txtBankName.Text, Val.ToInt64(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
                {
                    lstError.Add(new ListError(23, "Bank Name"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtBankName.Focus();
                    }
                }
            }

            catch (Exception ex)
            {
                lstError.Add(new ListError(ex));
            }
            return (!(BLL.General.ShowErrors(lstError)));
        }
        public void GetData()
        {
            try
            {
                DataTable DTab = objBank.GetData();
                grdBankMaster.DataSource = DTab;
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }

        }
        private void Export(string format, string dlgHeader, string dlgFilter)
        {
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = format;
                svDialog.Title = dlgHeader;
                svDialog.FileName = "Report";
                svDialog.Filter = dlgFilter;
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    string Filepath = svDialog.FileName;

                    switch (format)
                    {
                        case "pdf":
                            dgvBankMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvBankMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvBankMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvBankMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvBankMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvBankMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvBankMaster.ExportToCsv(Filepath);
                            break;
                    }

                    if (format.Equals(Exports.xlsx.ToString()))
                    {
                        if (Global.Confirm("Export Done\n\nYou Want To Open Excel File ?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(Filepath);
                        }
                    }
                    else if (format.Equals(Exports.pdf.ToString()))
                    {
                        if (Global.Confirm("Export Done\n\nYou Want To Open PDF File ?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(Filepath);
                        }
                    }
                    else
                    {
                        if (Global.Confirm("Export Done\n\nYou Want To Open File ?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(Filepath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString(), "Error in Export");
            }
        }
        #endregion

        #region Export Grid
        private void MNExportExcel_Click(object sender, EventArgs e)
        {
            //Global.Export("xlsx", dgvRoughClarityMaster);
            Export("xlsx", "Export to Excel", "Excel files 97-2003 (Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*");
        }
        private void MNExportPDF_Click(object sender, EventArgs e)
        {
            // Global.Export("pdf", dgvRoughClarityMaster);
            Export("pdf", "Export Report to PDF", "PDF (*.PDF)|*.PDF");
        }
        private void MNExportTEXT_Click(object sender, EventArgs e)
        {
            Export("txt", "Export to Text", "Text files (*.txt)|*.txt|All files (*.*)|*.*");
        }

        private void MNExportHTML_Click(object sender, EventArgs e)
        {
            Export("html", "Export to HTML", "Html files (*.html)|*.html|Htm files (*.htm)|*.htm");
        }

        private void MNExportRTF_Click(object sender, EventArgs e)
        {
            Export("rtf", "Export to RTF", "Word (*.doc) |*.doc;*.rtf|(*.txt) |*.txt|(*.*) |*.*");
        }

        private void MNExportCSV_Click(object sender, EventArgs e)
        {
            Export("csv", "Export Report to CSVB", "csv (*.csv)|*.csv");
        }
        #endregion
    }
}
