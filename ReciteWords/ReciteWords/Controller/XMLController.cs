using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using ReciteWords.Model;

namespace ReciteWords.Controller
{
    /// <summary>
    /// 控制个人列表的XML文件
    /// </summary>
    class XMLController
    {
        /// <summary>
        /// 用来初始化一个完全新的个人单词列表
        /// </summary>
        /// <param name="name">xml文档的名字</param>
        /// <returns></returns>
        public void CreateXML(string name)
        {
            string XMLFilePath = ".\\DontTouch\\" + name + ".xml";
            XDocument doc = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XElement
                    (
                        "wordbook",
                        new XElement
                        (
                            "num",
                            new XAttribute("num", 0)
                        )
                    )
                );
            doc.Save(XMLFilePath);
        }

        /// <summary>
        /// 在列表内添加新的单词
        /// </summary>
        /// <param name="name">xml文档名</param>
        /// <param name="word">存储的单词</param>
        /// <returns></returns>
        public static void AddWord(string name, Word word)
        {
            string XMLFilePath = ".\\DontTouch\\" + name + ".xml";
            XDocument doc = XDocument.Load(XMLFilePath);
            XElement root = doc.Root;
            
                XElement item = new XElement("item");

                XCData trans = new XCData(word.Trans);
                XCData phonetic = new XCData(word.Phonetic);


                root.Add(new XElement(
                    "item",
                    new XElement("word",word._Word),
                    new XElement("trans",trans),
                    new XElement("phonetic",phonetic)
                    )
                );
                int index = Convert.ToInt16(root.Element("num").Attribute("num").Value);
                root.Element("num").Attribute("num").SetValue(index + 1);
                doc.Save(XMLFilePath);
            
        }

        /// <summary>
        /// 删除列表最后一个单词，并将单词数减一
        /// </summary>
        /// <param name="name">词库名</param>
        public void DelateLastWord(string name)
        {
            string XMLFilePath = ".\\DontTouch\\" + name + ".xml";
            XDocument doc = XDocument.Load(XMLFilePath);
            XElement root = doc.Root;
            int index = Convert.ToInt16(root.Element("num").Attribute("num").Value);
            if (index == 0)
                return;
            root.Element("num").Attribute("num").SetValue(index - 1);
            XElement xItem = root.Elements("item").ElementAtOrDefault(index - 1);
            xItem.Remove();
            doc.Save(XMLFilePath);
        }
    }
}
