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
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    /// <summary>
    /// NPOI操作Excel工具集合
    /// </summary>
    public class ExcelUtils
    {

        /// <summary>
        /// 保存excel
        /// </summary>
        /// <param name="FileName">保存文件名称</param>
        /// <param name="data">保存数据</param>
        public void WriteExcel(string FileName, DataTable data)
        {
            IWorkbook workbook;
            string fileExt = Path.GetExtension(FileName).ToLower();
            if (fileExt == ".xlsx") { workbook = new XSSFWorkbook(); }
            else if (fileExt == ".xls") { workbook = new HSSFWorkbook(); }
            else { return; }

            ISheet sheet = workbook.CreateSheet(data.TableName);

            // 表头  
            IRow row = sheet.CreateRow(0);
            for (int i = 0; i < data.Columns.Count; i++)
            {
                row.CreateCell(i).SetCellValue(data.Columns[i].ColumnName);
            }
            // 数据  
            for (int i = 0; i < data.Rows.Count; i++)
            {
                IRow row1 = sheet.CreateRow(i + 1);
                for (int j = 0; j < data.Columns.Count; j++)
                {
                    row1.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                }
            }

            //转为字节数组  
            MemoryStream stream = new MemoryStream();
            workbook.Write(stream);
            var buf = stream.ToArray();

            //保存为Excel文件  
            using (FileStream fs = new FileStream(FileName, FileMode.Create, FileAccess.Write))
            {
                fs.Write(buf, 0, buf.Length);
                fs.Flush();
                fs.Close();
            }
        }

        /// <summary>
        /// 保存CSV文件
        /// </summary>
        /// <param name="dt">数据DataTable</param>
        /// <param name="fileName">文件全名</param>
        /// <returns>true保存成功 false保存失败</returns>
        public bool SaveCSV(string fullFileName, DataTable dt)
        {
            Boolean r = false;
            FileStream fs;
            StreamWriter sw;
            using (fs = new FileStream(fullFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write))
            {
                sw = new StreamWriter(fs, System.Text.Encoding.Default);
            }
            string data = "";

            //写出列名称
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                data += dt.Columns[i].ColumnName.ToString();
                if (i < dt.Columns.Count - 1)
                {
                    data += ",";
                }
            }
            sw.WriteLine(data);

            //写出各行数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                data = "";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    data += dt.Rows[i][j].ToString();
                    if (j < dt.Columns.Count - 1)
                    {
                        data += ",";
                    }
                }
                sw.WriteLine(data);
            }
            r = true;
            return r;
        }

        /// <summary>
        /// 获取Workbook
        /// </summary>
        /// <param name="excelurl">文件名称路径</param>
        /// <returns></returns>
        public (IWorkbook workbook, IFormulaEvaluator evaluator) GetWorkbook(string excelurl)
        {
            IWorkbook workbook = null;
            IFormulaEvaluator evaluator = null;

            //判断文件是否存在
            if (!File.Exists(excelurl))
            {
                return (null, null);
            }

            Stream fs = System.IO.File.OpenRead(excelurl);
            if (excelurl.LastIndexOf(".xlsx") > 0) // 2007版本
            {
                workbook = new XSSFWorkbook(fs);
                evaluator = new XSSFFormulaEvaluator(workbook);
            }
            else if (excelurl.IndexOf(".xls") > 0) // 2003版本
            {
                workbook = new HSSFWorkbook(fs);
                evaluator = new HSSFFormulaEvaluator(workbook);
            }
            if (fs != null)
            {
                fs.Close();
                fs.Dispose();
                fs = null;
            }
            return (workbook, evaluator);
        }

        /// <summary>
        /// 获取工作表列表
        /// </summary>
        /// <param name="workbook"></param>
        /// <returns></returns>
        public List<string> GetSheetNames(IWorkbook workbook)
        {
            List<string> sh = new List<string>();
            try
            {
                var SheetsNames = workbook.GetEnumerator();
                while (SheetsNames.MoveNext()) //一定要先movenext,不然会有异常
                {
                    sh.Add(SheetsNames.Current.SheetName.ToString());
                }
            }
            catch { }
            return sh;
        }

        /// <summary>
        /// 读取指定坐标的单元格数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sheet"></param>
        /// <param name="evaluator"></param>
        /// <param name="coord"></param>
        /// <returns></returns>
        public T GetCellValue<T>(ISheet sheet, IFormulaEvaluator evaluator, string coord)
        {
            #region 备注
            //Unknown 未知     -1,
            //Numeric 数值型   0,
            //String  字符串型 1,
            //Formula 公式型   2,
            //Blank   空值     3,
            //Boolean 布尔型   4,
            //Error   错误     5
            #endregion

            if (coord == null || coord == "")
                return (T)Convert.ChangeType("", typeof(T));

            coord = coord.ToUpper();
            if (Regex.Matches(coord, @"\D+").Count == 0) // 提取字母
                return (T)Convert.ChangeType("", typeof(T));
            if (Regex.Matches(coord, @"\d+").Count == 0) // 提取数字
                return (T)Convert.ChangeType("", typeof(T));

            string value = null;
            var cr = new CellReference(coord);

            IRow row = sheet.GetRow(cr.Row);
            ICell cell = null;
            if (row == null && cr.Row >= 0)
            {
                row = sheet.CreateRow(cr.Row);
            }
            if (row != null)
            {
                cell = row.GetCell(cr.Col);
                if (cell == null)
                {
                    cell = row.CreateCell(cr.Col);
                }
            }

            if (cell != null)
            {
                #region 判断是否为合并单元格
                if (cell.IsMergedCell)
                {
                    for (int i = 0; i < sheet.NumMergedRegions; i++)//遍历所有的合并单元格
                    {
                        var cellRange = cell.Sheet.GetMergedRegion(i);

                        //判断查询的单元格是否在合并单元格内
                        if (cell.ColumnIndex >= cellRange.FirstColumn &&
                            cell.ColumnIndex <= cellRange.LastColumn &&
                            cell.RowIndex >= cellRange.FirstRow &&
                            cell.RowIndex <= cellRange.LastRow)
                        {
                            cell = sheet.GetRow(cellRange.FirstRow)?.GetCell(cellRange.FirstColumn);
                            break;
                        }
                    }
                }
                #endregion

                switch (cell.CellType)
                {
                    case CellType.Formula: // 公式型
                        switch (cell.CachedFormulaResultType)
                        {
                            case CellType.Numeric:
                                // 公式型日期和公式型数字无法明确区分，根据测试结果，公式型日期，时分秒基本都有值
                                if (DateUtil.IsCellDateFormatted(cell))
                                {
                                    value = cell.DateCellValue.ToString("yyyy-MM-dd HH:mm:ss");
                                }
                                else
                                {
                                    value = cell.NumericCellValue.ToString();
                                }
                                break;
                            case CellType.String:
                                value = cell.StringCellValue;
                                break;

                            //case CellType.Blank:  // 空值
                            //    value = "";
                            //    break;
                            //case CellType.Error:  // 错误
                            //    value = "";
                            //    break;

                            default:
                                // 这边报错一直没解决
                                try { value = evaluator.Evaluate(cell).FormatAsString(); } catch { }
                                break;
                        }
                        if (value == null) value = "";
                        break;
                    case CellType.Blank:  // 空值
                        value = "";
                        break;
                    case CellType.Numeric:
                        if (DateUtil.IsCellDateFormatted(cell))
                        {
                            value = cell.DateCellValue.ToString("yyyy-MM-dd HH:mm:ss");
                            if (value.Contains(" 00:00:00")) value = value.Replace(" 00:00:00", "");
                        }
                        else
                        {
                            short format = cell.CellStyle.DataFormat;
                            if (format == 14 || format == 31 || format == 57 || format == 58)
                            {
                                value = cell.DateCellValue.ToString("yyyy-MM-dd HH:mm:ss");
                                if (value.Contains(" 00:00:00")) value = value.Replace(" 00:00:00", "");
                            }
                            else
                                value = cell.NumericCellValue.ToString();
                        }
                        break;
                    case CellType.String:
                        value = cell.StringCellValue;
                        break;
                    default:
                        value = sheet.GetRow(cr.Row)?.GetCell(cr.Col)?.ToString();
                        break;
                }
            }

            if (value == null) value = "";
            return (T)Convert.ChangeType(value, typeof(T));
        }

        /// <summary>
        /// excel坐标转换
        /// </summary>
        /// <param name="start"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        private string ColCoord(string start, int num)
        {
            int Dif = start[0] - 'A';
            int ColNum = num + Dif;
            return $"{(ColNum >= 26 ? "A" : "")}{Convert.ToChar('A' + (ColNum >= 26 ? ColNum - 26 : ColNum)).ToString()}";
        }

        /// <summary>
        /// 导入读取excel文件
        /// </summary>
        /// <param name="FileName"></param>
        public void ReadExcel(ISheet sheet, IFormulaEvaluator evaluator, DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();

            int rowCount = sheet.LastRowNum + 1;// 判断有多少行
            int colCount = 0; // 最大列数

            // 找出最大列数，避免因为每行的列数不一致导致采样数据不准
            for (int i = 0; i < rowCount; i++)
            {
                int max = sheet.GetRow(i).LastCellNum;
                if (max > colCount) colCount = max;
            }

            // 读取标题行
            for (int j = 0; j < colCount; j++)
            {
                string data = GetCellValue<string>(sheet, evaluator, $"{ColCoord("A", j)}1");
                dataGridView.Columns.Add(j.ToString() + data, data);
            }

            // 读取数据行
            for (int i = 2; i <= rowCount; i++)      //行循环
            {
                int index = dataGridView.Rows.Add();
                for (int j = 0; j < colCount; j++)  //列循环
                {
                    string data = GetCellValue<string>(sheet, evaluator, $"{ColCoord("A", j)}{i}");
                    dataGridView.Rows[index].Cells[j].Value = data;
                }
            }
        }

        /// <summary>
        /// 打开CSV 文件
        /// </summary>
        /// <param name="fileName">文件全名</param>
        /// <returns>DataTable</returns>
        public DataTable OpenCSV(string fullFileName)
        {
            return OpenCSV(fullFileName, 0, 0, 0, 0, true);
        }

        /// <summary>
        /// 打开CSV 文件
        /// </summary>
        /// <param name="fileName">文件全名</param>
        /// <param name="firstRow">开始行</param>
        /// <param name="firstColumn">开始列</param>
        /// <param name="getRows">获取多少行</param>
        /// <param name="getColumns">获取多少列</param>
        /// <param name="haveTitleRow">是有标题行</param>
        /// <returns>DataTable</returns>
        public DataTable OpenCSV(string fullFileName, Int16 firstRow = 0, Int16 firstColumn = 0, Int16 getRows = 0, Int16 getColumns = 0, bool haveTitleRow = true)
        {
            DataTable dt = new DataTable();
            FileStream fs;
            StreamReader sr;
            using (fs = new FileStream(fullFileName, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                sr = new StreamReader(fs, System.Text.Encoding.Default);
            }

            //记录每次读取的一行记录
            string strLine = "";
            //记录每行记录中的各字段内容
            string[] aryLine;
            //标示列数
            int columnCount = 0;
            //是否已建立了表的字段
            bool bCreateTableColumns = false;
            //第几行
            int iRow = 1;

            //去除无用行
            if (firstRow > 0)
            {
                for (int i = 1; i < firstRow; i++)
                {
                    sr.ReadLine();
                }
            }
            string[] separators = { "," };
            //逐行读取CSV中的数据
            while ((strLine = sr.ReadLine()) != null)
            {
                strLine = strLine.Trim();
                aryLine = strLine.Split(separators, System.StringSplitOptions.RemoveEmptyEntries);
                if (aryLine.Length == 0) { continue; }

                if (bCreateTableColumns == false)
                {
                    bCreateTableColumns = true;
                    columnCount = aryLine.Length;
                    //创建列
                    for (int i = firstColumn; i < (getColumns == 0 ? columnCount : firstColumn + getColumns); i++)
                    {
                        DataColumn dc
                            = new DataColumn(haveTitleRow == true ? aryLine[i] : "COL" + i.ToString());
                        dt.Columns.Add(dc);
                    }

                    bCreateTableColumns = true;

                    if (haveTitleRow == true)
                    {
                        continue;
                    }
                }

                DataRow dr = dt.NewRow();
                for (int j = firstColumn; j < (getColumns == 0 ? columnCount : firstColumn + getColumns); j++)
                {
                    dr[j - firstColumn] = aryLine[j];
                }
                dt.Rows.Add(dr);

                iRow = iRow + 1;
                if (getRows > 0)
                {
                    if (iRow > getRows)
                    {
                        break;
                    }
                }

            }

            sr.Close();
            fs.Close();
            return dt;
        }

        /// <summary>
        /// CSV转化为DataTable
        /// </summary>
        /// <param name="filePath">CSV文件路径</param>
        /// <returns></returns>
        public DataTable CSVToDataTable(string filePath = null)
        {
            // Open explorer select file (.CSV | .csv)
            if (string.IsNullOrEmpty(filePath))
            {
                var ofd = new OpenFileDialog
                {
                    Filter = "CSV (逗号分隔)(*.csv)|*.csv",
                    DefaultExt = "csv"
                };
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    filePath = ofd.FileName;
                }
                else
                {
                    return null;
                }
            }

            DataTable dt = new DataTable();
            int intColCount = 0;
            bool isHeaderColumn = true;
            DataColumn column;
            DataRow row;
            string strline;

            using (var reader = new StreamReader(filePath, Encoding.UTF8))
            {
                while (!string.IsNullOrEmpty(strline = reader.ReadLine()))
                {
                    string[] aryline = strline.Split(new char[] { ',' });
                    if (isHeaderColumn)
                    {
                        isHeaderColumn = false;
                        intColCount = aryline.Length;
                        for (int i = 0; i < aryline.Length; i++)
                        {
                            column = new DataColumn(aryline[i]);
                            dt.Columns.Add(column);
                        }
                        continue;
                    }

                    // Set data is converted to row data for saving （集合数据转为行数据进行保存）
                    row = dt.NewRow();
                    for (int i = 0; i < aryline.Length; i++)
                    {
                        row[i] = aryline[i];
                    }
                    dt.Rows.Add(row);
                }
            }
            return dt;
        }
    }
}
