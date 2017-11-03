using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex_hangman
{
    public class HangmanController
    {
        private HangmanModel hangmanModel;
        private HangmanView hangmanView;

        public HangmanController()
        {
            this.hangmanModel = new HangmanModel();
            this.hangmanView = new HangmanView();
        }

        public bool LetsPlay()
        {
            return hangmanModel.ContainsWords();
        }

        public void Play()
        {
            
        }

        public int Deserialize(string _path)
        {
            return hangmanModel.Deserialize(_path);
        }

        public bool Serialize(string _path)
        {
            return hangmanModel.Serialize(_path);
        }
    }
}