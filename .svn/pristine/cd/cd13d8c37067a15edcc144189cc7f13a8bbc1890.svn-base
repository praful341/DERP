
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
    public partial class FrmSieveMaster : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.FormEvents objBOFormEvents;
        BLL.Validation Val;
        BLL.FormPer ObjPer;

        SieveMaster objSieve;

        #endregion

        #region Constructor
        public FrmSieveMaster()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            Val = new BLL.Validation();
            ObjPer = new BLL.FormPer();

            objSieve = new SieveMaster();
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
            objBOFormEvents.ObjToDispose.Add(objSieve);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }

        #endregion

        #region Events
        private void FrmSieveMaster_Load(object sender, EventArgs e)
        {
            try
            {
                GetData();
                btnClear_Click(btnClear, null);
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
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
                txtSieveName.Text = "";
                txtRemark.Text = "";
                txtFromPCS.Text = "";
                txtTOPCS.Text = "";
                txtFromCarat.Text = "";
                txtTOCarat.Text = "";
                txtSequenceNo.Text = "";
                chkActive.Checked = true;
                txtSieveName.Focus();
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region GridEvents
        private void dgvSieveMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvSieveMaster.GetDataRow(e.RowHandle);

                        lblMode.Text = "Edit Mode";
                        lblMode.Tag = Val.ToInt32(Drow["sieve_id"]);
                        txtSieveName.Text = Convert.ToString(Drow["sieve_name"]);
                        chkActive.Checked = Val.ToBoolean(Drow["active"]);
                        txtRemark.Text = Convert.ToString(Drow["remarks"]);
                        txtFromPCS.Text = Convert.ToString(Drow["from_pcs"]);
                        txtTOPCS.Text = Convert.ToString(Drow["to_pcs"]);
                        txtFromCarat.Text = Convert.ToString(Drow["from_carat"]);
                        txtTOCarat.Text = Convert.ToString(Drow["to_Carat"]);
                        txtSequenceNo.Text = Val.ToString(Drow["sequence_no"]);
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

        #region Functions
        private bool SaveDetails()
        {
            bool blnReturn = true;
            SieveMaster objSieve = new SieveMaster();
            Sieve_MasterProperty SieveMasterProperty = new Sieve_MasterProperty();

            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }

                SieveMasterProperty.Sieve_Id = Val.ToInt64(lblMode.Tag);
                SieveMasterProperty.Sieve_Name = txtSieveName.Text.ToUpper();
                SieveMasterProperty.Active = Val.ToBooleanToInt(chkActive.Checked);
                SieveMasterProperty.Remark = txtRemark.Text.ToUpper();
                SieveMasterProperty.From_Pcs = Val.ToDouble(txtFromPCS.Text);
                SieveMasterProperty.To_Pcs = Val.ToDouble(txtTOPCS.Text);
                SieveMasterProperty.From_Carat = Val.ToDouble(txtFromCarat.Text);
                SieveMasterProperty.To_Carat = Val.ToDouble(txtTOCarat.Text);
                SieveMasterProperty.Sequence_No = Val.ToInt(txtSequenceNo.Text);

                int IntRes = objSieve.Save(SieveMasterProperty);
                if (IntRes == -1)
                {
                    Global.Confirm("Error In Save Sieve Details");
                    txtSieveName.Focus();
                }
                else
                {
                    if (Val.ToInt(lblMode.Tag) == 0)
                    {
                        Global.Confirm("Sieve Details Data Save Successfully");
                    }
                    else
                    {
                        Global.Confirm("Sieve Details Data Update Successfully");
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
                SieveMasterProperty = null;
            }

            return blnReturn;
        }
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (txtSieveName.Text == string.Empty)
                {
                    lstError.Add(new ListError(12, "Sieve Name"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtSieveName.Focus();
                    }
                }

                if (!objSieve.ISExists(txtSieveName.Text, Val.ToInt64(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
                {
                    lstError.Add(new ListError(23, "Sieve Name"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtSieveName.Focus();
                    }

                }
                if (txtSequenceNo.Text == string.Empty || txtSequenceNo.Text == "0")
                {
                    lstError.Add(new ListError(12, "Sequence No"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        txtSequenceNo.Focus();
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
                DataTable DTab = objSieve.GetData();
                grdSieveMaster.DataSource = DTab;
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
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
                            dgvSieveMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvSieveMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvSieveMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvSieveMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvSieveMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvSieveMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvSieveMaster.ExportToCsv(Filepath);
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
