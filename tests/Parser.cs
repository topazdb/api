using System;
using Xunit;
using api.Util;
using api.Util.PathParser;

namespace api.Tests {
    public class ParserTest {

        [Fact]
        public void Constructor_ShouldThrowIfLastDirectoryNotScan() {
            Assert.Throws<ArgumentException>(() => {
                Parser parser = new Parser("/data/Hamby Set 44/Barrel 1/Bullet 1/test.x3p");
            });
        }

        [Fact]
        public void Result_ShouldContainScanNo() {
            Parser parser = new Parser("/Hamby Set 36/Barrel 1/Bullet 1/Scan 1/HS36 - Barrel 1 - Bullet 1 - Land 1 - Scan 1 - Sneox1 - 20x - auto light left image +20% - threshold 2 - resolution 4 - Connor Hergenreter.x3p");
            Assert.Equal(1, parser.result.scanNo);
        }

        [Fact]
        public void Result_ShouldContainAuthor() {
            Parser parser = new Parser("/Hamby Set 36/Barrel 1/Bullet 1/Scan 1/HS36 - Barrel 1 - Bullet 1 - Land 1 - Scan 1 - Sneox1 - 20x - auto light left image +20% - threshold 2 - resolution 4 - Connor Hergenreter.x3p");
            Assert.Equal("Connor Hergenreter", parser.result.authorName);
        }

        [Fact]
        public void Result_ShouldContainResolution() {
            Parser parser = new Parser("/Hamby Set 36/Barrel 1/Bullet 1/Scan 1/HS36 - Barrel 1 - Bullet 1 - Land 1 - Scan 1 - Sneox1 - 20x - auto light left image +20% - threshold 2 - resolution 4 - Connor Hergenreter.x3p");
            Assert.Equal(4, parser.result.resolution);
        }

        [Fact]
        public void Result_ShouldContainThreshold() {
            Parser parser = new Parser("/Hamby Set 36/Barrel 1/Bullet 1/Scan 1/HS36 - Barrel 1 - Bullet 1 - Land 1 - Scan 1 - Sneox1 - 20x - auto light left image +20% - threshold 2 - resolution 4 - Connor Hergenreter.x3p");
            Assert.Equal(2, parser.result.threshold);
        }

        [Fact]
        public void Result_ShouldContainMagnification() {
            Parser parser = new Parser("/Hamby Set 36/Barrel 1/Bullet 1/Scan 1/HS36 - Barrel 1 - Bullet 1 - Land 1 - Scan 1 - Sneox1 - 20x - auto light left image +20% - threshold 2 - resolution 4 - Connor Hergenreter.x3p");
            Assert.Equal(20, parser.result.magnification);
        }

        [Fact]
        public void Result_ShouldContainInstrument() {
            Parser parser = new Parser("/Hamby Set 36/Barrel 1/Bullet 1/Scan 1/HS36 - Barrel 1 - Bullet 1 - Land 1 - Scan 1 - Sneox1 - 20x - auto light left image +20% - threshold 2 - resolution 4 - Connor Hergenreter.x3p");
            Assert.Equal("sneox", parser.result.instrumentName);
            Assert.Equal(1, parser.result.instrumentVersion);
        }
    }
}