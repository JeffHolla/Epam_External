
namespace Task_1_2.Entities_Task_2
{
    public class Point : AbstractGeometricObject
    {
        public double X { get; }
        public double Y { get; }

        public override string Name { get { return "Point"; } }
        public override string Properties { get { return $"X = {X}, Y = {Y}"; } }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Point point = obj as Point;
            if (point as Point == null)
            {
                return false;
            }

            return X == point.X && Y == point.Y;
        }

        public static bool operator ==(Point point, Point otherPoint)
        {
            if(point is null || otherPoint is null)
            {
                return false;
            }

            return point.X == otherPoint.X && point.Y == otherPoint.Y;
        }

        public static bool operator !=(Point point, Point otherPoint)
        {
            if (point == null || otherPoint == null)
            {
                return false;
            }

            return point.X != otherPoint.X || point.Y != otherPoint.Y;
        }

        public override string ToString()
        {
            return $"X = {X}, Y = {Y}";
        }
    }
}
