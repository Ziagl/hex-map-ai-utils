using com.hexagonsimulations.HexMapBase.Geometry.Hex;

namespace HexMapAIUtils.Tests
{
    [TestClass]
    public sealed class AIUtilsTests
    {
        [TestMethod]
        public void TestFindPlayerStartingPositions()
        {
            List<List<int>> map = new List<List<int>>
            {
                new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
            };
            List<OffsetCoordinates> startPositions = AIUtils.FindPlayerStartingPositions(2, map);
            Assert.AreEqual(2, startPositions.Count);
            Assert.IsTrue(Distance(startPositions[0], startPositions[1]) > 4.0);

            new List<List<int>>
            {
                new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new List<int> { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new List<int> { 0, 0, 1, 1, 1, 1, 1, 0, 0, 1 },
                new List<int> { 1, 0, 0, 1, 1, 1, 0, 0, 0, 1 },
                new List<int> { 1, 1, 1, 1, 1, 1, 0, 0, 1, 1 },
                new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                new List<int> { 1, 1, 1, 1, 0, 0, 1, 1, 1, 1 },
                new List<int> { 1, 1, 1, 1, 0, 0, 0, 1, 1, 1 },
                new List<int> { 1, 1, 1, 1, 1, 0, 0, 1, 1, 1 }
            };
            startPositions = AIUtils.FindPlayerStartingPositions(5, map);
            Assert.AreEqual(5, startPositions.Count);
            Assert.IsTrue(Distance(startPositions[0], startPositions[1]) > 3.0);
            Assert.IsTrue(Distance(startPositions[2], startPositions[3]) > 3.0);
            Assert.IsTrue(Distance(startPositions[1], startPositions[4]) > 3.0);
        }

        private double Distance(OffsetCoordinates a, OffsetCoordinates b)
        {
            return Math.Sqrt(Math.Pow(a.x - b.x, 2) + Math.Pow(a.y - b.y, 2));
        }
    }
}
