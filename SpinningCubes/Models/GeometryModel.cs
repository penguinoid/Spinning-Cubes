using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace SpinningCubes.Models
{
    public class GeometryModel
    {
        public NameScope NameScope = new NameScope();

        public GeometryModel3D Create(string positions, string triangleIndices, string name, int x, int y, int z, Vector3D axis, int angle, double centerX, double centerY, double centerZ, int r, int g, int b)
        {
            var model = new GeometryModel3D
            {
                Geometry = MeshModel.Create(positions, triangleIndices, x, y ,z),
                Material = new DiffuseMaterial
                {
                    Brush = GetSolidColorBrush(name + "_Color", r, g, b)
                },
                Transform = GetRotateTransform3D(name + "_Transform", axis, angle, centerX, centerY, centerZ)
            };
            NameScope.Add(name, model);
            return model;
        }


        private SolidColorBrush GetSolidColorBrush(string name, int r, int g, int b)
        {
            var solidColorBrush = new SolidColorBrush
            {
                Color = Color.FromRgb((byte)r, (byte)g, (byte)b)
            };
            NameScope.Add(name, solidColorBrush);
            return solidColorBrush;
        }

        private RotateTransform3D GetRotateTransform3D(string name, Vector3D axis, int angle, double centerX, double centerY, double centerZ)
        {
            var rotateTransform3D = new RotateTransform3D
            {
                Rotation = GetAxisAngleRotation3D(name + "_Rotation", axis, angle),
                CenterX = centerX,
                CenterY = centerY,
                CenterZ = centerZ
            };
            NameScope.Add(name, rotateTransform3D);
            return rotateTransform3D;
        }

        private AxisAngleRotation3D GetAxisAngleRotation3D(string name, Vector3D axis, int angle)
        {
            var axisAngleRotation3D = new AxisAngleRotation3D
            {
                Axis = axis,
                Angle = angle
            };
            NameScope.Add(name, axisAngleRotation3D);
            return axisAngleRotation3D;
        }
    }
}
