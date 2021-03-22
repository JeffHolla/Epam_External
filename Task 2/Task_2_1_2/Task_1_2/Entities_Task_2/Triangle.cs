using System;


namespace Task_1_2.Entities_Task_2
{
    public class Triangle : AbstractFigure
    {
        public Line Line1 { get; }
        public Line Line2 { get; }
        public Line Line3 { get; }

        public override double Perimeter { get; }
        public override double Area { get; }

        public override string Name { get { return "Triangle"; } }

        public Triangle(Point p1, Point p2, Point p3)
            : base(new Point((p1.X + p2.X + p3.X) / 3, (p1.Y + p2.Y + p3.Y) / 3)) //получение центра треугольника
        {
            Line1 = new Line(p1, p2);
            Line2 = new Line(p2, p3);
            Line3 = new Line(p3, p1);

            Perimeter = Line1.Length + Line2.Length + Line3.Length;

            Area = Math.Sqrt(Perimeter / 2 *
                (Perimeter / 2 - Line1.Length) * (Perimeter / 2 - Line2.Length) * (Perimeter / 2 - Line3.Length));
        }
    }
}
