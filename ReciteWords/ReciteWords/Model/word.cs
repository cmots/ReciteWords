using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReciteWords.Model
{
    class Word
    {
        private string word;

        private string trans;

        private string phonetic;

        private int progress;

        public string _Word { get => word; set => word = value; }

        public string Trans { get => trans; set => trans = value; }

        public string Phonetic { get => phonetic; set => phonetic = value; }

        public int Progress { get => progress; set => progress = value; }
    }
}
