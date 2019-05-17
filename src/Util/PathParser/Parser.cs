using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace api.Util.PathParser {
    public class Parser {
        const string DIRECTORY_DELIMITER = "/";
        const string FILENAME_DELIMITER = "-";

        public PathParserResult result { get; private set; } = new PathParserResult();

        private string path;
        private string directory;
        private string filename;

        public Parser(string path) {
            this.path = path;
            this.directory = Path.GetDirectoryName(this.path);
            this.filename = Path.GetFileNameWithoutExtension(this.path);

            parseDirectory();
            parseFilename();
        }

        private void parseDirectory() {
            string[] parts = directory.Split(DIRECTORY_DELIMITER);
            result.sets = new List<Frame>();

            for(int i = 0; i < parts.Length; i++) {
                int j = parts.Length - 1 - i;
                string currentRaw = parts[j];
                Frame currentFrame = new Frame(currentRaw);
                

                switch(i) {
                    case 0:
                        if(!currentFrame.isCounting || currentFrame.countKey.ToLower() != "scan") {
                            throw new ArgumentException("Unhandled Path");
                        }

                        result.scanNo = (long) currentFrame.countValue;
                        break;
                    
                    default:
                        if(currentFrame.ToString() != "") {
                            result.sets.Add(currentFrame);
                        }
                        
                        break;
                }
            }

            result.sets.Reverse();
        }

        private void parseFilename() {
            string[] parts = filename.Split(FILENAME_DELIMITER);

            for(int i = 0; i < parts.Length; i++) {
                int j = parts.Length - 1 - i;
                string currentRaw = parts[j];
                Frame currentFrame = new Frame(currentRaw);

                if(parts.Length > 4 && i == 0) {
                    result.authorName = currentFrame.ToString();

                } else if(currentFrame.isCounting) {
                    setNumericValue(currentFrame.countKey.ToLower(), (long) currentFrame.countValue, i);

                } else if(currentFrame.isMagnification) {
                    result.magnification = (int) currentFrame.magnification;

                }
            }
        }

        private void setNumericValue(string key, long value, int index) {
            switch(key) {
                case "resolution": result.resolution = (int) value; break;
                case "threshold": result.threshold = (int) value; break;
                case "bullet": break;
                case "land": break;
                case "barrel": break;
                case "scan": break;

                default: 
                    if(index <= 6 && result.instrumentName == "") {
                        result.instrumentName = key;
                        result.instrumentVersion = (int) value;
                    }

                    break;
            }
        }
    }
}