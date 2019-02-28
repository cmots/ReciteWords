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
        public Word ChooseWord(string name,int i)
        {
            string XMLFilePath = ".\\" + name + ".xml";
            XDocument doc = XDocument.Load(XMLFilePath);
            XElement root = doc.Root;
            Word word = new Word();
            try
            {
                //XElement xItem = root.Elements("item").Where(x => (int)x.Element("progress") == -1).First();
                XElement xItem = root.Elements("item").ElementAtOrDefault(i);
                if ((int)xItem.Element("progress") == -1)
                {
                    word._Word = xItem.Element("word").Value;
                    word.Trans = xItem.Element("trans").Value;
                    word.Phonetic = xItem.Element("phonetic").Value;
                }
                else
                {
                    word._Word = "根据序列号找到了一个单词，但显示它已经被背过了";
                }
            }
            catch(Exception e)
            {
                word._Word = e.Message;
                return word;
            }
            return word;
        }
    }
}
