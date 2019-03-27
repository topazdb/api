using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace api.Util {
    public class X3PFileEnumerable : IEnumerable<string> {
        
        private DirectoryInfo root;
        private static string X3P_REGEX = "*.x3p";

        public X3PFileEnumerable(string path) {
            this.root = new DirectoryInfo(path);
        }

        public IEnumerator<string> GetEnumerator() {
            if(root == null || !root.Exists) yield break;

            IEnumerable<FileInfo> results = new List<FileInfo>();
            try {
                results = results.Concat(root.EnumerateFiles(X3P_REGEX, SearchOption.TopDirectoryOnly).ToList());
            } catch(Exception) {}

            foreach(FileInfo file in results) {
                yield return file.ToString();
            }

            IEnumerable<DirectoryInfo> directories = new List<DirectoryInfo>();
            try {
                directories = directories.Concat(root.EnumerateDirectories("*", SearchOption.TopDirectoryOnly).ToList());
            } catch(Exception) {}

            foreach(DirectoryInfo directory in directories) {
                IEnumerable<string> subResults = new X3PFileEnumerable(directory.ToString());
                foreach(string file in subResults) {
                    yield return file;
                }
            }
        }
        
        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}