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

        public string _Word { get => word; set => word = value; }

        public string Trans { get => trans; set => trans = value; }

        public string Phonetic { get => phonetic; set => phonetic = value; }

    }
}
