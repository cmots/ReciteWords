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
    class XMLController
    {
        //用来初始化一个完全新的个人单词列表
        public static bool CreateXML(string name)
        {
            string XMLFilePath = ".\\" + name + ".xml";
            if (!File.Exists(XMLFilePath))
            {
                File.Create(XMLFilePath);
            }
            try
            {
                XDocument doc = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XElement("wordbook")
                        );
                doc.Save(XMLFilePath);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        //用来在未来某个日期内添加单词
        public static bool AddDate(string name, string date)
        {
            string XMLFilePath = ".\\" + name + ".xml";
            XDocument doc = XDocument.Load(XMLFilePath);
            XElement root = doc.Root;

            XElement xDate = root.Elements("date").Where(x => x.Attribute("date").Value == date).Single();

            if (xDate == null)
            {
                try
                {
                    XElement nDate = new XElement("date");
                    nDate.SetAttributeValue("date", date);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
                return true;

            }
            else
                return false;
        }

        //用来在某个日期单词列表内添加新的单词
        public static bool AddWord(string name, string date, Word word)
        {
            string XMLFilePath = ".\\" + name + ".xml";
            XDocument doc = XDocument.Load(XMLFilePath);
            XElement root = doc.Root;

            XElement xDate = root.Elements("date").Where(x => x.Attribute("date").Value == date).Single();

            if (xDate == null)
                return false;

            try
            {
                XElement item = new XElement("item");

                XCData trans = new XCData(word.Trans);
                XCData phonetic = new XCData(word.Phonetic);

                item.SetElementValue("word", word._Word);
                item.SetElementValue("trans", trans);
                item.SetElementValue("phonetic", phonetic);
                item.SetElementValue("progress", word.Progress);

                xDate.Add(item);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        } 
    }
}
