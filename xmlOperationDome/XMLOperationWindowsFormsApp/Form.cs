using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net;
using System.Xml;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace XMLOperationWindowsFormsApp
{
    public partial class FormMain : System.Windows.Forms.Form
    {
        public String bookName = "";
        public String xmlPathURL = @"D:\project\windows-project\ConsoleApp\xmlOperationDome\ConsoleApp\domeXml.xml";
        public String addSaveXMLPathURL = @"D:\project\windows-project\ConsoleApp\xmlOperationDome\ConsoleApp\domeXmlByAdd.xml";
        public String updateSaveXMLPathURL = @"D:\project\windows-project\ConsoleApp\xmlOperationDome\ConsoleApp\domeXmlByUpdate.xml";
        public String deleteSaveXMLPathURL = @"D:\project\windows-project\ConsoleApp\xmlOperationDome\ConsoleApp\domeXmlByDelete.xml";
        public FormMain()
        {
            InitializeComponent();
        }

        public void ChangeText(string str)
        {
            bookName = str;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button_Click(object sender, EventArgs e)
        {
            XMLOperation xmlOperation = new XMLOperation();
            XmlDocument xmlDoc = xmlOperation.getXMLDocumentObject(xmlPathURL);
            String XMLDef = xmlOperation.readXmlContent(xmlDoc);
            textBox.Text = XMLDef;

        }

        private void button_Click_find(object sender, EventArgs e)
        {

            findBookByNameWin findBookByNameWino = new findBookByNameWin();
            findBookByNameWino.ShowDialog(this);
            XMLOperation xmlOperation = new XMLOperation();
            XmlDocument xmlDoc = xmlOperation.getXMLDocumentObject(xmlPathURL);
            String XMLDef = xmlOperation.getBookNodeInfo(xmlOperation.getBookNode(bookName, xmlDoc));
            textBox1.Text = XMLDef;

        }
        private void button_Click_add(object sender, EventArgs e)
        {
            XMLOperation xmlOperation = new XMLOperation();
            XmlDocument xmlDoc = xmlOperation.getXMLDocumentObject(xmlPathURL);
            xmlOperation.addBookNodePublishdate(xmlDoc, "zw", "publishdate", "2023-2-14");
            XmlDocument xmlAddXML = xmlOperation.getXMLDocumentObject(addSaveXMLPathURL);
            String XMLDef = xmlOperation.readXmlContent(xmlAddXML);
            textBox1.Text = XMLDef;
        }

        private void button_Click_update(object sender, EventArgs e)
        {
            XMLOperation xmlOperation = new XMLOperation();
            XmlDocument xmlDoc = xmlOperation.getXMLDocumentObject(xmlPathURL);
            xmlOperation.updateBookNodeByName(xmlDoc, "zw", "title", "C#中级教程");
            XmlDocument updateXML = xmlOperation.getXMLDocumentObject(updateSaveXMLPathURL);
            String XMLDef = xmlOperation.readXmlContent(updateXML);
            textBox1.Text = XMLDef;
        }

        private void button_Click_delete(object sender, EventArgs e)
        {
            XMLOperation xmlOperation = new XMLOperation();
            XmlDocument xmlDoc = xmlOperation.getXMLDocumentObject(xmlPathURL);
            xmlOperation.daleteBookNodeByName(xmlDoc, "zw", "author");
            XmlDocument deleteXML = xmlOperation.getXMLDocumentObject(deleteSaveXMLPathURL);
            String XMLDef = xmlOperation.readXmlContent(deleteXML);
            textBox1.Text = XMLDef;
        }

        private void button_Click_clear(object sender, EventArgs e)
        {
            textBox.Text = "";
            textBox1.Text = "";
        }
    }

    class XMLOperation {
        private String xmlPathURL = @"D:\project\windows-project\ConsoleApp\xmlOperationDome\ConsoleApp\domeXml.xml";
        private String addSaveXMLPathURL = @"D:\project\windows-project\ConsoleApp\xmlOperationDome\ConsoleApp\domeXmlByAdd.xml";
        private String updateSaveXMLPathURL = @"D:\project\windows-project\ConsoleApp\xmlOperationDome\ConsoleApp\domeXmlByUpdate.xml";
        private String deleteSaveXMLPathURL = @"D:\project\windows-project\ConsoleApp\xmlOperationDome\ConsoleApp\domeXmlByDelete.xml";

        /// <summary>
        /// 加载XML数据对象
        /// </summary>
        /// <returns>XmlDocument</returns>
        public XmlDocument getXMLDocumentObject(String xmlPathURL)
        {
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
        public void addBookNodePublishdate(XmlDocument xmlDoc, String uniqueAttribute, String labelName, String labelValue)
        {
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
        public String readXmlContent(XmlDocument xmlDoc)
        {
            //Console.WriteLine(value: xmlDoc.DocumentElement.OuterXml);
            return xmlDoc.DocumentElement.OuterXml;
        }

        /// <summary>
        /// 获取指定节点对象
        /// </summary>
        /// <param name="uniqueAttribute">指定节点属性字符串</param>
        /// <param name="doc">XML数据对象</param>
        /// <returns>节点对象</returns>
        public XmlNode getBookNode(string uniqueAttribute, XmlDocument doc)
        {
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
        public String getBookNodeInfo(XmlNode book)
        {
            if (book == null) { Console.WriteLine("查找节点不存在，请确认查询name值"); return "节点无数据，检查查询条件"; }
            XmlElement bookElement = (XmlElement)book;
            String author = bookElement["author"].InnerText;
            String title = bookElement["title"].InnerText;
            String publisher = bookElement["publisher"].InnerText;
            return "满足条件的书籍作者："+ author + " 名称："+ title + " 价格："+ publisher + "";
            //Console.WriteLine("满足条件的书籍作者：{0} 名称：{1} 价格：{2}", author, title, publisher);
        }

        /// <summary>
        /// 修改指定name节点标签为指定新值
        /// </summary>
        /// <param name="doc">XML数据对象</param>
        /// <param name="uniqueAttribute">指定节点属性字符串</param>
        /// <param name="LabelName">需要修改的标签名称</param>
        /// <param name="LabelValue">新的标签值</param>
        public void updateBookNodeByName(XmlDocument doc, string uniqueAttribute, String LabelName, String LabelValue)
        {
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
        public void daleteBookNodeByName(XmlDocument doc, string uniqueAttribute, String LabelName)
        {
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("bk", "http://www.contoso.com/books");
            string xPathString = "//bk:books/bk:book[@name='" + uniqueAttribute + "']";
            XmlNode xmlNode = doc.DocumentElement.SelectSingleNode(xPathString, nsmgr);
            XmlNodeList delNode = doc.SelectNodes("//bk:books/bk:book[@name='" + uniqueAttribute + "']/bk:" + LabelName, nsmgr);

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
    }
}
