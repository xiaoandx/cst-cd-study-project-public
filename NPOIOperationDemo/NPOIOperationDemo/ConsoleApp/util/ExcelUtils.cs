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
using ConsoleApp.entity;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Eval;
using NPOI.SS.UserModel;
using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace ConsoleApp.NPOIOperation
{

    /// <summary>
    /// Excel操作工具类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExcelUtils<T> where T : class
    {
        /// <summary>
        /// 保存Excel文件
        /// </summary>
        /// <param name="excelName">Excel文件名</param>
        /// <param name="sheetName">Sheet工作表名</param>
        /// <param name="data">实体类对象</param>
        public static void SaveExcelFile(string excelName, string sheetName, List<T> data)
        {
            //创建一个Excel文档
            IWorkbook workBook = new HSSFWorkbook();
            //创建一个工作表Sheet
            ISheet sheet = workBook.CreateSheet(sheetName); 

            int rowNum = 0;
            //LastRowNum记录当前可用写入的行索引
            var row = sheet.CreateRow(sheet.LastRowNum);
            //获取这个实体对象的所有属性
            PropertyInfo[] preInfo = typeof(T).GetProperties();
            foreach (var item in preInfo)
            {
                //获取当前属性的自定义特性列表
                object[] objPres = item.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (objPres.Length > 0)
                {
                    for (int i = 0; i < objPres.Length; i++)
                    {
                        //创建行，将当前自定义特性写入
                        row.CreateCell(rowNum).SetCellValue(((DescriptionAttribute)objPres[i]).Description);
                        //行索引加1，下次往后一格创建行
                        rowNum++;
                    }
                }
            }

            int j = sheet.LastRowNum + 1, columnNum = 0;
            foreach (var item in data)
            {
                columnNum = 0;
                row = sheet.CreateRow(j++);
                //获取当前对象的属性列表
                var itemProps = item.GetType().GetProperties();  
                foreach (var itemPropSub in itemProps)
                {
                    //获取当前对象特性中的自定义特性[Description("自定义特性")]
                    var objs = itemPropSub.GetCustomAttributes(typeof(DescriptionAttribute), true);
                    if (objs.Length > 0)
                    {
                        //将当前对象的特性值，插入当前行的第n列单元格
                        row.CreateCell(columnNum).SetCellValue(itemPropSub.GetValue(item, null) == null ? "" :
                            itemPropSub.GetValue(item, null).ToString());
                        columnNum++;
                    }
                }
            }

            //文件流写入计算机磁盘
            using (MemoryStream ms = new MemoryStream())
            {
                workBook.Write(ms,true);
                using (FileStream fs = new FileStream(excelName, FileMode.Create, FileAccess.Write))
                {
                    ms.WriteTo(fs);
                }
                ms.Flush();
                ms.Position = 0;
                workBook.Close();
            }
        }

        /// <summary>
        /// 读取excel
        /// </summary>
        /// <param name="strFileName">excel文件路径</param>
        /// <param name="sheet">需要导出的sheet</param>
        /// <param name="HeaderRowIndex">列头所在行号，-1表示没有列头</param>
        /// <returns></returns>
        public static List<T> ImportExcel(string strFileName, string SheetName, int HeaderRowIndex)
        {
            HSSFWorkbook workbook;
            IWorkbook wb;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                wb = new HSSFWorkbook(file);
            }
            ISheet sheet = wb.GetSheet(SheetName);
            Console.WriteLine(sheet.ToString());
            /*//ExcelFileStream.Close();
            workbook = null;
            sheet = null;*/
            return null ;
        }
    }
}
















