using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace SpinningCubes.Models
{
    public static class MeshModel
    {
        public static MeshGeometry3D Create(string positions, string triangleIndices, int x, int y, int z)
        {
            return new MeshGeometry3D
            {
                Positions = CreatePositions(positions, x, y, z),
                TriangleIndices = CreateTriangleIndices(triangleIndices)
            };
        }

        private static Point3DCollection CreatePositions(string positions, int x, int y, int z)
        {
            var collection = new Point3DCollection();
            var coordinates = positions.Split(';');
            foreach (var coordinate in coordinates)
            {
                var intervals = coordinate.Split(',');
                var xInterval = int.Parse(intervals[0]) + x;
                var yInterval = int.Parse(intervals[1]) + y;
                var zInterval = int.Parse(intervals[2]) + z;
                collection.Add(new Point3D(xInterval, yInterval, zInterval));
            }
            return collection;
        }

        private static Int32Collection CreateTriangleIndices(string triangleIndices)
        {
            var collection = new Int32Collection();
            var indices = triangleIndices.Split(',');
            foreach (var index in indices)
            {
                collection.Add(int.Parse(index));
            }
            return collection;
        }
    }
}
