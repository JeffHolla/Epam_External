
namespace Task_1_2.Entities_Task_2
{
    // Что-то вроде этакой обёртки над Rectangle
    public class Square : AbstractFigure
    {
        private Rectangle rectangle;

        public override double Perimeter { get; }
        public override double Area { get; }
        public double SideLength { get { return rectangle.Height; } }

        public override string Name { get { return "Square"; } }

        public Square(Point center, double sideLength) : base(center)
        {
            rectangle = new Rectangle(center, sideLength);

            Perimeter = rectangle.Perimeter;
            Area = rectangle.Area;
        }
    }
}
