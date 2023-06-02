using BLL.FunctionClasses.Transaction;
using BLL.PropertyClasses.Transaction;
using DERP.Class;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Transaction
{
    public partial class FrmPriceOptimization : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member
        BLL.FormEvents objBOFormEvents;
        BLL.FormPer ObjPer;
        BLL.Validation Val;

        Control _NextEnteredControl;
        private List<Control> _tabControls;

        PriceImport ObjPrcImp;

        DataTable DtControlSettings;
        DataTable DTabFile;
        DataTable DTab_Data;

        int m_numForm_id;

        #endregion

        #region Constructor
        public FrmPriceOptimization()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            ObjPer = new BLL.FormPer();
            Val = new BLL.Validation();

            _NextEnteredControl = new Control();
            _tabControls = new List<Control>();

            ObjPrcImp = new PriceImport();

            DtControlSettings = new DataTable();
            DTabFile = new DataTable();
            DTab_Data = new DataTable();

            m_numForm_id = 0;
        }
        public void ShowForm()
        {
            ObjPer.FormName = this.Name.ToUpper();
            m_numForm_id = ObjPer.form_id;
            if (ObjPer.CheckPermission() == false)
            {
                Global.Message(BLL.GlobalDec.gStrPermissionViwMsg);
                return;
            }
            if (Global.CheckDefault() == 0)
            {
                Global.Message("Please Check User Default Setting");
                this.Close();
                return;
            }
            Val.frmGenSet(this);
            AttachFormEvents();

            // for Dynamic Setting By Praful On 01022020

            if (Global.HideFormControls(Val.ToInt(ObjPer.form_id), this) != "")
            {
                Global.Message("Select First User Setting...Please Contact to Administrator...");
                return;
            }

            ControlSettingDT(Val.ToInt(ObjPer.form_id), this);
            AddGotFocusListener(this);
            this.KeyPreview = true;

            TabControlsToList(this.Controls);
            _tabControls = _tabControls.OrderBy(x => x.TabIndex).ToList();

            // End for Dynamic Setting By Praful On 01022020

            this.Show();
        }

        private void AddGotFocusListener(Control ctrl)
        {
            foreach (Control c in ctrl.Controls)
            {
                c.GotFocus += new EventHandler(Control_GotFocus);
                if (c.Controls.Count > 0)
                {
                    AddGotFocusListener(c);
                }
            }
        }
        private void Control_GotFocus(object sender, EventArgs e)
        {
            if (!((Control)sender).Name.ToString().Trim().Equals(string.Empty))
            {
                _NextEnteredControl = (Control)sender;
                if ((Control)sender is LookUpEdit)
                {
                    ((LookUpEdit)(Control)sender).ShowPopup();
                }
            }
        }

        private void TabControlsToList(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control.TabStop)
                    _tabControls.Add(control);
                if (control.HasChildren)
                    TabControlsToList(control.Controls);
            }
        }
        private void ControlSettingDT(int FormCode, Form pForm)
        {
            BLL.Validation Val = new BLL.Validation();
            Single_Setting ObjSingleSettings = new Single_Setting();
            Single_SettingProperty Property = new Single_SettingProperty();

            Property.role_id = Val.ToInt(BLL.GlobalDec.gEmployeeProperty.role_id);
            Property.form_id = Val.ToInt(FormCode);
            DataTable DtColSetting = ObjSingleSettings.GetData(Property);

            DataTable DtFilterColSetting = (from DataRow dr in DtColSetting.Rows
                                            where Val.ToBooleanToInt(dr["is_control"]) == 1
                                            && dr["column_type"].ToString() != "LABEL"
                                            select dr).CopyToDataTable();
            DevExpress.XtraLayout.LayoutControl l = new DevExpress.XtraLayout.LayoutControl();
            l.OptionsFocus.EnableAutoTabOrder = false;

            if (DtFilterColSetting.Rows.Count > 0)
            {
                DtControlSettings = DtFilterColSetting;
                foreach (Control item1 in pForm.Controls)
                {
                    ControllSettings(item1, DtFilterColSetting);
                }
            }
        }

        private static void ControllSettings(Control item2, DataTable DTab)
        {
            BLL.Validation Val = new BLL.Validation();

            //else
            {
                var VarControlSetting = (from DataRow dr in DTab.Rows
                                         where dr["column_name"].ToString() == item2.Name.ToString()
                                         select dr);

                if (VarControlSetting.Count() > 0)
                {
                    DataRow DRow = VarControlSetting.CopyToDataTable().Rows[0];
                    if (item2.Name.ToString() == Val.ToString(DRow["column_name"]))
                    {
                        if (!(item2 is TextEdit))
                        {
                            if (!(item2 is DateTimePicker))
                            {
                                if (!(item2 is DevExpress.XtraEditors.TextEdit))
                                {
                                    item2.Text = (Val.ToBooleanToInt(DRow["is_compulsory"]).Equals(0) ? Val.ToString(DRow["caption"]) : "* " + Val.ToString(DRow["caption"]));
                                }
                            }
                        }
                        if (Val.ToInt(DRow["tab_index"]) >= 0)
                        {
                            if (item2.CanSelect)
                                item2.TabStop = true;
                        }
                        else
                            item2.TabStop = false;
                        if (Val.ToBooleanToInt(DRow["is_visible"]).Equals(1))
                        {
                            item2.Visible = true;
                            item2.TabStop = true;
                        }
                        else
                        {
                            item2.Visible = false;
                            item2.TabStop = false;
                        }

                        item2.TabIndex = Val.ToInt(DRow["tab_index"]);
                        if (item2.TabIndex == 1)
                        {
                            item2.Select();
                            item2.Focus();
                        }
                        if (Val.ToBooleanToInt(DRow["is_editable"]).Equals(1))
                        {
                            item2.Enabled = true;
                        }
                        else
                        {
                            item2.Enabled = false;
                        }
                    }
                }
                else
                {
                    item2.TabStop = false;
                }
            }
            if (item2.Controls.Count > 0)
            {
                foreach (Control item1 in item2.Controls)
                {
                    ControllSettings(item1, DTab);
                }
            }
        }

        private void AttachFormEvents()
        {
            objBOFormEvents.CurForm = this;
            objBOFormEvents.FormKeyPress = true;
            objBOFormEvents.FormKeyDown = true;
            objBOFormEvents.FormResize = true;
            objBOFormEvents.FormClosing = true;
            //objBOFormEvents.ObjToDispose.Add(objCountry);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region "Events" 
        private void FrmPriceImport_Load(object sender, EventArgs e)
        {
            try
            {
                Global.LOOKUPRate(lueRateType);
                Global.LOOKUPCurrency(lueCurrency);

                DataTable dtbDetails = ObjPrcImp.GetSaleData(Val.ToInt(lueRateType.EditValue), Val.ToInt(lueCurrency.EditValue));
                grdSaleSummary.DataSource = dtbDetails;

                dtpDate.Visible = false;

            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void FrmPriceImport_Shown(object sender, System.EventArgs e)
        {
            //dtpDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            //dtpDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            //dtpDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            //dtpDate.Properties.CharacterCasing = CharacterCasing.Upper;

            //dtpDate.EditValue = DateTime.Now;
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtPer.Text = "";
                DTabFile.Rows.Clear();
                grdPriceRevised.DataSource = null;
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        private void btnGenrate_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                grdPriceRevised.DataSource = null;


                DTabFile = ObjPrcImp.GetCostData(Val.ToInt(lueRateType.EditValue), Val.ToInt(lueCurrency.EditValue), Val.ToInt(txtDays.Text), Val.ToDecimal(txtPer.Text), Val.ToDecimal(txtRate.Text), Val.ToString(Val.DBDate(dtpDate.Text)));

                DataView dtview = DTabFile.Copy().DefaultView;
                double TotalSaleAmount = Val.Val(dtview.ToTable().Compute("Sum(saleamount)", ""));

                foreach (DataRow Drw in DTabFile.Rows)
                {
                    if (Val.Val(Drw["salecarat"]) > 0)
                        Drw["avgsale"] = Math.Round(Val.Val((TotalSaleAmount) / Val.Val(Drw["salecarat"])), 3);
                    else
                        Drw["avgsale"] = 0;
                }

                grdPriceRevised.DataSource = DTabFile;
                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                this.Cursor = Cursors.Default;
                return;
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            Global.Export("xlsx", GrvPriceRevised);
        }

        #region GridEvents
        private void GrvPriceRevised_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (Val.ToInt(GrvPriceRevised.GetRowCellValue(e.RowHandle, clmIsMoving)) == 1)
                    {
                        e.Appearance.ForeColor = Color.Black;
                        e.Appearance.BackColor = Color.FromArgb(255, 233, 236);
                    }

                    if (e.Column == clmSaleCarat)
                    {
                        //decimal numTargetCarat = Val.ToDecimal(GrvPriceRevised.GetRowCellValue(e.RowHandle, clmTargetCarat));
                        //decimal numSaleCarat = Val.ToDecimal(GrvPriceRevised.GetRowCellValue(e.RowHandle, clmSaleCarat));

                        //if (numTargetCarat > 0 && numSaleCarat > 0)
                        //{
                        //    if ((numTargetCarat / 2) > numSaleCarat)
                        //    {
                        //        e.Appearance.BackColor = Color.LightPink;
                        //        e.Appearance.ForeColor = Color.FromArgb(151, 71, 6);
                        //    }
                        //}

                        if (Val.ToInt(GrvPriceRevised.GetRowCellValue(e.RowHandle, clmIsTargetMissed)) == 1)
                        {
                            e.Appearance.BackColor = Color.LightPink;
                            e.Appearance.BackColor = Color.FromArgb(255, 233, 236); e.Appearance.ForeColor = Color.FromArgb(151, 71, 6);
                        }

                    }
                    if (e.Column == clmSaleRate)
                    {
                        if ((Val.ToDecimal(GrvPriceRevised.GetRowCellValue(e.RowHandle, clmSujestedRate)) > Val.ToDecimal(GrvPriceRevised.GetRowCellValue(e.RowHandle, clmSaleRate))))
                        {
                            e.Appearance.BackColor = Color.LightYellow;
                            e.Appearance.ForeColor = Color.FromArgb(151, 71, 6);
                        }
                    }

                    if (e.Column == clmAging)
                    {
                        if (Val.ToInt(GrvPriceRevised.GetRowCellValue(e.RowHandle, clmIsAging)) == 1)
                        {
                            e.Appearance.ForeColor = Color.Black;
                            e.Appearance.BackColor = Color.OrangeRed;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }
        #endregion

        #endregion

        #region Function
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
                            dgvSaleSummary.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvSaleSummary.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvSaleSummary.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvSaleSummary.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvSaleSummary.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvSaleSummary.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvSaleSummary.ExportToCsv(Filepath);
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

        private void chkOldPrice_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOldPrice.Checked)
            {
                dtpDate.Properties.Items.Clear();

                DataTable dtb_Old_PriceDate = ObjPrcImp.GetOld_PriceDate(Val.ToInt(lueRateType.EditValue), Val.ToInt(lueCurrency.EditValue));

                if (dtb_Old_PriceDate.Rows.Count > 0)
                {
                    foreach (DataRow DRow in dtb_Old_PriceDate.Rows)
                    {
                        dtpDate.Properties.Items.Add(Convert.ToDateTime(DRow[0]).ToString("dd/MMM/yyyy"));
                    }
                    if (dtpDate.Properties.Items.Count >= 1)
                    {
                        dtpDate.SelectedIndex = 0;
                    }
                }
                else
                {
                    dtpDate.Properties.Items.Clear();
                    dtpDate.EditValue = null;
                }

                dtpDate.Visible = true;
                GrvPriceRevised.Columns["old_rate"].Visible = true;
            }
            else
            {
                GrvPriceRevised.Columns["old_rate"].Visible = false;
                dtpDate.Visible = false;
            }
        }
    }
}
