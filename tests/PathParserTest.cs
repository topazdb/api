using System;
using Xunit;
using api.Util;
using api.Util.PathParser;

namespace api.Tests {
    public class PathParserTest {
        [Fact]
        public void TestPathParser_IncorrectElementCount() {
            const string badPath = "/mnt/csafe/HS224 Clone - Test Set 8 - Land 1 - Sneox1 - 20x - auto light left image +20% - threshold 2 - resolution 4 - Connor Hergenreter.x3p";
            Parser parser;
            Assert.Throws<PathParserException>(() => parser = new Parser(badPath));
        }

        [Fact]
        public void TestPathParser_SetName_Concat() {
            const string path = "/mnt/csafe/HS224 Clone - Test Set 8 - Bullet 1 - Land 1 - Sneox1 - 20x - auto light left image +20% - threshold 2 - resolution 4 - Connor Hergenreter.x3p";
            Parser parser = new Parser(path);
            PathParserResult result = parser.result;

            Assert.Equal("HS224 Clone - Test Set 8", result.setName);
        }

        [Fact]
        public void TestPathParser_SetName_Single() {
            const string path = "/mnt/csafe/LAPD/FAU 608/Bullet A/LAPD - FAU 608 - Bullet A - Land 6 - Sneox1 - 20x - auto light left image +20 perc. & x10 - threshold 2 - resolution 4 - Allison Mark.x3p";
            Parser parser = new Parser(path);
            PathParserResult result = parser.result;

            Assert.Equal("LAPD", result.setName);
        }

        [Fact]
        public void TestPathParser_BarrelNo_Null() {
            const string path = "/mnt/csafe/Hamby Set 224 Clone/Test Set 13/Bullet H/HS224 Clone - Test Set 13 - Bullet H - Land 6 - Sneox2 - 20x - auto light left image +20% - threshold 2 - resolution 4 - Connor Hergenreter.x3p";
            Parser parser = new Parser(path);
            PathParserResult result = parser.result;

            Assert.Equal(null, result.barrelNo);
        }

        [Fact]
        public void TestPathParser_BarrelNo_NotNull() {
            const string path = "/mnt/csafe/LAPD/FAU 608/Bullet A/LAPD - FAU 608 - Bullet A - Land 6 - Sneox1 - 20x - auto light left image +20 perc. & x10 - threshold 2 - resolution 4 - Allison Mark.x3p";
            Parser parser = new Parser(path);
            PathParserResult result = parser.result;

            Assert.Equal(608, result.barrelNo);
        }

        [Fact]
        public void TestPathParser_BulletNo_Alpha() {
            const string path = "/mnt/csafe/Hamby Set 224 Clone/Test Set 13/Bullet H/HS224 Clone - Test Set 13 - Bullet H - Land 6 - Sneox2 - 20x - auto light left image +20% - threshold 2 - resolution 4 - Connor Hergenreter.x3p";
            Parser parser = new Parser(path);
            PathParserResult result = parser.result;

            Assert.Equal(8, result.bulletNo);
        }

        [Fact]
        public void TestPathParser_BulletNo_Numeric() {
            const string path = "/mnt/csafe/HS224 Clone - Test Set 8 - Bullet 1 - Land 1 - Sneox1 - 20x - auto light left image +20% - threshold 2 - resolution 4 - Connor Hergenreter.x3p";
            Parser parser = new Parser(path);
            PathParserResult result = parser.result;

            Assert.Equal(1, result.bulletNo);
        }

        [Fact]
        public void TestPathParser_Instrument_NoSpace() {
            const string path = "/mnt/csafe/HS224 Clone - Test Set 8 - Bullet 1 - Land 1 - Sneox1 - 20x - auto light left image +20% - threshold 2 - resolution 4 - Connor Hergenreter.x3p";
            Parser parser = new Parser(path);
            PathParserResult result = parser.result;

            Assert.Equal("Sneox", result.instrumentName);
            Assert.Equal(1, result.instrumentVersion);
        }

        [Fact]
        public void TestPathParser_Instrument_Space() {
            const string path = "/mnt/csafe/HS224 Clone - Test Set 8 - Bullet 1 - Land 1 - Sneox 1 - 20x - auto light left image +20% - threshold 2 - resolution 4 - Connor Hergenreter.x3p";
            Parser parser = new Parser(path);
            PathParserResult result = parser.result;

            Assert.Equal("Sneox", result.instrumentName);
            Assert.Equal(1, result.instrumentVersion);
        }

        [Fact]
        public void TestPathParser_Magnification() {
            const string path = "/mnt/csafe/HS224 Clone - Test Set 8 - Bullet 1 - Land 1 - Sneox 1 - 20x - auto light left image +20% - threshold 2 - resolution 4 - Connor Hergenreter.x3p";
            Parser parser = new Parser(path);
            PathParserResult result = parser.result;

            Assert.Equal(20, result.magnification);
        }

        [Fact]
        public void TestPathParser_LightSettings() {
            const string path = "/mnt/csafe/HS224 Clone - Test Set 8 - Bullet 1 - Land 1 - Sneox 1 - 20x - auto light left image +20% - threshold 2 - resolution 4 - Connor Hergenreter.x3p";
            Parser parser = new Parser(path);
            PathParserResult result = parser.result;

            Assert.Equal("auto light left image +20%", result.lightSettings);
        }

        [Fact]
        public void TestPathParser_Threshold() {
            const string path = "/mnt/csafe/HS224 Clone - Test Set 8 - Bullet 1 - Land 1 - Sneox 1 - 20x - auto light left image +20% - threshold 2 - resolution 4 - Connor Hergenreter.x3p";
            Parser parser = new Parser(path);
            PathParserResult result = parser.result;

            Assert.Equal(2, result.threshold);
        }

        [Fact]
        public void TestPathParser_Author() {
            const string path = "/mnt/csafe/HS224 Clone - Test Set 8 - Bullet 1 - Land 1 - Sneox 1 - 20x - auto light left image +20% - threshold 2 - resolution 4 - Connor Hergenreter.x3p";
            Parser parser = new Parser(path);
            PathParserResult result = parser.result;

            Assert.Equal("Connor Hergenreter", result.authorName);            
        }
    }
}