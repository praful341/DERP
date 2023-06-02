using BLL;
using BLL.FunctionClasses.Account;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Master.HR;
using BLL.FunctionClasses.Master.MFG;
using BLL.FunctionClasses.Master.Store;
using BLL.FunctionClasses.Rejection;
using BLL.FunctionClasses.Transaction;
using BLL.PropertyClasses.Transaction;
using DERP.Report.Barcode_Print;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using DevExpress.XtraTab;
using DLL;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DERP.Class
{
    static class Global
    {

        [DllImport("user32.dll", EntryPoint = "SendMessageA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        private const int WM_SETREDRAW = 0xB;

        public static Form gMainFormRef;
        private static string streamType = string.Empty;
        public static int Gwidth;
        public static int Gheight;

        public static bool gBoolForceFullyClose = false;
        public static string gStrConnectionStringA = string.Empty;
        public static string gStrproviderNameA = string.Empty;
        public static string gStrConnectionDeveloper = string.Empty;
        public static string gStrProviderDeveloper = string.Empty;
        public static string gStrExeUpdateMessageFilePath = string.Empty;

        public static string gStrProdSummarySeeting = string.Empty;

        public static string DevCellNumberFormatForFloat = "{0:N2}";
        public static string DevCellNumberFormatForInt = "{0:N0}";

        public static bool gBoolIS64BitOSSystem = false;
        public static string gStrVersion = "";
        public static bool gBool_Voucher_Update_On_Report = false;

        public static string gStrStrHostName = string.Empty;
        public static string gStrStrPort = string.Empty;
        public static string gStrStrServiceName = string.Empty;
        public static string gStrStrUserName = string.Empty;
        public static string gStrStrPasssword = string.Empty;

        public static string gStrDeveloperStrHostName = string.Empty;
        public static string gStrDeveloperStrPort = string.Empty;
        public static string gStrDeveloperStrServiceName = string.Empty;
        public static string gStrDeveloperStrUserName = string.Empty;
        public static string gStrDeveloperStrPasssword = string.Empty;

        public static string gStrWebStrHostName = string.Empty;
        public static string gStrWebStrDatabase = string.Empty;
        public static string gStrWebStrUserName = string.Empty;
        public static string gStrWebStrPasssword = string.Empty;

        public static DataTable DtTransfer = new DataTable();

        public static int Allow_Developer { get; private set; }

        [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
        private static extern bool BitBlt
        (
            IntPtr hdcDest, // handle to destination DC
            int nXDest, // x-coord of destination upper-left corner
            int nYDest, // y-coord of destination upper-left corner
            int nWidth, // width of destination rectangle
            int nHeight, // height of destination rectangle
            IntPtr hdcSrc, // handle to source DC
            int nXSrc, // x-coordinate of source upper-left corner
            int nYSrc, // y-coordinate of source upper-left corner
            System.Int32 dwRop // raster operation code
        );

        public static string GetCommaStr(string tstr)
        {
            if (tstr.Length > 0)
                return tstr = "'" + (tstr.Trim(',').Replace(",", "','")) + "'";
            else
                return "";
        }

        #region  For Suspend Grid To Repaing

        public static void SuspendDrawing(this Control target)
        {
            SendMessage(target.Handle, WM_SETREDRAW, 0, 0);
        }

        public static void ResumeDrawing(this Control target)
        {
            ResumeDrawing(target, true);
        }
        public static void ResumeDrawing(this Control target, bool redraw)
        {
            SendMessage(target.Handle, WM_SETREDRAW, 1, 0);

            if (redraw)
            {
                target.Refresh();
            }
        }

        internal static DialogResult Confirm(string v, string empty, MessageBoxIcon question)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Message Box
        public static void Message(string pStrText, string pStrTitle = "", MessageBoxButtons pMessageBoxButton = MessageBoxButtons.OK, MessageBoxIcon pMessageBoxIcon = MessageBoxIcon.None)
        {
            if (pStrTitle == "")
            {
                pStrTitle = BLL.GlobalDec.gStrMsgTitle;
            }
            DevExpress.XtraEditors.XtraMessageBox.Show(pStrText, pStrTitle, pMessageBoxButton, pMessageBoxIcon);
        }

        public static void ErrorMessage(string pStrText, string pStrTitle = "", MessageBoxButtons pMessageBoxButton = MessageBoxButtons.OK, MessageBoxIcon pMessageBoxIcon = MessageBoxIcon.Error)
        {
            if (pStrTitle == "")
            {
                pStrTitle = BLL.GlobalDec.gStrMsgTitle;
            }
            DevExpress.XtraEditors.XtraMessageBox.Show(pStrText, pStrTitle, pMessageBoxButton, pMessageBoxIcon);
        }
        public static int CheckDefault()
        {
            if (BLL.GlobalDec.gEmployeeProperty.currency_id == 0 && BLL.GlobalDec.gEmployeeProperty.secondary_currency_id == 0 && BLL.GlobalDec.gEmployeeProperty.rate_type_id == 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
        public static void CopyFormat(string FileName, String DefaultFileName, String format = "xls")
        {
            try
            {
                string dlgHeader = string.Empty;
                string dlgFilter = string.Empty;
                format = format.ToLower();

                if (format.Equals(Exports.xls.ToString()))
                {
                    dlgHeader = "Export to Excel";
                    dlgFilter = "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                }
                else if (format.Equals(Exports.csv.ToString()))
                {
                    dlgHeader = "Export to CSV";
                    dlgFilter = "csv (*.csv)|*.csv";
                }
                else if (format.Equals(Exports.xlsx.ToString()))
                {
                    dlgHeader = "Export to XLSX";
                    dlgFilter = "Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                }
                else if (format.Equals(Exports.xls.ToString()))
                {
                    dlgHeader = "Export to XLS";
                    dlgFilter = "Excel files 2007(*.xls)|*.xls|All files (*.*)|*.*";
                }

                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = format;
                svDialog.Title = "Copy Format";
                svDialog.FileName = DefaultFileName;
                svDialog.Filter = dlgFilter;
                string Filepath = string.Empty;
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    Filepath = svDialog.FileName;
                    try
                    {
                        File.Copy(FileName, Filepath);
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.ToString());
                    }
                }
                System.Diagnostics.Process.Start(Filepath, "CMD");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        #region Other Function

        public enum Exports
        {
            pdf = 1,
            xls = 2,
            rtf = 3,
            txt = 4,
            html = 5,
            csv = 6,
            xlsx = 7
        }
        public static void Export(string format, GridView gvExportGrid, bool isHeaderPrint = true)
        {
            try
            {
                if (gvExportGrid.RowCount < 1)
                {
                    Message("No Rows to Export");
                    return;
                }

                string dlgHeader = string.Empty;
                string dlgFilter = string.Empty;
                format = format.ToLower();
                if (format.Equals(Exports.xls.ToString()))
                {
                    dlgHeader = "Export to Excel";
                    dlgFilter = "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                }
                else if (format.Equals(Exports.pdf.ToString()))
                {
                    dlgHeader = "Export to PDF";
                    dlgFilter = "PDF (*.PDF)|*.PDF";
                }
                else if (format.Equals(Exports.rtf.ToString()))
                {
                    dlgHeader = "Export to RTF";
                    dlgFilter = "Word (*.doc) |*.doc;*.rtf|(*.txt) |*.txt|(*.*) |*.*";
                }
                else if (format.Equals(Exports.txt.ToString()))
                {
                    dlgHeader = "Export to Text";
                    dlgFilter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                }
                else if (format.Equals(Exports.html.ToString()))
                {
                    dlgHeader = "Export to HTML";
                    dlgFilter = "Html files (*.html)|*.html|Htm files (*.htm)|*.htm";
                }
                else if (format.Equals(Exports.csv.ToString()))
                {
                    dlgHeader = "Export to CSV";
                    dlgFilter = "csv (*.csv)|*.csv";
                }
                else if (format.Equals(Exports.xls.ToString()))
                {
                    dlgHeader = "Export to XLSX";
                    dlgFilter = "Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                }
                gvExportGrid.OptionsPrint.AutoWidth = false;
                gvExportGrid.OptionsPrint.PrintHeader = isHeaderPrint;

                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = format;
                svDialog.Title = dlgHeader;
                svDialog.FileName = "Report";
                svDialog.Filter = dlgFilter;
                string Filepath = string.Empty;
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    Filepath = svDialog.FileName;

                    switch (format)
                    {
                        case "pdf":
                            gvExportGrid.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            DevExpress.XtraPrinting.XlsExportOptionsEx op = new DevExpress.XtraPrinting.XlsExportOptionsEx();
                            op.ExportType = DevExpress.Export.ExportType.WYSIWYG;
                            gvExportGrid.ExportToXls(Filepath, op);
                            break;
                        case "rtf":
                            gvExportGrid.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            gvExportGrid.ExportToText(Filepath);
                            break;
                        case "html":
                            gvExportGrid.ExportToHtml(Filepath);
                            break;
                        case "csv":
                            gvExportGrid.ExportToCsv(Filepath);
                            break;
                        case "xlsx":
                            DevExpress.XtraPrinting.XlsxExportOptionsEx opx = new DevExpress.XtraPrinting.XlsxExportOptionsEx();
                            opx.ExportType = DevExpress.Export.ExportType.WYSIWYG;
                            gvExportGrid.ExportToXlsx(Filepath, opx);
                            break;
                    }
                }
                if (format.Equals(Exports.xls.ToString()))
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
                    if (Global.Confirm("Export Done\n\nYou Want To Open Excel File ?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(Filepath);
                    }
                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        public static string HideFormControls(int FormCode, Form pForm)
        {
            string Str = string.Empty;
            BLL.Validation Val = new BLL.Validation();
            Single_Setting ObjSingleSettings = new Single_Setting();
            Single_SettingProperty Property = new Single_SettingProperty();

            Property.role_id = Val.ToInt(BLL.GlobalDec.gEmployeeProperty.role_id);
            Property.form_id = Val.ToInt(FormCode);
            DataTable DtColSetting = ObjSingleSettings.GetData(Property);
            if (DtColSetting.Rows.Count > 0)
            {
                DataTable DtFilterColSetting = (from DataRow dr in DtColSetting.Rows
                                                where Val.ToBooleanToInt(dr["IS_CONTROL"]).ToString() == "1"
                                                select dr).CopyToDataTable();

                //
                foreach (Control item in pForm.Controls)
                {
                    TabStopFalse(item);
                }
                // 

                if (DtFilterColSetting.Rows.Count > 0)
                {
                    foreach (DataRow DRow in DtFilterColSetting.Rows)
                    {
                        foreach (Control item1 in pForm.Controls)
                        {
                            item1.TabStop = false;
                            ControlSetting(item1, DRow);
                        }
                    }
                }
            }
            else
            {
                Str = "Select First User Setting...Please Contact to Administrator...";
                return Str;
            }
            return Str;
        }

        private static void TabStopFalse(Control item)
        {
            foreach (Control item1 in item.Controls)
            {
                item1.TabStop = false;
                ControlSettingFalse(item1);

            }
        }
        private static void ControlSettingFalse(Control item1)
        {
            BLL.Validation Val = new BLL.Validation();
            if (item1 is PanelControl || item1 is GroupControl || item1 is XtraTabControl || item1 is XtraTabPage)
            {
                foreach (Control item2 in item1.Controls)
                {
                    item2.TabStop = false;

                    if (item2 is PanelControl || item2 is GroupControl || item2 is XtraTabControl || item2 is XtraTabPage)
                    {
                        ControlSettingFalse(item2);
                    }
                }
            }
            else
                item1.TabStop = false;
        }

        private static void ControlSetting(Control item1, DataRow DRow)
        {
            BLL.Validation Val = new BLL.Validation();
            if (item1 is PanelControl || item1 is GroupControl || item1 is XtraTabControl || item1 is XtraTabPage)
            {
                foreach (Control item2 in item1.Controls)
                {
                    item2.TabStop = false;

                    if (item2 is PanelControl || item2 is GroupControl || item2 is XtraTabControl || item2 is XtraTabPage)
                    {
                        //if (item2.Name.Contains("panelControl4"))
                        //{
                        //    //MessageBox.Show("panelControl4");
                        //}
                        ControlSetting(item2, DRow);
                    }
                    try
                    {

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
                            if (Val.ToBooleanToInt(DRow["is_visible"]).Equals(1))
                            {
                                item2.Visible = true;//Val.ToInt(DRow["IS_VISIBLE"]).Equals(1) == true ? true : false;
                            }
                            else
                            {
                                item2.Visible = false;
                            }
                            if (Val.ToInt(DRow["tab_index"]) != 0)
                            {
                                item2.TabStop = true;
                            }
                            item2.TabIndex = Val.ToInt(DRow["tab_index"]);
                            item2.TabStop = true;
                            if (item2.TabIndex == 1)
                            {
                                if (item2.Parent != null)
                                    item2.Parent.Focus();
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
                        else
                        {
                            item2.TabStop = false;
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            else
            {
                try
                {
                    if (Val.ToBooleanToInt(DRow["is_visible"]).Equals(1))
                    {
                        if (item1.Name.ToString() == Val.ToString(DRow["column_name"]))
                        {
                            item1.Text = (Val.ToBooleanToInt(DRow["is_compulsory"]).Equals(0) ? Val.ToString(DRow["caption"]) : "* " + Val.ToString(DRow["caption"]));
                            if (Val.ToBooleanToInt(DRow["is_visible"]).Equals(1))
                            {
                                item1.Visible = true;//Val.ToInt(DRow["IS_VISIBLE"]).Equals(1) == true ? true : false;
                            }
                            else
                            {
                                item1.Visible = false;
                            }
                            item1.TabIndex = Val.ToInt(DRow["tab_index"]);
                            if (item1.TabIndex == 1)
                            {
                                item1.Focus();
                            }
                            if (Val.ToBooleanToInt(DRow["is_editable"]).Equals(1))
                            {
                                item1.Enabled = true;
                            }
                            else
                            {
                                item1.Enabled = false;
                            }
                        }
                    }
                    else
                    {
                        item1.TabStop = false;
                    }
                }
                catch
                {
                    //continue;
                }
            }
        }

        public static Dictionary<Control, string> CheckCompulsoryControls(int FormCode, Form pForm)
        {
            Dictionary<Control, string> rtnCtrls = new Dictionary<Control, string>();
            Validation Val = new Validation();
            Single_Setting ObjSingleSettings = new Single_Setting();
            Single_SettingProperty Property = new Single_SettingProperty();

            Property.role_id = Val.ToInt(BLL.GlobalDec.gEmployeeProperty.role_id);
            Property.form_id = Val.ToInt(FormCode);
            DataTable DtColSetting = ObjSingleSettings.GetData(Property);

            var filter = (from DataRow dr in DtColSetting.Rows
                          where Val.ToBooleanToInt(dr["is_compulsory"]).ToString() == "1" && Val.ToBooleanToInt(dr["is_visible"]).ToString() == "1" && (dr["column_type"].ToString().ToUpper() == "TEXTBOX" ||
                           dr["column_type"].ToString().ToUpper() == "RADIOBUTTON" || dr["column_type"].ToString().ToUpper() == "CHECKBOX" ||
                           dr["column_type"].ToString().ToUpper() == "GRDCOLUMN" || dr["column_type"].ToString().ToUpper() == "DATETIMEPICKER" ||
                           dr["column_type"].ToString().ToUpper() == "COMBOBOX")
                          select dr);
            if (filter.Any())
            {
                DataTable DtFilterColSetting = filter.CopyToDataTable();
                Dictionary<Control, string> ctrls = new Dictionary<Control, string>();

                LoopControls(ctrls, pForm.Controls);
                foreach (KeyValuePair<Control, string> entry in ctrls)
                {
                    // do something with entry.Value or entry.Key
                    if (entry.Key is TextBox || entry.Key is DevExpress.XtraEditors.LookUpEdit || entry.Key is DevExpress.XtraEditors.TextEdit || entry.Key is System.Windows.Forms.RadioButton || entry.Key is Spire.Xls.RadioButton || entry.Key is CheckBox || entry.Key is DevExpress.XtraGrid.Columns.GridColumn || entry.Key is DateTimePicker || entry.Key is DevExpress.XtraEditors.ComboBox)
                    {
                        for (int i = 0; i < DtFilterColSetting.Rows.Count; i++)
                        {
                            if (entry.Key.Name.ToString().ToUpper() == DtFilterColSetting.Rows[i]["column_name"].ToString().ToUpper())
                            {
                                if (entry.Key.Text.Length == 0)
                                {
                                    rtnCtrls.Add(entry.Key, DtFilterColSetting.Rows[i]["caption"].ToString());
                                    //return DtFilterColSetting.Rows[i];
                                    //return rtnCtrls;
                                }
                            }
                        }
                    }
                }
            }
            return rtnCtrls;
        }

        private static void LoopControls(Dictionary<Control, string> ctrls, Control.ControlCollection controls)
        {
            foreach (Control control in controls.Cast<Control>().OrderBy(b => b.TabIndex))
            {
                ctrls.Add(control, control.Name);
                if (control.Controls.Count > 0)
                    LoopControls(ctrls, control.Controls);
            }
        }

        public static void Export(string format, DevExpress.XtraPivotGrid.PivotGridControl gvExportGrid)
        {
            try
            {
                string dlgHeader = string.Empty;
                string dlgFilter = string.Empty;
                format = format.ToLower();
                if (format.Equals(Exports.xls.ToString()))
                {
                    dlgHeader = "Export to Excel";
                    dlgFilter = "Excel files 97-2003 (*.xls)|*.xls|Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                }
                else if (format.Equals(Exports.pdf.ToString()))
                {
                    dlgHeader = "Export to PDF";
                    dlgFilter = "PDF (*.PDF)|*.PDF";
                }
                else if (format.Equals(Exports.rtf.ToString()))
                {
                    dlgHeader = "Export to RTF";
                    dlgFilter = "Word (*.doc) |*.doc;*.rtf|(*.txt) |*.txt|(*.*) |*.*";
                }
                else if (format.Equals(Exports.txt.ToString()))
                {
                    dlgHeader = "Export to Text";
                    dlgFilter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                }
                else if (format.Equals(Exports.html.ToString()))
                {
                    dlgHeader = "Export to HTML";
                    dlgFilter = "Html files (*.html)|*.html|Htm files (*.htm)|*.htm";
                }
                else if (format.Equals(Exports.csv.ToString()))
                {
                    dlgHeader = "Export to CSV";
                    dlgFilter = "csv (*.csv)|*.csv";
                }

                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = format;
                svDialog.Title = dlgHeader;
                svDialog.FileName = "Report";
                svDialog.Filter = dlgFilter;
                string Filepath = string.Empty;
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    Filepath = svDialog.FileName;
                    switch (format)
                    {
                        case "pdf":
                            gvExportGrid.OptionsPrint.PageSettings.Landscape = true;
                            gvExportGrid.OptionsPrint.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A3;
                            gvExportGrid.ExportToPdf(Filepath);
                            break;
                        case "xls":
                            gvExportGrid.ExportToXls(Filepath);
                            break;
                        case "rtf":
                            gvExportGrid.ExportToRtf(Filepath);
                            break;
                        case "txt":
                            gvExportGrid.ExportToText(Filepath);
                            break;
                        case "html":
                            gvExportGrid.ExportToHtml(Filepath);
                            break;
                    }
                }
                if (format.Equals(Exports.xls.ToString()))
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
                    if (Global.Confirm("Export Done\n\nYou Want To Open Excel File ?", "DERP", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(Filepath);
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        public static DataTable GetCSVObjList(string filename)
        {
            DataTable DTab = new DataTable();

            string[] Strline = File.ReadAllLines(filename);
            if (Strline.Count() == 0)
            {
                return DTab;
            }

            int n = 1;
            string[] Columns = Strline[0].Split(new char[] { ',' });
            foreach (string Str in Columns)
            {
                if (DTab.Columns.Contains("F" + n.ToString()) == false)
                {
                    DTab.Columns.Add("F" + n.ToString());
                }
                n++;
            }

            foreach (string line in Strline)
            {
                n = 1;
                string[] fields = line.Split(new char[] { ',' });

                DataRow DROw = DTab.NewRow();
                foreach (string Str in fields)
                {
                    DROw["F" + n.ToString()] = fields[n - 1];
                    n++;
                }
                DTab.Rows.Add(DROw);
            }
            return DTab;
        }

        public static double FindHHMMFormat(double DouValue)
        {
            BLL.Validation Val = new BLL.Validation();
            double i1 = 0;
            double i2 = DouValue;

            while (i2 >= 60)
            {
                i1 = i1 + 1;
                i2 = i2 - 60;
            }
            double answer = Val.Val(i1.ToString() + "." + ((i2 < 10) == true ? "0" + i2.ToString() : i2.ToString()));
            return answer;
        }
        public static string KeycodeToChar(KeyEventArgs e)
        {
            string output = "";
            switch (e.KeyCode)
            {
                case Keys.D0:
                case Keys.D1:
                case Keys.D2:
                case Keys.D3:
                case Keys.D4:
                case Keys.D5:
                case Keys.D6:
                case Keys.D7:
                case Keys.D8:
                case Keys.D9:
                    output += Strings.Chr(e.KeyValue);
                    break;
                case Keys.NumPad0:
                case Keys.NumPad1:
                case Keys.NumPad2:
                case Keys.NumPad3:
                case Keys.NumPad4:
                case Keys.NumPad5:
                case Keys.NumPad6:
                case Keys.NumPad7:
                case Keys.NumPad8:
                case Keys.NumPad9:
                    output += Strings.Chr(e.KeyValue - 48);
                    break;
                default:
                    output += Strings.Chr(e.KeyValue);
                    break;
            }
            return output;
        }

        public static String GetTimestamp()
        {
            DateTime DT = DateTime.Now;
            return DT.ToString("yyyyMMddHHmmssfffff");
        }
        public static String GetMonthName(int pIntMonthNo)
        {
            if (pIntMonthNo == 1) return "Jan";
            else if (pIntMonthNo == 2) return "Feb";
            else if (pIntMonthNo == 3) return "Mar";
            else if (pIntMonthNo == 4) return "Apr";
            else if (pIntMonthNo == 5) return "May";
            else if (pIntMonthNo == 6) return "Jun";
            else if (pIntMonthNo == 7) return "Jul";
            else if (pIntMonthNo == 8) return "Aug";
            else if (pIntMonthNo == 9) return "Sep";
            else if (pIntMonthNo == 10) return "Oct";
            else if (pIntMonthNo == 11) return "Nov";
            else if (pIntMonthNo == 12) return "Dec";
            else return "";
        }
        public static Int32 GetMonthCode(string pStrMonthName)
        {
            if (pStrMonthName.ToUpper() == "JAN") return 1;
            else if (pStrMonthName.ToUpper() == "FEB") return 2;
            else if (pStrMonthName.ToUpper() == "MAR") return 3;
            else if (pStrMonthName.ToUpper() == "APR") return 4;
            else if (pStrMonthName.ToUpper() == "MAY") return 5;
            else if (pStrMonthName.ToUpper() == "JUN") return 6;
            else if (pStrMonthName.ToUpper() == "JUL") return 7;
            else if (pStrMonthName.ToUpper() == "AUG") return 8;
            else if (pStrMonthName.ToUpper() == "SEP") return 9;
            else if (pStrMonthName.ToUpper() == "OCT") return 10;
            else if (pStrMonthName.ToUpper() == "NOV") return 11;
            else if (pStrMonthName.ToUpper() == "DEC") return 12;
            else return 0;
        }

        public static bool BarcodePrint(string pStrBarcode)
        {
            try
            {
                string fileLoc = Application.StartupPath + "\\Barcode.txt";
                FileStream fs = null;

                if (File.Exists(fileLoc) == true)
                {
                    File.Delete(fileLoc);
                }
                if ((!System.IO.File.Exists(fileLoc)))
                {
                    fs = System.IO.File.Create(fileLoc);
                    using (fs)
                    {

                    }
                    StreamWriter sw = new StreamWriter(fileLoc);
                    using (sw)
                    {

                        sw.WriteLine("G0");
                        sw.WriteLine("n");
                        sw.WriteLine("M0500");
                        sw.WriteLine("O0214");
                        sw.WriteLine("V0");
                        sw.WriteLine("t1");
                        sw.WriteLine("Kf0070");
                        sw.WriteLine("SG");
                        sw.WriteLine("L");
                        sw.WriteLine("D11");
                        sw.WriteLine("H19");
                        sw.WriteLine("PG");
                        sw.WriteLine("pG");
                        sw.WriteLine("SG");
                        sw.WriteLine("A2");
                        sw.WriteLine("1e4202700250013B" + pStrBarcode);
                        sw.WriteLine("ySPM");
                        sw.WriteLine("1911A1000100040" + pStrBarcode);
                        sw.WriteLine("Q0001");
                        sw.WriteLine("E");
                    }

                    if (File.Exists(Application.StartupPath + "\\PRINTBARCODE.BAT") && File.Exists(fileLoc))
                    {
                    }
                    sw.Close();
                    sw.Dispose();
                    sw = null;
                }
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message);
            }
            return true;
        }

        public static Boolean CheckFormVisible(Form pfrm)
        {
            if (pfrm == null)
            {
                return false;
            }
            else if (pfrm.IsHandleCreated == false)
            {
                return false;
            }
            else
            {
                pfrm.Focus();
                return true;
            }
        }

        public static bool OnKeyPressToOpenPopup(KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.F1)
            {
                return true;
            }
            if (Keys.Control == Control.ModifierKeys
                || Keys.Alt == Control.ModifierKeys
                || Keys.Shift == Control.ModifierKeys
                )
            {
                return false;
            }

            if (e.KeyChar != (Char)Keys.Enter
                && e.KeyChar != (Char)Keys.Back
                && e.KeyChar != (Char)Keys.Escape
                && e.KeyChar != (Char)Keys.Delete
                && e.KeyChar != (Char)Keys.Left
                && e.KeyChar != (Char)Keys.Right
                && e.KeyChar != (Char)Keys.Up
                )
            {
                return true;
            }
            return false;
        }

        public static byte[] ImageToByte(Image pImage)
        {
            byte[] byteArray = new byte[0];
            using (MemoryStream stream = new MemoryStream())
            {
                pImage.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                stream.Close();

                byteArray = stream.ToArray();
            }
            return byteArray;
        }

        public static byte[] ImageToByte(Bitmap pImage)
        {
            byte[] byteArray = new byte[0];
            using (MemoryStream stream = new MemoryStream())
            {
                pImage.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                stream.Close();

                byteArray = stream.ToArray();
            }
            return byteArray;
        }

        public static Bitmap ByteToImage(byte[] blob)
        {
            MemoryStream mStream = new MemoryStream();
            byte[] pData = blob;
            mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
            Bitmap bm = new Bitmap(mStream, false);
            mStream.Dispose();
            return bm;
        }

        public static void OpenForm(string pStrFormName, string pStrReportCode, string pStrNamespace = "Report")
        {
            try
            {
                Form objForm = new Form();
                string FullTypeName;
                Type FormInstanceType;
                FullTypeName = (Application.ProductName + ("." + pStrNamespace + "." + pStrFormName));
                FormInstanceType = Type.GetType(FullTypeName, true, true);
                objForm = ((Form)(Activator.CreateInstance(FormInstanceType)));
                objForm.MdiParent = gMainFormRef;
                objForm.Tag = pStrReportCode;
                objForm.Show();
            }
            catch
            {
                Message(pStrFormName + " :  Form Not Exists");
            }
        }

        public static string GetFinancialYear(string pStrDate)
        {
            BLL.Validation Val = new BLL.Validation();
            string StrReturn = "";
            DateTime DT = DateTime.Parse(pStrDate);
            if (DT.Month >= 4 && DT.Month <= 12)
            {
                StrReturn = DT.Year.ToString() + "-" + Val.Right((DT.Year + 1).ToString(), 2);
            }
            else
            {
                StrReturn = (DT.Year - 1).ToString() + "-" + Val.Right((DT.Year).ToString(), 2);
            }
            Val = null;
            return StrReturn;
        }

        public static string GetFinancialYearNew(string pStrDate)
        {
            BLL.Validation Val = new BLL.Validation();
            string StrReturn = "";
            DateTime DT = DateTime.Parse(pStrDate);
            if (DT.Month >= 1 && DT.Month <= 9)
            {
                StrReturn = DT.Year.ToString() + "0" + Val.Right(DT.Month.ToString(), 2);
            }
            else
            {
                StrReturn = DT.Year.ToString() + "" + Val.Right(DT.Month.ToString(), 2);
            }
            Val = null;
            return StrReturn;
        }

        public static string GetFinancialYearFullFormat(string pStrDate)
        {
            BLL.Validation Val = new BLL.Validation();
            string StrReturn = "";
            DateTime DT = DateTime.Parse(pStrDate);
            if (DT.Month >= 4 && DT.Month <= 12)
            {
                StrReturn = DT.Year.ToString() + "-" + (DT.Year + 1).ToString();
            }
            else
            {
                StrReturn = (DT.Year - 1).ToString() + "-" + (DT.Year).ToString();
            }
            Val = null;
            return StrReturn;
        }

        public static string RemoveDuplicateFromString(string pStrMainString, char SplitChar = ' ', string Seperator = " ")
        {
            return string.Join(Seperator, pStrMainString.Split(SplitChar).Distinct());
        }

        public static string DataGridExportToExcel(DevExpress.XtraPivotGrid.PivotGridControl pDataGFrid, string StrFileName)
        {
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Blue");

            string StrFilePath = Application.StartupPath + "\\" + StrFileName + ".xls";

            if (File.Exists(StrFilePath))
            {
                File.Delete(StrFilePath);
            }

            pDataGFrid.ExportToXls(StrFilePath);
            return StrFilePath;
        }

        public static string DataGridExportToExcel(DevExpress.XtraGrid.Views.Grid.GridView pDataGrid, string StrFileName)
        {
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Blue");

            string StrFilePath = Application.StartupPath + "\\" + StrFileName + ".xls";

            if (File.Exists(StrFilePath))
            {
                File.Delete(StrFilePath);
            }

            if (pDataGrid.DataRowCount == 0) return "";
            DevExpress.XtraPrinting.XlsExportOptions options = new DevExpress.XtraPrinting.XlsExportOptions { SheetName = StrFileName, Suppress65536RowsWarning = false };

            pDataGrid.ExportToXls(StrFilePath, options);
            return StrFilePath;
        }

        public static string ExportFormAsImage(Form pThis, string StrFileName)
        {
            string StrFilePath = string.Empty;
            try
            {
                StrFilePath = Application.StartupPath + "\\" + StrFileName + ".jpg";
                Graphics g1 = pThis.CreateGraphics();
                Image MyImage = new Bitmap(pThis.ClientRectangle.Width, pThis.ClientRectangle.Height, g1);
                Graphics g2 = Graphics.FromImage(MyImage);
                IntPtr dc1 = g1.GetHdc();
                IntPtr dc2 = g2.GetHdc();
                BitBlt(dc2, 0, 0, pThis.ClientRectangle.Width, pThis.ClientRectangle.Height, dc1, 0, 0, 13369376);
                g1.ReleaseHdc(dc1);
                g2.ReleaseHdc(dc2);
                MyImage.Save(StrFilePath, ImageFormat.Jpeg);
                FileStream fileStream = new FileStream(StrFilePath, FileMode.Open, FileAccess.Read);

                fileStream.Close();
                fileStream.Dispose();
                fileStream = null;
                g2.Dispose();
                g2 = null;
                MyImage.Dispose();
                MyImage = null;
            }
            catch (Exception)
            {
            }
            return StrFilePath;
        }

        public static double NumberConversion(double pDouNumber, string pToNumberFormat)
        {
            BLL.Validation Val = new BLL.Validation();
            double DouAnswer = 0;
            if (pToNumberFormat == "HUNDREDS")
            {
                DouAnswer = Val.Val(pDouNumber / 100);
            }
            else if (pToNumberFormat == "THOUSANDS")
            {
                DouAnswer = Val.Val(pDouNumber / 1000);
            }
            else if (pToNumberFormat == "LAKHS")
            {
                DouAnswer = Val.Val(pDouNumber / 100000);
            }
            else if (pToNumberFormat == "MILLIONS")
            {
                DouAnswer = Val.Val(pDouNumber / 10000000);
            }
            else if (pToNumberFormat == "CRORES")
            {
                DouAnswer = Val.Val(pDouNumber / 10000000);
            }
            else if (pToNumberFormat == "BILLIONS")
            {
                DouAnswer = Val.Val(pDouNumber / 1000000000);
            }
            else if (pToNumberFormat == "TRILLIONS")
            {
                DouAnswer = Val.Val(pDouNumber / 1000000000000);
            }
            else
            {
                DouAnswer = pDouNumber;
            }
            Val = null;
            return DouAnswer;
        }

        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }
            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }
            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }
            return words;
        }

        public static DataSet ImportExcelXLS(string FileName, bool hasHeaders, int IntIMEX = 0)
        {
            string HDR = hasHeaders ? "Yes" : "No";
            // string HDR = "No";
            string strConn;
            if (FileName.Substring(FileName.LastIndexOf('.')).ToLower() == ".xlsx")
                //    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=1\"";
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=" + IntIMEX + "\"";

            else
                //strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FileName + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=0\"";
                strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FileName + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=" + IntIMEX + "\"";

            DataSet output = new DataSet();

            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                DataTable schemaTable = conn.GetOleDbSchemaTable(
                    OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                foreach (DataRow schemaRow in schemaTable.Rows)
                {
                    string sheet = schemaRow["TABLE_NAME"].ToString();

                    if (!sheet.EndsWith("_"))
                    {
                        try
                        {
                            OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + sheet + "]", conn);
                            cmd.CommandType = CommandType.Text;

                            DataTable outputTable = new DataTable(sheet);
                            output.Tables.Add(outputTable);
                            new OleDbDataAdapter(cmd).Fill(outputTable);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message + string.Format("Sheet:{0}.File:F{1}", sheet, FileName), ex);
                        }
                    }
                }
            }
            return output;
        }

        public static DataTable ImportExcelXLSWithSheetName(string FileName, bool hasHeaders, string SheetName, int IntIMEX = 0)
        {
            string HDR = hasHeaders ? "Yes" : "No";
            // string HDR = "No";
            string strConn;
            if (FileName.Substring(FileName.LastIndexOf('.')).ToLower() == ".xlsx")
                //    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=1\"";
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=" + IntIMEX + "\"";

            else
                //strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FileName + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=0\"";
                strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FileName + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=" + IntIMEX + "\"";

            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                DataTable schemaTable = new DataTable("Temp");
                try
                {
                    OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + SheetName + "]", conn);
                    cmd.CommandType = CommandType.Text;

                    new OleDbDataAdapter(cmd).Fill(schemaTable);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + string.Format("Sheet:{0}.File:F{1}", SheetName, FileName), ex);
                }
                return schemaTable;
            }
        }

        public static string GetSplitedCriteriaForReport(string PStr)
        {
            BLL.Validation Val = new BLL.Validation();
            string ResStr = "";

            if (PStr == "")
            {
                ResStr = "";
            }
            else
            {
                foreach (string str in PStr.Split(','))
                {
                    ResStr += "'" + Val.Trim(str) + "',";
                }

                if (ResStr.Length != 0)
                {
                    ResStr = ResStr.Substring(0, ResStr.Length - 1);
                }
            }
            Val = null;
            return ResStr;
        }

        public static string FindDifferenceGroup(double ConsPer, double ReceivePer)
        {
            double DouDiff = ConsPer - ReceivePer;

            if (Math.Round(DouDiff, 2) >= 10)
                return "A. MORE THAN 10";
            else if (Math.Round(DouDiff, 2) <= 9.99 && Math.Round(DouDiff, 2) >= 9)
                return "B. 9 to 9.99";
            else if (Math.Round(DouDiff, 2) <= 8.99 && Math.Round(DouDiff, 2) >= 8)
                return "C. 8 to 8.99";
            else if (Math.Round(DouDiff, 2) <= 7.99 && Math.Round(DouDiff, 2) >= 7)
                return "D. 7 to 7.99";
            else if (Math.Round(DouDiff, 2) <= 6.99 && Math.Round(DouDiff, 2) >= 6)
                return "E. 6 to 6.99";
            else if (Math.Round(DouDiff, 2) <= 5.99 && Math.Round(DouDiff, 2) >= 5)
                return "F. 5 to 5.99";
            else if (Math.Round(DouDiff, 2) <= 4.99 && Math.Round(DouDiff, 2) >= 4)
                return "G. 4 to 4.99";
            else if (Math.Round(DouDiff, 2) <= 3.99 && Math.Round(DouDiff, 2) >= 3)
                return "H. 3 to 3.99";
            else if (Math.Round(DouDiff, 2) <= 2.99 && Math.Round(DouDiff, 2) >= 2)
                return "I. 2 to 2.99";
            else if (Math.Round(DouDiff, 2) <= 1.99 && Math.Round(DouDiff, 2) >= 1)
                return "J. 1 to 1.99";
            else if (Math.Round(DouDiff, 2) <= 0.99 && Math.Round(DouDiff, 2) >= 0)
                return "K. 0 to 0.99";
            else if (Math.Round(DouDiff, 2) <= -0.01 && Math.Round(DouDiff, 2) >= -0.99)
                return "L. -0.01 to -0.99";
            else if (Math.Round(DouDiff, 2) <= -1 && Math.Round(DouDiff, 2) >= -1.99)
                return "M. -1 to -1.99";
            else if (Math.Round(DouDiff, 2) <= -2 && Math.Round(DouDiff, 2) >= -2.99)
                return "N. -2 to -2.99";
            else if (Math.Round(DouDiff, 2) <= -3 && Math.Round(DouDiff, 2) >= -3.99)
                return "O. -3 to -3.99";
            else if (Math.Round(DouDiff, 2) <= -4 && Math.Round(DouDiff, 2) >= -4.99)
                return "P. -4 to -4.99";
            else if (Math.Round(DouDiff, 2) <= -5 && Math.Round(DouDiff, 2) >= -5.99)
                return "Q. -5 to -5.99";
            else if (Math.Round(DouDiff, 2) <= -6 && Math.Round(DouDiff, 2) >= -6.99)
                return "R. -6 to -6.99";
            else if (Math.Round(DouDiff, 2) <= -7 && Math.Round(DouDiff, 2) >= -7.99)
                return "S. -7 to -7.99";
            else if (Math.Round(DouDiff, 2) <= -8 && Math.Round(DouDiff, 2) >= -8.99)
                return "T. -8 to -8.99";
            else if (Math.Round(DouDiff, 2) <= -9 && Math.Round(DouDiff, 2) >= -9.99)
                return "U. -9 to -9.99";
            else if (Math.Round(DouDiff, 2) <= -10)
                return "V. MORE THAN -10";
            else
                return "";
        }

        #endregion

        #region Temporary Table

        public static DataTable DTabStockFlag()
        {
            DataTable DTab = new DataTable("StatusType");
            DTab.Columns.Add("STOCKFLAG");
            DTab.Columns.Add("STOCKFLAG1");
            DTab.Rows.Add("STOCK3", "STOCK3");
            DTab.Rows.Add("STOCK4", "STOCK4");
            return DTab;
        }

        public static DataTable DTabDiscription()
        {
            DataTable DTab = new DataTable("StatusType");

            DTab.Columns.Add("DESCRIPTION");
            DTab.Columns.Add("DESCRIPTION1");
            DTab.Rows.Add(".", ".");
            DTab.Rows.Add("B", "B");
            DTab.Rows.Add("D", "D");
            DTab.Rows.Add("E", "E");
            DTab.Rows.Add("F", "F");
            DTab.Rows.Add("FANCY", "FANCY");
            DTab.Rows.Add("G", "G");
            DTab.Rows.Add("H", "H");
            DTab.Rows.Add("I", "I");
            DTab.Rows.Add("J", "J");
            DTab.Rows.Add("K", "K");
            DTab.Rows.Add("L", "L");
            DTab.Rows.Add("M", "M");
            DTab.Rows.Add("N", "N");
            DTab.Rows.Add("N.A", "N.A");
            DTab.Rows.Add("O", "O");
            DTab.Rows.Add("OVAL", "OVAL");
            DTab.Rows.Add("P", "P");
            DTab.Rows.Add("TLB", "TLB");
            DTab.Rows.Add("TTLB", "TTLB");
            DTab.Rows.Add("WHITE", "WHITE");
            return DTab;
        }

        public static DataTable DTabMonth()
        {
            DataTable DTab = new DataTable("Month");
            DTab.Columns.Add("Month");
            DTab.Columns.Add("SrNo");
            DTab.Rows.Add("January", "1");
            DTab.Rows.Add("February", "2");
            DTab.Rows.Add("March", "3");
            DTab.Rows.Add("April", "4");
            DTab.Rows.Add("May", "5");
            DTab.Rows.Add("June", "6");
            DTab.Rows.Add("July", "7");
            DTab.Rows.Add("August", "8");
            DTab.Rows.Add("September", "9");
            DTab.Rows.Add("October", "10");
            DTab.Rows.Add("November", "11");
            DTab.Rows.Add("Decembar", "12");
            return DTab;
        }

        public static DataTable DTabDealType()
        {
            DataTable DTab = new DataTable("Month");
            DTab.Columns.Add("DEALTYPE");
            DTab.Columns.Add("DEALTYPE1");
            DTab.Rows.Add("NOT-APPLICABLE", "NOT-APPLICABLE");
            DTab.Rows.Add("CONTRACT", "CONTRACT");
            DTab.Rows.Add("NON-CONTRACT", "NON-CONTRACT");
            return DTab;
        }

        public static DataTable DTabRelation()
        {
            DataTable DTab = new DataTable("Month");
            DTab.Columns.Add("Relation");
            DTab.Rows.Add("GRAND-FATHER");
            DTab.Rows.Add("GRAND-MONTHER");
            DTab.Rows.Add("FATHER");
            DTab.Rows.Add("MOTHER");
            DTab.Rows.Add("ELDER BROTHER");
            DTab.Rows.Add("YOUNGER BROTHER");
            DTab.Rows.Add("BROTHER'S WIFE");
            DTab.Rows.Add("SISTER");
            DTab.Rows.Add("HUSBAND");
            DTab.Rows.Add("WIFE");
            DTab.Rows.Add("UNCLE");
            DTab.Rows.Add("ANTY");
            DTab.Rows.Add("SON");
            DTab.Rows.Add("DAUGHTER");
            DTab.Rows.Add("GRANDSON");
            DTab.Rows.Add("GRANDDAUGHTER");
            return DTab;
        }

        public static DataTable DTabNumericFormat()
        {
            DataTable DTab = new DataTable("");
            DTab.Columns.Add("FORMAT");
            DTab.Rows.Add("N0");
            DTab.Rows.Add("N1");
            DTab.Rows.Add("N2");
            DTab.Rows.Add("N3");
            DTab.Rows.Add("N4");
            DTab.Rows.Add("N5");
            DTab.Rows.Add("N6");
            DTab.Rows.Add("N7");
            DTab.Rows.Add("N8");
            DTab.Rows.Add("N9");
            DTab.Rows.Add("N10");
            return DTab;
        }

        public static DataTable DTabYESNO()
        {
            DataTable DTab = new DataTable("Month");
            DTab.Columns.Add("Option");
            DTab.Rows.Add("YES");
            DTab.Rows.Add("NO");
            return DTab;
        }

        public static DataTable DTabSawType()
        {
            DataTable DTab = new DataTable("Month");
            DTab.Columns.Add("Option");
            DTab.Columns.Add("Option1");
            DTab.Rows.Add("RR", "RR");
            DTab.Rows.Add("TOP-M", "TOP-M");
            DTab.Rows.Add("CENTRE-M", "CENTRE-M");
            DTab.Rows.Add("TOP-SS-DM", "TOP-SS-DM");
            DTab.Rows.Add("TOP-TSS-DM", "TOP-TSS-DM");
            DTab.Rows.Add("CENT-SS-DM", "CENT-SS-DM");
            DTab.Rows.Add("CENT-TSS-DM", "CENT-TSS-DM");
            DTab.Rows.Add("PIE-DM", "PIE-DM");
            DTab.Rows.Add("PLATE-M", "PLATE-M");
            DTab.Rows.Add("KERF", "KERF");
            DTab.Rows.Add("BRUTING-DM", "BRUTING-DM");
            DTab.Rows.Add("T1-DM", "T1-DM");
            DTab.Rows.Add("T2-DM", "T2-DM");
            DTab.Rows.Add("T3-DM", "T3-DM");
            DTab.Rows.Add("T4-DM", "T4-DM");
            DTab.Rows.Add("T5-DM", "T5-DM");
            DTab.Rows.Add("T1-M", "T1-M");
            DTab.Rows.Add("T2-M", "T2-M");
            DTab.Rows.Add("T3-M", "T3-M");
            DTab.Rows.Add("T4-M", "T4-M");
            DTab.Rows.Add("T5-M", "T5-M");
            return DTab;
        }

        #endregion

        public static void HideGridColumn(int FormCode, DevExpress.XtraGrid.Views.Grid.GridView GrdGrid, String GridControlName = "")
        {
            BLL.Validation Val = new BLL.Validation();
            Single_Setting ObjSingleSettings = new Single_Setting();
            Single_SettingProperty Property = new Single_SettingProperty();

            Property.role_id = Val.ToInt(BLL.GlobalDec.gEmployeeProperty.role_id);
            Property.form_id = Val.ToInt(FormCode);
            DataTable DtColSetting = ObjSingleSettings.GetData(Property);

            if (GridControlName != "")
            {
                DtColSetting = DtColSetting.Select("column_type='grdcolumn' AND grid_name = '" + GridControlName.ToUpper() + "'").CopyToDataTable();
            }
            else
            {
                if (DtColSetting.Select("column_type='grdcolumn' AND grid_name = '" + GrdGrid.Name.ToUpper() + "'").Length > 0)
                {
                    DtColSetting = DtColSetting.Select("column_type='grdcolumn' AND grid_name = '" + GrdGrid.Name.ToUpper() + "'").CopyToDataTable();
                }
            }
            List<int> orderList = new List<int>();
            int IntI = 0;

            foreach (DataRow DRow in DtColSetting.Rows)
            {
                try
                {
                    GrdGrid.Columns[Val.ToString(DRow["column_name"])].Visible = false;

                    if (GridControlName != "")
                    {
                        //try
                        //{
                        //    if (Val.ToInt(DRow["is_compulsory"]).Equals(0))
                        //    {
                        //        if (Val.ToString(DRow["USER_PREFER_CURRENCY"]).ToUpper() == "N")
                        //            GrdGrid.Columns[Val.ToString(DRow["COLUMN_NAME"])].Caption = Val.ToString(DRow["CAPTION"]);
                        //        else if (Val.ToString(DRow["USER_PREFER_CURRENCY"]).ToUpper() == "M")
                        //            GrdGrid.Columns[Val.ToString(DRow["COLUMN_NAME"])].Caption = String.Format(Val.ToString(DRow["CAPTION"]), BLL.GlobalDec.gEmployeeProperty.gMain_currency_Symbol);
                        //        else if (Val.ToString(DRow["USER_PREFER_CURRENCY"]).ToUpper() == "S")
                        //            GrdGrid.Columns[Val.ToString(DRow["COLUMN_NAME"])].Caption = String.Format(Val.ToString(DRow["CAPTION"]), BLL.GlobalDec.gEmployeeProperty.gSecond_currency_Symbol);
                        //    }
                        //    else
                        //    {
                        //        if (Val.ToString(DRow["USER_PREFER_CURRENCY"]).ToUpper() == "N")
                        //            GrdGrid.Columns[Val.ToString(DRow["COLUMN_NAME"])].Caption = Val.ToInt(DRow["IS_COMPULSORY"]).Equals(0) ? Val.ToString(DRow["CAPTION"]) : "* " + Val.ToString(DRow["CAPTION"]);
                        //        else if (Val.ToString(DRow["USER_PREFER_CURRENCY"]).ToUpper() == "M")
                        //            GrdGrid.Columns[Val.ToString(DRow["COLUMN_NAME"])].Caption = String.Format("* " + Val.ToString(DRow["CAPTION"]), BLL.GlobalDec.gEmployeeProperty.gMain_currency_Symbol);
                        //        else if (Val.ToString(DRow["USER_PREFER_CURRENCY"]).ToUpper() == "S")
                        //            GrdGrid.Columns[Val.ToString(DRow["COLUMN_NAME"])].Caption = String.Format("* " + Val.ToString(DRow["CAPTION"]), BLL.GlobalDec.gEmployeeProperty.gSecond_currency_Symbol);
                        //    }
                        //}
                        //catch (Exception ex)
                        //{ }
                    }
                    else
                    {
                        GrdGrid.Columns[Val.ToString(DRow["column_name"])].Caption = (Val.ToInt(DRow["is_compulsory"]).Equals(0) ? Val.ToString(DRow["caption"]) : "* " + Val.ToString(DRow["caption"]));
                    }

                    if (Val.ToInt(DRow["is_visible"]).Equals(1))
                    {
                        GrdGrid.Columns[Val.ToString(DRow["column_name"])].Visible = true;

                        if (GridControlName != "")
                        {
                            GrdGrid.Columns[Val.ToString(DRow["column_name"])].VisibleIndex = Val.ToInt(DRow["tab_index"]);
                        }
                        else
                        {
                            GrdGrid.Columns[Val.ToString(DRow["column_name"])].VisibleIndex = IntI;
                        }

                        GrdGrid.Columns[Val.ToString(DRow["column_name"])].OptionsColumn.AllowEdit = Val.ToInt32(DRow["is_editable"]) == 1 ? true : false;
                        IntI++;
                    }
                }
                catch
                {
                    continue;
                }
            }
        }

        public static DialogResult Confirm(string pStrText, string pStrTitle = "", MessageBoxButtons btns = MessageBoxButtons.OK, MessageBoxIcon pMessageBoxIcon = MessageBoxIcon.Information)
        {
            if (pStrTitle == "")
            {
                pStrTitle = BLL.GlobalDec.gStrMsgTitle;
            }
            return MessageBox.Show(pStrText, pStrTitle, btns, pMessageBoxIcon, MessageBoxDefaultButton.Button1);
        }

        public static DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();
            // column names 
            PropertyInfo[] oProps = null;
            if (varlist == null) return dtReturn;
            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow 
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;
                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }
                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }
                DataRow dr = dtReturn.NewRow();
                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }
                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }
        public static Boolean CheckIPValid(String strIP)
        {
            //  Split string by ".", check that array length is 3
            char chrFullStop = '.';
            string[] arrOctets = strIP.Split(chrFullStop);
            if (arrOctets.Length != 4)
            {
                return false;
            }
            //  Check each substring checking that the int value is less than 255 and that is char[] length is !> 2
            Int16 MAXVALUE = 255;
            Int32 temp; // Parse returns Int32
            foreach (String strOctet in arrOctets)
            {
                if (strOctet.Length > 3)
                {
                    return false;
                }

                temp = int.Parse(strOctet);
                if (temp > MAXVALUE)
                {
                    return false;
                }
            }
            return true;
        }

        public static DataTable GetDate()
        {
            InterfaceLayer Ope = new InterfaceLayer();
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.TRN_OpeningDate_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@company_id", BLL.GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", BLL.GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", BLL.GlobalDec.gEmployeeProperty.location_id, DbType.Int32);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }

        public static DataTable CurrencyType()
        {
            InterfaceLayer Ope = new InterfaceLayer();
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MST_Currency_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@active", 1, DbType.Int32);


            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public static DataTable CheckConfirmLot(Int64 LotId)
        {
            InterfaceLayer Ope = new InterfaceLayer();
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Check_Confirm_Lot;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@lot_id", LotId, DbType.Int64);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public static string CheckEstimationDoneOrNot(Int64 LotId)
        {
            Validation Val = new Validation();
            InterfaceLayer Ope = new InterfaceLayer();
            return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "MFG_TRN_Estimation", "lot_id", "AND lot_id = '" + LotId + "'"));
        }
        public static DataTable CheckConfirmJangedLot(Int64 LotId)
        {
            InterfaceLayer Ope = new InterfaceLayer();
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Check_Janged_Confirm_Lot;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@lot_id", LotId, DbType.Int64);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public static DataTable GetRoughCutAll()
        {
            InterfaceLayer Ope = new InterfaceLayer();
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_RoughCut_GetAllData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@company_id", BLL.GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", BLL.GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", BLL.GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", BLL.GlobalDec.gEmployeeProperty.department_id, DbType.Int32);


            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public static DataTable GetRoughKapanWise_Data(int KapanId = 0, Int64 LotId = 0)
        {
            InterfaceLayer Ope = new InterfaceLayer();
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_RoughCut_GetKapanWise;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@kapan_id", KapanId, DbType.Int32);
            Request.AddParams("@lot_id", LotId, DbType.Int64);
            Request.AddParams("@company_id", BLL.GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", BLL.GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", BLL.GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", BLL.GlobalDec.gEmployeeProperty.department_id, DbType.Int32);


            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public static DataTable GetRoughKapanWise_RejectionAvgData(Int64 KapanId = 0, Int64 LotId = 0)
        {
            InterfaceLayer Ope = new InterfaceLayer();
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.Get_Rough_Data_Kapanwise;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@kapan_id", KapanId, DbType.Int64);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public static DataTable GetRoughKapanPending_Data(Int64 KapanId = 0)
        {
            InterfaceLayer Ope = new InterfaceLayer();
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_PendingCarat_GetKapanWise;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@kapan_id", KapanId, DbType.Int64);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public static DataTable GetMFGRoughKapanPending_Data(Int64 KapanId = 0)
        {
            InterfaceLayer Ope = new InterfaceLayer();
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_PendingMFGCarat_GetKapanWise;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@kapan_id", KapanId, DbType.Int64);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public static DataTable GetRoughPurityWisePending_Data(Int64 PurityId = 0)
        {
            InterfaceLayer Ope = new InterfaceLayer();
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_PendingCarat_GetPurityWise;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@purity_id", PurityId, DbType.Int64);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public static DataTable GetRoughKapanWise(int KapanId = 0, Int64 LotId = 0)
        {
            InterfaceLayer Ope = new InterfaceLayer();
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_RoughCut_GetKapanWise;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@kapan_id", KapanId, DbType.Int32);
            Request.AddParams("@lot_id", LotId, DbType.Int64);
            Request.AddParams("@company_id", BLL.GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", BLL.GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", BLL.GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", BLL.GlobalDec.gEmployeeProperty.department_id, DbType.Int32);


            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);

            if (DTab.Rows.Count == 0)
            {
                Request Request1 = new Request();
                Request1.CommandText = BLL.TPV.SProc.MFG_TRN_RoughCut_GetStockData;
                Request1.CommandType = CommandType.StoredProcedure;
                Request1.AddParams("@kapan_id", KapanId, DbType.Int32);
                Request1.AddParams("@lot_id", LotId, DbType.Int32);
                Request1.AddParams("@company_id", BLL.GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
                Request1.AddParams("@branch_id", BLL.GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
                Request1.AddParams("@location_id", BLL.GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
                Request1.AddParams("@department_id", BLL.GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request1);
            }
            return DTab;
        }
        public static DataTable GetRoughStockWise(int KapanId = 0, Int64 LotId = 0)
        {
            InterfaceLayer Ope = new InterfaceLayer();
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_RoughCut_GetStockData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@kapan_id", KapanId, DbType.Int32);
            Request.AddParams("@lot_id", LotId, DbType.Int64);
            Request.AddParams("@company_id", BLL.GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", BLL.GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", BLL.GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", BLL.GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public static DataTable GetRoughJangedStockWise(int KapanId = 0, Int64 LotId = 0)
        {
            InterfaceLayer Ope = new InterfaceLayer();
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_RoughCut_GetJangedStockData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@kapan_id", KapanId, DbType.Int32);
            Request.AddParams("@lot_id", LotId, DbType.Int64);
            Request.AddParams("@company_id", BLL.GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", BLL.GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", BLL.GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", BLL.GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public static DataTable GetRoughJangedMainStockWise(int KapanId = 0, Int64 LotId = 0, string Type = "")
        {
            InterfaceLayer Ope = new InterfaceLayer();
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_RoughCut_GetMainStockData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@kapan_id", KapanId, DbType.Int32);
            Request.AddParams("@lot_id", LotId, DbType.Int64);
            Request.AddParams("@type", Type, DbType.String);
            Request.AddParams("@company_id", BLL.GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", BLL.GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", BLL.GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", BLL.GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }

        public static DataTable GetStockCutwise(int KapanId = 0, int CutId = 0, int flag = 0)
        {
            InterfaceLayer Ope = new InterfaceLayer();
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_StockData_Cutwise;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@kapan_id", KapanId, DbType.Int32);
            Request.AddParams("@cut_id", CutId, DbType.Int32);
            Request.AddParams("@company_id", BLL.GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", BLL.GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", BLL.GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", BLL.GlobalDec.gEmployeeProperty.department_id, DbType.Int32);
            Request.AddParams("@flag", flag, DbType.Int32);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }


        public static DataTable GetRoughStock(int KapanId = 0, Int64 LotId = 0)
        {
            InterfaceLayer Ope = new InterfaceLayer();
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_RoughCut_StockData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@kapan_id", KapanId, DbType.Int32);
            Request.AddParams("@lot_id", LotId, DbType.Int64);
            //Request.AddParams("@company_id", BLL.GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            //Request.AddParams("@branch_id", BLL.GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            //Request.AddParams("@location_id", BLL.GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            //Request.AddParams("@department_id", BLL.GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public static DataTable GetReportKapanWise(string KapanId)
        {
            InterfaceLayer Ope = new InterfaceLayer();
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.RPT_RoughCut_GetKapanWise;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@kapan_id", KapanId, DbType.String);
            Request.AddParams("@company_id", BLL.GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", BLL.GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", BLL.GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", BLL.GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public static DataTable GetReportProcessWise(string ProcessId)
        {
            InterfaceLayer Ope = new InterfaceLayer();
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.RPT_SubProcess_GetProcessWise;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@process_id", ProcessId, DbType.String);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public static DataTable GetReportKapanWise_LotNo(string Kapan_No)
        {
            InterfaceLayer Ope = new InterfaceLayer();
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.RPT_LotNo_GetKapanNoWise;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@kapan_no", Kapan_No, DbType.String);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public static DataTable GetKapanAll()
        {
            InterfaceLayer Ope = new InterfaceLayer();
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Kapan_GetAllData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@company_id", BLL.GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", BLL.GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", BLL.GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", BLL.GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public static DataTable GetKapanLottingAll()
        {
            InterfaceLayer Ope = new InterfaceLayer();
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Kapan_GetLottingData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@company_id", BLL.GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", BLL.GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", BLL.GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", BLL.GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public static DataTable GetSection(int active)
        {
            InterfaceLayer Ope = new InterfaceLayer();
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@active", active, DbType.Int32);

            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Section_GetData;
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public static DataTable GetKapanAll_Assort()
        {
            InterfaceLayer Ope = new InterfaceLayer();
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Kapan_GetAll_AssortData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@company_id", BLL.GlobalDec.gEmployeeProperty.company_id, DbType.Int32);
            Request.AddParams("@branch_id", BLL.GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            Request.AddParams("@location_id", BLL.GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            Request.AddParams("@department_id", BLL.GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public static DataTable ConfigSubprocess(string process, int userId)
        {
            InterfaceLayer Ope = new InterfaceLayer();
            DataTable DTab = new DataTable();
            Request Request = new Request();
            Request.CommandText = BLL.TPV.SProc.MFG_Config_Sub_Process_GetData;
            Request.CommandType = CommandType.StoredProcedure;
            Request.AddParams("@process_id", process, DbType.Int32);
            Request.AddParams("@user_id", userId, DbType.Int32);
            //Request.AddParams("@branch_id", BLL.GlobalDec.gEmployeeProperty.branch_id, DbType.Int32);
            //Request.AddParams("@location_id", BLL.GlobalDec.gEmployeeProperty.location_id, DbType.Int32);
            //Request.AddParams("@department_id", BLL.GlobalDec.gEmployeeProperty.department_id, DbType.Int32);

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            return DTab;
        }
        public static int ValidateDate(Int64 LotId, string date)
        {
            InterfaceLayer Ope = new InterfaceLayer();
            int flag = 0;
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@lot_id", LotId, DbType.Int64);
            Request.AddParams("@recieve_date", date, DbType.Date);
            Request.CommandText = BLL.TPV.SProc.MFG_TRN_Check_Janged_Val;
            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request);
            if (DTab.Rows.Count > 0)
            {
                flag = Convert.ToInt32(DTab.Rows[0]["flag"]);
            }
            else
            {
                flag = 0;
            }

            return flag;
        }
        #region "Lookup Editor"
        public static void LOOKUPRate(LookUpEdit lookup)
        {
            RateTypeMaster objRateType = new RateTypeMaster();
            DataTable RateType = objRateType.GetData(1);
            lookup.Properties.DataSource = RateType;
            lookup.Properties.ValueMember = "ratetype_id";
            lookup.Properties.DisplayMember = "rate_type";
            lookup.ClosePopup();
        }

        public static void LOOKUPCompany(LookUpEdit lookup)
        {
            CompanyMaster objCompany = new CompanyMaster();
            DataTable Company = objCompany.GetData();
            lookup.Properties.DataSource = Company;
            lookup.Properties.ValueMember = "company_id";
            lookup.Properties.DisplayMember = "company_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPHRManager(LookUpEdit lookup)
        {
            HRManagerMaster objHRManager = new HRManagerMaster();
            DataTable HR_Manager = objHRManager.GetData();
            lookup.Properties.DataSource = HR_Manager;
            lookup.Properties.ValueMember = "manager_id";
            lookup.Properties.DisplayMember = "manager_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPHRFactory(LookUpEdit lookup)
        {
            HRFactoryMaster objHRFactory = new HRFactoryMaster();
            DataTable HR_Factory = objHRFactory.GetData();
            lookup.Properties.DataSource = HR_Factory;
            lookup.Properties.ValueMember = "factory_id";
            lookup.Properties.DisplayMember = "factory_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPHRActiveFactory(LookUpEdit lookup)
        {
            HRFactoryMaster objHRFactory = new HRFactoryMaster();
            DataTable HR_Factory = objHRFactory.GetData(1);
            lookup.Properties.DataSource = HR_Factory;
            lookup.Properties.ValueMember = "factory_id";
            lookup.Properties.DisplayMember = "factory_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPHRFactoryDept(LookUpEdit lookup)
        {
            HREmployeeMaster objHRFactoryDept = new HREmployeeMaster();
            DataTable HR_FactoryDept = objHRFactoryDept.Fact_Dept_GetData();
            lookup.Properties.DataSource = HR_FactoryDept;
            lookup.Properties.ValueMember = "fact_department_id";
            lookup.Properties.DisplayMember = "fact_dept_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPHRFactoryActiveDept(LookUpEdit lookup)
        {
            HREmployeeMaster objHRFactoryDept = new HREmployeeMaster();
            DataTable HR_FactoryDept = objHRFactoryDept.Fact_Dept_GetData(1);
            lookup.Properties.DataSource = HR_FactoryDept;
            lookup.Properties.ValueMember = "fact_department_id";
            lookup.Properties.DisplayMember = "fact_dept_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPCompany_New(LookUpEdit lookup)
        {
            CompanyMaster objCompany = new CompanyMaster();
            FillCombo ObjFillCombo = new FillCombo();

            ObjFillCombo.user_id = BLL.GlobalDec.gEmployeeProperty.user_id;

            DataTable Company = ObjFillCombo.FillCmb(FillCombo.TABLE.Company_Master_New);

            lookup.Properties.DataSource = Company;
            lookup.Properties.ValueMember = "company_id";
            lookup.Properties.DisplayMember = "company_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPCopy_User(LookUpEdit lookup)
        {
            FillCombo ObjFillCombo = new FillCombo();
            DataTable Copy_User = ObjFillCombo.FillCmb(FillCombo.TABLE.Copy_User_Master);

            lookup.Properties.DataSource = Copy_User;
            lookup.Properties.ValueMember = "user_id";
            lookup.Properties.DisplayMember = "user_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPBranch(LookUpEdit lookup)
        {
            BranchMaster objBranch = new BranchMaster();
            DataTable Branch = objBranch.GetData();
            lookup.Properties.DataSource = Branch;
            lookup.Properties.ValueMember = "branch_id";
            lookup.Properties.DisplayMember = "branch_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPBranch_New(LookUpEdit lookup)
        {
            BranchMaster objBranch = new BranchMaster();
            FillCombo ObjFillCombo = new FillCombo();

            ObjFillCombo.user_id = BLL.GlobalDec.gEmployeeProperty.user_id;

            DataTable Branch = ObjFillCombo.FillCmb(FillCombo.TABLE.Branch_Master_New);
            lookup.Properties.DataSource = Branch;
            lookup.Properties.ValueMember = "branch_id";
            lookup.Properties.DisplayMember = "branch_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPStoreDepartment(LookUpEdit lookup)
        {
            StoreDepartmentMaster objStoreDepartment = new StoreDepartmentMaster();
            DataTable StoreDepartment = objStoreDepartment.GetData();
            lookup.Properties.DataSource = StoreDepartment;
            lookup.Properties.ValueMember = "department_id";
            lookup.Properties.DisplayMember = "department_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPStoreDivision(LookUpEdit lookup)
        {
            StoreDivisionMaster objStoreDivision = new StoreDivisionMaster();
            DataTable StoreDepartment = objStoreDivision.GetData();
            lookup.Properties.DataSource = StoreDepartment;
            lookup.Properties.ValueMember = "division_id";
            lookup.Properties.DisplayMember = "division_name";
            lookup.ClosePopup();
        }

        public static void LOOKUPLocation(LookUpEdit lookup)
        {
            LocationMaster objLocation = new LocationMaster();
            DataTable Location = objLocation.GetData();
            lookup.Properties.DataSource = Location;
            lookup.Properties.ValueMember = "location_id";
            lookup.Properties.DisplayMember = "location_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPLocation_New(LookUpEdit lookup)
        {
            LocationMaster objLocation = new LocationMaster();
            FillCombo ObjFillCombo = new FillCombo();

            ObjFillCombo.user_id = BLL.GlobalDec.gEmployeeProperty.user_id;

            DataTable Location = ObjFillCombo.FillCmb(FillCombo.TABLE.Location_Master_New);
            lookup.Properties.DataSource = Location;
            lookup.Properties.ValueMember = "location_id";
            lookup.Properties.DisplayMember = "location_name";
            lookup.ClosePopup();
        }

        public static void LOOKUPBillManager(LookUpEdit lookup)
        {
            MstBillManagerMaster billManager = new MstBillManagerMaster();
            DataTable Manager = billManager.GetData();
            lookup.Properties.DataSource = Manager;
            lookup.Properties.ValueMember = "manager_id";
            lookup.Properties.DisplayMember = "manager_name";
            lookup.ClosePopup();


        }

        public static void LOOKUPDepartment(LookUpEdit lookup)
        {
            DepartmentMaster objDepartment = new DepartmentMaster();
            DataTable Department = objDepartment.GetData();
            lookup.Properties.DataSource = Department;
            lookup.Properties.ValueMember = "department_id";
            lookup.Properties.DisplayMember = "department_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPDepartment_New(LookUpEdit lookup)
        {
            DepartmentMaster objDepartment = new DepartmentMaster();
            FillCombo ObjFillCombo = new FillCombo();

            ObjFillCombo.user_id = BLL.GlobalDec.gEmployeeProperty.user_id;

            DataTable Department = ObjFillCombo.FillCmb(FillCombo.TABLE.Department_Master_New);
            lookup.Properties.DataSource = Department;
            lookup.Properties.ValueMember = "department_id";
            lookup.Properties.DisplayMember = "department_name";
            lookup.ClosePopup();
        }

        public static void LOOKUPDepartment_Costing(LookUpEdit lookup)
        {
            DepartmentMaster objDepartment = new DepartmentMaster();
            DataTable Department = objDepartment.GetData();

            DataTable DTab_Dept = Department.Select("department_name in('ROUGH ASSORTMENT','SARIN','4P','TABLE PATTA','ASSORTMENT','POLISH','POLISH REPARING')").CopyToDataTable();

            lookup.Properties.DataSource = DTab_Dept;
            DTab_Dept.DefaultView.Sort = "department_id";
            lookup.Properties.ValueMember = "department_id";
            lookup.Properties.DisplayMember = "department_name";

            lookup.ClosePopup();
        }
        public static void LOOKUPUnit(LookUpEdit lookup)
        {
            MfgUnitMaster objUnit = new MfgUnitMaster();
            DataTable Unit = objUnit.GetData();
            lookup.Properties.DataSource = Unit;
            lookup.Properties.ValueMember = "unit_id";
            lookup.Properties.DisplayMember = "unit_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPItemType(LookUpEdit lookup)
        {
            MfgItemTypeMaster objItemType = new MfgItemTypeMaster();
            DataTable Item_Type = objItemType.GetData();
            lookup.Properties.DataSource = Item_Type;
            lookup.Properties.ValueMember = "item_type_id";
            lookup.Properties.DisplayMember = "item_type_name";
            lookup.ClosePopup();
        }

        public static void LOOKUPMfgPacketTypeWages(LookUpEdit lookup)
        {
            MfgPacketTypeWages objMfgWagesType = new MfgPacketTypeWages();
            DataTable PacketTypeWages = objMfgWagesType.GetData();
            lookup.Properties.DataSource = PacketTypeWages;
            lookup.Properties.ValueMember = "packet_type_id";
            lookup.Properties.DisplayMember = "type";
            lookup.ClosePopup();
        }

        public static void LOOKUPCountry(LookUpEdit lookup)
        {
            CountryMaster objCountry = new CountryMaster();
            DataTable Country = objCountry.GetData();
            lookup.Properties.DataSource = Country;
            lookup.Properties.ValueMember = "country_id";
            lookup.Properties.DisplayMember = "country_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPState(LookUpEdit lookup)
        {
            StateMaster objState = new StateMaster();
            DataTable State = objState.GetData();
            lookup.Properties.DataSource = State;
            lookup.Properties.ValueMember = "state_id";
            lookup.Properties.DisplayMember = "state_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPCity(LookUpEdit lookup)
        {
            CityMaster objCity = new CityMaster();
            DataTable City = objCity.GetData();
            lookup.Properties.DataSource = City;
            lookup.Properties.ValueMember = "city_id";
            lookup.Properties.DisplayMember = "city_name";
            lookup.ClosePopup();
        }

        public static void LOOKUPDesignation(LookUpEdit lookup)
        {
            DesignationMaster objDesign = new DesignationMaster();
            DataTable Design = objDesign.GetData();
            lookup.Properties.DataSource = Design;
            lookup.Properties.ValueMember = "designation_id";
            lookup.Properties.DisplayMember = "designation";
            lookup.ClosePopup();
        }
        public static void LOOKUPEmployee(LookUpEdit lookup)
        {
            EmployeeMaster objEmp = new EmployeeMaster();
            DataTable Emp = objEmp.GetData(1, null);
            lookup.Properties.DataSource = Emp;
            lookup.Properties.ValueMember = "employee_id";
            lookup.Properties.DisplayMember = "employee_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPEmp(LookUpEdit lookup)
        {
            EmployeeMaster objEmp = new EmployeeMaster();
            DataTable Emp = objEmp.GetEmployee(1);
            lookup.Properties.DataSource = Emp;
            lookup.Properties.ValueMember = "employee_id";
            lookup.Properties.DisplayMember = "employee_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPEmpHistory(LookUpEdit lookup)
        {
            EmployeeMaster objEmp = new EmployeeMaster();
            DataTable Emp = objEmp.GetEmployee_History(1);
            lookup.Properties.DataSource = Emp;
            lookup.Properties.ValueMember = "employee_id";
            lookup.Properties.DisplayMember = "employee_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPEmployeeCompanyWise(LookUpEdit lookup, int company, int branch, int location, int department)
        {
            EmployeeMaster objEmp = new EmployeeMaster();
            DataTable Emp = objEmp.GetCompanyWiseEmpData(company, branch, location, department, 1);
            lookup.Properties.DataSource = Emp;
            lookup.Properties.ValueMember = "employee_id";
            lookup.Properties.DisplayMember = "employee_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPManager(LookUpEdit lookup)
        {
            EmployeeMaster objEmp = new EmployeeMaster();
            DataTable Emp = objEmp.GetData(1, "MANAGER");
            lookup.Properties.DataSource = Emp;
            lookup.Properties.ValueMember = "employee_id";
            lookup.Properties.DisplayMember = "employee_name";
            lookup.ClosePopup();
        }

        public static void LOOKUPAllManager(LookUpEdit lookup)
        {
            EmployeeMaster objMGR = new EmployeeMaster();
            DataTable mgr = objMGR.GetManager(1, "MANAGER");
            lookup.Properties.DataSource = mgr;
            lookup.Properties.ValueMember = "employee_id";
            lookup.Properties.DisplayMember = "employee_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPManagerName(LookUpEdit lookup)
        {
            EmployeeMaster objEmp = new EmployeeMaster();
            DataTable Emp = objEmp.GetData(1, "MANAGER");
            lookup.Properties.DataSource = Emp;
            lookup.Properties.ValueMember = "manager_id";
            lookup.Properties.DisplayMember = "short_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPActiveManagerName(LookUpEdit lookup)
        {
            //EmployeeMaster objEmp = new EmployeeMaster();
            //DataTable Emp = objEmp.GetData_ActiveManager(1, "MANAGER");
            //lookup.InvokeEx(t =>
            //{
            //    t.Properties.DataSource = Emp;
            //    t.Properties.ValueMember = "manager_id";
            //    t.Properties.DisplayMember = "short_name";
            //    t.ClosePopup();
            //});

            EmployeeMaster objEmp = new EmployeeMaster();
            DataTable Emp = objEmp.GetData_ActiveManager(1, "MANAGER");
            lookup.Properties.DataSource = Emp;
            lookup.Properties.ValueMember = "manager_id";
            lookup.Properties.DisplayMember = "short_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPItemCompany(LookUpEdit lookup)
        {
            MfgMachineItemCompanyMaster objCompany = new MfgMachineItemCompanyMaster();
            DataTable Company = objCompany.GetData();
            lookup.Properties.DataSource = Company;
            lookup.Properties.ValueMember = "item_company_id";
            lookup.Properties.DisplayMember = "company_name";
            lookup.ClosePopup();
        }

        public static void LOOKUPItemName(LookUpEdit lookup)
        {
            MfgMachineItemMaster objItem = new MfgMachineItemMaster();
            DataTable Company = objItem.GetData();
            lookup.Properties.DataSource = Company;
            lookup.Properties.ValueMember = "item_id";
            lookup.Properties.DisplayMember = "item_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPPartyName(LookUpEdit lookup)
        {
            MfgMachinePartyMaster objParty = new MfgMachinePartyMaster();
            DataTable Company = objParty.GetData();
            lookup.Properties.DataSource = Company;
            lookup.Properties.ValueMember = "party_id";
            lookup.Properties.DisplayMember = "party_name";
            lookup.ClosePopup();
        }

        public static void LOOKUPMachineEntry(LookUpEdit lookup)
        {
            MfgMachineDetailEntry objItem = new MfgMachineDetailEntry();
            DataTable Company = objItem.GetData(1, 1, "1900-01-01", "2900-01-01");
            lookup.Properties.DataSource = Company;
            lookup.Properties.ValueMember = "machine_item_id";
            lookup.Properties.DisplayMember = "item_name";
            lookup.ClosePopup();
        }

        public static void LOOKUPManagerRep(DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookup)
        {
            EmployeeMaster objEmp = new EmployeeMaster();
            DataTable Emp = objEmp.GetData(1, "MANAGER");
            lookup.DataSource = Emp;
            lookup.ValueMember = "manager_id";
            lookup.DisplayMember = "manager_short_name";
        }
        public static void LOOKUPDeptType(LookUpEdit lookup)
        {
            MfgDepartmentTypeMaster objDeptType = new MfgDepartmentTypeMaster();
            DataTable DeptType = objDeptType.GetData(1);
            lookup.Properties.DataSource = DeptType;
            lookup.Properties.ValueMember = "department_type_id";
            lookup.Properties.DisplayMember = "department_type";
            lookup.ClosePopup();
        }
        public static void LOOKUPRole(LookUpEdit lookup)
        {
            ConfigRoleMaster objRole = new ConfigRoleMaster();
            DataTable Role = objRole.GetData_Lookup();
            lookup.Properties.DataSource = Role;
            lookup.Properties.ValueMember = "role_id";
            lookup.Properties.DisplayMember = "role_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPMenu(LookUpEdit lookup)
        {
            FillCombo ObjFillCombo = new FillCombo();
            DataTable DTab = ObjFillCombo.FillCmb(FillCombo.TABLE.Menu_Master);
            lookup.Properties.DataSource = DTab;
            lookup.Properties.ValueMember = "menu_id";
            lookup.Properties.DisplayMember = "menu_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPParty(LookUpEdit lookup, int IsRej = 0)
        {
            PartyMaster objParty = new PartyMaster();
            DataTable Party = objParty.GetData(1, IsRej);
            lookup.Properties.DataSource = Party;
            lookup.Properties.ValueMember = "party_id";
            lookup.Properties.DisplayMember = "party_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPRejectionParty(LookUpEdit lookup)
        {
            RejectionPartyMaster objRejectionParty = new RejectionPartyMaster();
            DataTable Party = objRejectionParty.GetData(1);
            lookup.Properties.DataSource = Party;
            lookup.Properties.ValueMember = "rejection_party_id";
            lookup.Properties.DisplayMember = "rejection_party_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPRejectionParty(CheckedComboBoxEdit lookup)
        {
            RejectionPartyMaster objRejectionParty = new RejectionPartyMaster();
            DataTable Party = objRejectionParty.GetData(1);
            lookup.Properties.DataSource = Party;
            lookup.Properties.ValueMember = "rejection_party_id";
            lookup.Properties.DisplayMember = "rejection_party_name";
            lookup.ClosePopup();
        }


        public static void LOOKUPStoreParty(LookUpEdit lookup, string Party_Group)
        {
            StorePartyMaster objStoreParty = new StorePartyMaster();
            DataTable Party = objStoreParty.Party_Group_WiseGetData(Party_Group);
            lookup.Properties.DataSource = Party;
            lookup.Properties.ValueMember = "party_id";
            lookup.Properties.DisplayMember = "party_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPStoreManager(LookUpEdit lookup)
        {
            StoreManagerMaster objStoreManager = new StoreManagerMaster();
            DataTable Manager = objStoreManager.GetData();
            lookup.Properties.DataSource = Manager;
            lookup.Properties.ValueMember = "manager_id";
            lookup.Properties.DisplayMember = "manager_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPPartyGroup(LookUpEdit lookup)
        {
            MfgPartyGroupMaster objPartyGroup = new MfgPartyGroupMaster();
            DataTable PartyGroup = objPartyGroup.GetData(1);
            lookup.Properties.DataSource = PartyGroup;
            lookup.Properties.ValueMember = "party_group_id";
            lookup.Properties.DisplayMember = "party_group_name";
            lookup.ClosePopup();
        }
        public static void LOOKUP_AvakJavakPartyGroup(LookUpEdit lookup, int IsRej = 0)
        {
            AvakJavakPartyMaster objAvakJavakParty = new AvakJavakPartyMaster();
            DataTable AvakJavakParty = objAvakJavakParty.GetData_AvakJavakPartyGroup();
            lookup.Properties.DataSource = AvakJavakParty;
            lookup.Properties.ValueMember = "party_group_id";
            lookup.Properties.DisplayMember = "party_group_name";
            lookup.ClosePopup();
        }
        public static void LOOKUP_AvakJavakParty(LookUpEdit lookup, int IsRej = 0)
        {
            AvakJavakPartyMaster objAvakJavakParty = new AvakJavakPartyMaster();
            DataTable AvakJavakParty = objAvakJavakParty.GetData_AvakJavakParty(1);
            lookup.Properties.DataSource = AvakJavakParty;
            lookup.Properties.ValueMember = "party_id";
            lookup.Properties.DisplayMember = "party_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPInvoiceNo(LookUpEdit lookup)
        {
            SaleInvoice objSaleInvoice = new SaleInvoice();
            DataTable SaleInvoice = objSaleInvoice.GetInvoiceNo();
            lookup.Properties.DataSource = SaleInvoice;
            lookup.Properties.ValueMember = "invoice_id";
            lookup.Properties.DisplayMember = "invoice_no";
            lookup.ClosePopup();
        }

        public static void LOOKUPParty_Rough(LookUpEdit lookup)
        {
            RoughPartyMaster objParty = new RoughPartyMaster();
            DataTable Party = objParty.GetData(1);
            lookup.Properties.DataSource = Party;
            lookup.Properties.ValueMember = "rough_party_id";
            lookup.Properties.DisplayMember = "rough_party_name";
            lookup.ClosePopup();
        }

        public static void LOOKUPSight(LookUpEdit lookup)
        {
            MfgSightTypeMaster objSightType = new MfgSightTypeMaster();
            DataTable SightType = objSightType.GetData(1);
            lookup.Properties.DataSource = SightType;
            lookup.Properties.ValueMember = "sight_type_id";
            lookup.Properties.DisplayMember = "sight_type";
            lookup.ClosePopup();
        }
        public static void LOOKUPSource(LookUpEdit lookup)
        {
            MfgSourceMaster objSource = new MfgSourceMaster();
            DataTable Source = objSource.GetData(1);
            lookup.Properties.DataSource = Source;
            lookup.Properties.ValueMember = "source_id";
            lookup.Properties.DisplayMember = "source_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPArticle(LookUpEdit lookup)
        {
            MfgArticleMaster objArticle = new MfgArticleMaster();
            DataTable Article = objArticle.GetData(1);
            lookup.Properties.DataSource = Article;
            lookup.Properties.ValueMember = "article_id";
            lookup.Properties.DisplayMember = "article_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPGroup(LookUpEdit lookup)
        {
            MfgGroupMaster objGroup = new MfgGroupMaster();
            DataTable Group = objGroup.GetData(1);
            lookup.Properties.DataSource = Group;
            lookup.Properties.ValueMember = "group_id";
            lookup.Properties.DisplayMember = "group_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPTeam(LookUpEdit lookup)
        {
            MfgTeamMaster objTeam = new MfgTeamMaster();
            DataTable Team = objTeam.GetData(1);
            lookup.Properties.DataSource = Team;
            lookup.Properties.ValueMember = "team_id";
            lookup.Properties.DisplayMember = "team_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPJangedSieve(LookUpEdit lookup)
        {
            MfgJangedSieve objJangedSieve = new MfgJangedSieve();
            DataTable JangedSieve = objJangedSieve.GetData(1);
            lookup.Properties.DataSource = JangedSieve;
            lookup.Properties.ValueMember = "janged_sieve_id";
            lookup.Properties.DisplayMember = "sieve_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPRoughType(LookUpEdit lookup)
        {
            MfgRoughTypeMaster objRoughType = new MfgRoughTypeMaster();
            DataTable RoughType = objRoughType.GetData(1);
            lookup.Properties.DataSource = RoughType;
            lookup.Properties.ValueMember = "rough_type_id";
            lookup.Properties.DisplayMember = "rough_type";
            lookup.ClosePopup();
        }
        public static void LOOKUPRoughSieve(LookUpEdit lookup)
        {
            MfgRoughSieve objRoughSieve = new MfgRoughSieve();
            DataTable RoughSieve = objRoughSieve.GetData(1);
            lookup.Properties.DataSource = RoughSieve;
            lookup.Properties.ValueMember = "rough_sieve_id";
            lookup.Properties.DisplayMember = "sieve_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPRoughSieveWages(LookUpEdit lookup)
        {
            //MfgRoughSieve objRoughSieve = new MfgRoughSieve();
            //DataTable RoughSieve = objRoughSieve.GetData(1);
            //lookup.InvokeEx(t =>
            //{
            //    t.Properties.DataSource = RoughSieve;
            //    t.Properties.ValueMember = "factory_wages_sieve_id";
            //    t.Properties.DisplayMember = "wages_sieve";
            //    t.ClosePopup();
            //});
            MfgRoughSieve objRoughSieve = new MfgRoughSieve();
            DataTable RoughSieve = objRoughSieve.GetData(1);
            lookup.Properties.DataSource = RoughSieve;
            lookup.Properties.ValueMember = "factory_wages_sieve_id";
            lookup.Properties.DisplayMember = "wages_sieve";
            lookup.ClosePopup();
        }
        public static void LOOKUPRoughShade(LookUpEdit lookup)
        {
            ColorMaster objRoughShade = new ColorMaster();
            DataTable Color = objRoughShade.GetData(1);
            lookup.Properties.DataSource = Color;
            lookup.Properties.ValueMember = "color_id";
            lookup.Properties.DisplayMember = "color_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPLedger(LookUpEdit lookup, int num_LocationID = 0)
        {
            LedgerMaster objLedger = new LedgerMaster();
            DataTable Ledger = objLedger.GetData(1, num_LocationID);
            lookup.Properties.DataSource = Ledger;
            lookup.Properties.ValueMember = "ledger_id";
            lookup.Properties.DisplayMember = "ledger_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPMachine(LookUpEdit lookup, int processId = 0)
        {
            MfgMachineMaster objMachine = new MfgMachineMaster();
            DataTable Machine = objMachine.GetData(1, processId);
            lookup.Properties.DataSource = Machine;
            lookup.Properties.ValueMember = "machine_id";
            lookup.Properties.DisplayMember = "machine_name";
            lookup.ClosePopup();
        }
        public static void LOOKUP_Bank_CashLedger(LookUpEdit lookup, int num_LocationID = 0)
        {
            try
            {
                LedgerMaster objLedger = new LedgerMaster();
                DataTable Ledger = objLedger.GetData(1, num_LocationID);
                DataTable DTab_Bank_Cash = Ledger.Select("ledger_Name in('CASH BALANCE','BANK BALANCE','PATTY CASH')").CopyToDataTable();
                lookup.Properties.DataSource = DTab_Bank_Cash;
                lookup.Properties.ValueMember = "ledger_id";
                lookup.Properties.DisplayMember = "ledger_name";
                lookup.ClosePopup();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public static void LOOKUPItem(LookUpEdit lookup)
        {
            MfgItemMaster objItem = new MfgItemMaster();
            objItem.GetData();
            DataTable Item = objItem.DTab;
            lookup.Properties.DataSource = Item;
            lookup.Properties.ValueMember = "item_id";
            lookup.Properties.DisplayMember = "item_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPSubItem(LookUpEdit lookup)
        {
            MfgSubItemMaster objSubItem = new MfgSubItemMaster();
            objSubItem.GetData();
            DataTable Item = objSubItem.DTab;
            lookup.Properties.DataSource = Item;
            lookup.Properties.ValueMember = "sub_item_id";
            lookup.Properties.DisplayMember = "sub_item_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPBroker(LookUpEdit lookup)
        {
            BrokerMaster objBroker = new BrokerMaster();
            DataTable Broker = objBroker.GetData(1, 0);
            lookup.Properties.DataSource = Broker;
            lookup.Properties.ValueMember = "broker_id";
            lookup.Properties.DisplayMember = "broker_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPRejectionBroker(LookUpEdit lookup)
        {
            RejectionPartyMaster objRejectionParty = new RejectionPartyMaster();
            DataTable Broker = objRejectionParty.Broker_GetData(1);
            lookup.Properties.DataSource = Broker;
            lookup.Properties.ValueMember = "rejection_party_id";
            lookup.Properties.DisplayMember = "broker_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPRejectionContactPerson(ComboBoxEdit lookup)
        {
            RejectionPartyMaster objRejectionParty = new RejectionPartyMaster();
            DataTable Broker = objRejectionParty.Contact_Person_Distinct_GetData();

            foreach (DataRow DRaw in Broker.Rows)
            {
                lookup.Properties.Items.Add(DRaw["rejection_broker_name"]);
                lookup.ClosePopup();
            }
        }
        public static void LOOKUPRejPurity(LookUpEdit lookup)
        {
            MfgRejectionPurityMaster objRejPurity = new MfgRejectionPurityMaster();
            DataTable RejPurity = objRejPurity.GetData(1);
            lookup.Properties.DataSource = RejPurity;
            lookup.Properties.ValueMember = "purity_id";
            lookup.Properties.DisplayMember = "purity_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPRejSection(LookUpEdit lookup)
        {
            MFGRejectionSectionMaster objRejSection = new MFGRejectionSectionMaster();
            DataTable RejSection = objRejSection.GetData(1);
            lookup.Properties.DataSource = RejSection;
            lookup.Properties.ValueMember = "section_id";
            lookup.Properties.DisplayMember = "section_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPRejKapan(LookUpEdit lookup)
        {
            MFGRoughStockEntry objRoughStock = new MFGRoughStockEntry();
            DataTable RoughStock = objRoughStock.Kapan_GetData();
            lookup.Properties.DataSource = RoughStock;
            lookup.Properties.ValueMember = "kapan_id";
            lookup.Properties.DisplayMember = "kapan_no";
            lookup.ClosePopup();
        }

        public static void LOOKUPRoughCheckClarity(CheckedComboBoxEdit lookup)
        {
            MfgRejectionPurityMaster objRejPurity = new MfgRejectionPurityMaster();
            DataTable RejPurity = objRejPurity.GetData(1);
            lookup.Properties.DataSource = RejPurity;
            lookup.Properties.ValueMember = "purity_id";
            lookup.Properties.DisplayMember = "purity_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPRoughParty(CheckedComboBoxEdit lookup)
        {
            PartyMaster objParty = new PartyMaster();
            DataTable Party = objParty.GetData(1);
            lookup.Properties.DataSource = Party;
            lookup.Properties.ValueMember = "party_id";
            lookup.Properties.DisplayMember = "party_name";
            lookup.ClosePopup();
        }

        public static void Rough_LOOKUPBroker(LookUpEdit lookup)
        {
            RoughBrokerMaster objRoughBroker = new RoughBrokerMaster();
            DataTable Broker = objRoughBroker.GetData(1, 0);
            lookup.Properties.DataSource = Broker;
            lookup.Properties.ValueMember = "rough_broker_id";
            lookup.Properties.DisplayMember = "rough_broker_name";
            lookup.ClosePopup();
        }

        public static void LOOKUPDeliveryType(LookUpEdit lookup)
        {
            DeliveryTypeMaster objDeliveryType = new DeliveryTypeMaster();
            DataTable DeliveryType = objDeliveryType.GetData(1);
            lookup.Properties.DataSource = DeliveryType;
            lookup.Properties.ValueMember = "delivery_type_id";
            lookup.Properties.DisplayMember = "delivery_type";
            lookup.ClosePopup();
        }
        public static void LOOKUPSubSieve(LookUpEdit lookup, int Sieve_id)
        {
            SieveMaster objSubSieve = new SieveMaster();
            DataTable Sub_Sieve = objSubSieve.SubSieve_GetData(1, Sieve_id);
            lookup.Properties.DataSource = Sub_Sieve;
            lookup.Properties.ValueMember = "sub_sieve_id";
            lookup.Properties.DisplayMember = "sub_sieve_name";
            lookup.ClosePopup();
        }

        public static void LOOKUPCategory(LookUpEdit lookup)
        {
            CategoryMaster objCat = new CategoryMaster();
            DataTable Cat = objCat.GetData();
            lookup.Properties.DataSource = Cat;
            lookup.Properties.ValueMember = "category_id";
            lookup.Properties.DisplayMember = "category_name";
            lookup.ClosePopup();
        }

        public static void LOOKUPMenuHeader(LookUpEdit lookup)
        {
            MenuHeaderMaster objMenu = new MenuHeaderMaster();
            DataTable menu = objMenu.GetData();
            lookup.Properties.DataSource = menu;
            lookup.Properties.ValueMember = "menu_id";
            lookup.Properties.DisplayMember = "menu_name";
            lookup.ClosePopup();
        }

        public static void LOOKUPForm(LookUpEdit lookup)
        {
            ConfigFormMaster objForm = new ConfigFormMaster();
            DataTable form = objForm.GetData();
            lookup.Properties.DataSource = form;
            lookup.Properties.ValueMember = "form_id";
            lookup.Properties.DisplayMember = "form_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPUser(LookUpEdit lookup)
        {
            UserMaster objUser = new UserMaster();
            DataTable user = objUser.GetData();
            lookup.Properties.DataSource = user;
            lookup.Properties.ValueMember = "user_id";
            lookup.Properties.DisplayMember = "user_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPUser_GetData(LookUpEdit lookup)
        {
            UserMaster objUser = new UserMaster();
            DataTable user = objUser.GetData();
            lookup.Properties.DataSource = user;
            lookup.Properties.ValueMember = "user_id";
            lookup.Properties.DisplayMember = "username";
            lookup.ClosePopup();
        }
        public static void LOOKUPCurrency(LookUpEdit lookup)
        {
            CurrencyMaster objCurr = new CurrencyMaster();
            DataTable user = objCurr.GetData();
            lookup.Properties.DataSource = user;
            lookup.Properties.ValueMember = "currency_id";
            lookup.Properties.DisplayMember = "currency";
            lookup.ClosePopup();
        }
        public static void LOOKUPSieve(LookUpEdit lookup)
        {
            SieveMaster objSieve = new SieveMaster();
            DataTable Sieve = objSieve.GetData();
            lookup.Properties.DataSource = Sieve;
            lookup.Properties.ValueMember = "sieve_id";
            lookup.Properties.DisplayMember = "sieve_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPRoughSieveRep(DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookup)
        {
            MfgRoughSieve objRoughSieve = new MfgRoughSieve();
            DataTable RoughSieve = objRoughSieve.GetData(1);
            lookup.DataSource = RoughSieve;
            lookup.ValueMember = "rough_sieve_id";
            lookup.DisplayMember = "sieve_name";
        }

        public static void LOOKUPParty_RoughRep(DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookup)
        {
            RoughPartyMaster objParty = new RoughPartyMaster();
            DataTable Party = objParty.GetData(1);

            lookup.DataSource = Party;
            lookup.ValueMember = "rough_party_id";
            lookup.DisplayMember = "rough_party_name";
        }
        public static void LOOKUPStatusRep(DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookup)
        {
            DataTable DTab = new DataTable("status");
            DTab.Columns.Add("status");
            DTab.Rows.Add("");
            DTab.Rows.Add("PAID");
            DTab.Rows.Add("UNPAID");

            lookup.DataSource = DTab;
            lookup.ValueMember = "status";
            lookup.DisplayMember = "status";
        }

        public static void LOOKUPBroker_RoughRep(DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookup)
        {
            RoughBrokerMaster objRoughBroker = new RoughBrokerMaster();
            DataTable Broker = objRoughBroker.GetData(1, 0);
            lookup.DataSource = Broker;
            lookup.ValueMember = "rough_broker_id";
            lookup.DisplayMember = "rough_broker_name";
        }

        public static void LOOKUPRoughSieveWagesRep(DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookup)
        {
            MfgRoughSieve objRoughSieve = new MfgRoughSieve();
            DataTable RoughSieve = objRoughSieve.GetData(1);
            lookup.DataSource = RoughSieve;
            lookup.ValueMember = "factory_wages_sieve_id";
            lookup.DisplayMember = "wages_sieve";
        }
        public static void LOOKUPShape(LookUpEdit lookup)
        {
            ShapeMaster objShape = new ShapeMaster();
            DataTable Shape = objShape.GetData(1);
            lookup.Properties.DataSource = Shape;
            lookup.Properties.ValueMember = "shape_id";
            lookup.Properties.DisplayMember = "shape_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPPermission(LookUpEdit lookup)
        {
            ConfigRoleMaster objPermission = new ConfigRoleMaster();
            DataTable Permission = objPermission.GetData_Lookup(1);
            Permission.DefaultView.Sort = "role_name asc";
            lookup.Properties.DataSource = Permission;
            lookup.Properties.ValueMember = "role_id";
            lookup.Properties.DisplayMember = "role_name";
            lookup.ClosePopup();
        }

        public static void LOOKUPUserType(LookUpEdit lookup)
        {
            UserMaster objUserMaster = new UserMaster();
            DataTable Permission = objUserMaster.UserType_GetData(1);
            Permission.DefaultView.Sort = "user_type asc";
            lookup.Properties.DataSource = Permission;
            lookup.Properties.ValueMember = "usertype_id";
            lookup.Properties.DisplayMember = "user_type";
            lookup.ClosePopup();
        }
        public static void LOOKUPAccountType(LookUpEdit lookup)
        {
            AccountTypeMaster objAccTypeMaster = new AccountTypeMaster();
            DataTable AccountType = objAccTypeMaster.GetData(1);
            AccountType.DefaultView.Sort = "account_type asc";
            lookup.Properties.DataSource = AccountType;
            lookup.Properties.ValueMember = "account_type_id";
            lookup.Properties.DisplayMember = "account_type";
            lookup.ClosePopup();
        }
        public static void LOOKUPProcess(LookUpEdit lookup)
        {
            ProcessMaster objProcessMaster = new ProcessMaster();
            DataTable process = objProcessMaster.GetData(1);
            process.DefaultView.Sort = "sequence_no asc";
            lookup.Properties.DataSource = process;
            lookup.Properties.ValueMember = "process_id";
            lookup.Properties.DisplayMember = "process_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPProcessAll(LookUpEdit lookup)
        {
            ProcessMaster objProcessMaster = new ProcessMaster();
            DataTable process = objProcessMaster.GetData_All(1);
            process.DefaultView.Sort = "sequence_no asc";
            lookup.Properties.DataSource = process;
            lookup.Properties.ValueMember = "process_id";
            lookup.Properties.DisplayMember = "process_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPConfigPartyBroker(LookUpEdit lookup, int partyID)
        {
            ConfigPartyBrokerMaster objConfigPartyBroker = new ConfigPartyBrokerMaster();
            DataTable broker = objConfigPartyBroker.GetConfigPartyBroker(partyID);
            broker.DefaultView.Sort = "broker_id asc";
            lookup.Properties.DataSource = broker;
            lookup.Properties.ValueMember = "broker_id";
            lookup.Properties.DisplayMember = "broker_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPBank(LookUpEdit lookup)
        {
            BankMaster objBank = new BankMaster();
            DataTable bank = objBank.GetData(1);
            lookup.Properties.DataSource = bank;
            lookup.Properties.ValueMember = "bank_id";
            lookup.Properties.DisplayMember = "bank_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPHead(LookUpEdit lookup)
        {
            IncomeEntryMaster objIncome = new IncomeEntryMaster();
            DataTable Head = objIncome.GetHead();
            lookup.Properties.DataSource = Head;
            lookup.Properties.ValueMember = "head_id";
            lookup.Properties.DisplayMember = "head_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPPurityGroup(LookUpEdit lookup)
        {
            MfgPurityGroupMaster objPurityGroup = new MfgPurityGroupMaster();
            DataTable PurityGroup = objPurityGroup.GetData();
            lookup.Properties.DataSource = PurityGroup;
            lookup.Properties.ValueMember = "purity_group_id";
            lookup.Properties.DisplayMember = "purity_group";
            lookup.ClosePopup();
        }
        public static void LOOKUPSubProcess(LookUpEdit lookup)
        {
            MfgSubProcessMaster objSubProcessMaster = new MfgSubProcessMaster();
            DataTable SubProcess = objSubProcessMaster.GetData(1);
            SubProcess.DefaultView.Sort = "sequence_no asc";
            lookup.Properties.DataSource = SubProcess;
            lookup.Properties.ValueMember = "sub_process_id";
            lookup.Properties.DisplayMember = "sub_process_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPMachineType(LookUpEdit lookup)
        {
            MfgMachineTypeMaster objMachineTypeMaster = new MfgMachineTypeMaster();
            DataTable MachineType = objMachineTypeMaster.GetData(1);
            MachineType.DefaultView.Sort = "sequence_no asc";
            lookup.Properties.DataSource = MachineType;
            lookup.Properties.ValueMember = "machine_type_id";
            lookup.Properties.DisplayMember = "machine_type_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPShift(LookUpEdit lookup)
        {
            MfgShiftMaster objShiftMaster = new MfgShiftMaster();
            DataTable Shift = objShiftMaster.GetData(1);
            Shift.DefaultView.Sort = "shift_id asc";
            lookup.Properties.DataSource = Shift;
            lookup.Properties.ValueMember = "shift_id";
            lookup.Properties.DisplayMember = "shift_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPRoughCutType(LookUpEdit lookup)
        {
            MfgRoughCutTypeMaster objRoughCutTypeMaster = new MfgRoughCutTypeMaster();
            DataTable CutType = objRoughCutTypeMaster.GetData(1);
            CutType.DefaultView.Sort = "rough_cuttype_id asc";
            lookup.Properties.DataSource = CutType;
            lookup.Properties.ValueMember = "rough_cuttype_id";
            lookup.Properties.DisplayMember = "rough_cuttype_name";
            lookup.ClosePopup();
        }

        public static void LOOKUPIDProof(LookUpEdit lookup)
        {
            IDProofMaster objIDProofMaster = new IDProofMaster();
            DataTable IDProof = objIDProofMaster.GetData(1);
            lookup.Properties.DataSource = IDProof;
            lookup.Properties.ValueMember = "idproof_id";
            lookup.Properties.DisplayMember = "idproof_name";
            lookup.ClosePopup();
        }

        public static void LOOKUPPurityGroupWisePurity(LookUpEdit lookup, int purityGroupId)
        {
            MfgPurityGroupMaster objPurityGroup = new MfgPurityGroupMaster();
            DataTable Purity = objPurityGroup.PurityGroupWisePurity(purityGroupId);
            lookup.Properties.DataSource = Purity;
            lookup.Properties.ValueMember = "purity_id";
            lookup.Properties.DisplayMember = "purity_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPPurity(LookUpEdit lookup)
        {
            ClarityMaster objPurity = new ClarityMaster();
            DataTable Purity = objPurity.GetData(1);
            lookup.Properties.DataSource = Purity;
            lookup.Properties.ValueMember = "purity_id";
            lookup.Properties.DisplayMember = "purity_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPClarity(LookUpEdit lookup)
        {
            MfgRoughClarityMaster objRoughClarity = new MfgRoughClarityMaster();
            DataTable RoughClarity = objRoughClarity.GetData(1);
            lookup.Properties.DataSource = RoughClarity;
            lookup.Properties.ValueMember = "rough_clarity_id";
            lookup.Properties.DisplayMember = "rough_clarity_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPQuality(LookUpEdit lookup)
        {
            MfgQualityMaster objRoughQuality = new MfgQualityMaster();
            DataTable RoughQuality = objRoughQuality.GetData(1);
            lookup.Properties.DataSource = RoughQuality;
            lookup.Properties.ValueMember = "quality_id";
            lookup.Properties.DisplayMember = "quality_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPWagesType(LookUpEdit lookup)
        {
            //MfgPacketTypeWages objTypeOfWages = new MfgPacketTypeWages();
            //DataTable TypeOfWages = objTypeOfWages.GetData(1);
            //lookup.InvokeEx(t =>
            //{
            //    t.Properties.DataSource = TypeOfWages;
            //    t.Properties.ValueMember = "packet_type_id";
            //    t.Properties.DisplayMember = "type";
            //    t.ClosePopup();
            //});

            MfgPacketTypeWages objTypeOfWages = new MfgPacketTypeWages();
            DataTable TypeOfWages = objTypeOfWages.GetData(1);
            lookup.Properties.DataSource = TypeOfWages;
            lookup.Properties.ValueMember = "packet_type_id";
            lookup.Properties.DisplayMember = "type";
        }
        public static void LOOKUPRoughClarity(CheckedComboBoxEdit lookup)
        {
            MfgRoughClarityMaster objRoughClarity = new MfgRoughClarityMaster();
            DataTable RoughClarity = objRoughClarity.GetData(1);
            lookup.Properties.DataSource = RoughClarity;
            lookup.Properties.ValueMember = "rough_clarity_id";
            lookup.Properties.DisplayMember = "rough_clarity_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPPurity(CheckedComboBoxEdit lookup)
        {
            ClarityMaster objPurity = new ClarityMaster();
            DataTable Purity = objPurity.GetData(1);
            lookup.Properties.DataSource = Purity;
            lookup.Properties.ValueMember = "purity_id";
            lookup.Properties.DisplayMember = "purity_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPRoughShade(CheckedComboBoxEdit lookup)
        {
            ColorMaster objRoughShade = new ColorMaster();
            DataTable Color = objRoughShade.GetData(1);
            lookup.Properties.DataSource = Color;
            lookup.Properties.ValueMember = "color_id";
            lookup.Properties.DisplayMember = "color_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPRoughQuality(CheckedComboBoxEdit lookup)
        {
            MfgQualityMaster objRoughQuality = new MfgQualityMaster();
            DataTable RoughQuality = objRoughQuality.GetData(1);
            lookup.Properties.DataSource = RoughQuality;
            lookup.Properties.ValueMember = "quality_id";
            lookup.Properties.DisplayMember = "quality_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPRoughSieve(CheckedComboBoxEdit lookup)
        {
            MfgRoughSieve objRoughSieve = new MfgRoughSieve();
            DataTable RoughSieve = objRoughSieve.GetData(1);
            lookup.Properties.DataSource = RoughSieve;
            lookup.Properties.ValueMember = "rough_sieve_id";
            lookup.Properties.DisplayMember = "sieve_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPSieve(CheckedComboBoxEdit lookup)
        {
            SieveMaster objSieve = new SieveMaster();
            DataTable Sieve = objSieve.GetData(1);
            lookup.Properties.DataSource = Sieve;
            lookup.Properties.ValueMember = "sieve_id";
            lookup.Properties.DisplayMember = "sieve_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPAssortLocation(LookUpEdit lookup)
        {
            AssortLocationMaster objAssortLocationMaster = new AssortLocationMaster();
            DataTable AssortLoc = objAssortLocationMaster.GetData(1);
            AssortLoc.DefaultView.Sort = "assort_location_name asc";
            lookup.Properties.DataSource = AssortLoc;
            lookup.Properties.ValueMember = "assort_location_id";
            lookup.Properties.DisplayMember = "assort_location_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPAssort(CheckedComboBoxEdit lookup)
        {
            AssortMaster objAssort = new AssortMaster();
            DataTable Assort = objAssort.GetData(1);
            Assort = Assort.Select("type <> '" + System.DBNull.Value + "'").CopyToDataTable();
            lookup.Properties.DataSource = Assort;
            lookup.Properties.ValueMember = "assort_id";
            lookup.Properties.DisplayMember = "assort_name";
            lookup.ClosePopup();
        }
        public static void LOOKUPProcess(CheckedComboBoxEdit lookup)
        {
            ProcessMaster objProcessMaster = new ProcessMaster();
            DataTable process = objProcessMaster.GetData(1);
            process.DefaultView.Sort = "sequence_no asc";
            lookup.Properties.DataSource = process;
            lookup.Properties.ValueMember = "process_id";
            lookup.Properties.DisplayMember = "process_name";
            lookup.ClosePopup();
        }
        #endregion

        //// Repository Coding
        public static void LOOKUPCompanyRep(DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookup)
        {
            CompanyMaster objCompany = new CompanyMaster();
            DataTable Company = objCompany.GetData();
            lookup.DataSource = Company;
            lookup.ValueMember = "company_id";
            lookup.DisplayMember = "company_name";
        }

        public static void LOOKUPTypeOfWages(DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookup)
        {
            //MfgPacketTypeWages objTypeOfWages = new MfgPacketTypeWages();
            //DataTable TypeOfWages = objTypeOfWages.GetData(1);
            //lookup.(t =>
            //{
            //    t.DataSource = TypeOfWages;
            //    t.ValueMember = "packet_type_id";
            //    t.DisplayMember = "type";
            //});

            MfgPacketTypeWages objTypeOfWages = new MfgPacketTypeWages();
            DataTable TypeOfWages = objTypeOfWages.GetData(1);
            lookup.DataSource = TypeOfWages;
            lookup.ValueMember = "packet_type_id";
            lookup.DisplayMember = "type";
        }

        public static void LOOKUPBranchRep(DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookup)
        {
            BranchMaster objBranch = new BranchMaster();
            DataTable Branch = objBranch.GetData();
            lookup.DataSource = Branch;
            lookup.ValueMember = "branch_id";
            lookup.DisplayMember = "branch_name";
        }
        public static void LOOKUPLocationRep(DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookup)
        {
            LocationMaster objLocation = new LocationMaster();
            DataTable Location = objLocation.GetData();
            lookup.DataSource = Location;
            lookup.ValueMember = "location_id";
            lookup.DisplayMember = "location_name";
        }
        public static void LOOKUPDepartmentRep(DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookup)
        {
            DepartmentMaster objDepartment = new DepartmentMaster();
            DataTable Department = objDepartment.GetData();
            Department.DefaultView.Sort = "department_name asc";
            lookup.DataSource = Department;
            lookup.ValueMember = "department_id";
            lookup.DisplayMember = "department_name";
        }
        public static void LOOKUPRejPurityRep(DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookup)
        {
            MfgRejectionPurityMaster objRejPurity = new MfgRejectionPurityMaster();
            DataTable Purity = objRejPurity.GetData();
            Purity.DefaultView.Sort = "purity_name asc";
            lookup.DataSource = Purity;
            lookup.ValueMember = "purity_id";
            lookup.DisplayMember = "purity_name";
        }
        public static DataTable DTabType(string Remark)
        {
            DataTable DTab = new DataTable("Type");
            DTab.Columns.Add("Type");
            DTab.Columns.Add("Type1");
            if (Remark == "FACTORY_MIX_SPLIT_REPORT")
            {
                DTab.Rows.Add("FACTORY MIX", "FACTORY MIX");
                DTab.Rows.Add("FACTORY SPLIT", "FACTORY SPLIT");
            }
            else
            {
                DTab.Rows.Add("MIX", "MIX");
                DTab.Rows.Add("SPLIT", "SPLIT");
            }

            return DTab;
        }

        public static DataTable DTabEntryType()
        {
            DataTable DTab = new DataTable("Entry_Type");
            DTab.Columns.Add("Entry_Type");
            DTab.Columns.Add("Entry_Type1");
            DTab.Rows.Add("ALL", "ALL");
            DTab.Rows.Add("ROUGH", "ROUGH");
            DTab.Rows.Add("REJECTION", "REJECTION");
            return DTab;
        }

        public static DataTable Rough_Type()
        {
            DataTable DTab = new DataTable("Type");
            DTab.Columns.Add("Type");
            DTab.Columns.Add("Type1");
            DTab.Rows.Add("STOCK", "STOCK");
            return DTab;
        }

        public static DataTable DTabTransactionType()
        {
            DataTable DTab = new DataTable("Transaction_Type");
            DTab.Columns.Add("Transaction_Type");
            DTab.Columns.Add("Transaction_Type1");
            DTab.Rows.Add("Balance", "Balance");
            DTab.Rows.Add("Rejection", "Rejection");
            return DTab;
        }

        //public static void SingleBarcodePrint(string pStrLotId, bool ShowReportViewer, string pstrRPTName)
        //{
        //    try
        //    {
        //        System.ComponentModel.BackgroundWorker myWorker = new BackgroundWorker();
        //        myWorker.DoWork += new DoWorkEventHandler(myWorker_DoWork);
        //        myWorker.WorkerReportsProgress = true;
        //        myWorker.WorkerSupportsCancellation = true;

        //        //int numericValue = (int)numericUpDownMax.Value;//Capture the user input
        //        object[] arrObjects = new object[] { pstrRPTName, pStrLotId, ShowReportViewer };
        //        if (!myWorker.IsBusy)//Check if the worker is already in progress
        //        {
        //            myWorker.RunWorkerAsync(arrObjects);//Call the background worker
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        Global.Message("Invalid Report File Path");
        //    }
        //}


        public static void LotIDPrint(string pStrPacket, bool ShowReportViewer, string pstrRPTName)
        {
            try
            {
                System.ComponentModel.BackgroundWorker myWorker = new BackgroundWorker();
                myWorker.DoWork += new DoWorkEventHandler(myWorker_DoWork);
                myWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(myWorker_RunWorkerCompleted);
                myWorker.ProgressChanged += new ProgressChangedEventHandler(myWorker_ProgressChanged);
                myWorker.WorkerReportsProgress = true;
                myWorker.WorkerSupportsCancellation = true;


                //int numericValue = (int)numericUpDownMax.Value;//Capture the user input
                object[] arrObjects = new object[] { pstrRPTName, pStrPacket, ShowReportViewer };
                if (!myWorker.IsBusy)//Check if the worker is already in progress
                {
                    myWorker.RunWorkerAsync(arrObjects);//Call the background worker
                }
            }
            catch (Exception)
            {
                Global.Message("Invalid Report File Path");
            }
        }

        public static void myWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker sendingWorker = (BackgroundWorker)sender;//Capture the BackgroundWorker that fired the event
            object[] arrObjects =
            (object[])e.Argument;//Collect the array of objects the we received from the main thread

            string pstrRPTName = (string)arrObjects[0];
            string pStrPacket = (string)arrObjects[1];
            bool ShowReportViewer = (bool)arrObjects[2];

            string TableName = string.Empty;
            string BarcodeName = string.Empty;
            if (pstrRPTName.ToUpper() == "ABC")
            {
                TableName = "lot_id";
                BarcodeName = "lot_id";
            }

            BLL.Validation Val = new BLL.Validation();
            DataTable dt = new DataTable();


            if (BarcodeName.ToString() == "")
            {
                Global.ErrorMessage("Please Select Lot ID Format For This User");
                return;
            }

            dt = new MFGProcessIssue().RPTBarcodeGetdata(pStrPacket, BarcodeName);

            if (dt.Rows.Count > 0)
            {
                try
                {
                    DSLotID dsb = new DSLotID();
                    DataView dv = dt.DefaultView;
                    dv.Sort = "lot_id ASC";
                    DataTable sortedDT = dv.ToTable();
                    if (pstrRPTName.ToUpper() == "ABC")
                    {
                        XtraRptLotID report = new XtraRptLotID();

                        sortedDT.TableName = "DTLotID";
                        dsb.Tables.Clear();
                        dsb.Tables.Add(sortedDT.Clone());
                        dsb.Tables["DTLotID"].Merge(sortedDT);
                        report.DataSource = dsb;
                        report.ShowPrintMarginsWarning = false;
                        if (!ShowReportViewer)
                        {
                            using (ReportPrintTool printTool = new ReportPrintTool(report))
                            {
                                //printTool.Print();
                                printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
                            }
                        }
                        else
                        {
                            using (ReportPrintTool printTool = new ReportPrintTool(report))
                            {
                                //printTool.ShowRibbonPreviewDialog();
                                printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        //public static void myWorker_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    BackgroundWorker sendingWorker = (BackgroundWorker)sender;//Capture the BackgroundWorker that fired the event
        //    object[] arrObjects =
        //    (object[])e.Argument;//Collect the array of objects the we received from the main thread

        //    string pstrRPTName = (string)arrObjects[0];
        //    string pStrPacket = (string)arrObjects[1];
        //    bool ShowReportViewer = (bool)arrObjects[2];

        //    string TableName = string.Empty;
        //    string BarcodeName = string.Empty;
        //    if (pstrRPTName.ToUpper() == "RPTBARCODEPRINT.RPT")
        //    {
        //        TableName = "PACKET_MASTER";
        //        BarcodeName = "BARCODE";
        //    }
        //    else if (pstrRPTName.ToUpper() == "RPTALLPRINT.RPT")
        //    {
        //        TableName = "ALL_DATA";
        //        BarcodeName = "AFTER SAWING BARCODE";
        //    }
        //    else if (pstrRPTName.ToUpper() == "RPTPOLISHBARCODEPRINT.RPT")
        //    {
        //        TableName = "POLISH_BARCODE";
        //        BarcodeName = "POLISH BARCODE";
        //    }
        //    else if (pstrRPTName.ToUpper() == "RPTBARCODEPRINT_CURRENT.RPT")//Add By Sandip on 27-02-2019
        //    {
        //        TableName = "PACKET_MASTER";
        //        BarcodeName = "BARCODE 7";
        //    }
        //    else if (pstrRPTName.ToUpper() == "RPTSAWINGBARCODEPRINT.RPT")
        //    {
        //        TableName = "SAWING_BARCODE";
        //        BarcodeName = "SAWING BARCODE";
        //    }
        //    else if (pstrRPTName.ToUpper() == "XTRARPTBARCODE_CERT")
        //    {
        //        TableName = "CERTI_BARCODE";
        //        BarcodeName = "CERTI BARCODE";
        //    }


        //    BLL.Validation Val = new BLL.Validation();
        //    DataTable dt = new DataTable();

        //    if (("XTRARPTBARCODE_CERT,XTRARPTBARCODE_CERT_SPE,XTRARPTBARCODE_UNCERT,XTRARPTBARCODE_UNCERT_SPE,XTRARPTBARCODE_FMCERT,XTRARPTBARCODE_BN,XTRARPTBARCODEWITHOUT,XTRARPTBARCODE_QRCODE").Contains(pstrRPTName.ToUpper()))
        //    {
        //        if (MDI.MDIMain.FormTypeStr.ToUpper() == "TRADING") //Add By Sandip On 05-12-2018
        //            BarcodeName = "TRAD";
        //    }


        //    if (BarcodeName.ToString() == "")
        //    {
        //        Global.ErrorMessage("Please Select Barcode Format For This User");
        //        return;
        //    }

        //    if (("XTRARPTBARCODE_CERT,XTRARPTBARCODE_CERT_SPE,XTRARPTBARCODE_UNCERT,XTRARPTBARCODE_UNCERT_SPE,XTRARPTBARCODE_FMCERT,XTRARPTBARCODE_BN,XTRARPTBARCODEWITHOUT,XTRARPTBARCODE_QRCODE").Contains(pstrRPTName.ToUpper()))
        //    {
        //        if (MDI.MDIMain.FormTypeStr.ToUpper() == "TRADING") //Add By Sandip On 05-12-2018
        //            dt = new Trad_LiveStock().TradRPTBarcodeGetdata(pStrPacket, pstrRPTName.ToUpper());
        //        else
        //            dt = new SL_PacketMaster().RPTBarcodeGetdata(pStrPacket, BarcodeName); //Add By Sandip On 05-12-2018

        //        if (!dt.Columns.Contains("SEQ"))
        //        {
        //            dt.Columns.Add("SEQ", typeof(Int32));
        //        }

        //        string[] seq_string = pStrPacket.Trim().TrimEnd(',').TrimStart(',').Split(',');
        //        int seq_len = seq_string.Length;

        //        for (int i = 0; i < seq_string.Length; i++)
        //        {
        //            for (int j = 0; j < dt.Rows.Count; j++)
        //            {
        //                if (Convert.ToString(dt.Rows[j]["BARCODE"]) == seq_string[i])
        //                {
        //                    dt.Rows[j]["SEQ"] = i + 1;
        //                    break;
        //                }
        //            }
        //        }

        //        dt.AcceptChanges();

        //        DataView dv = dt.DefaultView;
        //        dv.Sort = "SEQ ASC";

        //        dt = dv.ToTable();

        //        dt.TableName = "DTBarcode";
        //        BarcodeName = "TRAD";
        //    }
        //    else
        //    {
        //        dt = new SL_PacketMaster().RPTBarcodeGetdata(pStrPacket, BarcodeName);
        //    }


        //    if (dt.Rows.Count > 0)
        //    {
        //        try
        //        {
        //            if (("XTRARPTBARCODE_CERT,XTRARPTBARCODE_CERT_SPE,XTRARPTBARCODE_UNCERT,XTRARPTBARCODE_UNCERT_SPE,XTRARPTBARCODE_FMCERT,XTRARPTBARCODE_BN,XTRARPTBARCODEWITHOUT,XTRARPTBARCODE_QRCODE").Contains(pstrRPTName.ToUpper()))
        //            {
        //                DSBarCode dsb = new DSBarCode();
        //                if (pstrRPTName.ToUpper() == "XTRARPTBARCODE_CERT")
        //                {
        //                    if (MDI.MDIMain.FormTypeStr.ToUpper() == "TRADING")
        //                    {
        //                        XtraRptBarcode_Cert report = new XtraRptBarcode_Cert();
        //                        dt.TableName = "DTBarcode";
        //                        dsb.Tables.Clear();
        //                        dsb.Tables.Add(dt.Clone());
        //                        dsb.Tables["DTBarcode"].Merge(dt);
        //                        report.DataSource = dsb;  // report.DataSource = dt;
        //                        report.ShowPrintMarginsWarning = false;


        //                        if (!ShowReportViewer)
        //                        {
        //                            using (ReportPrintTool printTool = new ReportPrintTool(report))
        //                            {
        //                                printTool.Print();
        //                            }
        //                        }
        //                        else
        //                        {
        //                            using (ReportPrintTool printTool = new ReportPrintTool(report))
        //                            {
        //                                printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        XtraRptBarcode_Cert_MFG report = new XtraRptBarcode_Cert_MFG();
        //                        report.Margins = new System.Drawing.Printing.Margins(15, 0, 0, 0);
        //                        dt.TableName = "DTBarcode";
        //                        dsb.Tables.Clear();
        //                        dsb.Tables.Add(dt.Clone());
        //                        dsb.Tables["DTBarcode"].Merge(dt);
        //                        report.DataSource = dsb;  // report.DataSource = dt;
        //                        report.ShowPrintMarginsWarning = false;


        //                        if (!ShowReportViewer)
        //                        {
        //                            using (ReportPrintTool printTool = new ReportPrintTool(report))
        //                            {
        //                                printTool.Print();
        //                            }
        //                        }
        //                        else
        //                        {
        //                            using (ReportPrintTool printTool = new ReportPrintTool(report))
        //                            {
        //                                printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
        //                            }
        //                        }
        //                    }
        //                }
        //                else if (pstrRPTName.ToUpper() == "XTRARPTBARCODE_QRCODE")
        //                {
        //                    if (MDI.MDIMain.FormTypeStr.ToUpper() == "TRADING")
        //                    {
        //                        XtraRptBarcode_QRCode report = new XtraRptBarcode_QRCode();
        //                        dt.TableName = "DTBarcode";
        //                        dsb.Tables.Clear();
        //                        dsb.Tables.Add(dt.Clone());
        //                        dsb.Tables["DTBarcode"].Merge(dt);
        //                        report.DataSource = dsb;  // report.DataSource = dt;
        //                        report.ShowPrintMarginsWarning = false;

        //                        if (!ShowReportViewer)
        //                        {
        //                            using (ReportPrintTool printTool = new ReportPrintTool(report))
        //                            {
        //                                printTool.Print();
        //                            }
        //                        }
        //                        else
        //                        {
        //                            using (ReportPrintTool printTool = new ReportPrintTool(report))
        //                            {
        //                                printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
        //                            }
        //                        }
        //                    }
        //                }
        //                else if (pstrRPTName.ToUpper() == "XTRARPTBARCODE_CERT_SPE")
        //                {
        //                    XtraRptBarcode_Cert_Spe report1 = new XtraRptBarcode_Cert_Spe();
        //                    dt.TableName = "DTBarcode";
        //                    dsb.Tables.Clear();
        //                    dsb.Tables.Add(dt.Clone());
        //                    dsb.Tables["DTBarcode"].Merge(dt);
        //                    report1.DataSource = dsb; // report1.DataSource = dt;
        //                    report1.ShowPrintMarginsWarning = false;
        //                    if (!ShowReportViewer)
        //                    {
        //                        using (ReportPrintTool printTool = new ReportPrintTool(report1))
        //                        {
        //                            printTool.Print();
        //                        }
        //                    }
        //                    else
        //                    {
        //                        using (ReportPrintTool printTool = new ReportPrintTool(report1))
        //                        {
        //                            printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
        //                        }
        //                    }
        //                }
        //                else if (pstrRPTName.ToUpper() == "XTRARPTBARCODE_UNCERT")
        //                {
        //                    XtraRptBarcode_UnCert report2 = new XtraRptBarcode_UnCert();
        //                    dt.TableName = "DTBarcode";
        //                    dsb.Tables.Clear();
        //                    dsb.Tables.Add(dt.Clone());
        //                    dsb.Tables["DTBarcode"].Merge(dt);
        //                    report2.DataSource = dsb;   // report2.DataSource = dt;
        //                    report2.ShowPrintMarginsWarning = false;
        //                    if (!ShowReportViewer)
        //                    {
        //                        using (ReportPrintTool printTool = new ReportPrintTool(report2))
        //                        {
        //                            printTool.Print();
        //                        }
        //                    }
        //                    else
        //                    {
        //                        using (ReportPrintTool printTool = new ReportPrintTool(report2))
        //                        {
        //                            printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
        //                        }
        //                    }
        //                }
        //                else if (pstrRPTName.ToUpper() == "XTRARPTBARCODE_UNCERT_SPE")
        //                {
        //                    XtraRptBarcode_UnCert_SPE report3 = new XtraRptBarcode_UnCert_SPE();
        //                    dt.TableName = "DTBarcode";
        //                    dsb.Tables.Clear();
        //                    dsb.Tables.Add(dt.Clone());
        //                    dsb.Tables["DTBarcode"].Merge(dt);
        //                    report3.DataSource = dsb; // report3.DataSource = dt;
        //                    report3.ShowPrintMarginsWarning = false;
        //                    if (!ShowReportViewer)
        //                    {
        //                        using (ReportPrintTool printTool = new ReportPrintTool(report3))
        //                        {
        //                            printTool.Print();
        //                        }
        //                    }
        //                    else
        //                    {
        //                        using (ReportPrintTool printTool = new ReportPrintTool(report3))
        //                        {
        //                            printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
        //                        }
        //                    }
        //                }
        //                else if (pstrRPTName.ToUpper() == "XTRARPTBARCODE_FMCERT")
        //                {
        //                    XtraRptBarcode_FMCert report4 = new XtraRptBarcode_FMCert();
        //                    dt.TableName = "DTBarcode";
        //                    dsb.Tables.Clear();
        //                    dsb.Tables.Add(dt.Clone());
        //                    dsb.Tables["DTBarcode"].Merge(dt);
        //                    report4.DataSource = dsb; // report3.DataSource = dt;
        //                    report4.ShowPrintMarginsWarning = false;
        //                    if (!ShowReportViewer)
        //                    {
        //                        using (ReportPrintTool printTool = new ReportPrintTool(report4))
        //                        {
        //                            printTool.Print();
        //                        }
        //                    }
        //                    else
        //                    {
        //                        using (ReportPrintTool printTool = new ReportPrintTool(report4))
        //                        {
        //                            printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
        //                        }
        //                    }
        //                }
        //                else if (pstrRPTName.ToUpper() == "XTRARPTBARCODEWITHOUT")
        //                {
        //                    XtraRptBarcodeWithout report3 = new XtraRptBarcodeWithout();
        //                    dt.TableName = "DTBarcode";
        //                    dsb.Tables.Clear();
        //                    dsb.Tables.Add(dt.Clone());
        //                    dsb.Tables["DTBarcode"].Merge(dt);
        //                    report3.DataSource = dsb; // report3.DataSource = dt;
        //                    report3.ShowPrintMarginsWarning = false;
        //                    if (!ShowReportViewer)
        //                    {
        //                        using (ReportPrintTool printTool = new ReportPrintTool(report3))
        //                        {
        //                            printTool.Print();
        //                        }
        //                    }
        //                    else
        //                    {
        //                        using (ReportPrintTool printTool = new ReportPrintTool(report3))
        //                        {
        //                            printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
        //                        }
        //                    }
        //                }
        //                else if (pstrRPTName.ToUpper() == "XTRARPTBARCODE_BN")
        //                {
        //                    XtraRptBarcode_BN report5 = new XtraRptBarcode_BN();
        //                    dt.TableName = "DTBarcode";
        //                    dsb.Tables.Clear();
        //                    dsb.Tables.Add(dt.Clone());
        //                    dsb.Tables["DTBarcode"].Merge(dt);
        //                    report5.DataSource = dsb; // report3.DataSource = dt;
        //                    report5.ShowPrintMarginsWarning = false;
        //                    if (!ShowReportViewer)
        //                    {
        //                        using (ReportPrintTool printTool = new ReportPrintTool(report5))
        //                        {
        //                            printTool.Print();
        //                        }
        //                    }
        //                    else
        //                    {
        //                        using (ReportPrintTool printTool = new ReportPrintTool(report5))
        //                        {
        //                            printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
        //                        }
        //                    }
        //                }

        //            }
        //            else
        //            {
        //                DSBarCode dsb = new DSBarCode();
        //                DataView dv = dt.DefaultView;
        //                dv.Sort = "BARCODE ASC";
        //                DataTable sortedDT = dv.ToTable();
        //                if (pstrRPTName.ToUpper() == "RPTBARCODEPRINT.RPT" || pstrRPTName.ToUpper() == "RPTBARCODEPRINT_CURRENT.RPT")
        //                {
        //                    XtraRptBarcode report = new XtraRptBarcode();
        //                    DataSet DsBarcode = new DataSet();
        //                    DsBarcode.Tables.Clear();
        //                    DsBarcode.Clear();
        //                    DsBarcode.Tables.Add(dt);
        //                    sortedDT.TableName = "DTBarcode";
        //                    dsb.Tables.Clear();
        //                    dsb.Tables.Add(sortedDT.Clone());
        //                    dsb.Tables["DTBarcode"].Merge(sortedDT);
        //                    report.DataSource = dsb;
        //                    report.ShowPrintMarginsWarning = false;
        //                    if (!ShowReportViewer)
        //                    {
        //                        using (ReportPrintTool printTool = new ReportPrintTool(report))
        //                        {
        //                            printTool.Print();
        //                        }
        //                    }
        //                    else
        //                    {
        //                        using (ReportPrintTool printTool = new ReportPrintTool(report))
        //                        {
        //                            printTool.ShowRibbonPreviewDialog();
        //                            printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
        //                        }
        //                    }
        //                }
        //                else if (pstrRPTName.ToUpper() == "RPTPOLISHBARCODEPRINT.RPT")
        //                {
        //                    XtraRptPolish report = new XtraRptPolish();
        //                    sortedDT.TableName = "DTPolishBarcode";
        //                    dsb.Tables.Clear();
        //                    dsb.Tables.Add(sortedDT.Clone());
        //                    dsb.Tables["DTPolishBarcode"].Merge(sortedDT);
        //                    report.DataSource = dsb;
        //                    report.ShowPrintMarginsWarning = false;
        //                    if (!ShowReportViewer)
        //                    {
        //                        using (ReportPrintTool printTool = new ReportPrintTool(report))
        //                        {
        //                            printTool.Print();
        //                        }
        //                    }
        //                    else
        //                    {
        //                        using (ReportPrintTool printTool = new ReportPrintTool(report))
        //                        {
        //                            printTool.ShowRibbonPreviewDialog();
        //                            printTool.ShowRibbonPreviewDialog(UserLookAndFeel.Default);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception et) { }
        //    }
        //}

        public static void myWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }
        public static void myWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }


    }//Global Method

    public static class Validator
    {
        static Regex ValidEmailRegex = CreateValidEmailRegex();
        /// <returns></returns>
        private static Regex CreateValidEmailRegex()
        {
            string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return new Regex(validEmailPattern, RegexOptions.IgnoreCase);
        }

        internal static bool EmailIsValid(string emailAddress)
        {
            bool isValid = ValidEmailRegex.IsMatch(emailAddress);

            return isValid;
        }
    }
}