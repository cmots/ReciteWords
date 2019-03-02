using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ReciteWords.Model;

namespace ReciteWords.Controller
{
    /// <summary>
    /// 控制单词词库
    /// </summary>
    class WordbookController
    {
        /// <summary>
        /// 从词库中选取一个已知序列号的单词
        /// </summary>
        /// <param name="name">词库名</param>
        /// <param name="index">单词序列号</param>
        /// <returns></returns>
        public Word ChooseWord(string name,int index)
        {
            Word word = new Word();
            try
            {
                string XMLFilePath = ".\\DontTouch\\" + name + ".xml";
                XDocument doc = XDocument.Load(XMLFilePath);
                XElement root = doc.Root;
                try
                {
                    XElement xItem = root.Elements("item").ElementAtOrDefault(index);
                    word._Word = xItem.Element("word").Value;
                    word.Trans = xItem.Element("trans").Value;
                    word.Phonetic = xItem.Element("phonetic").Value;
                }
                catch (Exception e)
                {
                    word._Word = e.Message;
                    return word;
                }
            }
            catch(Exception e)
            {
                word._Word = e.Message;
                return word;
            }
            return word;
        }

        /// <summary>
        /// 为刚使用的词库添加序列号
        /// </summary>
        /// <param name="name">词库名</param>
        /// <returns></returns>
        public void MakeIndex(string name)
        {
            string XMLFilePath = ".\\DontTouch\\" + name + ".xml";
            XDocument doc = XDocument.Load(XMLFilePath);
            XElement root = doc.Root;

            XElement xIndex = root.Element("index");
            if(xIndex==null)
            {
                XElement nIndex = new XElement("index");
                nIndex.SetAttributeValue("index", 0);
                root.AddFirst(nIndex);
                doc.Save(XMLFilePath);
            }
        }

        /// <summary>
        /// 从词库中提取出序列号
        /// </summary>
        /// <param name="name">词库名</param>
        /// <returns></returns>
        public int GetIndex(string name)
        {
            string XMLFilePath = ".\\DontTouch\\" + name + ".xml";
            XDocument doc = XDocument.Load(XMLFilePath);
            XElement root = doc.Root;
            XElement xIndex = root.Element("index");
            if (xIndex == null)
            {
                return -1;
            }
            else
            {
                return Convert.ToInt16(xIndex.Attribute("index").Value);
            }
        }

        /// <summary>
        /// 将词库序列号加一
        /// </summary>
        /// <param name="name">词库名</param>
        /// <returns></returns>
        public static bool UpdateIndex(string name)
        {
            string XMLFilePath = ".\\DontTouch\\" + name + ".xml";
            XDocument doc = XDocument.Load(XMLFilePath);
            XElement root = doc.Root;
            XElement xIndex = root.Element("index");
            if (xIndex == null)
            {
                return false;
            }
            else
            {
                int value = Convert.ToInt16(xIndex.Attribute("index").Value);
                xIndex.Attribute("index").SetValue(value + 1);
                doc.Save(XMLFilePath);
                return true;
            }
        }

        /// <summary>
        /// 将词库序号减一
        /// </summary>
        /// <param name="name">词库名</param>
        /// <returns></returns>
        public static bool BackIndex(string name)
        {
            string XMLFilePath = ".\\DontTouch\\" + name + ".xml";
            XDocument doc = XDocument.Load(XMLFilePath);
            XElement root = doc.Root;
            XElement xIndex = root.Element("index");
            if (xIndex == null)
            {
                return false;
            }
            else
            {
                int value = Convert.ToInt16(xIndex.Attribute("index").Value);
                if (value >= 1)
                {
                    xIndex.Attribute("index").SetValue(value - 1);
                }
                doc.Save(XMLFilePath);
                return true;
            }
        }
    }
}
