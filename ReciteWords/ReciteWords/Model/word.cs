using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReciteWords.Model
{
    class Word
    {
        private string word;//单词

        private string trans;//翻译、词性（使用cdata）

        private string phonetic;//音标（使用cdata）

        private int progress;//学习进度（-1：未学习；0：真的完全掌握；1：会了；2：模糊；3：不会）

        public string _Word { get => word; set => word = value; }

        public string Trans { get => trans; set => trans = value; }

        public string Phonetic { get => phonetic; set => phonetic = value; }

        public int Progress { get => progress; set => progress = value; }
    }
}
