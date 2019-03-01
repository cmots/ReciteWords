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
            string XMLFilePath = ".\\DontTouch\\" + name + ".xml";
            XDocument doc = XDocument.Load(XMLFilePath);
            XElement root = doc.Root;
            Word word = new Word();
            try
            {
                //XElement xItem = root.Elements("item").Where(x => (int)x.Element("progress") == -1).First();
                XElement xItem = root.Elements("item").ElementAtOrDefault(index);
                if ((int)xItem.Element("progress") == -1)
                {
                    word._Word = xItem.Element("word").Value;
                    word.Trans = xItem.Element("trans").Value;
                    word.Phonetic = xItem.Element("phonetic").Value;
                }
                else
                {
                    word._Word = "根据序列号找到了一个单词，但显示它已经被背过了,而这种情况是不可能出现的";
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
        public static bool MakeIndex(string name)
        {
            string XMLFilePath = ".\\DontTouch\\wordbook\\" + name + ".xml";
            XDocument doc = XDocument.Load(XMLFilePath);
            XElement root = doc.Root;

            XElement xIndex = root.Element("index");
            if(xIndex==null)
            {
                XElement nIndex = new XElement("index");
                nIndex.SetAttributeValue("index", 0);
                root.AddFirst(nIndex);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 从词库中提取出序列号
        /// </summary>
        /// <param name="name">词库名</param>
        /// <returns></returns>
        public static int GetIndex(string name)
        {
            string XMLFilePath = ".\\DontTouch\\wordbook\\" + name + ".xml";
            XDocument doc = XDocument.Load(XMLFilePath);
            XElement root = doc.Root;
            XElement xIndex = root.Element("index");
            if (xIndex == null)
            {
                return -1;
            }
            else
            {
                return Convert.ToInt16(xIndex.Value);
            }
        }

        /// <summary>
        /// 将词库序列号加一
        /// </summary>
        /// <param name="name">词库名</param>
        /// <returns></returns>
        public static bool UpdateIndex(string name)
        {
            string XMLFilePath = ".\\DontTouch\\wordbook\\" + name + ".xml";
            XDocument doc = XDocument.Load(XMLFilePath);
            XElement root = doc.Root;
            XElement xIndex = root.Element("index");
            if (xIndex == null)
            {
                return false;
            }
            else
            {
                int value = Convert.ToInt16(xIndex.Value);
                xIndex.Value = (value + 1).ToString();
                return true;
            }
        }
    }
}
