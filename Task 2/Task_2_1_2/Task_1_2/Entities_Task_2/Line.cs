using System;

namespace Task_1_2.Entities_Task_2
{
    public class Line : AbstractGeometricObject
    {
        public Point StartPoint { get; }
        public Point EndPoint { get; }

        public double Length { get; }

        public override string Name { get { return "Line"; } }

        public Line(double x1, double y1, double x2, double y2)
        {
            StartPoint = new Point(x1, y1);
            EndPoint = new Point(x2, y2);
            
            Length = Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
        }

        public Line(Point startPoint, Point endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            
            Length = Math.Sqrt((EndPoint.X - StartPoint.X) * (EndPoint.X - StartPoint.X) +
                (EndPoint.Y - StartPoint.Y) * (EndPoint.Y - StartPoint.Y));
        }
    }
}
