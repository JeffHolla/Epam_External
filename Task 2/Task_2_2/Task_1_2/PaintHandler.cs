using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_1_2.Entities_Task_2;

namespace Task_1_2
{
    public class PaintHandler
    {
        List<AbstractGeometricObject> shapes = new List<AbstractGeometricObject>();


        public void AddShape()
        {
            string currentCommand = "";

            bool isChoosing = true;
            while (isChoosing)
            {
                Console.WriteLine();
                Console.WriteLine(
    @"Введите комманду : 
    1: Точка
    2: Линия
    3: Круг
    4: Кольцо
    5: Треугольник
    6: Прямоугольник
    7: Квадрат"
);

                currentCommand = Console.ReadLine();
                isChoosing = false;
                switch (currentCommand)
                {
                    case "1":
                        shapes.Add(CreatePoint());
                        break;
                    case "2":
                        shapes.Add(CreateLine());
                        break;
                    case "3":
                        shapes.Add(CreateCircle());
                        break;
                    case "4":
                        shapes.Add(CreateRing());
                        break;
                    case "5":
                        shapes.Add(CreateTriangle());
                        break;
                    case "6":
                        shapes.Add(CreateRectangle());
                        break;
                    case "7":
                        shapes.Add(CreateSquare());
                        break;
                    default:
                        Console.WriteLine("Выберите правильный вариант!");
                        isChoosing = true;
                        break;
                }
            }
        }

        public void ShowAllShapes()
        {
            foreach (var shape in shapes)
            {
                Console.WriteLine(shape.Name);
            }
        }

        public void RemoveAllShapes()
        {
            shapes.Clear();
        }


        private double InputCoordinate(string coordinateOrient)
        {
            while (true)
            {
                Console.Write($"{coordinateOrient}: ");
                if (double.TryParse(Console.ReadLine(), out double coordinate))
                {
                    return coordinate;
                }
                else
                {
                    Console.WriteLine("Неверное значение! Введите координату снова.");
                }
            }
        }

        private double InputSide(string coordinateOrient)
        {
            while (true)
            {
                Console.Write($"{coordinateOrient}: ");

                if (double.TryParse(Console.ReadLine(), out double sideValue))
                {
                    if (sideValue <= 0)
                    {
                        Console.WriteLine("Неправильное значение! Введите число больше нуля.");
                        continue;
                    }

                    return sideValue;
                }
                else
                {
                    Console.WriteLine("Неправильное значение! Введите корректное значение.");
                }
            }
        }

        private Point CreatePoint()
        {
            Console.WriteLine("Введите координату X: ");
            var x = InputCoordinate("X");

            Console.WriteLine("Введите координату Y: ");
            var y = InputCoordinate("Y");

            return new Point(x, y);
        }

        private Line CreateLine()
        {
            Console.WriteLine("Введите первую точку");
            var point1 = CreatePoint();

            Console.WriteLine("Введите вторую точку");
            var point2 = CreatePoint();

            return new Line(point1, point2);
        }

        private Circle CreateCircle()
        {
            Console.WriteLine("Введите центр");
            var center = CreatePoint();

            Console.WriteLine("Введите радиус: ");
            var radius = CreateLine();

            var circle = new Circle(center, radius.Length);
            if (FigureValidator.IsValid(circle))
            {
                return circle;
            }
            return null;
        }

        private Ring CreateRing()
        {
            Console.WriteLine("Введите центр");
            var center = CreatePoint();

            Console.WriteLine("Введите радиус первой окружности: ");
            var radius1 = CreateLine();

            Console.WriteLine("Введите радиус второй окружности: ");
            var radius2 = CreateLine();

            //return new Ring(center, Math.Min(radius1.Length, radius2.Length), Math.Max(radius1.Length, radius2.Length));
            var ring = new Ring(center, Math.Min(radius1.Length, radius2.Length), Math.Max(radius1.Length, radius2.Length));
            if (FigureValidator.IsValid(ring))
            {
                return ring;
            }
            return null;
        }

        private Triangle CreateTriangle()
        {

            Console.WriteLine("Введите первую точку");
            var point1 = CreatePoint();

            Console.WriteLine("Введите вторую точку");
            var point2 = CreatePoint();

            Console.WriteLine("Введите третью точку");
            var point3 = CreatePoint();

            var triangle = new Triangle(point1, point2, point3);
            if (FigureValidator.IsValid(triangle))
            {
                return triangle;
            }
            return null;
        }

        private Rectangle CreateRectangle()
        {
            Console.WriteLine("Введите центр");
            var center = CreatePoint();

            Console.WriteLine("Введите первую сторону: ");
            var firstSide = CreateLine();

            Console.WriteLine("Введите вторую сторону: ");
            var secondSide = CreateLine();

            //return new Rectangle(center, firstSide.Length, secondSide.Length);
            var rectangle = new Rectangle(center, firstSide.Length, secondSide.Length);
            if (FigureValidator.IsValid(rectangle))
            {
                return rectangle;
            }
            return null;
        }

        private Square CreateSquare()
        {
            Console.WriteLine("Введите центр");
            var center = CreatePoint();

            Console.WriteLine("Введите сторону: ");
            var side = CreateLine();

            var square = new Square(center, side.Length);
            if (FigureValidator.IsValid(square))
            {
                return square;
            }

            return null;
        }
    }
}
