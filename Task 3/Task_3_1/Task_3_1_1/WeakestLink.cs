using System;

namespace Task_3_1
{
    // Содержит основную логику задания
    public class WeakestLink<T>
    {
        // Наверное лучше его как-то сократить
        // Это уводит программу в рекурсию
        //public int IndexToDel {
        //    get
        //    {
        //        return IndexDeleter;
        //    }
        //    set
        //    {
        //        if (value < 0)
        //        {
        //            throw new IndexOutOfRangeException("Недопустимый индекс!");
        //        }
        //        else
        //        {
        //            IndexDeleter = value;
        //        }
        //    }
        //}

        CircleList<T> list;

        public int IndexDeleter { get; set; }

        private int CurrentIndex { get; set; }
        private int RoundCounter = 0;

        public WeakestLink(int indexToDel, CircleList<T> list)
        {
            this.list = list;

            IndexDeleter = indexToDel;
            CurrentIndex = 0;

            CheckCorrectnessGame();
        }

        public void StartWeakestLink()
        {
            while (list.Length >= IndexDeleter)
            {
                list.RemoveAt(IndexDeleter);

                //Console.WriteLine($"Раунд {RoundCounter}. Вычеркнут человек. Людей осталось: {list.Length}");
                Game.PrintMessage($"Раунд {RoundCounter}. Вычеркнут человек. Людей осталось: {list.Length}");


                CurrentIndex += IndexDeleter;
                RoundCounter += 1;
            }
        }

        private void CheckCorrectnessGame()
        {
            if (list.Length < IndexDeleter)
            {
                throw new Exception("Невозможно удалить пользователя по индексу, если индекс больше количества пользователей!");
            }

            if (IndexDeleter < 0)
            {
                throw new IndexOutOfRangeException("Недопустимый индекс!");
            }
        }
    }
}
