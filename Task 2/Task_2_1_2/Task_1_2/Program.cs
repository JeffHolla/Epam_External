using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

/*
 * Напишите класс, задающий круг с указанными координатами центра, радиусом, а также
 * свойствами, позволяющими узнать длину описанной окружности и площадь круга.
 * 
 * Кроме этого, создайте класс, описывающий кольцо, заданное координатами центра, внешним и
 * внутренним радиусами, а также свойствами, позволяющими узнать площадь кольца и суммарную
 * длину внешней и внутренней окружностей.
 * 
 * Подумайте над взаимосвязью этих сущностей, возможной иерархией. Задача – максимально
 * сократить повтор кода в рамках задания.
 * 
 * По аналогии опишите классы других фигур. На их основе реализуйте собственный графический
 * редактор, который взаимодействует с кольцами, окружностями, кругами, прямоугольниками,
 * квадратами, треугольниками и линиями.
 * 
 * Пользователю доступны следующие действия:
 * - добавить фигуру (предварительно введя её характеристики)
 * - вывести все фигуры на экран (вывести список фигур и их характеристик)
 * - очистить холст (удалить все фигуры)
 * 
 * Требование корректности характеристик фигур на каждом этапе неизменно, помните об этом!
 */


namespace Task_1_2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Или лучше вынести всю логику Пользователей в Handler?
            Console.WriteLine("ВЫВОД: Введите имя пользователя");
            Console.Write("ВВОД: ");
            User currentUser = new User()
            {
                Name = Console.ReadLine()
            };

            PaintHandler paintHandler = new PaintHandler(currentUser);

            string currentCommand = "";
            while (currentCommand != "0")
            {
                Console.Clear();
                Console.WriteLine(
                    $@"Текущий пользователь : {currentUser}
Введите комманду : 
    1: Добавить фигуру
    2: Вывести все фигуры
    3: Очистить список фигур
    4: Добавить пользователя
    5: Сменить пользователя
    0: Выход
");
                currentCommand = Console.ReadLine().Trim();
                switch (currentCommand)
                {
                    case "1":
                        paintHandler.AddShape(currentUser);
                        break;
                    case "2":
                        paintHandler.ShowAllShapes(currentUser);
                        break;
                    case "3":
                        paintHandler.RemoveAllShapes(currentUser);
                        break;
                    case "4":
                        paintHandler.AddUser();
                        break;
                    case "5":
                        currentUser = paintHandler.ChangeUser();
                        break;

                    case "0":
                        currentCommand = "0";
                        break;
                }
                Console.ReadKey();
            }

            Console.ReadKey();
        }

    }
}
