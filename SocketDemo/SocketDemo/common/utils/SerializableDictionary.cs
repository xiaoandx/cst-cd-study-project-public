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
///
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SocketDemo.common.utils {

    /// <summary>
    /// Dictionary对象本身不支持序列化和反序列化，需要定义一个继承自Dictionary, IXmlSerializable类的自定义类来实现该功能。
    /// 感觉完全可以把这样的类封装到C#库中，很具有通用性
    /// 使用方法：
    ///     创建SerializableDictionary对象，这里以存储<string,string>
    ///     eg:SerializableDictionary<string, string> serializableDictionary = new SerializableDictionary<string, string>();
    ///     serializableDictionary.Add("Key1", "Value");
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable {
        public SerializableDictionary() {
        }

        /// <summary>
        /// 序列化
        /// 使用方法：
        ///     using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
        ///     {
        ///         XmlSerializer xmlFormatter = new XmlSerializer(typeof(SerializableDictionary<string, string>));
        ///         xmlFormatter.Serialize(fileStream, this.serializableDictionary);
        ///     }
        /// </summary>
        /// <param name="write">XML输出对象</param>
        public void WriteXml(XmlWriter write) {
            XmlSerializer KeySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer ValueSerializer = new XmlSerializer(typeof(TValue));

            foreach (KeyValuePair<TKey, TValue> kv in this) {
                write.WriteStartElement("SerializableDictionary");
                write.WriteStartElement("key");
                KeySerializer.Serialize(write, kv.Key);
                write.WriteEndElement();
                write.WriteStartElement("value");
                ValueSerializer.Serialize(write, kv.Value);
                write.WriteEndElement();
                write.WriteEndElement();
            }
        }

        /// <summary>
        /// 返序列化
        /// 使用方法：
        ///      using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
        ///      {
        ///          XmlSerializer xmlFormatter = new XmlSerializer(typeof(SerializableDictionary<string, string>));
        ///          this.serializableDictionary = (SerializableDictionary<string, string>)xmlFormatter.Deserialize(fileStream);
        ///      }
        /// </summary>
        /// <param name="reader">XML读对象</param>
        public void ReadXml(XmlReader reader) {
            reader.Read();
            XmlSerializer KeySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer ValueSerializer = new XmlSerializer(typeof(TValue));

            while (reader.NodeType != XmlNodeType.EndElement) {
                reader.ReadStartElement("SerializableDictionary");
                reader.ReadStartElement("key");
                TKey tk = (TKey)KeySerializer.Deserialize(reader);
                reader.ReadEndElement();
                reader.ReadStartElement("value");
                TValue vl = (TValue)ValueSerializer.Deserialize(reader);
                reader.ReadEndElement();
                reader.ReadEndElement();
                this.Add(tk, vl);
                reader.MoveToContent();
            }
            reader.ReadEndElement();

        }
        public XmlSchema GetSchema() {
            return null;
        }
    }
}
