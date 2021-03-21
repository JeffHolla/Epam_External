
namespace Task_1_2
{
    public abstract class AbstractFigure
    {
        public Point Center { get; }

        public abstract double Perimeter { get; }

        public abstract double Area { get; }

        public AbstractFigure(Point center)
        {
            Center = center;
        }
    }
}
