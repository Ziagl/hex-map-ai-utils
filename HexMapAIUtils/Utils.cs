using com.hexagonsimulations.HexMapBase.Geometry.Hex;

namespace HexMapAIUtils;

internal class Utils
{
    /// <summary>
    /// Initializes centroids by randomly selecting points from the given set of points.
    /// </summary>
    /// <param name="points">The set of points to select from.</param>
    /// <param name="numCentroids">The number of centroids to initialize.</param>
    /// <returns>An array of initialized centroids.</returns>
    internal static double[][] InitializeCentroids(int[][] points, int numCentroids)
    {
        Random rand = new Random();
        double[][] centroids = new double[numCentroids][];
        HashSet<int> usedIndices = new HashSet<int>();

        for (int i = 0; i < numCentroids; i++)
        {
            int index;
            do
            {
                index = rand.Next(points.Length);
            } while (usedIndices.Contains(index));

            usedIndices.Add(index);
            centroids[i] = new double[] { points[index][0], points[index][1] };
        }

        return centroids;
    }

    /// <summary>
    /// Finds the closest centroid to a given point.
    /// </summary>
    /// <param name="point">The point to find the closest centroid for.</param>
    /// <param name="centroids">The array of centroids.</param>
    /// <returns>The index of the closest centroid.</returns>
    internal static int FindClosestCentroid(int[] point, double[][] centroids)
    {
        double minDistance = double.MaxValue;
        int closest = 0;

        for (int i = 0; i < centroids.Length; i++)
        {
            double distance = Math.Pow(point[0] - centroids[i][0], 2) + Math.Pow(point[1] - centroids[i][1], 2);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = i;
            }
        }

        return closest;
    }

    /// <summary>
    /// Updates the centroids based on the given points and their assigned labels.
    /// </summary>
    /// <param name="points">The set of points.</param>
    /// <param name="labels">The labels indicating the centroid each point is assigned to.</param>
    /// <param name="numCentroids">The number of centroids.</param>
    /// <returns>An array of updated centroids.</returns>
    internal static double[][] UpdateCentroids(int[][] points, int[] labels, int numCentroids)
    {
        double[][] newCentroids = new double[numCentroids][];
        int[] counts = new int[numCentroids];

        for (int i = 0; i < numCentroids; i++)
        {
            newCentroids[i] = new double[2];
        }

        for (int i = 0; i < points.Length; i++)
        {
            int label = labels[i];
            newCentroids[label][0] += points[i][0];
            newCentroids[label][1] += points[i][1];
            counts[label]++;
        }

        for (int i = 0; i < numCentroids; i++)
        {
            newCentroids[i][0] /= counts[i];
            newCentroids[i][1] /= counts[i];
        }

        return newCentroids;
    }

    /// <summary>
    /// Checks if two sets of centroids are equal.
    /// </summary>
    /// <param name="a">The first set of centroids.</param>
    /// <param name="b">The second set of centroids.</param>
    /// <returns>True if the centroids are equal, otherwise false.</returns>
    internal static bool AreCentroidsEqual(double[][] a, double[][] b)
    {
        for (int i = 0; i < a.Length; i++)
        {
            if (a[i][0] != b[i][0] || a[i][1] != b[i][1])
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Finds the nearest position of given point and list of passable positions.
    /// </summary>
    /// <param name="x">X Coordinate of point.</param>
    /// <param name="y">> Coordinate of point.</param>
    /// <param name="passablePositions">A list of points that are available.</param>
    /// <returns>The geometrical nearest point of given list.</returns>
    internal static OffsetCoordinates FindNearestPosition(double x, double y, List<int[]> passablePositions)
    {
        int foundX = 0;
        int foundY = 0;
        double minDistance = double.MaxValue;
        foreach(var position in passablePositions)
        {
            double distance = Math.Pow(x - position[0], 2) + Math.Pow(y - position[1], 2);
            if(distance < minDistance)
            {
                minDistance = distance;
                foundX = position[0];
                foundY = position[1];
            }
        }
        return new OffsetCoordinates(foundX, foundY);
    }
}
