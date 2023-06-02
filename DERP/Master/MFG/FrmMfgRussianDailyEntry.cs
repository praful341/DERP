using BLL;
using BLL.FunctionClasses.Master.MFG;
using BLL.PropertyClasses.Master.MFG;
using DERP.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using static DERP.Class.Global;

namespace DERP.Master.MFG
{
    public partial class FrmMfgRussianDailyEntry : DevExpress.XtraEditors.XtraForm
    {
        #region Data Member

        BLL.FormEvents objBOFormEvents;
        BLL.Validation Val;
        BLL.FormPer ObjPer;
        BLL.BeginTranConnection Conn;

        FrmMfgRussianDailyEntryMaster objRussianDailyEntry;
        #endregion

        #region Constructor 
        public FrmMfgRussianDailyEntry()
        {
            InitializeComponent();

            objBOFormEvents = new BLL.FormEvents();
            Val = new BLL.Validation();
            ObjPer = new BLL.FormPer();

            //objMiscellaneous = new MfgMiscellaneousEntryMaster();
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
            //objBOFormEvents.ObjToDispose.Add(objMiscellaneous);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        #endregion

        #region Events
        private void FrmMfgMachineCompanyMaster_Load(object sender, EventArgs e)
        {
            try
            {

                GetData();
                Global.LOOKUPParty(lueParty);
                btnClear_Click(btnClear, null);
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
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

                txt4PPcs.Text = "";
                txt4PMach.Text = "";
                txt4PEmp.Text = "";
                txt4PMAvg.Text = "";
                txt4PEmpAvg.Text = "";

                lueParty.EditValue = null;
                txtSarinPcs.Text = "";
                txtSarinMach.Text = "";
                txtSarinEmp.Text = "";
                txtSarinMAvg.Text = "";
                txtSarinEmpAvg.Text = "";

                txtStitchPcs.Text = "";
                txtStitchMach.Text = "";
                txtStitchEmp.Text = "";
                txtStitchMAvg.Text = "";
                txtStitchEmpAvg.Text = "";

                txtRobotPcs.Text = "";
                txtRobotMach.Text = "";
                txtRobotEmp.Text = "";
                txtRobotMAvg.Text = "";
                txtRobotEmpAvg.Text = "";

                txtManualPcs.Text = "";
                txtManualMach.Text = "";
                txtManualEmp.Text = "";
                txtManualMAvg.Text = "";
                txtManualEmpAvg.Text = "";

                var today = DateTime.Now;
                var From_Date = new DateTime(today.Year, today.Month, 1);
                dtpFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpFromDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpFromDate.EditValue = From_Date;

                dtpToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                dtpToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                dtpToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                dtpToDate.Properties.CharacterCasing = CharacterCasing.Upper;
                dtpToDate.EditValue = DateTime.Now;

                DTPDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                DTPDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                DTPDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                DTPDate.Properties.CharacterCasing = CharacterCasing.Upper;
                DTPDate.EditValue = DateTime.Now;
                lblUnionId.Text = "0";
                lblSarin.Tag = 0;
                lblStitching.Tag = 0;
                lblRobot.Tag = 0;
                lbl4PTotal.Tag = 0;
                lblManual.Tag = 0;

                GetData();
                lueParty.Focus();


            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region GridEvents       
        private void dgvMfgItemCompanyMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                if (e.RowHandle >= 0)
                {
                    if (e.Clicks == 2)
                    {
                        DataRow Drow = dgvItemCompanyMaster.GetDataRow(e.RowHandle);
                        lblMode.Text = "Edit Mode";
                        //lblMode.Tag = Val.ToInt64(Drow["miscellaneous_id"]);
                        lblUnionId.Text = Val.ToString(Drow["union_id"]);
                        lblSarin.Tag = Val.ToInt64(Drow["Sarin_id"]);
                        lblStitching.Tag = Val.ToInt64(Drow["Stich_id"]);
                        lblRobot.Tag = Val.ToInt64(Drow["Robot_id"]);
                        lblManual.Tag = Val.ToInt64(Drow["Manual_id"]);
                        lbl4PTotal.Tag = Val.ToInt64(Drow["4Ptotal_id"]);

                        lueParty.EditValue = Val.ToInt32(Drow["party_id"]);
                        DTPDate.EditValue = Val.ToString(Drow["date"]);

                        txtSarinPcs.Text = Val.ToString(Val.ToInt64(Drow["Sarin_pcs"]));
                        txtSarinMach.Text = Val.ToString(Val.ToInt64(Drow["Sarin_Mach"]));
                        txtSarinMAvg.Text = Val.ToString(Val.ToDecimal(Drow["Sarin_MacAvg"]));
                        txtSarinEmp.Text = Val.ToString(Val.ToInt64(Drow["Sarin_Emp"]));
                        txtSarinEmpAvg.Text = Val.ToString(Val.ToDecimal(Drow["Sarin_Emp_Avg"]));

                        txtStitchPcs.Text = Val.ToString(Val.ToInt64(Drow["Stich_pcs"]));
                        txtStitchMach.Text = Val.ToString(Val.ToInt64(Drow["Stich_Mach"]));
                        txtStitchMAvg.Text = Val.ToString(Val.ToDecimal(Drow["Stich_MacAvg"]));
                        txtStitchEmp.Text = Val.ToString(Val.ToInt64(Drow["Stich_Emp"]));
                        txtStitchEmpAvg.Text = Val.ToString(Val.ToDecimal(Drow["Stich_Emp_Avg"]));

                        txtRobotPcs.Text = Val.ToString(Val.ToInt64(Drow["Robot_pcs"]));
                        txtRobotMach.Text = Val.ToString(Val.ToInt64(Drow["Robot_Mach"]));
                        txtRobotMAvg.Text = Val.ToString(Val.ToDecimal(Drow["Robot_MacAvg"]));
                        txtRobotEmp.Text = Val.ToString(Val.ToInt64(Drow["Robot_Emp"]));
                        txtRobotEmpAvg.Text = Val.ToString(Val.ToDecimal(Drow["Robot_Emp_Avg"]));

                        txtManualPcs.Text = Val.ToString(Val.ToInt64(Drow["Manual_pcs"]));
                        txtManualMach.Text = Val.ToString(Val.ToInt64(Drow["Manual_Mach"]));
                        txtManualMAvg.Text = Val.ToString(Val.ToDecimal(Drow["Manual_MacAvg"]));
                        txtManualEmp.Text = Val.ToString(Val.ToInt64(Drow["Manual_Emp"]));
                        txtManualEmpAvg.Text = Val.ToString(Val.ToDecimal(Drow["Manual_Emp_Avg"]));

                        txt4PPcs.Text = Val.ToString(Val.ToInt64(Drow["4Ptotal_pcs"]));
                        txt4PMach.Text = Val.ToString(Val.ToInt64(Drow["4Ptotal_Mach"]));
                        txt4PMAvg.Text = Val.ToString(Val.ToDecimal(Drow["4Ptotal_MacAvg"]));
                        txt4PEmp.Text = Val.ToString(Val.ToInt64(Drow["4Ptotal_Emp"]));
                        txt4PEmpAvg.Text = Val.ToString(Val.ToDecimal(Drow["4Ptotal_Emp_Avg"]));


                        lueParty.Focus();
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
            FrmMfgRussianDailyEntryProperty russianObj = new FrmMfgRussianDailyEntryProperty();
            objRussianDailyEntry = new FrmMfgRussianDailyEntryMaster();
            Conn = new BeginTranConnection(true, false);
            int UnionId = Val.ToInt32(lblUnionId.Text);
            int IntRes;
            try
            {
                if (!ValidateDetails())
                {
                    blnReturn = false;
                    return blnReturn;
                }

                for (int i = 0; i < 5; i++)
                {
                    if (i == 0)
                    {
                        russianObj.id = Val.ToInt32(lblSarin.Tag);
                        russianObj.process_name = "SARIN";
                        russianObj.union_id = UnionId;
                        russianObj.party_id = Val.ToInt(lueParty.EditValue);
                        russianObj.date = DTPDate.DateTime;
                        russianObj.pcs = Val.ToInt(txtSarinPcs.Text);
                        russianObj.no_of_mach = Val.ToInt(txtSarinMach.Text);
                        russianObj.mach_avg = Val.ToDecimal(txtSarinMAvg.Text);
                        russianObj.no_of_emp = Val.ToInt(txtSarinEmp.Text);
                        russianObj.emp_avg = Val.ToDecimal(txtSarinEmpAvg.Text);
                        //IntRes = objRussianDailyEntry.Save(russianObj);
                        russianObj = objRussianDailyEntry.Save(russianObj, DLL.GlobalDec.EnumTran.Continue, Conn);
                        //process_name = "Sarin"
                        //pcs = val.toint32(txtboilingno.text);
                        //objmfgboilingnoproperty = mfgboilingno.save(objmfgboilingnoproperty, dll.globaldec.enumtran.continue, conn);
                        //boil_intres = objmfgboilingnoproperty.boiling_no;
                        UnionId = Val.ToInt32(russianObj.union_id);
                    }
                    else if (i == 1)
                    {
                        russianObj.id = Val.ToInt32(lblStitching.Tag);
                        russianObj.process_name = "STITCHING";
                        russianObj.union_id = UnionId;
                        russianObj.party_id = Val.ToInt(lueParty.EditValue);
                        russianObj.date = DTPDate.DateTime;
                        russianObj.pcs = Val.ToInt(txtStitchPcs.Text);
                        russianObj.no_of_mach = Val.ToInt(txtStitchMach.Text);
                        russianObj.mach_avg = Val.ToDecimal(txtStitchMAvg.Text);
                        russianObj.no_of_emp = Val.ToInt(txtStitchEmp.Text);
                        russianObj.emp_avg = Val.ToDecimal(txtStitchEmpAvg.Text);
                        russianObj = objRussianDailyEntry.Save(russianObj, DLL.GlobalDec.EnumTran.Continue, Conn);
                        //process_name = "Sarin"
                        //pcs = val.toint32(txtboilingno.text);
                        //objmfgboilingnoproperty = mfgboilingno.save(objmfgboilingnoproperty, dll.globaldec.enumtran.continue, conn);
                        //boil_intres = objmfgboilingnoproperty.boiling_no;
                        UnionId = Val.ToInt32(russianObj.union_id);
                    }
                    else if (i == 2)
                    {
                        russianObj.id = Val.ToInt32(lblRobot.Tag);
                        russianObj.process_name = "ROBOT";
                        russianObj.union_id = UnionId;
                        russianObj.party_id = Val.ToInt(lueParty.EditValue);
                        russianObj.date = DTPDate.DateTime;
                        russianObj.pcs = Val.ToInt(txtRobotPcs.Text);
                        russianObj.no_of_mach = Val.ToInt(txtRobotMach.Text);
                        russianObj.mach_avg = Val.ToDecimal(txtRobotMAvg.Text);
                        russianObj.no_of_emp = Val.ToInt(txtRobotEmp.Text);
                        russianObj.emp_avg = Val.ToDecimal(txtRobotEmpAvg.Text);
                        russianObj = objRussianDailyEntry.Save(russianObj, DLL.GlobalDec.EnumTran.Continue, Conn);
                        //process_name = "Sarin"
                        //pcs = val.toint32(txtboilingno.text);
                        //objmfgboilingnoproperty = mfgboilingno.save(objmfgboilingnoproperty, dll.globaldec.enumtran.continue, conn);
                        //boil_intres = objmfgboilingnoproperty.boiling_no;
                        UnionId = Val.ToInt32(russianObj.union_id);
                    }
                    else if (i == 3)
                    {
                        russianObj.id = Val.ToInt32(lblManual.Tag);
                        russianObj.process_name = "MANUAL";
                        russianObj.union_id = UnionId;
                        russianObj.party_id = Val.ToInt(lueParty.EditValue);
                        russianObj.date = DTPDate.DateTime;
                        russianObj.pcs = Val.ToInt(txtManualPcs.Text);
                        russianObj.no_of_mach = Val.ToInt(txtManualMach.Text);
                        russianObj.mach_avg = Val.ToDecimal(txtManualMAvg.Text);
                        russianObj.no_of_emp = Val.ToInt(txtManualEmp.Text);
                        russianObj.emp_avg = Val.ToDecimal(txtManualEmpAvg.Text);
                        russianObj = objRussianDailyEntry.Save(russianObj, DLL.GlobalDec.EnumTran.Continue, Conn);
                        //process_name = "Sarin"
                        //pcs = val.toint32(txtboilingno.text);
                        //objmfgboilingnoproperty = mfgboilingno.save(objmfgboilingnoproperty, dll.globaldec.enumtran.continue, conn);
                        //boil_intres = objmfgboilingnoproperty.boiling_no;
                        UnionId = Val.ToInt32(russianObj.union_id);
                    }
                    else if (i == 4)
                    {
                        russianObj.id = Val.ToInt32(lbl4PTotal.Tag);
                        russianObj.process_name = "4P TOTAL";
                        russianObj.union_id = UnionId;
                        russianObj.party_id = Val.ToInt(lueParty.EditValue);
                        russianObj.date = DTPDate.DateTime;
                        russianObj.pcs = Val.ToInt(txt4PPcs.Text);
                        russianObj.no_of_mach = Val.ToInt(txt4PMach.Text);
                        russianObj.mach_avg = Val.ToDecimal(txt4PMAvg.Text);
                        russianObj.no_of_emp = Val.ToInt(txt4PEmp.Text);
                        russianObj.emp_avg = Val.ToDecimal(txt4PEmpAvg.Text);
                        russianObj = objRussianDailyEntry.Save(russianObj, DLL.GlobalDec.EnumTran.Continue, Conn);
                        //process_name = "Sarin"
                        //pcs = val.toint32(txtboilingno.text);
                        //objmfgboilingnoproperty = mfgboilingno.save(objmfgboilingnoproperty, dll.globaldec.enumtran.continue, conn);
                        //boil_intres = objmfgboilingnoproperty.boiling_no;
                        UnionId = Val.ToInt32(russianObj.union_id);
                    }

                }
                Conn.Inter1.Commit();
                if (UnionId > 0 && Val.ToInt32(lblUnionId.Text) == 0)
                {
                    Global.Message("Entry Save Succesfully..");
                }
                else if (Val.ToInt32(lblUnionId.Text) > 0)
                {
                    Global.Message("Entry Update Succesfully..");
                }
                else
                {
                    Global.Message("Entry Not Saved..");
                }

            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                blnReturn = false;
            }

            return blnReturn;
        }
        private bool ValidateDetails()
        {
            bool blnFocus = false;
            List<ListError> lstError = new List<ListError>();
            try
            {
                if (DTPDate.Text == null)
                {
                    lstError.Add(new ListError(12, "Purchase Date"));
                    if (!blnFocus)
                    {
                        blnFocus = true;
                        DTPDate.Focus();
                    }
                }

                //if (txtItemName.Text == null)
                //{
                //    lstError.Add(new ListError(12, "Item Name"));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        txtItemName.Focus();
                //    }
                //}

                //if (txtQty.Text == null)
                //{
                //    lstError.Add(new ListError(12, "Qty"));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        txtQty.Focus();
                //    }
                //}

                //if (txtRate.Text == null)
                //{
                //    lstError.Add(new ListError(12, "Rate"));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        txtRate.Focus();
                //    }
                //}
                //if (!objMachineCompany.ISExists(txtPartyName.Text, Val.ToInt64(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
                //{
                //    lstError.Add(new ListError(23, "Company"));
                //    if (!blnFocus)
                //    {
                //        blnFocus = true;
                //        txtPartyName.Focus();
                //    }

                //}

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
                objRussianDailyEntry = new FrmMfgRussianDailyEntryMaster();
                DataTable DTab = objRussianDailyEntry.GetData(Val.DBDate(dtpFromDate.Text), Val.DBDate(dtpToDate.Text));
                grdItemCompanyMaster.DataSource = DTab;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
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
                            dgvItemCompanyMaster.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            dgvItemCompanyMaster.ExportToXls(Filepath);
                            break;
                        case "xlsx":
                            dgvItemCompanyMaster.ExportToXlsx(Filepath);
                            break;
                        case "rtf":
                            dgvItemCompanyMaster.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            dgvItemCompanyMaster.ExportToText(Filepath);
                            break;
                        case "html":
                            dgvItemCompanyMaster.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            dgvItemCompanyMaster.ExportToCsv(Filepath);
                            break;
                    }

                    if (format.Equals(Exports.xlsx.ToString()))
                    {
                        if (Global.Confirm("Export Done\n\nDo you want to open Excel File?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(Filepath);
                        }
                    }
                    else if (format.Equals(Exports.pdf.ToString()))
                    {
                        if (Global.Confirm("Export Done\n\nDo you want to open PDF File?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(Filepath);
                        }
                    }
                    else
                    {
                        if (Global.Confirm("Export Done\n\nDo you want to open file?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
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

        private void DTPPurchaseDate_EditValueChanged(object sender, EventArgs e)
        {

        }

        //private void txtQty_EditValueChanged(object sender, EventArgs e)
        //{
        //    if ((txtQty.Text == "" || txtQty.Text == string.Empty) && (txtRate.Text == "" || txtRate.Text == string.Empty))
        //    {
        //        txtAmount.Text = string.Empty;
        //    }
        //    else
        //    {
        //        txtAmount.Text = Val.ToString(Val.ToDecimal(txtQty.Text) * Val.ToDecimal(txtRate.Text));
        //    }
        //}

        //private void txtRate_EditValueChanged(object sender, EventArgs e)
        //{
        //    if ((txtQty.Text == "" || txtQty.Text == string.Empty) && (txtRate.Text == "" || txtRate.Text == string.Empty))
        //    {
        //        txtAmount.Text = string.Empty;
        //    }
        //    else
        //    {
        //        txtAmount.Text = Val.ToString(Val.ToDecimal(txtQty.Text) * Val.ToDecimal(txtRate.Text));
        //    }
        //}

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable DTab = objRussianDailyEntry.GetData(Val.DBDate(dtpFromDate.Text), Val.DBDate(dtpToDate.Text));
                grdItemCompanyMaster.DataSource = DTab;
            }
            catch (Exception ex)
            {
                BLL.General.ShowErrors(ex);
                return;
            }

        }

        private void panelControl5_Paint(object sender, PaintEventArgs e)
        {

        }

        //private void lueManager_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Button.Index == 1)
        //        {
        //            FrmMstBillManagerMaster billManager = new FrmMstBillManagerMaster();
        //            billManager.ShowDialog();
        //            Global.LOOKUPBillManager(lueManager);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        General.ShowErrors(ex.ToString());
        //        return;
        //    }
        //}

        #region ControlFill
        private void txtSarinPcs_Validated(object sender, EventArgs e)
        {
            //try
            //{
            //    txtSarinMAvg.Text = Val.ToString(Val.ToInt(txtSarinPcs.Text) / Val.ToInt(txtSarinMach.Text));
            //    txtSarinEmpAvg.Text = Val.ToString(Val.ToInt(txtSarinPcs.Text) / Val.ToInt(txtSarinEmp.Text));
            //}
            //catch (Exception ex)
            //{
            //    General.ShowErrors(ex.ToString());
            //    return;
            //}
        }

        private void txtSarinMach_Validated(object sender, EventArgs e)
        {
            try
            {
                txtSarinMAvg.Text = Val.ToString(Val.ToDecimal(txtSarinPcs.Text) / Val.ToDecimal(txtSarinMach.Text));
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }

        private void txtSarinEmp_Validated(object sender, EventArgs e)
        {
            try
            {
                txtSarinEmpAvg.Text = Val.ToString(Val.ToDecimal(txtSarinPcs.Text) / Val.ToDecimal(txtSarinEmp.Text));
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }

        }

        private void txtStitchPcs_Validated(object sender, EventArgs e)
        {
            //try
            //{
            //    txtStitchMAvg.Text = Val.ToString(Val.ToInt(txtStitchPcs.Text) / Val.ToInt(txtStitchMach.Text));
            //    txtStitchEmpAvg.Text = Val.ToString(Val.ToInt(txtStitchPcs.Text) / Val.ToInt(txtStitchEmp.Text));
            //}
            //catch (Exception ex)
            //{
            //    General.ShowErrors(ex.ToString());
            //    return;
            //}
        }

        private void txtStitchMach_Validated(object sender, EventArgs e)
        {
            try
            {
                txtStitchMAvg.Text = Val.ToString(Val.ToDecimal(txtStitchPcs.Text) / Val.ToDecimal(txtStitchMach.Text));
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }

        private void txtStitchEmp_Validated(object sender, EventArgs e)
        {
            try
            {
                txtStitchEmpAvg.Text = Val.ToString(Val.ToDecimal(txtStitchPcs.Text) / Val.ToDecimal(txtStitchEmp.Text));
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }

        private void txtRobotPcs_Validated(object sender, EventArgs e)
        {
            //try
            //{
            //    txtRobotMAvg.Text = Val.ToString(Val.ToInt(txtRobotPcs.Text) / Val.ToInt(txtRobotMach.Text));
            //    txtRobotEmpAvg.Text = Val.ToString(Val.ToInt(txtRobotPcs.Text) / Val.ToInt(txtRobotEmp.Text));
            //    txt4PPcs.Text = Val.ToString(Val.ToInt(txtRobotPcs.Text) + Val.ToInt(txtManualPcs.Text));
            //}
            //catch (Exception ex)
            //{
            //    General.ShowErrors(ex.ToString());
            //    return;
            //}
        }

        private void txtRobotMach_Validated(object sender, EventArgs e)
        {
            try
            {
                txtRobotMAvg.Text = Val.ToString(Val.ToDecimal(txtRobotPcs.Text) / Val.ToDecimal(txtRobotMach.Text));
                txt4PMach.Text = Val.ToString(Val.ToInt(txtRobotMach.Text) + Val.ToInt(txtManualMach.Text));
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }

        private void txtRobotEmp_Validated(object sender, EventArgs e)
        {
            try
            {
                txtRobotEmpAvg.Text = Val.ToString(Val.ToDecimal(txtRobotPcs.Text) / Val.ToDecimal(txtRobotEmp.Text));
                txt4PEmp.Text = Val.ToString(Val.ToInt(txtManualEmp.Text) + Val.ToInt(txtRobotEmp.Text));
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }

        private void txtManualPcs_Validated(object sender, EventArgs e)
        {
            try
            {
                //    txtManualMAvg.Text = Val.ToString(Val.ToInt(txtManualPcs.Text) / Val.ToInt(txtManualMach.Text));
                //    txtManualEmpAvg.Text = Val.ToString(Val.ToInt(txtManualPcs.Text) / Val.ToInt(txtManualEmp.Text));
                txt4PPcs.Text = Val.ToString(Val.ToInt(txtRobotPcs.Text) + Val.ToInt(txtManualPcs.Text));
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }

        private void txtManualMach_Validated(object sender, EventArgs e)
        {
            try
            {
                txtManualMAvg.Text = Val.ToString(Val.ToDecimal(txtManualPcs.Text) / Val.ToDecimal(txtManualMach.Text));
                txt4PMach.Text = Val.ToString(Val.ToInt(txtRobotMach.Text) + Val.ToInt(txtManualMach.Text));
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }

        private void txtManualEmp_Validated(object sender, EventArgs e)
        {
            try
            {
                txtManualEmpAvg.Text = Val.ToString(Val.ToDecimal(txtManualPcs.Text) / Val.ToDecimal(txtManualEmp.Text));
                txt4PEmp.Text = Val.ToString(Val.ToInt(txtManualEmp.Text) + Val.ToInt(txtRobotEmp.Text));
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }

        private void txt4PPcs_Validated(object sender, EventArgs e)
        {
            try
            {
                txt4PMAvg.Text = Val.ToString(Val.ToInt(txt4PPcs.Text) / Val.ToInt(txt4PMach.Text));
                txt4PEmpAvg.Text = Val.ToString(Val.ToInt(txt4PPcs.Text) / Val.ToInt(txt4PEmp.Text));
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }

        private void txt4PMach_Validated(object sender, EventArgs e)
        {
            try
            {
                txt4PMAvg.Text = Val.ToString(Val.ToInt(txt4PPcs.Text) / Val.ToInt(txt4PMach.Text));
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }

        private void txt4PEmp_Validated(object sender, EventArgs e)
        {
            try
            {
                txt4PEmpAvg.Text = Val.ToString(Val.ToInt(txt4PPcs.Text) / Val.ToInt(txt4PEmp.Text));
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }

        private void txt4PPcs_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToDecimal(txt4PMach.Text) > 0 && Val.ToDecimal(txt4PEmp.Text) > 0)
                {
                    txt4PMAvg.Text = Val.ToString(Val.ToDecimal(txt4PPcs.Text) / Val.ToDecimal(txt4PMach.Text));
                    txt4PEmpAvg.Text = Val.ToString(Val.ToDecimal(txt4PPcs.Text) / Val.ToDecimal(txt4PEmp.Text));
                }
                else
                {
                    txt4PMAvg.Text = "0";
                    txt4PEmpAvg.Text = "0";
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }

        private void txt4PMach_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToDecimal(txt4PMach.Text) > 0)
                {
                    txt4PMAvg.Text = Val.ToString(Val.ToDecimal(txt4PPcs.Text) / Val.ToDecimal(txt4PMach.Text));
                }
                else
                {
                    txt4PMAvg.Text = "0";
                }

            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }

        private void txt4PEmp_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToDecimal(txt4PEmp.Text) > 0)
                {
                    txt4PEmpAvg.Text = Val.ToString(Val.ToDecimal(txt4PPcs.Text) / Val.ToDecimal(txt4PEmp.Text));
                }
                else
                {
                    txt4PEmpAvg.Text = "0";
                }
            }
            catch (Exception ex)
            {
                General.ShowErrors(ex.ToString());
                return;
            }
        }
        #endregion


    }
}
