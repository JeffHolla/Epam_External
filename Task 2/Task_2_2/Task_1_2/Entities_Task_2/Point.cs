
namespace Task_1_2.Entities_Task_2
{
    public class Point : AbstractGeometricObject
    {
        public double X { get; }
        public double Y { get; }

        public override string Name { get { return "Point"; } }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Point point = obj as Point;
            if (point as Point == null)
                return false;

            return X == point.X && Y == point.Y;
        }
    }
}
