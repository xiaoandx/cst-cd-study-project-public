/*
 * Copyright (c) 2023 WEI.ZHOU. All rights reserved.
 * The following code is only used for learning and communication, not for illegal and
 * commercial use.
 * If the code is used, no consent is required, but the author has nothing to do with any problems
 * and consequences.
 * In case of code problems, feedback can be made through the following email address.
 *
 *                        <wei.zhou@ccssttcn.com>
 */
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class NPOIOperation : Form
    {
        public NPOIOperation()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 表格中添加数据
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            DataGridView table = dataGridView1;
            int cel = 0;
            int index = table.Rows.Add();
            table.Rows[index].Cells[cel++].Value = table.Rows.Count + 1;
            table.Rows[index].Cells[cel++].Value = "a";
            table.Rows[index].Cells[cel++].Value = "b";
            table.Rows[index].Cells[cel++].Value = "c";
        }

        /// <summary>
        /// 导出数据到XLSX文件
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            new ExcelUtils().WriteExcel("xlsxFileDemo.xlsx", GetDgvToTable("Sheet1", dataGridView1));
            MessageBox.Show("XLSX文件导出成功！", "导出结果", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //new Excel().WriteExcel("AAA.csv", GetDgvToTable("tab1", dataGridView1));
        }

        /// <summary>
        /// 导出数据到XLS文件
        /// </summary>
        private void button5_Click(object sender, EventArgs e)
        {
            new ExcelUtils().WriteExcel("xlsFileDemo.xls", GetDgvToTable("Sheet1", dataGridView1));
            MessageBox.Show("XLS文件导出成功！", "导出结果", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// 将DataGridView转换成DataTable
        /// </summary>
        public DataTable GetDgvToTable(string TabName, DataGridView dgv)
        {
            DataTable dt = new DataTable();
            dt.TableName = TabName;
            // 列强制转换
            for (int count = 0; count < dgv.Columns.Count; count++)
            {
                DataColumn dc = new DataColumn(dgv.Columns[count].Name.ToString());
                dt.Columns.Add(dc);
            }
            // 循环行
            for (int count = 0; count < dgv.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow();
                for (int countsub = 0; countsub < dgv.Columns.Count; countsub++)
                {
                    dr[countsub] = Convert.ToString(dgv.Rows[count].Cells[countsub].Value);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        private ExcelUtils mExcel = new ExcelUtils();
        private (IWorkbook workbook, IFormulaEvaluator evaluator) work;

        /// <summary>
        /// 导入excel
        /// </summary>
        private void button4_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.FileName = "";
            this.openFileDialog1.Filter = "*.xlsx|*.xlsx|*.xls|*.xls|所有文件(*.*)|*.*";
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // 打开excel
                work = mExcel.GetWorkbook(this.openFileDialog1.FileName);
                // 获取所有工作表
                List<string> sheetNames = mExcel.GetSheetNames(work.workbook);
                // 默认读取第一个工作表内容
                if (sheetNames.Count > 0)
                {
                    ISheet sheet = work.workbook.GetSheet(sheetNames[0]);
                    mExcel.ReadExcel(sheet, work.evaluator, dataGridView1);
                }
            }
        }

        /// <summary>
        /// 导出数据为CSV格式文件
        /// </summary>
        private void button7_Click(object sender, EventArgs e)
        {
            bool saveState = new ExcelUtils().SaveCSV("CSVFileDemo.csv", GetDgvToTable("Sheet1", dataGridView1));
            MessageBox.Show("CSV文件导出成功！", "导出结果", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 导入CSV数据
        /// </summary>
        private void button6_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.FileName = "";
            this.openFileDialog1.Filter = "*.CSV|*.csv";
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                DataTable dataTable = new ExcelUtils().CSVToDataTable(this.openFileDialog1.FileName);
                Console.WriteLine("HEllo");
                DataGridView table = dataGridView1;
                int index = table.Rows.Add();
                for (int i = index, k = 0; i < dataTable.Rows.Count; i++, k++)
                {
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                        table.Rows[i].Cells[j].Value = dataTable.Rows[k][j].ToString();
                    }
                    table.Rows.Add();
                }
            }
        }
    }
}
