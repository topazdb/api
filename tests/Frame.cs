using System;
using Xunit;
using api.Util;
using api.Util.PathParser;

namespace api.Tests {
    public class FrameTest {

        [Fact]
        public void IsCounting_RightHandNumbersShouldBeTrue() {
            Frame frame = new Frame("Barrel 1");
            Assert.True(frame.isCounting);
        }

        [Fact]
        public void IsCounting_RightHandNumbersShouldBeTrue_2() {
            Frame frame = new Frame("Hamby Set 224");
            Assert.True(frame.isCounting);
        }

        [Fact]
        public void IsCounting_RightHandSingleLetterShouldBeTrue() {
            Frame frame = new Frame("Barrel A");
            Assert.True(frame.isCounting);
        }

        [Fact]
        public void IsCounting_ShortHandNumbersShouldBeTrue() {
            Frame frame = new Frame("U1");
            Assert.True(frame.isCounting);
        }

        [Fact]
        public void IsShorthand_SingleDigitNumbersShouldBeTrue() {
            Frame frame = new Frame("U1");
            Assert.True(frame.isShorthand);
        }

        [Fact]
        public void IsShorthand_MultipleDigitNumbersShouldBeTrue() {
            Frame frame = new Frame("U1234");
            Assert.True(frame.isShorthand);
        }

        [Fact]
        public void IsShorthand_SingleLettersShouldBeTrue() {
            Frame frame = new Frame("UA");
            Assert.True(frame.isShorthand);
        }

        public void IsShorthand_WordsShouldBeFalse() {
            Frame frame = new Frame("Test");
            Assert.False(frame.isShorthand);
        }

        [Fact]
        public void IsShorthand_MultipleSpacesShouldBeFalse() {
            Frame frame = new Frame("Barrel 1");
            Assert.False(frame.isShorthand);
        }

        [Fact]
        public void ToString_ShouldReturnTrimmedInput() {
            Frame frame = new Frame("    Barrel 1    ");
            Assert.Equal("Barrel 1", frame.ToString());
        }

        [Fact]
        public void CountValue_ShouldBeNullIfNotCounting() {
            Frame frame = new Frame("test");
            Assert.Null(frame.countValue);
        }

        [Fact]
        public void CountValue_ShouldBeCorrectForShorthandLetter() {
            Frame frame = new Frame("UA");
            Assert.Equal(1, frame.countValue);
        }

        [Fact]
        public void CountValue_ShouldBecorrectForShorthandSingleDigit() {
            Frame frame = new Frame("U1");
            Assert.Equal(1, frame.countValue);
        }

        [Fact]
        public void CountValue_ShouldBeCorrectForShorthandMultipleDigits() {
            Frame frame = new Frame("U5324");
            Assert.Equal(5324, frame.countValue);
        }

        [Fact]
        public void CountValue_ShouldBeCorrectForLonghandLetter() {
            Frame frame = new Frame("Bullet A");
            Assert.Equal(1, frame.countValue);
        }

        [Fact]
        public void CountValue_ShouldBeCorrectForLonghandSingleDigit() {
            Frame frame = new Frame("Bullet 1");
            Assert.Equal(1, frame.countValue);
        }

        [Fact]
        public void CountValue_ShouldBeCorrectForLonghandMultipleDigits() {
            Frame frame = new Frame("Bullet 5231");
            Assert.Equal(5231, frame.countValue);
        }

        [Fact]
        public void CountValue_ShouldBeCorrectForLonghandMultiWord() {
            Frame frame = new Frame("Hamby Set 224");
            Assert.Equal(224, frame.countValue);
        }

        [Fact]
        public void CountKey_ShouldBeCorrectForShorthand() {
            Frame frame = new Frame("UA");
            Assert.Equal("U", frame.countKey);
        }

        [Fact]
        public void CountKey_ShouldBeCorrectForLonghandSingleWord() {
            Frame frame = new Frame("Bullet 1");
            Assert.Equal("Bullet", frame.countKey);
        }

        [Fact]
        public void CountKey_ShouldBeCorrectForLonghandMultiWord() {
            Frame frame = new Frame("Hamby Set 224");
            Assert.Equal("Hamby Set", frame.countKey);
        }

        [Fact]
        public void CountKey_ShouldBeNullForNonCountingFrames() {
            Frame frame = new Frame("test");
            Assert.Null(frame.countKey);
        }
    }
}