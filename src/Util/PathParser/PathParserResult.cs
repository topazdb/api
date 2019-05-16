namespace api.Util.PathParser {
    public class PathParserResult {
        public string studyName { get; set; }
        public string setName { get; set; }
        public long? barrelNo { get; set; }
        public long bulletNo { get; set; }
        public long landNo { get; set; }
        public long scanNo { get; set; }
        public string instrumentName { get; set; } = "";
        public int instrumentVersion { get; set; }
        public int magnification { get; set; }
        public string lightSettings { get; set; }
        public int threshold { get; set; }
        public int resolution { get; set; }
        public string authorName { get; set; }
    }
}