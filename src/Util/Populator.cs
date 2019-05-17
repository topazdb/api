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

        private Task runner;

        private ArrayList errors = new ArrayList();

        public PopulatorStatus status;

        public Populator(string directory) {
            this.directory = directory;
            this.status = new PopulatorStatus(
                this.runner = Task.Run(() => this.iterate())
            );

        }

        private void addFileError(string file, string error) {
            List<string> errors = this.status.errors.GetValueOrDefault(file, new List<string>());
            this.status.errors[file] = errors;
            errors.Add(error);
        }

        /**
         * Iterate through files, apply path fixes, populate database
         */
        private void iterate() {
            this.status.codeMinor++;
            
            IEnumerable<string> paths = new X3PFileEnumerable(directory);

            this.status.codeMinor++;
            foreach(var file in paths) {
                Task.Run(() => {
                    Context context = new Context();
                    DataAccess dba = new DataAccess(context);

                    if(dba.landExists(file)) {
                        this.status.processedFiles++;
                        return;
                    }
                
                    try { 
                        Parser parser = new Parser(file); 
                        dba.insertFromParserResult(file, parser.result);                    

                    } catch(Exception e) {
                        string errorType = (e is PathParserException) ? "Parser Error" : "Database Error";
                        string message = e.InnerException != null ? e.InnerException.Message : e.Message;
                        this.addFileError(file, $"{errorType}: {message}");
                        
                    } finally {
                        this.status.processedFiles++;
                    }
                });
                
            }
        }
    }
}