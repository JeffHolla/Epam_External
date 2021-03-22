namespace Task_1_2.Entities_Task_2
{
    public class Ring : AbstractFigure
    {
        public Circle InnerCircle { get; }
        public Circle OuterCircle { get; }

        public override double Perimeter { get; }
        public override double Area { get; }

        public override string Name { get { return "Ring"; } }

        public Ring(Point center, double innerCircleRadius, double outerCircleRadius)
            : base(center)
        {
            InnerCircle = new Circle(center, innerCircleRadius);
            OuterCircle = new Circle(center, outerCircleRadius);

            Perimeter = InnerCircle.Perimeter + OuterCircle.Perimeter;

            Area = OuterCircle.Area - InnerCircle.Area;
        }
    }
}
