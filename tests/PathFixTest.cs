using System;
using Xunit;
using api.Util;

namespace api.Tests {
    public class PathFixTest {
        [Fact]
        public void TestFix_AuthorDash() {
            const string pathname = "/mnt/csafe/Houston Persistence/Barrel F/Bullet 50/HTX - Persistence - Barrel F - Bullet 50 - Land 5 - Sneox2 - 20x - auto light left image +20 perc. - Threshold 2 - Resolution 4 -Jozef Lamfers.x3p";
            const string expected = "/mnt/csafe/Houston Persistence/Barrel F/Bullet 50/HTX - Persistence - Barrel F - Bullet 50 - Land 5 - Sneox2 - 20x - auto light left image +20 perc. - Threshold 2 - Resolution 4 - Jozef Lamfers.x3p";
            string result = PathFixes.fix(pathname);
            
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestFix_MagnificationDash() {
            const string pathname = "/mnt/csafe/Houston Persistence/Barrel J/Bullet 13/HTX - Persistance - Barrel J - Bullet 13 - Land 1 - Sneox 1 - 20x autolight left image +20% & x10 - Threshold 2 - Resolution 4 - Jozef Lamfers.x3p";
            const string expected = "/mnt/csafe/Houston Persistence/Barrel J/Bullet 13/HTX - Persistance - Barrel J - Bullet 13 - Land 1 - Sneox 1 - 20x - autolight left image +20% & x10 - Threshold 2 - Resolution 4 - Jozef Lamfers.x3p";
            string result = PathFixes.fix(pathname);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestFix_BarrelNo() {
            const string pathname = "/mnt/csafe/LAPD/FAU 602/Bullet B/LAPD - FAU602 - Bullet B - Land 4 - Sneox1 - 20x - autolight leftimage +20 perc. - threshold 2 - resolution 4 - Marco Yepez.x3p";
            const string expected = "/mnt/csafe/LAPD/FAU 602/Bullet B/LAPD - FAU 602 - Bullet B - Land 4 - Sneox1 - 20x - autolight leftimage +20 perc. - threshold 2 - resolution 4 - Marco Yepez.x3p";
            string result = PathFixes.fix(pathname);

            Assert.Equal(expected, result);
        }
    }
}
