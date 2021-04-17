
namespace Task_1_2.Entities_Task_2
{
    public abstract class AbstractFigure : AbstractGeometricObject
    {
        public Point Center { get; }

        public abstract double Perimeter { get; }

        public abstract double Area { get; }

        public override string Properties { get { return $"Perimeter = {Perimeter}, Area = {Area}"; } }

        public AbstractFigure(Point center)
        {
            Center = center;
        }
    }
}
