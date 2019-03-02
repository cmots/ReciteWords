using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using ReciteWords.Controller;
using ReciteWords.Model;

namespace ReciteWords
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            WordbookController wordbookController = new WordbookController();
            XMLController xMLController = new XMLController();
            BookController bookController = new BookController();

            if (!File.Exists(".\\DontTouch\\self.xml"))
                xMLController.CreateXML("self");

            if (!File.Exists(".\\DontTouch\\bookname.xml"))
                bookController.CreateXML("bookname");

            string name = BookController.GetName();         

            wordbookController.MakeIndex(name);
            Word word = wordbookController.ChooseWord(name, wordbookController.GetIndex(name));
            textBox1.Text = word._Word + Environment.NewLine + word.Phonetic + Environment.NewLine + word.Trans;
        }

        /// <summary>
        /// 会了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string name = BookController.GetName();
            WordbookController wordbookController = new WordbookController();
            BookController bookController = new BookController();

            Word word = new Word();

            WordbookController.UpdateIndex(name);
            int index = wordbookController.GetIndex(name);
            if (index == -1)
            {
                word._Word = "我发现你已经背完了所有的单词，下面将复习你不会的单词";
                bookController.ChangeName();
            }
            else
                word = wordbookController.ChooseWord(name, index);
            textBox1.Text = word._Word + Environment.NewLine + word.Phonetic + Environment.NewLine + word.Trans;
        }

        /// <summary>
        /// 不会
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            string name = BookController.GetName();
            WordbookController wordbookController = new WordbookController();
            BookController bookController = new BookController();

            Word word = new Word();

            WordbookController.UpdateIndex(name);
            int index = wordbookController.GetIndex(name);

            if (index == -1)
            {
                word._Word = "我发现你已经背完了所有的单词，下面将复习你不会的单词";
                bookController.ChangeName();
            }
            else
                word = wordbookController.ChooseWord(name, index);
            textBox1.Text = word._Word + Environment.NewLine + word.Phonetic + Environment.NewLine + word.Trans;
            XMLController.AddWord("self", word);
        }

        /// <summary>
        /// 上一个
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            string name = BookController.GetName();
            WordbookController wordbookController = new WordbookController();
            XMLController xMLController = new XMLController();

            WordbookController.BackIndex(name);
            Word word = wordbookController.ChooseWord(name, wordbookController.GetIndex(name));
            textBox1.Text = word._Word + Environment.NewLine + word.Phonetic + Environment.NewLine + word.Trans;
            xMLController.DelateLastWord("self");
        }
    }
}
