
namespace Task_1_2
{
    public class Rectangle : AbstractFigure
    {
        public double Height { get; }
        public double Width { get; }

        public Rectangle(Point center, double sideLenght) : base(center)
        {
            Height = sideLenght;
            Width = sideLenght;
        }

        // Здесь тоже можно сократить конструктор на одну строку, но стоит ли?
        public Rectangle(Point center, double height, double width) : base(center)
        {
            Height = height;
            Width = width;
        }

        public Rectangle(double x_center, double y_center, double sideLenght) : base(new Point(x_center, y_center))
        {
            Height = sideLenght;
            Width = sideLenght;
        }

        // Не уверен, что стоит вовсе здесь пытаться сократить конструкторы через this, потому что это уменьшает понятность кода
        // (а может просто можно сделать как-то лучше, но я не знаю как)
        public Rectangle(double x_center, double y_center, double height, double width) : this(x_center, y_center, height)
        {
            Width = width;
        }

        public override double Perimeter {
            get {
                return 2 * Height + 2 * Width;
            }
        }

        public override double Area {
            get {
                return Height * Width;
            }
        }
    }
}
