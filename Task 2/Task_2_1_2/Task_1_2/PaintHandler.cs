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
        Dictionary<User, List<AbstractGeometricObject>> userShapesPairs = new Dictionary<User, List<AbstractGeometricObject>>();
        //List<AbstractGeometricObject> shapes = new List<AbstractGeometricObject>();
        
        public PaintHandler(User user)
        {
            userShapesPairs.Add(user, new List<AbstractGeometricObject>());
        }

        public void AddShape(User user)
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
    7: Квадрат");

                isChoosing = false;
                currentCommand = Console.ReadLine();
                switch (currentCommand)
                {
                    case "1":
                        //shapes.Add(CreatePoint());
                        userShapesPairs[user].Add(CreatePoint());
                        break;
                    case "2":
                        //shapes.Add(CreateLine());
                        userShapesPairs[user].Add(CreateLine());
                        break;
                    case "3":
                        //shapes.Add(CreateCircle());
                        userShapesPairs[user].Add(CreateCircle());
                        break;
                    case "4":
                        //shapes.Add(CreateRing());
                        userShapesPairs[user].Add(CreateRing());
                        break;
                    case "5":
                        //shapes.Add(CreateTriangle());
                        userShapesPairs[user].Add(CreateTriangle());
                        break;
                    case "6":
                        //shapes.Add(CreateRectangle());
                        userShapesPairs[user].Add(CreateRectangle());
                        break;
                    case "7":
                        //shapes.Add(CreateSquare());
                        userShapesPairs[user].Add(CreateSquare());
                        break;
                    default:
                        Console.WriteLine("ВЫВОД: Выберите правильный вариант!");
                        isChoosing = true;
                        break;
                }
            }
        }

        public void ShowAllShapes(User user)
        {
            foreach (var shape in userShapesPairs[user])
            {
                Console.WriteLine(shape.Name);
            }
        }

        public void RemoveAllShapes(User user)
        {
            userShapesPairs[user].Clear();
        }

        public void AddUser(User user)
        {
            userShapesPairs.Add(user, new List<AbstractGeometricObject>());
        }

        public void AddUser(string username)
        {
            userShapesPairs.Add(new User { Name = username }, new List<AbstractGeometricObject>());
        }

        public void AddUser()
        {
            // Можно, конечно, написать одной строкой. Хоть код довольно и прост, но, возможно, это затруднит читаемость (но я в этом не уверен).
            // Рад был бы почитать об этом в отчёте об этом задании :)
            //userShapesPairs.Add(new User { Name = Console.ReadLine() }, new List<AbstractGeometricObject>());

            Console.WriteLine("ВЫВОД: Введите пользователя");
            Console.Write("ВВОД: ");
            string name = Console.ReadLine();
            User user = new User() { Name = name };

            if (!userShapesPairs.Keys.Contains(user))
            {
                userShapesPairs.Add(user, new List<AbstractGeometricObject>());
            }
            else
            {
                Console.WriteLine("ВЫВОД: Пользователь с таким именем уже существует!");
            }
        }

        public User ChangeUser()
        {
            Console.WriteLine("ВЫВОД: Доступные пользователи:");
            // Выцепляем всех пользователей
            var allUsers = userShapesPairs.Keys.ToArray();
            // Выводим их на экран
            for (int i = 0; i < allUsers.Length; i++)
            {
                Console.WriteLine($"{i + 1} : {allUsers[i]}");
            }
            
            // Пытаемся получить вразумительный ID от пользователя
            int userID = 0;
            while (true)
            {
                Console.WriteLine("ВЫВОД: Введите номер пользователя");
                Console.Write("ВВОД: ");

                if(int.TryParse(Console.ReadLine(), out userID))
                {
                    if(userID - 1 >= 0 && userID - 1 < allUsers.Length)
                    {
                        return allUsers[userID - 1];
                    }
                    else
                    {
                        Console.WriteLine("ВЫВОД: Введён неверный id пользователя");
                        continue;
                    }
                }
            }
        }

        private double InputCoordinate(string coordinateOrient)
        {
            while (true)
            {
                Console.Write($"ВВОД {coordinateOrient}: ");
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
                Console.Write($"ВВОД {coordinateOrient}: ");

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
            Console.WriteLine("ВЫВОД: Введите координату X: ");
            var x = InputCoordinate("X");

            Console.WriteLine("ВЫВОД: Введите координату Y: ");
            var y = InputCoordinate("Y");

            return new Point(x, y);
        }

        private Line CreateLine()
        {
            Console.WriteLine("ВЫВОД: Введите первую точку");
            var point1 = CreatePoint();

            Console.WriteLine("ВЫВОД: Введите вторую точку");
            var point2 = CreatePoint();
            
            var line = new Line(point1, point2);
            if (FigureValidator.IsValid(line))
            {
                return line;
            }

            return null;
        }

        private Circle CreateCircle()
        {
            Console.WriteLine("ВЫВОД: Введите центр");
            var center = CreatePoint();

            Console.WriteLine("ВЫВОД: Введите радиус: ");
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
            Console.WriteLine("ВЫВОД: Введите центр");
            var center = CreatePoint();

            Console.WriteLine("ВЫВОД: Введите радиус первой окружности: ");
            var radius1 = CreateLine();

            Console.WriteLine("ВЫВОД: Введите радиус второй окружности: ");
            var radius2 = CreateLine();

            var ring = new Ring(center, Math.Min(radius1.Length, radius2.Length), Math.Max(radius1.Length, radius2.Length));
            if (FigureValidator.IsValid(ring))
            {
                return ring;
            }

            return null;
        }

        private Triangle CreateTriangle()
        {

            Console.WriteLine("ВЫВОД: Введите первую точку");
            var point1 = CreatePoint();

            Console.WriteLine("ВЫВОД: Введите вторую точку");
            var point2 = CreatePoint();

            Console.WriteLine("ВЫВОД: Введите третью точку");
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
            Console.WriteLine("ВЫВОД: Введите центр");
            var center = CreatePoint();

            Console.WriteLine("ВЫВОД: Введите первую сторону: ");
            var firstSide = CreateLine();

            Console.WriteLine("ВЫВОД: Введите вторую сторону: ");
            var secondSide = CreateLine();

            var rectangle = new Rectangle(center, firstSide.Length, secondSide.Length);
            if (FigureValidator.IsValid(rectangle))
            {
                return rectangle;
            }

            return null;
        }

        private Square CreateSquare()
        {
            Console.WriteLine("ВЫВОД: Введите центр");
            var center = CreatePoint();

            Console.WriteLine("ВЫВОД: Введите сторону: ");
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
