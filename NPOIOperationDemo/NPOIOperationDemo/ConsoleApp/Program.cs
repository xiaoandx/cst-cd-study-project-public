﻿/*
using ConsoleApp.entity;
using ConsoleApp.NPOIOperation;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using System.ComponentModel;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleApp.NPOIOperation
{
    public class ExlceUtilTest
    {

        public static void Main(string[] args)
        {
            /*List<User> userList = new List<User>();
            userList.Add(new User() { ID = "100001", Name = "小明", Age = "24", Six = "男", Address = "四川省成都市", Tel = "15908576555" });
            userList.Add(new User() { ID = "100002", Name = "小红", Age = "22", Six = "女", Address = "四川省成都市", Tel = "15908576445" });
            Console.WriteLine(userList[0].ToString());
            ConsoleApp.NPOIOperation.ExcelUtils<User>.SaveExcelFile(System.AppDomain.CurrentDomain.BaseDirectory + "\\StudentInfo.xls", "Student", userList);
*/
            ConsoleApp.NPOIOperation.ExcelUtils<User>.ImportExcel(System.AppDomain.CurrentDomain.BaseDirectory + "\\StudentInfo.xls", "Student", 1);
        }
    }
}















