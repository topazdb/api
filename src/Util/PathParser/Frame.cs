using System;
using System.Linq;

namespace api.Util.PathParser {
    public class Frame {
        const string DELIMITER = " ";
        private string data;
        private string[] pieces;

        public Frame(string data) {
            this.data = data.Trim();
            this.pieces = data.Split(DELIMITER);
            this.parse();
        }

        private void parse() {
            if(!isCounting) return;

            if(isShorthand) {
                countKey = pieces[0][0].ToString();
                countValue = isShorthandLetter ? (long) Char.ToUpper(pieces[0][1]) - 64 : Int64.Parse(pieces[0].Substring(1));
                
            } else {
                int i = pieces.Length - 1;
                long result;
                
                countKey = String.Join(" ", pieces.Take(i).ToArray());
                countValue = Int64.TryParse(pieces[i], out result) ? result : (long) Char.ToUpper(pieces[i][0]) - 64;
            }            
        }

        public bool isCounting {
            get {
                string last = pieces[pieces.Length - 1];
                long result;

                if(this.isShorthand) return true;
                if(last.Length == 1 && Char.IsLetter(last, 0)) return true;                

                return Int64.TryParse(last, out result);
            }
        }

        public bool isShorthand {
            get {
                return 
                    pieces.Length == 1 &&
                    (isShorthandLetter || isShorthandNumber);
            }
        }

        public bool isShorthandLetter {
            get {
                return 
                    pieces.Length == 1 && 
                    pieces[0].Length == 2 && 
                    Char.IsLetter(pieces[0], 0) && 
                    Char.IsLetter(pieces[0], 1); 
            }
        }

        public bool isShorthandNumber {
            get {
                long result;
                
                return 
                    pieces.Length == 1 &&
                    pieces[0].Length > 1 &&
                    Char.IsLetter(pieces[0], 0) &&
                    Int64.TryParse(pieces[0].Substring(1), out result);
            }
        }

        public string countKey { get; private set; }
        public long? countValue { get; private set; }

        public override string ToString() {
            return this.data;
        }
    }
}