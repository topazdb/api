using System;
using System.Linq;

namespace api.Util.PathParser {
    public class Frame {
        const string DELIMITER = " ";
        private string data;
        private string[] pieces;
        private bool doneParsing = false;

        public Frame(string data) {
            this.data = data.Trim();
            this.pieces = this.data.Split(DELIMITER);
            parse();
        }

        private void parse() {
            bool success;
            success = isCounting;
            success = isMagnification;
            doneParsing = true;
        }

        public bool isCounting {
            get {
                string last = pieces[pieces.Length - 1];
                long value = 0;
                bool success = false;

                if(isShorthand) {
                    return true;
                }

                if(last.Length == 1 && Char.IsLetter(last, 0)) {
                    success = true;
                    value = (long) Char.ToUpper(last[last.Length - 1]) - 64;
                }    

                if(!success) {
                    success = Int64.TryParse(last, out value);
                }

                if(!doneParsing && success) {
                    countKey = String.Join(" ", pieces.Take(pieces.Length - 1).ToArray());
                    countValue = value;
                }

                return success;
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
                bool success = 
                    pieces.Length == 1 && 
                    pieces[0].Length == 2 && 
                    Char.IsLetter(pieces[0], 0) && 
                    Char.IsLetter(pieces[0], 1); 

                if(!doneParsing && success) {
                    countKey = pieces[0][0].ToString();
                    countValue = (long) Char.ToUpper(pieces[0][1]) - 64;
                } 

                return success;
            }
        }

        public bool isShorthandNumber {
            get {
                if(pieces.Length != 1) return false;
                int startedNumbersAt = -1;
                bool success = true;

                for(int i = 0; i < pieces[0].Length; i++) {
                    char character = pieces[0][i];
                    if((Char.IsLetter(character) && startedNumbersAt != -1) || !Char.IsLetterOrDigit(character)) {
                        success = false;
                        break;
                    }

                    if(Char.IsDigit(character) && startedNumbersAt == -1) {
                        startedNumbersAt = i;
                    }
                }
                
                if(!doneParsing && success && startedNumbersAt > 0) {
                    countKey = pieces[0].Substring(0, startedNumbersAt);
                    countValue = Int64.Parse(pieces[0].Substring(startedNumbersAt));
                }

                return success && startedNumbersAt > 0;
            }
        }

        public bool isMagnification {
            get {
                if(pieces.Length != 1 || pieces[0].Length <= 1) return false;
                
                string raw = pieces[0];
                string beginning = raw.Substring(0, raw.Length - 1);
                char end = Char.ToLower(raw[raw.Length - 1]);
                int value;

                bool success = Int32.TryParse(beginning, out value) && end == 'x';

                if(!doneParsing && success) {
                    magnification = (int) value;
                }

                return success;
            }
        }

        public string countKey { get; private set; }
        public long? countValue { get; private set; }

        public int? magnification { get; private set; }

        public override string ToString() {
            return this.data;
        }
    }
}