using System;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace api {

    [DataContract]
    public class PopulatorStatus {
        private Task runner;

        private static string[] STATUSES = new string[] {
            "Initialized",
            "Awaiting activation",
            "Waiting to run",
            "Running",
            "Awaiting completion of child tasks",
            "Completed",
            "Canceled",
            "Errored"
        };

        private static string[] SUB_STATUSES = new string[] {
            "None",
            "Queuing",
            "Parsing"
        };

        public PopulatorStatus(Task runner) {
            this.errors = new Dictionary<string, List<string>>();
            this.runner = runner;
        }

        [DataMember]
        public double code {
            get {
                double code = (double) this.codeMajor;
                code = code == 3 ? code + ((double) this.codeMinor / 10) : code;
                return code;
            }
        }

        public int codeMajor {
            get { 
                return (int) runner.Status; 
            }
        }

        public int codeMinor { get; set; }

        [DataMember]
        public string message {
            get {
                if(this.codeMajor == 3 && this.codeMinor > 0 && SUB_STATUSES.Length > this.codeMinor) {
                    return SUB_STATUSES[this.codeMinor]; 
                }
                
                return STATUSES.Length <= this.codeMajor ? "Unknown" : STATUSES[this.codeMajor];
            }
        }

        [DataMember]
        public int totalFiles { get; set; }

        [DataMember]
        public int processedFiles { get; set; }

        [DataMember]
        public int erroredFiles {
            get {
                return errors.Count;
            }
        }

        public Dictionary<string, List<string>> errors { get; set; }
    }
}