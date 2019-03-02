using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace ReciteWords.Controller
{
    class BookController
    {
        public void CreateXML(string name)
        {
            string XMLFilePath = ".\\DontTouch\\" + name + ".xml";
            XDocument doc = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XElement
                    (
                        "book",
                        new XElement
                        (
                            "name",
                            new XAttribute("name", "CET-6")
                        )
                    )
                );
            doc.Save(XMLFilePath);
        }

        public static string GetName()
        {
            string XMLFilePath = ".\\DontTouch\\bookname.xml";
            XDocument doc = XDocument.Load(XMLFilePath);
            XElement root = doc.Root;
            string bookname = root.Element("name").Attribute("name").Value;
            return bookname;
        }

        public void ChangeName()
        {
            string XMLFilePath = ".\\DontTouch\\bookname.xml";
            XDocument doc = XDocument.Load(XMLFilePath);
            XElement root = doc.Root;
            root.Element("name").Attribute("name").SetValue("self");
            doc.Save(XMLFilePath);
        }
    }
}
