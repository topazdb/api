using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace api.Util {
    public class PathFixes {
        private static HashSet<(Regex,string)> PATH_FIXES = new HashSet<(Regex,string)>() {
            (new Regex(@"-([a-zA-Z])"), "- $1"),                     // changes -Author.x3p to - Author.x3p
            (new Regex(@"([a-zA-Z0-9])-"), "$1 -"),
            (new Regex(@"reolution"), "resolution"),
            (new Regex(@"([0-9]{2}x) ([a-z])"), "$1 - $2"),                                 // changes 20x autolight to 20x - autolight
            (new Regex(@"\/([a-zA-Z]+) ([a-zA-Z]|[0-9]+)\/(.*)\1\2"), "/$1 $2/$3$1 $2"),     // changes FAU123 to FAU 123 (if the latter is detected earlier in the path)
            (new Regex(@"(?:HS 44|HS44)"), "Hamby Set 44"),
            (new Regex(@"HTX - (?:Persistence|Persistance)"), "Houston Persistence")
        };

        /**
         * Makes adjustments to a pathname but does not move the file
         */
        public static string fix(string pathname) {
            foreach((Regex, string) pathFix in PATH_FIXES) {
                (Regex match, string replace) = pathFix;
                pathname = match.Replace(pathname, replace);
            }
            
            return pathname;
        }

        /**
         * Makes adjustments to a pathname and moves the file
         */
        public static string apply(string pathname) {
            string before = pathname;
            string after = fix(pathname);

            if(before != after) {
                File.Move(before, after);
            }

            return after;
        }
    }
} 