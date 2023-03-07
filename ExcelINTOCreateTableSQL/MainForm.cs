///-------------------------------------------------------------------------------------------------
/// Copyright (c) 2023 WEI.ZHOU. All rights reserved.
/// The following code is only used for learning and communication, not for illegal and
/// commercial use.
/// If the code is used, no consent is required, but the author has nothing to do with any problems
/// and consequences.
/// In case of code problems, feedback can be made through the following email address.
/// 
///                        <wei.zhou@ccssttcn.com>
///-------------------------------------------------------------------------------------------------                        
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ExcelINTOCreateTableSQL
{
    /// <summary>
    /// Excel data field data conversion SQL script
    /// </summary>
    public partial class MainForm : Form
    {
        private ExcelUtils _mExcel = new ExcelUtils();
        private CreateSQLUtil _createSQL = new CreateSQLUtil();
        private (IWorkbook workbook, IFormulaEvaluator evaluator) _work;
        private List<RadioButton> _OptionBoxGroup;
        private int _SelectDBType = 0;
        private string[] _excelfileURL;
        private int _selectFileCount;
        private string _filesPathString;

        public MainForm()
        {
            InitializeComponent();
            InitializationSelection();
        }

        #region type selector

        /// <summary>
        /// Initializes the database type selector
        /// </summary>
        private void InitializationSelection()
        {
            _OptionBoxGroup = new List<RadioButton>();
            _OptionBoxGroup.AddRange(new RadioButton[] { rbtnDBTypeSQLServer, rbtnDBTypeOracle });
            for (int i = 0; i < _OptionBoxGroup.Count; i++)
            {
                _OptionBoxGroup[i].CheckedChanged += _OptionBoxGroup_CheckedChanged;
            }
        }

        /// <summary>
        /// database type selector click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _OptionBoxGroup_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < _OptionBoxGroup.Count; i++)
            {
                if (_OptionBoxGroup[i].Checked)
                {
                    _SelectDBType = i;
                    break;
                }
            }
        }

        #endregion

        /// <summary>
        /// Select the Excel file that you want to convert click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.FileName = "";
            this.openFileDialog1.Filter = "*.xlsx|*.xlsx";
            this.openFileDialog1.Multiselect = true;
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _excelfileURL = openFileDialog1.FileNames;
                _selectFileCount = _excelfileURL.Length;
                _filesPathString = StringArrayToString(_excelfileURL);
                txtExcelFilePathURLSet.Text = _filesPathString;
            }
        }

        /// <summary>
        /// An array of strings is converted to a string
        /// </summary>
        /// <param name="strArray">string Array</param>
        /// <returns>string</returns>
        private string StringArrayToString(string[] strArray)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string str in strArray)
            {
                sb.Append(str);
                sb.Append("\r\n");
            }
            return sb.ToString();
        }

        /// <summary>
        /// Clear All Files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearExcelFilePath_Click(object sender, EventArgs e)
        {
            txtExcelFilePathURLSet.Text = "";
            MessageBox.Show("File list is cleared successfully");
        }

        /// <summary>
        /// Transform SQL statement
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butConversion_Click(object sender, EventArgs e)
        {
            StringBuilder allFileSQL = new StringBuilder();
            for (int i = 0; i < _selectFileCount; i++)
            {
                // Open excel
                _work = _mExcel.GetWorkbook(_excelfileURL[i]);
                // Get all sheets
                List<string> sheetNames = _mExcel.GetSheetNames(_work.workbook);
                ISheet sheet = _work.workbook.GetSheet(sheetNames[1]);
                // Get table name
                string tatbleName = _mExcel.GetCellValue<string>(sheet, _work.evaluator, "E1");
                // Fields count
                int rowCount = sheet.LastRowNum + 1;
                // A field information is all in the array, all the field information array is stored in the collection
                List<string[]> fieldInformationSet = _createSQL.GetFieldInformationSet(rowCount, sheet, _work.evaluator);
                // Concatenated sql
                StringBuilder SingleFileSQL = _createSQL.ParseListConcatenatesSQL(tatbleName, fieldInformationSet, _SelectDBType);

                allFileSQL.Append(SingleFileSQL);
                allFileSQL.Append("\r\n");
            }
            textCreateSQLStatement.Text = allFileSQL.ToString();
        }

        /// <summary>
        /// copy SQL click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butCopySQL_Click(object sender, EventArgs e)
        {
            if (textCreateSQLStatement.Text != "")
                Clipboard.SetDataObject(textCreateSQLStatement.Text);
            MessageBox.Show("复制SQL成功\r\n进行粘贴使用！");
        }
    }
}
