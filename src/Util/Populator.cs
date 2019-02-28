using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using api.Util.PathParser;
using api.db;
using api.Models;

namespace api.Util {

    public class Populator {

        const string EXT_REGEX = "*.x3p";
        
        private string directory;

        private ArrayList errors = new ArrayList();

        public Populator(string directory) {
            this.directory = directory;
            Task.Run(() =>  this.iterate());
        }

        public string[] getPaths() {
            return Directory.GetFiles(directory, EXT_REGEX, SearchOption.AllDirectories);
        }

        /**
         * Iterate through files, apply path fixes, populate database
         */
        private void iterate() {
            Context context = new Context();
            DataAccess dba = new DataAccess(context);

            foreach(string file in this.getPaths()) {
                PathParserResult result;
                string fixedPath = PathFixes.fix(file);

                try {
                    Parser parser = new Parser(fixedPath);
                    result = parser.result;
                    
                } catch(Exception e) {
                    errors.Add($"Unhandled path: {fixedPath}");
                    continue;
                }

                try {
                    dba.insertFromParserResult(file, result);                    
                } catch(Exception e) {
                    errors.Add($"Error adding file to database: {fixedPath}");
                }
            }
        }
    }
}