using System;

namespace api.Util.PathParser {
    public class PathParserException : Exception {
        public PathParserException(string message) : base(message) {}
    }
}