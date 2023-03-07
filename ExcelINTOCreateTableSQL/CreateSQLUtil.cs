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
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ExcelINTOCreateTableSQL
{
    /// <summary>
    /// Excel data tables are converted to SQL script tool classes
    /// </summary>
    public class CreateSQLUtil
    {
        /// <summary>
        /// Field information extraction and conversion are saved in string [], and all field information arrays are saved
        /// in List collection for subsequent SQL concatenation ## 字段信息抽取转换保存在string【】，所有的字段信息数组保存在List集合
        /// 中用于后续拼接SQL使用
        /// </summary>
        /// <param name="numberField">Field count</param>
        /// <param name="sheet">sheet object</param>
        /// <param name="evaluator">IFormulaEvaluator object</param>
        /// <returns>All the field information arrays are stored in the List collection
        /// #所有的字段信息数组保存在List集合</returns>
        public List<string[]> GetFieldInformationSet(int numberField, ISheet sheet, IFormulaEvaluator evaluator) {
            ExcelUtils mExcel = new ExcelUtils();
            List<string[]> sqlRowList = new List<string[]>();
            for (int i = 4; i < numberField - 2; i++)
            {
                string[] sqlRow = new string[9];
                for (int j = 0, k = 0; j < 9; j++, k++)
                {
                    string cellValue = mExcel.GetCellValue<string>(sheet, evaluator, $"{mExcel.ColCoord("A", j)}{i}");
                    if (!"".Equals(cellValue))
                    {
                        sqlRow[k] = cellValue;
                        Debug.WriteLine(cellValue);
                    }
                }
                if (sqlRow[3] != null)
                {
                    sqlRowList.Add(sqlRow);
                }
            }
            return sqlRowList;
        }

        /// <summary>
        /// Parse field information List collection concatenates SQL statements
        /// </summary>
        /// <param name="tableName">Table Name</param>
        /// <param name="fieldInformationSet">All the field information arrays are stored in the List collection</param>
        /// <param name="SelectDBType">Database type</param>
        /// <returns>SQL script from a single Excel file</returns>
        public StringBuilder ParseListConcatenatesSQL(string tableName, List<string[]> fieldInformationSet, int SelectDBType) {
            //拼接sql
            string sqlHead = $"create table {tableName}(\r\n";
            StringBuilder sqlBoby = new StringBuilder();
            sqlBoby.Append(sqlHead);
            for (int x = 0; x < fieldInformationSet.Count; x++)
            {
                sqlBoby.Append(ConcatenatedRow(fieldInformationSet[x], SelectDBType));
            }
            if (SelectDBType == 1) {
                if (!string.IsNullOrEmpty(fieldInformationSet[0][1])) {
                    sqlBoby.Append($"\tconstraint TF_B_AIR_CONFIG_PK primary key({fieldInformationSet[0][3]}),\r\n");
                }
            }
            sqlBoby.Remove(sqlBoby.Length - 1, 1);
            sqlBoby.Remove(sqlBoby.Length - 1, 1);
            sqlBoby.Remove(sqlBoby.Length - 1, 1);
            sqlBoby.Append("\r\n)");
            return sqlBoby;
        }

        /// <summary>
        /// Field script splicing
        /// </summary>
        /// <param name="fieldInformationArray">FieldInformationArray #字段信息数组</param>
        /// <param name="SelectDBType">Database type</param>
        /// <returns>SQL script string that concatenates all fields</returns>
        public string ConcatenatedRow(string[] fieldInformationArray, int SelectDBType)
        {
            StringBuilder fieldSql = new StringBuilder();
            fieldSql.Append("\t" + fieldInformationArray[3] + " ");

            if (SelectDBType == 0)
                fieldSql.Append(fieldInformationArray[6] + " ");
            else
                fieldSql.Append(fieldInformationArray[5] + " ");

            if ("Y".Equals(fieldInformationArray[7]))
                fieldSql.Append(" ");
            else {
                fieldSql.Append("not null ");
                if (!string.IsNullOrEmpty(fieldInformationArray[1]))
                {
                    if (SelectDBType == 0)
                    {
                        fieldSql.Append("primary key ");
                    }
                }
            }

            if (fieldInformationArray[8] == null)
                fieldSql.Append(",\r\n");
            else
                fieldSql.Append("comment " + fieldInformationArray[8] + ",\r\n");
            return fieldSql.ToString();
        }
    }
}
