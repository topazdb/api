using System;
using System.Linq;
using System.IO;
using api.Models;
using api.Util.PathParser;

namespace api.db {
    public class DataAccess {
        private Context context;

        public DataAccess(Context context) {
            this.context = context;
        }

        public Author insertAuthor(string name, string contact = null) {
            var query = from author in context.Authors
                where author.name == name
                select author;
         
            if(query.Count() > 0) {
                return query.First();
            } 

            Author newAuthor = new Author();
            newAuthor.name = name;
            newAuthor.contact = contact;

            context.Authors.Add(newAuthor);
            context.SaveChanges();
            return newAuthor;
        }

        public Instrument insertInstrument(string model, int version, string manufacturer = "", string serialNo = null) {
            var query = from instrument in context.Instruments
                where instrument.type.model == model && instrument.serialNo == null
                select instrument;
            
            if(query.Count() > 0) {
                return query.First();
            }

            InstrumentType type = insertInstrumentType(model, version, manufacturer);
            Instrument instr = new Instrument();
            
            instr.instrumentTypeId = type.id;
            instr.serialNo = serialNo;
            instr.calibrationDate = DateTime.Now;

            context.Instruments.Add(instr);
            return instr;
        }

        public InstrumentType insertInstrumentType(string model, int version, string manufacturer = "") {
            var query = from instrumentType in context.InstrumentTypes
                where instrumentType.model == model && instrumentType.manufacturer == manufacturer
                select instrumentType;

            if(query.Count() > 0) {
                return query.First();
            }

            InstrumentType type = new InstrumentType();
            type.model = model;
            type.version = version;
            type.manufacturer = manufacturer;

            context.InstrumentTypes.Add(type);
            context.SaveChanges();
            return type;
        }

        public Set insertSet(string name) {
            var query = from set in context.Sets
                where set.name == name
                select set;
            
            if(query.Count() > 0) {
                return query.First();
            }

            Set newSet = new Set();
            newSet.name = name;

            context.Sets.Add(newSet);
            context.SaveChanges();
            return newSet;            
        }

        public Scan insertScan(string path, PathParserResult result, Set set, Instrument instrument, Author author) {
            var query = from scan in context.Scans
                join land in context.Lands on scan.id equals land.scanId
                where Path.GetDirectoryName(land.path) == Path.GetDirectoryName(path)
                select scan;
            
            if(query.Count() > 0) {
                return query.First();
            }

            Scan newScan = new Scan();
            newScan.authorId = author.id;
            newScan.setId = set.id;
            newScan.instrumentId = instrument.id;
            newScan.barrelNo = result.barrelNo;
            newScan.bulletNo = result.bulletNo;
            newScan.creationDate = DateTime.Now;
            newScan.magnification = result.magnification;
            newScan.threshold = result.threshold;
            newScan.resolution = result.resolution;

            context.Scans.Add(newScan);
            context.SaveChanges();
            return newScan;
        }

        public Land getLand(string path) {
            var query = from land in context.Lands
                where land.path == path
                select land;

            return query.Count() > 0 ? query.First() : null;
        }

        public bool landExists(string path) {
            return getLand(path) != null;
        }

        public Land insertLand(string path, PathParserResult result, Set set, Instrument instrument, Author author) {
            Land land = getLand(path);            
            
            if(land != null) {
                return land;
            }

            Scan scan = insertScan(path, result, set, instrument, author);
            
            Land newLand = new Land();
            newLand.path = path;
            newLand.scanId = scan.id;

            context.Lands.Add(newLand);
            context.SaveChanges();
            return newLand;
        }

        public void insertFromParserResult(string path, PathParserResult result) {
            Author author = insertAuthor(result.authorName);
            Instrument instrument = insertInstrument(result.instrumentName, result.instrumentVersion);
            Set set = insertSet(result.setName);
            Land land = insertLand(path, result, set, instrument, author);
        }
    }
}