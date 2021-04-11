using System;

namespace Task_3_1
{
    public static class Game
    {
        public enum InputOutput
        {
            Input,
            Output
        }

        public static void StartGame()
        {
            // Оставил, если будет интересно посмотреть с обычными именами, а не guid
            //List<Human> humans = new List<Human>
            //{
            //    new Human{Name = "Tasker"},
            //    new Human{Name = "Person2"},
            //    new Human{Name = "Markus"},
            //    new Human{Name = "Polo"},
            //    new Human{Name = "Just"},
            //    new Human{Name = "Testy"},
            //    new Human{Name = "Some"}
            //};
            //CircleList<Human> cList = new CircleList<Human>(humans.Select(human => (Human)human.Clone()));

            PrintMessage("Введите N");
            PrintMessage(messageType: InputOutput.Input);
            int peopleCount = 0;
            if (!int.TryParse(Console.ReadLine(), out peopleCount))
            {
                throw new Exception("Неверно введено число!");
            }
            CircleList<Human> cList = new CircleList<Human>(HumanGenerator.GenerateHumans(peopleCount));

            PrintMessage("Введите, какой по счёту человек будет вычеркнут каждый раунд:");
            PrintMessage(messageType: InputOutput.Input);
            int indexDeleter = 0;
            if (!int.TryParse(Console.ReadLine(), out indexDeleter))
            {
                throw new Exception("Неверно введено число!");
            }
            WeakestLink<Human> weakLink = new WeakestLink<Human>(indexDeleter, cList);
            weakLink.StartWeakestLink();
        }
        
        public static void PrintMessage(string message = "", InputOutput messageType = InputOutput.Output)
        {
            switch (messageType)
            {
                case InputOutput.Input:
                    Console.Write($"ВВОД: {message}");
                    break;
                case InputOutput.Output:
                    Console.WriteLine($"ВЫВОД: {message}");
                    break;
            }
        }
    }
}
