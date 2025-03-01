using com.hexagonsimulations.HexMapBase.Geometry.Hex;

namespace HexMapAIUtils;

public class AIUtils
{
    /// <summary>
    /// Finds starting points for players on a given map. The starting points are
    /// equally distributed across the map.
    /// </summary>
    /// <param name="numPositions">Number of player starting positions needed.</param>
    /// <param name="map">A map of binary values meaning 0 unpassable and >0 passable.</param>
    /// <returns>Returns a list of computed starting positions.</returns>
    public static List<OffsetCoordinates> FindPlayerStartingPositions(int numPositions, List<int> map, int rows, int columns)
    {
        List<int[]> passablePositions = new List<int[]>();
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (map[i * columns + j] != 0)
                {
                    passablePositions.Add(new int[] { i, j });
                }
            }
        }

        // K-Means algorithm
        double[][] centroids = Utils.InitializeCentroids(passablePositions.ToArray(), numPositions);
        double[][] oldCentroids;
        int[] labels = new int[passablePositions.Count];

        do
        {
            oldCentroids = (double[][])centroids.Clone();

            for (int i = 0; i < passablePositions.Count; i++)
            {
                labels[i] = Utils.FindClosestCentroid(passablePositions[i], centroids);
            }

            centroids = Utils.UpdateCentroids(passablePositions.ToArray(), labels, numPositions);

        } while (!Utils.AreCentroidsEqual(oldCentroids, centroids));

        List<OffsetCoordinates> startPositions = new List<OffsetCoordinates>();
        for (int i = 0; i < numPositions; i++)
        {
            startPositions.Add(new OffsetCoordinates((int)centroids[i][1], (int)centroids[i][0]));
        }

        return startPositions;
    }
}
