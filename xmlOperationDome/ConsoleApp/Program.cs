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
using System;
using System.Diagnostics;
using System.Net;
using System.Xml;
using System.Xml.Linq;

namespace XMLOperationDome{

    /// <summary>
    /// C# 操作xml文件工具类
    /// </summary>
    public class XMLOperation {

        private String xmlPathURL = @"D:\project\windows-project\ConsoleApp\xmlOperationDome\ConsoleApp\domeXml.xml";
        private String addSaveXMLPathURL = @"D:\project\windows-project\ConsoleApp\xmlOperationDome\ConsoleApp\domeXmlByAdd.xml";
        private String updateSaveXMLPathURL = @"D:\project\windows-project\ConsoleApp\xmlOperationDome\ConsoleApp\domeXmlByUpdate.xml";
        private String deleteSaveXMLPathURL = @"D:\project\windows-project\ConsoleApp\xmlOperationDome\ConsoleApp\domeXmlByDelete.xml";

        /// <summary>
        /// 加载XML数据对象
        /// </summary>
        /// <returns>XmlDocument</returns>
        private XmlDocument getXMLDocumentObject(String xmlPathURL) {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPathURL);
            return xmlDoc;
        }

        /// <summary>
        /// 向指定节点对象添加新的标签
        /// </summary>
        /// <param name="xmlDoc">XML文件对象</param>
        /// <param name="uniqueAttribute">条件节点name</param>
        /// <param name="labelName">标签名称</param>
        /// <param name="labelValue">标签值</param>
        private void addBookNodePublishdate(XmlDocument xmlDoc, String uniqueAttribute,String labelName, String labelValue) {
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsmgr.AddNamespace("bk", "http://www.contoso.com/books");
            string xPathString = "//bk:books/bk:book[@name='" + uniqueAttribute + "']";
            #pragma warning disable CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
            XmlNode root = xmlDoc.DocumentElement.SelectSingleNode(xPathString, nsmgr);
            #pragma warning restore CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。

            if (root == null) { Console.WriteLine("查找节点不存在，请确认查询name值"); return; }

            //Console.WriteLine(root.ToString());
            //创建一个新的Xml节点元素
            XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, labelName, null);
            node.InnerText = labelValue;
            //将创建的item子节点添加到items节点的尾部
            root.AppendChild(node);

            //保存修改的Xml文件内容
            if (File.Exists(addSaveXMLPathURL))
            {
                Console.WriteLine("目标xml文件已存在并进行覆盖");
            }
            xmlDoc.Save(addSaveXMLPathURL);
        }

        /// <summary>
        /// 读取并打印现有的XML数据文件
        /// </summary>
        private void readXmlContent(XmlDocument xmlDoc) {
            Console.WriteLine(value: xmlDoc.DocumentElement.OuterXml);
        }

        /// <summary>
        /// 获取指定节点对象
        /// </summary>
        /// <param name="uniqueAttribute">指定节点属性字符串</param>
        /// <param name="doc">XML数据对象</param>
        /// <returns>节点对象</returns>
        private XmlNode? getBookNode(string uniqueAttribute, XmlDocument doc) {
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("bk", "http://www.contoso.com/books");
            string xPathString = "//bk:books/bk:book[@name='" + uniqueAttribute + "']";
            XmlNode xmlNode = doc.DocumentElement.SelectSingleNode(xPathString, nsmgr);
            return xmlNode;
        }

        /// <summary>
        /// 获取书籍节点相信信息
        /// </summary>
        /// <param name="book"> 书籍节点对象 </param>
        private void getBookNodeInfo(XmlNode book){
            if (book == null) { Console.WriteLine("查找节点不存在，请确认查询name值"); return; }
            XmlElement bookElement = (XmlElement)book;
            String author = bookElement["author"].InnerText;
            String title = bookElement["title"].InnerText;
            String publisher = bookElement["publisher"].InnerText;
            Console.WriteLine("满足条件的书籍作者：{0} 名称：{1} 价格：{2}",author,title,publisher );
        }

        /// <summary>
        /// 修改指定name节点标签为指定新值
        /// </summary>
        /// <param name="doc">XML数据对象</param>
        /// <param name="uniqueAttribute">指定节点属性字符串</param>
        /// <param name="LabelName">需要修改的标签名称</param>
        /// <param name="LabelValue">新的标签值</param>
        private void updateBookNodeByName(XmlDocument doc, string uniqueAttribute, String LabelName, String LabelValue) {
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("bk", "http://www.contoso.com/books");
            string xPathString = "//bk:books/bk:book[@name='" + uniqueAttribute + "']";
            XmlNode xmlNode = doc.DocumentElement.SelectSingleNode(xPathString, nsmgr);
            if (xmlNode == null) { Console.WriteLine("查找节点不存在，请确认查询name值"); return; }
            XmlElement bookElement = (XmlElement)xmlNode;
            bookElement[LabelName].InnerText = LabelValue;

            //保存修改的Xml文件内容
            if (File.Exists(updateSaveXMLPathURL))
            {
                Console.WriteLine("目标xml文件已存在并进行覆盖");
            }
            doc.Save(updateSaveXMLPathURL);

        }

        /// <summary>
        /// 删除指定name节点下的标签
        /// </summary>
        /// <param name="doc">XML数据对象</param>
        /// <param name="uniqueAttribute">指定节点属性字符串</param>
        /// <param name="LabelName">需要删除的标签名称</param>
        private void daleteBookNodeByName(XmlDocument doc, string uniqueAttribute, String LabelName) {
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("bk", "http://www.contoso.com/books");
            string xPathString = "//bk:books/bk:book[@name='" + uniqueAttribute + "']";
            XmlNode xmlNode = doc.DocumentElement.SelectSingleNode(xPathString, nsmgr);
            XmlNodeList delNode = doc.SelectNodes("//bk:books/bk:book[@name='" + uniqueAttribute + "']/bk:"+ LabelName, nsmgr);

            if (xmlNode == null) { Console.WriteLine("查找节点不存在，请确认查询name值"); return; }

            //XmlNode delNode = doc.DocumentElement.SelectSingleNode("//bk:books/bk:book[@name='" + uniqueAttribute + "']:" + LabelName, nsmgr);
            xmlNode.RemoveChild(delNode[0]);

            //保存删除的Xml标签后的文件内容
            if (File.Exists(deleteSaveXMLPathURL))
            {
                Console.WriteLine("目标xml文件已存在并进行覆盖");
            }
            doc.Save(deleteSaveXMLPathURL);

        }

        /// <summary>
        /// XML操作菜单
        /// </summary>
        /// <returns></returns>
        private Int32 menu() {
            Console.WriteLine("\t\t\tXML文件基础操作模拟控制器");
            Console.WriteLine("\t1.读取本地XML文件并输出\n\n\t2.获取指定节点数据并打印(模拟查询book:name=zw的标签下的数据)\n\n\t" +
                "3.向指定条件节点添加新的标签（模拟向book:name=zw的标签下添加新标签<publishdate>并设置值）\n\n\t" +
                "4.修改指定条件节点下指定标签的值（模拟修改book:name=zw的标签下title标签的值为<C#中级教程>）\n\n\t" +
                "5.删除指定条件节点下的指定标签并保存(模拟删除book:name=zw中的author标签)\n\n\t"+
                "6.退出\n\n");
            Console.Write("请输入操作编号进行操作，为了方便未进行输入操作：");

            string codeKey = Console.ReadLine();
 
            return Convert.ToInt32(codeKey);
        }

        /// <summary>
        /// 输出分割线
        /// </summary>
        private void printSplitLine() {
            Console.WriteLine("\n\n=============数据加载输出完成=============\n\n\n");
        }

        /// <summary>
        /// 程序运行入口
        /// </summary>
        /// <param name="args"></param>
        public static void Main(String[] args) {
            XMLOperation xmlOperation = new XMLOperation();
            XmlDocument xmlDoc = xmlOperation.getXMLDocumentObject(xmlOperation.xmlPathURL);

            //加载XML文件对象并全部打印
            //xmlOperation.readXmlContent(xmlDoc);

            //获取指定节点数据并打印
            //xmlOperation.getBookNodeInfo(xmlOperation.getBookNode("zw", xmlDoc));

            //向指定条件节点添加新的标签
            //xmlOperation.addBookNodePublishdate(xmlDoc, "zw", "publishdate", "2023-2-14");

            //修改指定条件节点下指定标签的值
            //xmlOperation.updateBookNodeByName( xmlDoc, "zw", "title", "C#中级教程");

            //删除指定条件节点下的指定标签并保存
            //xmlOperation.daleteBookNodeByName(xmlDoc, "zw", "author");
            int codeKeyInput = 0;
            do{
                try {
                    codeKeyInput = xmlOperation.menu();
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message + " 操作编号输入错误请重新输入\n\n");
                    codeKeyInput = xmlOperation.menu();
                }
                switch (codeKeyInput) {
                    case 1:
                        xmlOperation.readXmlContent(xmlDoc);
                        xmlOperation.printSplitLine();
                        break;
                    case 2:
                        xmlOperation.getBookNodeInfo(xmlOperation.getBookNode("zw", xmlDoc));
                        xmlOperation.printSplitLine();
                        break;
                    case 3:
                        xmlOperation.addBookNodePublishdate(xmlDoc, "zw", "publishdate", "2023-2-14");
                        XmlDocument xmlAddXML = xmlOperation.getXMLDocumentObject(xmlOperation.addSaveXMLPathURL);
                        Console.WriteLine("下面是添加后的XML文件数据");
                        xmlOperation.readXmlContent(xmlAddXML);
                        xmlOperation.printSplitLine();
                        break;
                    case 4:
                        xmlOperation.updateBookNodeByName(xmlDoc, "zw", "title", "C#中级教程");
                        XmlDocument updateXML = xmlOperation.getXMLDocumentObject(xmlOperation.updateSaveXMLPathURL);
                        Console.WriteLine("下面是修改后的XML文件数据");
                        xmlOperation.readXmlContent(updateXML);
                        xmlOperation.printSplitLine();
                        break;
                    case 5:
                        xmlOperation.daleteBookNodeByName(xmlDoc, "zw", "author");
                        XmlDocument deleteXML = xmlOperation.getXMLDocumentObject(xmlOperation.deleteSaveXMLPathURL);
                        Console.WriteLine("下面是删除后的XML文件数据");
                        xmlOperation.readXmlContent(deleteXML);
                        xmlOperation.printSplitLine();
                        break;
                    default:
                        Console.WriteLine("操作编号输入错误请重新输入\n\n");
                        break;
                }
            } while (codeKeyInput != 6);
            Console.ReadKey();
        }
    } 
}
