using api.Util;
using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace api.Util.PathParser {
    public class Parser {
        private const string DELIMITER = " - ";
        private const string COUNT_REGEX = @"^(.*) ([a-zA-Z]|[0-9]+)$";
        private const string NUMBER_REGEX = @"^([0-9]+)$";
        private const string INSTR_REGEX = @"^([a-zA-Z]+)(?:[ ]?)([0-9]+)$";
        private const string MAGNIFICATION_REGEX = @"^([0-9]+)x";
        private string pathname;
        private string filename;
        private string[] pieces;
        public PathParserResult result { get; private set; }
        private Type resultType;

        private static string[] RESULT_PROPERTIES = new string[] { 
            "setName", 
            "barrelNo", 
            "bulletNo", 
            "landNo", 
            "instrument",
            "magnification",
            "lightSettings",
            "threshold",
            "resolution",
            "author",
        };

        public Parser(string pathname) {
            this.pathname = pathname;
            this.filename = Path.GetFileNameWithoutExtension(pathname);
            this.result = new PathParserResult();
            this.resultType = this.result.GetType();
            this.pieces = filename.Split(DELIMITER);

            if(this.pieces.Length < 8) {
                throw new PathParserException($"Incorrect number of elements in filename.  Got: {this.pieces.Length}, Expected: 8-11");
            }

            this.parse();
        }

        private long parseLetterOrNumber(string numeric) {
            return Char.IsLetter(numeric, 0) ? (long) Char.ToUpper(numeric[0]) - 64 : Int64.Parse(numeric);
        }

        private void parse(int pieceIndex = 0, int propertyIndex = 0) {
            if(pieceIndex >= pieces.Length || propertyIndex >= RESULT_PROPERTIES.Length) {
                return;
            }
            
            string currentKey = RESULT_PROPERTIES[propertyIndex];
            string currentValue = pieces[pieceIndex].Trim();
            (int, int) next = ( pieceIndex + 1, propertyIndex + 1 );

            switch(currentKey) {
                case "setName": 
                    next = parseSetName(currentKey, currentValue, pieceIndex, propertyIndex); 
                    break;

                case "barrelNo":
                case "bulletNo":
                case "landNo":
                    next = parseBulletBarrelLand(currentKey, currentValue, pieceIndex, propertyIndex);
                    break;

                case "instrument":
                    next = parseInstrument(currentKey, currentValue, pieceIndex, propertyIndex);
                    break;

                case "magnification": 
                    next = parseMagnification(currentKey, currentValue, pieceIndex, propertyIndex);
                    break;

                case "lightSettings":
                    next = parseLightSettings(currentKey, currentValue, pieceIndex, propertyIndex);
                    break;
                
                case "threshold":
                case "resolution":
                    next = parseThresholdResolution(currentKey, currentValue, pieceIndex, propertyIndex);
                    break;

                case "author":
                    next = parseAuthor(currentKey, currentValue, pieceIndex, propertyIndex);
                    break;
            }

            (int nextPiece, int nextProperty) = next;
            parse(nextPiece, nextProperty);
        }

        private (int, int) parseSetName(string currentKey, string currentValue, int pieceIndex, int propertyIndex) {
            result.setName = result.setName != null ? $"{result.setName} - {currentValue}" : currentValue; 
            return (pieceIndex + 1, propertyIndex + 1);
        }

        private (int, int) parseBulletBarrelLand(string currentKey, string currentValue, int pieceIndex, int propertyIndex) {
            Match valueIsCounting = Regex.Match(currentValue, COUNT_REGEX);
            Match valueIsNumber = Regex.Match(currentValue, NUMBER_REGEX);
            bool noMatches = !valueIsCounting.Success && !valueIsNumber.Success;
            PropertyInfo currentProp = resultType.GetProperty(currentKey);

            if((propertyIndex == 1 && noMatches) || currentValue.ToLower().Contains("set"))  {
                return (pieceIndex, propertyIndex - 1);
            }

            string key, value;
            if(propertyIndex == 1 && valueIsNumber.Success) {
                key = "barrel";
                value = valueIsNumber.Groups[1].Captures[0].Value;
            } else {
                key = valueIsCounting.Groups[1].Captures[0].Value.ToLower();
                value = valueIsCounting.Groups[2].Captures[0].Value;
            }
                
            if(key.Equals("bullet") && currentKey.Equals("barrelNo")) {
                return (pieceIndex, propertyIndex + 1);
            }
            
            currentProp.SetValue(result, parseLetterOrNumber(value));
            return (pieceIndex + 1, propertyIndex + 1);
        }

        private (int, int) parseInstrument(string currentKey, string currentValue, int pieceIndex, int propertyIndex) {
            Match instrMatch = Regex.Match(currentValue, INSTR_REGEX);
                    
            if(!instrMatch.Success) {
                return (pieceIndex, propertyIndex + 1);
            }
            
            string instrName = instrMatch.Groups[1].Captures[0].Value;
            string instrVersion = instrMatch.Groups[2].Captures[0].Value;
            result.instrumentName = instrName;
            result.instrumentVersion = Int32.Parse(instrVersion);

            return (pieceIndex + 1, propertyIndex + 1);
        }

        private (int, int) parseMagnification(string currentKey, string currentValue, int pieceIndex, int propertyIndex) {
            Match magMatch = Regex.Match(currentValue, MAGNIFICATION_REGEX);

            if(!magMatch.Success) {
                return (pieceIndex, propertyIndex + 1);
            }

            string mag = magMatch.Groups[1].Captures[0].Value;
            result.magnification = Int32.Parse(mag);
            return (pieceIndex + 1, propertyIndex + 1);
        }

        private (int, int) parseLightSettings(string currentKey, string currentValue, int pieceIndex, int propertyIndex) {
            result.lightSettings = currentValue;
            return (pieceIndex + 1, propertyIndex + 1);
        }

        private (int, int) parseThresholdResolution(string currentKey, string currentValue, int pieceIndex, int propertyIndex) {
            Match match = Regex.Match(currentValue, COUNT_REGEX);

            if(!match.Success) {
                return (pieceIndex, propertyIndex + 1);
            }
            
            string trKey = match.Groups[1].Captures[0].Value.Trim().ToLower();
            string trValue = match.Groups[2].Captures[0].Value;

            if(!trKey.Equals(currentKey)) {
                return (pieceIndex, propertyIndex + 1);
            }

            PropertyInfo trCurrentProp = resultType.GetProperty(trKey);
            trCurrentProp.SetValue(result, Int32.Parse(trValue));
            return (pieceIndex + 1, propertyIndex + 1);
        }

        private (int, int) parseAuthor(string currentKey, string currentValue, int pieceIndex, int propertyIndex) {
            result.authorName = currentValue;
            return (pieceIndex + 1, propertyIndex + 1);
        }
    }
}